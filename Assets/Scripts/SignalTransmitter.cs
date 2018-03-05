using System;
using System.Collections;
using System.Collections.Generic;
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

    private float _signalStrength;

    private List<HitData> _hitDatas = new List<HitData>();

    private struct HitData{
        public Collider collider;
        public Vector3 normal;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Reset variables
        _lastPosition = transform.position;
        _direction = -transform.right;
        _signalStrength = _startingSignalStrength;
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
                if (receiver)
                {
                    // If we hit goal, stop bouncing
                    receiver.Receive(_signalStrength);
                    AddBounceNodeAt(i, _lastPosition);
                    return;
                }

                // If collider has signal strength multiplier, use it
                SignalBouncer bouncer = hit.collider.gameObject.GetComponent<SignalBouncer>();
                if (bouncer)
                {
                    _signalStrength *= bouncer.BounceAmount;
                }

                // Reflect at same angle
                Vector3 oldDirection = _direction;
                _direction = Vector3.Reflect(_direction, hit.normal);

                // If angle is too small, stop bouncing
                if (180f - Vector3.Angle(_direction, oldDirection) < _minBounceAngle)
                {
                    AddBounceNodeAt(i, hit.point);
                    return;
                }

                var hitData = new HitData();
                hitData.collider = hit.collider;
                hitData.normal = hit.normal;

                // If we've already bounced off of this surface, stop bouncing
                if(_hitDatas.Exists(x => x.collider == hitData.collider && x.normal == hitData.normal)){
                    AddBounceNodeAt(i, hit.point);
                    print("already bounced off " + hit.collider.gameObject.name);
                    return;
                }
                _hitDatas.Add(hitData);

                // Restart from this point, add this point to line
                _lastPosition = hit.point;
                AddBounceNodeAt(i, _lastPosition);

                i++;

                // If signal strength is below 0 or we're over maxBounces, stop bouncing
                if (_signalStrength <= 0.5f || i >= _maxBounces)
                {
                    return;
                }
            }
        }
    }

    private void AddBounceNodeAt(int index, Vector3 pos){
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(index, pos);
    }
}
