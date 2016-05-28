using UnityEngine;
using System.Collections;

public class GameComplete : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FreeCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
