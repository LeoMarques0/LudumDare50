using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy_Base
{

    [SerializeField] private Transform aimDirection = null;
    [SerializeField] private ParticleSystem bow = null;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (spawned && !dead)
        {
            FindPlayer();
            Aim(playerTransform.position);
        }
    }

    public void Aim(Vector2 pos)
    {
        aimDirection.up = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
    }

    public void Shoot()
    {
        bow.Stop();
        bow.Play();
        StartCoroutine(ShootTimer());
    }

    public IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(2f);
        Shoot();
    }

    public void FindPlayer()
    {
        if (playerTransform == null)
            return;

        if(Vector2.Distance(transform.position, playerTransform.position) > 8f)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        int xScale = transform.position.x < playerTransform.position.x ? 2 : -2;
        spriteTransform.localScale = new Vector3(xScale, 2, 2);
    }

    protected override void Die()
    {
        base.Die();
        StopAllCoroutines();
    }

    protected override void SpawnDelay()
    {
        base.SpawnDelay();
        StartCoroutine(ShootTimer());
    }
}
