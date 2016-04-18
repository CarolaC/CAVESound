using UnityEngine;
using System.Collections;

public class LoopNote {

    public int instrument;
    public int pitch;
    public float time;    // in milliseconds
    public Vector3 direction;
    public GameObject soundLight;

    public LoopNote(int instrument, int pitch, float time, Vector3 direction)
    {
        this.instrument = instrument;
        this.pitch = pitch;
        this.time = time;
        this.direction = direction;

        soundLight = (GameObject)GameObject.Instantiate(GameObject.Find("SoundLight"), direction, Quaternion.identity);
    }

    public void Play()
    {
        soundLight.GetComponent<AudioSource>().Play();
    }

    // flashing light
    public IEnumerator LightFlashCoroutine()
    {
        soundLight.GetComponent<Light>().range = 5;
        Debug.Log("Light flash");
        yield return new WaitForSeconds(0.2f);
        soundLight.GetComponent<Light>().range = 2;
        Debug.Log("Light normal");
    }
}
