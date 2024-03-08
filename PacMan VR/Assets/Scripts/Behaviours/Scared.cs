using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scared : MonoBehaviour
{
    public Transform[] targets;
    public float scaredSpeed = 0.5f; // Normal speed when not sneaking
    private NavMeshAgent agent;
    private int currentTargetIndex;
    public Material scaredMaterial;
    private Material priviousMaterial;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set initial destination
        agent.destination = targets[currentTargetIndex].position;
        agent.speed = scaredSpeed; // Set initial speed
    }

    public void OnEnable()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        priviousMaterial = rend.material;
        rend.material = scaredMaterial;
    }

    public void OnDisable()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material = priviousMaterial;
    }

    void Update()
    {
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
