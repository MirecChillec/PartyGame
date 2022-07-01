using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public GameObject loadingScreen;
    public float waitSceenTime;
    List<AsyncOperation> operations = new List<AsyncOperation>();
    public bool loading = false;
    void Awake()
    {
        GameData.sceneManager = this;
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
    public void LoadScene(string newScene, string curScene)
    {
        loading = true;
        loadingScreen.gameObject.SetActive(true);
        operations.Add(SceneManager.UnloadSceneAsync(curScene));
        operations.Add(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive));
        StartCoroutine(LoadingProgress());
        Time.timeScale = 1;
    }

    public IEnumerator LoadingProgress()
    {
        for (int i = 0; i < operations.Count; i++)
        {
            while (!operations[i].isDone)
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(waitSceenTime);
        loadingScreen.gameObject.SetActive(false);
        loading = false;
    }
}
