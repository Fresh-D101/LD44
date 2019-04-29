using GameEvents;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.UIElements.Cursor;

public class Invoice : MonoBehaviour, IGameEventListener<GameEvent_SignatureDone>
{
    [SerializeField] private InvoiceData m_InvoiceData = null;
    [Space]
    [SerializeField] private TextMeshProUGUI m_Reason = null;
    [SerializeField] private TextMeshProUGUI m_Cost = null;
    [SerializeField] private TextMeshProUGUI m_Duration = null;
    [SerializeField] private Button m_SignatureButton = null;
    [SerializeField] private GameObject m_Signature = null;
    [SerializeField] private Animator m_animator;

    public Animator Animator => m_animator;

    private bool m_isNewOpenedInvoice;

    [Header("Buttons")] 
    [SerializeField] private Button m_PayButton;
    [SerializeField] private Button m_archiveButton;
    [SerializeField] private Button m_postponeButton;
    private static readonly int Signature = Animator.StringToHash("Signature");

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
        m_animator = GetComponentInChildren<Animator>();
        
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
        if (InvoiceData.Price > PlayerData.Instance.CurrentMoney)
        {
            Debug.Log("Not enough money");

            return;
        }

        InvoiceData.IsSigned = true;

        m_Signature.SetActive(true);

        m_SignatureButton.enabled = false;

        PlayerData.Instance.SubstractMoney(InvoiceData.Price);
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
            m_postponeButton.interactable = !InvoiceData.IsPostponed; 
        }

        m_PayButton.interactable = PlayerData.Instance.CurrentMoney >= InvoiceData.Price;
    }

    public void ArchiveInvoice()
    {
        PlayerData.Instance.ArchiveInvoice(InvoiceData);
        CloseInvoice();
    }

    public void PayInvoice()
    {
        if (!InvoiceData.IsPostponed)
        {
            PlayerData.Instance.UpdateExtendProgress();
        }
        
        m_animator.SetTrigger(Signature);
        PlayerData.Instance.RemoveFromArchive(InvoiceData);
    }

    public void PostponeInvoice()
    {
        InvoiceData.CurrentDuration += (InvoiceData.TotalDuration / 2);
        PlayerData.Instance.UseUpExtend();
        InvoiceData.IsPostponed = true;
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
        
        GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(false));
        
        GameManager.Instance.ResetInvoice(InvoiceData.InvoiceDesignType);
    }
    
    public void OnGameEvent(GameEvent_SignatureDone eventType)
    {
        PlayerData.Instance.SubstractMoney(InvoiceData.Price);
        Invoke(nameof(CloseInvoice), 0.8f);
    }

    private void OnEnable()
    {
        this.EventStartListening();
    }

    private void OnDisable()
    {
        this.EventStopListening();
    }
}
