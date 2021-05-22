using UnityEngine;

namespace Weapon
{
    public class ProjectileStandard : MonoBehaviour
    {
        public Transform root;
        public Transform tip;
        public float radius = 0.01f;
        public LayerMask hittableLayers = -1;
        private Vector3 _spawnPoint;
        
        
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
            
            // hit detection
            RaycastHit closestHit = new RaycastHit();
            closestHit.distance = Mathf.Infinity;
            bool foundHit = false;
            
            //SphereCastAll
            Vector3 displacementSinceLastFrame = tip.position - _spawnPoint;
            
            RaycastHit[] hits = Physics.SphereCastAll(_spawnPoint, radius,
                displacementSinceLastFrame.normalized,
                displacementSinceLastFrame.magnitude,
                hittableLayers,
                QueryTriggerInteraction.Collide);

            foreach (var hit in hits)
            {
                if (IsHitValid(hit) && hit.distance < closestHit.distance)
                {
                    closestHit = hit;
                    foundHit = true;
                }
            }

            if (foundHit)
            {
                if (closestHit.distance <= 0)
                {
                    closestHit.point = root.position;
                    closestHit.normal = -transform.forward;
                }

                OnHit();
            }
        }

        private void OnHit()
        {
            print("hit");
            Destroy(gameObject);
        }

        private bool IsHitValid(RaycastHit hit)
        {
            return !hit.collider.isTrigger;
        }

        #endregion

        private void Shoot()
        {
            _spawnPoint = root.position;
            _velocity = transform.forward * speed;
        }
    }
}