using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    string path;
    string fileName;
    public bool start;
    string[] text;
    public float timer;
    int counter;
    float braker;
    AudioSource pianoSound;
    public bool restart;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        restart = false;
        start = false;
        path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\Beats\";
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
        text = File.ReadAllLines(@path + fileName + ".txt");
        timer = (float)(Single.Parse(text[0]) / braker);
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        pianoSound.time = 0.169f;
    }
    void Update()
    {
        if (start)
        {
            if (timer >= Single.Parse(text[counter]) / braker)
            {
                PlayForTime(0.1f);
                counter++;
            }
            if (counter >= text.Length)
            {
                start = false;
            }            
        }
        if (Input.GetKeyDown("r"))
        {
            counter = 0;
            timer = Single.Parse(text[0]) / braker;
            start = true;
        }
        /*if (Input.GetKeyDown("n"))
        {
            counter = 0;
            Restart();
        }*/
        timer += Time.deltaTime;
        if (restart)
        {
            Restart();
        }
    }
    public void PlayForTime(float time)
    {
        pianoSound.Play();
        Invoke("StopAudio", time);
    }
    private void StopAudio()
    {
        pianoSound.Stop();
        pianoSound.time = 0.169f;
    }
    void Restart()
    {
        counter = 0;
        restart = false;
        start = true;
        path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\Beats\";
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
        text = File.ReadAllLines(@path + fileName + ".txt");
        timer = (float)(Single.Parse(text[0]) / braker);
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        pianoSound.time = 0.169f;
    }
}





