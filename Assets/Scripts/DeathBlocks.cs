using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathBlocks : MonoBehaviour
{

    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            foreach (GameObject key in KeyCollectable.storeForSpawn)
            {
                key.SetActive(true);
            }
            KeyCollectable.keysCollected = 0;

            foreach (GameObject hollowBlock in mainCamera.GetComponent<WorldObjectLists>().hollowBlocks)
            {
                hollowBlock.transform.parent.GetComponent<MeshRenderer>().enabled = false;
                hollowBlock.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            }

            foreach (GameObject bounceBlock in mainCamera.GetComponent<WorldObjectLists>().bounceBlocks)
            {
                bounceBlock.GetComponent<MeshRenderer>().enabled = true;
                bounceBlock.GetComponent<BoxCollider2D>().enabled = true;
                bounceBlock.GetComponent<BouncePlatform>().enabled = true;
                bounceBlock.GetComponentInChildren<TMP_Text>().enabled = true;
                bounceBlock.GetComponent<BouncePlatform>().bounceCounter = bounceBlock.GetComponent<BouncePlatform>().bouncesLeft;
            }

            foreach (GameObject tempBlock in mainCamera.GetComponent<WorldObjectLists>().tempBlocks)
            {
                tempBlock.GetComponent<MeshRenderer>().enabled = true;
                tempBlock.GetComponent<BoxCollider2D>().enabled = true;
                tempBlock.GetComponent<TempBlock>().keyCounter = 0;
            }

        }
    }
}
