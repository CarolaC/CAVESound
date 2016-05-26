using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Instrument : MonoBehaviour {

    [HideInInspector]
    public int midiInstrument;
    [HideInInspector]
    public Color color;
    private Text textField;
    private Image image;

	// Use this for initialization
    void Start()
    {
        midiInstrument = 0;
        textField = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        color = new Color(0, 0, 0);
	}
	    
    public void InstrumentChanged(float number)
    {
        midiInstrument = Mathf.RoundToInt(number);
        textField.text = "Instrument: " + midiInstrument;
        color = new Color((1.0f / 20.0f) * number, (1.0f / 40.0f) * number, (1.0f / 60.0f) * number);
        image.color = color;
    }
}
