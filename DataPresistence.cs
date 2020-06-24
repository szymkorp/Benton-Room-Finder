using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore.Data
{
    public class DataPresistence : MonoBehaviour
    {
        private static readonly string changeScene = "changeScene";
        private static readonly string loadScene = "loadScene";
        private static readonly string floor00 = "floor00";
        private static readonly string startX0 = "startx0";
        private static readonly string startY0 = "startY0";
        private static readonly string floor000 = "floor000";
        private static readonly string endX000 = "endX000";
        private static readonly string endY000 = "endY000";
        private static readonly string floor01 = "floor01";
        private static readonly string startX1 = "startX1";
        private static readonly string startY1 = "startY1";
        private static readonly string floor001 = "floor001";
        private static readonly string endX1 = "endX1";
        private static readonly string endY1 = "endY1";

        private static readonly string floor02 = "floor02";
        private static readonly string startX2 = "startX2";
        private static readonly string startY2 = "startY2";
        private static readonly string floor002 = "floor002";
        private static readonly string endX2 = "endX2";
        private static readonly string endY2 = "endY2";


        //   ChangeScene = 1 there will be a scene change
        //   changeScene = 0 NO SCENE CHANGE
        public int ChangeScene
        {
            get
            {
                return GetInt(changeScene);
            }
            set
            {
                SaveInt(changeScene, value);
            }
        }
        public int LoadScene
        {
            get
            {
                return GetInt(loadScene);
            }
            set
            {
                SaveInt(loadScene, value);
            }
        }
        public int Floor00
        {
            get
            {
                return GetInt(floor00);
            }
            set
            {
                SaveInt(floor00, value);
            }
        }
        public int StartX0
        {
            get
            {
                return GetInt(startX0);
            }
            set
            {
                SaveInt(startX0, value);

            }
        }
        public int StartY0
        {
            get
            {
                return GetInt(startY0);
            }
            set
            {
                SaveInt(startY0, value);
            }
        }
        public int Floor000
        {
            get
            {
                return GetInt(floor000);
            }
            set
            {
                SaveInt(floor00, value);

            }
        }
        public int EndX00
        {
            get
            {
                return GetInt(endX000);
            }
            set
            {
                SaveInt(endX000, value);
            }
        }
        public int EndY00
        {
            get
            {
                return GetInt(endY000);
            }
            set
            {
                SaveInt(endY000, value);
            }
        }
        public int Floor01
        {
            get
            {
                return GetInt(floor01);
            }
            set
            {
                SaveInt(floor01, value);
            }
        }
        public int StartX1
        {
            get
            {
                return GetInt(startX1);
            }
            set
            {
                SaveInt(startX1, value);
            }
        }
        public int StartY1
        {
            get
            {
                return GetInt(startY1);
            }
            set
            {
                SaveInt(startY1, value);
            }
        }
        public int Floor001
        {
            get
            {
                return GetInt(floor001);
            }
            set
            {
                SaveInt(floor001, value);
            }
        }
        public int EndX1
        {
            get
            {
                return GetInt(endX1);
            }
            set
            {
                SaveInt(endX1, value);
            }
        }
        public int EndY1
        {
            get
            {
                return GetInt(endY1);
            }
            set
            {
                SaveInt(endY1, value);
            }
        }



        /// //////////////////

        public int Floor02
        {
            get
            {
                return GetInt(floor02);
            }
            set
            {
                SaveInt(floor02, value);
            }
        }
        public int StartX2
        {
            get
            {
                return GetInt(startX2);
            }
            set
            {
                SaveInt(startX2, value);
            }
        }
        public int StartY2
        {
            get
            {
                return GetInt(startY2);
            }
            set
            {
                SaveInt(startY2, value);
            }
        }
        public int Floor002
        {
            get
            {
                return GetInt(floor002);
            }
            set
            {
                SaveInt(floor002, value);
            }
        }
        public int EndX2
        {
            get
            {
                return GetInt(endX2);
            }
            set
            {
                SaveInt(endX2, value);
            }
        }
        public int EndY2
        {
            get
            {
                return GetInt(endY2);
            }
            set
            {
                SaveInt(endY2, value);
            }
        }



        //Private Functions//

        private void SaveInt(string _data, int _value)
        {
            PlayerPrefs.SetInt(_data, _value);
        }

        private int GetInt(string _data)
        {
            return PlayerPrefs.GetInt(_data, 0);
        }

    }


}
