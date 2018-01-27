using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃カード
public class Card01 : BasicCard {
	void Start () {
		Initialize();
	}

    protected override IEnumerator Action() {
        charaAnimation.anim.Play("Attack");
        yield return null;
        yield return new WaitForAnimation(charaAnimation.anim, 0);

        played = true;
    }
}
