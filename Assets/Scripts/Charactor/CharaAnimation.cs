using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnimation : MonoBehaviour {
    [HideInInspector]
    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
		//anim.Play("Standing@loop");
	}

    public void SetTrigger(string name) {
        anim.SetTrigger(name);
    }
}
