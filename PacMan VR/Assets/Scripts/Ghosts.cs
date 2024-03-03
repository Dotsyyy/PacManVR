using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public Vector3 startingPosition { get; private set; }

    public int points = 200;

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.transform.position = this.startingPosition;
    }
}
