using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasJoystickManager : MonoBehaviour {

    public GameObject canvasJoystick;

	// Use this for initialization
	void Start ()
    {

#if UNITY_ANDROID

        canvasJoystick.SetActive(true);


#elif UNITY_STANDALONE || UNITY_EDITOR

        canvasJoystick.SetActive(false);
        
#endif



    }

    // Update is called once per frame
    void Update () {




    }
}
