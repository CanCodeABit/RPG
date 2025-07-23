using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private GameObject target;

        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.transform.position;
            //transform.LookAt(this.transform.position);
        }
    }
}
