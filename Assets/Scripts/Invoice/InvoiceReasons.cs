using UnityEngine;

[CreateAssetMenu(fileName = "New Reason", menuName = "Invoice/New Reason")]
public class InvoiceReasons : ScriptableObject
{
    [TextArea(0, 50)]
    [SerializeField] private string m_Text = null;

    public string Text { get => m_Text; set => m_Text = value; }
}
