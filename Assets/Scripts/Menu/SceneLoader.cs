using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour {

    private SoundAreaPanelsManager soundAreaPanelsManager;

    // Use this for initialization
    void Start()
    {
		soundAreaPanelsManager = GameObject.Find("SoundAreas").GetComponent<SoundAreaPanelsManager>();
    }

    public void StartButtonClickHandler()
    {
		soundAreaPanelsManager.SaveInstrumentSettings();
        SceneManager.LoadScene ("CaveSoundMain");
    }

    public void RandomButtonClickHandler()
    {
		Slider slider = GameObject.Find ("InstrumentsSlider").GetComponent<Slider> ();

		for (int i = 0; i < soundAreaPanelsManager.soundAreaPanels.Count; i++)
        {
			soundAreaPanelsManager.soundAreaPanels[i].GetComponent<Instrument>().InstrumentChanged(Random.Range(slider.minValue, slider.maxValue));
        }
    }
}
