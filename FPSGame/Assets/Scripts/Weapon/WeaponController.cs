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

        #region MyFunctions

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
    }
}