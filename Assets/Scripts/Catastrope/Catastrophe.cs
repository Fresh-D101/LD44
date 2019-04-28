using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Catastrophe : MonoBehaviour, ICatastrophe
{
    [SerializeField] private CatastropheData m_CatastropheData = null;
    [Space]
    [SerializeField] private TextMeshProUGUI m_AppName = null;
    [SerializeField] private TextMeshProUGUI m_Kills = null;
    [SerializeField] private TextMeshProUGUI m_Cooldown = null;
    [SerializeField] private Image m_Icon = null;

    public CatastropheData CatastropheData { get => m_CatastropheData; set => m_CatastropheData = value; }

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        if (m_CatastropheData == null)
        {
            return;
        }

        m_AppName.text = m_CatastropheData.CatastropheName;
        m_Kills.text = $"Kills: {m_CatastropheData.MinimumKills} - {m_CatastropheData.MaximumKills}";
        m_Cooldown.text = m_CatastropheData.Cooldown.ToString();
    }

    public void StartCatastrophe()
    {
        Debug.Log("SKIGI");
        Invoke("Yeet", 4f);
    }

    private void Yeet()
    {
        Debug.Log("YOOOT");
    }
}
