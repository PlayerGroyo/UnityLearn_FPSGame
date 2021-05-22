using System;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// 负责控制玩家当前手中的武器
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        public GameObject weaponRoot;
        public bool IsWeaponActive { get; private set; }
        public GameObject Owner { get; set; }
        public GameObject SourcePrefab { get; set; }

        public float shootInterval;
        private float _lastShootTime = 0;

        public Transform weaponMuzzlePos;
        public GameObject muzzleFlashPrefab;
        private ParticleSystem _muzzleParticle;

        public Vector3 MuzzleWorldVelocity { get; private set; }

        public ProjectileBase projectileBase;


        #region Unity Event functions

        private void Start()
        {
            if (!muzzleFlashPrefab)
            {
                Debug.LogWarning("muzzle prefab missing!");
            }
            else
            {
                _muzzleParticle = Instantiate(muzzleFlashPrefab, weaponMuzzlePos.position, weaponMuzzlePos.rotation,weaponMuzzlePos)
                    .GetComponent<ParticleSystem>();
            }
        }

        #endregion

        #region wenpon show

        /// <summary>
        /// show or hide weapon
        /// </summary>
        /// <param name="active">true -> show || false -> hide</param>
        public void ShowWeapon(bool active)
        {
            weaponRoot.SetActive(active);
            IsWeaponActive = active;
        }

        #endregion

        #region Fire

        public bool HandleShootInput(bool inputHeld)
        {
            if (inputHeld)
            {
                return TryShoot();
            }

            return false;
        }

        private bool TryShoot()
        {
            if (_lastShootTime + shootInterval <= Time.time)
            {
                HandleShoot();
                return true;
            }

            return false;
        }

        private void HandleShoot()
        {
            if (projectileBase)
            {
                Vector3 shootDirection = weaponMuzzlePos.forward;
                ProjectileBase newProjectileBase =
                    Instantiate(projectileBase, weaponMuzzlePos.position, weaponMuzzlePos.rotation)
                        .GetComponent<ProjectileBase>();
                
                newProjectileBase.Shoot(this);
            }
            _muzzleParticle.Play(true);

            _lastShootTime = Time.time;
        }

        #endregion
    }
}