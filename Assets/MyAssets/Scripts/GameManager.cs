using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject _finalSet;
    [SerializeField] private UIHandler _uiHandler;

    private int _numberOfSetsCompleted = 0;

    public void IncrementSetsCompleted(){
        _numberOfSetsCompleted += 1;

        if(_numberOfSetsCompleted >= 3){
            _finalSet.SetActive(true);
            if(_numberOfSetsCompleted >=4 ){
                _uiHandler.ShowGameWonDialog();
            }
        }
    }

}
