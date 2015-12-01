using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LoopManager : MonoBehaviour {

	private SoundAreaSelector soundAreaSelector;
	private PitchRangeSelector pitchRangeSelector;
	private SimulateRotation simulator;

	// Use this for initialization
	void Start () {	
		simulator = GameObject.Find("SecondTarget").GetComponent<SimulateRotation>();
		simulator.setOnLoopEvent.AddListener (setOnLoop);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setOnLoop() {
		print ("Loopmanager here - event was invoked");
	}
}
