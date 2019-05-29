using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

	int score = 0;
	[SerializeField] int health;
    // Start is called before the first frame update
 	void Start(){
 		health = FindObjectOfType<Player>().GetHealth();
 	}
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton(){
    	int numberGameSessions = FindObjectsOfType<GameSession>().Length;
    	if(numberGameSessions>1){
    		Destroy(gameObject);
    	}else{
    		DontDestroyOnLoad(gameObject);
    	}
    }

    public int GetScore(){return score;}
    public int GetHealth(){return health;}
    public void AddToScore(int scoreVal){score+=scoreVal;}
    public void UpdateHealth(int num){health -=num;}
    public void ResetGame(){
    	Destroy(gameObject);
    }
}
