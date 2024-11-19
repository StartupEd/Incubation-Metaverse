using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Addressable
{
    public class Initialize_AddressableScene : MonoBehaviour
    {
        public static Initialize_AddressableScene instance;

        public GameObject loadingPanel, moduleParent, warningPopup;

        public string assetPathLocator; // Path for assets in the Addressable group window

        Scene initiallyLoadedScene;
        internal SceneInstance initialAddressableSceneInstance;
        internal bool isPreviousAddressableSceneLoaded, isDownloading, isLoading;
        internal IResourceLocator resource_Locator;

        AsyncOperationHandle<IResourceLocator> loadContentCatalogAsync;
        AsyncOperationHandle<SceneInstance> loadedScene;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            if (loadingPanel != null) loadingPanel.SetActive(false);
            Addressables.InitializeAsync().Completed += AfterInitialize;
        }

        void AfterInitialize(AsyncOperationHandle<IResourceLocator> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Addressables initialization completed successfully.");
            }
            else
            {
                Debug.LogError("Failed to initialize Addressables.");
                if (warningPopup != null) warningPopup.SetActive(true);
            }
        }

        public void LoadModuleUsingPath(string jsonCatalogPath)
        {
            moduleParent.SetActive(false);
            Loading_Handler.instance.SetLoadingPanel(true);
            initiallyLoadedScene = SceneManager.GetActiveScene();
            loadContentCatalogAsync = Addressables.LoadContentCatalogAsync(jsonCatalogPath);
            isDownloading = true;

            loadContentCatalogAsync.Completed += OnCatalogDownloadCompleted;
            Loading_Handler.instance.UpdateLoadingBar(loadContentCatalogAsync);
        }

        private void OnCatalogDownloadCompleted(AsyncOperationHandle<IResourceLocator> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                StartCoroutine(OnLoadingCompleteRoutine(obj));
            }
            else
            {
                Debug.LogError("Failed to download catalog.");
                if (warningPopup != null) warningPopup.SetActive(true);
            }
        }

        IEnumerator OnLoadingCompleteRoutine(AsyncOperationHandle<IResourceLocator> obj)
        {
            yield return new WaitForEndOfFrame();

            IResourceLocator resourceLocator = obj.Result;
            resource_Locator = resourceLocator;
            resourceLocator.Locate(assetPathLocator, typeof(SceneInstance), out IList<IResourceLocation> locations);

            if (locations == null || locations.Count == 0)
            {
                Debug.LogError("No assets found at the given path.");
                if (warningPopup != null) warningPopup.SetActive(true);
                yield break;
            }

            IResourceLocation resourceLocation = locations[0];
            yield return Addressables.GetDownloadSizeAsync(resourceLocation);

            LoadModuleAfterDownloading(resourceLocation);
        }

        public void LoadModuleAfterDownloading(IResourceLocation resourceLocation)
        {
            loadedScene = Addressables.LoadSceneAsync(resourceLocation, LoadSceneMode.Additive);
            isLoading = true;
            isDownloading = false;

            loadedScene.Completed += (sceneHandler) =>
            {
                if (sceneHandler.Status == AsyncOperationStatus.Succeeded)
                {
                    initialAddressableSceneInstance = sceneHandler.Result;
                    isPreviousAddressableSceneLoaded = true;
                    Loading_Handler.instance.SetLoadingPanel(false);

                    if (initiallyLoadedScene != null)
                    {
                        SceneManager.UnloadSceneAsync(initiallyLoadedScene);
                    }
                }
                else
                {
                    Debug.LogError("Failed to load scene.");
                    if (warningPopup != null) warningPopup.SetActive(true);
                }
            };

            Loading_Handler.instance.UpdateLoadingBar(loadedScene);
        }

        private void Update()
        {
            if (isDownloading && loadContentCatalogAsync.IsValid())
            {
                float downloadProgress = Mathf.Clamp01(loadContentCatalogAsync.PercentComplete);
                Loading_Handler.instance.UpdateLoadingBar(downloadProgress);
            }

            if (isLoading && loadedScene.IsValid())
            {
                float loadProgress = Mathf.Clamp01(loadedScene.PercentComplete);
                Loading_Handler.instance.UpdateLoadingBar(loadProgress);
            }
        }
    }
}
