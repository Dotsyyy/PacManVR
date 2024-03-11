using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        other.transform.position = connection.position;
    }
}
