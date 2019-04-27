using Observer;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour, IDataObserver
    {
        [SerializeField] private TextMeshProUGUI m_currentMoneyText = null;

        private float m_currentMoney = 0f;

        private float CurrentMoney
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

        public void UpdateMoney(float moneyAmount)
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
    }
}
