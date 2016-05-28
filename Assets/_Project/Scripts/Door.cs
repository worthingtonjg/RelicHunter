using UnityEngine;
using System.Collections.Generic;

public class Door : MonoBehaviour {
    public enum EnumDoorState
    {
        Closed,
        Open,
        Locked,
        Triggered
    }

    public Vector3 OpenPosition;
    public float OpenSpeed = 3f;
    public EnumDoorState DoorState;
    public List<GameObject> Listeners;

    private bool isOpening;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
	    if(isOpening)
        {
            Vector3 openPosition = new Vector3(transform.localPosition.x, OpenPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, openPosition, OpenSpeed * Time.deltaTime);

            if(Mathf.Approximately(transform.localPosition.y, OpenPosition.y))
            {
                isOpening = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(object damage)
    {
        Debug.Log("Door took damage");

        switch (DoorState)
        {
            case EnumDoorState.Closed:
                OpenDoor();
                break;
            case EnumDoorState.Open:
                // Do Nothing
            case EnumDoorState.Locked:
                Debug.Log("Unlock Locked Door");
                player.SendMessage("UnlockDoor", gameObject);
                break;
            case EnumDoorState.Triggered:
                TriggerDoor();
                break;
            default:
                break;
        }
    }

    public void Unlock()
    {
        Debug.Log("Unlock Door");
        OpenDoor();
    }

    private void TriggerDoor()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        Debug.Log("Opening Door");
        isOpening = true;
        DoorState = EnumDoorState.Open;

        // Notify Listeners
        Listeners.ForEach(l =>
        {
            l.SendMessage("DoorOpened", gameObject.name, SendMessageOptions.DontRequireReceiver);
        });
    }
}
