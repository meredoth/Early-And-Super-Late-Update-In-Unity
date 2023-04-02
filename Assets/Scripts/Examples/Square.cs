using EarlyAndSuperLateUpdate;
using UnityEngine;

namespace Examples
{
public class Square : MonoBehaviour, IEarlyUpdate
{
    private void OnEnable()
    {
        UpdateManager.RegisterEarlyUpdate(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterEarlyUpdate(this);
    }

    private void Update()
    {
        Debug.Log("Inside square Update");
    }

    void IEarlyUpdate.EarlyUpdate()
    {
        Debug.Log("Inside Square Early Update");
    }
}
}
