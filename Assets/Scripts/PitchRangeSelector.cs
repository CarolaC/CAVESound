using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// divides y-axis (arm range) into pitch ranges and return active pitch
public class PitchRangeSelector : MonoBehaviour {

    public Transform listenerTransform;
    public float armRangeBottom;
    public float armRangeTop;

	[HideInInspector]
    public int pitchCount = 8;
    private float pitchRangeLength;
    private List<float> pitchRanges = new List<float>();
    [HideInInspector]
    public int activePitch;

	// Use this for initialization
	void Start () {
        pitchRangeLength = (Mathf.Abs(armRangeBottom) + armRangeTop) / pitchCount;
        
        float lastPosition = armRangeBottom;
        
        for (int i = 0; i < pitchCount; i++)
        {
            pitchRanges.Add(lastPosition);
            lastPosition += pitchRangeLength;
        }
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pitchRanges.Count; i++)
        {
            if ((listenerTransform.position.y > pitchRanges[i]) && (listenerTransform.position.y < pitchRanges[i] + pitchRangeLength))
            {
                activePitch = i;
                //print("Listener in Pitch Range " + i);
            }
        }
	}
}
