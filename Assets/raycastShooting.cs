using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastShooting : MonoBehaviour
{
    public GameObject shootingPoint; // Assign this in the Inspector
    public float shootingRange = 100f; // The range of the raycast
    public LayerMask ignoreLayerMask;

    GameObjectPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 shootingDirection = ray.direction;
        RaycastHit hit;

        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(shootingPoint.transform.position, shootingDirection * shootingRange, Color.red, 2f);

        if (Physics.Raycast(shootingPoint.transform.position, shootingDirection, out hit, shootingRange,~ignoreLayerMask))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.gameObject.tag == "destr")
			{
                GameObject destrObjEffect = pool.GetObject(1);
                if (destrObjEffect != null)
                {
                    destrObjEffect.SetActive(false);
                    destrObjEffect.transform.position = hit.point;

                    destrObjEffect.SetActive(true);
                }

                hit.transform.gameObject.GetComponent<destroyObjectScript>().destroyIt();
                hit.transform.gameObject.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;
            }

            if (hit.collider.gameObject.tag == "obstacles" || hit.collider.gameObject.tag == "ground")
            {
                GameObject destrObjEffect = pool.GetObject(2);
                if (destrObjEffect != null)
                {
                    destrObjEffect.SetActive(false);
                    destrObjEffect.transform.position = hit.point;
                    destrObjEffect.SetActive(true);
                }
            }
        }
    }
}
