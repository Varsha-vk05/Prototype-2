using UnityEngine;

public class ChangeMesh : MonoBehaviour
{

    [SerializeField] private MeshFilter modelYouWantToChange;
    [SerializeField] private Mesh modelYouWantToUse;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            modelYouWantToChange.mesh = modelYouWantToUse;
        }
    }

    public void ChangeMeshWithButton()
    {
        modelYouWantToChange.mesh = modelYouWantToUse;

    }
}
