using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    [SerializeField] GameCanvas gameCanvas;
    [SerializeField] ResultCanvas resultCanvas;
    BattleSystem battleSystem;
    [SerializeField] Player player;
    [SerializeField] Com com;

	// Use this for initialization
	void Start () {
        battleSystem = GetComponent<BattleSystem>();
		StartCoroutine(GameLoop());
	}

    IEnumerator GameLoop() {
        yield return null;

        //初期化
        player.Initialize();
        com.Initialize();

        //***どちらかのHPがゼロになるまでループ***//
        while (true) {
            yield return null;
            //ターン開始
            yield return StartCoroutine(StartTurn());
            //カードセレクト
            yield return StartCoroutine(CardSelection());
            //バトル開始(カードの早い順)
            yield return StartCoroutine(Battle());

            if (FinishGame())
                break;
        }
        //リザルト
        StartCoroutine(Result());
    }

    IEnumerator StartTurn() {
        Debug.Log("start turn");
        yield return null;
    }

    //カードセレクト
    IEnumerator CardSelection() {
        Debug.Log("card select");
        //プレイヤー
        player.StartCardSelection();
        yield return null;
        yield return StartCoroutine(player.SelectCardLoop());

        //COM
        com.StartCardSelection();
        yield return null;
    }

    //バトル
    IEnumerator Battle() {
        Debug.Log("battle");
        yield return null;
        battleSystem.PrepareBattle(player.useCard, com.useCard);
        battleSystem.ShowBattleCard();
        yield return new WaitForSeconds(2f);
        battleSystem.HideBattleCard();

        //コストの低いプレイヤからカード使用
        var firstMove = battleSystem.DecideFirstMovePlayer(player.useCard, com.useCard);
        Debug.Log("first move:"+firstMove);
        var firstCard = firstMove == Owner.PLAYER ? player.useCard : com.useCard;
        var secondCard = firstMove == Owner.PLAYER ? com.useCard : player.useCard;

        yield return StartCoroutine(battleSystem.PlayBattle(firstCard, secondCard));
    }

    IEnumerator Result() {
        if (player.Dead()) {
            player.Lose();
            com.Win();
            resultCanvas.Show("YOU LOSE...");
        } else {
            player.Win();
            com.Lose();
            resultCanvas.Show("YOU WIN!");
        }
        yield return null;
    }

    public bool FinishGame() {
        return player.Dead() || com.Dead();
    }
}
