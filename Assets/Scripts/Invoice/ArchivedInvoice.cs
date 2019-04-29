using UnityEngine;
using UnityEngine.UI;
using GameEvents;

public class ArchivedInvoice : MonoBehaviour,
    IGameEventListener<GameEvent_DayElapsed>
{
    [SerializeField] private InvoiceData m_InvoiceData = null;
    [SerializeField] private Button m_ExtendButton = null;
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
        //Get the Invoice from the Invoice factory
    }

    public void ExtendInvoice()
    {
        //Implement Extend Logic
    }

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
    }

    public void OnGameEvent(GameEvent_DayElapsed eventType)
    {
        throw new System.NotImplementedException();
    }
}
