using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage;

    private void OnParticleCollision(GameObject other)
    {
        print("Hit");
    }
}
