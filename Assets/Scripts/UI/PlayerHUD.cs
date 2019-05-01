using GameEvents;
using Observer;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour, IDataObserver, IGameEventListener<GameEvent_ContextMenuOpen>
    {
        [SerializeField] private TextMeshProUGUI m_currentMoneyText = null;
        [SerializeField] private TextMeshProUGUI m_currentStrikesText = null;

        private int m_currentMoney = 0;
        private int m_currentStrikes = 0;

        [SerializeField] private GameObject InvoicePanel = null;

        private int CurrentMoney
        {
            get => m_currentMoney;
            set
            {
                m_currentMoney = value;
                UpdateMoneyDisplay();
            }
        }

        private int CurrentStrikes 
        {
            get => m_currentStrikes;
            set 
            {
                m_currentStrikes = value;
                UpdateStrikesDisplay();
            }
        }

        private void Start()
        {
            PlayerData.Instance.RegisterObserver(this);
            
            CurrentMoney = PlayerData.Instance.CurrentMoney;
            CurrentStrikes = PlayerData.Instance.CurrentStrikes;
        }

        public void UpdateMoney(int moneyAmount) => CurrentMoney = moneyAmount;

        public void UpdateStrikes(int strikesAmount) => CurrentStrikes = strikesAmount;

        private void OnDestroy()
        {
            PlayerData.Instance.RemoveObserver(this);
        }
        
        [ContextMenu("Add Money")]
        private void AddMoney()
        {
            PlayerData.Instance.AddMoney(Random.Range(10, 10000));
        }

        [ContextMenu("Remove Money")]
        private void RemoveMoney()
        {
            PlayerData.Instance.SubstractMoney(Random.Range(10, 10000));
        }

        [ContextMenu("Add Strike")]
        private void AddStrike()
        {
            PlayerData.Instance.AddStrike(1);
        }

        [ContextMenu("Remove Strike")]
        private void RemoveStrike()
        {
            PlayerData.Instance.SubstractStrike(1);
        }

        private void UpdateMoneyDisplay()
        {
            m_currentMoneyText.text = CurrentMoney.ToString();
        }

        private void UpdateStrikesDisplay() 
        {
            m_currentStrikesText.text = CurrentStrikes.ToString();
        }

        public void OnGameEvent(GameEvent_ContextMenuOpen eventType)
        {
            InvoicePanel.SetActive(eventType.IsOpen);
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
}
