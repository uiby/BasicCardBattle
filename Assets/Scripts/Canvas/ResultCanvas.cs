﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCanvas : GuiParts {
    [SerializeField] Text gameResultText;

    protected override void Awake() {
        base.Awake();
        Hide();
    }

    public void Show(string result) {
        gameResultText.text = result;
        gameObject.SetActive(true);
        MovePosition(Vector2.zero, 0.25f);
    }

    public void Hide() {
        MovePositionInTheMoment(new Vector2(width, 0));
        gameObject.SetActive(false);
    }

    public void OnTapRetry() {
        SceneManager.LoadScene("Main");
    }
}
