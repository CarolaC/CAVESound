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
        lineRenderer.SetColors(Color.white, Color.white);
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.SetVertexCount(3);
	}
	
	// Update is called once per frame
	void Update () {
        // calculate line
        Vector3 direction = secondTarget.transform.position - listener.transform.position;
        Vector3 pointTo = secondTarget.transform.position + 2 * direction;

        // update linerenderer points
        lineRenderer.SetPosition(0, listener.transform.position);
        lineRenderer.SetPosition(1, secondTarget.transform.position);
        lineRenderer.SetPosition(2, pointTo);

        // detect collision with wall
        RaycastHit hit;
        if (Physics.Raycast(secondTarget.transform.position, direction, out hit))
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (hit.transform.gameObject.name == walls[i].name)
                {
                    print("Raycast hit " + hit.transform.gameObject.name + " at " + hit.point);

                    // set hit point a bit closer to listener so that the light becomes visible in front of the wall
                    Vector3 distanceToTarget = hit.point - secondTarget.transform.position;
                    Vector3 shortenedDistanceToTarget = new Vector3(distanceToTarget.x * 0.1f, distanceToTarget.y * 0.1f, distanceToTarget.z * 0.1f);
                    Vector3 newHitPoint = hit.point - shortenedDistanceToTarget;
                    soundDirection.x = newHitPoint.x;
                    soundDirection.y = newHitPoint.y;
                    soundDirection.z = newHitPoint.z;
                }
            }
        }
	}
}
