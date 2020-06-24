using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Data;
using UnityCore.Scene;
using UnityCore.Menu;


public class ButtonHandler : MonoBehaviour
{
    public SceneController sceneController;
    public DataPresistence DP;
    public PageController menu;

    public void nextScene()
    {
        int loadScene = DP.LoadScene;
        if(loadScene == 0)
        {
            sceneController.Load(SceneType.Benton00);//, null, false, PageType.Loading);
        }
        if (loadScene == 1)
        {
            sceneController.Load(SceneType.Benton01);
        }
        if (loadScene == 2)
        {
            sceneController.Load(SceneType.Benton02);
        }

    }

}
