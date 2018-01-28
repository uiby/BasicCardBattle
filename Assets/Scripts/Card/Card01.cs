using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃カード
public class Card01 : BasicCard {
    [SerializeField] Charactor user; //使用者
    [SerializeField] Charactor opponent; //自分
    [SerializeField, Range(10, 30)] int additionalAttackAmount = 5;
	void Awake () {
		Initialize(2, "相手に攻撃(+"+additionalAttackAmount+")", "攻撃力上昇。コスト+1", CardType.ATTACK);
	}

    protected override IEnumerator Action() {
        charaAnimation.anim.Play("Attack");
        yield return null;
        yield return new WaitForAnimation(charaAnimation.anim, 0);

        yield return StartCoroutine(opponent.Damaged(user.GetAttacOnBuff() + additionalAttackAmount * level));
    }

    protected override void OnLevelUp() {
        cost++;
        sentence = "相手に攻撃(+"+additionalAttackAmount * level+")";
    }
}
