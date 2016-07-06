using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// divides y-axis (arm range) into pitch ranges and return active pitch
public class PitchRangeSelector : MonoBehaviour {

    public Transform secondTargetTransform;
    public float armRangeBottom;
    public float armRangeTop;
	public int pitchCount;
	public Text pitchText;
	public Text newPitchText;
    
	private float pitchTotalLength;
	private float pitchRangeLength;
	private List<float> pitchRanges = new List<float>();
	private List<float> pitchFactors = new List<float>();
	[HideInInspector]
	public int activePitchRange;
    [HideInInspector]
	public float activePitch;

	// Use this for initialization
	void Start () {
		newPitchText.text = "";
		pitchText.text = "Your arm height is at pitch (1-" + pitchCount + "):";

		pitchTotalLength = (Mathf.Abs(armRangeBottom) + armRangeTop) / pitchCount;
		pitchRangeLength = pitchTotalLength / pitchCount;

		// divide into pitchRanges
		float lastPosition = armRangeBottom;
				        
        for (int i = 0; i < pitchCount; i++)
        {
            pitchRanges.Add(lastPosition);
			lastPosition += pitchRangeLength;
        }

		// set pitchFactors (differences in pitch between two notes)
		float normalPitch = 1; // unaltered pitch
		pitchFactors.Add (normalPitch);

		float quart = 0.35f;
		float second = 0.15f;
		float secondQuart = 0.5f;

		float sumPitchFactor = normalPitch + quart;
		float lastPitchFactor = quart;

		for (int i = 1; i < pitchCount; i++) {
			pitchFactors.Add (sumPitchFactor);
			lastPitchFactor = (lastPitchFactor == quart) ? second : ((lastPitchFactor == second) ? secondQuart : quart);
			sumPitchFactor += lastPitchFactor;
		}
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pitchRanges.Count; i++)
        {
			if ((secondTargetTransform.position.y > pitchRanges[i]) && (secondTargetTransform.position.y < pitchRanges[i] + pitchRangeLength))
            {
				activePitchRange = i;
				activePitch = pitchFactors[i];
				newPitchText.text = "" + (activePitchRange + 1);
				print("Listener in Pitch Range " + i + "with active pitch " + activePitch);
            }
        }
	}
}
