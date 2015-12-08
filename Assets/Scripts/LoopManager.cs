using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class LoopManager : MonoBehaviour {

	private SoundAreaSelector soundAreaSelector;
	private PitchRangeSelector pitchRangeSelector;
    private AudioPlayer audioPlayer;
	private SimulateRotation simulator;
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
        audioPlayer = GameObject.Find("Listener").GetComponent<AudioPlayer>();

		simulator = GameObject.Find("SecondTarget").GetComponent<SimulateRotation>();
		simulator.setOnLoopEvent.AddListener(setOnLoop);

        // create beat
        loopNotes.Add(new LoopNote(beatInstrument, 80, 0, new Vector3(0, 0, 0)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 1, new Vector3(0, 0, 0)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 2, new Vector3(0, 0, 0)));
        loopNotes.Add(new LoopNote(beatInstrument, 60, 3, new Vector3(0, 0, 0)));
        // sort notes by time
        loopNotes.Sort((x, y) => x.time.CompareTo(y.time));

        timer = 0;
        loopDuration = 4;
        noteIndex = 0;
        currentNote = loopNotes[noteIndex];
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        print("Time is: " + timer);

        // play note if time has been reached
        if (!lastNote && timer >= currentNote.time)
        {
            audioPlayer.playLoopNote(currentNote);
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
            print("new loop - Time is: " + timer);

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
	void setOnLoop() {
		print("Set on loop was invoked");

        LoopNote note = new LoopNote(soundAreaSelector.activeSoundArea, minPitch + pitchRangeSelector.activePitch, timer, new Vector3(0, 0, 0));
        audioPlayer.playLoopNote(note);
        pendingNotes.Add(note);
	}
}
