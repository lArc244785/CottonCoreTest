using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingShooter : MonoBehaviour
{
    [SerializeField]
    private GrapplingGun m_grpplingGun;
   

    public void Start()
    {
        m_grpplingGun.init();
    }

    public void Fire()
    {
        m_grpplingGun.Fire();
    }

    public void Pull()
    {
        m_grpplingGun.Pull();
    }

    public void Cancel()
    {
        m_grpplingGun.Cancel();
    }

    public bool isGrapplingAction
    {
        get
        {
            return m_grpplingGun.m_eState != GrapplingGun.E_State.E_NONE;
        }
    }
}
