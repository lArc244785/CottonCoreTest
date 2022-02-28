using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("NomalMovement Parameter")]
    [SerializeField]
    private float m_moveSpeed;
    private Vector2 m_moveDir;

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
    private bool m_isGroundSensorOn;

    [Header("DirSensor")]
    [SerializeField]
    private DirSensor m_dirSensor;
   

    private enum State
    {
        E_GROUND, E_AREA
    }

    private State m_currentState;

    public void init(Rigidbody2D rig2D)
    {
        m_currentJump = 0;
        m_rig2D = rig2D;
        m_moveDir = Vector2.zero;
        m_reboundDir = Vector2.zero;

        m_groundSensor.init();
        m_dirSensor.init();

        m_isGroundSensorOn = false;
    }



    public void Move()
    {
        if (!m_dirSensor.isCheak(m_moveDir))
        {
            m_rig2D.AddForce(m_moveDir * m_moveSpeed);
        }

    }

    public void Jump()
    {
        if (m_currentJump >= m_maxJumpCount)
            return;

        m_rig2D.AddForce(Vector2.up * m_jumpPower);

        m_currentState = State.E_AREA;

        Invoke("GroundSensorOn", 0.1f); 

        m_currentJump++;
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

        if (m_currentState == State.E_AREA)
        {
            //공중인지 땅인지 체크
            if(m_isGroundSensorOn && m_groundSensor.isGround())
            {
                m_currentJump = 0;
                m_currentState = State.E_GROUND;
                m_isGroundSensorOn = false;
            }
        }

        

    }



    private void GroundSensorOn()
    {
        m_isGroundSensorOn = true;
    }


    public Vector2 moveDir
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
