using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAndNames : MonoBehaviour
{
    // Update the base URL for asset bundles based on the environment.
    // Link to asset bundles (use the appropriate link based on your environment)
    public static string LinkToAssetBundle = "https://storage.googleapis.com/onos2023/orange-dev/Development/";
    // For production use:
    // public static string LinkToAssetBundle = "https://storage.googleapis.com/onos2023/orange-dev/Production/";

    // Static URLs for various APIs
    public static string userApi, questTrailApiUrl, feedbackApi, trusityContestApi,
        learningTrail_SchoolNameApiUrl, trusityReportApiUrl, walletApi;

#if UNITY_ANDROID
    public static string deviceType = "Android";
#elif UNITY_WEBGL
    public static string deviceType = "WebGL";
#elif UNITY_IOS
    public static string deviceType = "IOS";
#else
    public static string deviceType = "Unknown"; // Default for unsupported platforms
#endif

    // Version of the asset bundle being used
    public static string bundleVersion = "1";

    // Path to the catalog JSON file for addressable assets
    public static string catalogJsonPath = "catalog.json";

    // Builds the catalog file URL dynamically for the specified module
    public static string GetCatalogFileUrl(string moduleName)
    {
        return $"{LinkToAssetBundle}{moduleName}_{deviceType}_{bundleVersion}/{moduleName}_{bundleVersion}_{catalogJsonPath}";
    }

    // Warning messages
    public static string Warning01 = "Something went wrong. Start without updating?";
    public static string Warning02 = "Something went wrong, check internet connection and restart downloading?";
    public static string Warning03 = "Something went wrong, restart loading?";

    // Error messages
    public static string error01 = "Activation Not Allowed. Key has been used on maximum devices.";
    public static string error02 = "Could not connect to server, please check your internet connection or try again later.";
    public static string error03 = "Server is under maintenance, please be patient and try again later.";
    public static string error04 = "Wrong Activation Key. Please check and re-enter the key.";
    public static string error05 = "This Activation key is not for the book you are trying to activate. Thank you.";
    public static string error06 = "Unable to process your request, please try again later.";
    public static string error07 = "Unknown Error. Please restart your application or re-download the app.";
    public static string error08 = "Your application is outdated, please update your app from the store.";
    public static string error09 = "Login error, try again later or use another login method.";

#if UNITY_ANDROID
    // Screenshot paths on Android
    public static string PathToScreenshots = "/mnt/sdcard/DCIM/WizAR/";
    public static string PathToScreenshotsThumbnail = "/mnt/sdcard/DCIM/WizAR/";
#elif UNITY_IOS
    // Screenshot paths on iOS
    public static string PathToScreenshots = Application.persistentDataPath + "/WizAR/";
    public static string PathToScreenshotsThumbnail = Application.persistentDataPath + "/WizAR/";
#endif

    // Method to construct the User API URL with email and password
    public static string UserApiUrlEmail_Pass(string email, string pass)
    {
        // URL-encode email and password to handle special characters
        string encodedEmail = Uri.EscapeDataString(email);
        string encodedPass = Uri.EscapeDataString(pass);

        // Ensure userApi base URL is set before constructing the URL
        if (string.IsNullOrEmpty(userApi))
        {
            Debug.LogError("User API URL is not set.");
            return null;
        }

        return $"{userApi}Email={encodedEmail}&&Password={encodedPass}";
    }
}

