using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContactController : MonoBehaviour
{
    public Text text;
    public float countdown;
    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        countdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;
        if (countdown < 5 )
        {
            gameObject.GetComponent<Text>().text = "";

        }
        else if(countdown < 10) 
        {
            gameObject.GetComponent<Text>().text = "Animated by Scythker. \r\nscythker@gmail.com";
        }
        else if(countdown >= 10)
        {
            countdown = 0;
        }

    }
}
