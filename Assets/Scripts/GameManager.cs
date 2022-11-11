using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player PlayerPrefab;
    public Player Player;

    public AudioSource Ambient;
    public AudioClip AmbientClip;
    [Range(0, 1)]
    public float AmbientVolume;

    public GameObject SpawnPoint;

    public Enemy[] Enemies = new Enemy[0];

    public InteractableObject ItemInstancePrefab;

    #region MONODEVELOP_CONSTRUCTIONS

    private void Awake()
    {
        Instance = this;
        Enemies = GameObject.FindObjectsOfType<Enemy>();
        Ambient.volume = AmbientVolume;
        Ambient.PlayOneShot(AmbientClip);
    }

    private void LateUpdate()
    {
    }

    private void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    #endregion

    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void Unpause()
    {
        Time.timeScale = 1;
    }
    public event Action OnPlayerDeath;
    public void ReloadLevel()
    {
        if(OnPlayerDeath != null) 
        {
            OnPlayerDeath();
        }
        StartCoroutine(DelayOnPlayerDeath());
        Debug.Log("Player dead :_(");
        
    }

    public void SavePlayerData()
    {
        /*if (Player == null)
            Player = FindObjectOfType<Player>();

        PlayerData data = new PlayerData();
        data.Read(Player);

        using (FileStream fs = new FileStream(
            Application.persistentDataPath + PlayerDataFileName, 
            FileMode.OpenOrCreate))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }*/
    }
    public void LoadPlayerData()
    {
        /*if (Player == null)
            Player = Instantiate(PlayerPrefab).GetComponent<Player>();

        PlayerData data = new PlayerData();

        using (FileStream fs = new FileStream(PlayerDataFileName, FileMode.Open))
        {
            BinaryFormatter bf = new BinaryFormatter();
            data = bf.Deserialize(fs) as PlayerData;
        }

        data.Apply(Player);*/
    }

    public static void SpawnItem(ItemInstance item, Vector3 position)
    {
        Instantiate(item, position, Quaternion.identity);
    }
    public static void PlayerAddItem(Item item)
    {
        Instance.Player.Inventory.Items.Add(item);
    }
    IEnumerator DelayOnPlayerDeath(){

        //Pause();
        yield return new WaitForSeconds(0.5f);
        //Player.PlayerUI.Respawn();
        // respawning enemies
        foreach (var enemy in Enemies)
        {
            enemy.gameObject.GetComponent<Enemy>().Respawn();
        }

        //Unpause();
        Player.transform.position = SpawnPoint.transform.position;
        Player.StateMachine.ChangeState(Player.SpawnState);
        yield return null;
    }
}
