using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor {
    [SerializeField] GameObject selectionCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
    public override void StartCardSelection() {
        base.StartCardSelection();
        selectionCanvas.SetActive(true);
    }

    public void OnTapCard(BasicCard card) {
        SelectCard(card);
        selectionCanvas.SetActive(false);
    }
}
