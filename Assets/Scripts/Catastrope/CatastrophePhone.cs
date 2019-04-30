using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class CatastrophePhone : MonoBehaviour
{
    [SerializeField] private Transform m_anchor = null;
    [SerializeField] private Transform m_disabled = null;
    [SerializeField] private Transform m_enabled = null;

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
}
