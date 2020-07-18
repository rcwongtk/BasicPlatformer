using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private static CheckpointController instance;
    public Vector2 lastCheckPointPos;
    private GameObject playerObject;
    public GameObject playerPrefab;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerObject == null)
        {
            playerObject = Instantiate(playerPrefab);
        }
    }
}
