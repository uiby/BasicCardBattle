using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com : Charactor {
    [SerializeField] List<BasicCard> myCardList = new List<BasicCard>();

	// Use this for initialization
	void Start () {
		
	}

    public override void StartCardSelection() {
        base.StartCardSelection();

        var card = myCardList[Random.Range(0, myCardList.Count)];
        SelectCard(card);
    }

}
