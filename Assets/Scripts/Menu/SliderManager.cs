using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderManager : MonoBehaviour {
	
	private AudioClip[] audioFiles;
	public bool playMidi;
	private Slider slider;
	[HideInInspector]
	public bool finishedLoading = false;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();

		if (playMidi) {
			slider.minValue = 0;
			slider.maxValue = 127;
		}
		else
			StartCoroutine (LoadAudioFiles());
	}

	IEnumerator LoadAudioFiles()
	{
		audioFiles = Resources.LoadAll<AudioClip> ("Audio/Instruments");
		slider.minValue = 0;
		slider.maxValue = audioFiles.Length - 1;
		finishedLoading = true;
		yield return null;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public string GetInstrumentName(int number)
	{
		if (finishedLoading)
			return audioFiles [number].name;
		else
			return null;
	}

	public AudioClip GetAudioFile(int number)
	{
		if (finishedLoading)
			return audioFiles [number];
		else
			return null;
	}
}
