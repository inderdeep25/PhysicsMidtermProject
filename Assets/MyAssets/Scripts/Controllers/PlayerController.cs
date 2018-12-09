using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerAnimationHandler _animationHandler;
    private PhysicsController _physicsController;
    private Quaternion _targetRotation;
    private bool _isPlayerInStartPointOfAnyMovableObject;
    private MovableObjectController _objectToMove;

    [SerializeField] private UIHandler _uiHandler;

    private bool _isGaugeOpen = false;

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
        if(Input.GetKeyDown(KeyCode.Space) && _isPlayerInStartPointOfAnyMovableObject && _isGaugeOpen)
        {
            _uiHandler.SetPowerGauge(false);

            var percentageOfExactForceToApply = _uiHandler.GetAccuracyForCurrentGaugeValue() / 100f;
            var forceToApply = _objectToMove.GetExactForceRequiredToReachDestination() * percentageOfExactForceToApply;
            Debug.Log("Value From Gauge (int) -> " + forceToApply.ToString());

            _objectToMove.PushObject(forceToApply);
            _isGaugeOpen = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && _isPlayerInStartPointOfAnyMovableObject && !_isGaugeOpen)
        {
            Debug.Log("Should open gauge!");
            _uiHandler.SetPowerGauge(true);
            _isGaugeOpen = true;
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
            Debug.Log("Should enable Space");
            _isPlayerInStartPointOfAnyMovableObject = true;
            _objectToMove = otherCollider.GetComponentInParent<MovableObjectController>();
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.tag == "StartPoint")
        {
            Debug.Log("Should close gauge and disable Space!");
            _isPlayerInStartPointOfAnyMovableObject = false;
            _objectToMove = null;
            _uiHandler.SetPowerGauge(false);
            _uiHandler.ResetGauge();
            _isGaugeOpen = false;
        }
    }
}
