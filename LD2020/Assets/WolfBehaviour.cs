using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The below class makes the assumption that chase range is strictly larger than growl range
/// </summary>
public class WolfBehaviour : MonoBehaviour, IGrowlRangeHandler, IChaseRangeHandler
{
    public AudioSource growlAudioSource;
    public GameObject idleModel;
    public GameObject alertModel;
    public GameObject growlModel;
    public List<GameObject> chaseModels;

    public const int MaxMagnitude = 30;
    public const int MoveForce = 200;
    public double chaseAnimationSpeed = 0.1;
    private List<GameObject> targetsInGrowlRange;
    public WolfState state;
    GameObject chaseTarget;
    public float speed;
    public readonly float maxGrowlTime = 1; // Default 2 seconds of growl time
    private float remainingGrowlTime;
    private double chaseAnimationTimer;
    private int chaseAnimationFrame;
    private MusicPlayer _musicPlayer;
    private Rigidbody _rb;
    private Collider _collider;
    private Renderer _renderer;

    public GameObject optionalPreLoadedTarget;


    // Start is called before the first frame update

    public WolfState GetState ()
    {
        return state;
    }
    void Start()
    {
        _musicPlayer = GameObject.FindWithTag("musicPlayer").GetComponent<MusicPlayer>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        state = WolfState.Sleeping;
        targetsInGrowlRange = new List<GameObject>();
        targetsInGrowlRange = new List<GameObject>();

        if (optionalPreLoadedTarget)
        {
            chaseTarget = optionalPreLoadedTarget;
            EnterChaseState();
        }
    }

    /// <summary>
    /// Choose the closest target in growl range.
    /// </summary>
    /// <returns>True if successfully found a legal target and false otherwise</returns>
    public bool ChoosePriorityTarget()
    {
        float smallestDist = float.MaxValue;
        chaseTarget = null;
        var result = false;
        foreach (GameObject targ in targetsInGrowlRange)
        {
            float dist = Vector3.Distance(transform.position, targ.transform.position);
            if (dist <= smallestDist)
            {
                smallestDist = dist;
                result = true;
                chaseTarget = targ;
            }
        }

        return result;
    }

    void Update()
    {
        switch (state)
        {
            case WolfState.Sleeping:
                break;
            case WolfState.Growl:
                ChoosePriorityTarget();
                remainingGrowlTime -= Time.deltaTime;
                if (remainingGrowlTime <= 0)
                {
                    if (chaseTarget)
                    {
                        EnterChaseState();
                    } else
                    {
                        // This shouldn't happen
                        Debug.LogWarning("Wolf unexpectedly failed to chase and slept instead");
                        EnterSleepState();
                    }
                }

                break;
            case WolfState.Chase:
                chaseAnimationTimer -= Time.deltaTime;
                if (chaseAnimationTimer <= 0)
                {
                    chaseAnimationFrame += 1;
                    chaseAnimationTimer = chaseAnimationSpeed;
                    SetActiveModel(chaseModels[chaseAnimationFrame % chaseModels.Count]);
                }

                break;
            case WolfState.Alert:
                remainingGrowlTime -= Time.deltaTime;
                if (remainingGrowlTime <= 0)
                {
                    // He might get woken by someone entering growl range and immediately chase from there
                    EnterSleepState();
                }

                break;
        }
    }

    void FixedUpdate()
    {
        if (chaseTarget == null)
        {
            return;
        }

        if (state == WolfState.Growl)
        {
            var yLessTargetPosition = new Vector3(chaseTarget.transform.position.x, transform.position.y, chaseTarget.transform.position.z);
            transform.rotation = Quaternion.LookRotation(yLessTargetPosition - transform.position);
        } else if (state == WolfState.Chase)
        {
            var yLessTargetPosition = new Vector3(chaseTarget.transform.position.x, transform.position.y, chaseTarget.transform.position.z);
            /*transform.position = Vector3.MoveTowards(transform.position, yLessTargetPosition, speed * Time.fixedDeltaTime);*/
            
            float moveForce = MoveForce * Time.fixedDeltaTime;
            var influenceVector = yLessTargetPosition - transform.position;

            influenceVector = Vector3.Normalize(influenceVector) * moveForce;

            var limitedVelocity = LimitProposedVelocity(_rb, influenceVector);
            if (speed != 0 && limitedVelocity.magnitude > speed)
            {
                limitedVelocity = Vector3.Normalize(limitedVelocity) * speed;
            }
            _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
            transform.rotation = Quaternion.LookRotation(influenceVector);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), _collider);
        }
    }

    public void OnEnterGrowlRange(object sender, GrowlRangeArgs args)
    {
        targetsInGrowlRange.Add(args.gameObject);
        switch (state)
        {
            case WolfState.Sleeping:
                EnterGrowlState();
                break;
            case WolfState.Growl:
                break;
            case WolfState.Chase:
                break;
            case WolfState.Alert:
                ChoosePriorityTarget();
                EnterChaseState();
                break;
        }
    }

    public void OnExitGrowlRange(object sender, GrowlRangeArgs args)
    {
        targetsInGrowlRange.Remove(args.gameObject);

        switch (state)
        {
            case WolfState.Sleeping:
                break;
            case WolfState.Growl:
                if (targetsInGrowlRange.Count == 0)
                {
                    EnterSleepState();
                }

                break;
            case WolfState.Chase:
                break;
            case WolfState.Alert:
                break;
        }
    }

    public void OnEnterChaseRange(object sender, ChaseRangeArgs args)
    {
        switch (state)
        {
            case WolfState.Sleeping:
                break;
            case WolfState.Growl:
                break;
            case WolfState.Chase:
                break;
            case WolfState.Alert:
                break;
        }
    }

    public void OnExitChaseRange(object sender, ChaseRangeArgs args)
    {
        switch (state)
        {
            case WolfState.Sleeping:
                break;
            case WolfState.Growl:
                break;
            case WolfState.Chase:
                Debug.Log("Left chase range");
                if (!optionalPreLoadedTarget)
                {
                    if (args.gameObject == chaseTarget)
                    {
                        Debug.Log("No longer chasing target. Alert Mode");
                        EnterAlertState();
                    }
                }
            
                break;
            case WolfState.Alert:
                break;
        }
    }

    private void EnterSleepState()
    {
        chaseTarget = null;
        state = WolfState.Sleeping;
        SetActiveModel(idleModel);
    }

    private void EnterGrowlState()
    {
        _musicPlayer.PlayWolfGrowlSound(UnityEngine.Random.Range(0, 1));
        chaseTarget = null;
        state = WolfState.Growl;

        remainingGrowlTime = maxGrowlTime;
        SetActiveModel(growlModel);

    }

    private void EnterChaseState()
    {
        _musicPlayer.PlayWolfChaseSound(UnityEngine.Random.Range(0, 4));
        state = WolfState.Chase;
        chaseAnimationTimer = chaseAnimationSpeed;
        SetActiveModel(chaseModels[0]);
    }

    private void EnterAlertState()
    {
        if (ChoosePriorityTarget())
        {
            // Go from alert to growl if something was in our zone the whole time
            EnterGrowlState();
        } else
        {
            remainingGrowlTime = maxGrowlTime;
            state = WolfState.Alert;
            SetActiveModel(alertModel);
        }
    }

    private static Vector3 LimitProposedVelocity(Rigidbody rb, Vector3 influenceVector)
    {
        var existingVector = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        var newVector = existingVector + influenceVector;
        var newMagnitude = newVector.magnitude;

        // Check that new magnitude is within bounds
        var existingMagnitude = existingVector.magnitude;
        var magnitudeLimit = Math.Max(MaxMagnitude, existingMagnitude);
        if (newMagnitude > magnitudeLimit)
        {
            newVector = Vector3.Normalize(newVector) * magnitudeLimit;
        }

        return newVector;
    }

    private void SetActiveModel(GameObject newActiveModel)
    {
        idleModel.SetActive(false);
        growlModel.SetActive(false);
        alertModel.SetActive(false);
        foreach(var chaseModel in chaseModels)
        {
            chaseModel.SetActive(false);
        }

        newActiveModel.SetActive(true);
    }
}
