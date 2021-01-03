
using Controllers;
using UnityEngine;

namespace Objects
{
    public class Asteroid : MonoBehaviour
    {
        private Collider2D _col;
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _col = GetComponent<Collider2D>();
            _col.isTrigger = false;
            _rigidbody = GetComponent<Rigidbody2D>();
            var randomVector = PlayerController.Instance.transform.position-transform.position;
            _rigidbody.AddForce(randomVector.normalized * Random.Range(1f,3f), ForceMode2D.Impulse);
        }

        private void Push(Vector2 dir)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(dir , ForceMode2D.Impulse);
        }
        
        public void Crushed(Vector2 hitPos)
        {
            if (transform.localScale.magnitude < 0.2f)
            {
                Destroy(gameObject);
                return ;
            }

            _col.isTrigger = true;
            var currentPos = (Vector2)transform.position;
            var leftAsteroidPos = Vector2.Perpendicular(hitPos - currentPos).normalized;
            var rightAsteroidPos = -leftAsteroidPos;

            var leftAsteroid = Instantiate(gameObject, currentPos + leftAsteroidPos, Quaternion.identity).GetComponent<Asteroid>();
            var rightAsteroid = Instantiate(gameObject, currentPos + rightAsteroidPos, Quaternion.identity).GetComponent<Asteroid>();

            leftAsteroid.Push(leftAsteroidPos);
            rightAsteroid.Push(rightAsteroidPos);
            
            leftAsteroid.transform.localScale *= 0.7f;
            rightAsteroid.transform.localScale *= 0.7f;
            
            var newAsteroid = Instantiate(gameObject, -currentPos*2, Quaternion.identity);
            newAsteroid.transform.localScale = Vector3.one * Random.Range(6f,10f);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                Destroy(other.gameObject);
                Debug.Log("GameOver!");
            }
        }
    }
}