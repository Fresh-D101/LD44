﻿using UnityEngine;

[System.Serializable]
public class InvoiceData
{
    [SerializeField] private InvoiceReasons m_Reason;
    [SerializeField] private int m_Price;
    [SerializeField] private int m_Duration;
    [SerializeField] private bool m_IsSigned;
    [SerializeField] private bool m_IsExtended;

    public InvoiceReasons Reason { get => m_Reason; set => m_Reason = value; }
    public int Price { get => m_Price; set => m_Price = value; }
    public int Duration { get => m_Duration; set => m_Duration = value; }
    public bool IsExtended { get => m_IsExtended; set => m_IsExtended = value; }
    public bool IsSigned { get => m_IsSigned; set => m_IsSigned = value; }

    public InvoiceData(InvoiceReasons reason, int price, int duration)
    {
        m_Reason = reason;
        m_Price = price;
        m_Duration = duration;
    }
}
