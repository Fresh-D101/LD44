using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvoiceUI : MonoBehaviour
{
    [SerializeField] private InvoiceScriptable m_InvoiceScriptable;
    [Space]
    [SerializeField] private TextMeshProUGUI m_Reason = null;
    [SerializeField] private TextMeshProUGUI m_Cost = null;
    [SerializeField] private TextMeshProUGUI m_Duration = null;
    [SerializeField] private Button m_SignatureButton = null;
    [SerializeField] private GameObject m_Signature = null;

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
        if (m_InvoiceScriptable.Price > PlayerData.Instance.CurrentMoney)
        {
            Debug.Log("Not enough money");

            return;
        }

        m_InvoiceScriptable.IsSigned = true;

        m_Signature.SetActive(true);

        m_SignatureButton.enabled = false;

        PlayerData.Instance.SubstractMoney(m_InvoiceScriptable.Price);
    }
}
