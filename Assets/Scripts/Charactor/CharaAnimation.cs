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

    public IEnumerator PlayAnimation(string name) {
        anim.Play(name);
        yield return null;
        yield return new WaitForAnimation(anim, 0);
    }
}
