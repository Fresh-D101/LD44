using UnityEngine;
using UnityEngine.UI;
using GameEvents;
using TMPro;

public class ArchivedInvoice : MonoBehaviour,
    IGameEventListener<GameEvent_DayElapsed>
{
    [SerializeField] private InvoiceData m_InvoiceData = null;
    [SerializeField] private Button m_ExtendButton = null;
    [SerializeField] private TextMeshProUGUI m_DurationText = null;
    [SerializeField] private TextMeshProUGUI m_PriceText = null;
    [Space]
    [SerializeField] private int m_Duration = 0;
    [SerializeField] private int m_Price = 0;
    
    public void Initialize(InvoiceData inputInvoiceData)
    {
        m_InvoiceData = inputInvoiceData;

        m_Duration = m_InvoiceData.CurrentDuration;
        m_Price = m_InvoiceData.Price;

        UpdateUI();
    }

    private void OnEnable()
    {
        this.EventStartListening<GameEvent_DayElapsed>();
    }

    private void OnDisable()
    {
        this.EventStopListening<GameEvent_DayElapsed>();
    }

    public void ViewInvoice()
    {
        var invoice = GameManager.InvoiceFactory.CreateInvoiceFromArchive(m_InvoiceData);
        
        invoice.ShowInvoice();
    }

    public void ExtendInvoice()
    {
        
    }

    [ContextMenu("Update UI")]
    public void UpdateUI()
    {
        if (PlayerData.Instance.GetAvailableExtendCount() <= 0)
        {
            m_ExtendButton.enabled = false;
        }
        else
        {
            m_ExtendButton.enabled = !m_InvoiceData.IsExtended;
        }

        m_DurationText.text = $"Due in: {m_Duration} days";
        m_PriceText.text = $"Price: {m_Price}";
    }

    public void OnGameEvent(GameEvent_DayElapsed eventType)
    {
        m_Duration--;

        if (m_Duration <= 0)
        {
            Debug.LogWarning("Duration of Invoice has reached 0!");
        }

        m_InvoiceData.CurrentDuration = m_Duration;
    }
}
