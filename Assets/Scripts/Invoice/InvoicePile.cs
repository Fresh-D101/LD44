using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvoicePile : MonoBehaviour
{
    
    private Image m_image;
    private Button m_button;
    private PlayerData m_data;
    private float m_timer;

    [SerializeField] 
    private Sprite m_emptyTray, m_singleLetter, m_doubleLetter, m_multipleLetter;

    private void Start()
    {
        m_data = PlayerData.Instance;
        m_image = GetComponent<Image>();
        m_button = GetComponent<Button>();
        
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
            m_button.enabled = false;
        }
        else if (invoiceCount > 1 && invoiceCount < 5)
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

        m_button.enabled = true;
    }

    public void OpenInvoice()
    {
        var invoiceData = m_data.GetOldestUnopenedInvoiceData();
        var prefab = GameManager.Instance.GetInvoicePrefab(out var designType);
        invoiceData.InvoiceDesignType = designType;

        var invoiceComponent = prefab.GetComponent<Invoice>();
        if (invoiceComponent == null)
        {

#if DEBUG
            Debug.LogWarning("The Invoice Template Prefabs must have an Invoice Script attached");
            invoiceComponent = prefab.AddComponent<Invoice>();
#else
            return;
#endif
        }
        
        invoiceComponent.InvoiceData = invoiceData;
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

}
