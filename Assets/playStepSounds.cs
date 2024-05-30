using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playStepSounds : MonoBehaviour
{
	[SerializeField] AudioSource _audiosource;
	[SerializeField] AudioClip[] _clip;

	public void playStep()
	{
		_audiosource.clip = _clip[Random.Range(0, _clip.Length)];
		_audiosource.Play();
	}
}
