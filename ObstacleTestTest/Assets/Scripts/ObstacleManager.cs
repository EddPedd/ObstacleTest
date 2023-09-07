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
    [SerializeField]
    private AnimationCurve _gravity;
    private static AnimationCurve gravity;

    [SerializeField]                        //Scale
    private float _smallScale;
    private static float smallScale;
    [SerializeField]
    private float _mediumScale;
    private static float mediumScale;
    [SerializeField]
    private float _largeScale;
    private static float largeScale;

    [SerializeField]                        //Bounce time Modifiers
    private float _smallBounceTimeModifier;
    private static float smallBounceTimeModifier;
    [SerializeField]
    private float _mediumBounceTimeModifier;
    private static float mediumBounceTimeModifier;
    [SerializeField]
    private float _largeBounceTimeModifier;
    private static float largeBounceTimeModifier;


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

        smallBounceTimeModifier = _smallBounceTimeModifier;         //BounceTime
        mediumBounceTimeModifier = _mediumBounceTimeModifier;
        largeBounceTimeModifier = _largeBounceTimeModifier;

        gravity = _gravity;

        SmallSizeInstance = new ObstacleSize (smallScale, smallBounceTimeModifier, gravity);
        MediumSizeInstance = new ObstacleSize (mediumScale, mediumBounceTimeModifier, gravity);
        LargeSizeInstance = new ObstacleSize (largeScale, largeBounceTimeModifier, gravity);
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
    public float bounceTimeModifier;
    public AnimationCurve gravityCurve;

    //Add a SoundClass here and to play when bounceing

    //public void PlayBounceSound()             //OBS method for playing sound based on sieze
    //{
     //   Debug.Log("palyed " + scale + " sound on Bounce");
   // }
    
    public ObstacleSize (float _scale, float _bounceTimeModifier, AnimationCurve _gravityCurve){
        scale = _scale;
        bounceTimeModifier = _bounceTimeModifier;
        gravityCurve = _gravityCurve;
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
