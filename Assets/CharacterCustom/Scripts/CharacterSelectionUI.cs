using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionUI : MonoBehaviour
{
    /// <summary>
    /// Sélectionne le personnage masculin et charge la scène de personnalisation.
    /// </summary>
    public void SelectMaleCharacter()
    {
        CharacterManager.Instance.SelectCharacter(CharacterManager.Instance.MaleCharacterPrefab);
        //LoadCustomizationScene();
    }

    /// <summary>
    /// Sélectionne le personnage féminin et charge la scène de personnalisation.
    /// </summary>
    public void SelectFemaleCharacter()
    {
        CharacterManager.Instance.SelectCharacter(CharacterManager.Instance.FemaleCharacterPrefab);
        //LoadCustomizationScene();
    }

    /// <summary>
    /// Charge la scène de personnalisation.
    /// </summary>
    private void LoadCustomizationScene()
    {
        SceneManager.LoadScene("3");
    }
}
