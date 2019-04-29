using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class SignatureHelper : MonoBehaviour
{
    public void OnSignatureDone()
    {
        GameEventManager.TriggerEvent(new GameEvent_SignatureDone());
    }
}
