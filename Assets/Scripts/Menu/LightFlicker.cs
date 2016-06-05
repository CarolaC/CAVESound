using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    private Light light;
    private bool grow;

	// Use this for initialization
	void Start () {
	    light = GetComponent<Light>();
        grow = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (grow && light.intensity > 1.8)
            grow = false;
        else if (!grow && light.intensity < 0.5)
            grow = true;

        if (grow)
            light.intensity += 0.01f;
        else if (!grow)
            light.intensity -= 0.01f;
	}
}
