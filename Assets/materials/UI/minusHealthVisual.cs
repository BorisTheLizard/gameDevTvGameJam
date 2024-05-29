using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minusHealthVisual : MonoBehaviour
{
    public GameObject[] objectsToCount; // Array of game objects to count
    public int count = 0; // Counter for number of objects counted

    private void Start()
    {
        // Get all child objects of this object and add them to the objectsToCount array
        int childCount = transform.childCount;
        objectsToCount = new GameObject[childCount];
        count = childCount;
        for (int i = 0; i < childCount; i++)
        {
            objectsToCount[i] = transform.GetChild(i).gameObject;
        }
    }

    public void DestroyLastCounted()
    {
        if (count > 0)
        {
            count--;
            GameObject lastObject = objectsToCount[count];
            lastObject.GetComponentInChildren<Animator>().SetTrigger("hearthMinus");
        }
    }
    public void PlusHealth()
	{
        if (count < objectsToCount.Length)
        {
            GameObject lastObject = objectsToCount[count];
            lastObject.GetComponentInChildren<Animator>().SetTrigger("hearthPlus");
            count++;
        }
    }
}
