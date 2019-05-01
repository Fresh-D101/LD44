using System;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class InvoiceBook : MonoBehaviour,
    IGameEventListener<GameEvent_InvoiceArchived>,
    IGameEventListener<GameEvent_UsedExtend>,
    IGameEventListener<GameEvent_GainedExtend>
{
    [Header("Transforms")]
    [SerializeField] private Transform m_book = null;
    [SerializeField] private Transform m_bookOpened = null; 
    [SerializeField] private Transform m_bookClosed = null; 

    [Header("Pages and Invoices")]
    [SerializeField] private GameObject m_ArchivedInvoicePrefab = null;
    [SerializeField] private Transform m_LeftPage = null;
    [SerializeField] private Transform m_RightPage = null;

    private List<ArchivedInvoice> m_InvoiceDataList = new List<ArchivedInvoice>();
    private ArchivedInvoice[] m_archivedInvoices = new ArchivedInvoice[16];

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        GameObject temp = null;

        for (int left = 0; left < 8; left++)
        {
            temp = Instantiate(m_ArchivedInvoicePrefab, m_LeftPage);
            temp.transform.name = $"Invoice Info Box - left {left}";

            m_archivedInvoices[left] = temp.GetComponent<ArchivedInvoice>();
        }

        for (int right = 0; right < 8; right++)
        {
            temp = Instantiate(m_ArchivedInvoicePrefab, m_RightPage);
            temp.transform.name = $"Invoice Info Box - right {right}";

            m_archivedInvoices[right] = temp.GetComponent<ArchivedInvoice>();
        }
    }

    public void OnGameEvent(GameEvent_InvoiceArchived eventType)
    {
        var archivedInvoice = new ArchivedInvoice();
        archivedInvoice.Initialize(eventType.InvoiceData);

        m_InvoiceDataList.Add(archivedInvoice);
    }

    public void OpenInvoice()
    {

    }

    public void OnGameEvent(GameEvent_UsedExtend eventType)
    {
        UpdateUI();
    }

    public void OnGameEvent(GameEvent_GainedExtend eventType)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var invoice in m_archivedInvoices)
        {
            invoice.UpdateUI();
        }
    }

    public void ToggleBook(bool input) 
    {
        if (input)
        {
            m_book.localPosition = m_bookOpened.localPosition;
            GameEvents.GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(true));
        } 
        else
        {
            m_book.localPosition = m_bookClosed.localPosition;
            GameEvents.GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(false));
        }
    }

    private void OnEnable()
    {
        this.EventStartListening<GameEvent_InvoiceArchived>();
        this.EventStartListening<GameEvent_UsedExtend>();
        this.EventStartListening<GameEvent_GainedExtend>();
    }

    private void OnDisable()
    {
        this.EventStopListening<GameEvent_InvoiceArchived>();
        this.EventStopListening<GameEvent_UsedExtend>();
        this.EventStopListening<GameEvent_GainedExtend>();
    }
}
