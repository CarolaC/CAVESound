using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightLoop : MonoBehaviour {

	public List<GameObject> lightObjects;
    private bool grow;

	// Use this for initialization
	void Start () {
        grow = true;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject lightObject in lightObjects) {
			Light light = lightObject.GetComponent<Light> ();
			LightState state = lightObject.GetComponent<LightState> ();

			if (state.grow && light.intensity > 1.8)
				state.grow = false;
			else if (!state.grow && light.intensity < 0.5)
				state.grow = true;
		
			if (state.grow)
				light.intensity += 0.01f;
			else if (!state.grow)
				light.intensity -= 0.01f;
		}

	}
}
