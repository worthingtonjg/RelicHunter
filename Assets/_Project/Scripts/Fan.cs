using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour
{
    public float RotateSpeed = 2f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed));
	}
}
