using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundAreaVisualizer : MonoBehaviour {

    public Canvas canvas;
    public GameObject soundAreaPanelPrefab;
    [HideInInspector]
    public List<GameObject> soundAreaPanels = new List<GameObject>();

	// Use this for initialization
	void Start () {
        int soundAreaCount = PlayerPrefs.GetInt("NumSoundAreas");

        GameObject soundAreaPanel;

        // create and order new sound area panels
        for (int i = 0; i < soundAreaCount; i++)
        {
            soundAreaPanel = (GameObject)Instantiate(soundAreaPanelPrefab, Vector3.zero, Quaternion.identity);
            soundAreaPanel.transform.localScale = new Vector3(1, 1, 1);
            soundAreaPanel.transform.localPosition = Vector3.zero;
            soundAreaPanel.transform.SetParent(gameObject.transform);
            soundAreaPanels.Add(soundAreaPanel);
        }        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
