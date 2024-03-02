using UnityEngine;
using UnityEngine.AI;

public class Navigation_Ghosts : MonoBehaviour
{
    public Transform[] targets;
    public Transform player; // Reference to the Player transform
    public float chaseDistance = 5f; // Distance threshold to start chasing Player
    public float chaseDuration = 7f; // Duration of chasing in seconds
    private NavMeshAgent agent;
    private int currentTargetIndex;
    private bool isChasing = false;
    private float chaseTimer = 0f;

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
        // Check if the agent has reached its current destination
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // Move to a new destination if not chasing Player
            if (!isChasing)
            {
                MoveToRandomDestination();
            }
        }

        // Check if Player is close enough to start chasing
        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
            if (!isChasing)
            {
                StartChasing();
            }
            else
            {
                UpdateChase();
            }
        }
        else
        {
            if (isChasing)
            {
                StopChasing();
            }
        }
    }

    // Start chasing Player
    void StartChasing()
    {
        isChasing = true;
        agent.destination = player.position;
        chaseTimer = 0f;
    }

    // Update chasing behavior
    void UpdateChase()
    {
        chaseTimer += Time.deltaTime;

        // Check if chase duration is reached
        if (chaseTimer >= chaseDuration)
        {
            isChasing = false;
            MoveToRandomDestination(); // Go back to random destination
        }
    }

    // Stop chasing Player
    void StopChasing()
    {
        isChasing = false;
    }

    // Move to a new random destination
    void MoveToRandomDestination()
    {
        // Choose a new random destination index
        int newIndex = Random.Range(0, targets.Length);

        // Update the current target index
        currentTargetIndex = newIndex;

        // Set the agent's destination to the new target position
        agent.destination = targets[currentTargetIndex].position;
    }
}
