using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class LoadPresetScript : MonoBehaviour {
    public Text text;
    public Dropdown dropdown;
    public InputField field;
    public List<List<string>> soundList;
    public Button recordButton;
    void Awake()
    {
        soundList = new List<List<string>>();
        StringBuilder temp = new StringBuilder();
        temp.Append(Application.dataPath);
        temp.Append("/Presets/");
        field.text = temp.ToString();
        LoadPreset();
    }
    public void LoadPreset()
    {
        bool already = true;
        if(field.text == "")
        {
            text.text = "You must enter a file path to load presets.";
            return;
        }
        string[] files = null;
        try
        {
            files = Directory.GetFiles(field.text, "*.pre");
        }
        catch(ArgumentException e)
        {
            text.text = "This file path does not exist. Please enter a correct path.";
            return;
        }
        catch(DirectoryNotFoundException e)
        {
            text.text = "This file path does not exist. Please enter a correct path.";
            return;
        }
        if (files.Length != 0)
        {
            for(int i = 0; i < files.Length; ++i)
            {
                bool skip = false;
                using (StreamReader sr = File.OpenText(files[i]))
                {
                    string line, name = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        skip = false;
                        int j = 0;
                        while (line[j] == ' ') ++j;
                        int numBoxes = Int32.Parse(line[j++].ToString());
                        while (line[j] != '"') ++j;
                        ++j;
                        {
                            StringBuilder temp = new StringBuilder();
                            while (line[j] != '"') temp.Append(line[j++]);
                            name = temp.ToString();
                        }
                        for(j = 0; j < dropdown.options.Count; ++j)
                        {
                            if(name.Equals(dropdown.options[j].text))
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (skip) continue;
                    }
                }
                if (skip) continue;
                using (StreamReader sr = File.OpenText(files[i]))
                {
                    already = false;
                    string line, name;
                    while ((line = sr.ReadLine()) != null)
                    {
                        int j = 0;
                        while (line[j] == ' ') ++j;
                        int numBoxes = Int32.Parse(line[j++].ToString());
                        while (line[j] != '"') ++j;
                        ++j;
                        {
                            StringBuilder temp = new StringBuilder();
                            while (line[j] != '"') temp.Append(line[j++]);
                            name = temp.ToString();
                        }
                        while (line[j] == '"' || line[j] == ' ') ++j;
                        List<string> sounds = new List<string>();
                        for(int k = 0; k < numBoxes; ++k)
                        {
                            while (j < line.Length && line[j] == ' ') ++j;
                            StringBuilder temp = new StringBuilder();
                            while (j < line.Length && line[j] != ' ') temp.Append(line[j++]);
                            sounds.Add(temp.ToString());
                        }
                        dropdown.options.Add(new Dropdown.OptionData(name, null));
                        soundList.Add(sounds);
                    }
                }
            }
            if(already)
            {
                text.text = "All of the presets at this file path have already been loaded.";
                return;
            }
            dropdown.value = -1;
            text.text = "Loaded presets! Choose them in the dropdown above.";
            field.text = "";
            recordButton.gameObject.SetActive(true);
        }
        else text.text = "No presets found. Please enter a new directory.";
    }
}
