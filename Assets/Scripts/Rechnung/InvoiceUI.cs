using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvoiceUI : MonoBehaviour
{
    [SerializeField] private InvoiceScriptable m_InvoiceScriptable;
    [Space]
    [SerializeField] private TextMeshProUGUI m_Reason;
    [SerializeField] private TextMeshProUGUI m_Cost;
    [SerializeField] private TextMeshProUGUI m_Duration;
    [SerializeField] private Button m_SignatureButton;
    [SerializeField] private GameObject m_Signature;
    [Space]
    [SerializeField] private bool m_IsSigned;

    public InvoiceScriptable InvoiceScriptable { get => m_InvoiceScriptable; set => m_InvoiceScriptable = value; }

    private void Start()
    {
        if (InvoiceScriptable == null)
        {
            return;
        }

        m_Reason.text = m_InvoiceScriptable.Reason;
        m_Cost.text = m_InvoiceScriptable.Price.ToString();
        m_Duration.text = m_InvoiceScriptable.Duration.ToString();
    }

    public void SignInvoice()
    {
        m_IsSigned = true;

        m_Signature.SetActive(true);

        m_SignatureButton.enabled = false;
    }
}
