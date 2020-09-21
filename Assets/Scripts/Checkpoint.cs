using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private CheckpointController checkPointPosition;
    private GameObject mainCamera;


    void Start()
    {
        checkPointPosition = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Player"))
        {
            checkPointPosition.lastCheckPointPos = transform.position;

            Debug.Log("Checkpoint Entered");
        }

        foreach(GameObject gate in mainCamera.GetComponent<WorldObjectLists>().levelGates)
        {
            gate.GetComponent<MeshRenderer>().enabled = true;
            gate.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


}
