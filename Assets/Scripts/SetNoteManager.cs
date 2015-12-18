using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// checks the rotation of the second target and invokes an event which the loopmanager receives
public class SetNoteManager : MonoBehaviour {

	public Transform secondTargetTransform;
	private int timeOut;

	[HideInInspector]
	public UnityEvent setOnLoopEvent;
	
	// Use this for initialization
	void Start () {
		if (setOnLoopEvent == null)
			setOnLoopEvent = new UnityEvent ();

		timeOut = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//print ("ANGLES: " + secondTargetTransform.rotation.eulerAngles.x + ", " + secondTargetTransform.rotation.eulerAngles.y + ", " + secondTargetTransform.rotation.eulerAngles.z);

		if (secondTargetTransform.rotation.eulerAngles.z > 200 && timeOut == 0) {
			print ("You have set a loop note!");
			setOnLoopEvent.Invoke ();
			timeOut = 100;
		}

		if (timeOut > 0)
			timeOut--;
	}
}
