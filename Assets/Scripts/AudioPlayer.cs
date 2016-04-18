﻿using UnityEngine;
using System.Collections;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

// plays the selection sounds (based on sound area and pitch range) and the loop notes
public class AudioPlayer : MonoBehaviour {
    
	private StreamSynthesizer midiStreamSynthesizer;
    private string bankFilePath = "GM Bank/gm";
    private int bufferSize = 1024;
    private int chooseNoteVolume = 30;
    private int loopNoteVolume = 100;
    private int minPitch = 60;
    private float[] sampleBuffer;
    private float gain = 1f;

    private SoundAreaSelector soundAreaSelector;
    private PitchRangeSelector pitchRangeSelector;
    private int activeSoundArea;
    private int activePitch;

	// Awake is called when the script instance
	// is being loaded.
	void Awake ()
	{
        midiStreamSynthesizer = new StreamSynthesizer(44100, 2, bufferSize, 40);
        midiStreamSynthesizer.LoadBank(bankFilePath);
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		

        soundAreaSelector = GameObject.Find("SoundManagers").GetComponent<SoundAreaSelector>();
		pitchRangeSelector = GameObject.Find("SoundManagers").GetComponent<PitchRangeSelector>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((soundAreaSelector.activeSoundArea != activeSoundArea) || (pitchRangeSelector.activePitch != activePitch))
        {
            midiStreamSynthesizer.NoteOff(1, minPitch + activePitch);

            activeSoundArea = soundAreaSelector.activeSoundArea;
            activePitch = pitchRangeSelector.activePitch;
            midiStreamSynthesizer.NoteOn(1, minPitch + activePitch, chooseNoteVolume, soundAreaSelector.activeInstrument);
            print("Played note with instrument " + soundAreaSelector.activeInstrument + " and pitch " + activePitch);
        }
	}

    public void PlayLoopNote(LoopNote note)
    {
        StartCoroutine(LoopNoteCoroutine(note));     
    }

    IEnumerator LoopNoteCoroutine(LoopNote note)
    {
        note.Play();
		StartCoroutine(note.LightFlashCoroutine());
        yield return null;

        //midiStreamSynthesizer.NoteOn(1, note.pitch, loopNoteVolume, note.instrument);
        //yield return new WaitForSeconds(1);
        //midiStreamSynthesizer.NoteOff(1, note.pitch);
    }

    // this function plays the audio data (code from UnitySynthTest.cs)
    // See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
    // If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
    /*private void OnAudioFilterRead(float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        midiStreamSynthesizer.GetNext(sampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = sampleBuffer[i] * gain;
        }
    }*/
}
