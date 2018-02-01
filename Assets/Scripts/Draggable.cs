using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

    private Vector3 _screenPoint;
    private Vector3 _offset;
    private GameConstants _gameConstants;
    private AudioClip _pickUpSound;
    private AudioClip _putDownSound;

    private void Start()
    {
        _pickUpSound = Resources.Load("PickUp") as AudioClip;
        _putDownSound = Resources.Load("PutDown") as AudioClip;
        _gameConstants = Resources.Load("GameConstants") as GameConstants;
    }

    private void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        AudioSource.PlayClipAtPoint(_pickUpSound, transform.position);
    }

    private void OnMouseOver()
    {
        gameObject.transform.Rotate(new Vector3(0, _gameConstants.RotationAngle, 0) * Input.GetAxis("Mouse ScrollWheel") * 10);
    }

    private void OnMouseUp()
    {
        AudioSource.PlayClipAtPoint(_putDownSound, transform.position);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
