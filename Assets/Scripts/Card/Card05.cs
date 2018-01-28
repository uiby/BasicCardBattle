using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回復
public class Card05 : BasicCard {
    [SerializeField] Charactor user; //使用者
    int percent = 60;
    int decreasePercent = 20;
	void Awake () {
		Initialize(3, percent+"%回復", "回復量-"+decreasePercent+"%。コスト+1", CardType.RECOVERY);
	}

    protected override IEnumerator Action() {
        user.Recovery((int)(user.maxLife * percent/100f));
        yield return null;
        yield return new WaitForSeconds(2f);
    }

    protected override void OnLevelUp() {
        cost += 1;
        percent = Mathf.Clamp(percent - decreasePercent, 10, 60);
        sentence = percent+"%回復";
    }
}
