using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Com : Charactor {
    [SerializeField] List<BasicCard> myCardList = new List<BasicCard>();
    [SerializeField] List<BasicCard> opCardList = new List<BasicCard>();
    [SerializeField] Charactor op;
    Fuzzy fuzzy; //思考ルーチン

	// Use this for initialization
	void Start () {
        fuzzy = GetComponent<Fuzzy>();
	}

    public override void StartCardSelection() {
        base.StartCardSelection();

        //var card = myCardList[Random.Range(0, myCardList.Count)];
        var card = myCardList[Think()];
        SelectCard(card);
    }

    ///ルーチン///
    // アタック
    // ガード
    // バフ
    // カウンター
    // 回復

    //返却値 アドレス
    int Think() {
        var myLife = life;
        var myMaxLife = maxLife;
        var myAttackCost = myCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var myCounterCost = myCardList.Find(n => n.cardType == CardType.COUNTER).cost;
        var myGuardCost = myCardList.Find(n => n.cardType == CardType.GUARD).cost;
        var opLife = op.life;
        var opMaxLife = op.maxLife;
        var opAttackCost = opCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var opCounterCost = opCardList.Find(n => n.cardType == CardType.COUNTER).cost;
        var opGuardCost = opCardList.Find(n => n.cardType == CardType.GUARD).cost;

        var attackValue = fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(opLife, opMaxLife)); //敵のライフ次第
        var guardValue = fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myGuardCost, opAttackCost)); //自分のガードコストと敵のアタックコスト次第
        var counterValue = fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myCounterCost, opAttackCost)); //自分のガードコストと敵のアタックコスト次第
        var buffValue = fuzzy.FuzzyLinear(myLife, myMaxLife); //自分のライフ次第
        var recoveryValue = fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(myLife, myMaxLife)); //自分のライフ次第

        Debug.Log("ATK:"+attackValue+" GUD:"+guardValue+" CUT:"+counterValue+" BUF:"+buffValue+" RCV:"+recoveryValue);

        float maxValue = Mathf.Max(attackValue,　guardValue, counterValue, buffValue, recoveryValue);
        if (maxValue == attackValue) return myCardList.FindIndex(n => n.cardType == CardType.ATTACK);
        if (maxValue == guardValue) return myCardList.FindIndex(n => n.cardType == CardType.GUARD);
        if (maxValue == counterValue) return myCardList.FindIndex(n => n.cardType == CardType.COUNTER);
        if (maxValue == buffValue) return myCardList.FindIndex(n => n.cardType == CardType.BUFF);

        return myCardList.FindIndex(n => n.cardType == CardType.RECOVERY);
    }

}
