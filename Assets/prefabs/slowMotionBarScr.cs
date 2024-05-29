using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slowMotionBarScr : MonoBehaviour
{
    timeController timeContr;
    public Image fillImage;

	private void Awake()
	{
        timeContr = FindAnyObjectByType<timeController>();
    }

	public void Update()
    {
        float currentEnergy = timeContr.energy;
        float maxEnergy = timeContr.MaxEnergy;
        fillImage.fillAmount = currentEnergy / maxEnergy;
    }
}
