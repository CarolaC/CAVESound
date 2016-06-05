using UnityEngine;
using System.Collections;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

// plays the loop notes (midi)
public class MidiPlayer : MonoBehaviour {

    private StreamSynthesizer midiStreamSynthesizer; 
    private float[] sampleBuffer;
    private float gain = 1f;
    private int loopNoteVolume = 100;

	// Use this for initialization
	void Awake () {
        AudioPlayer player = GameObject.Find("Listener").GetComponent<AudioPlayer>();
        midiStreamSynthesizer = player.midiStreamSynthesizer;
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void Play(LoopNote note)
    {
        StartCoroutine(LoopNoteCoroutine(note));
    }
    
    IEnumerator LoopNoteCoroutine(LoopNote note)
    {
        midiStreamSynthesizer.NoteOn(1, note.pitch, loopNoteVolume, note.instrument);
        StartCoroutine(note.LightFlashCoroutine());
        yield return new WaitForSeconds(1);
        midiStreamSynthesizer.NoteOff(1, note.pitch);
    }

    public void Stop(LoopNote note)
    {
        midiStreamSynthesizer.NoteOff(1, note.pitch);
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
