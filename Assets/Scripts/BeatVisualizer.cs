using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// manages the visualization on the canvas beat panel
public class BeatVisualizer : MonoBehaviour {

    public Canvas canvas;
    public GameObject beatPointPrefab;

    private float timer;
    private int numberOfBeats;
    private float loopDuration;
	private GameObject beatCursor;
	// a bit of offset so that the beatCursor and -Points are inside the beatPanel rectangle
	private Vector3 beatStartPosition = new Vector3 (15, 0, 0);
	private Vector3 beatCursorPosition;
    private float beatPanelWidth;
    private List<GameObject> beatPoints = new List<GameObject>();
    
	// Use this for initialization
	void Start () {
        LoopManager loopManager = GameObject.Find("SoundManagers").GetComponent<LoopManager>();
        numberOfBeats = loopManager.numberOfBeats;
        loopDuration = loopManager.loopDuration;

        RectTransform rectTransform = GetComponent<RectTransform>();
        beatPanelWidth = rectTransform.rect.width * canvas.scaleFactor;

		beatCursor = GameObject.Find("BeatCursor");
		beatCursorPosition = beatStartPosition;

		GameObject beatPoint;
		Vector3 beatPointPosition = beatStartPosition;
		float beatPointOffset = beatPanelWidth / numberOfBeats;

        for (int i = 0; i < numberOfBeats; i++)
        {
			beatPoint = (GameObject)Instantiate(beatPointPrefab);
			beatPoint.transform.localPosition = beatPointPosition;
			beatPoint.transform.SetParent(gameObject.transform, false);
			beatPoints.Add(beatPoint);

			beatPointPosition.x += beatPointOffset;
        }

        timer = 0; 
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		beatCursor.transform.localPosition  = beatCursorPosition;
		beatCursorPosition.x += (beatPanelWidth / loopDuration) * timer + beatStartPosition.x;

        // start new loop if loop duration has been reached
        if (timer > loopDuration)
        {
            // reset timer to overstepped milliseconds
            timer %= loopDuration;
            beatCursorPosition.x = (beatPanelWidth / loopDuration) * timer + beatStartPosition.x;
        }
	}
}
