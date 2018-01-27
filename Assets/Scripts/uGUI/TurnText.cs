using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour {
    Text text;
    void Awake() {
        text = GetComponent<Text>();
    }

    public void UpdateView(int turn) {
        text.text = "turn "+turn;
    }
}
