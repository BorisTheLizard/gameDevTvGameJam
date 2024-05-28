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

    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
		if (isPlayer)
		{
            Health -= damage;

        }

		if (isEnemy)
		{
            Health -= damage;
  
            if (Health <= 0)
            {
                if (!isDead)
                {
                    isDead = true;
                    ai.StopAllCoroutines();
                    ai.enabled = false;
                    agent.enabled = false;
                    col.enabled = false;
                    anim.enabled = false;
                    anim.gameObject.SetActive(false);
                    deathAnimator.gameObject.SetActive(true);
                    deathAnimator.SetTrigger("death");
                }
            }

        }
    }

    public void RestoreHealth(int heal)
    {
        if (Health < MaxHealth)
        {
            Health += heal;
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

}
