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
    List<Buff> attackBuffList = new List<Buff>();// 攻撃バフリスト

    public int life{get; private set;} //ライフ
    public int maxLife{get; private set;} //最大ライフ
    public int attack{get; private set;} //攻撃力
    public int defense{get; private set;} //防御力
    public int attackRate{get; private set;} //攻撃倍率

    protected bool selectedCard;
    protected CharaAnimation charaAnimation;
    public BasicCard useCard {get; protected set;} //使用するカード

    public int GetAttacOnBuff() {
        var rate = 1f;
        for (int n = 0; n < attackBuffList.Count; n++) {
            rate *= attackBuffList[n].rate;
        }
        return (int)(attack * rate);
        //return baseAttackPoint * attackBuffList.Aggregate((now, next) => now * next.rate);
    }

    public void AddBuff(float rate, int count) {
        attackBuffList.Add(new Buff(rate, count));
    }
    public void UpdateBuff() {
        attackBuffList.ForEach(buff => buff.CountDown());
        attackBuffList = attackBuffList.Where(buff => buff.count >= 0).ToList();
    }

    //ゲーム開始時の初期化
    public void Initialize() {
        charaAnimation = GetComponent<CharaAnimation>();
        life = paraTable.lifeScale * lifePoint;
        maxLife = life;
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
        if (useCard.OnPlayedOpponent()) {
            yield return new WaitForSeconds(1f);
            yield break;
        }
        life = Mathf.Clamp(life - amount, 0, maxLife);
        lifeGauge.UpdateAmount(life);
        yield return null;
        yield return StartCoroutine(charaAnimation.PlayAnimation("Damaged"));
    }

    public void Recovery(int amount) {
        life = Mathf.Clamp(life + amount, 0, maxLife);
        lifeGauge.UpdateAmount(life);
    }

    public void Win() {
        StartCoroutine(charaAnimation.PlayAnimation("Winner"));
    }

    public void Lose() {
        StartCoroutine(charaAnimation.PlayAnimation("Loser"));
    }
}
