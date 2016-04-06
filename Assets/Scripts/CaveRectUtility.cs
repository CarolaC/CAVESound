using UnityEngine;
using System.Collections;

public class CaveRectUtility : MonoBehaviour {

    public Vector2 caveTopLeftCorner;
    public Vector2 caveBottomRightCorner;

    [HideInInspector]
    public Rect caveRect;

	// Use this for initialization
	void Awake () {
        caveRect = Rect.MinMaxRect(caveTopLeftCorner.x, caveTopLeftCorner.y, caveBottomRightCorner.x, caveBottomRightCorner.y);
        print("CAVE Rect: " + caveRect);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
