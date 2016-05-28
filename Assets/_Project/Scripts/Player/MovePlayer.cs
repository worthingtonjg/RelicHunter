using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    [HideInInspector]public Vector3 PlayerOriginalPosition;
    private CharacterController controller;
    public float PlayerSpeed = 20f;
    private float RotationSpeed = 3f;

	// Use this for initialization
	private void Start ()
    {
        controller = gameObject.GetComponent<CharacterController>();
        PlayerOriginalPosition = transform.position;
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("ShoulderLeft") || Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, 0f, RotationSpeed);
        }

        if (Input.GetButton("ShoulderRight") || Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, 0f, -RotationSpeed);
        }

        if (Input.GetAxis("Horizontal") != 0 || (Input.GetAxis("Vertical") != 0))
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            controller.Move(moveDirection * PlayerSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, -Input.GetAxis("Mouse X") * RotationSpeed, 0);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * RotationSpeed, 0, 0);
        }

        if (Input.GetAxis("RightJoystickHorizontal") != 0)
        {
            transform.Rotate(0, -Input.GetAxis("RightJoystickHorizontal") * RotationSpeed, 0);
        }

        if (Input.GetAxis("LeftJoystickVertical") != 0)
        {
            transform.Rotate(-Input.GetAxis("LeftJoystickVertical") * RotationSpeed, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Rune") // or "Health" or some pickup
        {

        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = PlayerOriginalPosition;
    }
}
