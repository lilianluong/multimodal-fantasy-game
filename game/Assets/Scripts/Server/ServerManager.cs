using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ServerManager : MonoBehaviour
{
    private bool waitingForRequest;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PollTurn()
    {
        if (!waitingForRequest)
        {
            waitingForRequest = true;
            StartCoroutine(GetRequest("http://127.0.0.1:5000/api/pollTurn", (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
                else
                {
                    Debug.Log($"GET request returned: {req.downloadHandler.text}");
                    // PollTurnResponse response = JsonConvert.DeserializeObject<PollTurnResponse>(req.downloadHandler.text);
                }
                waitingForRequest = false;
            }));
            return true;
        }
        return false;
    }

    public bool StartTurn(float turnLength)
    {
        if (!waitingForRequest)
        {
            waitingForRequest = true;
            WWWForm postData = new WWWForm();
            postData.AddField("turnLength", turnLength.ToString());
            StartCoroutine(PostRequest("http://127.0.0.1:5000/api/startTurn", postData, (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
                else
                {
                    Debug.Log($"POST request returned: {req.downloadHandler.text}");
                }
                waitingForRequest = false;
            }));
            return true;
        }
        return false;
    }

    public bool CanMakeRequest()
    {
        return !waitingForRequest;
    }

    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    IEnumerator PostRequest(string url, WWWForm postData, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, postData))
        {
            yield return request.SendWebRequest();
            callback(request);
        }
    }
}
