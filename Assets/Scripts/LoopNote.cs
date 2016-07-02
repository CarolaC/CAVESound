using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoopNote {

    public int soundArea;
	public int instrumentNum;
	public float pitch;
	public int pitchRange;
    public Color color;
    public float time;    // in milliseconds
    public Vector3 direction;
    public GameObject soundLight;
    public GameObject beatSoundPoint;
    public BeatVisualizer beatVisualizer;

	public LoopNote(AudioClip audioClip, Color color, int soundArea, int instrumentNum, int pitchRange, float pitch, 
		float time, Vector3 direction)
	{
		this.color = color;
		this.soundArea = soundArea;
		this.instrumentNum = instrumentNum;
		this.pitchRange = pitchRange;
		this.pitch = pitch;
        this.time = time;
        this.direction = direction;

        soundLight = (GameObject)GameObject.Instantiate(GameObject.Find("SoundLight"), direction, Quaternion.identity);
		soundLight.GetComponent<AudioSource> ().clip = audioClip;
		soundLight.GetComponent<AudioSource> ().pitch = pitch;

        beatVisualizer = GameObject.Find("BeatPanel").GetComponent<BeatVisualizer>();
        beatSoundPoint = (GameObject)GameObject.Instantiate(beatVisualizer.beatPointPrefab);
        beatSoundPoint.transform.localPosition = beatVisualizer.calculateBeatSoundPointPosition(pitchRange, time);
        beatSoundPoint.transform.localScale = new Vector3(1, 0.2f, 1);
        beatSoundPoint.transform.SetParent(beatVisualizer.gameObject.transform, false);
        soundLight.GetComponent<Light>().color = color;
        beatSoundPoint.GetComponent<Image>().color = color;
    }

	// flashing light
    public IEnumerator LightFlashCoroutine()
    {
        soundLight.GetComponent<Light>().intensity = 4f;
        Debug.Log("Light flash");
        yield return new WaitForSeconds(0.2f);
        soundLight.GetComponent<Light>().intensity = 3f;
        Debug.Log("Light normal");
    }

    public void DeleteLight()
    {
        UnityEngine.Object.Destroy(soundLight);
        UnityEngine.Object.Destroy(beatSoundPoint);
    }
}
