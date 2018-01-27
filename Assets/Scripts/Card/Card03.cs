using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状態変化
public class Card03 : BasicCard {
    [SerializeField] Charactor user; //使用者
    float buffRate = 1.8f;
    int count = 3;

	void Awake () {
		Initialize(3, count+"ターン攻撃力"+buffRate+"倍(重複可)", "コスト+1");
	}

    protected override IEnumerator Action() {
        charaAnimation.anim.Play("AttackBuff");
        user.AddBuff(buffRate, count);
        yield return null;
        yield return new WaitForAnimation(charaAnimation.anim, 0);
    }

    protected override void OnLevelUp() {
        cost += 1;
    }
}
