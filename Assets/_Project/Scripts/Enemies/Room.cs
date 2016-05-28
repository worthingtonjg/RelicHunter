using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Room : MonoBehaviour {
    public List<GameObject> Listeners;
    public string RoomEnteredMessage = "RoomEntered";
    public string RoomExitedMessage = "RoomExited";

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Listeners.ForEach(l =>
            {
                l.SendMessage(RoomEnteredMessage);
            });
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Listeners.ForEach(l =>
            {
                l.SendMessage(RoomExitedMessage);
            });
        }
    }
}
