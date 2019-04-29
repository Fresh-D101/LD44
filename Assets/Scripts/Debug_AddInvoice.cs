using UnityEngine;
using UnityEditor;

public class Debug_AddInvoice : MonoBehaviour
{
    [SerializeField] private InvoiceReasons m_Reason = null;
    [SerializeField] private int m_Price = 0;
    [SerializeField] private int m_Duration = 0;
    [Space]
    [SerializeField] private Transform m_Parent = null;
    [SerializeField] private GameObject m_Prefab = null;

    public void CreateInvoice()
    {
        GameObject temp = Instantiate(m_Prefab, m_Parent);
        var invoice = temp.GetComponent<Invoice>(); 
        invoice.Initialize(m_Reason, m_Price, m_Duration, false);
    }
}

[CustomEditor(typeof(Debug_AddInvoice))]
public class Debug_AddInvoiceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Debug_AddInvoice addInvoice = (Debug_AddInvoice)target;

        if (GUILayout.Button("Create Invoice"))
        {
            addInvoice.CreateInvoice();
        }
    }
}
