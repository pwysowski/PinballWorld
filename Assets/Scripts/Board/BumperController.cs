using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour, IBumper
{
    [SerializeField]
    private bool _active;
    [SerializeField]
    private float BumpValue;
    public Action<float> OnBump { get; set; }
    public bool Active { 
        get
        {
            return _active;
        }
        set
        {
            _active = value;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            OnBump?.Invoke(BumpValue);
        }
    }
}
