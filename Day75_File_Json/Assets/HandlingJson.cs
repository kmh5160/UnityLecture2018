using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class HandlingJson : MonoBehaviour {
    string pathJson;
    GameScore gs;

	void Start () {
        pathJson = Application.persistentDataPath + "/gameScore.json";
        print(pathJson);

        gs = new GameScore();
        gs.level = 10;
        gs.timeElapsed = 300.1f;
        gs.playerName = "ctkim";

        Item item = new Item();
        item.itemID = 1000;
        item.iconImage = "image1.png";
        item.price = 10000;
        gs.items = new List<Item>();
        gs.items.Add(item);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
            SaveJsonFile();
        if (Input.GetKeyDown(KeyCode.L))
            LoadJsonFile();
    }

    private void LoadJsonFile()
    {
        if (File.Exists(pathJson))
        {
            string dataAsJson = File.ReadAllText(pathJson);
            GameScore newGS = JsonUtility.FromJson<GameScore>(dataAsJson);
            print(newGS);
            print(newGS.level == 10);
        }
        else
        {
            print("no file!");
        }
    }

    private void SaveJsonFile()
    {
        string dataAsJson = JsonUtility.ToJson(gs, true);
        print(dataAsJson);
        File.WriteAllText(pathJson, dataAsJson);
    }
}

[Serializable]
public class GameScore
{
    public int level;
    public float timeElapsed;
    public string playerName;

    public List<Item> items;

    [NonSerialized]
    public string dontCare;

    public override string ToString()
    {
        return level + ", " + timeElapsed + ", " + playerName;
    }
}

[Serializable]
public class Item
{
    public int itemID;
    public string iconImage;
    public int price;

    public override string ToString()
    {
        return itemID.ToString();
    }
}
