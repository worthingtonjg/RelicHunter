using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fungus;

public class PlayerHealth : MonoBehaviour {
    public float TotalHealth = MAXHEALTH;
    public float CurrentHealth = MAXHEALTH;
    public Image HealthBar;
    public GameObject ExplosionPrefab;
    public int InitialLives = 3;
    public int CurrentLives;
    public string SceneName = "Scene01";
    public string LoadScene = "Scene00";
    public Text Lives;
    public AudioClip pickupClip;
    public AudioClip damageClip;
    public AudioClip playerDieClip;
    public Flowchart Flowchart;

    private AudioSource audioSource;
    private const int MAXHEALTH = 100;
    

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == SceneName)
        {
            CurrentLives = InitialLives;
            Lives.text = CurrentLives.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(object damage)
    {
        Debug.Log("Take Damage Called");

        UpdateHealth(-((float)damage));

        Flowchart.ExecuteBlock("TakeDamage");
        audioSource.PlayOneShot(damageClip);

        CheckIsDead();

        
    }

    public void PickupHealth(object healthPickup)
    {
        GameObject health = (GameObject)healthPickup;
        Pickup pickup = health.GetComponent<Pickup>();

        UpdateHealth(pickup.PickupValue);

        Debug.Log("Health Pickup += " + pickup.PickupValue);

        audioSource.PlayOneShot(pickupClip);

        health.SetActive(false);
    }

    public void UpdateHealth(float damage)
    {
        CurrentHealth += damage;
        if (CurrentHealth > TotalHealth) CurrentHealth = TotalHealth;
        HealthBar.fillAmount = CurrentHealth / TotalHealth;
    }

    private bool CheckIsDead()
    {
        if (CurrentHealth > 0) return false;

        if(gameObject.tag == "Player")
        {
            // Kill the player - respawn or restart level
            CurrentLives--;
            Lives.text = CurrentLives.ToString();
            CurrentHealth = TotalHealth;
            HealthBar.fillAmount = CurrentHealth / TotalHealth;
            if (CurrentLives == 0)
            {
                audioSource.PlayOneShot(playerDieClip);
                Flowchart.ExecuteBlock("GameOver");
            }
            else
            {
                audioSource.PlayOneShot(playerDieClip);
                Flowchart.ExecuteBlock("PlayerDie");
                SendMessage("ResetPlayerPosition");
            }
        }

        return true;
    }
}
