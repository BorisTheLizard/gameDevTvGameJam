using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        pool = FindObjectOfType<GameObjectPool>();
        audioSource = GetComponent<AudioSource>();

        bulletsInClip = maxBulletsInClip;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {

            if (bulletsInClip > 0)
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(Camera.main.transform.position, ray.direction * shootingRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, shootingRange, ~ignoreLayerMask))
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
                    yield return new WaitForSeconds(1f);
                    bulletsInClip = maxBulletsInClip;
                    isReloading = false;
                }
            }
        }
    }
}
