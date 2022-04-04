using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{

    public Animator anim = null;
    public Animator damageAnim = null;
    public Transform spriteTransform = null;
    public Transform gunTransform = null;

    public void LookDirection(Vector2 mousePosition)
    {
        int xScale = transform.position.x < mousePosition.x ? 2 : -2;
        spriteTransform.localScale = new Vector3(xScale, 2, 2);
        gunTransform.localScale = new Vector3(2, xScale, 2);
    }

    public void WalkAnimation(Vector2 movement)
    {
        anim.SetBool("Walking", movement != Vector2.zero);
    }

    public void ChangeDamageAnimation()
    {
        damageAnim.SetTrigger("ChangeState");
    }
}
