using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadLevelDelayed(string level)
    {
        StartCoroutine(LoadEnumerator(level));
    }

    public IEnumerator LoadEnumerator(string level)
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(level);
    }
}
