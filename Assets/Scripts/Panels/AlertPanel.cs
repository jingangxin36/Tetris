using UnityEngine.UI;

public class AlertPanel : BasePanel {
    public Button closeButton;
    
    void Awake() {
        Init();

    }


    public override BasePanel Show() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        return this;
    }

    public override void Init() {
        panelType = 4;
        closeButton.onClick.AddListener(() => UIManager.Instance.SetClose(this));
    }

    public override void Hide() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    public override void Destroy() {
    }
}
