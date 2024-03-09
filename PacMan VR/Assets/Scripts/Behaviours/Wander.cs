using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Wander : MonoBehaviour
{
    public Transform[] targets;
    public float wanderSpeed = 3.5f; // Normal speed when not sneaking
    private NavMeshAgent agent;
    private int currentTargetIndex;
    public bool IsActive { get; set; } = true; // Indicates whether the chase behavior is active

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set initial destination
        agent.destination = targets[currentTargetIndex].position;
        agent.speed = wanderSpeed; // Set initial speed
    }

    void Update()
    {
        agent.speed = wanderSpeed;

        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                MoveToRandomDestination();
            }
        }
    }

    void MoveToRandomDestination()
    {
        int newIndex = Random.Range(0, targets.Length);
        currentTargetIndex = newIndex;
        agent.destination = targets[currentTargetIndex].position; // Set destination to a random waypoint
    }
}
