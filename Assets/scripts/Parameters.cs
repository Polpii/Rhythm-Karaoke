using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public bool Speech_Mode = true;
    public bool Beat_Mode = false;
    public bool Test_Mode = false;
    public string FileName;
    public int circle_Number;
    public float braker;
    public float ManipulandumSensi;
    public float ManipulandumSensiRelease;

    string[] text;
    int lignCounter;
    bool nextLine;
    bool sameLine;
    string[] lign;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("endGame").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("endText").GetComponent<MeshRenderer>().enabled = false;
        if (!Test_Mode)
        {
            GameObject.Find("GameManager").GetComponent<SaveData>().enabled = false;
        }
        if (Test_Mode)
        {
            GameObject.Find("GameManager").GetComponent<SaveData>().enabled = true;
            nextLine = false;
            lignCounter = 0;
            path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\processFile.txt";
            text = File.ReadAllLines(@path);
            lign = text[lignCounter].Split(';'); 
            
            if(lign[0] == "b")
            {
                SetBeatMode(lign);
            }
            else if(lign[0] == "s")
            {
                SetSpeechMode(lign);
            }
        }
        else if (Speech_Mode)
        {
            ActiveLyrics(true);
            ActivePoints(false);
        }
        else if(Beat_Mode)
        {
            ActiveLyrics(false);
            ActivePoints(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Test_Mode && Input.GetKeyDown("n"))
        {
            //NextLine();
            StartCoroutine(NextLineDelayed(1));
        }
        if (Test_Mode && Input.GetKeyDown("s"))
        {
            SameLine();
        }
        if (Test_Mode && Input.GetKeyDown("g"))
        {
            if(GameObject.Find("Canvas").GetComponent<Canvas>().enabled == true)
            {
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
            }
            else
            {
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            }
        }
        if (nextLine)
        {
            GameObject.Find("GameManager").GetComponent<MaxMessageSender>().restart = true;
            lignCounter++;
            if (lignCounter < text.Length)
            {
                lign = text[lignCounter].Split(';');
                if (lign[0] == "b")
                {
                    SetBeatMode(lign);
                }
                else if (lign[0] == "s")
                {
                    SetSpeechMode(lign);
                }
            }
            else
            {
                GameObject.Find("endGame").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("endText").GetComponent<MeshRenderer>().enabled = true;
            }
            nextLine = false;
        }
        if (sameLine)
        {
            GameObject.Find("GameManager").GetComponent<MaxMessageSender>().restart = true;
            if (lignCounter < text.Length)
            {
                if (lign[0] == "b")
                {
                    SetBeatMode(lign);
                }
                else if (lign[0] == "s")
                {
                    SetSpeechMode(lign);
                }
            }
            sameLine = false;
        }
    }
    void SetBeatMode(string[] lign)
    {
        ActiveLyrics(false);
        ActivePoints(true);
        FileName = lign[1];
        circle_Number = Convert.ToInt32(lign[3]);
        braker = Single.Parse(lign[2]);
    }
    void SetSpeechMode(string[] lign)
    {
        ActiveLyrics(true);
        ActivePoints(false);
        FileName = lign[1];
        braker = Single.Parse(lign[2]);
    }
    void ActiveLyrics(bool mode)
    {
        GameObject.Find("Lyrics").GetComponent<SyllabesColoration>().restart = mode;
        GameObject.Find("Lyrics").GetComponent<Song>().restart = mode;
        GameObject.Find("GameManager").GetComponent<MaxMessageSender>().restart = mode;
        GameObject.Find("GameManager").GetComponent<MaxMessageSender>().playing = mode;
        //GameObject.Find("GameManager").GetComponent<MaxMessageSender>().enabled = mode;
        GameObject.Find("Lyrics").GetComponent<MeshRenderer>().enabled = mode;
        GameObject.Find("Lyrics").GetComponent<SyllabesColoration>().enabled = mode;
        GameObject.Find("Lyrics").GetComponent<Song>().enabled = mode;
    }
    void ActivePoints(bool mode)
    {
        GameObject.Find("Points").GetComponent<Counter>().restart = mode;
        GameObject.Find("Points").GetComponent<Beat>().restart = mode;
        GameObject.Find("Points").GetComponent<CircleDraw>().restart = mode;
        GameObject.Find("Points").GetComponent<CircleDraw>().enabled = mode;
        GameObject.Find("Points").GetComponent<Counter>().enabled = mode;
        GameObject.Find("Points").GetComponent<Sound>().enabled = mode;
        GameObject.Find("Points").GetComponent<Sound>().restart = mode;
        GameObject.Find("Points").GetComponent<Beat>().enabled = mode;
        LineRenderer[] LR = GameObject.Find("Points").GetComponentsInChildren<LineRenderer>();
        for(int i = 0; i < LR.Length; i++)
        {
            LR[i].enabled = mode;
        }        
    }
    IEnumerator NextLineDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        nextLine = true;
    }
    public void NextLine()
    {
        nextLine = true;
    }
    public void SameLine()
    {
        sameLine = true;
    }
}
