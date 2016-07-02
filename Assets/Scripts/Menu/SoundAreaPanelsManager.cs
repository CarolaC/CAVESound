using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundAreaPanelsManager : MonoBehaviour {

    public Dropdown numSoundAreasDropDown;
    public GameObject soundAreaPanelPrefab;
    [HideInInspector]
    public List<GameObject> soundAreaPanels = new List<GameObject>();

    private DragInstrument dragger;
	private SliderManager sliderManager;

   
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start () {
        // trigger the creation of sound area panels on startup
        NumOfSoundAreasChanged(numSoundAreasDropDown.value);

		sliderManager = GameObject.Find ("InstrumentsSlider").GetComponent<SliderManager> ();
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

		if (sliderManager.playMidi) {
			for (int i = 0; i < soundAreaPanels.Count; i++) {
				PlayerPrefs.SetInt ("Instrument" + i, soundAreaPanels [i].GetComponent<Instrument> ().midiInstrument);
			}
		} else {	
			for (int i = 0; i < soundAreaPanels.Count; i++) {
				PlayerPrefs.SetInt ("Instrument" + i, soundAreaPanels [i].GetComponent<Instrument> ().audioFileNumber);
				PlayerPrefs.SetString ("InstrumentName" + i, soundAreaPanels [i].GetComponent<Instrument> ().GetInstrumentName ());
			}
		}
    }
}
