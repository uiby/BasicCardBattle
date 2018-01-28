using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFirst : GuiParts {
    [SerializeField] Vector2 playerPos;
    [SerializeField] Vector2 comPos;
    public Owner randomer{get; private set;}

    protected override void Awake() {
        base.Awake();
        randomer = (Owner)Random.Range(0, 2);
        ChangeRandomer();
    }

    public void ChangeRandomer() {
        randomer = randomer == Owner.PLAYER ? Owner.COM : Owner.PLAYER;
        MovePosition(randomer == Owner.PLAYER ? playerPos : comPos, 0.2f);
    }
}
