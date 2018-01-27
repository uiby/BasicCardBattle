using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardName : MonoBehaviour {
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        Hide();
	}

    public void Show(string str) {
        StartCoroutine(Effect(str));
    }

    IEnumerator Effect(string str) {
        text.text = str;
        text.enabled = true;
        yield return new WaitForSeconds(2f);
        Hide();
    }

    void Hide() {
        text.text = "";
        text.enabled = false;
    }
}
