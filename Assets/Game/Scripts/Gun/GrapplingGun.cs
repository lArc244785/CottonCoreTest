using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("General Reference")]
    [SerializeField]
    private PlayerMovementManager m_playerMovementManager;
    [SerializeField]
    private Hook m_hook;
    [SerializeField]
    private float m_hookSpeed;
    [SerializeField]
    private RopeRenderer m_ropeRenderer;
    [SerializeField]
    private Transform m_firePoint;
    [SerializeField]
    private SpringJoint2D m_springJoint2D;
    [SerializeField]
    private Rigidbody2D m_springRig2D;


    [Header("Option")]
    [SerializeField]
    private bool m_bBodyFix;
    [SerializeField]
    private LayerMask m_pullLayerMask;
    [SerializeField]
    private float m_pullDistance;
    [SerializeField]
    private float m_pullMaxClampLength;
    [SerializeField]
    private float m_pullFrequency;
    [SerializeField]
    private float m_cancelMaxClampLength;




    public enum E_State
    {
        E_NONE, E_HOOKFIRE, E_GRAPPLING, E_PULL
    }

    public E_State m_eState;


    public void init()
    {
        m_hook.init(m_hookSpeed, this);
    }

    public void Fire()
    {
        m_eState = E_State.E_HOOKFIRE;

        m_hook.transform.parent = null;

        Vector2 dir = InputManager.Instance.inGameMousePosition2D - (Vector2)m_hook.transform.position;
        dir.Normalize();

        m_playerMovementManager.shoulderMovement.setLookPosition(InputManager.Instance.inGameMousePosition2D);

        m_hook.Fire(dir);
        m_ropeRenderer.isDraw = true;
    }

    public void Grappling()
    {
        float distance = getHookDistance();

        m_springJoint2D.distance = distance;
        m_springJoint2D.enabled = true;

        m_playerMovementManager.setTypeGrappling((Vector2)m_hook.transform.position, m_bBodyFix);



        m_eState = GrapplingGun.E_State.E_GRAPPLING;
    }

    public void Cancel()
    {
        HookReset();
        JointDisable();



        m_ropeRenderer.isDraw = false;

        m_eState = GrapplingGun.E_State.E_NONE;
    }


    private void HookReset()
    {
        m_hook.transform.parent = m_firePoint;
        m_hook.transform.localPosition = Vector3.zero;
        m_hook.transform.localRotation = Quaternion.identity;
    }

    private void JointDisable()
    {
        m_springJoint2D.enabled = false;
        m_springJoint2D.frequency = .0f;
        m_springRig2D.velocity = Vector2.ClampMagnitude(m_springRig2D.velocity, m_cancelMaxClampLength);
    }




    public void Pull()
    {
        StartCoroutine(PullProcesses());
    }

    private IEnumerator PullProcesses()
    {
        m_springJoint2D.distance = m_pullDistance;
        m_springJoint2D.frequency = m_pullFrequency;

        Vector2 dir = (Vector2)m_hook.transform.position - (Vector2)m_firePoint.position;
        dir.Normalize();

        while (m_springJoint2D.enabled && !Physics2D.Raycast(m_firePoint.position, dir, m_springJoint2D.distance, m_pullLayerMask))
        {
            m_springRig2D.velocity = Vector2.ClampMagnitude(m_springRig2D.velocity, m_pullMaxClampLength);
            yield return null;
        }

        if (m_springJoint2D.enabled)
        {
            m_springRig2D.velocity = Vector2.zero;
            m_springJoint2D.frequency = 0.0f;
        }

    }

    private float getHookDistance()
    {
        return Vector2.Distance(m_hook.transform.position, m_springRig2D.transform.position);
    }



}