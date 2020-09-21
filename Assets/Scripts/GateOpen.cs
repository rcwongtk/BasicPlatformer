using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    bool gateDoorPassed = false;
    int layerMask = 1 << 8; //Layer 8
    Vector3 gateSize;


    void Start()
    {
        // Determine what side of the gate the player will pass through (0.5 thickness) and expand that.
        gateSize = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        if(gateSize.x == 0.5)
        {
            gateSize.x = 2;
        }
        else
        {
            gateSize.y = 2;
        }

    }

    void Update()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(gameObject.transform.position, gateSize, 0, layerMask);
        if(detectedObjects.Length > 0)
        {
            if (KeyCollectable.keysCollected == 3 && gateDoorPassed == false)
            {
                KeyCollectable.keysCollected = 0;
                foreach (GameObject key in KeyCollectable.storeForSpawn)
                {
                    Destroy(key);
                }
                KeyCollectable.storeForSpawn.Clear();

                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                gateDoorPassed = true;
            }
        }
    }

}
