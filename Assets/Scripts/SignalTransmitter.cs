using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SignalTransmitter : MonoBehaviour {

    private Vector3 _lastPosition;
    private Vector3 _direction;
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _startingSignalStrength = 100f;
    [SerializeField]
    private int _maxBounces = 100;
    [SerializeField]
    private float _minBounceAngle = 5f;
    [SerializeField]
    private ScoreDisplay _scoreDisplay;

    [SerializeField]
    private float _animationSpeed = 2f;

    private float _signalStrength;
    private bool _animationInProgress;

    private List<HitData> _hitDatas = new List<HitData>();
    private List<HitData> _lastHitDatas = new List<HitData>();

    private struct HitData{
        public Collider collider;
        public Vector3 normal;
        public Vector3 point;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update(){
        if(!_animationInProgress){
            RecalculateSignal();
        }
    }

    private void RecalculateSignal()
    {
        // Reset variables
        _lastPosition = transform.position;
        _direction = -transform.right;
        _signalStrength = _startingSignalStrength;
        _lastHitDatas = _hitDatas;
        _hitDatas.Clear();

        // Set line's first vertex to starting position
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _lastPosition);

        RaycastHit hit = new RaycastHit();
        int i = 1;

        while (true)
        {
            // If there's something in the way, bounce
            if (Physics.Raycast(_lastPosition, _direction, out hit))
            {
                SignalReceiver receiver = hit.collider.gameObject.GetComponent<SignalReceiver>();
                SignalBouncer bouncer = hit.collider.gameObject.GetComponent<SignalBouncer>();
                // Reflect at same angle
                Vector3 oldDirection = _direction;
                _direction = Vector3.Reflect(_direction, hit.normal);
                float reflectionAngle = 180f - Vector3.Angle(_direction, oldDirection);

                var hitData = new HitData();
                hitData.collider = hit.collider;
                hitData.normal = hit.normal;
                hitData.point = hit.point;
                bool alreadyBouncedOffThis = _hitDatas.Exists(x => x.collider == hitData.collider && x.normal == hitData.normal);
                _hitDatas.Add(hitData);

                if (bouncer)
                {
                    _signalStrength *= bouncer.BounceAmount;
                }

                AddBounceNodeAt(i, hit.point);

                if(receiver){
                    receiver.Receive(_signalStrength);
                    return;
                }

                if (reflectionAngle < _minBounceAngle || alreadyBouncedOffThis)
                {
                    return;
                }

                // Restart from this point, add this point to line
                _lastPosition = hit.point;
                i++;

                // If signal strength is below 0 or we're over maxBounces, stop bouncing
                if (_signalStrength <= 0.5f || i >= _maxBounces)
                {
                    return;
                }
            }
        }
    }

    [Button]
    public void Animate(){
        StartCoroutine(AnimateSignal());
    }
    private IEnumerator AnimateSignal(){
        _animationInProgress = true;

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float progress = 0f;
        Vector3 lastPos = transform.position;

        foreach (var data in _lastHitDatas)
        {
            _lineRenderer.positionCount++;
            while(progress < 1f){
                progress += Time.deltaTime / (data.point-lastPos).magnitude * _animationSpeed;
                var position = Vector3.Lerp(lastPos, data.point, progress);
                _lineRenderer.SetPosition(_lineRenderer.positionCount-1, position);
                yield return null;
            }
            progress = 0f;
            lastPos = data.point;
        }

        _animationInProgress = false;
    }

    private void AddBounceNodeAt(int index, Vector3 pos){
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(index, pos);
    }
}
