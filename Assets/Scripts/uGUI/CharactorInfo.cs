using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorInfo : GuiParts {
    [SerializeField] Text charaName;
    [SerializeField] Text personalityText;
    [SerializeField] Text lifeText;
    [SerializeField] Text attackText;
    [SerializeField] Text buffText;

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

    public void Show(Charactor chara) {
        showing = true;
        charaName.text = chara.myName;
        switch(chara.personality) {
            case Personality.UNKNOWN: personalityText.text = "unknown"; break;
            case Personality.NORMAL: personalityText.text = "決断強い"; break;
            case Personality.HARD: personalityText.text = "柔軟性"; break;
        }
        lifeText.text = chara.life.ToString();
        attackText.text = chara.attack.ToString();
        buffText.text = chara.GetTotalBuff().ToString(".##倍");
        MovePosition(new Vector2(290, -70), 0.1f);
    }

    public void Hide() {
        showing = false;
        timer = 0.1f;
    }

    void TimeOver() {
        timer = 0;
        MovePosition(new Vector2(520, -70), 0.1f);
    }
}

