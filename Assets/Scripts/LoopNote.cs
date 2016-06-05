using UnityEngine;
using System.Collections;

public class LoopNote {

    public int instrument;
    public Color color;
    public int pitch;
    public float time;    // in milliseconds
    public Vector3 direction;
    public GameObject soundLight;

    public LoopNote(int instrument, Color color, int pitch, float time, Vector3 direction)
    {
        this.instrument = instrument;
        this.color = color;
        this.pitch = pitch;
        this.time = time;
        this.direction = direction;

        soundLight = (GameObject)GameObject.Instantiate(GameObject.Find("SoundLight"), direction, Quaternion.identity);
        soundLight.GetComponent<Light>().color = color;
    }

    public void PlayAudio()
    {
        soundLight.GetComponent<AudioSource>().Play();
    }

    public void PlayMidi()
    {
        soundLight.GetComponent<MidiPlayer>().Play(this);
    }

    // flashing light
    public IEnumerator LightFlashCoroutine()
    {
        soundLight.GetComponent<Light>().intensity = 1.5f;
        Debug.Log("Light flash");
        yield return new WaitForSeconds(0.2f);
        soundLight.GetComponent<Light>().intensity = 0.5f;
        Debug.Log("Light normal");
    }

    public void DeleteLight()
    {
        soundLight.GetComponent<MidiPlayer>().Stop(this);
        UnityEngine.Object.Destroy(soundLight);
    }
}
