using UnityEngine;
using System.Collections;

public class LoopNote {

    public int instrument;
    public int pitch;
    public float time;    // in milliseconds
    public Vector3 direction;

    public LoopNote(int instrument, int pitch, float time, Vector3 direction)
    {
        this.instrument = instrument;
        this.pitch = pitch;
        this.time = time;
        this.direction = direction;
    }

}
