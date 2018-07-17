﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelThree : MonoBehaviour {

    public Image lock1;
    public GameObject star2;

    void Start () {
        Button btn = this.GetComponent<Button> ();
        btn.onClick.AddListener (OnClick);
        if(PlayerPrefs.GetInt("Level2", 0)>0){
        	lock1.enabled=false;
        }
        else {
        	gameObject.GetComponent<Image>().enabled = false;
        }


        if(PlayerPrefs.GetInt("Level3", 0)>0){
        	star2.GetComponent<Star>().Star1.enabled = true;
        	star2.GetComponent<Star>().Star4.enabled = false;


        	if(PlayerPrefs.GetInt("Level3", 0)>1){
        		star2.GetComponent<Star>().Star2.enabled = true;
        		star2.GetComponent<Star>().Star5.enabled = false;

        		if(PlayerPrefs.GetInt("Level3", 0)>2){
        			star2.GetComponent<Star>().Star3.enabled = true;
        			star2.GetComponent<Star>().Star6.enabled = false;

        		}
        	}
        }
    }

    private void OnClick(){
        //
        if(PlayerPrefs.GetInt("Level2", 0)>0){
        	 Application.LoadLevel(7);
        }
        else{
        	//
        	Debug.Log ("Lv3 locked.");
        }
       
    }
}