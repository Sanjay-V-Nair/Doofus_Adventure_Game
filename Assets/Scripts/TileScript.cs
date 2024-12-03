using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TileScript : MonoBehaviour
{
    //public float initialtime = 5;
    [SerializeField] private ScriptableObjectTemplate env_values;
    private float tileCountDown;


    public static event EventHandler OnEnterNewTile;
    public static event EventHandler OnExitNewTile;

    [SerializeField]private TMP_Text tileCountDownText;

    private void Start() {
        tileCountDown = env_values.tileLife;
    }
    private void Update() {
        tileCountDown -= Time.deltaTime;
        
        tileCountDown = Mathf.Clamp(tileCountDown, 0, Mathf.Infinity);
        if (tileCountDown <= 0) tileCountDown = env_values.tileLife;
        tileCountDownText.text = string.Format("{0:00.00}", tileCountDown);
    }

    private void OnCollisionEnter(Collision collision) {
        OnEnterNewTile?.Invoke(this, EventArgs.Empty);
    }
    private void OnCollisionExit(Collision collision) {
        OnExitNewTile?.Invoke(this, EventArgs.Empty);
    }
}
