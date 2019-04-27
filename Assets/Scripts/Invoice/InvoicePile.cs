using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvoicePile : MonoBehaviour
{
    
    private Image m_image;
    private Button m_button;
    private PlayerData m_data;

    [SerializeField] 
    private Sprite m_singleLetter, m_doubleLetter, m_multipleLetter;

    private void Start()
    {
        m_data = PlayerData.Instance;
        m_image = GetComponent<Image>();
        m_button = GetComponent<Button>();
    }

    public void AddNewInvoice(Invoice invoice)
    {
        m_data.AddNewInvoice(invoice);
        UpdateUI();
    }

    private void UpdateUI()
    {
        var invoiceCount = m_data.GetUnopenedInvoices().Count;

        if (invoiceCount == 0)
        {
            m_image.sprite = null;
            m_button.enabled = false;
        }
        else if (invoiceCount > 1 && invoiceCount < 5)
        {
            m_image.sprite = m_doubleLetter;
        }
        else if (invoiceCount < 5)
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
        var invoice = m_data.GetOldestUnopenedInvoice();        
    }

}
