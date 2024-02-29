using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public bool isHorizontal;
    public Vector3 position;
    public int speed;
    public string text;
    public TextMesh textMesh;
    public Animator animator;
    [HideInInspector] public string word;
    public int ChinNum;
    public int EngNum;
    void Start()
    {
        textMesh.text = "";
        word = convertText(text);
        speed = 3;
    }
    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).length <= animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            textMesh.text = word;
        }
        position = this.gameObject.GetComponent<Transform>().position;
        position.y = position.y + (0.1f * speed * Time.deltaTime);
        this.gameObject.GetComponent<Transform>().position = position;

        if (this.gameObject.GetComponent<Transform>().position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public string convertText(string text)
    {
        word = "";
        if (isHorizontal)
        {
            ChinNum = 14;
            EngNum = 25;
        }
        else
        {
            ChinNum = 16;
            EngNum = 8;
        }
        //---check is english or chinese
        if (isEnglish(text))
        {
            string[] sentences = text.Split("\n");
            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i].Length <= EngNum)
                {
                    word = word + sentences[i];
                }
                else
                {
                    string[] words = sentences[i].Split(" ");
                    int num = EngNum - word.Length;
                    for (int j = 0; j < words.Length; j++)
                    {
                        int no = num - words[j].Length - 1;
                        if (no >= 0)
                        {
                            word = word + " " + words[j];
                            num = num - words[j].Length - 1;
                        }
                        else
                        {
                            word = word + "\n" + words[j];
                            num = EngNum - words[j].Length;
                        }

                    }
                }
            }
        }
        else if (isSymbol(text))
        {

        }
        else//chinese
        {
            word = "";
            string[] sentences = text.Split("\n");
            List<string> words = new List<string>();
            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i].Length <= ChinNum)
                {
                    char[] chars = sentences[i].ToCharArray();
                    for (int j = 0; j < chars.Length; j++)
                    {
                        words.Add(chars[j].ToString());
                    }
                }
                else
                {
                    string[] stringWords = sentences[i].Split(" ");
                    for (int k = 0; k < stringWords.Length; k++)
                    {
                        if (isEnglish(stringWords[k]))
                        {
                            words.Add(stringWords[k]);
                        }
                        else
                        {
                            char[] chars = stringWords[k].ToCharArray();
                            for (int l = 0; l < chars.Length; l++)
                            {
                                words.Add(chars[l].ToString());
                            }
                        }
                    }
                }
            }
            if (isHorizontal)
            {
                int num = ChinNum;
                for (int i = 0; i < words.Count; i++)
                {
                    int no = num - words[i].Length;
                    if (no >= 0)
                    {
                        if (isEnglish(words[i]))
                        {
                            word = word + " " + words[i];
                            num = num - words[i].Length - 1;
                        }
                        else
                        {
                            word = word + words[i];
                            num = num - words[i].Length;
                        }
                    }
                    else
                    {
                        word = word + "\n" + words[i];
                        num = ChinNum - words[i].Length - 1;
                    }
                }
            }
            else
            {
                int num = ChinNum;
                int no = 3;
                if (words.Count > num * 2)
                {
                    no = 3;
                }
                else if (words.Count > num)
                {
                    no = 2;
                }
                else
                {
                    no = 1;
                }
                int noA = words.Count / no;
                int noB = words.Count % no;
                int noC = noA;
                int noD = words.Count;
                //Debug.Log(noA + ": " + noB + ": " + noC + " : " + noD);
                if (noB > 0)
                {
                    noC++;
                    for (int i = 0; i <= noB; i++)
                    {
                        words.Add("  ");
                    }
                    noD = words.Count;
                }
                //Debug.Log(noC + ":" + noD);
                //Debug.Log(words[words.Count - 2] + words[words.Count - 1]);
                for (int i = 0; i < noC; i++)
                {
                    if (no == 2)
                    {
                        word = word + words[i + (noC * 1)] + " " + words[i] + "\n";

                    }
                    else if (no == 3)
                    {
                        word = word + words[i + (noC * 2)] + " " + words[i + (noC * 1)] + " " + words[i] + "\n";

                    }
                    else
                    {
                        word = word + words[i] + "\n";
                    }
                }
            }
        }
        return word;
    }

    public bool isEnglish(string text)
    {
        char[] chars = text.ToCharArray();
        int total = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            if (Regex.IsMatch(chars[i].ToString(), "[a-zA-Z0-9]", RegexOptions.IgnoreCase))
            {
                total++;
            }
        }
        if (total > (chars.Length - total))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isSymbol(string text)
    {
        char[] chars = text.ToCharArray();
        int total = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsLetter(chars[i]))
            {
                total++;
            }
        }
        if (total > (chars.Length - total))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
