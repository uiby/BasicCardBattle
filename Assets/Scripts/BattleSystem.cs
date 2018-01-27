using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {
    [SerializeField] BattleCanvas battleCanvas;
    GameSystem gameSystem;

    void Awake() {
        gameSystem = GetComponent<GameSystem>();
    }

    public void PrepareBattle(BasicCard playerCard, BasicCard comCard) {
        battleCanvas.SetUsingCard(playerCard, comCard);
    }

    public void ShowBattleCard() {
        battleCanvas.Show();
    }

    public void HideBattleCard() {
        battleCanvas.Hide();
    }

    public Owner DecideFirstMovePlayer(BasicCard playerCard, BasicCard comCard) {
        var pCost = playerCard.GetCost();
        var cCost = comCard.GetCost();
        if (pCost < cCost)
            return Owner.PLAYER;
        if (cCost < pCost)
            return Owner.COM;

        return Random.Range(0, 2) == 0 ? Owner.PLAYER : Owner.COM;
    }

    public IEnumerator PlayBattle(BasicCard firstCard, BasicCard secondCard) {
        yield return null;
        //カード名表示
        battleCanvas.ShowBattleCardName(firstCard.GetOwner());
        yield return StartCoroutine(firstCard.Play());

        if (gameSystem.FinishGame())
            yield break;

        //カード名表示
        battleCanvas.ShowBattleCardName(secondCard.GetOwner());
        yield return StartCoroutine(secondCard.Play());
    }
}
