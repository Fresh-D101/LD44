using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using GameEvents;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    ////Prefabs////
    [SerializeField] private GameObject InvoiceDesignTemplateWhite;
    [SerializeField] private GameObject InvoiceDesignTemplateRed;
    [SerializeField] private GameObject InvoiceDesignTemplateBlue;

    private PlayerData m_playerData;
    
    private static List<ISerialize> MasterData = new List<ISerialize>();
    
    private void Awake()
    {
        Instance = this;
        m_playerData = new PlayerData(2000);

        InvokeRepeating("HourElapser", PlayerData.TimeScale, PlayerData.TimeScale);
    }

    private static void Save()
    {
        var jsonStrings = new StringCollection();
        
        foreach (var serializable in MasterData)
        {
            jsonStrings.Add(serializable.Serialize());
        }     
    }

    public GameObject GetInvoicePrefab(out EInvoiceDesignType designType)
    {
        designType = EInvoiceDesignType.White;
        return InvoiceDesignTemplateWhite;
    }
    
    public void HourElapser()
    {
        GameEventManager.TriggerEvent(new GameEvent_HourElapsed());
    }
}
