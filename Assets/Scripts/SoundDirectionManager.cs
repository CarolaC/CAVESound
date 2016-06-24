using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// sends a raycast from the targets to the wall and returns the wall hit point
public class SoundDirectionManager : MonoBehaviour {

    public float soundLightHeight;
    private CaveRectUtility caveRectUtil;
    private GameObject listener;
    private GameObject secondTarget;
    private List<GameObject> walls = new List<GameObject>();
    private LineRenderer lineRenderer;
    private GameObject laserPointer;
    private bool lineRendererActive = true;

    [HideInInspector]
    public Vector3 soundDirection;

	// Use this for initialization
	void Start () {
        caveRectUtil = GameObject.Find("CaveRect").GetComponent<CaveRectUtility>();
        listener = GameObject.Find("Listener");
        secondTarget = GameObject.Find("SecondTarget");
        laserPointer = GameObject.Find("LaserPointer");

        walls.Add(GameObject.Find("Wall1"));
        walls.Add(GameObject.Find("Wall2"));
        walls.Add(GameObject.Find("Wall3"));

        lineRenderer = listener.GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.red);
        lineRenderer.SetWidth(0.02f, 0.02f);
        lineRenderer.SetVertexCount(3);
	}

    public void ToggleLineRenderer(bool active)
    {
        lineRendererActive = active;
        lineRenderer.enabled = active;
    }

	// Update is called once per frame
	void Update () {
        // calculate line
        Vector3 direction = secondTarget.transform.position - listener.transform.position;
        Vector3 pointTo = secondTarget.transform.position + 2 * direction;

        if (lineRendererActive)
        {
            // update linerenderer points
            lineRenderer.SetPosition(0, listener.transform.position);
            lineRenderer.SetPosition(1, secondTarget.transform.position);
            lineRenderer.SetPosition(2, pointTo);
        }

        // detect collision with wall
        RaycastHit hit;
        if (Physics.Raycast(secondTarget.transform.position, direction, out hit))
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (hit.transform.gameObject.name == walls[i].name)
                {
                   //print("Raycast hit " + hit.transform.gameObject.name + " at " + hit.point);

					// set hit point a bit closer to listener so that the light becomes visible in front of the wall
					Vector3 newHitPoint;
					if (walls [i].name.Equals ("Wall1"))
						newHitPoint = new Vector3 (hit.point.x + 0.1f, hit.point.y, hit.point.z);
					else if (walls [i].name.Equals ("Wall2"))
						newHitPoint = new Vector3 (hit.point.x, hit.point.y, hit.point.z - 0.1f);
					else if (walls [i].name.Equals ("Wall3"))
						newHitPoint = new Vector3 (hit.point.x - 0.1f, hit.point.y, hit.point.z);
					else
						newHitPoint = hit.point;
					
                    soundDirection.x = newHitPoint.x;
                    soundDirection.y = newHitPoint.y;
                    soundDirection.z = newHitPoint.z;
                    laserPointer.transform.position = newHitPoint;
                }
            }
        }
	}
}
