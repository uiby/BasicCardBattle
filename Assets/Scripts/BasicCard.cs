using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCard : MonoBehaviour {
    [SerializeField] string name;
    [SerializeField] string sentence; //効果
    [SerializeField] string levelSentence; //レベルが上がった際の効果

    protected int level; //カードレベル 3段階 プレイする度に上がる
    [SerializeField, Range(1, 5)]protected int cost = 1; //カードコスト 1~5

    public void Initialize() { //初期化
        level = 1;
    }

    public virtual void OnPlay() {} //カードをプレイ時

    public virtual void Action() {} //アクション内容
    public virtual void OnTap() {} //タップ時

    protected void OnPlayed() { //カードプレイ後
        if (level == 3) return; //既にmax

        level++;
    }

    public virtual void OnLevelUp() {} //レベルアップ時の効果
}
