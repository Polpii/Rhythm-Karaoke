using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    string fileName;
    bool start;

    [Header("Audio Stuff")]
    AudioSource song;
    AudioClip audioClip;
    string path;
    float braker;
    public bool restart;

    // Start is called before the first frame update
    void Start()
    {        
        restart = false;
        braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
        path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\audio\";
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        fileName += ".wav";
        start = false;
        song = gameObject.GetComponent<AudioSource>();
        var pitchBendGroup = Resources.Load("Pitch");
        //song.outputAudioMixerGroup = pitchBendGroup;
        song.pitch = braker;
        song.outputAudioMixerGroup.audioMixer.SetFloat("pitchBend", 1f / braker);
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            Restart();
        }
        if (start)
        {
            braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
            song.pitch = braker;
            song.outputAudioMixerGroup.audioMixer.SetFloat("pitchBend", 1f / braker);
            StartCoroutine(LoadAudio());
            start = false;
        }
        if (Input.GetKeyDown("r"))
        {
            start = true;
        }
        /*if (Input.GetKeyDown("n"))
        {
            start = true;
        }
        if (Input.GetKeyDown("s"))
        {
            start = true;
        }*/
    }

    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(path, fileName);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = fileName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        song.clip = audioClip;
        song.Play();
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }
    void Restart()
    {
        restart = false;
        braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        fileName += ".wav";
        start = true;
        song = gameObject.GetComponent<AudioSource>();
        song.pitch = braker;
        song.outputAudioMixerGroup.audioMixer.SetFloat("pitchBend", 1f / braker);
    }
}
