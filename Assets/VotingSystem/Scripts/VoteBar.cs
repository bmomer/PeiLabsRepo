using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteBar : MonoBehaviour
{
    public Voter voter; //oylama barının sahibi olan oyuncu, VoteManager tarafından atanıyor
    public int voteBarVoteCount = 0; //oylama barındaki oy miktarının görselleştirilmesi için kullanılan oy sayısı

    public TextMeshProUGUI voteBarNameText;//oylama barı sahibinin ismi
    [SerializeField] private Image fillImg;
    [SerializeField] private Button btn; //oylama butonu
    Slider slider;

    VoteManager voteManager;

    void Awake()
    {
        voteManager = FindObjectOfType<VoteManager>();
        slider = GetComponent<Slider>();
        btn.onClick.AddListener(() => UseVote());
    }

    void Start()
    {
        voteBarNameText.text = voter.voterName; //oyuncunun oylama barı üzerindeki isminin atanması
    }

    void UseVote()//buton ile çağrılır, bu oylama barının sahibinin oy sayısını arttırır
    {
        voteManager.IncreaseVote(voter.voterId);
    }

    public void UpdateVoteBarVisual() //çağırıldığında oyuncunun oylama barı üzerindeki görsel oy miktarını günceller, VoteManagerdan çağrılır
    {
        voteBarVoteCount = voter.voteCount;
        
        if(voteBarVoteCount >= 3)
            fillImg.color = Color.red;

        else
            fillImg.color = Color.yellow;

        slider.value = voteBarVoteCount / 3f;
    }


}
