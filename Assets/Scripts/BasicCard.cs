using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicCard : MonoBehaviour {
    [SerializeField] Owner owner; //所有者
    [SerializeField] protected CharaAnimation charaAnimation;
    [SerializeField] Text costText;
    [SerializeField] Text levelText;
    [SerializeField] Text nameText;
    [SerializeField] string cardName;
    [SerializeField] string sentence; //効果
    [SerializeField] string levelSentence; //レベルが上がった際の効果
    [SerializeField, Range(1, 5)] int cost; //カードコスト 1~5
    Image background;
    public int level {get; protected set;} //カードレベル 3段階 プレイする度に上がる

    //ゲーム開始時
    public void Initialize() { //初期化
        background = GetComponent<Image>();
        level = 1;
        UpdateView();
    }

    protected virtual IEnumerator Action() {
        yield return null;
    } //カードをプレイ時

    protected void OnPlayed() { //カードプレイ後
        level++;
        OnLevelUp();
        UpdateView();
    }

    //カード実行
    public IEnumerator Play() {
        yield return null;
        yield return StartCoroutine(Action());

        OnPlayed();
        Debug.Log(cardName+": played");
    }

    public virtual void OnLevelUp() {} //レベルアップ時の効果

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
