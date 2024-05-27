using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    [SerializeField] int wallHitImpactIndex;
    [SerializeField] int HitImpactIndex;
    [SerializeField] int damageAmount;

    [SerializeField] float timeToWait;
    [SerializeField] float bulletSpeed = 100;
    GameObjectPool pool;
    TrailRenderer trail;

    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(returnToPoolTimer());
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
                GameObject hitImpact = pool.GetObject(wallHitImpactIndex);
                if (hitImpact != null)
                {
                    hitImpact.SetActive(false);
                    hitImpact.transform.position = transform.position;
                    hitImpact.transform.rotation = transform.rotation;
                    hitImpact.SetActive(true);
                }
                pool.ReturnObject(this.gameObject);
                break;

            case "player":
                //Damage Player
                //other.gameObject.GetComponent<HealthSystem>().TakeDamage(damageAmount);
                //Invoke effect from pool
                GameObject PlayerhitImpact = pool.GetObject(HitImpactIndex);
                if (PlayerhitImpact != null)
                {
                    PlayerhitImpact.SetActive(false);
                    PlayerhitImpact.transform.position = transform.position;
                    PlayerhitImpact.transform.rotation = transform.rotation;
                    PlayerhitImpact.SetActive(true);
                }
                //Return bullet in pool
                pool.ReturnObject(this.gameObject);
                break;

            case "enemy":
/*                if (other.GetComponent<HealthSystem>() != null)
                {
                    other.gameObject.GetComponent<HealthSystem>().TakeDamage(damageAmount);
                    GameObject EnemyhitImpact = pool.GetObject(HitImpactIndex);
                    if (EnemyhitImpact != null)
                    {
                        EnemyhitImpact.SetActive(false);
                        EnemyhitImpact.transform.position = transform.position;
                        EnemyhitImpact.transform.rotation = transform.rotation;
                        EnemyhitImpact.SetActive(true);
                    }
                }
                pool.ReturnObject(this.gameObject);*/
                break;
            case "destr":
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

                break;
        }
    }

    IEnumerator returnToPoolTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        //trail.Clear();
        pool.ReturnObject(this.gameObject);
    }
}
