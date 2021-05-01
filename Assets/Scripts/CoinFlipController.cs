using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Dto;
using System;
using TMPro;

public class CoinFlipController : MonoBehaviour
{
    public TMP_Text balanceText;
    public ParticleSystem winParticleSystem;
    private bool isSpinning = false;
    private bool isStopping = false;
    private bool heads;

    void Start()
    {
        StartCoroutine(balance());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpinning && !isStopping)
        {
            StartCoroutine(play());
        }

        if (isSpinning || isStopping)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 1440f * Time.deltaTime, 0);
        }

        if (isStopping)
        {
            var targetRotation = heads ? 0f : 180f;
            if (Mathf.Abs(transform.rotation.eulerAngles.y - targetRotation) < 6f)
            {
                isStopping = false;
                transform.rotation = Quaternion.Euler(0, targetRotation, 0);
                if (heads)
                {
                    winParticleSystem.Play();
                }
            }
        }
    }

    private void startSpinning()
    {
        isSpinning = true;
        isStopping = false;
    }

    private void stopSpinning(bool heads)
    {
        this.heads = heads;
        isSpinning = false;
        isStopping = true;
    }

    private void setBalance(int balance)
    {
        balanceText.text = String.Format("{0:n0}", balance);
    }

    private IEnumerator balance()
    {
        var request = UnityWebRequest.Get("http://localhost:5000/balance");

        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log("Balance: " + request.downloadHandler.text);

        var balance = BalanceResponse.FromJson(request.downloadHandler.text);

        setBalance(balance.balance);
    }

    private IEnumerator play()
    {
        startSpinning();

        var request = new UnityWebRequest("http://localhost:5000/play", "POST");
        var playRequest = new PlayRequest("coin-flip");

        byte[] body = Encoding.UTF8.GetBytes(JsonUtility.ToJson(playRequest));

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log("Play: " + request.downloadHandler.text);

        var response = PlayResponse.FromJson(request.downloadHandler.text);

        yield return new WaitForSeconds(1);

        var heads = response.result == "heads";

        stopSpinning(heads);      

        yield return new WaitForSeconds(0.5f);        

        setBalance(response.balance);
    }
}
