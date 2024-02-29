using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.Video;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.Rendering;
using System.Xml.Schema;
public class Manager : MonoBehaviour
{
    [Header("file uploader")]
    [SerializeField]private string fileComments = "Comments.csv";
    [Header("Input Field asset")]
    public InputField inputFieldScrollH;
    public InputField inputFieldScrollV;
    public List<string> comments;
    [HideInInspector] public GameObject inpfieldScrollH;
    [HideInInspector] public GameObject inpfieldScrollV;
    public GameObject panel;
    public UnityEngine.UI.Text outputText;

    [Header("Prefab asset")]
    public GameObject scrollPrefabH;
    public GameObject scrollPrefabV;
    public GameObject[] logoPrefab;

    [Header("setting")]
    public Vector3 scrPrefHVector;
    public Vector3 scrPrefVVector;
    public Vector3[] logoPrefVector;
    public float countdown;

    [Header("Button update asset")]
    public Transform scrollViewContent;
    public GameObject prefabBtn;
    //[Header("Video Player asset")]
    //public RawImage boardPanel;
    //public VideoPlayer videoPlayer;
    //public AudioSource audioSource;

    

    public int isPlay;
    // Start is called before the first frame update
    void Start()
    {
        comments = new List<string>();
        string[] words = File.ReadAllLines("Assets/Resources/"+ fileComments);
        for (int i = 1; i < words.Length; i++)
        {
            string[] dataTemp = words[i].Split(new string[] { ",", "\n" }, StringSplitOptions.None);
            for (int j = 0; j < dataTemp.Length; j++)
            {
                string json = dataTemp[j];
                if (json == "")
                {
                }
                else
                {
                    comments.Add(json);
                }
            }
        }
        for (int i = 0; i < comments.Count; i++)
        {
            GameObject lanternBtn = Instantiate(prefabBtn, Vector3.zero, Quaternion.identity, scrollViewContent.transform);
            lanternBtn.GetComponentInChildren<UnityEngine.UI.Text>().text = comments[i];
            lanternBtn.GetComponent<LanternBtn>().num = i;
        }
        isPlay = 0;
        //StartCoroutine(PlayVideo());
        inpfieldScrollH = inputFieldScrollH.GetComponent<Transform>().gameObject;
        inpfieldScrollV = inputFieldScrollV.GetComponent<Transform>().gameObject;
        inpfieldScrollH.SetActive(true);
        inpfieldScrollV.SetActive(false);

        scrPrefHVector = new Vector3(4.07f, -1f, -1f);
        scrPrefVVector = new Vector3(5.02f, -1f, -1f);
        //logoPrefVector[0] = new Vector3(5.12f, -1f, -0.1f);
        countdown = 0;
    }
    //IEnumerator PlayVideo()
    //{
    //    videoPlayer.Prepare();
    //    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    //    while(!videoPlayer.isPrepared)
    //    {
    //        yield return waitForSeconds;
    //        break;
    //    }
    //    boardPanel.texture = videoPlayer.texture;
    //    videoPlayer.Play();
    //    audioSource.Play();
    //}

    private void Update()
    {
        countdown += (Time.deltaTime);
        switch (isPlay)
        {
            case 0:
                {
                    panel.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    break;
                }
            case 1:
                {
                    panel.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;

                    if(countdown >= 5)
                    {
                        if (inpfieldScrollH.activeInHierarchy)
                        {
                            PrintObject(scrollPrefabH, comments[UnityEngine.Random.Range(0, comments.Count)], scrPrefHVector);
                        }
                        else if (inpfieldScrollV.activeInHierarchy)
                        {
                            PrintObject(scrollPrefabV, comments[UnityEngine.Random.Range(0, comments.Count)], scrPrefVVector);
                        }
                        countdown = 0;
                    }
                    break;
                }
            case 2:
                {
                    panel.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                    if (countdown >= 5)
                    {
                        float randomNo = UnityEngine.Random.Range(0f, 1.0f);
                        //Debug.Log(randomNo);
                        if (randomNo > 0.5f)
                        {
                            PrintObject(scrollPrefabH, comments[UnityEngine.Random.Range(0, comments.Count)], scrPrefHVector);
                        }
                        else if (randomNo <= 0.5f)
                        {
                            PrintObject(scrollPrefabV, comments[UnityEngine.Random.Range(0, comments.Count)], scrPrefVVector);
                        }
                        countdown = 0;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

    }

    public void Post()
    {
        if (inpfieldScrollH.activeInHierarchy)
        {
            PrintObject(scrollPrefabH,inputFieldScrollH.text, scrPrefHVector);
            inputFieldScrollH.text = "";
        }
        else if (inpfieldScrollV.activeInHierarchy)
        {
            PrintObject(scrollPrefabV, inputFieldScrollV.text, scrPrefVVector);
            inputFieldScrollV.text = "";
            outputText.text = "";
        }
        
    }
    public void SaveAndPost()
    {
        string text = "";
        if (inpfieldScrollH.activeInHierarchy)
        {
            text = inputFieldScrollH.text;
            if (text != "")
            {
                comments.Add(text);
                PrintObject(scrollPrefabH, text, scrPrefHVector);

                GameObject lanternBtn = Instantiate(prefabBtn, Vector3.zero, Quaternion.identity, scrollViewContent.transform);
                lanternBtn.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
                lanternBtn.GetComponent<LanternBtn>().num = (comments.Count - 1);
                inputFieldScrollH.text = "";
            }
        }
        else if (inpfieldScrollV.activeInHierarchy)
        {
            text = inputFieldScrollV.text;
            if (text != "")
            {
                comments.Add(text);
                PrintObject(scrollPrefabV, text, scrPrefVVector);

                GameObject lanternBtn = Instantiate(prefabBtn, Vector3.zero, Quaternion.identity, scrollViewContent.transform);
                lanternBtn.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
                lanternBtn.GetComponent<LanternBtn>().num = (comments.Count - 1);
                inputFieldScrollV.text = "";
                outputText.text = "";
            }
        }

    }

    public void Replay(int num)
    {
        isPlay = num;
    }

    public void ReplayBtn(int num)
    {
        if (inpfieldScrollH.activeInHierarchy)
        {
            PrintObject(scrollPrefabH, comments[num],scrPrefHVector);
        }
        else if (inpfieldScrollV.activeInHierarchy)
        {
            PrintObject(scrollPrefabV, comments[num], scrPrefVVector);
        }
    }

    public void PrintObject(GameObject scrollPrefab, string text, Vector3 posVector)
    {
        GameObject fire = Instantiate(scrollPrefab);
        fire.GetComponent<ScrollController>().text = text;
        fire.transform.position = new Vector3(UnityEngine.Random.Range(-posVector.x, posVector.x), posVector.y, posVector.z);
    }
    public void PrintLogoObject(GameObject logoPrefab)
    {
        GameObject fire = Instantiate(logoPrefab);
        fire.transform.position = new Vector3(UnityEngine.Random.Range(-5.12f, 5.12f), -1f, -0.1f);
    }

}
