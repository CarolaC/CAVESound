using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// manages the visualization on the canvas beat panel
public class BeatVisualizer : MonoBehaviour {

    public Canvas canvas;
    public GameObject beatPointPrefab;
	public Camera activeCamera;

	private float loopDuration;
	private int numberOfBeats;
	private int pitchCount;

	private float timer;
	private GameObject beatCursor;
	private Vector3 beatCursorPosition;
    private float beatPanelWidth;
	private List<GameObject> beatPoints = new List<GameObject>();
	private float beatPanelHeight;
    private float beatPanelLayerHeight;
    
	// Use this for initialization
	void Start () {
        LoopManager loopManager = GameObject.Find("SoundManagers").GetComponent<LoopManager>();
        numberOfBeats = loopManager.numberOfBeats;
        loopDuration = loopManager.loopDuration;

		PitchRangeSelector pitchRangeSelector = GameObject.Find("SoundManagers").GetComponent<PitchRangeSelector>();
		pitchCount = pitchRangeSelector.pitchCount;

        RectTransform rectTransform = GetComponent<RectTransform>();
        beatPanelWidth = rectTransform.rect.width * canvas.scaleFactor;

		beatCursor = GameObject.Find("BeatCursor");
		beatCursorPosition = new Vector3 (0, 0, 0);

		GameObject beatPoint;
		float beatPointOffset = beatPanelWidth / numberOfBeats;
		Vector3 beatPointPosition = new Vector3 (beatPointOffset, 0, 0);

		for (int i = 0; i < (numberOfBeats - 1); i++)
        {
			beatPoint = (GameObject)Instantiate(beatPointPrefab);
			beatPoint.transform.localPosition = beatPointPosition;
			beatPoint.transform.SetParent(gameObject.transform, false);
			beatPoints.Add(beatPoint);

			beatPointPosition.x += beatPointOffset;
        }

		beatPanelHeight = rectTransform.rect.height * canvas.scaleFactor;
		beatPanelLayerHeight = beatPanelHeight / pitchCount;

        timer = 0; 
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		beatCursor.transform.localPosition = beatCursorPosition;
		// subtract half of the beatPanelWidth because the null-point is in the middle of the panel
		beatCursorPosition.x = (beatPanelWidth / loopDuration) * timer - (beatPanelWidth * 0.5f);

        // start new loop if loop duration has been reached
        if (timer > loopDuration)
        {
            // reset timer to overstepped milliseconds
            timer %= loopDuration;
			beatCursorPosition.x = (beatPanelWidth / loopDuration) * timer - (beatPanelWidth * 0.5f);
        }
	}

    public Vector3 calculateBeatSoundPointPosition(float pitchRange, float time)
    {
		return new Vector3((beatPanelWidth / loopDuration) * timer, beatPanelLayerHeight * pitchRange - (beatPanelHeight * 0.5f) + (beatPanelLayerHeight * 0.5f), 0);
    }
}
