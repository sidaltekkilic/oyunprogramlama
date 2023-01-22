using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class TrainMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float trainSpeed = 0.05f;
    //float distanceTravelled;
    public float startTime = 0;
    public GameObject trainStopButton;
    public GameObject trainStartButton;
    float timeTravelled = 0;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            timeTravelled = startTime;
        }
    }

    void Update()
    {
        if (pathCreator != null)
        {
            //distanceTravelled += speed * Time.deltaTime;
            //transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            timeTravelled += trainSpeed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtTime(timeTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotation(timeTravelled, endOfPathInstruction);
        }
      
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        //distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    public float GetCurrentTimeTravelled()
    {
        return timeTravelled;
    }

    public void StopMovement()
    {
        trainSpeed = 0;
    }

    public void StopClick()
    {
        trainSpeed= 0;
        trainStopButton.SetActive(false);
        trainStartButton.SetActive(true);

    }
    public void StartClick()
    {
        trainSpeed = 0.05f;
        trainStopButton.SetActive(true);
        trainStartButton.SetActive(false);
    }
}