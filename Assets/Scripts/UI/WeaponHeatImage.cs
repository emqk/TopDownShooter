using UnityEngine;
using UnityEngine.UI;

public class WeaponHeatImage : MonoBehaviour
{
    [SerializeField] Image heatFillImage;
    [SerializeField] Color coldColor;
    [SerializeField] Color warmColor;

    public void Refresh(float percentage01)
    {
        heatFillImage.fillAmount = percentage01;
        heatFillImage.color = Color.Lerp(coldColor, warmColor, percentage01);
    }
}
