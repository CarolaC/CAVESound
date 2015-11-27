using UnityEngine;
using System.Collections;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

// plays the sound of the instruments based on the sound area and pitch range selection
public class AudioPlayer : MonoBehaviour {
    
    private StreamSynthesizer midiStreamSynthesizer;
    private string bankFilePath = "GM Bank/gm";
    private int bufferSize = 1024;
    private int midiNoteVolume = 100;
    private int minPitch = 60;
    private int minInstrument = 44;
    private float[] sampleBuffer;
    private float gain = 1f;

    private SoundAreaSelector soundAreaSelector;
    private PitchRangeSelector pitchRangeSelector;
    private int activeSoundArea;
    private int activePitch;

	// Use this for initialization
	void Start () {
        midiStreamSynthesizer = new StreamSynthesizer(44100, 2, bufferSize, 40);
        midiStreamSynthesizer.LoadBank(bankFilePath);
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		

        soundAreaSelector = GameObject.Find("SoundSelectors").GetComponent<SoundAreaSelector>();
        pitchRangeSelector = GameObject.Find("SoundSelectors").GetComponent<PitchRangeSelector>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((soundAreaSelector.activeSoundArea != activeSoundArea) || (pitchRangeSelector.activePitch != activePitch))
        {
            midiStreamSynthesizer.NoteOff(1, minPitch + activePitch);

            activeSoundArea = soundAreaSelector.activeSoundArea;
            activePitch = pitchRangeSelector.activePitch;
            midiStreamSynthesizer.NoteOn(1, minPitch + activePitch, midiNoteVolume, minInstrument + activeSoundArea);
            print("Played note with instrument " + activeSoundArea + " and pitch " + activePitch);
        }
	}

    // this function plays the audio data (code from UnitySynthTest.cs)
    // See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
    // If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
    private void OnAudioFilterRead(float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        midiStreamSynthesizer.GetNext(sampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = sampleBuffer[i] * gain;
        }
    }
}
