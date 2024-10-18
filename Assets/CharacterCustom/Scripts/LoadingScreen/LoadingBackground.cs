using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBackground : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    /// <summary>
    /// Démarre le chargement de la scène spécifiée.
    /// </summary>
    /// <param name="sceneId">L'identifiant de la scène à charger.</param>
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    /// <summary>
    /// Charge la scène de manière asynchrone et met à jour la barre de chargement.
    /// </summary>
    /// <param name="sceneId">L'identifiant de la scène à charger.</param>
    /// <returns>Un IEnumerator pour le chargement asynchrone.</returns>
    IEnumerator LoadSceneAsync(int sceneId)
    {
        // Démarre le chargement asynchrone de la scène
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        // Active l'écran de chargement
        LoadingScreen.SetActive(true);

        // Pendant que la scène se charge
        while (!operation.isDone)
        {
            // Met à jour la barre de chargement en fonction des progrès
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            // Attends le prochain frame
            yield return null;
        }
    }
}
