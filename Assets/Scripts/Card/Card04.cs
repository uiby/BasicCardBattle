using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//反撃
public class Card04 : BasicCard {
    [SerializeField] Charactor user; //使用者
    [SerializeField] Charactor opponent; //自分
    float counterRate = 1.5f;

	void Awake () {
		Initialize(2, "敵が攻撃した場合"+counterRate+"倍で反撃する(先行で発動の場合)", "コスト+2");
	}

    protected override IEnumerator Action() {
        yield return null;
        yield return new WaitForSeconds(2f);
    }

    protected override void OnLevelUp() {
        cost += 2;
    }

    public override bool OnPlayedOpponent() {
        if (!isPlayed) return false; //後行のためガード失敗

        charaAnimation.anim.Play("CounterAttack");
        StartCoroutine(opponent.Damaged((int)(opponent.GetAttacOnBuff() * counterRate)));

        return true;
    }

}
