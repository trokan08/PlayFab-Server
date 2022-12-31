using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Network.Photon
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
       /* [SerializeField] private string nickName;
        public static Action GetPhotonFriends = delegate { };
        public static Action OnLobbyJoined = delegate { };

        #region Unity Method
        private void Awake()
        {
            nickName = PlayerPrefs.GetString("USERNAME");            
        }
        private void Start()
        {
            if (PhotonNetwork.IsConnectedAndReady || PhotonNetwork.IsConnected) return;

            ConnectToPhoton();
        }
        #endregion
        #region Private Methods
        private void ConnectToPhoton()
        {
            Debug.Log($"Connect to Photon as {nickName}");
            PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }        
        #endregion
        #region Photon Callbacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("You have connected to the Photon Master Server");
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("You have connected to a Photon Lobby");
            Debug.Log("Invoking get Playfab friends");
            GetPhotonFriends?.Invoke();
            OnLobbyJoined?.Invoke();
        }
        #endregion*/
       private void Start()
       {
           string randomName = $"Tester{Guid.NewGuid().ToString()}";
           ConnectToPhoton(randomName);
           
       }

       private void ConnectToPhoton(string nickname)
       {
           PhotonNetwork.AuthValues = new AuthenticationValues(nickname);
           PhotonNetwork.AutomaticallySyncScene = true;
           PhotonNetwork.NickName = nickname;
           PhotonNetwork.ConnectUsingSettings();
       }
       public override void OnConnectedToMaster()
       {
           Debug.Log("You have connected to the Photon Master Server");
          /* if (!PhotonNetwork.InLobby)
           {
               PhotonNetwork.JoinLobby();
           }*/
       }

       private void CreatePhotonRoom(string roomName)
       {
           RoomOptions roomOptions = new RoomOptions();
           roomOptions.IsOpen = true;
           roomOptions.IsVisible = true;
           roomOptions.MaxPlayers = 4;
           PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
       }

     

       public override void OnCreatedRoom()
       {
           Debug.Log($"{PhotonNetwork.CurrentRoom.Name} created");
       }

       public override void OnJoinedLobby()
       {
           Debug.Log("Joined to Photon Lobby");
           CreatePhotonRoom("TestRoom");
       }

       public override void OnJoinedRoom()
       {
           Debug.Log("Joined Room");
       }

       public override void OnLeftRoom()
       {
           Debug.Log("Player left room");
       }

       public override void OnJoinRoomFailed(short returnCode, string message)
       {
           Debug.Log($"Room join failed {message}");
       }
       
       public override void OnPlayerEnteredRoom(Player newPlayer)
       {
           Debug.Log($"Player joined room");
       }

       public override void OnPlayerLeftRoom(Player player)
       {
           Debug.Log($"{player.UserId} left room");
       }

       public override void OnMasterClientSwitched(Player newMasterClient)
       {
           Debug.Log($"New master client is {newMasterClient.NickName}");
       }

    }
}