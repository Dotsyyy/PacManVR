using UnityEngine;
using UnityEngine.AI;

public class ClydeBehavior : MonoBehaviour
{

    private Wander wanderScript;
    private Scared scaredScript;

    void Start()
    {
        wanderScript = GetComponent<Wander>();
        scaredScript = GetComponent<Scared>();
    }

    private void Update()
    {

        if (GameManager.Instance.SwordGrabbed)
        {
            
            scaredScript.enabled = true;
            wanderScript.enabled = false;

        }
        else
        {
            scaredScript.enabled = false;
            wanderScript.enabled = true;
        }
    }
}
