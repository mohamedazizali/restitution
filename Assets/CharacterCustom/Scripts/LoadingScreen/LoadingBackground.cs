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
    /// D�marre le chargement de la sc�ne sp�cifi�e.
    /// </summary>
    /// <param name="sceneId">L'identifiant de la sc�ne � charger.</param>
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    /// <summary>
    /// Charge la sc�ne de mani�re asynchrone et met � jour la barre de chargement.
    /// </summary>
    /// <param name="sceneId">L'identifiant de la sc�ne � charger.</param>
    /// <returns>Un IEnumerator pour le chargement asynchrone.</returns>
    IEnumerator LoadSceneAsync(int sceneId)
    {
        // D�marre le chargement asynchrone de la sc�ne
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        // Active l'�cran de chargement
        LoadingScreen.SetActive(true);

        // Pendant que la sc�ne se charge
        while (!operation.isDone)
        {
            // Met � jour la barre de chargement en fonction des progr�s
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            // Attends le prochain frame
            yield return null;
        }
    }
}
