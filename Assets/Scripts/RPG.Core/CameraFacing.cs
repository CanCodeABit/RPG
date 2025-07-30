using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
           // transform.LookAt(Camera.main.transform);
           // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.forward = Camera.main.transform.forward;
        }
    }
}
