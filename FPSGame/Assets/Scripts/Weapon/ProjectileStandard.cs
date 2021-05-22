using System;
using UnityEngine;

namespace Weapon
{
    public class ProjectileStandard : MonoBehaviour
    {
        public float maxLifeTime = 5f;
        public float speed = 300f;

        private ProjectileBase _projectileBase;
        private Vector3 _velocity;

        #region Unity Event Functions

        private void OnEnable()
        {
            _projectileBase = GetComponent<ProjectileBase>();
            _projectileBase.OnShoot += Shoot;
            Destroy(gameObject,maxLifeTime);
        }

        private void Update()
        {
            // move
            transform.position += _velocity * Time.deltaTime;
            
            // orient
            // transform.forward = _velocity.normalized;
        }

        #endregion

        private void Shoot()
        {
            _velocity = transform.forward * speed;
        }
    }
}