using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fungus;

public class EnemyHealth : MonoBehaviour
{
    public float TotalHealth = 10;
    public float CurrentHealth = 10;
    public Image HealthBar;
    public GameObject ExplosionPrefab;
    public Flowchart flowChart;
    public AudioClip ExplosionClip;
    public AudioClip HitClip;

    private AudioSource audioSource;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
    }

    public void TakeDamage(object damage)
    {
        Debug.Log("Take Damage Called");

        CurrentHealth -= (float)damage;
        if (CurrentHealth > TotalHealth) CurrentHealth = TotalHealth;
        HealthBar.fillAmount = CurrentHealth / TotalHealth;

        if(audioSource != null && HitClip != null)
            audioSource.PlayOneShot(HitClip);

        if (!CheckIsDead())
        {
            SendMessageUpwards("HitByPlayer", player.gameObject);
        }
    }

    private bool CheckIsDead()
    {
        if (CurrentHealth > 0) return false;

        if (gameObject.tag == "Enemy" || gameObject.tag == "Boss")
        {
            GameObject clone = Instantiate(ExplosionPrefab, gameObject.transform.position + Vector3.up, Quaternion.identity) as GameObject;
            clone.SetActive(true);
            gameObject.SetActive(false);
            audioSource.PlayOneShot(ExplosionClip);

            if (gameObject.tag == "Boss")
            {
                player.SendMessage("BossKilled", gameObject);
                flowChart.ExecuteBlock("WinSequence");
            }
            else
            {
                player.SendMessage("EnemyKilled", gameObject);
            }
        }
        return true;
    }
}
