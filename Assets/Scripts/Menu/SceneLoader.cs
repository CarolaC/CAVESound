using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    private SoundAreaPanelsManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("SoundAreas").GetComponent<SoundAreaPanelsManager>();
    }

    public void MenuClickHandler()
    {
        manager.SaveInstrumentSettings();
        SceneManager.LoadScene ("CaveSoundMain");
    }
}
