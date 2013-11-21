using UnityEngine;
using System.Collections;

public enum UNIT_TYPE {BUILDER, MELEE, MOUNTED, RANGED, NONE}
public enum ABILITY {SPEED_BOOST, SPLASH_DAMAGE, SHIELD} // Not sure if this will be needed any longer? - SethB

public class BaseUnit : MonoBehaviour {
	protected UNIT_TYPE m_unitType;
	
	protected float m_health;
	protected float m_dmg;
	
	public GameObject m_target;	
	protected Vector3 m_targetPos; // want to remove, get targetPos from m_target.transform.position, check line 142 - SethB
	
	protected Vector3 m_currPos;
	protected Vector3 m_currRot;
	
	// Added in a member variable for fireRate and removed it from Attack function pass-in - SethB
	protected float m_attackRate;
	
	// Added in a member variable for checking if the target is attacking or not so that we don't have to
	// check for it in every frame of the movetowards coroutine.
	protected bool m_isTargetAttackable ;
	
	//consider min velocity
	//and max velocity
	public float m_velocity;
	public float m_rotationSpeed;

    // member variables for Ability CoolDowns and Durations - SethB
    public float m_attackRange;
    protected float m_abilityCD;
    protected float m_abilityDuration;
    protected float m_abilityCDTimer;
    protected float m_abilityDurationTimer;
    protected bool m_isAbilityAvailable;
    protected bool m_isAbilityActive;
	
	//added in a population cost to each unit. Spawners should check if it's even possible to create a unit before starting the queue - HM
	public int m_populationCost;
	
	//flocking variables
	protected float randomness;
	private GameObject Controller;
	private bool hasFlock;
	private bool inited = false;
	private float minVelocity;
	private float maxVelocity;	
	
	//necessary animation states
	//public Animator MechanimAnimation?
	public AnimationState idleAnimation;
	public AnimationState attackingAnimation;
	public AnimationState runningAnimation;
	public AnimationState takeDamageAnimation;
	public AnimationState specialMoveAnimation;
	
	// Use this for initialization
	public virtual void Start () 
	{
		m_unitType = UNIT_TYPE.NONE;
		
		m_target = null;
		m_isTargetAttackable = false ;
		m_targetPos = Vector3.zero;
		
		m_currPos = transform.position;
		m_currRot = transform.rotation.eulerAngles;
		m_attackRate = 1.0f ;
		m_rotationSpeed = 2.0f ;

        //StartCoroutine("IdleRoutine");
	}
	
	// Update is called once per frame
	public void Update ()
	{
		
		//turn left
		if(Input.GetKeyDown(KeyCode.A))
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,-50,0), m_rotationSpeed * Time.deltaTime);
			rigidbody.AddRelativeTorque(0,-200,0);
		}
		
		//turn right
		if(Input.GetKeyDown(KeyCode.D))
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,50,0), m_rotationSpeed * Time.deltaTime);
			rigidbody.AddRelativeTorque(0,200,0);
		}
		
		//move forward
		if(Input.GetKeyDown(KeyCode.W))
		{
			rigidbody.AddRelativeForce(0,0,300);	
		}
		
		//move backward
		if(Input.GetKeyDown(KeyCode.S))
		{
			rigidbody.AddRelativeForce(0,0,-300);
		}
		
        // Ability cool downs
        if (m_isAbilityActive)
        {
            m_abilityDurationTimer -= Time.deltaTime;

            if (m_abilityDurationTimer <= 0.0f)
            {
                DeactivateAbility();
            }
        }

        if (m_abilityCDTimer > 0.0f)
            m_abilityCDTimer -= Time.deltaTime;

        if (m_abilityCDTimer <= 0.0f)
            m_isAbilityAvailable = true;
        else
            m_isAbilityAvailable = false;
	}

    public virtual void UseAbility()
    {
        if (m_isAbilityAvailable)
        {
            m_abilityDurationTimer = m_abilityDuration;
            m_abilityCDTimer = m_abilityCD;
            m_isAbilityActive = true;
            m_isAbilityAvailable = false;
        }
        else
            Debug.Log("Ability is on Cooldown: " + m_abilityCDTimer + " Seconds left.");
    }

    public virtual void DeactivateAbility()
    {
        m_abilityDurationTimer = 0.0f;
        m_isAbilityActive = false;
    }
	
	public void SetPosition(Vector3 pos)
	{
		transform.position = pos;
	}
	
	/*
	 * I want to remove this function, there should be no need for it since target position
	 * can be extracted from the target gameobject, but something is calling this function in
	 * PlayerManager.  I request that it be set to use only SetTarget().
	 * - Seth B
	 */
	public void SetTargetPos(Vector3 pos)
	{
		pos.y = 1.5f;// meanwhile we decide what the common Y position will be
		
		m_targetPos = pos;
	}
	
		
	public virtual void MoveTowards(Vector3 target)
	{
		Vector3 direction = target - transform.position;
		
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), m_rotationSpeed * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		m_currPos += forward * m_velocity * Time.deltaTime;
		
		transform.position = m_currPos;
		// unit will fly like superman if we turn this on, is this intended??? - Seth B
		//transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y , 0));
	}
	
	//public virtual void TurnLeft(
	
	/*
	 * Added this function to replace any m_health -= var we had on scripts
	 * - Seth B
	 */
	public virtual void TakeDamage(float dmg)
	{
		m_health -= dmg ;
		if (m_health <= 0)
		{
			m_health = 0 ;
			gameObject.SetActive(false) ;
		}
	}
}
