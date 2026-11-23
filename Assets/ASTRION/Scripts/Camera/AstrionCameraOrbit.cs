using UnityEngine;

namespace Astrion
{
    public sealed class AstrionCameraOrbit : MonoBehaviour
    {
        public Transform Target { get; set; }
        [SerializeField] private float distance = 6f;
        [SerializeField] private float height = 2.2f;
        [SerializeField] private float sensitivity = 3f;
        private float yaw;
        private float pitch = 18f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            if (Target == null) return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bool locked = Cursor.lockState == CursorLockMode.Locked;
                Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = locked;
            }
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                yaw += Input.GetAxis("Mouse X") * sensitivity;
                pitch -= Input.GetAxis("Mouse Y") * sensitivity;
                pitch = Mathf.Clamp(pitch, -15f, 65f);
            }
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 focus = Target.position + Vector3.up * height;
            transform.position = focus - rotation * Vector3.forward * distance;
            transform.rotation = rotation;
        }
    }
}
