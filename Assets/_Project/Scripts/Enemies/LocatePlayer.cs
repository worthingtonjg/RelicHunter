using UnityEngine;
using System.Collections;

public class LocatePlayer : MonoBehaviour {
    public float moveSpeed = 10f;
    public float stoppingDistance = 20f;

    private GameObject target;
    private Vector3 lastSeenAt;
    private bool firedUpon;
    private bool inRange;
    private bool canSeePlayer;
    private CharacterController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        canSeePlayer = false;
        if (inRange || firedUpon)
        {
            Vector3 direction = target.transform.position - transform.position;

            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == target)
                {
                    Debug.DrawRay(transform.position, direction, Color.red);

                    transform.LookAt(target.transform);
                    gameObject.SendMessage("Shoot");

                    lastSeenAt = target.transform.position;
                }

                if (Vector3.Distance(target.transform.position, transform.position) > stoppingDistance)
                {
                    controller.Move((lastSeenAt - transform.position).normalized * moveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (lastSeenAt.x != 0 || lastSeenAt.y != 0 || lastSeenAt.z != 0)
            {
                controller.Move((lastSeenAt - transform.position).normalized * moveSpeed * Time.deltaTime);
            }
        }
	}

    public void PlayerInRange(object other)
    {
        target = (GameObject)other;
        Debug.Log("Player in range");
        inRange = true;
    }

    public void PlayerOutOfRange(object lastLocation)
    {
        target = null;
        Debug.Log("Player out of range");
        inRange = false;
    }

    public void HitByPlayer(object other)
    {
        target = (GameObject)other;
        Debug.Log("Fired upon by Player");
        firedUpon = true;
    }
}
