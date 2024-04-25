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
    }
}
