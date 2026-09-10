using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float m_scaleMultiplicator;
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale *= m_scaleMultiplicator;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale /= m_scaleMultiplicator;
    }
}
