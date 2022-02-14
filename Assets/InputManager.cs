using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleToon<InputManager>
{


    [SerializeField]
    PlayerMovementManager m_playerMovementManager;
    [SerializeField]
    GrapplingShooter m_shooter;

    private void Awake()
    {
        init();
    }

    protected override bool init()
    {
        return base.init();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(m_playerMovementManager.currentType == PlayerMovementManager.E_MOVEMENT_TYPE.E_NOMAL)
        {
            Vector2 moveDir = context.ReadValue<Vector2>();
            moveDir *= Vector2.right;
            m_playerMovementManager.nomalMovement.moveDir = moveDir;
        }
    }



    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_shooter.Fire();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (m_playerMovementManager.currentType == PlayerMovementManager.E_MOVEMENT_TYPE.E_NOMAL && context.started)
        {
            m_playerMovementManager.nomalMovement.Jump();
        }
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_shooter.Pull();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_shooter.Cancel();
            m_playerMovementManager.setTypeNomal();
            m_playerMovementManager.shoulderMovement.SetMouse();
        }
    }

    public void OnRightRebound(InputAction.CallbackContext context)
    {
        if (m_playerMovementManager.currentType == PlayerMovementManager.E_MOVEMENT_TYPE.E_GRAPPLING && 
            m_shooter.isGrapplingAction &&
            context.started)
        {
            m_playerMovementManager.nomalMovement.Rebound(true);
        }
    }

    public void OnLeftRebound(InputAction.CallbackContext context)
    {
        if (m_playerMovementManager.currentType == PlayerMovementManager.E_MOVEMENT_TYPE.E_GRAPPLING &&
            m_shooter.isGrapplingAction &&
            context.started)
        {
            m_playerMovementManager.nomalMovement.Rebound(false);
        }
    }


    public Vector2 inGameMousePosition2D
    {
        get
        {
            return (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}
  