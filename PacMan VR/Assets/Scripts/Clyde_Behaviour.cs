using UnityEngine;
using UnityEngine.AI;

public class ClydeBehavior : MonoBehaviour
{
    public Transform[] targets; // Waypoints for Clyde to navigate
    private NavMeshAgent agent;
    private int currentTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set initial destination
        agent.destination = targets[currentTargetIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            MoveToRandomDestination(); // Move to a random waypoint if reached the current destination
        }
    }

    void MoveToRandomDestination()
    {
        int newIndex = Random.Range(0, targets.Length);
        // Ensure that the new target index is not the same as the current one
        while (newIndex == currentTargetIndex)
        {
            newIndex = Random.Range(0, targets.Length);
        }
        currentTargetIndex = newIndex;
        agent.destination = targets[currentTargetIndex].position; // Set destination to a random waypoint
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Implement behavior when Clyde collides with the player, if needed
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Implement behavior when Clyde exits the collision with the player, if needed
        }
    }
}
