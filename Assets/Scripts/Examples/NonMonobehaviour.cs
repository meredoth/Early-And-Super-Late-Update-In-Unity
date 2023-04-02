using EarlyAndSuperLateUpdate;
using UnityEngine;

namespace Examples
{
public class NonMonobehaviour : IEarlyUpdate, ISuperLateUpdate
{
   void IEarlyUpdate.EarlyUpdate()
   {
      Debug.Log("Inside NonMonoBehaviour Early Update");
   }

   void ISuperLateUpdate.SuperLateUpdate()
   {
      Debug.Log("Inside NonMonoBehaviour Super Late Update");
   }
}
}