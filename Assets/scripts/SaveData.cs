using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using MenuCommand = System.ComponentModel.Design.MenuCommand;

public class SaveData : MonoBehaviour
{
    float timer;
    string path;
    string fileName;
    string[] text;
    string[] text2;
    float braker;
    string fileTitleResults;
    string fileTitlePythonResults;
    int fileNumber;
    string result;
    string perfectResult;
    string errorsResult;
    string pythonResults;
    float[] errors;
    bool pause;
    int counter;
    string[] lign;
    int lignCounter;
    string pathParameters;
    bool manipulandumPressed;
    public float[] averageBeatErrors;
    int beatCounter;
    public float[] averageSpeechErrors;
    int speechCounter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        manipulandumPressed = false;
        pause = true;
        fileNumber = 0;
        fileTitleResults = "result" + Convert.ToString(fileNumber);
        fileTitlePythonResults = "pythonResults";
        path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\Beats\";
        fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
        text = File.ReadAllLines(@path + fileName + ".txt");
        timer = Single.Parse(text[0]) / braker;
        lignCounter = 0;
        pathParameters = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\processFile.txt";
        text2 = File.ReadAllLines(@pathParameters);
        averageBeatErrors = new float[text2.Length * 1];
        beatCounter = 0;
        averageSpeechErrors = new float[text2.Length * 1];
        speechCounter = 0;
        lign = text2[lignCounter].Split(';');
        result = "[ " + lign[0] + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
        perfectResult = "[ p" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
        errorsResult = "[ e" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
        pythonResults = lign[0] + ";";
        errors = new float[text.Length];
        for(int i = 0; i < text.Length; i++)
        {
            perfectResult += Convert.ToString(Single.Parse(text[i]) / braker);
            perfectResult += " ";
            errors[i] = Single.Parse(text[i]) / braker;
        }

        NewFileTxt(fileTitleResults);
        WriteString("", fileTitleResults);
        WriteString("NEW RESULT", fileTitleResults);
        WriteString("", fileTitleResults);
        WriteString(perfectResult, fileTitleResults);
    }
    [MenuItem("Tools/Write file")]
    static void WriteString(string value, string title)
    {
        string path = @"Assets\results\" + title + ".txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(value);
        writer.Close();
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }

    static void NewFileTxt(string title)
    {
        string path = @"Assets\Beats\" + title + ".txt";
        File.CreateText(path);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            timer = Single.Parse(text[0]) / braker;
        }
        if (Input.GetKeyDown("p"))
        {
            pause = false;
            if (counter < text.Length)
            {
                errors[counter] = Math.Abs(errors[counter] - timer);
                errorsResult += Convert.ToString(errors[counter]) + " ";
                pythonResults += Convert.ToString(errors[counter]) + ";";
                counter++;
            }
            result += Convert.ToString(timer) + " ";            
        }
        else if ((float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !manipulandumPressed)//else if (Input.GetKeyDown("p"))
        {
            pause = false;
            if (counter < text.Length)
            {
                errors[counter] = Math.Abs(errors[counter] - timer);
                errorsResult += Convert.ToString(errors[counter]) + " ";
                pythonResults += Convert.ToString(errors[counter]) + ";";
                counter++;
            }
            result += Convert.ToString(timer) + " ";
            manipulandumPressed = true;
        }
        if ((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            manipulandumPressed = false;
        }
        if (Input.GetKeyDown("n"))
        {
            counter = 0;
            WriteString(result, fileTitleResults);
            WriteString(errorsResult, fileTitleResults);
            WriteString(pythonResults, fileTitlePythonResults);
            WriteString(Convert.ToString("Absolute Error Mean: " + Average(errors)), fileTitleResults);
            if (lign[0] == "b")
            {
                averageBeatErrors[beatCounter] = Average(errors);
                beatCounter++;
            }
            if (lign[0] == "s")
            {
                averageSpeechErrors[speechCounter] = Average(errors);
                speechCounter++;
            }
            WriteString("", fileTitleResults);
            lignCounter++;
            if (lignCounter < text2.Length)
            {
                lign = text2[lignCounter].Split(';');
            }
            else
            {
                lignCounter = 0;
                lign = text2[lignCounter].Split(';');
            }
            result = "[ " + lign[0] + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            timer = Single.Parse(text[0]) / braker;
            pause = true;

            fileName = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
            braker = GameObject.Find("GameManager").GetComponent<Parameters>().braker;
            text = File.ReadAllLines(@path + fileName + ".txt");
            timer = Single.Parse(text[0]) / braker;
            perfectResult = "[ p" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            errorsResult = "[ e" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            pythonResults = lign[0] + ";";
            errors = new float[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                perfectResult += Convert.ToString(Single.Parse(text[i]) / braker);
                perfectResult += " ";
                errors[i] = Single.Parse(text[i]) / braker;
            }
            WriteString(perfectResult, fileTitleResults);
        }
        if (Input.GetKeyDown("s"))
        {
            counter = 0;
            WriteString(result, fileTitleResults);
            WriteString(errorsResult, fileTitleResults);
            WriteString(pythonResults, fileTitlePythonResults);
            WriteString(Convert.ToString("Absolute Error Mean: " + Average(errors)), fileTitleResults);
            if (lign[0] == "b")
            {
                averageBeatErrors[beatCounter] = Average(errors);
                beatCounter++;
            }
            if (lign[0] == "s")
            {
                averageSpeechErrors[speechCounter] = Average(errors);
                speechCounter++;
            }
            WriteString("", fileTitleResults);

            result = "[ " + lign[0] + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            timer = Single.Parse(text[0]) / braker;
            pause = true;
            perfectResult = "[ p" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            errorsResult = "[ e" + " ; " + " " + lign[1] + " ; breaker: " + lign[2] + " ] --> ";
            pythonResults = lign[0] + ";";
            errors = new float[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                perfectResult += Convert.ToString(Single.Parse(text[i]) / braker);
                perfectResult += " ";
                errors[i] = Single.Parse(text[i]) / braker;
            }
            WriteString(perfectResult, fileTitleResults);
        }
        if (!pause)
        {
            timer += Time.deltaTime;
        }
    }

    static float Average(float[] tab)
    {
        float average = 0f;
        for(int i=0; i < tab.Length; i++)
        {
            average += tab[i];
        }
        average /= tab.Length;
        return average;
    }
}
