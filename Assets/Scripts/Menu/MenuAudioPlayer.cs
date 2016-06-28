using UnityEngine;
using System.Collections;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

public class MenuAudioPlayer : MonoBehaviour {
	
    private StreamSynthesizer midiStreamSynthesizer;
    private string bankFilePath = "GM Bank/gm";
    private int bufferSize = 1024;
    private int midiNoteVolume = 100;
    private int midiPitch = 60;
    private float[] sampleBuffer;
    private float gain = 1f;
	private SliderManager sliderManager;
	private AudioSource audioSource;
	private bool playMidi;
    
	// Use this for initialization
	void Awake () {
		sliderManager = GameObject.Find("InstrumentsSlider").GetComponent<SliderManager> ();
		playMidi = sliderManager.playMidi;
		audioSource = GetComponent<AudioSource> ();
        midiStreamSynthesizer = new StreamSynthesizer(44100, 2, bufferSize, 40);
        midiStreamSynthesizer.LoadBank(bankFilePath);
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];	
	}
	
    public void InstrumentChanged(float number)
    {
		if (playMidi)
			StartCoroutine (PlayInstrument (number));
		else {
			if (sliderManager.finishedLoading) {
				audioSource.clip = sliderManager.GetAudioFile (Mathf.RoundToInt (number));
				audioSource.Play ();
			}
		}       
    }

    IEnumerator PlayInstrument(float number)
    {
        int instrument = Mathf.RoundToInt(number);
        Debug.Log ("note on - pitch " + midiPitch + " volume " + midiNoteVolume + " instrument " + instrument);
        midiStreamSynthesizer.NoteOn(1, midiPitch, midiNoteVolume, instrument);
        yield return new WaitForSeconds(1);
        midiStreamSynthesizer.NoteOff(1, midiPitch);
    }

    // this function plays the audio data (code from UnitySynthTest.cs)
    // See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
    // If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
    private void OnAudioFilterRead(float[] data, int channels)
    {
		if (playMidi) {
			//This uses the Unity specific float method we added to get the buffer
			midiStreamSynthesizer.GetNext (sampleBuffer);

			for (int i = 0; i < data.Length; i++) {
				data [i] = sampleBuffer [i] * gain;
			}
		}
    }
}
