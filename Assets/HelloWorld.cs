using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelloWorld : MonoBehaviour
{
    TMP_Text TMPText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the person presses "W"
        if (Input.GetKeyDown(KeyCode.W))
        {
            TMPText = gameObject.GetComponent<TMP_Text>();
            TMPText.text = "Hello World";
        }
    }
}
