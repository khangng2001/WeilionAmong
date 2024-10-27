using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class FirebaseController : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject signInPanel;
    [SerializeField] private GameObject signUpPanel;
    [SerializeField] private GameObject forgotPanel;
    [SerializeField] private GameObject notificationPanel;
    [Header("Sign In")]
    [SerializeField] private TMP_InputField signInEmail;
    [SerializeField] private TMP_InputField signInPassWord;
    [Header("Sign Up")]
    [SerializeField] private TMP_InputField signUpUserName;
    [SerializeField] private TMP_InputField signUpEmail;
    [SerializeField] private TMP_InputField signUpPassWord;
    [SerializeField] private TMP_InputField signUpConPassWord;
    [Header("Forgot Password")]
    [SerializeField] private TMP_InputField forgotEmail;
    [Header("Notification")]
    [SerializeField] private TMP_Text notification_title;
    [SerializeField] private TMP_Text notification_massage;
    [Header("Remember")]
    [SerializeField] private Toggle remember;
    [Header("Event")]
    [SerializeField] private Button loginBtn;
    [SerializeField] private Button registerBtn;
    [SerializeField] private Button submitForgot;
    [SerializeField] private Button createAccountBtn;
    [SerializeField] private Button forgotPassBtn;
    [SerializeField] private Button back_signUpBtn;
    [SerializeField] private Button back_forgotUpBtn;
    [SerializeField] private Button closeNotifitBtn;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        OpenSignInPanel();
        CloseNotification();
        loginBtn.onClick.AddListener(LoginUser);
        registerBtn.onClick.AddListener(RigisterUser);
        submitForgot.onClick.AddListener(SubmitForgot);
        createAccountBtn.onClick.AddListener(OpenCreatAccount);
        forgotPassBtn.onClick.AddListener(OpenForgotPass);
        back_signUpBtn.onClick.AddListener(OpenSignInPanel);
        back_forgotUpBtn.onClick.AddListener(OpenSignInPanel);
        closeNotifitBtn.onClick.AddListener(CloseNotification);
    }
    public void OpenSignInPanel()
    {
        signInPanel.SetActive(true);
        signUpPanel.SetActive(false);
        forgotPanel.SetActive(false);
    }
    public void OpenCreatAccount()
    {
        signInPanel.SetActive(false);
        signUpPanel.SetActive(true);
        forgotPanel.SetActive(false);

    }
    public void OpenForgotPass()
    {
        signInPanel.SetActive(false);
        signUpPanel.SetActive(false);
        forgotPanel.SetActive(true);
    }
    public void LoginUser()
    {
        if (string.IsNullOrEmpty(signInEmail.text) && string.IsNullOrEmpty(signInPassWord.text))
        {
            ShowNotification("Error", "Fields Empty!\n" + "Please Input Details In All Fields");
            return;
        }
        else
        {
            SignInUser(signInEmail.text, signInPassWord.text);
            CloseNotification();
        }
    }
    public void RigisterUser()
    {
        if (string.IsNullOrEmpty(signUpUserName.text) && string.IsNullOrEmpty(signUpEmail.text) && string.IsNullOrEmpty(signUpPassWord.text) && string.IsNullOrEmpty(signUpConPassWord.text))
        {
            ShowNotification("Error", "Fields Empty!\n" + "Please Input Details In All Fields");
            return;
        }
        else
        {
            CreateUser(signUpEmail.text, signUpPassWord.text, signUpUserName.text);
            CloseNotification();
        }
    }
    public void SubmitForgot()
    {
        if (string.IsNullOrEmpty(forgotEmail.text))
        {
            ShowNotification("Error", "Forget Email Empty!\n"+ "Please Input Details In All Fields");
            return;
        }
        else
        {
            CloseNotification();
        }
    }
    private void ShowNotification(string title, string masssage)
    {
        notification_title.text = "" + title;
        notification_massage.text = "" + masssage;
        notificationPanel.SetActive(true);
    }
    public void CloseNotification()
    {
        notification_title.text = "";
        notification_massage.text = "";
        notificationPanel.SetActive(false);
    }

    void CreateUser(string email, string password, string UserName) 
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            UpdateUserProfile(UserName);
        });
    }
    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            Debug.Log("UserName: " + user.DisplayName);
            Debug.Log("UID: " + user.UserId);

            Singleton<InformationPlayer>.Instance.LoadData(new Information() { Uid = user.UserId, Name = user.DisplayName});
        });
    }
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
    void UpdateUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = UserName,
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
                ShowNotification(UserName, "Account successfully Created!");
            });
        }
    }
}
