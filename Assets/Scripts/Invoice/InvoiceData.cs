using UnityEngine;

[System.Serializable]
public class InvoiceData
{
    [SerializeField] private InvoiceReasons m_Reason;
    [SerializeField] private float m_Price;
    [SerializeField] private int m_Duration;
    [SerializeField] private bool m_IsSigned;

    public InvoiceReasons Reason { get => m_Reason; set => m_Reason = value; }
    public float Price { get => m_Price; set => m_Price = value; }
    public int Duration { get => m_Duration; set => m_Duration = value; }
    public bool IsSigned { get => m_IsSigned; set => m_IsSigned = value; }

    public InvoiceData(InvoiceReasons reason, float price, int duration)
    {
        m_Reason = reason;
        m_Price = price;
        m_Duration = duration;
    }
}
