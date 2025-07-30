using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
     
    }
}
