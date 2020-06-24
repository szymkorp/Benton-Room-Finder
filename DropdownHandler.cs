using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityCore.Data;

public class DropdownHandler : MonoBehaviour
{
    public DataPresistence DP;
    public int[] startEndArray = new int[6];

    [System.Serializable]
    public class Building
    {
        private string buildingId;
        private string buildingName;

        public Building(string buildingName, string buildingId)
        {
            this.buildingId = buildingId;
            this.buildingName = buildingName;
        }

        public string getName()
        {
            return buildingName;
        }
        public string getId()
        {
            return buildingId;
        }
    }

    [System.Serializable]
    public class Room
    {
        private string roomNum;
        private int xPos;
        private int yPos;
        private int floor;

        public Room(string roomNum, int xPos, int yPos, int floor)
        {
            this.roomNum = roomNum;
            this.xPos = xPos;
            this.yPos = yPos;
            this.floor = floor;
        }

        public string getRoomNumber()
        {
            return roomNum;
        }
        public int getXPosition()
        {
            return xPos;
        }
        public int getYPosition()
        {
            return yPos;
        }

        public int getFloor()
        {
            return floor;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    void useFirstCallData(string res)
    {
        string buildingName;
        string buildingId;
        var dropdown = GameObject.Find("BuildingSelect").GetComponent<Dropdown>();
        dropdown.options.Clear();
        JArray buildingArray = JArray.Parse(res);
        List<Building> buildingList = new List<Building>();
        for (int i = 0; i < buildingArray.Count; i++)
        {
            buildingName = buildingArray[i]["buildingName"].ToString();
            buildingId = buildingArray[i]["buildingId"].ToString();
            buildingList.Add(new Building(buildingName, buildingId));
        }
        foreach (Building building in buildingList)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = building.getName() });
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown, buildingList); });

    }

    void useSecondCallData(string response)
    {
        var startRoomDropdown = GameObject.Find("StartRoom").GetComponent<Dropdown>();
        startRoomDropdown.options.Clear();
        var endRoomDropdown = GameObject.Find("EndRoom").GetComponent<Dropdown>();
        endRoomDropdown.options.Clear();


        JArray roomData = JArray.Parse(response);
        List<Room> roomList = new List<Room>();
        int length = ((JArray)roomData[0]["Rooms"]).Count;
        string roomNum = "";
        int xPos = 0;
        int yPos = 0;
        int floor = 0;
        for (int i = 0; i < length; i++)
        {
            roomNum = roomData[0]["Rooms"][i]["roomNumber"].ToString();
            xPos = (int)roomData[0]["Rooms"][i]["roomX"];
            yPos = (int)roomData[0]["Rooms"][i]["roomY"];
            floor = (int)roomData[0]["Rooms"][i]["floor"];
            roomList.Add(new Room(roomNum, xPos, yPos, floor));
        }

        foreach (Room room in roomList)
        {
            startRoomDropdown.options.Add(new Dropdown.OptionData() { text = room.getRoomNumber() });
            endRoomDropdown.options.Add(new Dropdown.OptionData() { text = room.getRoomNumber() });
        }
        startRoomDropdown.onValueChanged.AddListener(delegate { Dropdown2ItemSelected(startRoomDropdown, roomList); });
        endRoomDropdown.onValueChanged.AddListener(delegate { Dropdown3ItemSelected(endRoomDropdown, roomList); });


    }

    void DropdownItemSelected(Dropdown dropdown, List<Building> buildingList)
    {
        int index = dropdown.value;
        string id = buildingList[index].getId();
        StartCoroutine(GetRooms(id));
    }
    void Dropdown2ItemSelected(Dropdown dropdown, List<Room> roomList)
    {
        int index = dropdown.value;
        startEndArray[0] = roomList[index].getFloor();
        startEndArray[1] = roomList[index].getXPosition();
        startEndArray[2] = roomList[index].getYPosition();
        print("start room: \n" + "Xpos: " + startEndArray[1] + "\nYpos: " + startEndArray[2] + "\nFloor: " + startEndArray[0]);
    }
    void Dropdown3ItemSelected(Dropdown dropdown, List<Room> roomList)
    {
        int index = dropdown.value;
        startEndArray[3] = roomList[index].getFloor();
        startEndArray[4] = roomList[index].getXPosition();
        startEndArray[5] = roomList[index].getYPosition();
        setPlayerPrefs(startEndArray);
        print("End room: \n" + "Xpos: " + startEndArray[4] + "\nYpos: " + startEndArray[5] + "\nFloor: " + startEndArray[3]);
    }
    public void setPlayerPrefs(int[] seArray)
    {
        bool changeFloors;
        int floor1 = seArray[3];
        int floor0 = seArray[0];
        //Setting playerPrefs for multiple floor navigation
        if (seArray[3] - seArray[0] != 0)
        {
            DP.ChangeScene = 1;
            switch (floor0)
            {
                case 0:
                    DP.LoadScene = 0;
                    DP.Floor00 = floor0;
                    DP.StartX0 = seArray[1];
                    DP.StartY0 = seArray[2];
                    DP.EndX00 = 4;
                    DP.EndY00 = 42;
                    break;
                case 1:
                    DP.LoadScene = 1;
                    DP.Floor01 = floor0;
                    DP.StartX1 = seArray[1];
                    DP.StartY1 = seArray[2];
                    DP.EndX1 = 10;
                    DP.EndY1 = 39;
                    break;
            }
            switch (floor1)
            {
                case 0:
                    DP.Floor000 = floor1;
                    DP.StartX0 = 4;
                    DP.StartY0 = 41;
                    DP.EndX00 = seArray[4];
                    DP.EndY00 = seArray[5];
                    break;
                case 1:
                    DP.Floor001 = floor1;
                    DP.StartX1 = 10;
                    DP.StartY1 = 39;
                    DP.EndX1 = seArray[4];
                    DP.EndY1 = seArray[5];
                    break;
            }
        }
        //Setting player prefs for single floor navigation
        else
        {
            DP.ChangeScene = 0;
            switch (floor0)
            {
                case 0:
                    DP.LoadScene = 0;
                    DP.Floor00 = floor0;
                    DP.Floor000 = floor0;
                    DP.StartX0 = seArray[1];
                    DP.StartY0 = seArray[2];
                    DP.EndX00 = seArray[4];
                    DP.EndY00 = seArray[5];
                    DP.Floor01 = 0;
                    DP.Floor001 = 0;
                    DP.StartX1 = 0;
                    DP.StartY1 = 0;
                    DP.EndX1 = 0;
                    DP.EndY1 = 0;
                    DP.Floor02 = 0;
                    DP.Floor002 = 0;
                    DP.StartX2 = 0;
                    DP.StartY2 = 0;
                    DP.EndX2 = 0;
                    DP.EndY2 = 0;
                    break;
                case 1:
                    DP.LoadScene = 1;
                    DP.Floor01 = floor0;
                    DP.Floor001 = floor0;
                    DP.StartX1 = seArray[1];
                    DP.StartY1 = seArray[2];
                    DP.EndX1 = seArray[4];
                    DP.EndY1 = seArray[5];
                    DP.Floor00 = 0;
                    DP.Floor000 = 0;
                    DP.StartX0 = 0;
                    DP.StartY0 = 0;
                    DP.EndX00 = 0;
                    DP.EndY00 = 0;
                    DP.Floor02 = 0;
                    DP.Floor002 = 0;
                    DP.StartX2 = 0;
                    DP.StartY2 = 0;
                    DP.EndX2 = 0;
                    DP.EndY2 = 0;
                    break;
                case 2:
                    DP.LoadScene = 2;
                    DP.Floor02 = floor0;
                    DP.Floor002 = floor0;
                    DP.StartX2 = seArray[1];
                    DP.StartY2 = seArray[2];
                    DP.EndX2 = seArray[4];
                    DP.EndY2 = seArray[5];
                    DP.Floor00 = 0;
                    DP.Floor000 = 0;
                    DP.StartX0 = 0;
                    DP.StartY0 = 0;
                    DP.EndX00 = 0;
                    DP.EndY00 = 0;
                    DP.Floor01 = 0;
                    DP.Floor001 = 0;
                    DP.StartX1 = 0;
                    DP.StartY1 = 0;
                    DP.EndX1 = 0;
                    DP.EndY1 = 0;
                    break;
            }
        }
        Log("Start X pos: " + DP.StartX2);
        Log("End Y pos :" + DP.EndY2);
        Log("Load Scene :" + DP.LoadScene);

    }
    private void Log(string _msg)
    {
        Debug.Log("[TestData] " + _msg);
    }


    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://unitytransportb-api.mikestahr.com/api.asmx/getBuildingList?");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            var res = www.downloadHandler.text;
            useFirstCallData(res);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    IEnumerator GetRooms(string id)
    {
        string url = "http://unitytransportb-api.mikestahr.com/api.asmx/";
        string apiCall = "getBuildingRoomList?buildingId=" + id + "&groupCode=B";
       // string apiCall = "getBuildingRoomList?buildingId=" + id;
        UnityWebRequest www = UnityWebRequest.Get(url + apiCall);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }  
        else
        {
            // Show results as text
            var res = www.downloadHandler.text;
            useSecondCallData(res);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

}
