/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using System;
using System.Collections;
using OptitrackManagement;

public class OptiTrackManagerRight : MonoBehaviour 
{
	public string myName;
	public float scale = 20.0f;
	private static OptiTrackManagerRight instance;
	public Vector3 origin = Vector3.zero; // set this to wherever you want the center to be in your scene
	
	public static OptiTrackManagerRight Instance
	{
		get { return instance; } 
	}
	
	void Awake()
	{
		instance = this;
	}
	
	~OptiTrackManagerRight ()
	{      
		Debug.Log("OptitrackManager: Destruct");
		OptitrackManagement.DirectMulticastSocketClient.Close();
	}
	
	void Start () 
	{
		Debug.Log(myName + ": Initializing");
		
		OptitrackManagement.DirectMulticastSocketClient.Start();
		Application.runInBackground = true;
		origin = this.gameObject.transform.position;
	}
	
	public OptiTrackRigidBody getOptiTrackRigidBody(int index)
	{
		// only do this if you want the raw data
		if(OptitrackManagement.DirectMulticastSocketClient.IsInit())
		{
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream();
			return networkData.getRigidbody(index);
		}
		else
		{
			OptitrackManagement.DirectMulticastSocketClient.Start();
			return getOptiTrackRigidBody(index);
		}
	}
	
	public Vector3 getPosition(int rigidbodyIndex)
	{
		if(OptitrackManagement.DirectMulticastSocketClient.IsInit())
		{
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream();
			Vector3 pos = origin + networkData.getRigidbody(rigidbodyIndex).position * scale;
			pos.x = -pos.x; // not really sure if this is the best way to do it
			//pos.y = -pos.y; // these may change depending on your configuration and calibration
			//pos.z = 0;
			return pos;
		}
		else
		{
			return Vector3.zero;
		}
	}
	
	public Quaternion getOrientation(int rigidbodyIndex)
	{
		// should add a way to filter it
		if(OptitrackManagement.DirectMulticastSocketClient.IsInit())
		{
			DataStream networkData = OptitrackManagement.DirectMulticastSocketClient.GetDataStream();
			Quaternion rot = networkData.getRigidbody(rigidbodyIndex).orientation;
			
			// change the handedness from motive
			//rot = new Quaternion(rot.z, rot.y, rot.x, rot.w); // depending on calibration
			
			// Invert pitch and yaw
			Vector3 euler = rot.eulerAngles;
			//rot.eulerAngles = new Vector3(euler.z, -euler.y, euler.x);
			rot.eulerAngles = new Vector3(0f, -euler.y, euler.x); // these may change depending on your calibration
			
			return rot;
		}
		else
		{
			return Quaternion.identity;
		}
	}
	
	public void DeInitialize()
	{
		OptitrackManagement.DirectMulticastSocketClient.Close();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
