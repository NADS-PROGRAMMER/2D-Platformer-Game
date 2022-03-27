using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] private Gradient gradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;

    private int currentHealth;

    private void Start()
    {
        this.currentHealth = (int) this.slider.maxValue;
        this.fill.color = gradient.Evaluate(1);
    }

    public void SetCurrentHealth(int newHealth)
    {
        this.currentHealth = newHealth;
        this.slider.value = (int) newHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
