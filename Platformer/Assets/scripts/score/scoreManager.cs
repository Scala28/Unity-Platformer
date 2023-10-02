using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI permanetText;
    int score;
    int permanentScore;

    public int timeBfTransfer;
    public float transferSpeed;
    private Coroutine transfer;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        transfer = null;
        permanentScore = 0;
        score = 0;
    }


    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "+" + score.ToString();
        
        if (transfer != null)
        {
            StopCoroutine(transfer);
        }
        transfer = StartCoroutine(TransferCoin());
    }

    IEnumerator TransferCoin()
    {
        yield return new WaitForSeconds(timeBfTransfer);

        while(score > 0)
        {
            score--;
            text.text = "+" + score.ToString();
            permanentScore++;
            permanetText.text = permanentScore.ToString();
            yield return new WaitForSeconds(transferSpeed);
        }
        transfer = null;
        text.text = "";
    }

    
}
