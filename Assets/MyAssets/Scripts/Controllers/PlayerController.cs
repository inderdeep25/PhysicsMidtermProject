using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerAnimationHandler _animationHandler;
    private PhysicsController _physicsController;
    private Quaternion _targetRotation;
    private bool _isPlayerInStartPointOfAnyMovableObject;
    private MovableObjectController _objectToMove;

    // Use this for initialization
    void Start () {
        _animationHandler = GetComponent<PlayerAnimationHandler>();
        _animationHandler.Stay();

        _physicsController = GetComponent<PhysicsController>();

        _isPlayerInStartPointOfAnyMovableObject = false;
        _objectToMove = null;

    }
	
    bool IsPlayerMoving(){
        var result = Input.GetAxis("Vertical") > 0 
                          || Input.GetAxis("Vertical") < 0
                          || Input.GetAxis("Horizontal") > 0 
                          || Input.GetAxis("Horizontal") < 0;
        return result;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isPlayerInStartPointOfAnyMovableObject){
            //var minimumForceRequired = _objectToMove.GetMinimumForceRequired();
            //_objectToMove.PushObject(minimumForceRequired + 220);
            _objectToMove.PushObject(_objectToMove.GetExactForceRequiredToReachDestination());
        }
    }

    void FixedUpdate () {
    
        if (IsPlayerMoving())
        {
            _animationHandler.Walk();
        }
        else{
            _animationHandler.Stay();
        }
	}

    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.tag == "StartPoint"){
            _isPlayerInStartPointOfAnyMovableObject = true;
            _objectToMove = otherCollider.GetComponentInParent<MovableObjectController>();
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.tag == "StartPoint")
        {
            _isPlayerInStartPointOfAnyMovableObject = false;
            _objectToMove = null;
        }
    }
}
