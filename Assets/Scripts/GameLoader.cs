using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameLoader
{
    //Array aus CatastropheData
    //Catastrophe Prefab
    //Catastrophe parent transform

    private const uint c_HeaderID = 0xDEAFDEAD;
    //Update whenever Savegame Code changes
    private const uint c_SaveGameVersion = 1;

    public static bool SaveGame(string filename, PlayerData playerData)
    {
        var sPath = Path.Combine(Application.persistentDataPath, "SaveGames", filename);
        using (var fileStream = new FileStream(sPath, FileMode.OpenOrCreate))
        {
            using (var writer = new BinaryWriter(fileStream))
            {
                writer.Write(c_HeaderID);
                writer.Write(c_SaveGameVersion);

                playerData.Serialize(writer);
            }
        }

        return true;
    }

    public static PlayerData LoadSaveGame(string filename)
    {
        var playerData = new PlayerData();
        var sPath = Path.Combine(Application.persistentDataPath, "SaveGames", filename);
        using (var fileStream = new FileStream(sPath, FileMode.Open))
        {
            using (var reader = new BinaryReader(fileStream))
            {
                var header = reader.ReadUInt32();
                if (header != c_HeaderID)
                {
                    return null;
                }

                var version = reader.ReadUInt32();
                if (version != c_SaveGameVersion)
                {
                    return null;
                }

                if (!playerData.Deserialize(reader))
                {
                    return null;
                }
            }
        }

        return playerData;
    }

    public static void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
