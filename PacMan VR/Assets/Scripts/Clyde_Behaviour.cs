using UnityEngine;
using UnityEngine.AI;

public class ClydeBehavior : MonoBehaviour
{

    private Wander wanderScript;
    private Scared scaredScript;
    [SerializeField] protected Sword sword;

    void Start()
    {
        wanderScript = GetComponent<Wander>();
        scaredScript = GetComponent<Scared>();
    }

    private void Update()
    {

        if (!sword.grabbed)
        {
            scaredScript.enabled = false;
            wanderScript.enabled = true;

        }
        else
        {
            scaredScript.enabled = true;
            wanderScript.enabled = false;
        }
    }
}
