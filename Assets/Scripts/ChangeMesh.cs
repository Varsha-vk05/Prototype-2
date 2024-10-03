using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChangeMesh : MonoBehaviour
{
    [SerializeField] private MeshFilter modelYouWantToChange;
    [SerializeField] private Mesh[] modelYouWantToUse;

    private int currentModel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            modelYouWantToChange.mesh = modelYouWantToUse[currentModel];
            currentModel++;
        }

        if (currentModel >= modelYouWantToUse.Length)
        {
            currentModel = 0;
        }
    }

    
}
