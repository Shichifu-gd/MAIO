using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnSwipeInput(DirectionTravel type);
    public static event OnSwipeInput SwipeEvent;

    private bool IsDragging;
    private bool IsMobilePlatform;

    private float MinSwipeDelta;

    private Vector2 TapPoint;
    private Vector2 SwipeDelta;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        IsMobilePlatform = false;
        MinSwipeDelta = 60;
#else
            MinSwipeDelta = 130f;
            isMobilePlatform = true;
#endif
    }

    private void Update()
    {
        if (!IsMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsDragging = true;
                TapPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0)) ResetSwipe();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    IsDragging = true;
                    TapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended) ResetSwipe();
            }
        }
        CalculateSwipe();
    }

    private void CalculateSwipe()
    {
        SwipeDelta = Vector2.zero;

        if (IsDragging)
        {
            if (!IsMobilePlatform && Input.GetMouseButton(0)) SwipeDelta = (Vector2)Input.mousePosition - TapPoint;
            else if (Input.touchCount > 0) SwipeDelta = Input.touches[0].position - TapPoint;
        }
        if (SwipeDelta.magnitude > MinSwipeDelta)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(SwipeDelta.x) > Mathf.Abs(SwipeDelta.y)) SwipeEvent(SwipeDelta.x < 0 ? DirectionTravel.West : DirectionTravel.East);
                else SwipeEvent(SwipeDelta.y > 0 ? DirectionTravel.North : DirectionTravel.South);
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        IsDragging = false;
        TapPoint = SwipeDelta = Vector2.zero;
    }
}