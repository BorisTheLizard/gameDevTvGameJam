using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countBullets : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	AttackSystem attack;
	[SerializeField] bool chaseScene;
	raycastShooting _shootinRays;

	private void Awake()
	{
		if (!chaseScene)
		{
			attack = FindObjectOfType<AttackSystem>();
		}
		else
		{
			_shootinRays = FindObjectOfType<raycastShooting>();
		}
	}

	private void Update()
	{
		if (!chaseScene)
		{
			text.text = "x" + attack.bulletsInClip.ToString();
		}
		else
		{
			text.text = "x" + _shootinRays.bulletsInClip.ToString();
		}
	}
}
