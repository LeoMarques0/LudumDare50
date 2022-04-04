using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy_Base
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (spawned && !dead)
            FindPlayer();
    }

    public void FindPlayer()
    {
        if (playerTransform == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        int xScale = transform.position.x < playerTransform.position.x ? 2 : -2;
        spriteTransform.localScale = new Vector3(xScale, 2, 2);
    }
}
