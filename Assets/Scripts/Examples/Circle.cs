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
}
}
