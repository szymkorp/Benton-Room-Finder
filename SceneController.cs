using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Menu;
using UnityEngine.SceneManagement;


namespace UnityCore.Scene
{
    public class SceneController : MonoBehaviour
    {
        public delegate void SceneLoadDelegate(SceneType _scene);
        public static SceneController instance;
        public bool debug;//debug bool

        private PageController m_Menus;
        private SceneType m_targetScene;
        private PageType m_LoadingPage;
        private SceneLoadDelegate m_sceneLoadDelegate;
        private bool m_sceneIsLoading;
        public float delayTime = 3f;
        private PageController menu
        {
            get
            {
                if (m_Menus == null)
                {
                    m_Menus = PageController.instance;
                }
                if (m_Menus == null)
                {
                 //   LogWarning("You are trying to access PageController but no instance was found");
                }
                return m_Menus;
            }
        }
        private string currentSceneName
        {
            get
            {
                return SceneManager.GetActiveScene().name;
            }
        }
#region Unity Functions
        private void awake()
        {
            if (!instance)
            {
                Configure();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDisabled()
        {
            Dispose();
        }
#endregion

#region Public Functions
        public void Load(SceneType _scene, SceneLoadDelegate _sceneLoadDelegate=null,
                         bool reload=false, PageType _loadingPage = PageType.None)
        {
            if(_loadingPage != PageType.None && !menu)
            {
                return;
            }
            if(!SceneCanBeLoaded(_scene, reload))
            {
                return;
            }
            m_sceneIsLoading = true;
            m_targetScene = _scene;
            m_LoadingPage = _loadingPage;
            m_sceneLoadDelegate = _sceneLoadDelegate;
            StartCoroutine("LoadScene");
        }


#endregion

#region privtae functions
        private void Configure()
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private async void OnSceneLoaded(UnityEngine.SceneManagement.Scene _Scene, LoadSceneMode _mode)
        {
            SceneType _sceneType = StringToSceneType(_Scene.name);
            if (m_targetScene == SceneType.None)
            {
                return;
            }

           
            if(m_targetScene != _sceneType)
            {
                return;
            }

            if(m_sceneLoadDelegate != null)
            {
                try
                {
                    m_sceneLoadDelegate(_sceneType);
                }
                catch (System.Exception)
                {
                   // LogWarning("Unable tp repsond with sceneLoadDelegate after scene [" + _sceneType + "] loaded");
                }
            }

            if (m_LoadingPage != PageType.None)
            {
                // await Task.Delay(1000);
                Invoke("DelayedAction", delayTime);
                //menu.TurnPageOff(m_LoadingPage);
            }
        }
        void DelayedAction()
        {
            menu.TurnPageOff(m_LoadingPage);
        }
        private IEnumerator LoadScene()
        {
            if(m_LoadingPage != PageType.None){
                menu.TurnPageOn(m_LoadingPage);
                while (!menu.PageIsOn(m_LoadingPage))
                {
                    yield return null;
                }
            }

            string _TargetSceneName = SceneTypeToString(m_targetScene);
            SceneManager.LoadScene(_TargetSceneName);
          
        }
        private bool SceneCanBeLoaded(SceneType _scene, bool reload)
        {
            string _targetSceneName = SceneTypeToString(_scene);
            if(currentSceneName == _targetSceneName && !reload)
            {
                //Add LogWarning later //
                return false;
            }
            else if(_targetSceneName == string.Empty)
            {
                //ADD LOGWARNING
                return false;
            }
            else if (m_sceneIsLoading)
            {
                //ADD LOGWARNING
                return false;
            }
            return true;
        }
        private string SceneTypeToString(SceneType _scene)
        {
            switch (_scene)
            {
                case SceneType.Benton00: return "Benton00";
                case SceneType.Benton01: return "Benton01";
                case SceneType.Benton02: return "Benton02";
                case SceneType.Menu: return "Menu";
                case SceneType.EndScene: return "EndScene";
                default:
                    //LOGWARNING IM TOO TIRED TO DO THIS
                    return string.Empty;
            }
        }
        private SceneType StringToSceneType(string _scene)
        {
            switch (_scene)
            {
                case "Benton00": return SceneType.Benton00;
                case "Benton01": return SceneType.Benton01;
                case "Benton02": return SceneType.Benton02;
                case "Menu": return SceneType.Menu;
                case "EndScene": return SceneType.EndScene;
                default:
                    //LOGWARNING IM TOO TIRED TO DO THIS
                    return SceneType.None;
            }
        }
        private void Log(string _msg)
        {

        }
        private void WarningLog(string _msg)
        {

        }


#endregion

    }
}

