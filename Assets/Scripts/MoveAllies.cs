﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAllies : MonoBehaviour {

public GameObject[] waypoints;
private int currentWaypoint = 0;
private float lastWaypointSwitchTime;
public float speed = 1.0f;
public int goldEarn;
public Transform Healthtrans;
public int AllyType;
	// Use this for initialization
void Start () {
	lastWaypointSwitchTime = Time.time;
		
}
	
	// Update is called once per frame
void Update () {
	// 1
	Vector3 startPosition = waypoints [currentWaypoint].transform.position;
  Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
  // 2 
  float pathLength = Vector3.Distance (startPosition, endPosition);
  float totalTimeForPath = pathLength / speed;
  float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
  gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
  // 3 
        // Kejin modify:
        if (Healthtrans.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
        //kejin end
  if (Vector3.Distance(gameObject.transform.position, endPosition) < 0.1f)
  //if (gameObject.transform.position.Equals(endPosition)) 
  {
    if (currentWaypoint < waypoints.Length - 2)
    {
      // 3.a 
      currentWaypoint++;
      lastWaypointSwitchTime = Time.time;
      // TODO: Rotate into move direction
      RotateIntoMoveDirection();
    }
    else
    {
      // 3.b 
      Destroy(gameObject);
      GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      gameManager.Gold += goldEarn;
      gameManager.AllyNum++;
      if(AllyType==2){
        Type2();
      }
      if(AllyType==3){
        gameManager.Health++;
        
      }
    }
  }
}

  private void RotateIntoMoveDirection()
{
  //1
  Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
  Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
  Vector3 newDirection = (newEndPosition - newStartPosition);
  //2
  float x = newDirection.x;
  float y = newDirection.y;
  float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
  //3
  GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
  sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
}
public float DistanceToGoal()
{
  float distance = 0;
  distance += Vector2.Distance(
      gameObject.transform.position, 
      waypoints [currentWaypoint + 1].transform.position);
  for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
  {
    Vector3 startPosition = waypoints [i].transform.position;
    Vector3 endPosition = waypoints [i + 1].transform.position;
    distance += Vector2.Distance(startPosition, endPosition);
  }
  return distance;
}

private void Type2(){
  GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
  foreach (GameObject enemy in enemies){
    enemy.gameObject.GetComponent<MoveEnemy>().speed= Mathf.Max(enemy.gameObject.GetComponent<MoveEnemy>().speed*0.5f, 0.5f);

  }

}


}
