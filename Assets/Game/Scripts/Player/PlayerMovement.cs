using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("NomalMovement Parameter")]
    [SerializeField]
    private float m_moveSpeed;
    private float m_moveDir;

    [SerializeField]
    private float m_jumpPower;
    [SerializeField]
    private int m_maxJumpCount;
    private int m_currentJump;

    [SerializeField]
    private float m_clampX;

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
   

    public void init(Rigidbody2D rig2D)
    {
        m_currentJump = 0;
        m_rig2D = rig2D;
        m_moveDir = .0f;
        m_reboundDir = Vector2.zero;

        m_groundSensor.init();
        m_dirSensor.init();

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

        Move();
        MoveClamp();

    }






    public float moveDirX
    {
        set
        {
            m_moveDir = value;
        }
        get
        {
            return m_moveDir;
        }
    }



}
