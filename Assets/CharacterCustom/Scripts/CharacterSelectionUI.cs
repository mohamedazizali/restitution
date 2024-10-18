using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionUI : MonoBehaviour
{
    /// <summary>
    /// S�lectionne le personnage masculin et charge la sc�ne de personnalisation.
    /// </summary>
    public void SelectMaleCharacter()
    {
        CharacterManager.Instance.SelectCharacter(CharacterManager.Instance.MaleCharacterPrefab);
        //LoadCustomizationScene();
    }

    /// <summary>
    /// S�lectionne le personnage f�minin et charge la sc�ne de personnalisation.
    /// </summary>
    public void SelectFemaleCharacter()
    {
        CharacterManager.Instance.SelectCharacter(CharacterManager.Instance.FemaleCharacterPrefab);
        //LoadCustomizationScene();
    }

    /// <summary>
    /// Charge la sc�ne de personnalisation.
    /// </summary>
    private void LoadCustomizationScene()
    {
        SceneManager.LoadScene("3");
    }
}
