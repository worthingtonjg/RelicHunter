using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public string PickupMessage = "Pickup";
    public float PickupValue = 50;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.SendMessage(PickupMessage, gameObject, SendMessageOptions.DontRequireReceiver);
            Debug.Log(PickupMessage + " " + gameObject.tag);
        }
    }
}
