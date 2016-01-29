using UnityEngine;
using System.Collections;

// simulate the listener's movement by pressing the arrow keys
public class SimulateMovement : MonoBehaviour {

    private float speed = 8;
    private GameObject secondTarget;

	// Use this for initialization
	void Start () {
        secondTarget = GameObject.Find("SecondTarget");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += Vector3.forward * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.back * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            transform.position += Vector3.up * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.down * speed * Time.deltaTime;

        secondTarget.transform.position = transform.position - new Vector3(1, 0, 0);
	}
}
