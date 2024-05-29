using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class healthSystem : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    public bool isDead = false;

    [Header("Objects Types")]
    public bool isEnemy;
    public bool isPlayer;


    [Header("references")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] topDownAI ai;
    [SerializeField] CapsuleCollider col;
    [SerializeField] Animator anim;
    [SerializeField] Animator deathAnimator;
    minusHealthVisual minusHealth;
    timeController _timeController;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] GameObject coocedScreen;

    [SerializeField] bool isTopDown;
    [SerializeField] bool notRunner;
    playerController _TDcontroller;
    AttackSystem attackSystem;


    void Start()
    {
        Health = MaxHealth;
        minusHealth = FindObjectOfType<minusHealthVisual>();
        _timeController = FindObjectOfType<timeController>();

		if (isTopDown)
		{
            _TDcontroller = FindObjectOfType<playerController>();
		}
		if (notRunner)
		{
            attackSystem = FindObjectOfType<AttackSystem>();
		}
    }

    public void TakeDamage(int damage)
    {
		if (isPlayer)
		{
            Health -= damage;
            minusHealth.DestroyLastCounted();

			if (Health <= 0)
			{
				if (!isDead)
				{
                    isDead = true;
                    _timeController.setTime(1);


                    if (isTopDown)
					{
                        _TDcontroller.enabled = false;
					}

					if (notRunner)
					{
                        attackSystem.enabled = false;
					}
					else
					{
                        //IF IT"S RUNNER!!!!!!!
					}

                    coocedScreen.SetActive(true);
				}
			}
        }

		if (isEnemy)
		{
            Health -= damage;

			if (ai.currentState == "idle")
			{
                ai.currentState = "chase";
			}
  
            if (Health <= 0)
            {
                if (!isDead)
                {
                    _audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
                    _audioSource.Play();
                    isDead = true;
                    ai.StopAllCoroutines();
                    ai.enabled = false;
                    agent.enabled = false;
                    col.enabled = false;
                    anim.enabled = false;
                    anim.gameObject.SetActive(false);
                    deathAnimator.gameObject.SetActive(true);
                    deathAnimator.SetTrigger("death");
                    _timeController.addEnergy();
                }
            }

        }
    }

    public void RestoreHealth(int heal)
    {
        if (Health < MaxHealth)
        {
            Health += heal;
            minusHealth.PlusHealth();
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

}
