using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Voter : MonoBehaviour
{
    public bool isVotedOut = false;
    public bool isPlayer = false;

    [Header("Player Info")]
    public int voterId; //VoteManager ve oylama barları arasındaki bağlantıyı kurmak için id
    public string voterName; 
    public int voteCount = 0; 

    [Header("Visuals")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material white;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private TextMeshProUGUI voterNameText;
    [SerializeField] private TextMeshProUGUI voteCountText;


    VoteManager voteManager;

    void Awake()
    {
        voteManager = FindObjectOfType<VoteManager>(); 
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize() //oyuncu VoteManager'daki listeye kaydını yapar
    {
        voteManager.voters.Add(this);
        voterId = voteManager.voters.IndexOf(this); //VoteManager ve oylama barları arasındaki bağlantıyı kurmak için her oyuncuya bir id atanır

        //oyuncu isim ve oy sayısı ataması
        voterNameText.text = voterName; 
        voteCountText.text = voteCount.ToString();
        //

        voteManager.InitializeVoteBar(this); //bu oyuncunun telefondaki oylama barını aktif eder
    }

    public void UpdateVoterVisual() //çağrıldığında oyuncunun görsellerini ve oy sayısını görselleştirir, VoteManagerdan çağrılır
    {
        voteCountText.text = voteCount.ToString();

        if(voteCount >= 3) meshRenderer.material = red;
        else if(voteCount > 0) meshRenderer.material = yellow;
        else if(isPlayer) meshRenderer.material = green;
        else if(voteCount == 0) meshRenderer.material = white;    
    }
}
