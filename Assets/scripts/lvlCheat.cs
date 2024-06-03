using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlCheat : MonoBehaviour
{

    sceneManagement _sm;
    void Start()
    {
        _sm = FindObjectOfType<sceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K)&& Input.GetKeyDown(KeyCode.L))
		{
            _sm.LoadNextLevel();
		}
    }
}
