using GameEvents;
using Observer;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour, IDataObserver, IGameEventListener<GameEvent_InvoiceOpen>
    {
        [SerializeField] private TextMeshProUGUI m_currentMoneyText = null;

        private int m_currentMoney = 0;

        [SerializeField] private GameObject InvoicePanel;

        private int CurrentMoney
        {
            get => m_currentMoney;
            set
            {
                m_currentMoney = value;
                UpdateMoneyDisplay();
            }
        }

        private void Start()
        {
            PlayerData.Instance.RegisterObserver(this);
            
            CurrentMoney = PlayerData.Instance.CurrentMoney;
        }

        public void UpdateMoney(int moneyAmount)
        {
            CurrentMoney = moneyAmount;
        }

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

        private void UpdateMoneyDisplay()
        {
            m_currentMoneyText.text = $"$ {CurrentMoney}";

            m_currentMoneyText.color = CurrentMoney > 0 ? Color.green : Color.red;
        }

        public void OnGameEvent(GameEvent_InvoiceOpen eventType)
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
