using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Portal : MonoBehaviour
{
    private GameObject _player;
    private int count = 0;

    public string SceneName;
    public int DoorPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _player != null)
        {
            StartCoroutine(LoadYourAsyncScene(_player));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            count--;
            if (count == 0)
                _player = null;
        }
    }

    IEnumerator LoadYourAsyncScene(GameObject m_MyGameObject)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        {
            var newPlayers = SceneManager.GetSceneByName(SceneName).GetRootGameObjects().Where(n => n.CompareTag("Player"));
            if (newPlayers.Count() != 0)
            {
                m_MyGameObject.transform.position = newPlayers.First().transform.position;
                Destroy(newPlayers.First());
            }
        }

        var portals = SceneManager.GetSceneByName(SceneName).GetRootGameObjects()
            .Select(n => n.GetComponent<Portal>())
            .Where(n => n != null)
            .Where(n => n.DoorPoint == DoorPoint);

        if (portals.Count() != 0)
        {
            m_MyGameObject.transform.position = portals.First().transform.position;
        }
        else
        {
            m_MyGameObject.transform.position = new Vector3(0f, 0f);
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByName(SceneName));

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
