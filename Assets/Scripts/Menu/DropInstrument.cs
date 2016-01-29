using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropInstrument : MonoBehaviour, IDropHandler {

    private Instrument dragPanelInstrument;

	// Use this for initialization
	void Start () {
        dragPanelInstrument = GameObject.Find("SoundAreaDragPanel").GetComponent<Instrument>();
	}
	
    public void OnDrop(PointerEventData eventData)
    {
        if (dragPanelInstrument.midiInstrument > 0)
            GetComponent<Instrument>().InstrumentChanged(dragPanelInstrument.midiInstrument);
    }
}
