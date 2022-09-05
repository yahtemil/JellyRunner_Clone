using UnityEngine;
using UnityEngine.Events;


public class SwerveConrol : MonoSingleton<SwerveConrol>
{
    #region Serialized Variables

    [SerializeField]
    [Tooltip("You can choose slide factor to make it slow or fast")]
    [Range(0f, 1f)]
    private float slidingFactor = .1f;

    [SerializeField] private UnityEvent ClickDown;
    [SerializeField] private UnityEvent Click;
    [SerializeField] private UnityEvent ClickUp;

    #endregion

    #region Private Variables

    private const int ResolutionReferenceY = 1920;
    private const int ResolutionReferenceX = 1080;

    private float resolutionFactorX = 1;
    private float resolutionFactorY = 1;

    private Vector2 touchStart = Vector2.zero;
    private Vector2 touchEnd = Vector2.zero;
    private float upDownSlide;
    private float leftRightSlide;

    #endregion

    #region Initialization

    private void Start()
    {
        resolutionFactorX = (float)ResolutionReferenceX / Screen.width;
        resolutionFactorY = (float)ResolutionReferenceY / Screen.height;
    }

    #endregion

    #region Control Loop

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            ClickDown.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            touchEnd = Input.mousePosition;

            upDownSlide = (touchEnd.y - touchStart.y) * resolutionFactorY * slidingFactor;
            leftRightSlide = (touchEnd.x - touchStart.x) * resolutionFactorX * slidingFactor;
            Click.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ClickUp.Invoke();
        }
    }

    #endregion

    public float GetUpDownSlide()
    {
        return upDownSlide;
    }

    public float GetLeftRightSlide()
    {
        return leftRightSlide;
    }
}
