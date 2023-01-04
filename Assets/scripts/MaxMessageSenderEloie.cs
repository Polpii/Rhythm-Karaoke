using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MaxMessageSenderEloie : MonoBehaviour
{
    string path = "Ton/Projet/Voks"; // Là où il y a les dossiers: "input_voks", "OscVoksBinary", "Voks". Le tiens s'appelle Polpi je crois. Les slash doivent être dans ce sens là: "/".
    string fileName;
    bool keyReleased;

    void Start()
    {
        keyReleased = false;
        fileName = "marie";
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("Max", "/rhythmMode", "binary" );
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/" + fileName + ".wav");
            OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/" + fileName + ".txt"); 
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
        else if (Input.GetKeyDown("p"))//Choisit la lettre que tu veux, moi j'ai "p".
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 1);
            keyReleased = true;
        }
        if (keyReleased)
        {
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 2);
            keyReleased = false;
        }
    }
}
