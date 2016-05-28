using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    public float fireSpeed = 1f;
    public float projectileSpeed = 20f;
    public float projectileLife = 3f;
    public bool autoFire;

    public GameObject weaponPort;
    public GameObject bulletPrefab;
    public AudioClip clip;

    private AudioSource audioSource;
    private float lastFire;

	// Use this for initialization
	void Start () {
        lastFire = Time.time - fireSpeed;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(autoFire)
        {
            Shoot();
            return;
        }

	    if(Input.GetAxis("Fire1") != 0 && gameObject.tag == "Player")
        {
            Shoot();
        }
	}

    public void Shoot()
    {
        Debug.Log("Shoot Pressed");
        if(Time.time >= lastFire + fireSpeed)
        {
            lastFire = Time.time;
            var clone = Instantiate(bulletPrefab, weaponPort.transform.position, weaponPort.transform.rotation) as GameObject;
            clone.GetComponent<Rigidbody>().velocity = gameObject.transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
            clone.GetComponent<Projectile>().SetFiredBy(gameObject.tag);
            Destroy(clone, projectileLife);
            audioSource.PlayOneShot(clip);
            Debug.Log("Shooting");
        }
    }


}
