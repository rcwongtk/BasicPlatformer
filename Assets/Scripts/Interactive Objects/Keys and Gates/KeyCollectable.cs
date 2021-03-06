﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectable : MonoBehaviour
{
    public static int keysCollected;
    public static List<GameObject> storeForSpawn = new List<GameObject>();
    public GameObject tempBlock;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(keysCollected < 3)
            {
                keysCollected++;
                Debug.Log(keysCollected);

                if (tempBlock != null)
                {
                    tempBlock.GetComponent<TempBlock>().keyCounter++;
                }
            }

            gameObject.SetActive(false);
            storeForSpawn.Add(gameObject);



        }


    }
}
