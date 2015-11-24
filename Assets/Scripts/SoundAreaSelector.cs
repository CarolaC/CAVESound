using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// divides cave into soundareas and returns the active sound area
public class SoundAreaSelector : MonoBehaviour {

    public Transform listenerTransform;
    public Vector2 caveTopLeftCorner;
    public Vector2 caveBottomRightCorner;
    public int soundAreaCount = 4;

    private List<Rect> soundAreas = new List<Rect>();
    [HideInInspector]
    public int activeSoundArea;

	// Use this for initialization
	void Start () {
        Rect caveRect = Rect.MinMaxRect(caveTopLeftCorner.x, caveTopLeftCorner.y, caveBottomRightCorner.x, caveBottomRightCorner.y);
        print("CAVE Rect: " + caveRect);

        divideRect(caveRect);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < soundAreas.Count; i++)
        {
            if (soundAreas[i].Contains(new Vector2(listenerTransform.position.x, listenerTransform.position.z), true))
            {
                activeSoundArea = i + 1;
                print("Listener in Area " + i);
            }
        }
	}
    
    // divide rect into sound areas
    void divideRect(Rect caveRect)
    {
        if (soundAreaCount == 4)
        {
            soundAreas.Add(new Rect(caveRect.x, caveRect.y, caveRect.width * 0.5f, caveRect.height * 0.5f));
            soundAreas.Add(new Rect(caveRect.x + (caveRect.width * 0.5f), caveRect.y, caveRect.width * 0.5f, caveRect.height * 0.5f));
            soundAreas.Add(new Rect(caveRect.x, caveRect.y + (caveRect.height * 0.5f), caveRect.width * 0.5f, caveRect.height * 0.5f));
            soundAreas.Add(new Rect(caveRect.center.x, caveRect.center.y, caveRect.width * 0.5f, caveRect.height * 0.5f));

            for (int i = 0; i < soundAreas.Count; i++)
            {
                print("Sound Area " + i + ": " + soundAreas[i]);
            }
        }
    }

}
