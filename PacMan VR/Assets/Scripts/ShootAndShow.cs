using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndShow : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cameraTransform;
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, 100))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.transform.name);

            //if you want to destroy an enemy, using tags
            if (hit.transform.tag == "Enemy")
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //  Debug.Log("Did not Hit");
        }
    }


}