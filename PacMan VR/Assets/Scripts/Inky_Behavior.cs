using UnityEngine;
using UnityEngine.AI;

public class InkyBehavior : MonoBehaviour
{
    public Transform[] targets; // Waypoints for Inky to navigate
    public Transform player; // Reference to the Player transform
    public float chaseDistance = 5f; // Distance threshold to start chasing Player
    public float chaseDuration = 7f; // Duration of chasing in seconds
    public float sneakDistance = 3f; // Distance threshold for sneaking
    public float sneakSpeed = 1.5f; // Speed while sneaking
    public float normalSpeed = 3.5f; // Normal speed when not sneaking
    private NavMeshAgent agent;
    private int currentTargetIndex;
    private bool isChasing = false;
    private bool isSneaking = false;
    private float chaseTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set initial destination
        agent.destination = targets[currentTargetIndex].position;
        agent.speed = normalSpeed; // Set initial speed
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (!isChasing && !isSneaking)
                {
                    MoveToRandomDestination(); // Move to a random waypoint if not chasing or sneaking
                }
            }

            RaycastHit hit;
            Vector3 directionToPlayer = player.position - transform.position;

            // Perform raycast to detect obstacles between Inky and the player
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (!isChasing)
                    {
                        if (isSneaking)
                        {
                            StopSneaking(); // Stop sneaking if player is within chase distance
                        }
                        else
                        {
                            StartChasing(); // Start chasing the player if within chase distance
                        }
                    }
                }
                else
                {
                    if (isChasing)
                    {
                        StopChasing(); // Stop chasing if player moves out of chase distance
                    }
                    if (isSneaking)
                    {
                        StopSneaking(); // Stop sneaking if player moves out of sneak distance
                    }
                }
            }

            // Check for sneaking conditions
            if (!isChasing && Vector3.Distance(transform.position, player.position) < sneakDistance)
            {
                StartSneaking(); // Start sneaking towards the player if within sneak distance
            }
        }
    }

    void StartSneaking()
    {
        isSneaking = true;
        agent.speed = sneakSpeed; // Set speed to sneak speed
        agent.destination = player.position; // Set destination to player position
    }

    void StopSneaking()
    {
        isSneaking = false;
        agent.speed = normalSpeed; // Reset speed to normal speed
        MoveToRandomDestination(); // Move to a random destination after stopping sneaking
    }

    void StartChasing()
    {
        isChasing = true;
        agent.destination = player.position; // Set destination to player position
        chaseTimer = 0f;
    }

    void UpdateChase()
    {
        chaseTimer += Time.deltaTime;

        if (chaseTimer >= chaseDuration)
        {
            isChasing = false;
            MoveToRandomDestination(); // Stop chasing and move to a random destination
        }
    }

    void StopChasing()
    {
        isChasing = false;
        agent.ResetPath(); // Stop chasing and reset the NavMesh path
    }

    void MoveToRandomDestination()
    {
        int newIndex = Random.Range(0, targets.Length);
        currentTargetIndex = newIndex;
        agent.destination = targets[currentTargetIndex].position; // Set destination to a random waypoint
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopChasing(); // Stop chasing if collided with the player
            agent.isStopped = true; // Stop the NavMeshAgent
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            agent.isStopped = false; // Resume the NavMeshAgent if player moves out of range
        }
    }
}
