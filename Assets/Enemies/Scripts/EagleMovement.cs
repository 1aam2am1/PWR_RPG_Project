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
    public readonly float timeBeforeTargetLost = 3.0f;

    [Header("Attack Data")]
    [EnemyRangeCheck]
    public float Range = 3.0f;





    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = .7f;

    private Path path;
    int currentWayPoint = 0;
    Seeker seeker;

    Vector2 m_Move;
    float m_TimeSinceLastTargetView;

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
        if (m_TimeSinceLastTargetView <= .1f) { return; }

        seeker.StartPath(m_controller.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

        float distance_sqr = float.MaxValue;
        for (int i = 0; i < path.vectorPath.Count; i++)
        {
            Vector3 dir = (Vector3)m_controller.position - path.vectorPath[i];

            if (dir.sqrMagnitude <= distance_sqr) { distance_sqr = dir.sqrMagnitude; currentWayPoint = i; }
        }
    }
    public void ScanForPlayer()
    {

        Vector3 dir = target.position - transform.position;

        if (dir.sqrMagnitude > viewDistance * viewDistance)
        {
            return;
        }


        Vector2 m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;

        Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
        if (m_spriteRenderer.flipX) testForward.x = -testForward.x;

        float angle = Vector3.Angle(testForward, dir);

        if (angle > viewFov * 0.5f)
        {
            return;
        }

        m_TimeSinceLastTargetView = timeBeforeTargetLost;
    }
    private void OnDestroy()
    {
        m_healthSystem.OnDeathEvent.RemoveListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        m_Move = Vector2.zero;

        ScanForPlayer();

        if (path == null) { return; }

        if (currentWayPoint >= path.vectorPath.Count) { return; }

        Vector3 dir = (Vector3)m_controller.position - path.vectorPath[currentWayPoint];

        if (dir.sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if (currentWayPoint >= path.vectorPath.Count) { return; }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - m_controller.position).normalized;
        m_Move = direction * speed;

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

        if (m_TimeSinceLastTargetView > 0.0f)
            m_TimeSinceLastTargetView -= Time.fixedDeltaTime;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        {
            Vector2 m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
            //if (m_spriteRenderer.flipX) m_SpriteForward = -m_SpriteForward;

            Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
            if (GetComponent<SpriteRenderer>().flipX) testForward.x = -testForward.x;

            Gizmos.DrawLine(transform.position, transform.position + testForward.normalized * 5f);
        }

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


        if (path != null && currentWayPoint < path.vectorPath.Count)
        {
            Vector3 nextPosition = path.vectorPath[currentWayPoint];

            Handles.color = new Color(.5f, .5f, 0, 0.1f);
            Handles.DrawSolidDisc(nextPosition, Vector3.back, .5f);
        }
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
