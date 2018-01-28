using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バリア
public class Card02 : BasicCard {
    int percent = 100;
	void Awake () {
		Initialize(1, "攻撃を"+percent+"%の確率でガード(先行で発動の場合)", "コスト+2", CardType.GUARD);
        percent = 100;
	}

    protected override IEnumerator Action() {
        charaAnimation.anim.Play("Guard");
        yield return null;
        yield return new WaitForAnimation(charaAnimation.anim, 0);
    }

    protected override void OnLevelUp() {
        cost += 2;
    }

    public override bool OnPlayedOpponent() {
        if (!isPlayed) return false; //後行のためガード失敗
        var rand = Random.Range(0, 101);
        if (rand <= percent) {
            //成功
            charaAnimation.anim.Play("Guard");
            percent /= 2;
            sentence = "攻撃を"+percent+"%の確率でガード(先行で発動の場合)";
            return true;
        }

        return false;
    }
}
