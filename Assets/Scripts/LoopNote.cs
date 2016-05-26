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

    public void Play()
    {
        soundLight.GetComponent<AudioSource>().Play();
    }

    // flashing light
    public IEnumerator LightFlashCoroutine()
    {
        soundLight.GetComponent<Light>().range = 3;
        Debug.Log("Light flash");
        yield return new WaitForSeconds(0.2f);
        soundLight.GetComponent<Light>().range = 1;
        Debug.Log("Light normal");
    }
}
