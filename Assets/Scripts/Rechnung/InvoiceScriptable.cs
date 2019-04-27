﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Invoice", menuName = "Invoice/New Invoice")]
public class InvoiceScriptable : ScriptableObject
{
    [SerializeField] private string m_Reason;
    [SerializeField] private float m_Price;
    [SerializeField] private int m_Duration;
    [SerializeField] private bool m_IsSigned;

    public string Reason { get => m_Reason; set => m_Reason = value; }
    public float Price { get => m_Price; set => m_Price = value; }
    public int Duration { get => m_Duration; set => m_Duration = value; }
    public bool IsSigned { get => m_IsSigned; set => m_IsSigned = value; }
}
