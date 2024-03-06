using UnityEngine;
using UnityEngine.AI;

public class PinkyBehavior : MonoBehaviour
{
    public Transform[] targets; // Waypoints for Pinky to patrol
    public Transform player; // Reference to the Player transform
    public float scanInterval = 3f; // Interval for scanning for the player in seconds
    public float scanDistance = 5f; // Distance to scan for the player
    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private float scanTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;

        // Set initial destination to the first waypoint
        agent.destination = targets[currentWaypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // Move to the next waypoint if reached the current destination
            MoveToNextWaypoint();
        }

        // Update the scan timer
        scanTimer += Time.deltaTime;

        // Check if it's time to scan for the player
        if (scanTimer >= scanInterval)
        {
            ScanForPlayer();
            scanTimer = 0f;
        }
    }

    void MoveToNextWaypoint()
    {
        // Move to the next waypoint in the array
        currentWaypointIndex = (currentWaypointIndex + 1) % targets.Length;
        agent.destination = targets[currentWaypointIndex].position; // Set destination to the next waypoint
    }

    void ScanForPlayer()
    {
        // Check if the player is within the scan distance
        if (Vector3.Distance(transform.position, player.position) <= scanDistance)
        {
            // If the player is within the scan distance, chase the player
            agent.destination = player.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Implement behavior when Pinky collides with the player, if needed
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Implement behavior when Pinky exits the collision with the player, if needed
        }
    }
}
