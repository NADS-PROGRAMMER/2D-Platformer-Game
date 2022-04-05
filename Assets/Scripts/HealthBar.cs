using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /** The "Health Properties"
      * @gradient 
      * - is a color that fills the color of the Image
      * 
      * @slider
      * - is a component that determines the value of our current health
      * 
      * @fill 
      * - an image component that enables the masking of the healthbar
      */
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


    /* Sets the new health of the gameObject that is attached to this script */
    public void SetCurrentHealth(int newHealth)
    {
        this.slider.value = (int) newHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
