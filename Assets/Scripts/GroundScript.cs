using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public event EventHandler OnPlayerTouchGround;

    private void OnCollisionEnter(Collision collision) {
        OnPlayerTouchGround?.Invoke(this, EventArgs.Empty);
    }
}
