using System;
using System.Collections.Generic;
using Observer;
using UnityEngine;


public class PlayerData : ISubject, ISerialize
{
    [Serializable]
    struct SerializableData
    {
        public int moneyAmount;
    }
    
    public static PlayerData Instance;

    private int m_currentMoney = 0;
    private readonly List<InvoiceData> m_unopenedInvoices = new List<InvoiceData>();
    private readonly List<InvoiceData> m_archivedInvoices = new List<InvoiceData>();
    
    private static readonly List<IDataObserver> s_observers = new List<IDataObserver>();

    public PlayerData()
    {
        Instance = this;
    }

    public PlayerData(int initialMoneyAmount) : this()
    {
        m_currentMoney = initialMoneyAmount;   
    }

    
    public List<InvoiceData> GetArchivedInvoices()
    {
        return m_archivedInvoices;
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
}
