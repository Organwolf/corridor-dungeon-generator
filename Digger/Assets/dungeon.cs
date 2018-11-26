using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeon : MonoBehaviour {

	public GameObject dirtPrefab;
	public bool[,] dug = new bool[50,50];
	public const int north = 1;
	public const int east = 2;
	public const int south = 3;
	public const int west = 4;
	public int agentPosX;
	public int agentPosY;

	// Use this for initialization
	void Start() {
		Generator();
	}
	
	public void Room(){
		for(int i = agentPosX; i < agentPosX+5; i++){
			for(int j = agentPosY; j < agentPosY+5; j++){
				dug[i,j] = true;
			}
		}
		agentPosX += 4;
		agentPosY += 4;		
	}

	public void Corridor(){
		int direction = Random.Range(1,5);
		int length = Random.Range(2,10);

		switch(direction){
			case north:
				for(int i = 0; i<length; i++){
					if(agentPosY < 48){
						dug[agentPosX,agentPosY++] = true;
					}
				}
				break;
			case east:
				for(int i = 0; i<length; i++){
					if(agentPosX < 48){
						dug[agentPosX++,agentPosY] = true;
					}
				}
				break;
			case south:
				for(int i = 0; i<length; i++){
					if(agentPosY > 1){
						dug[agentPosX,agentPosY--] = true;
					}
				}
				break;
			case west:
				for(int i = 0; i<length; i++){
					if(agentPosX > 1){
						dug[agentPosX--,agentPosY] = true;
					}
				}
				break;
		}
	}

	public void Agent(int x, int y){
		// första rummet placeras slumpmässigt

		for(int i = 0; i<300; i++){
			//Room();
			Corridor();
		}
	}

	// Update is called once per frame
	void Generator(){

		float width = dirtPrefab.transform.lossyScale.x;
		float height = dirtPrefab.transform.lossyScale.y;

		// skapar position för en agent inom 50x50
		// 45 pga hur jag implementerat rummen.. kan ändras
		agentPosX = Random.Range(1,45);
		agentPosY = Random.Range(1,45);

		Debug.Log("agentPosX " + agentPosX);
		Debug.Log("agentPosY " + agentPosY);
		
		Agent(agentPosX,agentPosY);

		// fyller i allt förutom det som är "utgrävt"
		for(int i = 0; i < 50; i++){
			for(int j = 0; j < 50; j++){
				if(dug[i,j] == false){
					Instantiate(dirtPrefab, new Vector2(i * width,j * height), Quaternion.identity);
				}		
			}
		}
	}		
}
