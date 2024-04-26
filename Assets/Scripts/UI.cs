using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PlayerInterface
{
    public class UI : MonoBehaviour
    {
        public GameObject decisionText;
        public GameObject yesButton;
        public GameObject noButton;
        
        // Enum to keep track of decision
        private EDecision currentDecision = EDecision.NO; // Default to NO

        public enum EDecision
        {
            YES,
            NO,
            NONE // Add NONE for uninitialized state
        };

        public enum EJailDecision
        {
            PAY,
            ROLL,
            CARD
        };

        // Start is called before the first frame update
        void Start()
        {
            if (decisionText != null)
            {
                decisionText.GetComponent<TextMeshProUGUI>().text = $"Cost of buying/upgrading property: ${0}";
            }
            else
            {
                Debug.LogError("DecisionText is not assigned in the inspector or has been destroyed.");
            }

            // And the same for buttons
            if (yesButton != null)
            {
                yesButton.SetActive(true);
            }
            else
            {
                Debug.LogError("YesButton is not assigned in the inspector or has been destroyed.");
            }

            if (noButton != null)
            {
                noButton.SetActive(true);
            }
            else
            {
                Debug.LogError("NoButton is not assigned in the inspector or has been destroyed.");
            }

            // // Assuming you want the buttons hidden when the game starts
            // yesButton.gameObject.SetActive(false);
            // noButton.gameObject.SetActive(false);

            // Add button listeners
            yesButton.GetComponent<Button>().onClick.AddListener(OnYesClicked);
            noButton.GetComponent<Button>().onClick.AddListener(OnNoClicked);
        }

        public EDecision PromptYesNoDecision(int propertyCost)
        {
            currentDecision = EDecision.NONE; // Reset the decision
            decisionText.GetComponent<TextMeshProUGUI>().text = $"Cost of buying/upgrading property: ${propertyCost}";
            yesButton.SetActive(true);
            noButton.SetActive(true);
            return currentDecision;
        }

        private void OnYesClicked()
        {
            currentDecision = EDecision.YES;
            HidePrompt();
            // Additional logic for YES decision...
        }

        private void OnNoClicked()
        {
            currentDecision = EDecision.NO;
            HidePrompt();
            // Additional logic for NO decision...
        }

        private void HidePrompt()
        {
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
        }

        // You may need a method to check the current decision
        public EDecision GetCurrentDecision()
        {
            return currentDecision;
        }

        public static EJailDecision GetJailDecision()
        {
            return EJailDecision.ROLL;
        }
    }
}
