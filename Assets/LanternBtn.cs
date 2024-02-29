using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternBtn : MonoBehaviour
{
    public Manager manager;
    public int num;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            replayBtn();
        });
    }
    public void replayBtn()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        manager.ReplayBtn(num);
    }
}
