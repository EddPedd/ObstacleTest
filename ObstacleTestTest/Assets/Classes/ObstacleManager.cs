using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour{
    
    //Colorclasses
    public ObstacleColor GreenColorInstance {get; private set;}
    public ObstacleColor BlueColorInstance {get; private set;}
    public ObstacleColor RedColorInstance {get; private set;}

    //Color variables
    [SerializeField]
    private Color _greenObstacleColor;
    private static Color greenObstacleColor;
    [SerializeField]
    private Color _blueObstacleColor;
    private static Color blueObstacleColor;

    [SerializeField]
    private Color _redObstacleColor;   
    private static Color redObstacleColor;


    //Create static instances of Color classes
    private void Awake(){
        greenObstacleColor = _greenObstacleColor;
        blueObstacleColor = _blueObstacleColor;
        redObstacleColor = _redObstacleColor;
        
        GreenColorInstance = new ObstacleColor(0, greenObstacleColor);  //0 for no bounces
        BlueColorInstance = new ObstacleColor(1, blueObstacleColor);
        RedColorInstance = new ObstacleColor(2, redObstacleColor);
    }
}

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

public interface Bouncable{
    void SetShape(GameObject obstacleObject){

    }
    
    void Bounce(){

    }
}

public class ObstacleShape : Bouncable
{
    public Sprite sprite;


    public virtual void SetShape(GameObject obstacleObject){

    }
    
    public virtual void Bounce(){
        
    }
}

public class CircleShape : ObstacleShape
{
    public override void SetShape(GameObject obstacleObject){
        //Set circlesprite 
        CircleCollider2D collider = obstacleObject.AddComponent<CircleCollider2D>();
    }

    public override void Bounce(){
        //Specific circle bounce method
    }   
}
