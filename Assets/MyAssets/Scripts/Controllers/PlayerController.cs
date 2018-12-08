using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerAnimationHandler _animationHandler;

	// Use this for initialization
	void Start () {
        _animationHandler = this.gameObject.GetComponent<PlayerAnimationHandler>();
        _animationHandler.Stay();
	}
	
    bool IsPlayerMoving(){
        var result = Input.GetAxis("Vertical") > 0 
                          || Input.GetAxis("Vertical") < 0
                          || Input.GetAxis("Horizontal") > 0 
                          || Input.GetAxis("Horizontal") < 0;
        return result;
    }

	// Update is called once per frame
	void Update () {

        if(IsPlayerMoving())
        {
            _animationHandler.Walk();
        }else{
            _animationHandler.Stay();
        }
	}
}
