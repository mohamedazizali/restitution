using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigametrigger : MonoBehaviour
{
    [SerializeField] GameObject shooterUI;
    // Start is called before the first frame update


    private bool inRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    public void ShowPopup()
    {
        shooterUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Method to hide the pop-up UI
    
    public void HidePopup()
    {
        
        shooterUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    
    private void Update()
    {
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {
            
            ShowPopup();
            
        }
    }
}

