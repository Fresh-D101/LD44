using UnityEngine;

[CreateAssetMenu(fileName = "New Catastrophe", menuName = "Catastrophe/New Catastrophe")]
public class CatastropheData : ScriptableObject
{
    [SerializeField] private Sprite m_lockedIcon = null;
    [SerializeField] private Sprite m_unlockedIcon = null;
    [SerializeField] private string m_CatastropheName = null;
    [SerializeField] private int m_Price = 0;
    [SerializeField] private int m_Duration = 0;
    [SerializeField] private int m_Cooldown = 0;
    [SerializeField] private int m_MinimumKills = 0;
    [SerializeField] private int m_MaximumKills = 1;
    [Range(0, 1)]
    [SerializeField] private float m_CritChance = 0f;

    public string CatastropheName { get => m_CatastropheName; set => m_CatastropheName = value; }
    public int Price { get => m_Price; set => m_Price = value; }
    public int Duration { get => m_Duration; set => m_Duration = value; }
    public int Cooldown { get => m_Cooldown; set => m_Cooldown = value; }
    public int MinimumKills { get => m_MinimumKills; set => m_MinimumKills = value; }
    public int MaximumKills { get => m_MaximumKills; set => m_MaximumKills = value; }
    public float CritChance { get => m_CritChance; set => m_CritChance = value; }
    public Sprite LockedIcon { get => m_lockedIcon; set => m_lockedIcon = value; }
    public Sprite UnlockedIcon { get => m_unlockedIcon; set => m_unlockedIcon = value; }
    
}
