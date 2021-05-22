using System;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    /// <summary>
    /// 负责管理玩家的所有武器，包括切换武器，添加武器等
    /// </summary>
    public class PlayerWeaponManager : MonoBehaviour
    {
        public List<WeaponController> startingWeapons = new List<WeaponController>();
        public Transform weaponPosition;
        public UnityAction<WeaponController> OnSwitchToWeapon;
        private WeaponController[] _weaponSlots = new WeaponController[9];

        public WeaponController activeWeapon;


        #region Unity Event Functions

        private void Start()
        {
            OnSwitchToWeapon += OnWeaponSwitched;

            foreach (WeaponController weapon in startingWeapons)
            {
                AddWeapon(weapon);
            }

            SwitchWeapon();
        }

        private void Update()
        {
            // TODO : 修改为在输入里调用
            activeWeapon.HandleShootInput(PlayerInputHandler.Instance.GetFireInputHeld());
        }

        #endregion

        #region weapon switch
        
        private bool AddWeapon(WeaponController weaponController)
        {
            for (int index = 0; index < _weaponSlots.Length; index++)
            {
                WeaponController weaponInstance = Instantiate(weaponController, weaponPosition);
                weaponInstance.transform.localPosition = Vector3.zero;
                weaponInstance.transform.localRotation = Quaternion.identity;

                weaponInstance.Owner = gameObject;
                weaponInstance.SourcePrefab = weaponController.gameObject;
                weaponInstance.ShowWeapon(false);
                _weaponSlots[index] = weaponInstance;

                return true;
            }

            return false;
        }
        
        private void SwitchWeapon()
        {
            SwitchWeaponToIndex(0);
        }

        private void SwitchWeaponToIndex(short weaponIndex)
        {
            if (weaponIndex < 0) { return; }

            WeaponController newWeapon = GetWeaponAtSlot(weaponIndex);
            OnSwitchToWeapon?.Invoke(newWeapon);
        }

        private WeaponController GetWeaponAtSlot(short weaponIndex)
        {
            return weaponIndex >= _weaponSlots.Length ? null : _weaponSlots[weaponIndex];
        }

        private void OnWeaponSwitched(WeaponController weaponController)
        {
            if (!weaponController)
            {
                activeWeapon = null;
                return;
            }

            activeWeapon = weaponController;
            weaponController.ShowWeapon(true);
        }
        

        #endregion
    }
}