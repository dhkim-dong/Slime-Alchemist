using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    string filename = "";

    [System.Serializable]
    public class Player
    {
        public string name;
        public int health;
        public int damage;
        public int defence;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }

    [System.Serializable]
    public class StringClass
    {
        public string name;
        public string description;
    }

    [System.Serializable]
    public class stringListClass
    {
        public StringClass[] strings;
    }

    public PlayerList myPlayerList = new PlayerList();

    public stringListClass myList = new stringListClass();

    private void Start()
    {
        filename = Application.dataPath + "/test2.csv";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteStringCSV();
        }
    }

    public void WriteCSV()
    {
        if (myPlayerList.player.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Health");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < myPlayerList.player.Length; i++)
            {
                tw.WriteLine(myPlayerList.player[i].name + "," + myPlayerList.player[i].health);
            }

            tw.Close();
        }
    }

    public void WriteStringCSV()
    {
        if(myList.strings.Length > 0)
        {
            StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default);
            sw.WriteLine("Name, Description");
            sw.Close();

            sw = new StreamWriter(filename, true, System.Text.Encoding.Default);

            for (int i = 0; i < myList.strings.Length; i++)
            {
                sw.WriteLine(myList.strings[i].name + "," + myList.strings[i].description);
            }

            sw.Close();
        }
    }

    public void ReadCSVFile(String path)
    {
        StreamReader sr;
        sr = new StreamReader(path, System.Text.Encoding.Default);

        while (!sr.EndOfStream)
        {
            string s = sr.ReadLine();
        }

        sr.Close();
    }

    public void WriteCSVFile(string path)
    {
        StreamWriter sw;
        sw = new StreamWriter(path, false, System.Text.Encoding.Default);

        sw.WriteLine("data");
    }
}