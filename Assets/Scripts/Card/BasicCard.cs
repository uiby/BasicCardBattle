using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicCard : MonoBehaviour {
    [SerializeField] Owner owner; //所有者
    [SerializeField] protected CharaAnimation charaAnimation;
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text costText;
    [SerializeField] string cardName;
    public int cost{get; protected set;} //カードコスト 1~5
    public string sentence{get; protected set;} //効果
    public string levelSentence{get; protected set;} //レベルが上がった際の効果
    protected bool isPlayed = false;
    public int level {get; protected set;} //カードレベル 3段階 プレイする度に上がる
    public CardType cardType {get; private set;}

    //ゲーム開始時
    public void Initialize(int _cost, string _sentence, string _levelSentence, CardType _cardType) { //初期化
        level = 1;
        cost = _cost;
        sentence = _sentence;
        levelSentence = _levelSentence;
        cardType = _cardType;
        UpdateView();
    }

    protected virtual void OnLevelUp() {} //レベルアップ時の効果
    protected virtual IEnumerator Action() {
        yield return null;
    } //カードをプレイ時

    protected void OnPlayed() { //カードプレイ後
        isPlayed = true;
        level++;
        OnLevelUp();
        UpdateView();
    }

    //カード実行
    public IEnumerator Play() {
        isPlayed = false;
        yield return null;
        yield return StartCoroutine(Action());

        OnPlayed();
        //Debug.Log(owner+" play "+cardName);
    }

    //カードリフレッシュ
    public void Reflesh() {
        isPlayed = false;
    }

    //相手のカード使用時 成功ならtrue
    public virtual bool OnPlayedOpponent() {
        return false;
    }

    public string GetCardName() {
        return cardName;
    }

    public Owner GetOwner() {
        return owner;
    }
    public CharaAnimation GetCharaAnimation() {
        return charaAnimation;
    }

    public void Copy(BasicCard card) {
        charaAnimation = card.GetCharaAnimation();
        cardName = card.GetCardName();
        sentence = card.sentence;
        levelSentence = card.levelSentence;
        level = card.level;
        cost = card.cost;
        UpdateView();
    }

    public void UpdateView() {
        costText.text = cost.ToString();
        levelText.text = level.ToString();
        nameText.text = cardName.ToString();
    }
}
