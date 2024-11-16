using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public bool votingEnded = false;
    [SerializeField] private List<GameObject> voteBars = new(); //oylama barlarının tutulduğu liste,oylama barları hierarchyden manuel olarak eklendi
    [HideInInspector] public List<Voter> voters = new(); //oyuncuların tutulduğu liste
    
    public void InitializeVoteBar(Voter v) //oyuncu için telefonda bir oylama barı açılıp bağlantı kurulur
    {
        voteBars[v.voterId].GetComponent<VoteBar>().voter = v;
        voteBars[v.voterId].SetActive(true);
    }

    public void IncreaseVote(int voterId)//oyuncunun oy sayısı arttırılır
    {
        Voter _voter = voters[voterId]; //liste içerisindeki oyuncu gelen id ile belirlenir

        if(votingEnded) return; //oylama bitmişse metottan çıkılır
            
        _voter.voteCount++;

        UpdateVoteVisuals();

        if(_voter.voteCount >= 3) 
        {
            _voter.isVotedOut = true; //seçilen oyuncuyu belirler
            votingEnded = true; //oylamayı bitirir
            VotingEnd(); //sonuçları görselleştirir
        }
    }
    
    public void Reset()
    {
        foreach(Voter voter in voters)
        {
            voter.voteCount = 0;
            voter.isVotedOut = false;
        }

        foreach(GameObject go in voteBars)
            go.GetComponent<VoteBar>().voteBarVoteCount = 0;

        votingEnded = false;

        UpdateVoteVisuals();         
    }
    
    private void UpdateVoteVisuals() //oyuncuların ve oylama barlarının görsel güncellemesi yapılır
    {
        foreach(Voter voter in voters)
            voter.UpdateVoterVisual();

        foreach(GameObject go in voteBars)
            go.GetComponent<VoteBar>().UpdateVoteBarVisual();    
    }

    private void VotingEnd() //oylama bittiğinde seçilen oyuncu hariç diğer oyuncuların görselleri resetlenir
    {
        foreach(Voter voter in voters)
        {
            if(!voter.isVotedOut)
            {
                voter.voteCount = 0;
                voter.UpdateVoterVisual();
            }
        }

        foreach(GameObject go in voteBars)
        {
            if(!go.GetComponent<VoteBar>().voter.isVotedOut)
            {
                go.GetComponent<VoteBar>().voter.voteCount = 0;
                go.GetComponent<VoteBar>().UpdateVoteBarVisual();   
            }
        }
             
    }
}
