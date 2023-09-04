using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColor
{
    public int bounces;
    public Color color;

    public ObstacleColor (int _bounces, Color _color)
    {
        bounces = _bounces;
        color = _color;
    }
}

public class ObstacleSieze
{
    public float scale;
    public float firstDrop;
    public float bounceTime;
    public float heightOffset;
    //Add a SoundClass here and to play when bounceing

    public void PlayBounceSound()
    {
        Debug.Log("palyed " + scale + " sound on Bounce");
    }
    
    public ObstacleSieze (float _scale, float _firstDrop, float _bounceTime, float _heightOffset){
        scale = _scale;
        firstDrop =_firstDrop;
        bounceTime = _bounceTime;
        heightOffset = _heightOffset;
    }
}

public class ObstacleShape
{
    public Sprite sprite;
    public int colliderInt;     //Int to decide what collider to ad

    public virtual void Start(){

    }
    
    public virtual void Bounce(){
        
    }
}

public class CircleShape : ObstacleShape
{
    public CircleShape(){
        //sprite = circleSprite; 
        //colliderInt = circleInt;
    }

    //public override void Start(){
      //  GameObject thisGameObject = this;
      //  CircleCollider2D collider = thisGameObject.AddComponent<CircleCollider2D>();
      //  base.Start();               //remember to add the shape, color, and seize in the awake method for this to run proporly (i think)
    //}   
}
