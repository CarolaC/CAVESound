using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// divides cave into soundareas and returns the active sound area
public class SoundAreaSelector : MonoBehaviour {

    public Transform listenerTransform;
	public GameObject soundAreaFloorPrefab;

    private CaveRectUtility caveRectUtil;
    private SoundAreaVisualizer soundAreaVisualizer;
    private List<Rect> soundAreas = new List<Rect>();
    private List<GameObject> soundAreaFloors = new List<GameObject>();
    private List<GameObject> soundAreaPanels = new List<GameObject>();
	
    private List<int> instruments = new List<int>();
    private List<Color> colors = new List<Color>();

    [HideInInspector]
    public int activeSoundArea;
    [HideInInspector]
    public int activeInstrument;
    [HideInInspector]
    public Color activeColor;
    [HideInInspector]
    public int soundAreaCount;

    public Text instrumentText;
    public GameObject testLight;

	// Use this for initialization
    void Start()
    {
        caveRectUtil = GameObject.Find("CaveRect").GetComponent<CaveRectUtility>();
        soundAreaVisualizer = GameObject.Find("SoundAreasPanel").GetComponent<SoundAreaVisualizer>();

        instrumentText.text = "";
        soundAreaCount = PlayerPrefs.GetInt("NumSoundAreas");
        soundAreaPanels = soundAreaVisualizer.soundAreaPanels;

        if (soundAreaCount == 0)
            soundAreaCount = 4;

        colors.Add(new Color32(255, 255, 145, 255));
        colors.Add(new Color32(255, 225, 145, 255));
        colors.Add(new Color32(255, 195, 145, 255));
        colors.Add(new Color32(255, 165, 145, 255));
        colors.Add(new Color32(255, 135, 145, 255));
        colors.Add(new Color32(255, 105, 145, 255));
        colors.Add(new Color32(255, 75, 145, 255));
        colors.Add(new Color32(255, 45, 145, 255));

        // get instruments that the user chose in the menu
        for (int i = 0; i < soundAreaCount; i++)
        {
            instruments.Add(PlayerPrefs.GetInt("Instrument" + i));
        }

		// divide the cave rect into sound areas
        divideCaveRect(caveRectUtil.caveRect);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < soundAreas.Count; i++)
        {
            if (soundAreas[i].Contains(new Vector2(listenerTransform.position.x, listenerTransform.position.z), true))
            {
                if (activeSoundArea != i)
                {
					soundAreaFloors[activeSoundArea].GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    soundAreaPanels[activeSoundArea].GetComponent<Image>().color = new Color(1, 1, 1);
                    activeSoundArea = i;
                    activeInstrument = instruments[activeSoundArea];
                    activeColor = colors[activeSoundArea];
                    soundAreaFloors[activeSoundArea].GetComponent<Renderer>().material.color = activeColor;
                    soundAreaPanels[activeSoundArea].GetComponent<Image>().color = activeColor;
                    print("Listener in Area " + activeSoundArea);
                    StartCoroutine(testLightCoroutine());
                    instrumentText.text = "" + activeInstrument;
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

    private IEnumerator testLightCoroutine()
    {
        testLight.GetComponent<Light>().range = 5;
        yield return new WaitForSeconds(0.2f);
        testLight.GetComponent<Light>().range = 2;
    }

}
