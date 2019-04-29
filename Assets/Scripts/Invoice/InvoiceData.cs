using GameEvents;
using UnityEngine;

public enum EInvoiceDesignType
{
    _Invalid = -1,
    
    White,
    Red,
    Blue,
          

    _Count
}

[System.Serializable]
public class InvoiceData
{
    [SerializeField] private InvoiceReasons m_Reason;
    [SerializeField] private int m_Price;
    [SerializeField] private int m_currentDuration;
    [SerializeField] private readonly int m_totalTotalDuration;
    [SerializeField] private bool m_IsSigned;
    [SerializeField] private bool m_IsExtended;
    [SerializeField] private EInvoiceDesignType invoiceDesignType = EInvoiceDesignType._Invalid;

    public InvoiceReasons Reason { get => m_Reason; set => m_Reason = value; }
    public int Price { get => m_Price; set => m_Price = value; }
    public int TotalDuration { get => m_totalTotalDuration; }
    public int CurrentDuration{ get => m_currentDuration; set => m_currentDuration = value; }
    public bool IsPostponed { get => m_IsExtended; set => m_IsExtended = value; }
    public bool IsSigned { get => m_IsSigned; set => m_IsSigned = value; }
    public EInvoiceDesignType InvoiceDesignType { get => invoiceDesignType; set => invoiceDesignType = value; }

    public InvoiceData(InvoiceReasons reason, int price, int totalTotalDuration)
    {
        m_Reason = reason;
        m_Price = price;
        m_totalTotalDuration = totalTotalDuration;
    }
}
