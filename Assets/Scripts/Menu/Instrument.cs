using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Instrument : MonoBehaviour {

    [HideInInspector]
    public int midiInstrument;
	[HideInInspector]
	public int audioFileNumber;
    private Text textField;
	private SliderManager sliderManager;

	// Use this for initialization
    void Start()
    {
		sliderManager = GameObject.Find ("InstrumentsSlider").GetComponent<SliderManager> ();
        midiInstrument = 0;
        textField = GetComponentInChildren<Text>();
	}
	    
    public void InstrumentChanged(float number)
    {
		if (sliderManager.playMidi) {
			midiInstrument = Mathf.RoundToInt (number);
			textField.text = "" + midiInstrument;
		} else {
			audioFileNumber = Mathf.RoundToInt(number);
			if (sliderManager.finishedLoading)
				textField.text = "" + sliderManager.GetInstrumentName (Mathf.RoundToInt (number));
			else
				textField.text = "Loading...";			
		}
    }

	public string GetInstrumentName ()
	{
		return sliderManager.GetInstrumentName (audioFileNumber);
	}

}
