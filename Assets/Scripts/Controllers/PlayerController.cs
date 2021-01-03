using System;
using System.Collections;
using Core;
using Objects;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnTransform;
        [SerializeField] private ForceMode2D forceMode;
        [SerializeField] private float playerMoveSpeed;
        [SerializeField] private float playerTurnSpeed;
        [SerializeField] private float playerMaxSpeed;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletFireRate;
        private bool _canFireBullet = true;
        private Rigidbody2D _rigidbody;
        private Bullet _currentBullet;

        private Vector2 _moveVector;

        private Vector2 _lookVector;

        // Start is called before the first frame update
        
        private void Start()
        {
            _rigidbody = player.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (player == null)
                return;
            
            _lookVector.x = Input.GetAxis("Horizontal");
            _moveVector.y = Input.GetKey(KeyCode.W) ? 1f : 0f;

            player.transform.Rotate(-player.transform.forward, _lookVector.x * playerTurnSpeed * Time.deltaTime);

            _rigidbody.AddForce(player.transform.TransformVector(_moveVector) * (playerMoveSpeed * Time.deltaTime), forceMode);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, playerMaxSpeed);

            if (Input.GetKeyDown(KeyCode.Space) && _canFireBullet)
            {
                StartCoroutine(nameof(BulletFired));
                _currentBullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity).GetComponent<Bullet>();
                _currentBullet.Fire(player.transform.up, bulletSpeed);
            }
        }

        private IEnumerator BulletFired()
        {
            _canFireBullet = false;
            yield return new WaitForSeconds(bulletFireRate);
            _canFireBullet = true;
        }
    }
}
