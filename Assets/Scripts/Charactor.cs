using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//キャラクターの処理
public class Charactor : MonoBehaviour {
    [SerializeField] public ParameterTable table; //倍率
    [SerializeField, Range(1, 10)]public int lifePoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int attackPoint; //ポイント振り
    [SerializeField, Range(1, 10)]public int defensePoint; //ポイント振り
    List<float> attackBuffList = new List<float>();// 攻撃バフリスト

    protected int life; //体力
    protected int attack; //攻撃力
    protected int defense; //防御力
    protected int attackRate; //攻撃倍率

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float GetBuffResult(float baseAttackPoint) { 
        return baseAttackPoint * attackBuffList.Aggregate( (now, next) => now * next);
    }
}
