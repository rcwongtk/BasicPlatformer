using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    int layerMask = 1 << 8; //Layer 8
    GameObject playerBlock;
    private GameObject mainCamera;

    public GameObject parentObject;
    GameObject backUpObject;

    // Start is called before the first frame update
    void Start()
    {
        playerBlock = GameObject.FindGameObjectWithTag("Player");

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Create a moving platform duplicate and disable it
        backUpObject = Instantiate(gameObject, parentObject.transform);
        mainCamera.GetComponent<WorldObjectLists>().fallBlocks.Add(backUpObject);
        backUpObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBlock == null)
        {
            playerBlock = GameObject.FindGameObjectWithTag("Player");
        }

        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(gameObject.transform.position, gameObject.transform.parent.localScale + new Vector3(0, 0.1f, 0), 0, layerMask);

        if (detectedObjects.Length > 0)
        {
            gameObject.GetComponent<PlatformController>().enabled = true;
        }

    }
}
