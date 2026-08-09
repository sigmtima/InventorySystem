using UnityEngine;

namespace _Project.Scripts.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target; // Сюда перетащите объект Player
        [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f); // Смещение камеры (высота и дистанция)
        [SerializeField] private float smoothSpeed = 5f; // Плавность движения

        private void LateUpdate()
        {
            if (target == null) return;

            // Вычисляем позицию, где должна быть камера, без учета вращения игрока
            Vector3 desiredPosition = target.position + offset;
            // Плавно перемещаем камеру в эту точку
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}