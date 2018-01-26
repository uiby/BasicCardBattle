using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour {
    [SerializeField] RectTransform selectedCard;
    [SerializeField] BasicCard playerCard;
    [SerializeField] CardName playerCardName;
    [SerializeField] BasicCard comCard;
    [SerializeField] CardName comCardName;

    void Start() {
        Hide();
    }

    //選んだカードをセットする
    public void SetUsingCard(BasicCard _playerCard, BasicCard _comCard) {
        playerCard.Copy(_playerCard);
        comCard.Copy(_comCard);
    }

    public void Show() {
        selectedCard.gameObject.SetActive(true);
    }
    public void Hide() {
        selectedCard.gameObject.SetActive(false);
    }

    //バトルカードの名前を表示
    public void ShowBattleCardName(Owner owner) {
        if (owner == Owner.PLAYER) playerCardName.Show("lv."+playerCard.level+" "+playerCard.GetCardName());
        else if (owner == Owner.COM) comCardName.Show("lv."+comCard.level+" "+comCard.GetCardName());
    }

}
