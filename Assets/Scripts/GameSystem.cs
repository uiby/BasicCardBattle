using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    [SerializeField] TurnText turnText;
    [SerializeField] ResultCanvas resultCanvas;
    [SerializeField] Player player;
    [SerializeField] Com com;
    BattleSystem battleSystem;

    int turnCount = 0;

	// Use this for initialization
	void Start () {
        battleSystem = GetComponent<BattleSystem>();
        turnCount = 0;
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

            player.useCard.Reflesh();
            com.useCard.Reflesh();
        }
        //リザルト
        StartCoroutine(Result());
    }

    IEnumerator StartTurn() {
        Debug.Log("start turn");
        turnCount++;
        turnText.UpdateView(turnCount);
        //バフの整理
        player.UpdateBuff();
        com.UpdateBuff();
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

        yield return new WaitForSeconds(1f);

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
