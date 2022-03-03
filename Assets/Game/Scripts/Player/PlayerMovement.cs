using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("NomalMovement Parameter")]
    [SerializeField]
    private float m_moveSpeed;
    private float m_moveDir;
    private float m_lastDir;


    [SerializeField]
    private float m_jumpPower;
    [SerializeField]
    private int m_maxJumpCount;
    private int m_currentJump;

    [SerializeField]
    private float m_clampX;

    private bool m_isMoveAble;

    [Header("RopeMovement Parameter")]
    [SerializeField]
    private float m_reboundPower;

    private Vector2 m_reboundDir;
    private Rigidbody2D m_rig2D;

    [Header("GroundSensor")]
    [SerializeField]
    private GroundSensor m_groundSensor;

    [Header("DirSensor")]
    [SerializeField]
    private DirSensor m_dirSensor;

    [Header("ReboundEventOption")]
    [SerializeField]
    private float m_reboundX;
    [SerializeField]
    private float m_reboundEventTime;

    public void init(Rigidbody2D rig2D)
    {
        m_currentJump = 0;
        m_rig2D = rig2D;
        m_moveDir = .0f;
        m_reboundDir = Vector2.zero;

        m_groundSensor.init();
        m_dirSensor.init();
        m_isMoveAble = true;
    }



    public void Move()
    {

        if (!m_dirSensor.isWall(m_moveDir))
        {
            m_rig2D.velocity = new Vector2(m_moveDir * m_moveSpeed, m_rig2D.velocity.y);
        }

    }

    public void Jump()
    {
        if (m_currentJump >= m_maxJumpCount)
            return;

        m_rig2D.AddForce(Vector2.up * m_jumpPower);

        m_currentJump++;
    }

    public void JumpReset()
    {
        m_currentJump = 0;
    }

    public void Rebound(bool isRight)
    {
        m_reboundDir = Vector2.right;
        if (!isRight)
            m_reboundDir *= -1.0f;

        m_rig2D.velocity = Vector2.zero;

        m_rig2D.AddForce(m_reboundDir * m_reboundPower);


    }


    private void MoveClamp()
    {
        float clampX = m_rig2D.velocity.x;
        clampX = Mathf.Clamp(clampX, -m_clampX, m_clampX);
        m_rig2D.velocity = new Vector2(clampX, m_rig2D.velocity.y);
    }

    public void UpdateProcess()
    {
        if(m_isMoveAble)
        {
            Move();
            MoveClamp();
        }

    }


    public void ReboundEvent()
    {
        m_isMoveAble = false;

        float xVelocity = m_reboundX;
        if (m_rig2D.velocity.x < 0.0f)
            xVelocity *= -1.0f;

        m_rig2D.velocity = new Vector2(xVelocity, m_rig2D.velocity.y);
        StartCoroutine(ReboundEventCoroutine());
    }

    private IEnumerator ReboundEventCoroutine()
    {
        float fTime = .0f;

        while(!m_dirSensor.isWall(m_lastDir) && fTime <= m_reboundEventTime)
        {
            fTime += Time.deltaTime;
            yield return null;
        }
        m_isMoveAble = true;
    }


    public float moveDirX
    {
        set
        {
            m_moveDir = value;
            if (m_moveDir > 0.0f || m_moveDir < 0.0f)
                m_lastDir = m_moveDir;
        }
        get
        {
            return m_moveDir;
        }
    }

    public bool isMoveAble
    {
        get
        {
            return m_isMoveAble;
        }
    }


}
