using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : MonoBehaviour
{
    string path = "C:/Users/paul-/UnityProject/Test_Link_Max&Unity/Assets/processFile.txt";
    string[] text;
    // Start is called before the first frame update
    void Start()
    {
        text = File.ReadAllLines(@path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
