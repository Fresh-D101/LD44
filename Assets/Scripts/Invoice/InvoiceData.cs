using System.IO;
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
    [SerializeField] private readonly int m_totalDuration;
    [SerializeField] private bool m_IsSigned;
    [SerializeField] private bool m_IsExtended;
    [SerializeField] private EInvoiceDesignType m_eInvoiceDesignType = EInvoiceDesignType._Invalid;

    public InvoiceReasons Reason { get => m_Reason; set => m_Reason = value; }
    public int Price { get => m_Price; set => m_Price = value; }
    public int TotalDuration { get => m_totalDuration; }
    public int CurrentDuration{ get => m_currentDuration; set => m_currentDuration = value; }
    public bool IsPostponed { get => m_IsExtended; set => m_IsExtended = value; }
    public bool IsSigned { get => m_IsSigned; set => m_IsSigned = value; }
    public EInvoiceDesignType InvoiceDesignType { get => m_eInvoiceDesignType; set => m_eInvoiceDesignType = value; }

    public InvoiceData(InvoiceReasons reason, int price, int totalDuration)
    {
        m_Reason = reason;
        m_Price = price;
        m_totalDuration = totalDuration;
    }

    public InvoiceData(BinaryReader reader)
    {
        m_Reason = ScriptableObject.CreateInstance<InvoiceReasons>();
        m_Reason.Text = reader.ReadString();
        m_Price = reader.ReadInt32();
        m_totalDuration = reader.ReadInt32();
        m_currentDuration = reader.ReadInt32();
        m_IsSigned = reader.ReadBoolean();
        m_IsExtended = reader.ReadBoolean();
        m_eInvoiceDesignType = (EInvoiceDesignType) reader.ReadInt32();
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(Reason.Text);
        writer.Write(Price);
        writer.Write(TotalDuration);
        writer.Write(CurrentDuration);
        writer.Write(IsSigned);
        writer.Write(m_IsExtended);
        writer.Write((int)InvoiceDesignType);
    }

}
