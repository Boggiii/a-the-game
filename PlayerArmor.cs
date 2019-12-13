using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmor : MonoBehaviour
{
    public float startArmor;
    public float currentArmor;
    public Slider armorSlider;
    public Text armorText;

    void Awake()
    {
        currentArmor = startArmor;
        armorSlider.maxValue = startArmor;
        armorSlider.value = currentArmor;
        armorText.text = currentArmor.ToString();
    }

}
