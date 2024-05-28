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

    [SerializeField] GameObject shootingPoint;
    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }

    private void Start()
	{
        player = GameObject.Find("PlayerTD");
        MoveToRandomSpot();
        timeToWait = maxTimeToWait;
    }

	private void Update()
	{
        logick();
    }


	void logick()
	{
        switch (currentState)
        {
            case "idle":

                if (!agent.pathPending && agent.remainingDistance < 7f)
                {
                    if (!inWait)
                    {
                        StartCoroutine(waitBeforeGotoNewPoint());
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

                if (fov.seePlayer && distance < 10)
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


                    shooting();
                

                if (fov.seePlayer)
                {
                    lookAtPlayerWhenAttack();

                    float distance1 = Vector3.Distance(transform.position, player.transform.position);


                    if (distance1 > 12)
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

}

