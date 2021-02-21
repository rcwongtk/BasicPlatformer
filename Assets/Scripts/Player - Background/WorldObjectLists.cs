using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectLists : MonoBehaviour
{

    public List<GameObject> levelGates = new List<GameObject>();
    public List<GameObject> hollowBlocks = new List<GameObject>();
    public List<GameObject> bounceBlocks = new List<GameObject>();
    public List<GameObject> tempBlocks = new List<GameObject>();
    public List<GameObject> breakBlockEnemies = new List<GameObject>();
    public List<GameObject> fallBlocks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] findGates = GameObject.FindGameObjectsWithTag("Gates");
        foreach(GameObject gate in findGates)
        {
            levelGates.Add(gate);
        }

        GameObject[] findHollowBlocks = GameObject.FindGameObjectsWithTag("HollowBlock");
        foreach (GameObject hollowBlock in findHollowBlocks)
        {
            hollowBlocks.Add(hollowBlock);
        }

        GameObject[] findBounceBlocks = GameObject.FindGameObjectsWithTag("BounceBlock");
        foreach (GameObject bounceBlock in findBounceBlocks)
        {
            bounceBlocks.Add(bounceBlock);
        }

        GameObject[] findTempBlocks = GameObject.FindGameObjectsWithTag("TempBlock");
        foreach (GameObject tempBlock in findTempBlocks)
        {
            tempBlocks.Add(tempBlock);
        }

        GameObject[] findBreakEnemies = GameObject.FindGameObjectsWithTag("BreakBlockEnemy");
        foreach (GameObject breakEnemy in findBreakEnemies)
        {
            breakBlockEnemies.Add(breakEnemy);
        }

        GameObject[] findFallingBlocks = GameObject.FindGameObjectsWithTag("FallingBlock");
        foreach (GameObject fallBlock in findFallingBlocks)
        {
            fallBlocks.Add(fallBlock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
