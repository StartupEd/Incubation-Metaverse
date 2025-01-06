using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AuraTrigger : MonoBehaviour
{
    public Transform targetEnterPosition; // Target position for Enter
    public Transform targetExitPosition;  // Target position for Exit
    public GameObject EnterButtonUi;      // UI for Enter Button
    public GameObject ExitButtonUi;       // UI for Exit Button
    public GameObject EnterPointAura;     // Reference to Enter Aura point
    public GameObject ExitPointAura;      // Reference to Exit Aura point
    public GameObject player;            // Reference to the player object

    private void Start()
    {
        // Ensure buttons have listeners
        if (EnterButtonUi != null)
        {
            Button enterButton = EnterButtonUi.GetComponent<Button>();
            if (enterButton != null)
            {
                enterButton.onClick.AddListener(MoveToEnterPosition);
            }
        }

        if (ExitButtonUi != null)
        {
            Button exitButton = ExitButtonUi.GetComponent<Button>();
            if (exitButton != null)
            {
                exitButton.onClick.AddListener(MoveToExitPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(EnterPointAura))
        {
            EnterButtonUi?.SetActive(true); // Show EnterButtonUi
            Debug.Log("Player entered EnterPointAura.");
        }
        if(other.gameObject.Equals(ExitPointAura))
        {
            ExitButtonUi?.SetActive(true);
        }
        else
        {
            ExitButtonUi?.SetActive(false);
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(EnterPointAura))
        {
            EnterButtonUi?.SetActive(false);
        }
        if (other.gameObject.Equals(ExitButtonUi))
        {
            ExitButtonUi?.SetActive(false);
        }

    }

    private void MoveToEnterPosition()
    {
        
            ThirdPersonController.instance.isControllingEnabled = false;
            player.transform.position = targetEnterPosition.position;
            StartCoroutine(EnableControlAfterDelay());
            Debug.Log("Player moved to Enter target position: " + targetEnterPosition.position);
        
    }

    private void MoveToExitPosition()
    {
       
            ThirdPersonController.instance.isControllingEnabled = false;
            player.transform.position = targetExitPosition.position;
            StartCoroutine(EnableControlAfterDelay());
            Debug.Log("Player moved to Exit target position: " + targetExitPosition.position);
       
    }

    private IEnumerator EnableControlAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Wait for 2 seconds
        ThirdPersonController.instance.isControllingEnabled = true; // Enable control
        Debug.Log("ThirdPersonController enabled after delay.");
    }
}
