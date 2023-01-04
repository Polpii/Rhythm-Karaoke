using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource pianoSound;
    bool ManipulandumPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        pianoSound = GetComponent<AudioSource>();
        pianoSound.volume = 0.5f;
        pianoSound.time = 0.169f;
        string path = "C:/Users/paul-/OneDrive/Bureau/Projets/PHD/TestMax";
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("Max", "/audio", path + "input_voks/audio/note.wav");
        OSCHandler.Instance.SendMessageToClient("Max", "/labeling", path + "input_voks/labeling/note.txt");
        List<object> msgRestart = new List<object> { "/restart" };
        OSCHandler.Instance.SendMessageToClient("Max", "", msgRestart);
        OSCHandler.Instance.SendMessageToClient("Max", "/articulationSpeed", 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            /*StopAudio();
            PlayForTime(0.1f);*/
            OSCHandler.Instance.SendMessageToClient("Max", "/binary", 1);
        }
        if (((float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !ManipulandumPressed))
        {
            StopAudio();
            PlayForTime(0.1f);
            ManipulandumPressed = true;
        }
        if ((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            ManipulandumPressed = false;
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
}
