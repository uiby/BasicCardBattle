using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor {
    [SerializeField] GuiParts selectionCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
    public override void StartCardSelection() {
        base.StartCardSelection();
        selectionCanvas.MovePosition(new Vector2(-150, -150), 0.3f);
    }

    public void OnTapCard(BasicCard card) {
        SelectCard(card);
        selectionCanvas.MovePosition(new Vector2(-700, -150), 0.3f);
    }
}
