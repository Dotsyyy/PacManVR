using UnityEngine;
using UnityEngine.AI;


public class Blinky_Behavior : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;

    private Wander wanderScript;
    private Chase chaseScript;
    private Scared scaredScript;

    void Start()
    {
        wanderScript = GetComponent<Wander>();
        chaseScript = GetComponent<Chase>();
        scaredScript = GetComponent<Scared>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (GameManager.Instance.SwordGrabbed)
        {
            scaredScript.enabled = true;
            chaseScript.enabled = false;
            wanderScript.enabled = false;
        }
        else
        {

            if (distanceToPlayer <= chaseRange)
            {
                chaseScript.enabled = true;
                wanderScript.enabled = false;
                scaredScript.enabled = false;
            }
            else
            {
                wanderScript.enabled = true;
                chaseScript.enabled = false;
                scaredScript.enabled = false;
            }

        }
    }
}
