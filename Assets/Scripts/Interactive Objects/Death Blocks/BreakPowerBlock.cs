using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakPowerBlock : MonoBehaviour
{
    public static int breakBlockCounter;
    public int breakBlockValue;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "BreakBlock")
        {
            gameObject.GetComponentInChildren<TextMeshPro>().text = breakBlockValue.ToString("#");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Player")
        {
            if (breakBlockCounter == 0)
            {
                gameObject.GetComponentInChildren<TextMeshPro>().text = "";
            }
            else
            {
                gameObject.GetComponentInChildren<TextMeshPro>().text = breakBlockCounter.ToString("#");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == "BreakBlock")
        {
            breakBlockCounter = breakBlockCounter + breakBlockValue;
        }

    }
}
