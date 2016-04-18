using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

// manages the loop cycle, creates new loop notes and adds them to the loop
public class LoopManager : MonoBehaviour {

	private SoundAreaSelector soundAreaSelector;
	private PitchRangeSelector pitchRangeSelector;
    private SoundDirectionManager soundDirectionManager;
    private AudioPlayer audioPlayer;
	private SimulateRotation simulator;
	private SetNoteManager setNoteManager;
    private List<LoopNote> loopNotes = new List<LoopNote>();
    private List<LoopNote> pendingNotes = new List<LoopNote>();
    private int beatInstrument = 115;
    private float timer;
    private float loopDuration;     // in milliseconds
    private int noteIndex;
    private LoopNote currentNote;
    private bool lastNote = false;
    private int minPitch = 60;
    
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
        loopNotes.Add(new LoopNote(beatInstrument, 80, 0, new Vector3(6, 2.5f, 2.8f)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 0.8f, new Vector3(7, 2.5f, 2.8f)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 1.6f, new Vector3(8, 2.5f, 2.8f)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 2.4f, new Vector3(9, 2.5f, 2.8f)));
        // sort notes by time
        loopNotes.Sort((x, y) => x.time.CompareTo(y.time));

        timer = 0;
        loopDuration = 3.2f;
        noteIndex = 0;
        currentNote = loopNotes[noteIndex];
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //print("Time is: " + timer);

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

        // start new loop if loop duration has been reached
        if (timer > loopDuration)
        {
            // reset timer to overstepped milliseconds
            timer %= loopDuration;
            lastNote = false;
            currentNote = loopNotes[noteIndex];
            //print("new loop - Time is: " + timer);

            // if the user has set some notes, add them to the loop
            if (pendingNotes.Count > 0)
            {
                loopNotes.AddRange(pendingNotes);
                loopNotes.Sort((x, y) => x.time.CompareTo(y.time));
                pendingNotes.Clear();
            }
        }
	}

    // this method is registered as listener
	void SetOnLoop() {
        LoopNote note = new LoopNote(soundAreaSelector.activeInstrument, minPitch + pitchRangeSelector.activePitch, timer, soundDirectionManager.soundDirection);
        audioPlayer.PlayLoopNote(note);
        pendingNotes.Add(note);
	}
}
