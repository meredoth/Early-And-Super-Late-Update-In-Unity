using EarlyAndSuperLateUpdate;
using UnityEngine;

namespace Examples
{
public class Circle : MonoBehaviour
{
   private NonMonobehaviour _nonMonobehaviour;

   private void Awake()
   {
      _nonMonobehaviour = new NonMonobehaviour();
   }

   private void OnEnable()
   {
      UpdateManager.RegisterEarlyUpdate(_nonMonobehaviour);
      UpdateManager.RegisterSuperLateUpdate(_nonMonobehaviour);
   }

   private void OnDisable()
   {
      UpdateManager.UnregisterEarlyUpdate(_nonMonobehaviour);
      UpdateManager.UnregisterSuperLateUpdate(_nonMonobehaviour);
   }
}
}
