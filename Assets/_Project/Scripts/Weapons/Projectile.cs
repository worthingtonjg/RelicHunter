using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float damage;

    private string firedBy;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("other.tag: " + other.tag);
        //Debug.Log("gameObject.tag: " + gameObject.tag);
        //Debug.Log("firedBy: " + firedBy);

        if (other.tag == gameObject.tag) return;
        if (other.isTrigger) return;
        if (other.tag == firedBy) return;

        //Debug.Log("Hit: " + other.tag);

        other.SendMessage("TakeDamage", damage, SendMessageOptions.RequireReceiver);

        Destroy(gameObject);
        
    }

    public void SetFiredBy(string tag)
    {
        firedBy = tag;
    }
}
