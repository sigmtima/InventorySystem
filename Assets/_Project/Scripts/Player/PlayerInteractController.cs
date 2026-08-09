using _Project.Scripts.Core;
using _Project.Scripts.Inventory;
using Input;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player
{
    public class PlayerInteractController : MonoBehaviour
    {
        [SerializeField] private Transform interactOrigin;
        [SerializeField] private float viewRadius;
        [SerializeField] private LayerMask interactLayer;
        [SerializeField, Range(0f, 180f)] private float interactAngle = 60f;
        public event System.Action<ItemData> OnCollect;
        private InputManager _inputManager;

        [Inject]
        public void Construct(InputManager inputManager)
        {
            inputManager.OnInteract += TryInteract;
            _inputManager = inputManager;
        }

        public void OnDisable()
        {
            _inputManager.OnInteract -= TryInteract;
        }

        private void TryInteract()
        {
            Collider[] results = Physics.OverlapSphere(interactOrigin.position, viewRadius, interactLayer);

            ICollectible closestInteractable = null;
            float minDistance = float.MaxValue;

            foreach (Collider item in results)
            {
                Vector3 direction = item.transform.position - transform.position;
                direction.y = 0;
                direction = direction.normalized;

                float angle = Vector3.Angle(transform.forward, direction);

                if (angle <= interactAngle)
                {
                    Vector3 startPos = transform.position + Vector3.up * 0.5f;
                    Vector3 endPos = item.transform.position + Vector3.up * 0.5f;

                    if (Physics.Linecast(startPos, endPos, out RaycastHit hit, interactLayer))
                    {
                        if (hit.collider != item)
                        {
                            continue;
                        }
                    }

                    float distance = Vector3.Distance(transform.position, item.transform.position);

                    if (distance < minDistance)
                    {
                        if (item.TryGetComponent(out ICollectible interactable))
                        {
                            minDistance = distance;
                            closestInteractable = interactable;
                        }
                    }
                }
            }

            if (closestInteractable != null)
            {
                ItemData data = closestInteractable.Collect();
                OnCollect?.Invoke(data);
            }
        }
    }
}

