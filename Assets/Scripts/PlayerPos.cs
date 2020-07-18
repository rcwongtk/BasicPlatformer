using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{

    private CheckpointController checkPointPosition;

    void Start()
    {
        checkPointPosition = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointController>();
        transform.position = checkPointPosition.lastCheckPointPos;

    }

    void Update()
    {
        // For Debugging, restart Level
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(checkPointPosition.lastCheckPointPos);
        }
    }
}
