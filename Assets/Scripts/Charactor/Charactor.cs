using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//キャラクターの処理
public class Charactor : MonoBehaviour {
    [SerializeField] Owner owner;
    [SerializeField] Gauge lifeGauge;
    [SerializeField] public ParameterTable paraTable; //倍率
    [SerializeField, Range(1, 10)]public int lifePoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int attackPoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int defensePoint; //ポイント振り
    List<float> attackBuffList = new List<float>();// 攻撃バフリスト

    public int life{get; private set;} //体力
    public int attack{get; private set;} //攻撃力
    public int defense{get; private set;} //防御力
    public int attackRate{get; private set;} //攻撃倍率

    protected bool selectedCard;
    protected CharaAnimation charaAnimation;
    public BasicCard useCard {get; protected set;} //使用するカード

    float GetBuffResult(float baseAttackPoint) { 
        return baseAttackPoint * attackBuffList.Aggregate( (now, next) => now * next);
    }

    //ゲーム開始時の初期化
    public void Initialize() {
        charaAnimation = GetComponent<CharaAnimation>();
        life = paraTable.lifeScale * lifePoint;
        attack = paraTable.attackScale * attackPoint;
        defense = paraTable.defenseScale * defensePoint;
        Debug.Log(owner+" life:"+life+" attack:"+attack+" defense:"+defense);
        lifeGauge.Initialize(life, life);
    }

    //カード選択タイム
    public IEnumerator SelectCardLoop() {
        yield return null;
        while(!selectedCard) {
            yield return null;
        }
    }

    //カードセレクト開始
    public virtual void StartCardSelection() {
        selectedCard = false;
    }

    public bool Dead() {
        return life <= 0;
    }

    protected void SelectCard(BasicCard card) {
        Debug.Log("select card:"+ card.GetCardName());
        useCard = card;
        selectedCard = true;
    }

    public IEnumerator Damaged(int amount) {
        life = Mathf.Clamp(life - amount, 0, paraTable.lifeScale * lifePoint);
        lifeGauge.UpdateAmount(life);
        yield return null;
        yield return StartCoroutine(charaAnimation.PlayAnimation("Damaged"));
    }

    public void Win() {
        StartCoroutine(charaAnimation.PlayAnimation("Winner"));
    }

    public void Lose() {
        StartCoroutine(charaAnimation.PlayAnimation("Loser"));
    }
}
