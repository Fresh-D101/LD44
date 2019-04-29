using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatastrophePhone : MonoBehaviour
{
    public void OpenPhone()
    {
        this.transform.GetChild(0).transform.gameObject.SetActive(true);
    }

    public void ClosePhone()
    {
        this.transform.GetChild(0).transform.gameObject.SetActive(false);
    }
}
