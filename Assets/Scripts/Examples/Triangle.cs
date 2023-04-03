using EarlyAndSuperLateUpdate;
using UnityEngine;

namespace Examples
{
public class Triangle : MonoBehaviour, ISuperLateUpdate
{
    private void OnEnable()
    {
        UpdateManager.RegisterSuperLateUpdate(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterSuperLateUpdate(this);
    }

    private void LateUpdate()
    {
        Debug.Log("Inside Triangle Late Update");
        transform.Rotate(Vector3.forward, 120f * Time.deltaTime);
    }

    void ISuperLateUpdate.SuperLateUpdate()
    {
        Debug.Log("Inside Triangle Super Late Update");
        transform.Rotate(Vector3.forward, -60f * Time.deltaTime);
    }
}
}
