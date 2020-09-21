using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : MonoBehaviour
{

    int layerMask = 1 << 8; //Layer 8
    GameObject playerBlock;

    // Start is called before the first frame update
    void Start()
    {
        playerBlock = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerBlock == null)
        {
            playerBlock = GameObject.FindGameObjectWithTag("Player");
        }

        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(gameObject.transform.parent.position - new Vector3(0, gameObject.transform.parent.localScale.y / 2, 0),
            gameObject.transform.parent.localScale - new Vector3(0, gameObject.transform.parent.localScale.y * 0.9f, 0) + new Vector3(0,0.1f,0), 0, layerMask);

        if (detectedObjects.Length > 0 && playerBlock.GetComponent<Player>().velocity.y > 0)
        {
            Debug.Log("Entered Area");
            gameObject.transform.parent.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
