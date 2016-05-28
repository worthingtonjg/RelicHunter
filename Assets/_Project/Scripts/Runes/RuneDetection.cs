using UnityEngine;
using System.Collections;

public class RuneDetection : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.SendMessage("PickUpRune", gameObject);
        }
    }
}
