using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirSensor : MonoBehaviour
{
    [SerializeField]
    private Transform m_startTr;
    [SerializeField]
    private Transform m_endTr;
    private float m_distance;

    [SerializeField]
    private LayerMask m_layerMask;

    private Vector2 m_localPos;
    private Vector2 m_dir;
    


    public void init()
    {
        m_distance = Vector2.Distance(m_startTr.position, m_endTr.position);
        m_localPos = m_endTr.localPosition;
    }


    public bool isCheak(Vector2 dir)
    {
        Debug.Log(dir);
        if(dir != Vector2.zero)
            m_dir = dir;

        return Physics2D.Raycast(m_startTr.position, dir, m_distance, m_layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 endPos = m_startTr.position;

        if (m_dir == Vector2.right)
            endPos += (Vector3)m_localPos;
        else
            endPos -= (Vector3)m_localPos;

        Gizmos.DrawLine(m_startTr.position, endPos);
    }
}
