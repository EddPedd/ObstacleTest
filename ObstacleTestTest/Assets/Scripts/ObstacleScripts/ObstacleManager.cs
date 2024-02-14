using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour{
    
    public static ObstacleManager Instance;     //single instance

    //General variables
    [SerializeField]
    private GameObject _pointBall;
    private static GameObject pointBall;
    
    //Color classes
    public ObstacleColor GreenColorInstance {get; private set;}
    public ObstacleColor BlueColorInstance {get; private set;}
    public ObstacleColor RedColorInstance {get; private set;}

    public ObstacleColor [] obstacleColors {get; private set;}  //ObstacleColor Array

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

    //Shape Classes
    public ObstacleShape CircleShapeInstance {get; private set;}
    public Triangle TriangleShapeInstance {get; private set;}
    
    public ObstacleShape [] obstacleShapes {get; private set;}  //ObstacleShape Array

    //Shape variables
    [SerializeField]                            //Shape sprites
    private Sprite _circleShapeSprite;
    private static Sprite circleShapeSprite;
    [SerializeField]
    private Sprite _triangleShapeSprite;
    private static Sprite triangleShapeSprite; 

    [SerializeField]
    private float _triangleBounceDistance;
    private static float triangleBounceDistance;
    private static float triangleScaleMultiplier = 0.6f;


    //Size classes
    public ObstacleSize SmallSizeInstance {get; private set;}
    public ObstacleSize MediumSizeInstance {get; private set;}
    public ObstacleSize LargeSizeInstance {get; private set;}

    public ObstacleSize [] obstacleSizes {get; private set;}        //ObstacleSize Array

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

    [SerializeField]                        //Bounce time Modifiers
    private float _smallBounceTimeModifier;
    private static float smallBounceTimeModifier;
    [SerializeField]
    private float _mediumBounceTimeModifier;
    private static float mediumBounceTimeModifier;
    [SerializeField]
    private float _largeBounceTimeModifier;
    private static float largeBounceTimeModifier;

    [SerializeField]                    //bounce Height
    private float _smallBounceHeight;
    private static float smallBounceHeight;
    [SerializeField]                    
    private float _mediumBounceHeight;
    private static float mediumBounceHeight;    
    [SerializeField]                    
    private float _largeBounceHeight;
    private static float largeBounceHeight;

    private static int smallSpriteLayer = 2;
    private static int mediumSpriteLayer = 1;
    private static int largeSpriteLayer = 0;

    //Create static instances of Color classes
    private void Awake(){

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
            return;
        }

        //Set general variables
        pointBall = _pointBall;


        greenObstacleColor = _greenObstacleColor;   //Set variables for color classes
        blueObstacleColor = _blueObstacleColor;
        redObstacleColor = _redObstacleColor;
        
        GreenColorInstance = new ObstacleColor(0, greenObstacleColor, pointBall);          //Create color classes
        BlueColorInstance = new ObstacleColor(1, blueObstacleColor, pointBall);            //1 for one bounce
        RedColorInstance = new ObstacleColor(2, redObstacleColor, pointBall);
        
        //Set ObstacleColor array
        obstacleColors = new ObstacleColor [] {GreenColorInstance, BlueColorInstance, RedColorInstance};

        //Set variabes for Shapes
        circleShapeSprite = _circleShapeSprite;
        triangleShapeSprite = _triangleShapeSprite;

        triangleBounceDistance = _triangleBounceDistance;

        CircleShapeInstance = new ObstacleShape (circleShapeSprite);
        TriangleShapeInstance = new Triangle (triangleShapeSprite, triangleBounceDistance, triangleScaleMultiplier);

        obstacleShapes = new ObstacleShape [] {CircleShapeInstance, TriangleShapeInstance};     //array for shapes

        //Set variables for Size classes
        smallScale = _smallScale;               //Scales    
        mediumScale = _mediumScale;                 
        largeScale = _largeScale;

        smallBounceTimeModifier = _smallBounceTimeModifier;         //BounceTime
        mediumBounceTimeModifier = _mediumBounceTimeModifier;
        largeBounceTimeModifier = _largeBounceTimeModifier;

        smallBounceHeight = _smallBounceHeight;
        mediumBounceHeight = _mediumBounceHeight;
        largeBounceHeight = _largeBounceHeight;

        SmallSizeInstance = new ObstacleSize (smallScale, smallBounceTimeModifier, smallBounceHeight, smallSpriteLayer);
        MediumSizeInstance = new ObstacleSize (mediumScale, mediumBounceTimeModifier, mediumBounceHeight, mediumSpriteLayer);
        LargeSizeInstance = new ObstacleSize (largeScale, largeBounceTimeModifier, largeBounceHeight, largeSpriteLayer);

        //Set ObstacleSize Array
        obstacleSizes = new ObstacleSize [] {SmallSizeInstance, MediumSizeInstance, LargeSizeInstance};

    }
}

public class ObstacleColor
{
    public int bounces;
    public Color color;
    public GameObject pointBall;

    public ObstacleColor (int _bounces, Color _color, GameObject pointBallPrefab)
    {
        bounces = _bounces;
        color = _color;
        pointBall = pointBallPrefab;
    }
}

public class ObstacleShape
{
    public Sprite shapeSprite;
    public float bounceDistance;
    public float scaleMultiplier = 1f;

    public virtual void AddCollisionCollider(GameObject targetObject)
    {
        CircleCollider2D circleCollider = targetObject.AddComponent<CircleCollider2D>();

        circleCollider.isTrigger = true;
        Debug.Log("Added Circle collision collider");
    }

    public virtual void CalculateBounce(float side, ObstacleScript obstacle)
    {
        obstacle.directionMultiplier = -1f;

        if(obstacle.transform.position.x > obstacle.startPosition.x)
        {
            obstacle.directionMultiplier = 1f;
        }
    }

    public ObstacleShape (Sprite _sprite)
    {
        shapeSprite = _sprite;
    }
}

public class Triangle : ObstacleShape
{
    public override void AddCollisionCollider(GameObject targetObject)
    {
        PolygonCollider2D triangleCollider = targetObject.AddComponent<PolygonCollider2D>();
        triangleCollider.isTrigger = true;

        Vector2 [] points = triangleCollider.GetPath(0);

        Vector2 [] newPoints = new Vector2[3];
        for(int i = 0; i<3; i++)
        {
            newPoints[i] = points[i];
        }
        
        newPoints [0] = new Vector2 (0f, 0.55f);
        newPoints [1] = new Vector2 (0.5f, -0.29f);
        newPoints [2] = new Vector2 (-0.5f, -0.29f);

        triangleCollider.SetPath(0, newPoints);
        Debug.Log("Added triangle Collision collider");
    }

    //Method for deciding which direction in x to bounce in.
    public override void CalculateBounce(float side, ObstacleScript obstacle)
    {
        obstacle.directionMultiplier = -1f;

        if(obstacle.transform.position.x < obstacle.player.position.x)
        {
            obstacle.directionMultiplier = 1f;
        }

        obstacle.xDifference = bounceDistance;
    }

    public Triangle (Sprite _sprite, float _bounceDistance, float _scaleMultiplier) : base(_sprite)
    {
        shapeSprite = _sprite;
        bounceDistance = _bounceDistance;
        scaleMultiplier = _scaleMultiplier;
    }
}

public class ObstacleSize
{
    public float scale;
    public float bounceTimeModifier;
    public float bounceHeight;
    public int spriteLayer;

    //Add a SoundClass here and to play when bounceing

    //public void PlayBounceSound()             //OBS method for playing sound based on sieze
    //{
     //   Debug.Log("palyed " + scale + " sound on Bounce");
   // }
    
    public ObstacleSize (float _scale, float _bounceTimeModifier, float _bounceHeight, int _sprietLayer){
        scale = _scale;
        bounceTimeModifier = _bounceTimeModifier;
        bounceHeight = _bounceHeight;
        spriteLayer = _sprietLayer;

    }
}