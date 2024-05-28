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
        Debug.Log(other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "obstacles":

                GameObject hitImpact = pool.GetObject(2);

                if (hitImpact != null)
                {
                    hitImpact.SetActive(false);
                    hitImpact.transform.position = transform.position;
                    hitImpact.SetActive(true);
                }
                pool.ReturnObject(this.gameObject);

                break;

            case "Player":

                //Damage Player
                //other.gameObject.GetComponent<HealthSystem>().TakeDamage(damageAmount);

                GameObject PlayerhitImpact = pool.GetObject(3);

                if (PlayerhitImpact != null)
                {
                    PlayerhitImpact.SetActive(false);
                    PlayerhitImpact.transform.position = transform.position;
                    PlayerhitImpact.transform.rotation = transform.rotation;
                    PlayerhitImpact.SetActive(true);
                }

                pool.ReturnObject(this.gameObject);

                break;

            case "enemy":

                GameObject PlayerhitImpact1 = pool.GetObject(3);

                if (PlayerhitImpact1 != null)
                {
                    PlayerhitImpact1.SetActive(false);
                    PlayerhitImpact1.transform.position = transform.position;
                    PlayerhitImpact1.transform.rotation = transform.rotation;
                    PlayerhitImpact1.SetActive(true);
                }

                pool.ReturnObject(this.gameObject);

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

                pool.ReturnObject(this.gameObject);

                break;
        }
    }

    IEnumerator returnToPoolTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        trail.Clear();
        pool.ReturnObject(this.gameObject);
    }
}
