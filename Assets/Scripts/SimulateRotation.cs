using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// simulate the rotation of the second target by pressing 'A', so that you can add loop notes with keyboard
public class SimulateRotation : MonoBehaviour {

    private float movementSpeed = 8;
	private float rotationSpeed = 200;
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
        // movement
        if (Input.GetKey(KeyCode.J))
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.L))
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.I))
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.K))
            transform.position += Vector3.back * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.R))
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.F))
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;

        // rotation
        if (Input.GetKeyDown(KeyCode.A)) 
		{
			setOnLoopEvent.Invoke ();
			performRotation = true;
		}

		if (performRotation) 
		{
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, Time.deltaTime * rotationSpeed);

			if (transform.rotation == rotateTo)
			{
				performRotation = false;
				performRotationBack = true;
			}
		}

		if (performRotationBack) 
		{
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateBack, Time.deltaTime * rotationSpeed);
			
			if (transform.rotation == rotateBack)
			{
				performRotationBack = false;
			}
		}
	}
}
