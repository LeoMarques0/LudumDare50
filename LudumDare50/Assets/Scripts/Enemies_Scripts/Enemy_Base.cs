using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    public int maxHp = 10;
    public int hp = 10;
    public float speed = 10f;
    public int scoreValue = 10;
    public bool dead = false;

    [HideInInspector] public Spawners spawner;

    [SerializeField] private Animator anim = null;
    [SerializeField] protected Transform spriteTransform = null;
    [SerializeField] private ParticleSystem bloodSplash = null;
    [SerializeField] private ParticleSystem textParticle = null;
    [SerializeField] private List<Drop> drops = new List<Drop>();
    [SerializeField] private AudioSource deathSource = null;

    protected Transform playerTransform = null;
    protected bool spawned = false;

    public virtual void Start()
    {
        playerTransform = GameManager.singleton.playerTransform;

        Invoke("SpawnDelay", 1f);
    }

    protected virtual void Die()
    {
        deathSource.Play();
        dead = true;
        ScoreManager.instance.AddScore(scoreValue);
        spawner.CheckDificulty();
        bloodSplash.Play();
        ScoreManager.instance.particleScoreText.text = (scoreValue * ScoreManager.instance.GetComboMultiplier()).ToString();
        textParticle.Play();
        DropItem();
        anim.SetTrigger("Dead");
        Screenshake.instance.StartScreenShake();
    }

    public void PrepareRespawn()
    {
        if(spawned)
            spawner.currentEnemies.Remove(gameObject);
    }

    public void Respawn()
    {
        spawned = false;
        dead = false;
        hp = maxHp;
        anim.SetTrigger("Live");
        Invoke("SpawnDelay", 1f);
    }

    private void DropItem()
    {
        int value = UnityEngine.Random.Range(0, 101);
        List<GameObject> possibleDrops = new List<GameObject>();
        foreach (Drop drop in drops)
        {
            if (value >= drop.range.x && value <= drop.range.y)
                possibleDrops.Add(drop.dropObject);
        }

        if(possibleDrops.Count > 0)
            Instantiate(possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)], transform.position, Quaternion.identity);
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void SpawnDelay()
    {
        spawned = true;
        anim.SetBool("Walk", true);
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Bang");
        Gun gun = other.GetComponent<Gun>();
        hp -= gun.damage;

        if (hp <= 0 && !dead)
            Die();
    }
}

[Serializable]
public class Drop
{
    public GameObject dropObject = null;
    public Vector2 range = Vector2.zero;
}
