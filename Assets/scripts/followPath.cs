using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class followPath : MonoBehaviour
{
    public CinemachinePathBase path;
    public float speed = 5f;
    private float distanceTravelled;

    void Update()
    {
        if (path != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            var positionOnPath = path.EvaluatePositionAtUnit(distanceTravelled, CinemachinePathBase.PositionUnits.Distance);
            transform.position = positionOnPath;

            // If you want to rotate along the path
            var rotationOnPath = path.EvaluateOrientationAtUnit(distanceTravelled, CinemachinePathBase.PositionUnits.Distance);
            transform.rotation = rotationOnPath;
        }
    }
}
