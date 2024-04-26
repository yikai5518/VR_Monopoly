using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerInterface
{
    public class UI : MonoBehaviour
    {
        public enum EDecision
        {
            YES,
            NO
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
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public static EDecision GetYesNoDecision()
        {
            return EDecision.YES;
        }

        public static EJailDecision GetJailDecision()
        {
            return EJailDecision.ROLL;
        }
    }
}
