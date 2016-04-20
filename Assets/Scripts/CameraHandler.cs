using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {

    public Camera mainCamera;
    public Camera insideCamera;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void SwitchView()
    {
       if (mainCamera.enabled)
       {
            mainCamera.enabled = false;
            insideCamera.enabled = true;
       }
       else
       {
            insideCamera.enabled = false;
            mainCamera.enabled = true;
       }
    }
}
