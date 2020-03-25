using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EagleMovement : MonoBehaviour
{
    private CharacterController2D m_controller;
    private SpriteRenderer m_spriteRenderer;
    private HealthSystem m_healthSystem;

    [Tooltip("If the sprite face left on the spritesheet, enable this. Otherwise, leave disabled")]
    public bool spriteFaceLeft = false;

    [Header("Scanning settings")]
    [Tooltip("The angle of the forward of the view cone. 0 is forward of the sprite, 90 is up, 180 behind etc.")]
    [Range(0.0f, 360.0f)]
    public float viewDirection = 0.0f;
    [Range(0.0f, 360.0f)]
    public float viewFov;
    public float viewDistance;
    [Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
    public float timeBeforeTargetLost = 3.0f;

    [Header("Attack Data")]
    [EnemyRangeCheck]
    public float Range = 3.0f;





    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 10f;

    private Path path;
    int currentWayPoint = 0;
    Seeker seeker;

    Vector2 m_Move;

    // Start is called before the first frame update
    void Awake()
    {
        m_controller = GetComponent<CharacterController2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_healthSystem = GetComponent<HealthSystem>();
        seeker = GetComponent<Seeker>();

        m_healthSystem.OnDeathEvent.AddListener(OnDeath);
    }
    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (!seeker.IsDone()) { return; }

        seeker.StartPath(m_controller.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

        //path can be completed with some timeback
        /// TODO: Check for nearest visible point and start from it

    }
    private void OnDestroy()
    {
        m_healthSystem.OnDeathEvent.RemoveListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        m_Move = Vector2.zero;

        if (path == null) { return; }

        if (currentWayPoint >= path.vectorPath.Count) { return; }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - m_controller.position).normalized;
        m_Move = direction * speed;

        Vector3 dir = (Vector3)m_controller.position - path.vectorPath[currentWayPoint];

        if (dir.sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if (m_Move.x > 0.01f)
        {
            m_spriteRenderer.flipX = spriteFaceLeft;
        }
        else if (m_Move.x < -0.01f)
        {
            m_spriteRenderer.flipX = !spriteFaceLeft;
        }

    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        m_controller.Move(m_Move * Time.fixedDeltaTime, false, false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //draw the cone of view
        Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
        forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

        if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

        Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

        Handles.color = new Color(0, 1.0f, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

        //Draw attack range
        Handles.color = new Color(1.0f, 0, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.back, Range);
    }
#endif
}

//bit hackish, to avoid to have to redefine the whole inspector, we use an attirbute and associated property drawer to 
//display a warning above the melee range when it get over the view distance.
public class EnemyRangeCheckAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnemyRangeCheckAttribute))]
public class EnemyRangePropertyDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty viewRangeProp = property.serializedObject.FindProperty("viewDistance");
        if (viewRangeProp.floatValue < property.floatValue)
        {
            Rect pos = position;
            pos.height = 30;
            EditorGUI.HelpBox(pos, "Melee range is bigger than View distance. Note enemies only attack if target is in their view range first", MessageType.Warning);
            position.y += 30;
        }

        EditorGUI.PropertyField(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty viewRangeProp = property.serializedObject.FindProperty("viewDistance");
        if (viewRangeProp.floatValue < property.floatValue)
        {
            return base.GetPropertyHeight(property, label) + 30;
        }
        else
            return base.GetPropertyHeight(property, label);
    }
}
#endif