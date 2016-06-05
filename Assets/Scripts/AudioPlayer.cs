using UnityEngine;
using System.Collections;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

// plays the selection sounds (based on sound area and pitch range) and the loop notes (audio files)
public class AudioPlayer : MonoBehaviour {
    
    [HideInInspector]
	public StreamSynthesizer midiStreamSynthesizer;
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
            activeSoundArea = soundAreaSelector.activeSoundArea;
            activePitch = pitchRangeSelector.activePitch;

            StartCoroutine(MidiNoteCoroutine());
        }
	}

    private IEnumerator MidiNoteCoroutine()
    {
        // the active pitch can change during this coroutine, so save it first
        int tempPitch = minPitch + activePitch;
        midiStreamSynthesizer.NoteOn(1, tempPitch, chooseNoteVolume, soundAreaSelector.activeInstrument);
        //print("Played note with instrument " + soundAreaSelector.activeInstrument + " and pitch " + activePitch);
        yield return new WaitForSeconds(0.5f);
        midiStreamSynthesizer.NoteOff(1, tempPitch);
    }

    public void PlayLoopNote(LoopNote note)
    {
        note.PlayAudio();
        StartCoroutine(note.LightFlashCoroutine());
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
