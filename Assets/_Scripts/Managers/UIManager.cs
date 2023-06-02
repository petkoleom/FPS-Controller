using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateVelocity(float _value)
    {
        velocityText.text = _value.ToString("f1");
    }

    public void UpdateState(PlayerState _state)
    {
        stateText.text = _state.ToString();
    }

    public void UpdateAmmo(string _value)
    {
        ammoText.text = _value;
    }

}
