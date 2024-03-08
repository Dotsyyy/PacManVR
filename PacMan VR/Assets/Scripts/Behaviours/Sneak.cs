using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sneak : MonoBehaviour
{
    public float sneakSpeed = 1f; // Normal speed when not sneaking
    private NavMeshAgent agent;
    public Transform player; // Reference to the Player transform

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
    }

    private void Update()
    {
        agent.destination = player.position;
        agent.speed = sneakSpeed;
    }
}
