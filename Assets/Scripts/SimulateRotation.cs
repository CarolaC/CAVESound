using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SimulateRotation : MonoBehaviour {
	
	private float speed = 200;
	private bool performRotation = false;
	private bool performRotationBack = false;
	private Quaternion rotateTo;
	private Quaternion rotateBack;

	[HideInInspector]
	public UnityEvent setOnLoopEvent;


	// Use this for initialization
	void Start () {
		if (setOnLoopEvent == null)
			setOnLoopEvent = new UnityEvent ();

		rotateTo = Quaternion.Euler (new Vector3 (90, 0, 0));
		rotateBack = Quaternion.Euler (new Vector3 (0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) 
		{
			setOnLoopEvent.Invoke ();
			performRotation = true;
		}

		if (performRotation) 
		{
			transform.rotation = Quaternion.RotateTowards (transform.rotation, rotateTo, Time.deltaTime * speed);

			if (transform.rotation == rotateTo)
			{
				performRotation = false;
				performRotationBack = true;
			}
		}

		if (performRotationBack) 
		{
			transform.rotation = Quaternion.RotateTowards (transform.rotation, rotateBack, Time.deltaTime * speed);
			
			if (transform.rotation == rotateBack)
			{
				performRotationBack = false;
			}
		}
	}
}
