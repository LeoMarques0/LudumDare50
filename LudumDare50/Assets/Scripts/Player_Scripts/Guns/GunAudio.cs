using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private ParticleSystem particlesSystem = null;
    [SerializeField] private Collider2D col = null;

    private void Start()
    {
        particlesSystem.trigger.SetCollider(0, col);
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = particlesSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        print(numEnter);
        if (numEnter > 0)
        {
            print("Audio");
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
