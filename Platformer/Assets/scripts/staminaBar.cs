using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;

        fill.color = gradient.Evaluate(1f);
    }



    public void SetStamina(int stamina)
    {
        slider.value = stamina;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
