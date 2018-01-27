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
    protected int cost; //カードコスト 1~5
    protected string sentence; //効果
    protected string levelSentence; //レベルが上がった際の効果
    Image background;
    protected bool isPlayed = false;
    public int level {get; protected set;} //カードレベル 3段階 プレイする度に上がる

    //ゲーム開始時
    public void Initialize(int _cost, string _sentence, string _levelSentence) { //初期化
        background = GetComponent<Image>();
        level = 1;
        cost = _cost;
        sentence = _sentence;
        levelSentence = _levelSentence;
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
        Debug.Log(cardName+": played");
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
    public string GetSentence() {
        return sentence;
    }
    public string GetLevelSentence() {
        return levelSentence;
    }
    public int GetCost() {
        return cost;
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
        sentence = card.GetSentence();
        levelSentence = card.GetLevelSentence();
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
