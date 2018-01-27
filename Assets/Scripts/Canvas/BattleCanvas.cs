using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour {
    [SerializeField] CardName playerCardName;
    [SerializeField] CardName comCardName;

    //バトルカードの名前を表示
    public void ShowBattleCardName(BasicCard card) {
        if (card.GetOwner() == Owner.PLAYER) playerCardName.Show("Lv."+card.level+" "+card.GetCardName());
        else if (card.GetOwner() == Owner.COM) comCardName.Show("Lv."+card.level+" "+card.GetCardName());
    }

}
