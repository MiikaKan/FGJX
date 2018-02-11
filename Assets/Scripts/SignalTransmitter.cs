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

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lastPosition = transform.position;
        _direction = -transform.right;
        _signalStrength = _startingSignalStrength;

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
                    _lineRenderer.positionCount++;
                    _lineRenderer.SetPosition(i, hit.point);
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
                    _lineRenderer.positionCount++;
                    _lineRenderer.SetPosition(i, hit.point);
                    return;
                }

                // Restart from this point, add this point to line
                _lastPosition = hit.point;
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(i, _lastPosition);

                // If signal strength is below 0, stop bouncing
                if (_signalStrength <= 0.5f)
                {
                    return;
                }
            }
            else
            {
                break;
            }

            i++;

            if(i >= _maxBounces)
            {
                break;
            }
        }

        // Set last line to be long (to appear to go on forever
        //_lineRenderer.positionCount++;
        //_lineRenderer.SetPosition(i, _lastPosition + _direction * 100);

        Debug.DrawRay(_lastPosition, Vector3.up, Color.yellow);
    }
}
