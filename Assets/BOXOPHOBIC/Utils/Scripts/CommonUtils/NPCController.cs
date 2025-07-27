using UnityEngine;

namespace Boxophobic.Utils
{
    public class NpcController : MonoBehaviour
    {
        private float _timeToChangeDirection;
        Vector3 _direction;

        public void Start()
        {
            ChangeDirection();
        }

        public void Update()
        {
            _timeToChangeDirection -= Time.deltaTime;

            if (_timeToChangeDirection <= 0)
            {
                ChangeDirection();
            }

            transform.Translate(_direction, Space.World);
        }

        private void ChangeDirection()
        {
            var speed = Random.Range(0.005f, 0.01f);
            _direction = new Vector3(Random.Range(-1f, 1f) * speed, 0, Random.Range(-1f, 1f) * speed);
            _timeToChangeDirection = Random.Range(0.5f, 2f);
        }
    }
}