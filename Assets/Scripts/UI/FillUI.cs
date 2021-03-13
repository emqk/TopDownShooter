using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillUI : MonoBehaviour
{
    [SerializeField] float imageYPercentOffset;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI amountText;

    Transform owner;
    Camera ownerCamera;

    public void Init(Transform _owner, Camera _ownerCamera)
    {
        owner = _owner;
        ownerCamera = _ownerCamera;
    }

    public void RefreshLocation()
    {
        Vector3 pos = ownerCamera.WorldToScreenPoint(owner.position);
        transform.position = new Vector3(pos.x, pos.y + imageYPercentOffset * Screen.height, pos.z);
    }

    public void Refresh(float amount, float fillPercentage)
    {
        fillImage.fillAmount = fillPercentage;
        amountText.text = amount.ToString();
    }
}
