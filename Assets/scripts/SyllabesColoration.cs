using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SyllabesColoration : MonoBehaviour
{
    TextMeshPro TextDisplay;
    string[] text;
    int[,] textSyllabes;
    int countSyllabes;
    int countLetters;
    int meshIndex;
    int vertexIndex;
    int lign;
    Color32[] vertexColors;
    Color color;
    string path;
    string file;
    bool ManipulandumPressed;
    public bool restart;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }
    // Update is called once per frame
    void Update()
    {
        if (lign < text.Length)
        {
            TextDisplay.text = text[lign];
        }
        if (Input.GetMouseButtonDown(0))
        {
            lign = 0;
            for (int i = 0; i < TextDisplay.textInfo.characterCount; i++)
            {
                ChangeColorCharacter(i, UnityEngine.Color.black);
            }
            countLetters = 0;
            countSyllabes = 0;
        }
        if (Input.GetKeyDown("p") || ((float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !ManipulandumPressed))
        {
            if (lign >= text.Length)
            {
                lign = 0;
                for (int i = 0; i < TextDisplay.textInfo.characterCount; i++)
                {
                    ChangeColorCharacter(i, UnityEngine.Color.black);
                }
                countLetters = 0;
                countSyllabes = 0;
            }
            if (countSyllabes >= textSyllabes.GetLength(1) || textSyllabes[lign, countSyllabes] == -1)
            {
                lign += 1;
                if (lign >= text.Length)
                {
                    lign = 0;
                }
                for (int i = 0; i < TextDisplay.textInfo.characterCount; i++)
                {
                    ChangeColorCharacter(i, UnityEngine.Color.black);
                }
                countLetters = 0;
                countSyllabes = 0;
                for (int i = 0; i < countLetters + textSyllabes[lign, countSyllabes]; i++)
                {
                    ChangeColorCharacter(i, color);
                }
                countLetters += textSyllabes[lign, countSyllabes];
                countSyllabes += 1;
            }
            else
            {
                for (int i = 0; i < countLetters + textSyllabes[lign, countSyllabes]; i++)
                {
                    ChangeColorCharacter(i, color);
                }
                countLetters += textSyllabes[lign, countSyllabes];
                countSyllabes += 1;
            }
            ManipulandumPressed = true;
        }
        if ((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            ManipulandumPressed = false;
        }
        if (Input.GetKeyDown("n") || Input.GetKeyDown("s") || restart)
        {
            Restart();
            for (int i = 0; i < TextDisplay.textInfo.characterCount; i++)
            {
                ChangeColorCharacter(i, UnityEngine.Color.black);
            }
            restart = false;
        }
    }
    void ChangeColorCharacter(int character, Color c)
    {
        meshIndex = TextDisplay.textInfo.characterInfo[character].materialReferenceIndex;
        vertexIndex = TextDisplay.textInfo.characterInfo[character].vertexIndex;
        vertexColors = TextDisplay.textInfo.meshInfo[meshIndex].colors32;
        vertexColors[vertexIndex + 0] = c;
        vertexColors[vertexIndex + 1] = c;
        vertexColors[vertexIndex + 2] = c;
        vertexColors[vertexIndex + 3] = c;
        TextDisplay.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
    void Restart()
    {
        restart = false;
        countSyllabes = 0;
        countLetters = 0;
        lign = 0;
        color = UnityEngine.Color.red;
        ManipulandumPressed = false;
        path = @"C:\Users\paul-\UnityProject\Test_Link_Max&Unity\Assets\Text\";
        file = GameObject.Find("GameManager").GetComponent<Parameters>().FileName;
        TextDisplay = FindObjectOfType<TextMeshPro>();
        text = File.ReadAllLines(@path + file + ".txt");
        if (file == "emmenezMoi")
        {
            textSyllabes = new int[,] {
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, 1, 1, 1, 1 },
                { 4, 2, 7, 3, 3, 4, 1, 4, 3, 3, 2, 5, 1, 3, 2, 5, -1, -1, 1, 1, 1, 1, 1 },
                { 4, 4, 5, 3, 4, 3, 6, 2, 3, 5, 1, 4, 4, -1, 5, 3, 3, -1, 1, 1, 1, 1, 1 },
                { 5, 4, 3, 2, 7, 4, 2, 3, 3, 3, 6, 2, 4, 1, 2, 4, 3, -1, -1, 1, 1, 1, 1 },
                { 0, 4, 5, 3, 4, 2, 7, 3, 5, 3, 3, 4, 3, 4, 2, 5, 1, 4, 3, 5, 3, -1, -1 },
                { 3, 4, 4, 2, 2, 4, 3, 2, 2, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 3, 4, 5, 2, 4, 3, 3, 3, 4, 4, 3, 3, 2, 3, 5, -1, 1, -1, 1, 1, 1, 1, 1 },
                { 0, 4, 2, 5, 2, 3, 4, 5, 3, 2, 2, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 3, 2, 4, 3, 5, -1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 2, 1, 1, 3, 8, 4, 3, 3, 3, 3, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1 },
                { 2, 5, 4, 3, 5, 1, 3, 4, 3, 4, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1 },
                { 2, 3, 4, 3, 2, 2, 3, 3, 2, 2, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1 },
                { 2, 4, 6, 3, 2, 3, 3, 2, 4, 1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -1 },// 13/39

                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
                { 0, 5, 4, 2, 4, 3, 3, 6, 3, 4, 2, 2, 3, 4, 5, 3, 3, -1, -1, -1, -1, -1, -1 },
            };
        }
        if (file == "salut2")
        {
            textSyllabes = new int[,] { { 2, 3 } };
        }
        if (file == "chantons3")
        {
            textSyllabes = new int[,] { { 4, 5, 4 } };
        }
        if (file == "rigoler4")
        {
            textSyllabes = new int[,] { { 4, 3, 2, 3 } };
        }
        if (file == "manger5")
        {
            textSyllabes = new int[,] { { 4, 4, 4, 4, 3 } };
        }
        if (file == "marie6")
        {
            textSyllabes = new int[,] { { 2, 4, 4, 5, 3, 4 } };
        }
        if (file == "soir7")
        {
            textSyllabes = new int[,] { { 4, 4, 4, 4, 3, 3, 5 } };
        }
        if (file == "nicolas8")
        {
            textSyllabes = new int[,] { { 2, 2, 4, 2, 2, 5, 3, 4 } };
        }
        if (file == "sophie8")
        {
            textSyllabes = new int[,] { { 2, 5, 3, 6, 3, 7, 4, 4 } };
        }
        if (file == "tableau9")
        {
            textSyllabes = new int[,] { { 4, 3, 3, 4, 3, 3, 2, 3, 5 } };
        }
        if (file == "jungle4")
        {
            textSyllabes = new int[,] { { 2, 3, 5, 4 } }; //4 syllables
        }
        if (file == "jungle8")
        {
            textSyllabes = new int[,] { { 2, 3, 5, 4, 5, 5, 4, 5 } }; //8 syllables
        }
        if (file == "jungle16")
        {
            textSyllabes = new int[,] { { 2, 3, 5, 4, 5, 5, 4, 5, 5, 4, 5, 4, 5, 5, 4, 5 } }; //16 syllables
        }
        if (file == "jungle26")
        {
            textSyllabes = new int[,] { { 2, 3, 5, 4, 5, 5, 4, 5, 5, 4, 5, 4, 5, 5, 4, 5, 3, 5, 3, 3, 3, 5, 3, 3, 2, 7 } }; //26 syllables
        }
        if (file == "LetItGo6")
        {
            textSyllabes = new int[,] { { 4, 3, 4, 4, 3, 4 } }; //6 syllables
        }
        if (file == "LetItGo13")
        {
            textSyllabes = new int[,] { { 4, 3, 4, 4, 3, 4, 6, 5, 3, 5, 1, 2, 6 } }; //13 syllables
        }
        if (file == "LetItGo26")
        {
            textSyllabes = new int[,] { { 4, 3, 4, 4, 3, 4, 6, 5, 3, 5, 1, 2, 6, 4, 3, 4, 4, 3, 4, 5, 1, 4, 4, 5, 4, 5 } }; //26 syllables
        }
    }
}
