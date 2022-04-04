using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Aim : MonoBehaviour
{
    [SerializeField] private Transform character = null;
    [SerializeField] private ParticleSystem bullets = null;
    [SerializeField] private List<ParticleSystem> possibleBullets = null;

    private bool canShoot = true;
    private float shootTimer = 1f;

    private void Awake()
    {
        shootTimer = bullets.main.duration;
    }

    public void Aim(Vector2 pos)
    {
        character.up = (pos - (Vector2)transform.position).normalized;
    }

    public void Shoot()
    {
        if (!canShoot)
            return;

        bullets.Play();
        StartCoroutine(ShootTimer());
    }

    public void StopShooting()
    {
        bullets.Stop();
    }

    public void ChangeGun(int index, float timer, InputAction shoot)
    {
        bullets.Stop();
        bullets = possibleBullets[index];
        shootTimer = bullets.main.duration;
        if (shoot.IsPressed())
            bullets.Play();
        StopAllCoroutines();
        StartCoroutine(GunTimer(timer, shoot));
        canShoot = true;
    }

    private IEnumerator ShootTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootTimer);
        canShoot = true;
    }

    private IEnumerator GunTimer(float timer, InputAction shoot)
    {
        yield return new WaitForSeconds(timer);
        bullets.Stop();
        bullets = possibleBullets[0];
        shootTimer = bullets.main.duration;
        if (shoot.IsPressed())
            bullets.Play();

        canShoot = true;
        StopAllCoroutines();
    }
}
