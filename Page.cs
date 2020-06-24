
using UnityEngine;

namespace UnityCore
 {
    namespace Menu
    {

        public class Page : MonoBehaviour
        {
            public static readonly string FLAG_ON = "On";
            public static readonly string FLAG_OFF = "Off";
            public static readonly string FLAG_NONE = "None";

            public PageType type;
            public bool useAnimation = false;
            public string targetState { get; private set;}
            private bool m_IsOn;
            

            public bool isOn
            {
                get
                {
                    return m_IsOn;
                }
                private set
                {
                    m_IsOn = false;
                }
            }


            #region Unity Functions

            private void OnEnable()
            {

            }

            #endregion
            public void Animate(bool flag)
            {
                    gameObject.SetActive(false);
            }



        }
    }
  
}
