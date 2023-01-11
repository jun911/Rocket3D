using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    int currentSceneIndex;
    bool isFinishLanded;
    bool isTransitioning = false;
    
    Rocket rocket;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        rocket = GameManager.instance.rocket;
    }

    private void Update()
    {
        CheckLandedForNextLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                StartApproachFriendly();
                break;
            case "Fuel":
                StartFuelSquence(collision);
                break;
            case "Finish":
                StartSucessSquence();
                break;
            default:
                StartCrashSequence(collision);
                break;
        }
    }

    #region Squence
    private void StartSucessSquence()
    {
        isFinishLanded = true;
    }

    private void StartCrashSequence(Collision collision)
    {
        isTransitioning = true;

        rocket.PlaySoundCrashExpotion();
        rocket.PlayEffectExplotion();

        StopMovement();
        collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartFuelSquence(Collision collision)
    {
        isTransitioning = true;
        Destroy(collision.gameObject);
        isTransitioning = false;
    }

    private void StartApproachFriendly()
    {
        Debug.Log("you hit friendly!");
    } 
    #endregion


    private void StopMovement()
    {
        gameObject.GetComponent<Movement>().enabled = false;
    }
        
    private void CheckLandedForNextLevel()
    {
        if (isFinishLanded && rocket.state == Rocket.State.LANDED && !isTransitioning)
        {
            isTransitioning = true;

            rocket.PlaySoundSuccess();
            rocket.PlayEffectSuccess();

            StopMovement();
            Invoke("GoNextLevel", levelLoadDelay);
        }
    }

    private void GoNextLevel()
    {
        int nextSceneIndex = ++currentSceneIndex;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
