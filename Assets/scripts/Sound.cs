using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource pianoSound;
    bool ManipulandumPressed = false;
    bool keyReleased = false;
    public bool restart;
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        //pianoSound.time = 0.169f;
        string path = "C:/Users/paul-/OneDrive/Bureau/Projets/PHD/TestMax";
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("Max", "/effort", 1);
        OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/note.wav");
        OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/note.txt");
        List<object> msgRestart = new List<object> { "/restart" };
        OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
        OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 8);
        OSCHandler.Instance.SendMessageToClient("Max", "/pitch", 52.5f);
    }

    private void Update()
    {
        if (restart)
        {
            Restart();
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("n"))
        {
            string path = "C:/Users/paul-/OneDrive/Bureau/Projets/PHD/TestMax";
            OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/note.wav");
            OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/note.txt");
            List<object> msgRestart = new List<object> { "/restart" };
            OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
            OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 10);
        }
        if (Input.GetKeyDown("p"))
        {
            /*StopAudio();
            PlayForTime(0.1f);*/
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 1);
            keyReleased = true;
        }
        if (((float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !ManipulandumPressed))
        {
            StopAudio();
            PlayForTime(0.1f);
            ManipulandumPressed = true;
            keyReleased = true;
        }
        if ((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            ManipulandumPressed = false;
        }
        if (keyReleased)
        {
            //OSCHandler.Instance.SendMessageToClient("Max", "/binary", 2);
            keyReleased = false;
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
        //pianoSound.time = 0.169f;
    }
    void Restart()
    {
        restart = false;
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        //pianoSound.time = 0.169f;
        string path = "C:/Users/paul-/OneDrive/Bureau/Projets/PHD/TestMax";
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("Max", "/effort", 1);
        OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/note.wav");
        OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/note.txt");
        List<object> msgRestart = new List<object> { "/restart" };
        OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
        OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 8);
        OSCHandler.Instance.SendMessageToClient("Max", "/pitch", 52.5f);
    }
}
