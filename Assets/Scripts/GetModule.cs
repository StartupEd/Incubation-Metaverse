using System.Collections;
using UnityEngine;

public class GetModule : MonoBehaviour
{
    public static GetModule instance; // Singleton instance

    private static bool isLoaded; // Track if the module has been loaded

    private void Awake()
    {
        // Set up singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes if needed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Start()
    {
        LoadModule();
    }

    public void LoadModule()
    {
        if (isLoaded)
        {
            Debug.Log("Module already loaded.");
            return;
        }

        isLoaded = true;

        // Get the catalog URL for the module, using the LinkAndNames class to build the URL
        string catalogUrl = LinkAndNames.GetCatalogFileUrl("crunch_metaverse");
        Debug.Log($"Catalog URL: {catalogUrl}");

        // Check if the Initialize_AddressableScene instance exists and is ready
        if (Addressable.Initialize_AddressableScene.instance != null)
        {
            // Call Initialize_AddressableScene to load the module using the catalog URL
            Addressable.Initialize_AddressableScene.instance.LoadModuleUsingPath(catalogUrl);
            Debug.LogError("Initialize_AddressableScene loaded.");
        }
        else
        {
            Debug.LogError("Initialize_AddressableScene instance is not set.");
        }
    }
}
