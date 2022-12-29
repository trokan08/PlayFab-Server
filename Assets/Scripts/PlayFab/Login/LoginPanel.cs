using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using TMPro;
namespace Login
{
    public partial class RegisterLogin : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _resetPasswordButton;
        [SerializeField] private Button _signUp;
        [SerializeField] private Button _loginStart;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private TMP_InputField _emailInputLogin;
        [SerializeField] private TMP_InputField _passwordInputLogin;
        [SerializeField] private TMP_InputField _emailInputRegister;
        [SerializeField] private TMP_InputField _passwordInputRegister;
        [SerializeField] private TMP_InputField _usernameInputRegister;

        [SerializeField] private GameObject _main;
        [SerializeField] private GameObject _register;
        [SerializeField] private GameObject _login;
        private void OnEnable()
        {
            _loginButton.onClick.AddListener(Login);
            _registerButton.onClick.AddListener(Register);
            _resetPasswordButton.onClick.AddListener(ResetPassword);
            _signUp.onClick.AddListener(OpenRegisterPanel);
            _loginStart.onClick.AddListener(OpenLoginPanel);
        }

        private void OnDisable()
        {
            _loginButton.onClick.RemoveListener(Login);
            _registerButton.onClick.RemoveListener(Register);
            _resetPasswordButton.onClick.RemoveListener(ResetPassword);
            _signUp.onClick.RemoveListener(OpenRegisterPanel);
            _loginStart.onClick.RemoveListener(OpenRegisterPanel);

        }

        private void DeActiveObject()
        {
            gameObject.SetActive(false);

        }

        private void OpenLoginPanel()
        {
            _main.SetActive(false);
            _login.SetActive(true);
        }

        private void OpenRegisterPanel()
        {
            _main.SetActive(false);
            _register.SetActive(true);
        }

    }
}