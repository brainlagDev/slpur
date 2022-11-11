using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    #region VARIABLES

    public Player Player;

    public Slider           HealthMajorBar;
    public Slider           HealthMinorBar;
    
    public TextMeshProUGUI  SpiritsCounter;
    public TextMeshProUGUI  PotionsCounter;

    public GameObject       Interaction;
    public TextMeshProUGUI  InteractionText;

    #region Windows
    [SerializeField] private GameObject RespawnWindow;
    [SerializeField] private GameObject InventoryWindow;
    #endregion

    #endregion  // VARIABLES

    #region MONODEVELOP_CONSTRUCTIONS

    void Start()
    {
        Player = GetComponentInParent<Player>();
    }

    void Update()
    {
        HealthMajorBar.value = (float)Player.Health / (float)Player.MaxHealth;
        HealthMinorBar.value = Mathf.Lerp(HealthMinorBar.value, HealthMajorBar.value, Time.deltaTime);

        SpiritsCounter.text = $"{Player.Spirits}";
        PotionsCounter.text = $"{Player.HealingPotions}";
    }

    #endregion // MONODEVELOP_CONSTRUCTIONS

    public void ShowInteractable(InteractableObject interactable)
    {
        Interaction.SetActive(true);
        InteractionText.text = interactable.InteractionText;
    }

    public void HideInteractable()
    {
        Interaction.SetActive(false);
        InteractionText.text = "";
    }

    #region Respawn window

    public void Respawn()
    {
        GameManager.Pause();
        RespawnWindow.SetActive(true);
    }

    #endregion

    public void ShowMenu()
    {

    }

    public void ShowInventory()
    {

    }
}
