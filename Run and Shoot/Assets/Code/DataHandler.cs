using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DataHandler : MonoBehaviour
{
    public SerializationScript serializedScript = new SerializationScript();

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create("save.dat");
        formatter.Serialize(fileStream, serializedScript);
        fileStream.Close();
        Debug.Log("Данные успешно сохранены.");
    }

    public void LoadData()
    {
        if (File.Exists("save.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open("save.dat", FileMode.Open);
            serializedScript = (SerializationScript)formatter.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log("Данные успешно загружены.");
        }
        else
        {
            Debug.Log("Файл сохранения не найден.");
        }
    }
}

[System.Serializable]
public class SerializationScript
{
    public int level;
    public float bulletSpeed;
    public float bulletInterval;
    public float playerSpeed;
    public int damagePlayer;
}
