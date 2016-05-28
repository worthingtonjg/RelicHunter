using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        SendMessageUpwards("PlayerInRange", other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        SendMessageUpwards("PlayerOutOfRange", other.gameObject.transform);
    }
}
