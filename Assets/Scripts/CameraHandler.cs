using UnityEngine;
using System.Collections;

// the "switch view" button toggles the view between two cameras
public class CameraHandler : MonoBehaviour {

    public Camera mainCamera;
    public Camera insideCamera;
    public GameObject listener;
    public GameObject secondTarget;
    public SoundDirectionManager soundDirectionManager;

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
           listener.GetComponent<MeshRenderer>().enabled = false;
           secondTarget.GetComponent<MeshRenderer>().enabled = false;
           soundDirectionManager.ToggleLineRenderer(false);
       }
       else
       {
            insideCamera.enabled = false;
            mainCamera.enabled = true;
            listener.GetComponent<MeshRenderer>().enabled = true;
            secondTarget.GetComponent<MeshRenderer>().enabled = true;
            soundDirectionManager.ToggleLineRenderer(true);
       }
    }
}
