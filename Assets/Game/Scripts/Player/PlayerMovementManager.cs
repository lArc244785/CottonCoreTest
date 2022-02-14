using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Movement Reference")]
    [SerializeField]
    private PlayerMovement m_nomalMovement;
    [SerializeField]
    private ShoulderMovement m_shoulderMovement;
    [SerializeField]
    private Rigidbody2D m_rig2D;

    public enum E_MOVEMENT_TYPE
    {
        E_NOMAL, E_GRAPPLING
    }

    private E_MOVEMENT_TYPE m_currentType;


    private void init()
    {
        m_nomalMovement.init(m_rig2D);
        m_shoulderMovement.init();

        m_currentType = E_MOVEMENT_TYPE.E_NOMAL;
    }

    private void Start()
    {
        init();
    }


    private void Update()
    {
        if (currentType == E_MOVEMENT_TYPE.E_NOMAL)
        {
            m_nomalMovement.UpdateProcess();
        }
        m_shoulderMovement.UpdateProcess();
    }

    public E_MOVEMENT_TYPE currentType
    {
        get
        {
            return m_currentType;
        }
    }



    public void setTypeGrappling(Vector2 lookPos, bool isBodyFix)
    {
        m_shoulderMovement.setLookPosition(lookPos);
        m_shoulderMovement.isBodyFix = isBodyFix;

        m_currentType = E_MOVEMENT_TYPE.E_GRAPPLING;
    }

    public void setTypeNomal()
    {
        m_currentType = E_MOVEMENT_TYPE.E_NOMAL;
    }



    public PlayerMovement nomalMovement
    {
        get
        {
            return m_nomalMovement;
        }
    }

    public ShoulderMovement shoulderMovement
    {
        get
        {
            return m_shoulderMovement;
        }
    }




}
