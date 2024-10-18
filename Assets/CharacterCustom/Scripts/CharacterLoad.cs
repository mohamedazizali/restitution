using UnityEngine;

public class CharacterLoad : MonoBehaviour
{
    [SerializeField] private GameObject maleCharacter; // R�f�rence au personnage masculin dans la sc�ne
    [SerializeField] private GameObject femaleCharacter; // R�f�rence au personnage f�minin dans la sc�ne

    [SerializeField] private Animator mainAnimator; // R�f�rence � l'animateur principal dans la sc�ne
    [SerializeField] private Avatar maleAvatar; // R�f�rence � l'avatar masculin
    [SerializeField] private Avatar femaleAvatar; // R�f�rence � l'avatar f�minin

    [SerializeField] private Animator femaleAnim,Maleanim; // R�f�rence � l'avatar f�minin

    private void Start()
    {
        femaleAnim.runtimeAnimatorController = null;
        Maleanim.runtimeAnimatorController = null;

        // V�rifie si un personnage est s�lectionn�
        if (CharacterManager.Instance.CurrentCharacter != null)
        {
            // D�sactive les deux personnages initialement
            maleCharacter.SetActive(false);
            femaleCharacter.SetActive(false);

            // Active le personnage appropri� et assigne l'avatar correct
            if (CharacterManager.Instance.IsMale)
            {
                maleCharacter.SetActive(true);
                mainAnimator.avatar = maleAvatar; // Assigne l'avatar masculin
            }
            else
            {
                femaleCharacter.SetActive(true);
                mainAnimator.avatar = femaleAvatar; // Assigne l'avatar f�minin
            }
        }
        else
        {
            Debug.LogError("Aucun personnage s�lectionn� !");
        }
    }
}
