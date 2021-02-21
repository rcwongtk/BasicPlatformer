using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncePlatform : MonoBehaviour
{
    int layerMask = 1 << 8; //Layer 8
    GameObject playerBlock;
    public bool bounceQuantityEnabled = false;
    public int bouncesLeft;
    public int bounceCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerBlock = GameObject.FindGameObjectWithTag("Player");
        bounceCounter = bouncesLeft;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bounceQuantityEnabled)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = bounceCounter.ToString("#");
        }

        if (bounceCounter == 0 && bounceQuantityEnabled)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BouncePlatform>().enabled = false;
            gameObject.GetComponentInChildren<TMP_Text>().enabled = false;
        }

        if (playerBlock == null)
        {
            playerBlock = GameObject.FindGameObjectWithTag("Player");
        }

        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(gameObject.transform.position, gameObject.transform.localScale - new Vector3(0.1f,0,0), 0, layerMask);
        if (detectedObjects.Length > 0)
        {
            playerBlock.GetComponent<Player>().velocity.y = playerBlock.GetComponent<Player>().maxJumpVelocity;

            if (bounceQuantityEnabled)
            {
                bounceCounter--;

            }
        }
    }
}
