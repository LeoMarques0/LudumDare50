using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{

    [SerializeField] private AudioSource explosionSource = null;

    private void OnParticleCollision(GameObject other)
    {
        explosionSource.Play();
    }
}
