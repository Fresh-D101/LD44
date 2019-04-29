using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InvoicePile : MonoBehaviour, IGameEventListener<GameEvent_InvoiceClosed>
{
    
    private Image m_image;
    private Button m_button;
    private PlayerData m_data;
    private float m_timer;
    [SerializeField]
    private Envelope m_envelope;
    [SerializeField]
    private GameObject m_openNextDialogue;
    

    [SerializeField] 
    private Sprite m_emptyTray, m_singleLetter, m_doubleLetter, m_multipleLetter;

    private void Start()
    {
        // Hier könnte man eventuell noch schauen, ob es sinvoller wäre, nur zu subscriben wenn durch den Pile eine Invoice geöffnet wurde
        // so spart man sich die überprüfung durch den Bool in der Invoice.cs
        this.EventStartListening();
        
        m_data = PlayerData.Instance;
        m_image = GetComponent<Image>();
        m_button = GetComponent<Button>();
        
        UpdateUI();
        
        Invoke(nameof(GenerateNewInvoice), 2f);
    }

    public void AddNewInvoice(InvoiceData invoice)
    {
        m_data.AddNewInvoice(invoice);
        UpdateUI();
    }

    private void UpdateUI()
    {
        var invoiceCount = m_data.GetUnopenedInvoices().Count;

        if (invoiceCount == 0)
        {
            m_image.sprite = m_emptyTray;
            m_button.interactable = false;
            return;
        }
        
        if (invoiceCount > 1 && invoiceCount < 5)
        {
            m_image.sprite = m_doubleLetter;
        }
        else if (invoiceCount >= 5)
        {
            m_image.sprite = m_multipleLetter;
        }
        else
        {
            m_image.sprite = m_singleLetter;
        }

        m_button.interactable = true;
    }

    public void CloseDialogue()
    {
        m_openNextDialogue.SetActive(false);
    }

    public void OpenInvoice()
    {
        // Make sure to close the dialogue
        m_openNextDialogue.SetActive(false);
        
        /*var invoiceData = m_data.GetOldestUnopenedInvoiceData();
        var prefab = GameManager.Instance.GetInvoicePrefab(out var designType);
        var invoiceObject = Instantiate(prefab, transform.parent.position, Quaternion.identity, transform.parent);
        invoiceObject.SetActive(false);
        
        invoiceData.InvoiceDesignType = designType;

        var invoiceComponent = invoiceObject.GetComponent<Invoice>();
        if (invoiceComponent == null)
        {

#if DEBUG
            Debug.LogWarning("The Invoice Template Prefabs must have an Invoice Script attached");
            invoiceComponent = invoiceObject.AddComponent<Invoice>();
#else
            return;
#endif
        }*/

        var invoiceData = m_data.GetOldestUnopenedInvoiceData();
        var invoiceComponent = GameManager.InvoiceFactory.CreateNewInvoice(invoiceData);
        
        invoiceComponent.Initialize(invoiceData, true);
      
        m_envelope.OpenEnvelope(invoiceComponent);
        
        UpdateUI();
    }

    private void GenerateNewInvoice()
    {
        if (m_data.GetUnopenedInvoices().Count >= 10)
        {
            return;
        }
        
        var reason = ScriptableObject.CreateInstance<InvoiceReasons>();
        AddNewInvoice(new InvoiceData(reason, 20, 5));

        var nextInvoke = Random.Range(2, 10);
        Invoke(nameof(GenerateNewInvoice), nextInvoke);
    }

    public void OnGameEvent(GameEvent_InvoiceClosed eventType)
    {
        m_openNextDialogue.SetActive(true);
    }

    private void OnDisable()
    {
        this.EventStopListening();
    }
}
