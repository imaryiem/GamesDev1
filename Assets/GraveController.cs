using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Animator myCrate = null;
    [SerializeField] private bool CrateTrigger = false;
  

    private bool isOpen = false;
   

  

    private void Start()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CrateTrigger)
        {
            if (isOpen)
            {
                Debug.Log("Press E to get the key");
            }
            else if (!isOpen)
            {
                Debug.Log("Press F to open the Crate");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && CrateTrigger)
        {
            Debug.Log("Player left the Crate proximity");
            if (isOpen)
            {
                CloseCrate();
            }
        }
    }

    private void Update()
    {
        if (CrateTrigger && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            OpenCrate();
        }

       
    }

    private void OpenCrate()
    {
        myCrate.Play("openCrate", 0, 0.0f);
        isOpen = true;
       
    }

    private void CloseCrate()
    {
        myCrate.Play("closeCrate", 0, 0.0f);
        isOpen = false;
        
    }

    
}
