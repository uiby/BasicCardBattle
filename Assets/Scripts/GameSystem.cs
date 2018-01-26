using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    [SerializeField] GameCanvas gameCanvas;
    [SerializeField] BattleCanvas battleCanvas;
    [SerializeField] Player player;
    [SerializeField] Com com;

	// Use this for initialization
	void Start () {
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

            if (player.Dead())
                break;
            else if (com.Dead())
                break;

            //ターン終了
            yield return StartCoroutine(FinishTurn());
        }

        //リザルト

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
        battleCanvas.SetUsingCard(player.useCard, com.useCard);
        battleCanvas.Show();
        yield return new WaitForSeconds(2f);

        //コストの低いプレイヤからカード使用
    }

    //ターン終了
    IEnumerator FinishTurn() {
        Debug.Log("finish turn");
        //カード効果アップ
        player.FinishTurn();
        com.FinishTurn();
        yield return null;
    }
}
