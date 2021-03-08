using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerOptions : MonoBehaviour
{
    List<KeyValuePair<string, float>> data = new List<KeyValuePair<string, float>>();

    void Start()
    {
        Read();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            foreach (KeyValuePair<string, float> pair in data)
                print(pair.Key + "=" + pair.Value);
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
    }

    public void Read()
    {
        string fileData;
        using (StreamReader inputFile = new StreamReader("Assets/Resources/SaveFile.txt"))
            fileData = inputFile.ReadToEnd();
        data.Clear();
        string[] lines = fileData.Split("\n".ToCharArray());
        foreach (string line in lines)
        {
            string[] pair = line.Split("=".ToCharArray());
            if (pair.Length == 2)
                data.Add(new KeyValuePair<string, float>(pair[0], float.Parse(pair[1])));
        }
    }

    public void Save()
    {
        using (StreamWriter outputFile = new StreamWriter("Assets/Resources/SaveFile.txt"))
            foreach (KeyValuePair<string, float> pair in data)
            {
                outputFile.WriteLine(pair.Key + "=" + pair.Value.ToString());
            }
    }

    public bool GetValue(string key, ref float value)
    {
        KeyValuePair<string, float> kvp = data.Find(pair => pair.Key == key);
        if (kvp.Key == key)
        {
            value = kvp.Value;
            return true;
        }
        return false;
    }

    public void ChangeOrAddValue(string key, float value)
    {
        
    }
}
