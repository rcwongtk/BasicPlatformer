using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyIndicators : MonoBehaviour
{
    public int keyNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(keyNumber)
        {
            case 1:
                if (KeyCollectable.keysCollected >= 1)
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                }
                break;
            case 2:
                if (KeyCollectable.keysCollected >= 2)
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                }
                break;
            case 3:
                if (KeyCollectable.keysCollected == 3)
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                }
                break;
            default:
                Debug.Log("Error - Key Indicator picking up odd key quantity");
                break;
        }
    }
}
