using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {

    [SerializeField]
    private int maxVertices = 10;

    private Vector3 lastPosition;
    private Vector3 direction;
    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lastPosition = transform.position;
        direction = transform.forward;

        // Set line's first vertex to starting position
        line.positionCount = 1;
        line.SetPosition(0, lastPosition);

        RaycastHit hit = new RaycastHit();

        int i = 1;
        while (true)
        {
            if(Physics.Raycast(lastPosition, direction, out hit))
            {
                direction = Vector3.Reflect(direction, hit.normal);

                lastPosition = hit.point;

                line.positionCount++;
                line.SetPosition(i, lastPosition);
            }
            else
            {
                break;
            }

            lastPosition += direction;
            i++;
        }

        line.positionCount++;
        line.SetPosition(i, lastPosition + direction * 100);
    }
}
