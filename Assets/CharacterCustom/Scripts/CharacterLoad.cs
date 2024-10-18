using UnityEngine;

public class CharacterLoad : MonoBehaviour
{
    [SerializeField] private GameObject maleCharacter; // Référence au personnage masculin dans la scène
    [SerializeField] private GameObject femaleCharacter; // Référence au personnage féminin dans la scène

    [SerializeField] private Animator mainAnimator; // Référence à l'animateur principal dans la scène
    [SerializeField] private Avatar maleAvatar; // Référence à l'avatar masculin
    [SerializeField] private Avatar femaleAvatar; // Référence à l'avatar féminin

    [SerializeField] private Animator femaleAnim,Maleanim; // Référence à l'avatar féminin

    private void Start()
    {
        femaleAnim.runtimeAnimatorController = null;
        Maleanim.runtimeAnimatorController = null;

        // Vérifie si un personnage est sélectionné
        if (CharacterManager.Instance.CurrentCharacter != null)
        {
            // Désactive les deux personnages initialement
            maleCharacter.SetActive(false);
            femaleCharacter.SetActive(false);

            // Active le personnage approprié et assigne l'avatar correct
            if (CharacterManager.Instance.IsMale)
            {
                maleCharacter.SetActive(true);
                mainAnimator.avatar = maleAvatar; // Assigne l'avatar masculin
            }
            else
            {
                femaleCharacter.SetActive(true);
                mainAnimator.avatar = femaleAvatar; // Assigne l'avatar féminin
            }
        }
        else
        {
            Debug.LogError("Aucun personnage sélectionné !");
        }
    }
}
