using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    protected virtual void Eat()
    {
        FindAnyObjectByType<GameManager>().PelletEaten(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        Eat();
    }
}

