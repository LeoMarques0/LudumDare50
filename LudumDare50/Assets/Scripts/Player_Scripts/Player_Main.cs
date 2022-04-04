using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player_Physics))]
[RequireComponent(typeof(Player_InputSystem))]
public class Player_Main : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private int maxHealth = 5;

    [SerializeField] private Player_InputSystem inputSystem = null;
    [SerializeField] private Player_Physics physics = null;
    [SerializeField] private Player_Animations animations = null;
    [SerializeField] private Player_Aim aim = null;

    [SerializeField] private Animator gameOverAnim = null;
    [SerializeField] private List<Image> healthImages = new List<Image>();
    [SerializeField] private Sprite fullHealth = null, emptyHealth = null;

    [SerializeField] private AudioSource damageTakenSource = null;
    [SerializeField] private AudioSource pickupAudioSource = null;

    private Camera mainCamera = null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        GameManager.singleton.playerTransform = transform;

        inputSystem.SetInputs();
        //inputSystem.SetDash(delegate { physics.Dash(); });
        inputSystem.SetShoot(delegate { aim.Shoot(); }, delegate { aim.StopShooting(); });
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = inputSystem.movement.ReadValue<Vector2>();
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(inputSystem.aim.ReadValue<Vector2>());
        physics.Move(movement);
        animations.WalkAnimation(movement);
        animations.LookDirection(mousePos);
        aim.Aim(mousePos);
    }

    private void FixedUpdate()
    {
        
    }

    private void Die()
    {
        gameOverAnim.Play("GameOverPopup");
        Destroy(gameObject);
    }

    private void SetHealth()
    {
        for (int i = 0; i < healthImages.Count; i++)
        {
            if (health > i)
                healthImages[i].sprite = fullHealth;
            else
                healthImages[i].sprite = emptyHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform);
            return;
        }

        if(collision.CompareTag("Health"))
        {
            if (health == maxHealth)
                return;

            pickupAudioSource.Play();
            Destroy(collision.gameObject);
            health++;
            SetHealth();
            return;
        }

        if(collision.CompareTag("Gun"))
        {
            pickupAudioSource.Play();
            GunDrop gunDrop = collision.GetComponent<GunDrop>();
            aim.ChangeGun(gunDrop.gunInxed, gunDrop.timer, inputSystem.shoot);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(Transform damageOrigin)
    {
        damageTakenSource.Play();
        animations.ChangeDamageAnimation();
        health--;
        SetHealth();
        physics.Knockback(damageOrigin.position, animations.damageAnim);

        if (health <= 0)
            Die();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(other.transform);
    }
}
