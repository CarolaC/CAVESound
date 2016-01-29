using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// divides cave into soundareas and returns the active sound area
public class SoundAreaSelector : MonoBehaviour {

    public Transform listenerTransform;
    public Vector2 caveTopLeftCorner;
    public Vector2 caveBottomRightCorner;
    public GameObject soundAreaPrefab;

    private List<Rect> soundAreas = new List<Rect>();
    private List<GameObject> soundAreaPanels = new List<GameObject>();
    private List<int> instruments = new List<int>();

    [HideInInspector]
    public int activeSoundArea;
    [HideInInspector]
    public int activeInstrument;
    [HideInInspector]
    public int soundAreaCount;

	// Use this for initialization
	void Start () {
        Rect caveRect = Rect.MinMaxRect(caveTopLeftCorner.x, caveTopLeftCorner.y, caveBottomRightCorner.x, caveBottomRightCorner.y);
        print("CAVE Rect: " + caveRect);

        soundAreaCount = PlayerPrefs.GetInt("NumSoundAreas");
        
        if (soundAreaCount == 0)
            soundAreaCount = 4;

        // get instruments that the user chose in the menu
        for (int i = 0; i < soundAreaCount; i++)
        {
            instruments.Add(PlayerPrefs.GetInt("Instrument" + i));
        }

        divideRect(caveRect);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < soundAreas.Count; i++)
        {
            if (soundAreas[i].Contains(new Vector2(listenerTransform.position.x, listenerTransform.position.z), true))
            {
                soundAreaPanels[activeSoundArea].GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                activeSoundArea = i;
                activeInstrument = instruments[activeSoundArea];
                soundAreaPanels[activeSoundArea].GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                print("Listener in Area " + activeSoundArea);
            }
        }
	}
    
    // divide rect into sound areas
    void divideRect(Rect caveRect)
    {
        if (soundAreaCount == 4) 
		{
			soundAreas.Add (new Rect (caveRect.x, caveRect.y, caveRect.width * 0.5f, caveRect.height * 0.5f));
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.5f), caveRect.y, caveRect.width * 0.5f, caveRect.height * 0.5f));
			soundAreas.Add (new Rect (caveRect.x, caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.5f, caveRect.height * 0.5f));
            soundAreas.Add(new Rect(caveRect.center.x, caveRect.center.y, caveRect.width * 0.5f, caveRect.height * 0.5f));
		} 
		else if (soundAreaCount == 8) 
		{
			soundAreas.Add (new Rect (caveRect.x, caveRect.y, caveRect.width * 0.25f, caveRect.height * 0.25f));
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.25f), caveRect.y, caveRect.width * 0.25f, caveRect.height * 0.25f));
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.5f), caveRect.y, caveRect.width * 0.25f, caveRect.height * 0.25f));			
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.75f), caveRect.y, caveRect.width * 0.25f, caveRect.height * 0.25f));
			soundAreas.Add (new Rect (caveRect.x, caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.25f, caveRect.height * 0.25f));	
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.25f), caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.25f, caveRect.height * 0.25f));			
			soundAreas.Add (new Rect (caveRect.x + (caveRect.width * 0.5f), caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.25f, caveRect.height * 0.25f));
            soundAreas.Add(new Rect(caveRect.x + (caveRect.width * 0.75f), caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.25f, caveRect.height * 0.25f));
		}

        GameObject soundAreaPanel;
        for (int i = 0; i < soundAreas.Count; i++)
        {
            print("Sound Area " + i + ": " + soundAreas[i]);
            soundAreaPanel = (GameObject)Instantiate(soundAreaPrefab, new Vector3(soundAreas[i].x, 0, soundAreas[i].y), Quaternion.identity);
            soundAreaPanel.transform.localScale = new Vector3(soundAreas[i].width * 2, 0.1f, soundAreas[i].height * 2);
            soundAreaPanels.Add(soundAreaPanel);
        }
    }

}
