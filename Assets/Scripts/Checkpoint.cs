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
    }


}
