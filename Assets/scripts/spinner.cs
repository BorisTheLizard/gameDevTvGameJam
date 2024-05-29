using UnityEngine;

public class spinner : MonoBehaviour
{
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;
    void Update()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
