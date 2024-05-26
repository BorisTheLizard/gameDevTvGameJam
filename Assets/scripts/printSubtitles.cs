using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class printSubtitles : MonoBehaviour
{
	string[] subtitles = new string[5];

	float totalTimeRemaining;

	[SerializeField] GameObject _ScreenSubsHolder;

	bool isPrinting = false;

	public float printInterval = 0.1f;

	TMP_Text[] SubtitlesTextLayers;

	public float extraTimeHoldSubtitles = 3;

	[SerializeField] AudioSource _audiosource;
	[SerializeField] AudioClip[] voiceClips;


	private void Awake()
	{
		subtitles[0] = "Never thought I’d come ‘round these parts again. Not after Sunny... Sigh.";
		subtitles[1] = "Show yourself, Benedict Bad! Sunny may be gone, but her husband ain’t! Let me give it to you like you gave it to her!";
		subtitles[2] = "I ain’t here to chit-chat with the likes of you. Put ‘em up, Benedict.";
		subtitles[3] = "Try chasing me now, you crowbait!";
		subtitles[4] = "No more running, Benedict. Let’s settle this like real men.";
	}

	private void Start()
	{
		int SubtitlesLayersCount = _ScreenSubsHolder.transform.childCount;
		SubtitlesTextLayers = new TMP_Text[SubtitlesLayersCount];
		for (int i = 0; i < SubtitlesLayersCount; i++)
		{
			SubtitlesTextLayers[i] = _ScreenSubsHolder.transform.GetChild(i).gameObject.GetComponent<TMP_Text>();
		}
	}

	public void showSubtitles(int subtitleIndex)
	{

		if (_ScreenSubsHolder.activeSelf == false)
		{
			if (!isPrinting)
			{
				slowPrintSubtitles();
			}
		}
		else
		{
			if (!isPrinting)
			{
				_ScreenSubsHolder.GetComponent<disableOnEnable>().StopAllCoroutines();
				_ScreenSubsHolder.SetActive(false);
				slowPrintSubtitles();
			}
		}


		void slowPrintSubtitles()
		{

			totalTimeRemaining = subtitles[subtitleIndex].Length * printInterval;

			//reset disable on enable counter
			_ScreenSubsHolder.SetActive(true);
			_ScreenSubsHolder.GetComponent<disableOnEnable>().timeToWait = totalTimeRemaining + extraTimeHoldSubtitles;
			_ScreenSubsHolder.GetComponent<disableOnEnable>().StopAllCoroutines();
			_ScreenSubsHolder.GetComponent<disableOnEnable>().StartCoroutine(_ScreenSubsHolder.GetComponent<disableOnEnable>().counter());

			StartCoroutine(PrintText(subtitles[subtitleIndex]));


			IEnumerator PrintText(string text)
			{
				isPrinting = true;
				// Clear the text initially
				foreach (TMP_Text layer in SubtitlesTextLayers)
				{
					layer.text = "";
				}

				for (int i = 0; i < text.Length; i++)
				{
					// Add the next character to the display text
					foreach (TMP_Text layer in SubtitlesTextLayers)
					{
						layer.text += text[i];
						if (!_audiosource.isPlaying)
						{
							_audiosource.clip = voiceClips[Random.Range(0, voiceClips.Length)];
							_audiosource.Play();
						}
					}
					// Wait for the specified interval before printing the next character
					yield return new WaitForSeconds(printInterval);
				}
				//yield return new WaitForSeconds(extraTimeHoldSubtitles);
				isPrinting = !true;
			}

		}
	}

}
