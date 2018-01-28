using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : GuiParts {
    [SerializeField] Text cardName;
    [SerializeField] Text Cost;
    [SerializeField] Text Level;
    [SerializeField] Text cardSentence;
    [SerializeField] Text levelUpSentence;

    bool showing;
    float timer = 0;

	// Use this for initialization
	void Start () {
		TimeOver();
	}

    void Update() {
        if (showing) return;
        if (timer == 0) return;

        timer += Time.deltaTime;
        if (timer > 1) {
            TimeOver();
        }
    }

    public void Show(BasicCard card) {
        showing = true;
        cardName.text = card.GetCardName();
        Cost.text = "Cost "+card.cost;
        Level.text = "Level "+card.level;
        cardSentence.text = card.sentence;
        levelUpSentence.text = card.levelSentence;
        MovePosition(new Vector2(290, -110), 0.2f);
    }

    public void Hide() {
        showing = false;
        timer = 0.1f;
    }

    void TimeOver() {
        timer = 0;
        MovePosition(new Vector2(290, -340), 0.2f);
    }
}

