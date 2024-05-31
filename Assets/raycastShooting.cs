using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class raycastShooting : MonoBehaviour
{
    public GameObject shootingPoint; // Assign this in the Inspector
    public float shootingRange = 100f; // The range of the raycast
    public LayerMask ignoreLayerMask;

    GameObjectPool pool;

    AudioSource audioSource;
    [SerializeField] AudioClip[] ShootClips;
    [SerializeField] AudioClip reloadSound;

    public int bulletsInClip = 5;
    public int maxBulletsInClip = 6;
    bool isReloading;
    CinemachineImpulseSource impulse;

    [SerializeField] GameObject targetPoint;
    [SerializeField] AudioClip shootingSound;

    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
        audioSource = GetComponent<AudioSource>();

        bulletsInClip = maxBulletsInClip;

        impulse = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        SetTargetPointPosition();

        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {

            if (bulletsInClip > 0 && !isReloading)
            {
                Shoot();
            }
        }

        if (!isReloading)
        {
            reloadGun();
        }
    }

    void Shoot()
    {
        bulletsInClip--;
        impulse.GenerateImpulse();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        audioSource.PlayOneShot(shootingSound);
        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(Camera.main.transform.position, ray.direction * shootingRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, shootingRange, ~ignoreLayerMask))
        {
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

    void reloadGun()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletsInClip < maxBulletsInClip)
            {
                audioSource.PlayOneShot(reloadSound);
                isReloading = true;
                StartCoroutine(reload());

                IEnumerator reload()
                {
                    yield return new WaitForSeconds(0.7f);
                    bulletsInClip = maxBulletsInClip;
                    isReloading = false;
                }
            }
        }
    }

    void SetTargetPointPosition()
    {
        // Perform the raycast
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Debug the ray in the scene view
        Debug.DrawRay(ray.origin, ray.direction * 999, Color.red);

        if (Physics.Raycast(ray, out hit, 999))
        {
            if (targetPoint != null)
            {
                targetPoint.transform.position = hit.point;
            }
        }
    }
}
