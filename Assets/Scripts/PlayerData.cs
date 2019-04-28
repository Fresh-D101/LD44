using System;
using System.Collections.Generic;
using GameEvents;
using Observer;
using UnityEngine;


public class PlayerData : ISubject, ISerialize, IGameEventListener<GameEvent_DayElapsed>
{
    [Serializable]
    struct SerializableData
    {
        public int moneyAmount;
    }
    
    public static PlayerData Instance;

    private static readonly float m_timeScale = 2f;
    private int m_currentMoney = 0;
    private int m_availableExtends;
    private int m_currentExtendProgress = 0;
    private readonly List<InvoiceData> m_unopenedInvoices = new List<InvoiceData>();
    private readonly List<InvoiceData> m_archivedInvoices = new List<InvoiceData>();
    
    //Balancing Values//
    private int maxExtends = 5;
    private int neededExtendProgress = 4;
    
    private static readonly List<IDataObserver> s_observers = new List<IDataObserver>();

    public PlayerData()
    {
        this.EventStartListening();
        Instance = this;
    }

    ~PlayerData()
    {
        this.EventStopListening();
    }

    public PlayerData(int initialMoneyAmount, int maxExtends, int neededExtendProgress) : this()
    {
        m_currentMoney = initialMoneyAmount;
        this.maxExtends = maxExtends;
        this.neededExtendProgress = neededExtendProgress;
    }

    public void UseUpExtend()
    {
        m_availableExtends--;
        
        GameEventManager.TriggerEvent(new GameEvent_UsedExtend());
    }

    public int GetAvailableExtendCount()
    {
        return m_availableExtends;
    }

    public void UpdateExtendProgress()
    {
        if (m_availableExtends == maxExtends)
        {
            return;
        }
        m_currentExtendProgress++;

        if (m_currentExtendProgress >= neededExtendProgress)
        {
            m_currentExtendProgress = 0;
            GameEventManager.TriggerEvent(new GameEvent_GainedExtend());
            m_availableExtends++;
        }
        
        GameEventManager.TriggerEvent(new GameEvent_UpdatedExtendProgress());
    }
    
    public List<InvoiceData> GetArchivedInvoices()
    {
        return m_archivedInvoices;
    }

    public void ArchiveInvoice(InvoiceData invoice)
    {
        if (m_archivedInvoices.Contains(invoice)) return;
        
        m_archivedInvoices.Add(invoice);
    }

    public bool RemoveFromArchive(InvoiceData invoice)
    {        
        return m_archivedInvoices.Remove(invoice);
    }

    public List<InvoiceData> GetUnopenedInvoices()
    {
        return m_unopenedInvoices;
    }
    
    public void AddNewInvoice(InvoiceData invoice)
    {
        m_unopenedInvoices.Add(invoice);
    }

    public InvoiceData GetOldestUnopenedInvoiceData()
    {
        var invoice = m_unopenedInvoices[0];
        m_unopenedInvoices.RemoveAt(0);
        return invoice;
    }
   
    public int CurrentMoney
    {
        get => m_currentMoney;
        private set
        {
            m_currentMoney = value;
            NotifyMoneyUpdate();
        }
    }

    public static float TimeScale => m_timeScale;

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
    }

    public void SubstractMoney(int amount)
    {
        CurrentMoney -= amount;
    }

    public void RegisterObserver(IDataObserver o)
    {
        s_observers.Add(o);
    }

    public void RemoveObserver(IDataObserver o)
    {
        s_observers.Remove(o);
    }

    public void NotifyMoneyUpdate()
    {
        foreach (var observer in s_observers)
        {
            observer.UpdateMoney(CurrentMoney);
        }
    }

    public string Serialize()
    {
        var data = new SerializableData {moneyAmount = CurrentMoney};

        var jsonString = JsonUtility.ToJson(data);
        return jsonString;
    }

    public void Deserialize(string json)
    {
        var data = JsonUtility.FromJson<SerializableData>(json);
        CurrentMoney = data.moneyAmount;
    }

    public void OnGameEvent(GameEvent_DayElapsed eventType)
    {
        for (int i = m_archivedInvoices.Count - 1; i >= 0; i--)
        {
            var duration = m_archivedInvoices[i].CurrentDuration;
            duration--;

            if (duration <= 0)
            {
                //Strike Player
                m_archivedInvoices.RemoveAt(i);
            }

            m_archivedInvoices[i].CurrentDuration = duration;
        }
        
        for (int i = m_unopenedInvoices.Count - 1; i >= 0; i--)
        {
            var duration = m_unopenedInvoices[i].CurrentDuration;
            duration--;

            if (duration <= 0)
            {
                //Strike Player
                m_unopenedInvoices.RemoveAt(i);
            }

            m_unopenedInvoices[i].CurrentDuration = duration;
        }
    }
}
