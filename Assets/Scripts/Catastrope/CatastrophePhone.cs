using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameEvents;

public class CatastrophePhone : MonoBehaviour
{

    public static CatastrophePhone Instance;
    
    [SerializeField] private Transform m_anchor = null;
    [SerializeField] private Transform m_disabled = null;
    [SerializeField] private Transform m_enabled = null;

    private Catastrophe[] m_Catastrophes;

    public void OpenPhone()
    {
        m_anchor.localPosition = m_enabled.localPosition;

        GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(true));
    }

    public void ClosePhone()
    {
        m_anchor.localPosition = m_disabled.localPosition;

        GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(false));
    }

    private void Awake()
    {
        Instance = this;
        m_Catastrophes = GetComponentsInChildren<Catastrophe>();
    }

    //TODO Dieser Punkt müsste eventuell noch überarbeitet werden. Absprache zum Verhalten der einzelnen Catastrophes nötig. Eventuell ID mitspeichern?
    public void Serialize(BinaryWriter writer)
    {
        writer.Write(m_Catastrophes.Length);
        
        foreach (var catastrophe in m_Catastrophes)
        {
            writer.Write(catastrophe.CatastropheData.IsUnlocked);
        }
    }

    public bool Deserialize(BinaryReader reader)
    {
        var iCount = reader.ReadInt32();

        if (iCount != m_Catastrophes.Length) return false;

        foreach (var catastrophe in m_Catastrophes)
        {
            catastrophe.CatastropheData.IsUnlocked = reader.ReadBoolean();
            catastrophe.UpdateUI();
        }

        return true;
    }
}
