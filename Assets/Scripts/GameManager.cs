using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using GameEvents;

public class GameManager : MonoBehaviour, IGameEventListener<GameEvent_MenuOpen>, IGameEventListener<GameEvent_InvoiceOpen>
{
    public static GameManager Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject InvoiceDesignTemplateWhite;
    [SerializeField] private GameObject InvoiceDesignTemplateRed;
    [SerializeField] private GameObject InvoiceDesignTemplateBlue;

    private static GameObject s_whiteInvoice;
    private static GameObject s_redInvoice;
    private static GameObject s_blueInvoice;
    
    [Header("Balancing Values")]
    [SerializeField] private int m_initialMoney;
    [SerializeField] private int m_maxExtends;
    [SerializeField] private int m_neededExtendProgress;

    [SerializeField] private Texture2D m_cursor;

    private PlayerData m_playerData;
    
    private static List<ISerialize> MasterData = new List<ISerialize>();
    private static readonly int Exit = Animator.StringToHash("Exit");

    public static class InvoiceFactory
    {
        public static Invoice CreateNewInvoice(InvoiceData data)
        {
            //Hier muss später mal ein code zur randomisierung hin
            var invoice = s_whiteInvoice.GetComponent<Invoice>();
#if DEBUG
            if (invoice == null)
            {
                Debug.LogError("InvoiceTemplatePrefabs MUST contain an Invoice Script!");
            }
#endif
            
            data.InvoiceDesignType = EInvoiceDesignType.White;
            invoice.Initialize(data, true);
            return invoice;
        }

        public static Invoice CreateInvoiceFromArchive(InvoiceData data)
        {
            Invoice invoice = null;
            switch (data.InvoiceDesignType)
            {
                case EInvoiceDesignType.White:
                    invoice = s_whiteInvoice.GetComponent<Invoice>();
                    break;
                
                default: 
                    invoice = s_whiteInvoice.GetComponent<Invoice>();
                    Debug.Log("Default Invoice has been chosen!");
                    break;
            }

            invoice.Initialize(data, false);
            return invoice;
        }
    }
    
    
    private void Awake()
    {
        Instance = this;
        
        Cursor.SetCursor(m_cursor, new Vector2(0.25f, 0f), CursorMode.ForceSoftware);
        m_playerData = new PlayerData(m_initialMoney, m_maxExtends, m_neededExtendProgress);

        SetUpPool();

        InvokeRepeating("HourElapser", PlayerData.TimeScale, PlayerData.TimeScale);
    }

    private void SetUpPool()
    {
        s_whiteInvoice = Instantiate(InvoiceDesignTemplateWhite);
        s_whiteInvoice.SetActive(false);

        s_redInvoice = Instantiate(InvoiceDesignTemplateRed);
        s_redInvoice.SetActive(false);
        
        s_blueInvoice = Instantiate(InvoiceDesignTemplateBlue);
        s_blueInvoice.SetActive(false);
    }

    //TODO
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

    public void OnGameEvent(GameEvent_MenuOpen eventType)
    {
        Cursor.visible = eventType.IsOpen;
    }

    public void OnGameEvent(GameEvent_InvoiceOpen eventType)
    {
        throw new NotImplementedException();
    }

    public void ResetInvoice(EInvoiceDesignType designType)
    {
        GameObject objectToReset;
        switch (designType)
        {
            case EInvoiceDesignType.White: objectToReset = s_whiteInvoice;
                break;
            default: objectToReset = s_whiteInvoice;
                break;
        }
        
        objectToReset.transform.SetParent(transform);
        var invoice = objectToReset.GetComponent<Invoice>();
        invoice.InvoiceData = null;
        invoice.Animator.SetTrigger(Exit);
        objectToReset.SetActive(false);
    }
}
