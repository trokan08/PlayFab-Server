using System;
using System.Collections;
using GameManager;
using Newtonsoft.Json;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using PlayFab.Public;
using TMPro;
using UnityEngine.UI;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;
using Object = System.Object;

public class MatchMaker : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject leaveQueueButton;
    [SerializeField] private TMP_Text queueStatusText;
    [SerializeField] private TMP_Text userText;
    [SerializeField] private Button _cancel;
    [SerializeField] private Transform _canvas;
    private string ticketId;
    private Coroutine pollTicketCoroutine;

    private static string QueueName = "MatchMaking";
    private string _playfabId;
    private string _userName;
    private void SetPlayFabId(string playfabID)
    {
        _playfabId = playfabID;
    }

    private void SetUserName(string userName)
    {
        _userName = userName;
        userText.text = userName;
        //StartMatchmaking();
    }

    private void Start()
    {
        
        GameActions.Instance.MatchMaking += SetPlayFabId;
        GameActions.Instance.SetUsername += SetUserName;
        GameActions.Instance.FindMatch += StartMatchmaking;
        _cancel.onClick.AddListener(CancelMatchMaking);

    }

    private void OnDisable()
    {
        GameActions.Instance.MatchMaking -= SetPlayFabId;
        GameActions.Instance.SetUsername -= SetUserName;
        GameActions.Instance.FindMatch -= StartMatchmaking;

        _cancel.onClick.RemoveListener(CancelMatchMaking);

    }


    public void StartMatchmaking()
    {
        Debug.Log("Start Match Making");
        playButton.SetActive(false);
        queueStatusText.text = "Submitting Ticket";
        UserName userName = new UserName();
        userName.Name = _userName;
        JsonConvert.SerializeObject(_userName);
        PlayFabMultiplayerAPI.CreateMatchmakingTicket(
            new CreateMatchmakingTicketRequest
            {
                Creator = new MatchmakingPlayer
                {
                    Entity = new EntityKey
                    {
                        Id = _playfabId,
                        Type = "title_player_account",
                    },
                    Attributes = new MatchmakingPlayerAttributes
                    {
                        DataObject = userName,
                        
                    }
                },

                GiveUpAfterSeconds = 120,

                QueueName = QueueName
            },
            OnMatchmakingTicketCreated,
            OnMatchmakingError
        );
    }

    private void CancelMatchMaking()
    {
        _canvas.gameObject.SetActive(false);
        LeaveQueue();
    }

    public void LeaveQueue()
    {
        leaveQueueButton.SetActive(false);

        PlayFabMultiplayerAPI.CancelMatchmakingTicket(
            new CancelMatchmakingTicketRequest
            {
                QueueName = QueueName,
                TicketId = ticketId
            },
            OnTicketCanceled,
            OnMatchmakingError
        );
    }

    private void OnTicketCanceled(CancelMatchmakingTicketResult result)
    {
        playButton.SetActive(true);
    }

    private void OnMatchmakingTicketCreated(CreateMatchmakingTicketResult result)
    {
        ticketId = result.TicketId;
        pollTicketCoroutine = StartCoroutine(PollTicket(result.TicketId));

        leaveQueueButton.SetActive(true);
        queueStatusText.text = "Ticket Created";
    }

    private void OnMatchmakingError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private IEnumerator PollTicket(string ticketId)
    {
        while (true)
        {
            PlayFabMultiplayerAPI.GetMatchmakingTicket(
                new GetMatchmakingTicketRequest
                {
                    TicketId = ticketId,
                    QueueName = QueueName
                },
                OnGetMatchMakingTicket,
                OnMatchmakingError
            );

            yield return new WaitForSeconds(6);
        }
    }

    private void OnGetMatchMakingTicket(GetMatchmakingTicketResult result)
    {
        queueStatusText.text = $"Status: {result.Status}";

        switch (result.Status)
        {
            case "Matched":
                StopCoroutine(pollTicketCoroutine);
                StartMatch(result.MatchId);
                break;
            case "Canceled":
                StopCoroutine(pollTicketCoroutine);
                leaveQueueButton.SetActive(false);
                playButton.SetActive(true);
                break;
        }
    }

    private void StartMatch(string matchId)
    {
        queueStatusText.text = $"Starting Match";

        PlayFabMultiplayerAPI.GetMatch(
            new GetMatchRequest
            {
                MatchId = matchId,
                ReturnMemberAttributes = true,
                QueueName = QueueName
            },
            OnGetMatch,
            OnMatchmakingError
        );
    }


    private void OnGetMatch(GetMatchResult result)
    {
        Debug.Log(result.ToJson());
        Debug.Log(result);
        queueStatusText.text = $"{result.Members[0].Entity.Id} vs {result.Members[1].Entity.Id}";
       
       UserName name1 = JsonConvert.DeserializeObject<UserName>(result.Members[0].Attributes.DataObject.ToString());;
       UserName name2 = JsonConvert.DeserializeObject<UserName>(result.Members[1].Attributes.DataObject.ToString());;
        queueStatusText.text = $"{name1.Name} vs {name2.Name}";

    }
    
  

 
    
    
    public  void OnError(PlayFabError playFabError)
    {
        Debug.Log(playFabError.Error);
    }
    
}

public class UserName
{
    public string Name;
}