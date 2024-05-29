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
    void Start()
    {
        Health = MaxHealth;
        minusHealth = FindObjectOfType<minusHealthVisual>();
        _timeController = FindObjectOfType<timeController>();
    }

    public void TakeDamage(int damage)
    {
		if (isPlayer)
		{
            Health -= damage;
            minusHealth.DestroyLastCounted();
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
