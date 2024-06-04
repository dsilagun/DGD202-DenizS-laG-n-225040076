using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _smoothSpeed = 0.125f;

    [SerializeField] private Vector3 _lookOffset;

    private void LateUpdate()
    {
        var target = _target;
        
        var desiredPosition = target.position + target.TransformDirection(_positionOffset);
        
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        
        transform.position = smoothedPosition;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(target.eulerAngles + _lookOffset), _smoothSpeed * Time.deltaTime);
    }
}