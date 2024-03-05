using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected Vector3 startingPosition;

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.transform.position = this.startingPosition;
    }
}
