using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataExtract : MonoBehaviour
{

    string filename = "";

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/results.csv";
    }

    // Update is called once per frame
    void Update()
    {
        // static boolean in level manager that determines if game is over
        /* if(!LevelManagerScript.isGameOver){
        WriteCSV();
    } */
    }

    public void WriteCSV()
    {
        /*  LevelManagerScript.choiceOne is replaced with the static variable of choice 1
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Choice 1, Choice 2, Choice 3");
        tw.Close();

        tw.WriteLine(LevelManagerScript.choiceOne + "," + LevelManagerScript.choiceTwo + "," + LevelManagerScript.choiceThree);
        tw.Close();
        */
    }
}
