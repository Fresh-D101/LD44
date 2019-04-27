using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Invoice : MonoBehaviour
{
    [SerializeField] private InvoiceData m_InvoiceData;
    [Space]
    [SerializeField] private TextMeshProUGUI m_Reason = null;
    [SerializeField] private TextMeshProUGUI m_Cost = null;
    [SerializeField] private TextMeshProUGUI m_Duration = null;
    [SerializeField] private Button m_SignatureButton = null;
    [SerializeField] private GameObject m_Signature = null;

    public InvoiceData InvoiceData { get => m_InvoiceData; set => m_InvoiceData = value; }

    public Invoice(InvoiceReasons invoiceReasons, float price, int duration)
    {
        m_InvoiceData = new InvoiceData(invoiceReasons, price, duration);
    }

    private void Start()
    {
        if (InvoiceData == null)
        {
            return;
        }

        m_Reason.text = m_InvoiceData.Reason.Text;
        m_Cost.text = m_InvoiceData.Price.ToString();
        m_Duration.text = m_InvoiceData.Duration.ToString();
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
}
