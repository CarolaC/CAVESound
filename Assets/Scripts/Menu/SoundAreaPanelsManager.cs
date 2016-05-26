using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundAreaPanelsManager : MonoBehaviour {

    public Dropdown numSoundAreasDropDown;
    public GameObject soundAreaPanelPrefab;
    private List<GameObject> soundAreaPanels = new List<GameObject>();

    private DragInstrument dragger;

   
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start () {
        // trigger the creation of sound area panels on startup
        NumOfSoundAreasChanged(numSoundAreasDropDown.value);

        GameObject soundAreaDragPanel = GameObject.Find("SoundAreaDragPanel");
        dragger = soundAreaDragPanel.GetComponent<DragInstrument>();
	}
	
    // user has chosen number of sound areas in dropdown
    public void NumOfSoundAreasChanged(int index)
    {
        int number = int.Parse(numSoundAreasDropDown.captionText.text);

        soundAreaPanels.ForEach((panel) => GameObject.Destroy(panel));
        soundAreaPanels.Clear();
        GameObject soundAreaPanel;

        // create and order new sound area panels
        for (int i = 0; i < number; i++)
        {
            soundAreaPanel = (GameObject)Instantiate(soundAreaPanelPrefab, Vector3.zero, Quaternion.identity);
            soundAreaPanel.transform.localScale = new Vector3(1, 1, 1);
            soundAreaPanel.transform.localPosition = Vector3.zero;
            soundAreaPanel.transform.SetParent(gameObject.transform);
            soundAreaPanels.Add(soundAreaPanel);
        }
    }

    public void SaveInstrumentSettings()
    {
        PlayerPrefs.SetInt("NumSoundAreas", soundAreaPanels.Count);

        for (int i = 0; i < soundAreaPanels.Count; i++)
        {
            PlayerPrefs.SetInt("Instrument" + i, soundAreaPanels[i].GetComponent<Instrument>().midiInstrument);
            PlayerPrefs.SetFloat("Instrument" + i + "Red", soundAreaPanels[i].GetComponent<Instrument>().color.r);
            PlayerPrefs.SetFloat("Instrument" + i + "Green", soundAreaPanels[i].GetComponent<Instrument>().color.g);
            PlayerPrefs.SetFloat("Instrument" + i + "Blue", soundAreaPanels[i].GetComponent<Instrument>().color.b);
        }
    }
}
