using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public ScrollRect scrollRect; // Перетащите ScrollRect сюда в Inspector
    public float scrollStep = 0.2f; // Размер шага прокрутки (0.2 = 20%)

    public void ScrollLeft()
    {
        scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition - scrollStep);
    }

    public void ScrollRight()
    {
        scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition + scrollStep);
    }
}
