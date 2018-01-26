using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//キャラクターの処理
public class Charactor : MonoBehaviour {
    [SerializeField] Owner owner;
    [SerializeField] public ParameterTable paraTable; //倍率
    [SerializeField, Range(1, 10)]public int lifePoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int attackPoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int defensePoint; //ポイント振り
    List<float> attackBuffList = new List<float>();// 攻撃バフリスト

    protected int life; //体力
    protected int attack; //攻撃力
    protected int defense; //防御力
    protected int attackRate; //攻撃倍率

    protected bool selectedCard;
    public BasicCard useCard {get; protected set;} //使用するカード

    float GetBuffResult(float baseAttackPoint) { 
        return baseAttackPoint * attackBuffList.Aggregate( (now, next) => now * next);
    }

    //ゲーム開始時の初期化
    public void Initialize() {
        life = paraTable.lifeScale * lifePoint;
        attack = paraTable.attackScale * attackPoint;
        defense = paraTable.defenseScale * defensePoint;
        Debug.Log(owner+" life:"+life+" attack:"+attack+" defense:"+defense);
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
        return life < 0;
    }

    public void FinishTurn() {

    }

    protected void SelectCard(BasicCard card) {
        Debug.Log("select card:"+ card.GetCardName());
        useCard = card;
        selectedCard = true;
    }
}
