using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public class BackedServiceHandler : MonoBehaviour
{
    public static BackedServiceHandler instance;

    public InputField emailInputField;
    public TMP_Text validationMessageText;
    public TMP_Text otpErrorMessageText;
    public InputField otpInputField;
    public TMP_Text insufMessageText;

    private string email;


    void Awake()
    {
        instance = this;
    }

   

    public void ValidateEmailAndRequestOTP()
    {
        string email = emailInputField.text;
        if (IsEmailValid(email))
        {
            StartCoroutine(SendOTPTask(email));
        }
        else
        {
            validationMessageText.text = "Invalid email format. Please enter a valid email.";
        }
    }

    private bool IsEmailValid(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    IEnumerator SendOTPTask(string email)
    {
        string url = $"https://app.startuped.ai/api/User/GetOTP?Email={email}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending OTP: " + request.error);
            }
            else
            {
                Debug.Log("OTP sent successfully.");
                MenuManager.instance.LoginUI.SetActive(false);
                MenuManager.instance.OTPUIBackground.SetActive(true);
                //MenuManager.instance;
            }
        }
    }


    public void VerifyOTP()
    {
        string email = emailInputField.text;
        string otp = otpInputField.text;
        StartCoroutine(VerifyOTPTask(email, otp));
    }


    IEnumerator VerifyOTPTask(string email, string otp)
    {
        string url = $"https://app.startuped.ai/api/User/VerifyOTP?Email={email}&OTP={otp}";

       // ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

         //   ServicePointManager.ServerCertificateValidationCallback -= (sender, certificate, chain, sslPolicyErrors) => true;

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error verifying OTP: " + request.error);
                otpErrorMessageText.text = "Wrong OTP";
            }
            else
            {
                string responseText = request.downloadHandler.text;
                if (responseText.Contains("User with email"))
                {
                    // Debug.Log("OTP verification failed.");  
                    otpErrorMessageText.text = "Enter Correct OTP";
                }
                else
                {
                    Debug.Log("OTP verification successful.");
                    MenuManager.instance.EnterDetailsUI.SetActive(true);
                    MenuManager.instance.OTPUIBackground.SetActive(false);
                    //MenuManager.instance.playUIParent.SetActive(true);
                    MenuManager.instance.mainmenuUI.SetActive(true);
                    MenuManager.instance.VideoPanel.SetActive(true);
                    wallet();
                    StartVideo.instance.PlayStart();
                }
            }
        }
    }


    public Text walletAmountText;
    // public string apiEndpoint = "https://wizar.startuped.xyz/api/v1/Wallet/Email/";

    public int UpdateAmount;

    public void wallet()
    {
        StartCoroutine(FetchWalletAmount());
    }

    IEnumerator FetchWalletAmount()
    {
        string email = emailInputField.text;
        string apihitpoint = "https://admin.wizar.io/api/v1/Wallet/Email/" + email;
        Debug.Log(apihitpoint + email);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apihitpoint))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {

                string jsonResponse = webRequest.downloadHandler.text;
                //string mockjsonResponse = @"{}";

                WalletData wallets = JsonConvert.DeserializeObject<WalletData>(jsonResponse);
                UpdateAmount = wallets.Wallets["Ipt"];
                walletAmountText.text = "$" + UpdateAmount.ToString();

            }
        }
    }

   
    [Serializable]
    public class WalletData : BaseModel
    {
        public Dictionary<string, int> Wallets { get; set; }
        public string? UserId { get; set; }   // Foregin Key

        // Reference not is use in a REST based transactional only system
    }

    [Serializable]
    public abstract class BaseModel
    {

        public string Id { get; set; }             // Unique & Primary (Also, used as Foreign Key by other Models) // Created DateTime is already hashed inside.
        public string? Name { get; set; }

        public string Email { get; set; }
        // Generic Name Descriptors
        public DateTime? UpdatedAt { get; set; }
    }




    public string currency;
    public int newAmount;

    public void SubmitIptButton()
    {
        string email = emailInputField.text;
        string currency = "Ipt";
        int newAmount = 100;


        StartCoroutine(SendUpdateRequest(email, currency, newAmount));
    }

    IEnumerator SendUpdateRequest(string email, string currency, int newAmount)
    {
        string apiUrl = $"https://admin.wizar.io/api/v1/Wallet/Email={email}/Deposit/Currency={currency}&Amount={newAmount}";

        Debug.Log("Here");
        UnityWebRequest request = UnityWebRequest.Put(apiUrl, ""); // The second argument should be the data you want to send, but in this case, it's empty.

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Update successful!");
            UpdateAmount += newAmount;
            walletAmountText.text = "$" + UpdateAmount.ToString();
        }
        else
        {
            Debug.LogError("Update failed: " + request.error);
        }
    }
    //////////////////////////////Withdraw////////////////////////////////////



    [Header("Buy Land Amount")]
    public SpendAmount[] spendamount;

            public void BuyButton()
            {
                foreach (SpendAmount spendAmountItem in spendamount)
                {


                    if (spendAmountItem.value == spendAmountItem.currentValue)
                    {
                        string email = emailInputField.text;
                        string currency = "Ipt";
                        int amount = spendAmountItem.currentValue;

                        StartCoroutine(SendUpdateRequests(email, currency, amount));
                        break;
                    }
                }
             }

            IEnumerator SendUpdateRequests(string email, string currency, int amount)
            {
                if (amount <= UpdateAmount)
                {
                    string apiUrl = $"https://wizar.startuped.xyz/api/v1/Wallet/Email={email}/Withdraw/Currency={currency}&Amount={amount}";

                    UnityWebRequest request = UnityWebRequest.Put(apiUrl, "");
                    request.SetRequestHeader("Content-Type", "application/json");

                    yield return request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Withdraw successful!");
                        UpdateAmount -= amount;
                        walletAmountText.text = "$" + UpdateAmount.ToString();
                    }
                    else
                    {
                        Debug.LogError("Withdraw failed: " + request.error);
                    }
                }
                else
                {
                    insufMessageText.text = "Sorry, You have insufucient amount";

                }

            }




            [System.Serializable]
            public class SpendAmount
    {
        public int value;
        public int currentValue;

    }


   public void PlayGames()
    {
        MenuManager.instance.LoginUI.SetActive(false);
       // MenuManager.instance.playUIParent.SetActive(true);
       // MenuManager.instance.mainmenuUI.SetActive(false);


        MenuManager.instance.EnterDetailsUI.SetActive(false);
       // MenuManager.instance.OTPUIBackground.SetActive(false);
        MenuManager.instance.playUIParent.SetActive(true);
       // MenuManager.instance.mainmenuUI.SetActive(false);
    }
}
       
