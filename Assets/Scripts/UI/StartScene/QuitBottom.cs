
using UnityEngine;
using UnityEngine.UI;

public class QuitBottom : MonoBehaviour
{
    private Button quitButton;

    void Start()
    {
        quitButton = GetComponent<Button>();
        quitButton.interactable = true; // 设置按钮为可交互
        quitButton.onClick.AddListener(OnButtonClick); // 添加点击事件监听器
    }

    void OnButtonClick()
    {
        UnityEditor.EditorApplication.isPlaying = false; // 编辑器环境下退出
        Application.Quit(); // 构建版本中退出
        Debug.Log("Game Quit!");
    }
}
