using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

// manages the loop cycle, creates new loop notes and adds them to the loop
public class LoopManager : MonoBehaviour {

	public float loopDuration;     // in seconds
    public int numberOfBeats;

	private SoundAreaSelector soundAreaSelector;
	private PitchRangeSelector pitchRangeSelector;
    private SoundDirectionManager soundDirectionManager;
    private AudioPlayer audioPlayer;
	private SimulateRotation simulator;
	private SetNoteManager setNoteManager;
    private List<LoopNote> loopNotes = new List<LoopNote>();
    private List<LoopNote> pendingNotes = new List<LoopNote>();
    private List<GameObject> beatPoints = new List<GameObject>();
	private AudioClip[] beatInstruments;
    private float timer;
    private int noteIndex;
    private LoopNote currentNote;
	private bool lastNote = false;
	private int minMidiPitch = 60;
    
	// Use this for initialization
	void Start () {
        soundAreaSelector = GameObject.Find("SoundManagers").GetComponent<SoundAreaSelector>();
        pitchRangeSelector = GameObject.Find("SoundManagers").GetComponent<PitchRangeSelector>();
        soundDirectionManager = GameObject.Find("SoundManagers").GetComponent<SoundDirectionManager>();
        audioPlayer = GameObject.Find("Listener").GetComponent<AudioPlayer>();
		
		setNoteManager = GameObject.Find("SoundManagers").GetComponent<SetNoteManager>();
		setNoteManager.setOnLoopEvent.AddListener(SetOnLoop);

		simulator = GameObject.Find("SecondTarget").GetComponent<SimulateRotation>();
		simulator.setOnLoopEvent.AddListener(SetOnLoop);

		// create beat
		beatInstruments = Resources.LoadAll<AudioClip> ("Audio/BeatInstruments");

		if (beatInstruments.Length >= 2) {
			AudioClip bassDrum = beatInstruments [0];
			AudioClip hihat = beatInstruments [1];

			float time = 0;

			for (int i = 0; i < numberOfBeats; i++) {
				loopNotes.Add (new LoopNote (bassDrum, soundAreaSelector.activeColor, soundAreaSelector.activeSoundArea, soundAreaSelector.activeInstrument, 0, 1, time, new Vector3 (0, 0, 0), true));
				time += loopDuration / numberOfBeats;
			}

			time -= (loopDuration / numberOfBeats) * 0.5f;
			print ("Hihat Time: " + time);
			loopNotes.Add (new LoopNote (hihat, soundAreaSelector.activeColor, soundAreaSelector.activeSoundArea, soundAreaSelector.activeInstrument, 0, 1, time, new Vector3 (0, 0, 0), true));
		}

        // sort notes by time
        loopNotes.Sort((x, y) => x.time.CompareTo(y.time));

        timer = 0;
        noteIndex = 0;

        if (loopNotes.Count > 0)
            currentNote = loopNotes[noteIndex];
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //print("Time is: " + timer);

        if (loopNotes.Count > 0)
        {
            // play note if time has been reached
            if (!lastNote && timer >= currentNote.time)
            {
                audioPlayer.PlayLoopNote(currentNote);

                noteIndex++;

                // reset note index if it has reached the end of the list
                if (noteIndex == loopNotes.Count)
                {
                    noteIndex = 0;
                    lastNote = true;
                }
                else
                {
                    currentNote = loopNotes[noteIndex];
                }
            }
        }

        // start new loop if loop duration has been reached
        if (timer > loopDuration)
        {
            // reset timer to overstepped milliseconds
            timer %= loopDuration;
            //print("new loop - Time is: " + timer);
            lastNote = false;
            
            // if the user has set some notes, add them to the loop
            if (pendingNotes.Count > 0)
            {
                loopNotes.AddRange(pendingNotes);
                loopNotes.Sort((x, y) => x.time.CompareTo(y.time));
                pendingNotes.Clear();
            }

            if (loopNotes.Count > 0)
                currentNote = loopNotes[noteIndex];
        }
	}

    // this method is registered as listener
	void SetOnLoop() 
    {
		LoopNote note = new LoopNote(soundAreaSelector.activeAudioClip, soundAreaSelector.activeColor, soundAreaSelector.activeSoundArea, soundAreaSelector.activeInstrument, pitchRangeSelector.activePitchRange, pitchRangeSelector.activePitch, timer, soundDirectionManager.soundDirection, false);
        
        audioPlayer.PlayLoopNote(note);
        
        pendingNotes.Add(note);
	}

    public void ResetButtonClickHandler()
    {
        foreach (LoopNote pendingNote in pendingNotes)
        {
            pendingNote.DeleteLight();
			audioPlayer.Stop (pendingNote);
        }
        pendingNotes.Clear();

        foreach (LoopNote loopNote in loopNotes)
        {
			loopNote.DeleteLight();
			audioPlayer.Stop (loopNote);
        }
        loopNotes.Clear();

        noteIndex = 0;
    }

	public void BackToMenuButtonClickHandler()
	{
		ResetButtonClickHandler (); // reset everything first
		SceneManager.LoadScene ("CaveSoundMenu");
	}
}
