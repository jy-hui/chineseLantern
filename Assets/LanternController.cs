using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LanternController : MonoBehaviour
{
    public UnityEngine.Vector3 position;
    public int speed;
    public UnityEngine.Vector3 rotation;
    public float countdown;

    private void Start()
    {
        rotation = new UnityEngine.Vector3(0, 0, 0);
        speed = 3;
        countdown = 0;
    }
    // Update is called once per frame
    //void Update()
    //{
        //countdown += (Time.deltaTime);
        //position = this.gameObject.GetComponent<Transform>().position;
        //position.y = position.y + (0.1f * speed * Time.deltaTime);
        //this.gameObject.GetComponent<Transform>().position = position;

        //if (this.gameObject.GetComponent<Transform>().position.y >= 6.0f)
        //{
        //    Destroy(this.gameObject);
        //}

        //if (countdown >= 5)
        //{
        //    playAnim();
                
        //    countdown = 0;
        //}

    //}

    //public void playAnim()
    //{
    //    rotation.z += 1;
    //    position.x -= 1;
    //    position.y += 1;
    //    this.gameObject.transform.rotation = UnityEngine.Quaternion.Euler(rotation);
    //    this.gameObject.transform.position = position;
    //}

    
}
