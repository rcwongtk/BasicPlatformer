using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBlock : MonoBehaviour
{
    public int keyCounter;

    // Start is called before the first frame update
    void Start()
    {
        keyCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCounter == 3)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            keyCounter = 0;
        }
    }

}
