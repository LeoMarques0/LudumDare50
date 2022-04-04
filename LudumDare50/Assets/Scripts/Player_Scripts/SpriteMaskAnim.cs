using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskAnim : MonoBehaviour
{
    [SerializeField] SpriteMask spriteMask = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;

    private void Update()
    {
        spriteMask.sprite = spriteRenderer.sprite;
        spriteMask.transform.localScale = spriteRenderer.transform.localScale;
    }
}
