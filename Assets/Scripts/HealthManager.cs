using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public Slider slider;
    public Color maxHealthColor;
    public Color minHealthColor;
    public Image healthFillImage;
    float maxHealth;
    void Start() {
        
    }

    void Update() {
        
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        healthFillImage.color = maxHealthColor;
        maxHealth = health;
    }

    public void SetHealth(int health) {
        slider.value = health;
        healthFillImage.color = Color.Lerp(minHealthColor,maxHealthColor,(float)health/maxHealth);
    }
}
