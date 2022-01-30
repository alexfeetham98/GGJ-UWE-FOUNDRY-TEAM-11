using UnityEngine;
// Require a Rigidbody and LineRenderer object for easier assembly
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (LineRenderer))]
 
public class RopeScript : MonoBehaviour 
{
	/*========================================
	==  Physics Based Rope				==
	==  File: Rope.js					  ==
	==  Original by: Jacob Fletcher		==
	==  Use and alter Freely			 ==
	==  CSharp Conversion by: Chelsea Hash  ==
	==========================================
	*/
 
	public Transform target;
	public float resolution = 0.5F;					    //  Sets the amount of joints there are in the rope (1 = 1 joint for every 1 unit)
	public float ropeDrag = 0.1F;				   	    //  Sets each joints Drag
	public float ropeMass = 0.1F;						//  Sets each joints Mass
	private Vector3[] _segmentPos;						//  DONT MESS!	This is for the Line Renderer's Reference and to set up the positions of the gameObjects
	private GameObject[] _joints;						//  DONT MESS!	This is the actual joint objects that will be automatically created
	private LineRenderer _line;							//  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
	private int _segments;							    //  DONT MESS!	The number of segments is calculated based off of your distance * resolution
	private bool _rope;						            //  DONT MESS!	This is to keep errors out of your debug window! Keeps the rope from rendering when it doesnt exist...
 
	//Joint Settings
	public Vector3 swingAxis = new Vector3(1,1,1);		//  Sets which axis the character joint will swing on (1 axis is best for 2D, 2-3 axis is best for 3D (Default= 3 axis))
	public float lowTwistLimit = -100.0F;						//  The lower limit around the primary axis of the character joint. 
	public float highTwistLimit = 100.0F;						//  The upper limit around the primary axis of the character joint.
	public float swing1Limit  = 20.0F;							//	The limit around the primary axis of the character joint starting at the initialization point.
 
	void Awake()
	{
		BuildRope();
	}
 
	void Update()
	{
		if(_rope && Input.GetKeyDown("f"))
		{
			DestroyRope();	
		}	
		if(!_rope && Input.GetKeyDown("r"))
		{
			BuildRope();
		}
	}
	void LateUpdate()
	{
		// Does rope exist? If so, update its position
		if(_rope) {
			for(int i=0;i<_segments;i++) {
				if(i == 0) {
					_line.SetPosition(i,transform.position);
				} else
				if(i == _segments-1) {
					_line.SetPosition(i,target.transform.position);	
				} else {
					_line.SetPosition(i,_joints[i].transform.position);
				}
			}
			_line.enabled = true;
		} else {
			_line.enabled = false;	
		}
	}
	void BuildRope()
	{
		var transfPos = transform.position;
		var targPos = target.position;
		
		_line = gameObject.GetComponent<LineRenderer>();
		_segments = (int)(Vector3.Distance(transfPos,targPos)*resolution);
		_line.SetVertexCount(_segments);
		_segmentPos = new Vector3[_segments];
		_joints = new GameObject[_segments];
		_segmentPos[0] = transfPos;
		_segmentPos[_segments-1] = targPos;
 
		// Find the distance between each segment
		var segs = _segments-1;
		var seperation = (targPos - transfPos)/segs;
 
		for(int s=1;s < _segments;s++)
		{
			// Find the each segments position using the slope from above
			Vector3 vector = (seperation*s) + transform.position;	
			_segmentPos[s] = vector;
 
			//Add Physics to the segments
			AddJointPhysics(s);
		}
 
		// Attach the joints to the target object and parent it to this object	
		CharacterJoint end = target.gameObject.AddComponent<CharacterJoint>();
		end.connectedBody = _joints[_joints.Length-1].transform.GetComponent<Rigidbody>();
		end.swingAxis = swingAxis;
		SoftJointLimit limitSetter = end.lowTwistLimit;
		limitSetter.limit = lowTwistLimit;
		end.lowTwistLimit = limitSetter;
		limitSetter = end.highTwistLimit;
		limitSetter.limit = highTwistLimit;
		end.highTwistLimit = limitSetter;
		limitSetter = end.swing1Limit;
		limitSetter.limit = swing1Limit;
		end.swing1Limit = limitSetter;
		//target.parent = transform;
		target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
 
		// Rope = true, The rope now exists in the scene!
		_rope = true;
	}
 
	void AddJointPhysics(int n)
	{
		_joints[n] = new GameObject("Joint_" + n);
		_joints[n].transform.parent = transform;
		Rigidbody rigid = _joints[n].AddComponent<Rigidbody>();
		
		SphereCollider sphereCollider = _joints[n].AddComponent<SphereCollider>();
		sphereCollider.radius = _line.startWidth;
		
		CharacterJoint constraint = _joints[n].AddComponent<CharacterJoint>();
		constraint.swingAxis = swingAxis;
		SoftJointLimit limitSetter = constraint.lowTwistLimit;
		limitSetter.limit = lowTwistLimit;
		constraint.lowTwistLimit = limitSetter;
		limitSetter = constraint.highTwistLimit;
		limitSetter.limit = highTwistLimit;
		constraint.highTwistLimit = limitSetter;
		limitSetter = constraint.swing1Limit;
		limitSetter.limit = swing1Limit;
		constraint.swing1Limit = limitSetter;

		_joints[n].transform.position = _segmentPos[n];
 
		rigid.drag = ropeDrag;
		rigid.mass = ropeMass;

		if(n==1){		
			constraint.connectedBody = transform.GetComponent<Rigidbody>();
		} else
		{
			constraint.connectedBody = _joints[n-1].GetComponent<Rigidbody>();	
		}
 
	}
 
	void DestroyRope()
	{
		// Stop Rendering Rope then Destroy all of its components
		_rope = false;
		for(int dj=0;dj<_joints.Length;dj++)
		{
			Destroy(_joints[dj]);	
		}
 
		_segmentPos = new Vector3[0];
		_joints = new GameObject[0];
		_segments = 0;
	}
}