﻿using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameEvents;
using Cursor = UnityEngine.UIElements.Cursor;

public class Catastrophe : MonoBehaviour, ICatastrophe,
    IGameEventListener<GameEvent_HourElapsed>
{
    [SerializeField] private CatastropheData m_CatastropheData = null;

    [Space] [SerializeField] private bool m_bIsUnlocked;
    [SerializeField] private TextMeshProUGUI m_AppName = null;
    [SerializeField] private TextMeshProUGUI m_Kills = null;
    [SerializeField] private TextMeshProUGUI m_Cooldown = null;
    [SerializeField] private Slider m_ActivationBar = null;
    [SerializeField] private Image m_Icon = null;
    [SerializeField] private GameObject m_GreyOut = null;
    [Space]
    [SerializeField] private Button m_Button = null;
    
    int m_progress = 0;
    bool hasCoolDown = false;

    public CatastropheData CatastropheData { get => m_CatastropheData; set => m_CatastropheData = value; }

    private void Start()
    {
        if (m_CatastropheData == null)
        {
            return;
        }
        
        Initialize();
        UpdateUI();
    }

    public void UpdateUI()
    {
         m_AppName.text = m_CatastropheData.CatastropheName;
        m_Kills.text = $"Deaths: {m_CatastropheData.MinimumKills} - {m_CatastropheData.MaximumKills}";
        m_Cooldown.text = m_CatastropheData.Cooldown.ToString();
        m_Icon.sprite = m_bIsUnlocked ? m_CatastropheData.UnlockedIcon : m_CatastropheData.LockedIcon; 
    }

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        if (m_CatastropheData == null)
        {
            return;
        }

        m_Button.onClick.RemoveAllListeners();

        if (m_bIsUnlocked)
        {
            m_Icon.sprite = m_CatastropheData.UnlockedIcon;
            m_Kills.text = $"Deaths: {m_CatastropheData.MinimumKills} - {m_CatastropheData.MaximumKills}";

            m_Button.onClick.AddListener(StartCatastrophe);
        }
        else
        {
            m_Icon.sprite = m_CatastropheData.LockedIcon;
            m_Kills.text = $"Price: {m_CatastropheData.Price}";

            m_Button.onClick.AddListener(UnlockButton);
        }
        
        m_ActivationBar.maxValue = m_CatastropheData.Duration * PlayerData.TimeScale;
        m_AppName.text = m_CatastropheData.CatastropheName;
        m_Cooldown.text = null;

        m_GreyOut.SetActive(false);
    }

    public void UnlockButton()
    {
        if (PlayerData.Instance.CurrentMoney >= m_CatastropheData.Price)
        {
            PlayerData.Instance.SubstractMoney(m_CatastropheData.Price);
            
            m_Button.onClick.RemoveListener(UnlockButton);
            m_Button.onClick.AddListener(StartCatastrophe);

            m_bIsUnlocked = true;
            UpdateUI();
        }
    }

    public void StartCatastrophe()
    {
        m_Button.enabled = false;
        m_ActivationBar.transform.gameObject.SetActive(true);

        m_progress = m_CatastropheData.Duration;
        hasCoolDown = false;
        
        this.EventStartListening<GameEvent_HourElapsed>();

        StartCoroutine(ActivationBar());
    }

    private void EndCatastrophe()
    {
        hasCoolDown = true;
        m_ActivationBar.transform.gameObject.SetActive(false);

        m_progress = m_CatastropheData.Cooldown;
        m_Cooldown.text = m_CatastropheData.Cooldown.ToString();

        m_GreyOut.SetActive(true);

        if (Random.Range(0f, 1f) <= m_CatastropheData.CritChance)
        {
            PlayerData.Instance.AddMoney(Random.Range(4 * (m_CatastropheData.MinimumKills + 1), 4 * m_CatastropheData.MaximumKills));

            Debug.Log(PlayerData.Instance.CurrentMoney.ToString());
        }
        else
        {
            PlayerData.Instance.AddMoney(Random.Range(m_CatastropheData.MinimumKills, m_CatastropheData.MaximumKills));

            Debug.Log(PlayerData.Instance.CurrentMoney.ToString());
        }
    }

    private void EndCooldown()
    {
        m_Button.enabled = true;
        hasCoolDown = false;
        m_GreyOut.SetActive(false);

        m_Cooldown.text = null;

        this.EventStopListening<GameEvent_HourElapsed>();

        m_ActivationBar.value = 0f;
    }

    private IEnumerator ActivationBar()
    {
        float counter = 0f;

        while (counter <= m_CatastropheData.Duration * PlayerData.TimeScale)
        {
            counter += Time.deltaTime;

            m_ActivationBar.value = counter;

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnGameEvent(GameEvent_HourElapsed eventType)
    {
        m_progress--;
        
        if (hasCoolDown)
        {
            m_Cooldown.text = $"{m_progress}";
        }

        if (m_progress <= 0)
        {
            if (!hasCoolDown)
            {
                EndCatastrophe();
            }
            else
            {
                EndCooldown();
            }
        }
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(m_bIsUnlocked);
    }

    public void Deserialize(BinaryReader reader)
    {
        m_bIsUnlocked = reader.ReadBoolean();
        UpdateUI();
    }
}
