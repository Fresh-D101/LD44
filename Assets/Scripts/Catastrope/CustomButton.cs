using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CustomButton : Button
{
    [SerializeField] private Image m_screen = null;

    protected override void Start() 
    {
        m_screen = GetComponentsInChildren<Image>()[1];

        Debug.Log(m_screen.transform.name);
    }

    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) 
    {
        base.OnPointerEnter(eventData);

        m_screen.color = Color.white;
    }

    public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) 
    {
        base.OnPointerExit(eventData);

        m_screen.color = Color.black;
    }
}