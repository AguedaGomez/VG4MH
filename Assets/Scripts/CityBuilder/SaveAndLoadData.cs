using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEngine;

public static class SaveAndLoadData
{
    public static string fullPath = Application.persistentDataPath;
    public static bool SaveinFile(string fileName, string fileContent)
    {
        /*var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(fullPath);

        try
        {
            File.WriteAllText(fullPath, fileContent);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }*/
        PlayerPrefs.SetString("save", fileContent);
        return true;
    }

    public static bool LoadFromFile(string fileName, out string result)
    {
        result = PlayerPrefs.GetString("save", "no exists");
        return result != "no exists";
        /*var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            if (File.Exists(fullPath))
            {
                result = File.ReadAllText(fullPath);
                Debug.Log("contenido guardado: " + result);
                return true;
            }
            else
            {
                result = "";
                return false;
            }
                

        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }*/
    }

}
