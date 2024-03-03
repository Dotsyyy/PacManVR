using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 startingPosition { get; private set; }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.transform.position = this.startingPosition;
    }
}
