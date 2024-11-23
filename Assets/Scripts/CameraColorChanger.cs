using UnityEngine;

[ExecuteAlways]
public class CameraColorChanger : MonoBehaviour
{
    [SerializeField] private Color color;

    private void OnEnable()
    {
        Camera.main.backgroundColor = color;
    }
}
