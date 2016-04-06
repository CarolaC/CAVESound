using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundDirectionManager : MonoBehaviour {

    private CaveRectUtility caveRectUtil;
    private GameObject listener;
    private GameObject secondTarget;
    private List<GameObject> walls = new List<GameObject>();
    private LineRenderer lineRenderer;

    [HideInInspector]
    public Vector3 soundDirection;

	// Use this for initialization
	void Start () {
        caveRectUtil = GameObject.Find("CaveRect").GetComponent<CaveRectUtility>();
        listener = GameObject.Find("Listener");
        secondTarget = GameObject.Find("SecondTarget");

        walls.Add(GameObject.Find("Wall1"));
        walls.Add(GameObject.Find("Wall2"));
        walls.Add(GameObject.Find("Wall3"));

        lineRenderer = listener.GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.red);
        lineRenderer.SetWidth(0.2f, 0.2f);
        lineRenderer.SetVertexCount(3);
	}
	
	// Update is called once per frame
	void Update () {
        // calculate line
        Vector3 direction = secondTarget.transform.position - listener.transform.position;
        Vector3 pointTo = secondTarget.transform.position + direction;

        // update linerenderer points
        lineRenderer.SetPosition(0, listener.transform.position);
        lineRenderer.SetPosition(1, secondTarget.transform.position);
        lineRenderer.SetPosition(2, pointTo);

        // detect collision with sound sphere
        RaycastHit hit;
        if (Physics.Raycast(secondTarget.transform.position, direction, out hit))
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (hit.transform.gameObject.name == walls[i].name)
                {
                    print("Raycast hit " + hit.transform.gameObject.name + " at " + hit.point);
                    soundDirection = hit.point;
                }
            }
        }
	}
}
