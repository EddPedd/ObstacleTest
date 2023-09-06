using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour{
    
    //Color classes
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

    //Size classes
    public ObstacleSize SmallSizeInstance {get; private set;}
    public ObstacleSize MediumSizeInstance {get; private set;}
    public ObstacleSize LargeSizeInstance {get; private set;}


    //Size Variables
    [SerializeField]                        //Scale
    private float _smallScale;
    private static float smallScale;
    [SerializeField]
    private float _mediumScale;
    private static float mediumScale;
    [SerializeField]
    private float _largeScale;
    private static float largeScale;

    [SerializeField]                        //Bounce time
    private float _smallBounceTime;
    private static float smallBounceTime;
    [SerializeField]
    private float _mediumBounceTime;
    private static float mediumBounceTime;
    [SerializeField]
    private float _largeBounceTime;
    private static float largeBounceTime;

    //Create static instances of Color classes
    private void Awake(){
        greenObstacleColor = _greenObstacleColor;   //Set variables for color classes
        blueObstacleColor = _blueObstacleColor;
        redObstacleColor = _redObstacleColor;
        
        GreenColorInstance = new ObstacleColor(0, greenObstacleColor);          //Create color classes
        BlueColorInstance = new ObstacleColor(1, blueObstacleColor);            //1 for one bounce
        RedColorInstance = new ObstacleColor(2, redObstacleColor);

        smallScale = _smallScale;                   //Set variables for Size classes
        mediumScale = _mediumScale;                 //Scales
        largeScale = _largeScale;

        smallBounceTime = _smallBounceTime;         //BounceTime
        mediumBounceTime = _mediumBounceTime;
        largeBounceTime = _largeBounceTime;

        SmallSizeInstance = new ObstacleSize (smallScale, smallBounceTime);
        MediumSizeInstance = new ObstacleSize (mediumScale, mediumBounceTime);
        LargeSizeInstance = new ObstacleSize (largeScale, largeBounceTime);
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

public class ObstacleSize
{
    public float scale;
    public float bounceTime;
    //Add a SoundClass here and to play when bounceing

    //public void PlayBounceSound()             //OBS method for playing sound based on sieze
    //{
     //   Debug.Log("palyed " + scale + " sound on Bounce");
   // }
    
    public ObstacleSize (float _scale, float _bounceTime){
        scale = _scale;
        bounceTime = _bounceTime;
    }
}

public interface Bouncable{
    void SetShape(GameObject obstacleObject){

    }
    
    void Bounce(Transform transform){

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
