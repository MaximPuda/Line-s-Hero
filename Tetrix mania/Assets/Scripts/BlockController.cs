using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BlockController : MonoBehaviour

{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Spawner2 spawner;
    [SerializeField] private TrailHandler trail;

    [Header("Movement")]
    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float horizontalMoveTime = 0.8f;
    [SerializeField] private float fallingDownSpeed = 0.04f;
    [SerializeField] private float verticalDeadZone = 80;
    [SerializeField] private float horizontalDeadZone = 120;
    [SerializeField, Range(1, 3)] private float sens = 2;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> OnHasLine;
    [SerializeField] private UnityEvent OnBlockGrounded;
    [SerializeField] private UnityEvent OnRotate;
    [SerializeField] private UnityEvent OnMove;
    [SerializeField] private UnityEvent<int> OnHasCombo;

    private Transform activeBlockTransform;
    private Transform rotationPoint;

    private float previousTime;
    private float activeSpeed;
    private float previousSideMoveTime;
    private bool isPressed;

    private static int height = 20;
    private static int width = 10;

    private static Transform[,] grid;

    private Vector2 startSwipe;
    private bool isSwipeMoved;


    private void Awake()
    {
        grid = new Transform[width, height];
        activeSpeed = normalSpeed;
        SetSens(PlayerPrefs.GetFloat("Sens"));
    }

    private void Update()
    {
        if (!GameManager.isGamePaused && !GameManager.isGameEnded && activeBlockTransform != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
               isPressed = true;

            if (isPressed && (Input.GetKey(KeyCode.S)))
            {
                trail.EmissionEnable();
                activeSpeed = fallingDownSpeed;
                isPressed = false;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                trail.EmissionDesable();
                activeSpeed = normalSpeed;
            }

            if (Time.time - previousTime > activeSpeed)
                FallingDown();

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                MoveLeft();

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                MoveRight();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                spawner.Hold();

            if (Input.GetKeyDown(KeyCode.Space))
                Rotate();

            if (Input.GetKeyDown(KeyCode.Escape))
                gameManager.Pause();

            if (Input.touchCount > 0)
                Swipe();
        } 
    }

    private void Swipe()
    {
        Touch activeTouch = Input.GetTouch(0);

        if (activeTouch.phase == TouchPhase.Began)
        {
            startSwipe = activeTouch.position;
            isPressed = true;
            isSwipeMoved = false;
        }

        if(activeTouch.phase == TouchPhase.Moved)
        {
            switch (GetSwipeDirection(activeTouch))
            {
                case SwipeDirection.Left:
                    MoveLeftSwipe();
                    startSwipe = activeTouch.position;
                    isSwipeMoved = true;
                    break;

                case SwipeDirection.Right:
                    MoveRightSwipe();
                    startSwipe = activeTouch.position;
                    isSwipeMoved = true;
                    break;

                case SwipeDirection.Up:
                    spawner.Hold();
                    isSwipeMoved = true;
                    break;

                case SwipeDirection.Down:
                    if (isPressed)
                    {
                        trail.EmissionEnable();
                        activeSpeed = fallingDownSpeed;
                        isSwipeMoved = true;
                        isPressed = false;
                    }
                    break;

                default:
                    break;
            }
        }

        if (activeTouch.phase == TouchPhase.Ended || activeTouch.phase == TouchPhase.Canceled)
        {
            if (!isSwipeMoved && Mathf.Abs(activeTouch.position.x - startSwipe.x) < horizontalDeadZone / sens
                && Mathf.Abs(activeTouch.position.y - startSwipe.y) < verticalDeadZone)
                Rotate();

            trail.EmissionDesable();
            activeSpeed = normalSpeed;
        }
    }

    private SwipeDirection GetSwipeDirection(Touch touch)
    {
        var deltaX = touch.position.x - startSwipe.x;
        var deltaY = touch.position.y - startSwipe.y;
        var absDeltaX = Mathf.Abs(deltaX);
        var absDeltaY = Mathf.Abs(deltaY);


        if (absDeltaX > absDeltaY && absDeltaX > horizontalDeadZone / sens)
        {
            if (deltaX < 0)
                return SwipeDirection.Left;
            
            return SwipeDirection.Right;
        }
        
        if (absDeltaY > absDeltaX && absDeltaY > verticalDeadZone)
        {
            if (deltaY < 0)
                return SwipeDirection.Down;
            
            return SwipeDirection.Up;
        }
 
        return SwipeDirection.Tap;
    }

    private void MoveLeft()
    {
        if (Time.time - previousSideMoveTime > horizontalMoveTime / 10)
        {
            activeBlockTransform.position += Vector3.left;

            if (!CanMove())
                activeBlockTransform.position += Vector3.right;
            else
                OnMove.Invoke();

            previousSideMoveTime = Time.time;
        }
    }

    private void MoveLeftSwipe()
    {
        activeBlockTransform.position += Vector3.left;

        if (!CanMove())
            activeBlockTransform.position += Vector3.right;
        else
            OnMove.Invoke();
    }

    private void MoveRight()
    {
        if (Time.time - previousSideMoveTime > horizontalMoveTime / 10)
        {
            activeBlockTransform.position += Vector3.right;

            if (!CanMove())
                activeBlockTransform.position += Vector3.left;
            else
                OnMove.Invoke();

            previousSideMoveTime = Time.time;
        }
    }

    private void MoveRightSwipe()
    {
        activeBlockTransform.position += Vector3.right;

        if (!CanMove())
            activeBlockTransform.position += Vector3.left;
        else
            OnMove.Invoke();
    }

    private void Rotate()
    {
        var savePosition = activeBlockTransform.position;
        var saveRotation = activeBlockTransform.rotation;

        activeBlockTransform.RotateAround(rotationPoint.position, new Vector3(0, 0, 1), 90);
        var offset = GetRotationOffset();
        activeBlockTransform.position += offset;

        if (!CanMove())
        {
            activeBlockTransform.position = savePosition;
            activeBlockTransform.rotation = saveRotation;
        }
        else
            OnRotate.Invoke();

    }

    private Vector3 GetRotationOffset()
    {
        var offset = new Vector3();
        var addedX = new List<int>();
        bool flag = false;
        
        foreach  (Transform children in activeBlockTransform)
        {
            if (children.tag == "Cube")
            {
                int roundedX = Mathf.RoundToInt(children.position.x);
                int roundedY = Mathf.RoundToInt(children.position.y);

                if(addedX.Count > 0)
                {
                    for (int i = 0; i < addedX.Count; i++)
                    {
                        if (roundedX == addedX[i])
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            addedX.Add(roundedX);
                            flag = false;
                            break;
                        }
                    }
                }
                else
                    addedX.Add(roundedX);


                if (flag)
                {
                    flag = true;
                    continue;
                }

                if (roundedY < 0)
                    offset += Vector3.up;
                else if (roundedY >= height)
                        offset += Vector3.down;
                else if (roundedX < 0)
                    offset += Vector3.right;
                else if (roundedX >= width)
                    offset += Vector3.left;
                else if (grid[roundedX, roundedY] != null && roundedX < rotationPoint.position.x)
                    offset += Vector3.right;
                else if (grid[roundedX, roundedY] != null && roundedX > rotationPoint.position.x)
                    offset += Vector3.left;
            }
        }

        return offset;
    }

    private void FallingDown()
    {
        if (!GameManager.isGameEnded)
        {

            activeBlockTransform.position += Vector3.down;

            if (!CanMove())
            {
                activeBlockTransform.position += Vector3.up;
                AddToGrid();
                CheckForLines();
                if(!GameManager.isGameEnded)
                    spawner.SpawnNewBlock();
                else
                    gameManager.GameOver();
            }
        }
        else
            gameManager.GameOver();

        previousTime = Time.time;

    }

    private bool CanMove()
    {
        foreach (Transform children in activeBlockTransform)
        {
            if (children.tag == "Cube")
            {
                int roundedX = Mathf.RoundToInt(children.position.x);
                int roundedY = Mathf.RoundToInt(children.position.y);

                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                    return false;
   
                if (grid[roundedX, roundedY] != null)
                    return false;
            }
        }

        return true;
    }

    private void AddToGrid()
    {
        foreach (Transform children in activeBlockTransform)
        {
            if (children.tag == "Cube")
            {
                int roundedX = Mathf.RoundToInt(children.position.x);
                int roundedY = Mathf.RoundToInt(children.position.y);

                if (roundedY < grid.GetLength(1)-1)
                {
                    OnBlockGrounded.Invoke();
                    grid[roundedX, roundedY] = children;
                }
                else
                    GameManager.isGameEnded = true;
            }
        }
    }

    private  void CheckForLines()
    {
        int combo = 0;

        for (int row = height - 1; row >= 0; row--)
        {
            if (HasLine(row))
            {
                OnHasLine.Invoke(row);
                DeleteLine(row);
                RowDown(row);
                combo++;
            }
        }

        if (combo > 0)
            OnHasCombo.Invoke(combo);
    }

    private bool HasLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            if (grid[column, row] == null)
                return false;
        }
        return true;
    }

    private void DeleteLine(int row)
    {
        for (int column = 0; column < width; column++)
        {
            Destroy(grid[column, row].gameObject);
            grid[column, row] = null;
        }
    }

    private void RowDown(int row)
    {
        for (int activeRow = row; activeRow < height; activeRow++)
        {
            for (int column = 0; column < width; column++)
            {
                if (grid[column,activeRow] != null)
                {
                    grid[column, activeRow - 1] = grid[column, activeRow];
                    grid[column, activeRow] = null;
                    grid[column, activeRow - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void SetActiveBlock(Transform blockTransform)
    {
        activeBlockTransform = blockTransform;
        rotationPoint = activeBlockTransform.GetChild(4);
        trail.EmissionDesable();
        trail.SetActiveBlock(blockTransform);
        isPressed = false;
        activeSpeed = normalSpeed;
    }

    public Transform GetActiveBlock()
    {
        return activeBlockTransform;
    }

    public void SetSpeed(float newSpeed)
    {
        normalSpeed = newSpeed;
        activeSpeed = normalSpeed;
    }

    public void SetSens(float value)
    {
        if (value < 1)
            sens = 1;
        else if (value > 3)
            sens = 3;
        else
            sens = value;

        PlayerPrefs.SetFloat("Sens", sens);
    }
}

public enum SwipeDirection { Left, Right, Up, Down, Tap }
