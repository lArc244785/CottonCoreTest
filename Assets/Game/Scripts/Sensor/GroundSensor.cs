using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField]
    private Transform m_startTr;
    [SerializeField]
    private Transform m_endTr;
    private float m_distance;

    [SerializeField]
    private LayerMask m_groundLayerMask;

    public void init()
    {
        m_distance = Vector2.Distance(m_startTr.position, m_endTr.position);
    }


    public bool isGround()
    {
        return Physics2D.Raycast(m_startTr.position, Vector2.down, m_distance, m_groundLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_startTr.position, m_endTr.position);
    }

}
