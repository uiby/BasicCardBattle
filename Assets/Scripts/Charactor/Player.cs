using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Charactor {
    [SerializeField] GuiParts selectionCanvas;

    void Start() {
        personality = Personality.UNKNOWN;
    }

    public override void StartCardSelection() {
        base.StartCardSelection();
        selectionCanvas.MovePosition(new Vector2(-120, -150), 0.3f);
    }

    public void OnTapCard(BasicCard card) {
        SelectCard(card);
        selectionCanvas.MovePosition(new Vector2(-700, -150), 0.3f);
    }
}
