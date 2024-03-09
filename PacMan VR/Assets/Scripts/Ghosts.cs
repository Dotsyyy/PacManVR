using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public Vector3 startingPosition;



    public int points = 200;

    private void Start()
    {
        startingPosition = this.transform.position;
        //ResetState();
    }

    public void DeActivate()
    {
        this.gameObject.SetActive(false);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.transform.position = this.startingPosition;
        Debug.Log("Working?" + startingPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.PlayerEaten();
        }
    }

}