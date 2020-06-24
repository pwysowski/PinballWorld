using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BumperType {
    DEFAULT,
    BOARD
}

public class BumperController : MonoBehaviour, IBumper
{
    [SerializeField]
    private bool _active;
    [SerializeField]
    private float BumpValue;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private BumperType bumperType;
    
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
        if (collision.gameObject.CompareTag("Ball") && bumperType == BumperType.DEFAULT)
        {
            OnBump?.Invoke(BumpValue);
            audioSource.Play();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball") && bumperType == BumperType.BOARD)
        {
            OnBump?.Invoke(BumpValue);
            audioSource.Play();
        }
    }
}
