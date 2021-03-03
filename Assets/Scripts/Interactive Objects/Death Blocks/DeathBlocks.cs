using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathBlocks : MonoBehaviour
{

    private GameObject mainCamera;
    public bool blockBreakable = false;
    public int blockBreakValue;
    public int checkPlayerBreakValue;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if(blockBreakable == true)
        {
            gameObject.GetComponentInChildren<TextMeshPro>().text = blockBreakValue.ToString("#");
        }
    }

    private void Update()
    {
        checkPlayerBreakValue = BreakPowerBlock.breakBlockCounter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (blockBreakable == false || blockBreakValue != checkPlayerBreakValue)
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

                foreach (GameObject breakEnemy in mainCamera.GetComponent<WorldObjectLists>().breakBlockEnemies)
                {
                    breakEnemy.GetComponent<MeshRenderer>().enabled = true;
                    breakEnemy.GetComponent<BoxCollider2D>().enabled = true;
                    if(breakEnemy.transform.childCount > 0)
                    {
                        breakEnemy.GetComponentInChildren<TextMeshPro>().enabled = true;
                    }
                    BreakPowerBlock.breakBlockCounter = 0;
                }

                List<GameObject> fallBlockList = mainCamera.GetComponent<WorldObjectLists>().fallBlocks;

                for (int i = 0; i < fallBlockList.Count; )
                {
                    if(fallBlockList[i].activeSelf == true)
                    {
                        GameObject toDestroy = fallBlockList[i];
                        mainCamera.GetComponent<WorldObjectLists>().fallBlocks.Remove(fallBlockList[i]);
                        Destroy(toDestroy);
                    }
                    else
                    {
                        fallBlockList[i].SetActive(true);
                        i++;
                    }
                }

            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponentInChildren<TextMeshPro>().enabled = false;
                BreakPowerBlock.breakBlockCounter = 0;
            }

        }
        
    }
}
