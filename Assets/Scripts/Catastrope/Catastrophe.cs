using UnityEngine;

public class Catastrophe : MonoBehaviour, ICatastrophe
{
    [SerializeField] private CatastropheData m_CatastropheData = null;

    public CatastropheData CatastropheData { get => m_CatastropheData; set => m_CatastropheData = value; }

    public void StartCatastrophe()
    {
        throw new System.NotImplementedException();
    }
}
