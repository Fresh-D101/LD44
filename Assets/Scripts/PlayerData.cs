using System;
using System.Collections.Generic;
using Observer;
using UnityEngine;


public class PlayerData : ISubject, ISerialize
{
    [Serializable]
    struct SerializableData
    {
        public float moneyAmount;
    }
    
    public static PlayerData Instance;

    private float m_currentMoney = 0f;
    private List<Invoice> m_unopenedInvoices;
    private List<Invoice> m_archivedInvoices;
    
    private static readonly List<IDataObserver> s_observers = new List<IDataObserver>();

    public PlayerData()
    {
        Instance = this;
    }

    public PlayerData(float initialMoneyAmount) : this()
    {
        m_currentMoney = initialMoneyAmount;   
    }

    
    public List<Invoice> GetArchivedInvoices()
    {
        return m_archivedInvoices;
    }

    public List<Invoice> GetUnopenedInvoices()
    {
        return m_unopenedInvoices;
    }
    
    public void AddNewInvoice(Invoice invoice)
    {
        m_unopenedInvoices.Add(invoice);
    }

    public Invoice GetOldestUnopenedInvoice()
    {
        var invoice = m_unopenedInvoices[0];
        m_unopenedInvoices.RemoveAt(0);
        return invoice;
    }
   
    public float CurrentMoney
    {
        get => m_currentMoney;
        private set
        {
            m_currentMoney = value;
            NotifyMoneyUpdate();
        }
    }

    public void AddMoney(float amount)
    {
        CurrentMoney += amount;
    }

    public void SubstractMoney(float amount)
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
