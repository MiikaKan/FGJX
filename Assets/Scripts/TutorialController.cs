using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    [SerializeField]
    private GameObject _introPanel;

    void Start()
    {

        LevelController levelController = (LevelController)FindObjectOfType(typeof(LevelController));
        if (levelController.tutorialRead)
        {
            _introPanel.SetActive(false);
        }
        levelController.SetTutorialRead();
    }

}
