
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBottom : MonoBehaviour
{
    [SerializeField] string sceneName;

    private Button startButton;
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.interactable = true; // 设置按钮为可交互
        startButton.onClick.AddListener(OnButtonClick); // 添加点击事件监听器
    }

    void OnButtonClick()
    {
        // 通过场景名切换场景
        SceneManager.LoadScene(sceneName);
        Debug.Log("To LevelSwitch");
    }

}
