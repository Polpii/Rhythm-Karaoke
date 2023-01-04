using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MaxMessageSender : MonoBehaviour
{
    string path = "C:/Users/paul-/OneDrive/Bureau/Projets/PHD/TestMax";
    bool keyReleased = false;
    bool ManipulandumPressed = false;
    string fileName;
    public bool restart;
    public bool playing;
   
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        OSCHandler.Instance.Init();
        //List<object> msg = new List<object> { "/rhythmMode" };
        //msg.Add("binary");
        OSCHandler.Instance.SendMessageToClient("Max", "/rhythmMode", "binary" );
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            Restart();
        }
        if (Input.GetMouseButtonDown(0) ||Input.GetKeyDown("n")) {
            OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/" + fileName + ".wav");
            OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/" + fileName + ".txt");
            // This restarts the binary mode  
            List<object> msgRestart = new List<object> { "/restart" };
            OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
            OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 2);
        }
        else if (Input.GetKeyDown("a"))
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/effort", 1 );
            keyReleased = true;
        }
        else if (Input.GetKeyDown("q"))
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/effort", 0);
            keyReleased = true;
        }
        else if (playing && Input.GetKeyDown("p"))
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 1);
            keyReleased = true;
        }
        else if (playing && (float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !ManipulandumPressed)//else if (Input.GetKeyDown("p"))
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 1);
            keyReleased = true;
            ManipulandumPressed = true;
        }
        if((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            ManipulandumPressed = false;
        }
        if (keyReleased)
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 2);
            keyReleased = false;
        }
    }
    void Restart()
    {
        restart = false;
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("Max", "/rhythmMode", "binary");

        //click mouse
        OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/" + fileName + ".wav");

        OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/" + fileName + ".txt");

        // This restarts the binary mode  
        List<object> msgRestart = new List<object> { "/restart" };
        OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
        OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 2);
    }
}
