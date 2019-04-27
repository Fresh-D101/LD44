using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerData m_playerData;
    
    private static List<ISerialize> MasterData = new List<ISerialize>();
    
    private void Awake()
    {
        m_playerData = new PlayerData(2000f);
    }

    private static void Save()
    {
        var jsonStrings = new StringCollection();
        
        foreach (var serializable in MasterData)
        {
            jsonStrings.Add(serializable.Serialize());
        }
        
        
    }
    
}
