using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    [SerializeField] int damageAmount;

    [SerializeField] float timeToWait;
    [SerializeField] float bulletSpeed = 100;
    GameObjectPool pool;
    TrailRenderer trail;

    public bool isSuperBullet;
    [SerializeField] float searchRadius = 25;
    public int jumps = 0;
    [SerializeField] LayerMask enemyLayer;

    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
		if (!isSuperBullet)
		{
            StartCoroutine(returnToPoolTimer(1));
        }
		else
		{
            StartCoroutine(returnToPoolTimer(6));
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        trail.Clear();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "obstacles":
				if (isSuperBullet)
				{
                    bulletJumps();

                    GameObject hitImpact = pool.GetObject(2);

                    if (hitImpact != null)
                    {
                        hitImpact.SetActive(false);
                        hitImpact.transform.position = transform.position;
                        hitImpact.SetActive(true);
                    }
                }
				else
				{
                    GameObject hitImpact = pool.GetObject(2);

                    if (hitImpact != null)
                    {
                        hitImpact.SetActive(false);
                        hitImpact.transform.position = transform.position;
                        hitImpact.SetActive(true);
                    }
                    returntItToPool();
                }


                break;

            case "Player":

                if (isSuperBullet)
                {

				}
                else
				{
                    other.gameObject.GetComponent<healthSystem>().TakeDamage(damageAmount);

                    GameObject PlayerhitImpact = pool.GetObject(3);

                    if (PlayerhitImpact != null)
                    {
                        PlayerhitImpact.SetActive(false);
                        PlayerhitImpact.transform.position = transform.position;
                        PlayerhitImpact.transform.rotation = transform.rotation;
                        PlayerhitImpact.SetActive(true);
                    }

                    returntItToPool();
                }


                break;

            case "enemy":

                if (isSuperBullet)
                {
                    other.gameObject.GetComponent<healthSystem>().TakeDamage(2);
                    bulletJumps();
                    GameObject hitImpact = pool.GetObject(2);

                    if (hitImpact != null)
                    {
                        hitImpact.SetActive(false);
                        hitImpact.transform.position = transform.position;
                        hitImpact.SetActive(true);
                    }
                }
				else
				{
                    other.gameObject.GetComponent<healthSystem>().TakeDamage(damageAmount);

                    GameObject PlayerhitImpact1 = pool.GetObject(3);

                    if (PlayerhitImpact1 != null)
                    {
                        PlayerhitImpact1.SetActive(false);
                        PlayerhitImpact1.transform.position = transform.position;
                        PlayerhitImpact1.transform.rotation = transform.rotation;
                        PlayerhitImpact1.SetActive(true);
                    }

                    returntItToPool();
                }


                break;


            case "destr":

                if (isSuperBullet)
                {
                    other.GetComponent<destroyObjectScript>().destroyIt();
                    other.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;

                    bulletJumps();

                    GameObject hitImpact = pool.GetObject(2);

                    if (hitImpact != null)
                    {
                        hitImpact.SetActive(false);
                        hitImpact.transform.position = transform.position;
                        hitImpact.SetActive(true);
                    }
                }
				else
				{
                    GameObject destrObjEffect = pool.GetObject(1);
                    if (destrObjEffect != null)
                    {
                        destrObjEffect.SetActive(false);
                        destrObjEffect.transform.position = transform.position;
                        destrObjEffect.transform.rotation = transform.rotation;
                        destrObjEffect.SetActive(true);
                    }

                    other.GetComponent<destroyObjectScript>().destroyIt();
                    other.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;

                    returntItToPool();
                }


                break;
        }

       
    }

    IEnumerator returnToPoolTimer(float returnWait)
    {
        yield return new WaitForSeconds(returnWait);
        trail.Clear();
        returntItToPool();
    }

    void returntItToPool()
	{
		if (isSuperBullet)
		{
            jumps = 0;
		}
        pool.ReturnObject(this.gameObject);

    }

    void bulletJumps()
    {
        if (jumps < 2)
        {
            Collider[] col = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);

            for (int i = 0; i < col.Length; i++)
            {
                if (col[0] != null)
                {
                    jumps++;
                    // Calculate direction to the target
                    Vector3 direction = col[i].transform.position - transform.position;
                    direction.y = 0; // Keep direction on the XZ plane
                    if (direction != Vector3.zero)
                    {
                        // Rotate bullet to look at the target
                        transform.rotation = Quaternion.LookRotation(direction);
                    }

                    Debug.Log(jumps);

                    break; // Exit the loop after first jump
                }
            }
        }
        else
        {
            returntItToPool();
        }
    }
}
