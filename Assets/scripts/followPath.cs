using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPath : MonoBehaviour
{
    public CinemachinePathBase path;
    public float speed = 5f;
    public float startingDistance = 0f; // The starting distance along the path
    private float distanceTravelled;

    void Start()
    {
        // Initialize the starting distance
        distanceTravelled = startingDistance;

        if (path != null)
        {
            // Set the initial position and rotation based on the starting distance
            var initialPosition = path.EvaluatePositionAtUnit(distanceTravelled, CinemachinePathBase.PositionUnits.Distance);
            transform.position = initialPosition;

            var initialRotation = path.EvaluateOrientationAtUnit(distanceTravelled, CinemachinePathBase.PositionUnits.Distance);
            transform.rotation = initialRotation;
        }
    }

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