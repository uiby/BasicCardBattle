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
        personality = Random.Range(0, 2) == 0 ? Personality.NORMAL : Personality.HARD;
	}

    public override void StartCardSelection() {
        base.StartCardSelection();

        //var card = myCardList[Random.Range(0, myCardList.Count)];
        var index = 0;
        if (personality == Personality.NORMAL)
            index = ThinkNormal();
        else index = ThinkHard();

        SelectCard(myCardList[index]);
    }

    ///ルーチン///
    // アタック
    // ガード
    // バフ
    // カウンター
    // 回復

    //返却値 アドレス
    int ThinkNormal() {
        var myLife = life;
        var myMaxLife = maxLife;
        var myBuff = GetTotalBuff();
        var myMaxBuff = GetMaxBuff();
        var myAttackCost = myCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var myCounterCost = myCardList.Find(n => n.cardType == CardType.COUNTER).cost;
        var myGuardCost = myCardList.Find(n => n.cardType == CardType.GUARD).cost;
        var opLife = op.life;
        var opMaxLife = op.maxLife;
        var opAttackCost = opCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var opBuff = op.GetTotalBuff();
        var opMaxBuff = op.GetMaxBuff();
        
        var attackValue = fuzzy.FuzzyOr(fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(opLife, opMaxLife)), fuzzy.FuzzyQuadraticDown(myBuff, myMaxBuff)); //敵のライフ次第
        var guardValue = fuzzy.FuzzyAnd(fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(myLife, myMaxLife)), fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myGuardCost, myCounterCost))); //自分のガードコストと敵のアタックコスト次第
        var counterValue = fuzzy.FuzzyAnd(fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myLife, myMaxLife)), fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(myCounterCost, opAttackCost))); //自分のガードコストと敵のアタックコスト次第
        var buffValue = fuzzy.FuzzyAnd(fuzzy.FuzzyLinear(myLife, myMaxLife), fuzzy.FuzzyNot(fuzzy.FuzzyLinear(opBuff, opMaxBuff))); //自分のライフ次第
        var recoveryValue = fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(myLife, myMaxLife)); //自分のライフ次第

        //Debug.Log("ATK:"+attackValue.ToString("N3")+" GUD:"+guardValue.ToString("N3")+" CUT:"+counterValue.ToString("N3")+" BUF:"+buffValue.ToString("N3")+" RCV:"+recoveryValue.ToString("N3"));

        float maxValue = Mathf.Max(attackValue,　guardValue, counterValue, buffValue, recoveryValue);
        if (maxValue == attackValue) return myCardList.FindIndex(n => n.cardType == CardType.ATTACK);
        if (maxValue == guardValue) return myCardList.FindIndex(n => n.cardType == CardType.GUARD);
        if (maxValue == counterValue) return myCardList.FindIndex(n => n.cardType == CardType.COUNTER);
        if (maxValue == buffValue) return myCardList.FindIndex(n => n.cardType == CardType.BUFF);

        return myCardList.FindIndex(n => n.cardType == CardType.RECOVERY);
    }

    int ThinkHard() {
        var myLife = life;
        var myMaxLife = maxLife;
        var myBuff = GetTotalBuff();
        var myMaxBuff = GetMaxBuff();
        var myAttackCost = myCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var myCounterCost = myCardList.Find(n => n.cardType == CardType.COUNTER).cost;
        var myGuardCost = myCardList.Find(n => n.cardType == CardType.GUARD).cost;
        var myRecoveryLevel = myCardList.Find(n => n.cardType == CardType.RECOVERY).level;
        var opLife = op.life;
        var opMaxLife = op.maxLife;
        var opBuff = op.GetTotalBuff();
        var opMaxBuff = op.GetMaxBuff();
        var opAttackCost = opCardList.Find(n => n.cardType == CardType.ATTACK).cost;
        var opCounterCost = opCardList.Find(n => n.cardType == CardType.COUNTER).cost;
        var opGuardCost = opCardList.Find(n => n.cardType == CardType.GUARD).cost;

        var attackValue = fuzzy.FuzzyOr(fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(opLife, opMaxLife)), fuzzy.FuzzyQuadraticDown(myBuff, myMaxBuff)); //敵のライフ次第
        var guardValue = fuzzy.FuzzyOr(fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myGuardCost, opAttackCost)), fuzzy.FuzzyQuadraticDown(opBuff, opMaxBuff)); //自分のガードコストと敵のアタックコスト次第
        var counterValue = fuzzy.FuzzyOr(fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myCounterCost, opAttackCost)), fuzzy.FuzzyQuadraticDown(opBuff, opMaxBuff)); //自分のガードコストと敵のアタックコスト次第
        var buffValue = fuzzy.FuzzyLinear(myLife, myMaxLife); //自分のライフ次第
        var recoveryValue = fuzzy.FuzzyAnd(fuzzy.FuzzyNot(fuzzy.FuzzyQuadraticUp(myLife, myMaxLife)), fuzzy.FuzzyNot(fuzzy.FuzzyLinear(myRecoveryLevel, 4))); //自分のライフ次第

        //Debug.Log("ATK:"+attackValue.ToString("N3")+" GUD:"+guardValue.ToString("N3")+" CUT:"+counterValue.ToString("N3")+" BUF:"+buffValue.ToString("N3")+" RCV:"+recoveryValue.ToString("N3"));

        float maxValue = Mathf.Max(attackValue,　guardValue, counterValue, buffValue, recoveryValue);
        if (maxValue == attackValue) return myCardList.FindIndex(n => n.cardType == CardType.ATTACK);
        if (maxValue == guardValue) return myCardList.FindIndex(n => n.cardType == CardType.GUARD);
        if (maxValue == counterValue) return myCardList.FindIndex(n => n.cardType == CardType.COUNTER);
        if (maxValue == buffValue) return myCardList.FindIndex(n => n.cardType == CardType.BUFF);

        return myCardList.FindIndex(n => n.cardType == CardType.RECOVERY);
    }

}
