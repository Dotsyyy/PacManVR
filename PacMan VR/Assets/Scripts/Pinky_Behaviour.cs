using UnityEngine;
using UnityEngine.AI;

public class PinkyBehavior : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float sneakRange = 4f;

    private Wander wanderScript;
    private Chase chaseScript;
    private Scared scaredScript;
    private Sneak sneakScript;

    void Start()
    {
        wanderScript = GetComponent<Wander>();
        chaseScript = GetComponent<Chase>();
        scaredScript = GetComponent<Scared>();
        sneakScript = GetComponent<Sneak>();
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
                if (distanceToPlayer <= sneakRange)
                {
                    chaseScript.enabled = false;
                    wanderScript.enabled = false;
                    scaredScript.enabled = false;
                    sneakScript.enabled = true;
                }
                else
                {
                    chaseScript.enabled = true;
                    wanderScript.enabled = false;
                    scaredScript.enabled = false;
                    sneakScript.enabled = false;
                }

            }
            else
            {
                wanderScript.enabled = true;
                chaseScript.enabled = false;
                scaredScript.enabled = false;
                sneakScript.enabled = false;
            }
           
        }
    }
}
