using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// divides cave into soundareas and returns the active sound area
public class SoundAreaSelector : MonoBehaviour {

    public Transform listenerTransform;
	public GameObject soundAreaFloorPrefab;
	public Canvas canvas;
	public GameObject soundAreasPanel;
	public GameObject soundAreaPanelPrefab;

    private CaveRectUtility caveRectUtil;
    private List<Rect> soundAreas = new List<Rect>();
	private List<Rect> canvasSoundAreas = new List<Rect>();
    private List<GameObject> soundAreaFloors = new List<GameObject>();
	private List<GameObject> soundAreaPanels = new List<GameObject>();
    private List<int> instruments = new List<int>();

    [HideInInspector]
    public int activeSoundArea;
    [HideInInspector]
    public int activeInstrument;
    [HideInInspector]
    public int soundAreaCount;

    public Text instrumentText;

    public GameObject testLight;

	// Use this for initialization
    void Start()
    {
        caveRectUtil = GameObject.Find("CaveRect").GetComponent<CaveRectUtility>();
        instrumentText.text = "";
        soundAreaCount = PlayerPrefs.GetInt("NumSoundAreas");
        
        if (soundAreaCount == 0)
            soundAreaCount = 4;

        // get instruments that the user chose in the menu
        for (int i = 0; i < soundAreaCount; i++)
        {
            instruments.Add(PlayerPrefs.GetInt("Instrument" + i));
        }

		// divide the cave rect into sound areas
        divideCaveRect(caveRectUtil.caveRect);
		// divide the panel on the canvas into sound areas
		dividePanelRect(soundAreasPanel.GetComponent<RectTransform>().rect);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < soundAreas.Count; i++)
        {
            if (soundAreas[i].Contains(new Vector2(listenerTransform.position.x, listenerTransform.position.z), true))
            {
                if (activeSoundArea != i)
                {
					soundAreaFloors[activeSoundArea].GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                    activeSoundArea = i;
                    activeInstrument = instruments[activeSoundArea];
					soundAreaFloors[activeSoundArea].GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                    print("Listener in Area " + activeSoundArea);
                    StartCoroutine(testLightCoroutine());
                    instrumentText.text = "Instrument " + activeInstrument;
                }
            }
        }
	}
    
    // divide the cave rect into sound areas
	void divideCaveRect(Rect rect)
    {
        if (soundAreaCount == 4) 
		{
			soundAreas.Add (new Rect (rect.x, rect.y, rect.width * 0.5f, rect.height * 0.5f));
			soundAreas.Add (new Rect (rect.x + (rect.width * 0.5f), rect.y, rect.width * 0.5f, rect.height * 0.5f));
			soundAreas.Add (new Rect (rect.x, rect.y + (rect.height * 0.5f), rect.width * 0.5f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.center.x, rect.center.y, rect.width * 0.5f, rect.height * 0.5f));
		}
        else if (soundAreaCount == 6)
        {
			soundAreas.Add(new Rect(rect.x, rect.y, rect.width * 0.33f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.33f), rect.y, rect.width * 0.33f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.66f), rect.y, rect.width * 0.33f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x, rect.y + (rect.height * 0.5f), rect.width * 0.33f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.33f), rect.y + (rect.height * 0.5f), rect.width * 0.33f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.66f), rect.y + (rect.height * 0.5f), rect.width * 0.33f, rect.height * 0.5f));
        }
        else if (soundAreaCount == 8)
        {
			soundAreas.Add(new Rect(rect.x, rect.y, rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.25f), rect.y, rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.5f), rect.y, rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.75f), rect.y, rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x, rect.y + (rect.height * 0.5f), rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.25f), rect.y + (rect.height * 0.5f), rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.5f), rect.y + (rect.height * 0.5f), rect.width * 0.25f, rect.height * 0.5f));
			soundAreas.Add(new Rect(rect.x + (rect.width * 0.75f), rect.y + (rect.height * 0.5f), rect.width * 0.25f, rect.height * 0.5f));
        }

		// create sound area floors
		GameObject soundAreaFloor;
		for (int i = 0; i < soundAreas.Count; i++) {
			print ("Sound Area " + i + ": " + soundAreas [i]);
			soundAreaFloor = (GameObject)Instantiate (soundAreaFloorPrefab, new Vector3 (soundAreas[i].center.x, 0, soundAreas[i].center.y), Quaternion.identity);
			soundAreaFloor.transform.localScale = new Vector3 (soundAreas [i].width, 0.1f, soundAreas[i].height);
			soundAreaFloors.Add (soundAreaFloor);
		}
    }

	void dividePanelRect(Rect rect)
	{
		float width = rect.width * canvas.scaleFactor;
		float height = rect.height * canvas.scaleFactor;

		if (soundAreaCount == 4) 
		{
			canvasSoundAreas.Add (new Rect (rect.x, rect.y, width * 0.5f, height * 0.5f));
			canvasSoundAreas.Add (new Rect (rect.x + (width * 0.5f), rect.y, width * 0.5f, height * 0.5f));
			canvasSoundAreas.Add (new Rect (rect.x, rect.y + (height * 0.5f), width * 0.5f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.center.x, rect.center.y, width * 0.5f, height * 0.5f));
		}
		else if (soundAreaCount == 6)
		{
			canvasSoundAreas.Add(new Rect(rect.x, rect.y, width * 0.33f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.33f), rect.y, width * 0.33f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.66f), rect.y, width * 0.33f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x, rect.y + (rect.height * 0.5f), width * 0.33f, rect.height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.33f), rect.y + (height * 0.5f), width * 0.33f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.66f), rect.y + (height * 0.5f), width * 0.33f, height * 0.5f));
		}
		else if (soundAreaCount == 8)
		{
			canvasSoundAreas.Add(new Rect(rect.x, rect.y, width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.25f), rect.y, width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.5f), rect.y, width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.75f), rect.y, width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x, rect.y + (height * 0.5f), width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.25f), rect.y + (height * 0.5f), width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.5f), rect.y + (height * 0.5f), width * 0.25f, height * 0.5f));
			canvasSoundAreas.Add(new Rect(rect.x + (width * 0.75f), rect.y + (height * 0.5f), width * 0.25f, height * 0.5f));
		}

		GameObject soundAreaPanel;
		for (int i = 0; i < canvasSoundAreas.Count; i++) {
			soundAreaPanel = (GameObject)Instantiate (soundAreaPanelPrefab);
			soundAreaPanel.transform.SetParent(soundAreasPanel.transform, false);
			soundAreaPanel.transform.localPosition = new Vector3 (canvasSoundAreas[i].x, canvasSoundAreas[i].y, 0);
			float panelWidth = soundAreaPanel.GetComponent<RectTransform> ().rect.width;
			panelWidth = canvasSoundAreas[i].width;
			float panelHeight = soundAreaPanel.GetComponent<RectTransform> ().rect.height;
			panelHeight = canvasSoundAreas[i].height;
			soundAreaPanels.Add(soundAreaPanel);
		}
	}

    private IEnumerator testLightCoroutine()
    {
        testLight.GetComponent<Light>().range = 5;
        yield return new WaitForSeconds(0.2f);
        testLight.GetComponent<Light>().range = 2;
    }

}
