using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Physics : MonoBehaviour
{

    public Rigidbody2D rb2d = null;
    private Vector2 velocity = Vector2.zero, moveVelocity = Vector2.zero, dashVelocity = Vector2.zero;

    [SerializeField] private Player_Physics_Settings settings = new Player_Physics_Settings();
    [SerializeField] private Transform moveDirection = null;
    [SerializeField] private GameObject colliderGameObject = null;

    private bool canMove = true;

    private void FixedUpdate()
    {
        if(dashVelocity != Vector2.zero)
            DashDrag();  
    }

    private void Update()
    {
        velocity = moveVelocity + dashVelocity;
        rb2d.velocity = velocity;
    }

    public void Move(Vector2 direction)
    {
        if (!canMove)
            return;

        if (direction != Vector2.zero)
            moveDirection.up = direction;

        moveVelocity = direction * settings.maxSpeed;
        
    }

    public void Dash()
    {
        if (!canMove)
            return;

        dashVelocity = moveDirection.up * settings.dashSpeed;
    }

    private void DashDrag()
    {
        dashVelocity *= (1 - Time.deltaTime * settings.dashDrag);
        if (Mathf.Abs(dashVelocity.magnitude) < 2f)
            dashVelocity = Vector2.zero;
    }

    public void Knockback(Vector2 collisionPosition, Animator dmgAnim)
    {
        Vector2 knockbackDirection = ((Vector2)transform.position - collisionPosition).normalized;
        canMove = false;
        dashVelocity = knockbackDirection * 40f;
        StartCoroutine(InvincibilityTimer(dmgAnim));
    }

    private IEnumerator InvincibilityTimer(Animator dmgAnim)
    {
        colliderGameObject.layer = 9;
        yield return new WaitForSeconds(.2f);
        canMove = true;
        yield return new WaitForSeconds(1.8f);
        colliderGameObject.layer = 6;
        dmgAnim.SetTrigger("ChangeState");
    }
}

[Serializable]
public class Player_Physics_Settings
{
    public float acceleration = 1f;
    public float maxSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDrag = 2f;
}
