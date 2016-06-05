using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Instrument : MonoBehaviour {

    [HideInInspector]
    public int midiInstrument;
    private Text textField;

	// Use this for initialization
    void Start()
    {
        midiInstrument = 0;
        textField = GetComponentInChildren<Text>();
	}
	    
    public void InstrumentChanged(float number)
    {
        midiInstrument = Mathf.RoundToInt(number);
        textField.text = "" + midiInstrument;
    }
}
