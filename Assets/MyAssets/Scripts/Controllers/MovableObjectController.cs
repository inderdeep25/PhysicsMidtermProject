using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectController : MonoBehaviour
{
    private readonly float _timeForWhichForceShouldBeApplied = 2.5f;

    private float _pushForce = 0;
    private float _acceleration = 0.0f;
    private float _timeElapsed = 0.0f;
    private float _minimumForceRequired;
    private float _frictionalForce;

    private bool _goalAchieved = false;
    private bool _isObjectInMotion = false;

    private Vector3 _initialPosition;

    private Rigidbody _rigidBody = null;
    private GameManager _gameManager;

    [SerializeField] private Transform destination;
    [SerializeField] private GameObject _playerPlatform;

    public bool hasEnteredDestination = false;
    public bool hasExitedDestination = false;

    public void ResetObject(){
        _playerPlatform.SetActive(true);
        hasEnteredDestination = false;
        hasExitedDestination = false;
    }

    public float GetExactForceRequiredToReachDestination(){
        _minimumForceRequired = CalculateMinimumForceRequired();
        float result = _minimumForceRequired + _frictionalForce;
        return result;
    }

    public float GetMinimumForceRequired(){
        return _minimumForceRequired;
    }

    public void PushObject(float force)
    {
        if(!_goalAchieved)
        {
            if (!_isObjectInMotion)
            {
                _initialPosition = this.transform.position;
                _playerPlatform.SetActive(false);
                _isObjectInMotion = true;
                _pushForce = force;
                _timeElapsed = 0.0f;
            }
            else
            {
                Debug.Log("Not applying force as object is already in motion!");
            }
        }else{
            Debug.Log("Not applying force as object is already reached destination!");
        }

    }

    private float CalculateAcceleration()
    {
        if (_timeForWhichForceShouldBeApplied <= 0.0f)
            return 0.0f;

        float Vi = _rigidBody.velocity.z;
        float d = Mathf.Abs(destination.position.z - transform.position.z);
        float a = (d - (Vi * _timeForWhichForceShouldBeApplied)) / (0.5f * Mathf.Pow(_timeForWhichForceShouldBeApplied, 2.0f));

        return a;
    }

    private float CalculateMinimumForceRequired()
    {
        float result;

        _acceleration = CalculateAcceleration();
        float groundDynamicFriction = GameObject.Find("Ground").GetComponent<Collider>().material.dynamicFriction;
        float groundStaticFriction = GameObject.Find("Ground").GetComponent<Collider>().material.staticFriction;
        float objDynamicFriction = GetComponent<Collider>().material.dynamicFriction;
        float objStaticFriction = GetComponent<Collider>().material.staticFriction;

        float dynamicfrictionAvg = (objDynamicFriction + groundDynamicFriction) / 2.0f;
        _frictionalForce = dynamicfrictionAvg * _rigidBody.mass * Physics.gravity.magnitude;  //Fk = umg
        float normalForce = _rigidBody.mass * _acceleration;   //F = ma

        Debug.Log("Force of Friction -> " + _frictionalForce.ToString());
        Debug.Log("Force of Normal -> " + normalForce.ToString());

        result = normalForce + _frictionalForce;   //Fapp = ma + Fk

        return result;
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _minimumForceRequired = CalculateMinimumForceRequired();
        _initialPosition = this.transform.position;
        hasExitedDestination = false;
        hasEnteredDestination = false;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!_isObjectInMotion)
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (_isObjectInMotion)
        {

            _timeElapsed += Time.fixedDeltaTime;

            _rigidBody.AddForce(transform.forward * _pushForce, ForceMode.Force);

            if (_timeElapsed > _timeForWhichForceShouldBeApplied)
            {
                _isObjectInMotion = false;

                if (hasEnteredDestination && !hasExitedDestination){
                    _goalAchieved = true;
                    _gameManager.IncrementSetsCompleted();
                    var material = this.gameObject.GetComponent<MeshRenderer>().materials[0];
                    _playerPlatform.GetComponent<BoxCollider>().isTrigger = false;
                    material.color = Color.green;
                }
                else if(hasEnteredDestination && hasExitedDestination){
                    this.transform.position = _initialPosition;
                    ResetObject();
                }
                else{
                    ResetObject();
                }
            }

        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.tag == "Destination"){
            hasEnteredDestination = true;
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.tag == "Destination")
        {
            hasExitedDestination = true;
        }
    }

}