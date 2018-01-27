using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃カード
public class Card01 : BasicCard {
    [SerializeField] Charactor user; //使用者
    [SerializeField] Charactor opponent; //自分
    [SerializeField, Range(10, 30)] int addtionalAttackAmount = 10;
	void Awake () {
		Initialize();
	}

    protected override IEnumerator Action() {
        charaAnimation.anim.Play("Attack");
        yield return null;
        yield return new WaitForAnimation(charaAnimation.anim, 0);

        yield return StartCoroutine(opponent.Damaged(user.attack + addtionalAttackAmount * level));
    }
}
