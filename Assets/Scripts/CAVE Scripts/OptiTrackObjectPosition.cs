/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System.Collections;

public class OptiTrackObjectPosition : MonoBehaviour {
	
	public int rigidbodyIndex;
	public Vector3 rotationOffset;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = OptiTrackManagerPosition.Instance.getPosition(rigidbodyIndex);
		Quaternion rot = OptiTrackManagerPosition.Instance.getOrientation(rigidbodyIndex);
		rot = rot * Quaternion.Euler(rotationOffset);
		this.transform.position = pos;
		//this.transform.rotation = rot;
	}
}