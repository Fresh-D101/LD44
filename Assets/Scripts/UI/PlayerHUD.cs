using Observer;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour, IDataObserver
    {
        [SerializeField] private TextMeshProUGUI m_currentMoneyText;
        [SerializeField] private TextMeshProUGUI m_totalDebtText;

        private void Start()
        {
            PlayerData.Instance.RegisterObserver(this);
            
            m_currentMoneyText.text = "$" + PlayerData.Instance.CurrentMoney;
            m_totalDebtText.text = "$ -" + PlayerData.Instance.TotalDebt;
        }

        public void UpdateMoney(float moneyAmount)
        {
            m_currentMoneyText.text = $"$ {moneyAmount}";
        }

        public void UpdateDebt(float debtAmount)
        {
            m_totalDebtText.text = $"$ -{debtAmount}";
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
    }
}
