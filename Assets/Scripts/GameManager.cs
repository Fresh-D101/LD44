using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerData m_playerData;
    
    private void Awake()
    {
        m_playerData = new PlayerData(2000f);
    }
}
