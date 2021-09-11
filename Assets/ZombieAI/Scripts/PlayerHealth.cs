using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float value = 100f;
    public PlayerHealth parentReference;
    public float damageMultiplier = 1.0f;

    public Image GotHitImage;
    public delegate void OnZeroHealthHandler();
    public event OnZeroHealthHandler OnZeroHealth;

    public void TakeDamage(float damage)
    {
        if (value == 0) return;
        damage *= damageMultiplier;
        if (parentReference != null)
        {
            parentReference.TakeDamage(damage);
            return;
        }
        value -= damage;
        value = value < 0 ? 0 : value;
        if (GotHitImage)
        {
            Color GotHitImageColor = GotHitImage.color;
            if (GotHitImageColor.a < 0.8f)
            {
                GotHitImageColor.a += 0.2f;
            }
            else
            {
                GotHitImageColor.a = 0.9f;
            }
            GotHitImage.color = GotHitImageColor;
        }
        if (value == 0)
        {
            OnZeroHealth?.Invoke();
        }

    }

    void Update()
    {
        if (GotHitImage && GotHitImage.color.a > 0)
        {
            Color GotHitImageColor = GotHitImage.color;
            GotHitImageColor.a -= 0.01f;
            GotHitImage.color = GotHitImageColor;
        }
    }
}
