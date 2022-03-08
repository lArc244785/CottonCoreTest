using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    PlayerMovement pm;
    [SerializeField]
    Animator ani;


    private void Update()
    {
        float move = Mathf.Abs(pm.moveDirX);

        ani.SetFloat("Move", move);
    }


}
