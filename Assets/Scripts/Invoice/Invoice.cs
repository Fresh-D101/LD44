using GameEvents;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.UIElements.Cursor;

public class Invoice : MonoBehaviour
{
    [SerializeField] private InvoiceData m_InvoiceData = null;
    [Space]
    [SerializeField] private TextMeshProUGUI m_Reason = null;
    [SerializeField] private TextMeshProUGUI m_Cost = null;
    [SerializeField] private TextMeshProUGUI m_Duration = null;
    [SerializeField] private Button m_SignatureButton = null;
    [SerializeField] private GameObject m_Signature = null;
    private bool m_isNewOpenedInvoice;

    [Header("Buttons")] 
    [SerializeField] private Button m_PayButton;
    [SerializeField] private Button m_archiveButton;
    [SerializeField] private Button m_postponeButton;

    public InvoiceData InvoiceData { get => m_InvoiceData; set => m_InvoiceData = value; }

    public void Initialize(InvoiceReasons invoiceReasons, int price, int duration, bool newOpened)
    {
        m_InvoiceData = new InvoiceData(invoiceReasons, price, duration);
        m_isNewOpenedInvoice = newOpened;
    }

    public void Initialize(InvoiceData data, bool newOpened)
    {
        m_InvoiceData = data;
        m_isNewOpenedInvoice = newOpened;
    }
    
    private void Start()
    {
        if (InvoiceData == null)
        {
            return;
        }

        m_Reason.text = m_InvoiceData.Reason.Text;
        m_Cost.text = m_InvoiceData.Price.ToString();
        m_Duration.text = m_InvoiceData.TotalDuration.ToString();
    }

    public void SignInvoice()
    {
        if (m_InvoiceData.Price > PlayerData.Instance.CurrentMoney)
        {
            Debug.Log("Not enough money");

            return;
        }

        m_InvoiceData.IsSigned = true;

        m_Signature.SetActive(true);

        m_SignatureButton.enabled = false;

        PlayerData.Instance.SubstractMoney(m_InvoiceData.Price);
    }

    public void ShowInvoice()
    {
        UpdateUI();
        gameObject.SetActive(true);
        //TODO Set the Cursor
    }

    private void UpdateUI()
    {
        if (PlayerData.Instance.GetAvailableExtendCount() <= 0)
        {
            m_postponeButton.interactable = false;
        }
        else
        {
            m_postponeButton.interactable = !m_InvoiceData.IsPostponed; 
        }

        m_PayButton.interactable = PlayerData.Instance.CurrentMoney >= m_InvoiceData.Price;
    }

    public void ArchiveInvoice()
    {
        PlayerData.Instance.ArchiveInvoice(m_InvoiceData);
        CloseInvoice();
    }

    public void PayInvoice()
    {
        if (!m_InvoiceData.IsPostponed)
        {
            PlayerData.Instance.UpdateExtendProgress();
        }
        //Play Animation for Signature
        PlayerData.Instance.RemoveFromArchive(m_InvoiceData);
    }

    public void PostponeInvoice()
    {
        PlayerData.Instance.UseUpExtend();
        m_InvoiceData.IsPostponed = true;
    }

    private void CloseInvoice()
    {
        if (m_isNewOpenedInvoice)
        {
            if (PlayerData.Instance.GetUnopenedInvoices().Count > 0)
            {
                GameEventManager.TriggerEvent(new GameEvent_InvoiceClosed());
            }
        }

        //Set object to inactive ("pool object")
        gameObject.SetActive(false);
        //Set the data to null, to ensure it is not being used by later invoices
        InvoiceData = null;
    }
}
