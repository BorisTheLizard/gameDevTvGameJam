using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyBook : MonoBehaviour
{
	[SerializeField] GameObject stroyText;

	public void openStory() 
	{
		stroyText.SetActive(true);
	}
	public void closeStory()
	{
		stroyText.SetActive(!true);
	}
}
