using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField idInput;
    public InputField pwInput;
    public Button LogInButton;


    private void Start()
    {
        LogInButton.onClick.AddListener(LoginButtonClick);
    }

    public void LoginButtonClick()
    {
        DataBaseManager.Instance.LogIn(idInput.text, pwInput.text, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess()
    {
        Debug.Log("로그인 성공");
    }

    private void OnLoginFailure()
    {
        Debug.Log("로그인 실패");
    }
   

}
