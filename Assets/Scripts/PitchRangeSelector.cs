using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// divides y-axis (arm range) into pitch ranges and return active pitch
public class PitchRangeSelector : MonoBehaviour {

    public Transform secondTargetTransform;
    public float armRangeBottom;
    public float armRangeTop;

	[HideInInspector]
    public int pitchCount = 8;
    private float pitchTotalLength;
	private float pitchRangeLength;
    private List<float> pitchRanges = new List<float>();
    [HideInInspector]
    public int activePitch;

	// Use this for initialization
	void Start () {
		pitchTotalLength = (Mathf.Abs(armRangeBottom) + armRangeTop) / pitchCount;
		pitchRangeLength = pitchTotalLength / pitchCount;
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
			if ((secondTargetTransform.position.y > pitchRanges[i]) && (secondTargetTransform.position.y < pitchRanges[i] + pitchRangeLength))
            {
                activePitch = i;
                //print("Listener in Pitch Range " + i);
            }
        }
	}
}
