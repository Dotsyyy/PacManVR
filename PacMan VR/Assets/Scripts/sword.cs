using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool Grabbed { get; private set; }

    private void Start()
    {
        // Ensure the sword is inactive at the start
        gameObject.SetActive(true);
    }

    public void IsGrabbed()
    {
        // Activate the sword when grabbed
        Grabbed = true;
        // Start the coroutine to deactivate the sword after 7 seconds
        StartCoroutine(DeactivateAfterDelay());
    }

    public void ExitGrabbed()
    {
        Grabbed = false;
    }

    private IEnumerator DeactivateAfterDelay()
    {
        // Wait for 7 seconds
        yield return new WaitForSeconds(7.0f);
        // Deactivate the sword after the delay
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a ghost
        Ghosts ghost = other.GetComponent<Ghosts>();

        // If a ghost is hit and the sword is grabbed, handle the interaction here
        if (ghost != null && Grabbed)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.SwordHit(ghost);
            }
        }
    }
}
