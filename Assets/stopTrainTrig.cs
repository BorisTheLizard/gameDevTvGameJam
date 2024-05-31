using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class stopTrainTrig : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] CinemachineVirtualCamera cam;
	[SerializeField] BoxCollider trigger;
	[SerializeField] AudioClip stopTrainClip;
	[SerializeField] AudioSource _audiosource;
	[SerializeField] GameObject trainSoundHolder;


	bool stopShaking = false;

	private void Update()
	{
		if (stopShaking)
		{
			if (cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain > 0)
			{
				cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain, 0, 0.5f * Time.deltaTime);
				cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Mathf.Lerp(cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain, 0, 0.5f * Time.deltaTime);
			}

		}
	}



	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			anim.SetTrigger("stopTrain");
			stopShaking = true;
			trigger.enabled = false;
			_audiosource.Stop();
			_audiosource.PlayOneShot(stopTrainClip);
			StartCoroutine(disableAudiosource());
		}
	}
	IEnumerator disableAudiosource()
	{
		yield return new WaitForSeconds(8);
		_audiosource.gameObject.SetActive(!true);
		trainSoundHolder.SetActive(false);
		this.enabled = false;
	}
}
