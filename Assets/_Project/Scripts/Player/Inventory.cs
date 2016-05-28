using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public bool RedRune = false;
    public bool BlueRune = false;
    public bool GreenRune = false;
    public bool YellowRune = false;
    public AudioClip pickupClip;
    public Text Enemies;
    public Text Relics;

    private int enemiesTotal;
    private int enemiesKilled;

    private int relicsTotal = 3;
    private int relicsCollected;

    private GameObject player;
    private AudioListener audioListener;
    private AudioSource audioSource;
    private List<GameObject> enemies;

    // Use this for initialization
    private void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        audioSource = GetComponent<AudioSource>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        enemies.Add(GameObject.FindGameObjectWithTag("Boss"));
    }

    private void Awake()
    {
        UpdateRuneCount();
        EnemyKilled();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void PickUpRune(object gameObject)
    {
        GameObject rune = (GameObject)gameObject;

        ++relicsCollected;
        UpdateRuneCount();

        switch (rune.tag)
        {
            case "Red":
                RedRune = true;
                break;
            case "Blue":
                BlueRune = true;
                break;
            case "Green":
                GreenRune = true;
                break;
            case "Yellow":
                YellowRune = true;
                break;
        }

        if (audioSource != null && pickupClip != null)
        {
            audioSource.PlayOneShot(pickupClip);
        }

        // Destroy object
        GameObject.Destroy(rune);
    }

    private void UpdateRuneCount()
    {
        Relics.text = relicsCollected + " / " + relicsTotal;
    }

    public void UnlockDoor(object door)
    {
        Debug.Log("Inventory.UnlockDoor");
        GameObject doorToOpen = (GameObject)door;
        Debug.Log(doorToOpen.tag + " " + RedRune);
        if (doorToOpen.tag == "RedDoor" && RedRune)
        {
            Debug.Log("Unlock Red Door");
            doorToOpen.SendMessage("Unlock");
        }
        if (doorToOpen.tag == "BlueDoor" && BlueRune)
        {
            doorToOpen.SendMessage("Unlock");
        }
        if (doorToOpen.tag == "GreenDoor" && GreenRune)
        {
            doorToOpen.SendMessage("Unlock");
        }
        if (doorToOpen.tag == "YellowDoor" && YellowRune)
        {
            doorToOpen.SendMessage("Unlock");
        }
    }

    public void EnemyKilled()
    {
        if(enemies == null)
        {
            Enemies.text = "";
            return;
        }

        int killed = enemies.Count(e => !e.activeSelf);

        Enemies.text = killed + " / " + enemies.Count;
    }

    public void BossKilled()
    {
        // Turn off the active scene's Audio Listener at the end of the level
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //player.SetActive(false);
            audioListener = player.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }
}
