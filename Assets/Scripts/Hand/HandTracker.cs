using UnityEngine;
using GameEvents;

public class HandTracker : MonoBehaviour,
    IGameEventListener<GameEvent_ContextMenuOpen>
{
    [SerializeField] private Transform m_Target = null;
    [SerializeField] private Transform m_HandPivot = null;
    [SerializeField] private Transform m_JacketPivot = null;
    [SerializeField] private Transform m_JacketTarget = null;
    [Space]
    [SerializeField] private float m_xAxisMultiplies = 1f;
    [SerializeField] private float m_yAxisMultiplies = 1f;

    [SerializeField] private Vector3 temp = Vector3.zero;

    private bool canMove = true;

    private void OnEnable()
    {
        this.EventStartListening<GameEvent_ContextMenuOpen>();
    }

    private void OnDisable()
    {
        this.EventStopListening<GameEvent_ContextMenuOpen>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        m_Target.transform.position = Input.mousePosition;

        m_HandPivot.localPosition = m_Target.localPosition;
        temp.x = m_Target.localPosition.x * m_xAxisMultiplies;
        temp.y = m_Target.localPosition.y * m_yAxisMultiplies;
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

    public void OnGameEvent(GameEvent_ContextMenuOpen eventType)
    {
        canMove = !eventType.IsOpen;
    }
}
