using Managers;
using UnityEngine;

namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector2 dir, float force)
        {
            _rigidbody2D.AddForce(dir * force, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Asteroid")) return;
            
            
            other.collider.GetComponent<Asteroid>().Crushed(other.contacts[0].point);
            GameManager.Instance.Scored();
            Destroy(gameObject);
        }
    }
}