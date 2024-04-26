using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PlayerInterface
{
    public class UI : MonoBehaviour
    {
        public TMP_Text decisionText;
        public Button yesButton;
        public Button noButton;
        
        // Enum to keep track of decision
        private EDecision currentDecision = EDecision.NO; // Default to NO

        public enum EDecision
        {
            YES,
            NO,
            NONE // Add NONE for uninitialized state
        };

        // Start is called before the first frame update
        void Start()
        {
            if (decisionText != null)
            {
                decisionText.text = $"Cost of buying/upgrading property: ${0}";
            }
            else
            {
                Debug.LogError("DecisionText is not assigned in the inspector or has been destroyed.");
            }

            // And the same for buttons
            if (yesButton != null)
            {
                yesButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("YesButton is not assigned in the inspector or has been destroyed.");
            }

            if (noButton != null)
            {
                noButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("NoButton is not assigned in the inspector or has been destroyed.");
            }

            // // Assuming you want the buttons hidden when the game starts
            // yesButton.gameObject.SetActive(false);
            // noButton.gameObject.SetActive(false);

            // Add button listeners
            yesButton.onClick.AddListener(OnYesClicked);
            noButton.onClick.AddListener(OnNoClicked);
        }

        public EDecision PromptYesNoDecision(int propertyCost)
        {
            currentDecision = EDecision.NONE; // Reset the decision
            decisionText.text = $"Cost of buying/upgrading property: ${propertyCost}";
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
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
    }
}
