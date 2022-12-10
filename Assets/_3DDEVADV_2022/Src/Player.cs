using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.OnGameWon += GameWon;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnGameWon -= GameWon;
    }

    void GameWon()
    {
        CharacterController charController = GetComponent<CharacterController>();
        if (charController != null) charController.enabled = false;

        StarterAssets.FirstPersonController fpsController = GetComponent<StarterAssets.FirstPersonController>();
        if (fpsController != null) fpsController.enabled = false;
    }
}