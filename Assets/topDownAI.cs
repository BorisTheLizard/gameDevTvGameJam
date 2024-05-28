using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class topDownAI : MonoBehaviour
{
    public NavMeshAgent agent;
    private bool inWait = false;
    public float wanderRadius = 10f;
    public bool isAttacking = false;
    public bool fastRotate = false;

    [SerializeField] fieldOfView fov;

    [SerializeField] Animator anim;

    public string currentState = "idle";

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] idle;
    [SerializeField] AudioClip[] attak;
    public AudioClip[] death;

    GameObject player;

    public float maxTimeToWait = 0.3f;
    float timeToWait;

    GameObjectPool pool;

    public bool isPatroling;
    public GameObject patrollingPoint;

    [SerializeField] GameObject shootingPoint;
    bool isAttackActing=false;
    bool isRight;
    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }
    private void Start()
	{
        player = GameObject.Find("PlayerTD");

		if (isPatroling)
		{
            agent.SetDestination(patrollingPoint.transform.position);
		}
		else
		{
            MoveToRandomSpot();
        }
        
        timeToWait = maxTimeToWait;
    }
	private void Update()
	{
        logick();
    }
	private void FixedUpdate()
	{
        animationController();
    }
	void logick()
	{
        switch (currentState)
        {
            case "idle":

				if (!isPatroling)
				{
                    if (!agent.pathPending && agent.remainingDistance < 7f)
                    {
                        if (!inWait)
                        {
                            StartCoroutine(waitBeforeGotoNewPoint());
                        }
                    }
                }

                if (fov.seePlayer)
                {
                    currentState = "chase";
                }


                break;


            case "chase":

                float distance = Vector3.Distance(transform.position, player.transform.position);
                agent.SetDestination(player.transform.position);

                if (fov.seePlayer && distance < 14)
                {
                    currentState = "attack";
                }


                if (inWait)
                {
                    StopCoroutine(waitBeforeGotoNewPoint());
                    inWait = false;
                }

                break;

            case "attack":

                agent.velocity = Vector3.zero;

				if (!isAttackActing)
				{
                    StartCoroutine(callAttackActions(2));
				}

                shooting();
                

                if (fov.seePlayer)
                {
                    lookAtPlayerWhenAttack();

                    float distance1 = Vector3.Distance(transform.position, player.transform.position);


                    if (distance1 > 16)
                    {
                  
                        currentState = "chase";
                    }

                }
                else
                {
                    currentState = "chase";
                }

                break;
        }
    }
    void shooting()
	{
        if (Time.time > timeToWait)
        {
            timeToWait = Time.time + maxTimeToWait;
            GameObject obj = pool.GetObject(0);
            if (obj != null)
            {
                obj.SetActive(false);
                obj.transform.position = shootingPoint.transform.position;
                obj.transform.rotation = shootingPoint.transform.rotation;
                obj.SetActive(true);
            }
        }
    }
    private void lookAtPlayerWhenAttack()
    {
        Vector3 lookTransform = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookTransform);
    }
    void MoveToRandomSpot()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
    IEnumerator waitBeforeGotoNewPoint()
    {
        inWait = true;
        float timeToWait = Random.Range(2f, 10f);
        yield return new WaitForSeconds(timeToWait);
        MoveToRandomSpot();
        inWait = false;
    }
    public void heardNoise()
    {
		if (currentState == "idle")
		{
            StartCoroutine(FastRotateCoroutine(player.transform.position, 0.1f));
            currentState = "chase";
		}
    }
    public IEnumerator FastRotateCoroutine(Vector3 lookAtTransform, float rotationTime) //rotates agent to face something (noise or player)
    {
        Vector3 lookTransform = new Vector3(lookAtTransform.x, transform.position.y, lookAtTransform.z);
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(lookTransform - transform.position);

        float elapsedTime = 0f;
        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
    public void animationController()
	{
        float moveingSpeed = agent.velocity.magnitude;
        anim.SetFloat("moveSpeed", moveingSpeed);
    }

    IEnumerator callAttackActions(float timeToWait)
	{
        isAttackActing = true;
        AttackActions();
        yield return new WaitForSeconds(timeToWait);
        isAttackActing = false;

	}
    private void AttackActions()
    {
        int actionNum = Random.Range(1, 4);

        //croach right
        if (actionNum == 2)
        {
            isRight = true;
            StartCoroutine(PushAsideCoroutine(isRight, 1.7f, 8, false));
        }

        //croach left
        if (actionNum == 3)
        {
            isRight = !true;
            StartCoroutine(PushAsideCoroutine(isRight, 1.7f, 8, false));
        }
    }

    IEnumerator PushAsideCoroutine(bool right, float duration, float distance, bool pushForward) //push agent aside when trigger colides with other agent
    {
        float timer = 0.0f;
        Vector3 originalPosition = agent.transform.position;
        Vector3 sideMovement = (right ? transform.right : -transform.right) * distance;

        //Vector3 targetPosition = originalPosition + sideMovement;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Calculate the side movement

            Vector3 sideOffset = Vector3.Lerp(Vector3.zero, sideMovement, progress);

            // Calculate the forward movement if needed
            Vector3 forwardOffset = Vector3.zero;
            if (pushForward)
            {
                // Calculate the forward movement based on the side movement
                forwardOffset = Vector3.Lerp(Vector3.zero, agent.transform.forward * distance, progress);
            }

            // Calculate the new position considering side and forward movement
            Vector3 newPosition = originalPosition + sideOffset + forwardOffset;

            // Move the agent to the new position
            agent.Move(newPosition - agent.transform.position);

            yield return null;
        }
    }
}

