using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    private Creature Creature;
    private Slider HealthSlider;

    private void Start()
    {
        Creature = GetComponentInParent<Creature>();
        HealthSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        HealthSlider.value = Creature.Health / Creature.MaxHealth;
    }
}
