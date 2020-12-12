using UnityEngine;
using UnityEngine.UI;

public abstract class BaseModalMessage : MonoBehaviour
{
    [SerializeField] private Button backgroundButton;

    void Start()
    {
        backgroundButton.onClick.AddListener(() => Destroy(this.gameObject));
    }
}
