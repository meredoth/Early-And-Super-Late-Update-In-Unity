using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace EarlyAndSuperLateUpdate
{
public static class UpdateManager
{
   private static readonly HashSet<IEarlyUpdate> _earlyUpdates = new();
   private static readonly HashSet<ISuperLateUpdate> _superLateUpdates = new();

   public static void RegisterEarlyUpdate(IEarlyUpdate earlyUpdate) => _earlyUpdates.Add(earlyUpdate);

   public static void RegisterSuperLateUpdate(ISuperLateUpdate superLateUpdate) => _superLateUpdates.Add(superLateUpdate);

   public static void UnregisterEarlyUpdate(IEarlyUpdate earlyUpdate) => _earlyUpdates.Remove(earlyUpdate);

   public static void UnregisterSuperLateUpdate(ISuperLateUpdate superLateUpdate) => _superLateUpdates.Remove(superLateUpdate);

   [RuntimeInitializeOnLoadMethod]
   private static void Init()
   {
      var defaultSystems = PlayerLoop.GetDefaultPlayerLoop();

      var mySuperLateUpdate = new PlayerLoopSystem
      {
         subSystemList = null,
         updateDelegate = OnSuperLateUpdate,
         type = typeof(MySuperLateUpdate)
      };
      
      var myEarlyUpdate = new PlayerLoopSystem
      {
         subSystemList = null,
         updateDelegate = OnEarlyUpdate,
         type = typeof(MyEarlyUpdate)
      };

      var loopWithSuperLateUpdate = AddSystem<PreLateUpdate>(in defaultSystems, mySuperLateUpdate);
      var loopWithEarlyAndSuperLateUpdate = AddSystem<PreUpdate>(in loopWithSuperLateUpdate, myEarlyUpdate);
      PlayerLoop.SetPlayerLoop(loopWithEarlyAndSuperLateUpdate);
      
      static PlayerLoopSystem AddSystem<T>(in PlayerLoopSystem loopSystem, PlayerLoopSystem systemToAdd) where T : struct
      {
         PlayerLoopSystem newPlayerLoop = new()
         {
            loopConditionFunction = loopSystem.loopConditionFunction,
            type = loopSystem.type,
            updateDelegate = loopSystem.updateDelegate,
            updateFunction = loopSystem.updateFunction
         };

         List<PlayerLoopSystem> newSubSystemList = new();

         foreach (var subSystem in loopSystem.subSystemList)
         {
            newSubSystemList.Add(subSystem);
         
            if (subSystem.type == typeof(T))
               newSubSystemList.Add(systemToAdd);
         }

         newPlayerLoop.subSystemList = newSubSystemList.ToArray();
         return newPlayerLoop;
      }
   }
   
   private static void OnEarlyUpdate()
   {
      using var e = _earlyUpdates.GetEnumerator();
      while (e.MoveNext())
      {
         e.Current?.EarlyUpdate();
      }
   }

   private static void OnSuperLateUpdate()
   {
      using var e = _superLateUpdates.GetEnumerator();
      while (e.MoveNext())
      {
         e.Current?.SuperLateUpdate();
      }
   }
}
}
