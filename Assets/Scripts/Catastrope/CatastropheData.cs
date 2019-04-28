using UnityEngine;

[CreateAssetMenu(fileName = "New Catastrophe", menuName = "Catastrophe/New Catastrophe")]
public class CatastropheData : ScriptableObject
{
    [SerializeField] private string m_CatastropheName = null;
    [SerializeField] private bool m_IsUnlocked = false;
    [SerializeField] private int m_Price = 0;
    [SerializeField] private float m_Duration = 0f;
    [SerializeField] private float m_Cooldown = 0f;
    [SerializeField] private int m_MinimumKills = 0;
    [SerializeField] private int m_MaximumKills = 1;
    [Range(0, 1)]
    [SerializeField] private float m_CritChance = 0f;

    public string CatastropheName { get => m_CatastropheName; set => m_CatastropheName = value; }
    public bool IsUnlocked { get => m_IsUnlocked; set => m_IsUnlocked = value; }
    public int Price { get => m_Price; set => m_Price = value; }
    public float Duration { get => m_Duration; set => m_Duration = value; }
    public float Cooldown { get => m_Cooldown; set => m_Cooldown = value; }
    public int MinimumKills { get => m_MinimumKills; set => m_MinimumKills = value; }
    public int MaximumKills { get => m_MaximumKills; set => m_MaximumKills = value; }
    public float CritChance { get => m_CritChance; set => m_CritChance = value; }
}
