using UnityEngine;
using System.Collections;

public class LoopNote {

    public int instrument;
    public int pitch;
    public float time;    // in milliseconds
    public Vector3 direction;
    public GameObject soundSphere;

    public LoopNote(int instrument, int pitch, float time, Vector3 direction)
    {
        this.instrument = instrument;
        this.pitch = pitch;
        this.time = time;
        this.direction = direction;

        soundSphere = (GameObject)GameObject.Instantiate(GameObject.Find("SoundSphere"), direction, Quaternion.identity);
    }

    public void play()
    {
        soundSphere.GetComponent<AudioSource>().Play();
    }
}
