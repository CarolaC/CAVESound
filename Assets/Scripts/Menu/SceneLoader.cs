using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour {

    private SoundAreaPanelsManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("SoundAreas").GetComponent<SoundAreaPanelsManager>();
    }

    public void StartButtonClickHandler()
    {
        manager.SaveInstrumentSettings();
        SceneManager.LoadScene ("CaveSoundMain");
    }

    public void RandomButtonClickHandler()
    {
        for (int i = 0; i < manager.soundAreaPanels.Count; i++)
        {
            manager.soundAreaPanels[i].GetComponent<Instrument>().InstrumentChanged(Random.Range(0, 127));
        }
    }
}
