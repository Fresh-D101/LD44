using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class CatastrophePhone : MonoBehaviour
{
    public void OpenPhone()
    {
        this.transform.GetChild(0).transform.gameObject.SetActive(true);

        GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(true));
    }

    public void ClosePhone()
    {
        this.transform.GetChild(0).transform.gameObject.SetActive(false);

        GameEventManager.TriggerEvent(new GameEvent_ContextMenuOpen(false));
    }
}
