using UnityEngine;
using UnityEngine.EventSystems;

namespace ReMaz.MapEditor
{
    public class PlaceZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool CanPlace { get; private set; }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            CanPlace = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CanPlace = false;
        }
    }
}