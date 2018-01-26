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

//lastPosition = transform.position;

//// Direction of movement
//velocity = transform.forward;

//line.positionCount = 1;
//line.SetPosition(0, lastPosition);

//RaycastHit hit = new RaycastHit();
//int i = 1;
//while (true) { 


//    // If there's something in our way, bounce (add new vertex)
//    if(Physics.Raycast(lastPosition, lastPosition + velocity, out hit)){
//        // Reflect velocity
//        velocity = Vector3.Reflect(velocity, hit.normal);

//        // Set lastPosition to where we hit
//        lastPosition = hit.point;

//        print(lastPosition);
//        Debug.DrawLine(lastPosition, lastPosition + velocity, Color.red);

//        line.positionCount += 1;
//        line.SetPosition(i, lastPosition);
//        lastPosition += velocity;
//        i++;
//    }
//    else {
//        break;
//    }
//}
