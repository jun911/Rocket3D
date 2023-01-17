using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;

    private int currentSceneIndex;
    private bool isApproachFinish = false;
    private bool isTransitioning = false;
    private bool isCollisionDisabled = false;
    
    private Rocket rocket;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        rocket = GameManager.instance.rocket;
    }

    private void Update()
    {
        ProcessNextLevel();
        RespondToDebugKeys();
    }

    // cheat code
    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) { LoadNextLevel(); }
        if (Input.GetKeyDown(KeyCode.C)) { rocket.gameObject.GetComponent<Rigidbody>().useGravity = false; }
        if (Input.GetKeyDown(KeyCode.R)) { ReloadLevel(); }
        if (Input.GetKeyDown(KeyCode.V)) { isCollisionDisabled = !isCollisionDisabled; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCollisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;

            case "Fuel":
                StartFuelSequence(collision);
                break;

            case "Finish":
                StartSucessSequence();
                break;

            default:
                StartCrashSequence(collision);
                break;
        }
    }


    #region Sequence

    private void StartSucessSequence()
    {
        isApproachFinish = true;
    }

    private void StartCrashSequence(Collision collision)
    {
        isTransitioning = true;
        StopMovement();
        UpdateObstacle(collision);
        rocket.StartCrashSequence();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartFuelSequence(Collision other)
    {
        isTransitioning = true;
        StopMovement();
        rocket.StartSoundFuel();
        Destroy(other.gameObject);
        StartMovement();
        isTransitioning = false;
    }

    #endregion Sequence

    private void UpdateObstacle(Collision obstacle)
    {
        obstacle.gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
    }

    private void StopMovement()
    {
        gameObject.GetComponent<Movement>().enabled = false;
    }

    private void StartMovement()
    {
        gameObject.GetComponent<Movement>().enabled = true;
    }

    private void ProcessNextLevel()
    {
        if (!isApproachFinish) { return; }
        if (isTransitioning) { return; }
        if (rocket.state != Rocket.State.LANDED) { return; }

        isTransitioning = true;
        rocket.StartSucessSequence();
        StopMovement();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
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