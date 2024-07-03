using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDissaper : MonoBehaviour
{
    private MeshRenderer _meshR;
    private Collider _collider;
    private float _Blinkcount;
    [SerializeField] private bool isOntheFloor;
    [SerializeField] private float _timeBeforeBlink = 2;
    [SerializeField] private float _timeBeforeBlinkCount;
    [SerializeField] private float _timeBetweenBlink = 0.35f;
    [SerializeField] private float _timeBetweenBlinkCount;
    [SerializeField] private float _deadCount;
    [SerializeField] private int _timeToRevive;
    public Material matTransparent;
    public Material matSolid;
    private bool isSolid = true;
    private bool isDead;

    void Start()
    {
        _meshR = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }


    void Update()
    {
        if (isOntheFloor)
        {
            _timeBeforeBlinkCount += Time.deltaTime;
        }
        else
        {
            _timeBeforeBlinkCount = 0;
            _Blinkcount = 0;
        }

        if(_timeBeforeBlinkCount > _timeBeforeBlink)
        {
            Blink();
        }

        if (isDead)
        {
            _deadCount += Time.deltaTime;
            if(_deadCount > _timeToRevive)
            {
                _collider.enabled = true;
                _meshR.material = matSolid;
                isDead = true;
                _deadCount = 0;
            }
        }
    }


    void Blink() 
    {
        _timeBetweenBlinkCount += Time.deltaTime;

        if (isSolid)
        {
            _meshR.material = matTransparent;
            isSolid = false;

        }
        else
        {
            _meshR.material = matSolid;
            isSolid = true;
        }
        _timeBetweenBlinkCount = 0;
        _Blinkcount += 1;

        if (_Blinkcount > 5)
        {
            _meshR.material = matTransparent;
            _collider.enabled = false;
            isDead = true;
            isOntheFloor = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOntheFloor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOntheFloor = false;
    }
}
