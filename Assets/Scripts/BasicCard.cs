using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicCard : MonoBehaviour {
    [SerializeField] Owner owner; //所有者
    [SerializeField] Text costText;
    [SerializeField] Text levelText;
    [SerializeField] Text nameText;
    Image background;
    [SerializeField] string cardName;
    [SerializeField] string sentence; //効果
    [SerializeField] string levelSentence; //レベルが上がった際の効果

    public int level {get; protected set;} //カードレベル 3段階 プレイする度に上がる
    [SerializeField, Range(1, 5)] int cost; //カードコスト 1~5

    //ゲーム開始時
    public void Initialize() { //初期化
        background = GetComponent<Image>();
        level = 1;
        UpdateView();
    }

    public virtual void OnPlay() {} //カードをプレイ時

    public virtual void Action() {} //アクション内容
    public virtual void OnTap() {} //タップ時

    protected void OnPlayed() { //カードプレイ後
        if (level == 3) return; //既にmax

        level++;
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

    public void Copy(BasicCard card) {
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
