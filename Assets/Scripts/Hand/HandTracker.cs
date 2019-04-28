using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    [SerializeField] private Transform m_Target = null;
    [SerializeField] private Transform m_HandPivot = null;
    [SerializeField] private Transform m_JacketPivot = null;
    [SerializeField] private Transform m_JacketTarget = null;

    [SerializeField] private Vector3 temp = Vector3.zero;

    private void Update()
    {
        m_Target.transform.position = Input.mousePosition;

        m_HandPivot.localPosition = m_Target.localPosition;
        temp.x = m_Target.localPosition.x * 0.6f;
        temp.y = m_Target.localPosition.y * 0.8f;
        temp.z = 0;
        m_JacketTarget.localPosition = temp;
        m_JacketPivot.position = m_JacketTarget.position;

        FaceTarget(m_HandPivot, m_JacketPivot, true);
        FaceTarget(m_JacketPivot, m_Target, false);
    }

    private void FaceTarget(Transform objectToRotate, Transform target, bool invertRotation)
    {
        Vector2 direction = new Vector2(target.position.x - objectToRotate.position.x, target.position.y - objectToRotate.position.y);

        objectToRotate.up = invertRotation ? -direction : direction;
    }
}
