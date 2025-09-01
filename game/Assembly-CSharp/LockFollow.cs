using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class LockFollow : MonoBehaviour
{
	// Token: 0x06001732 RID: 5938 RVA: 0x00092A44 File Offset: 0x00090C44
	public static LockFollow Follow(GameObject toAffect, Node n, EffectProperties props, Node poseRef)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		Vector3 vector = Vector3.zero;
		float rotFollowSpeed = 0f;
		float locFollowSpeed = 0f;
		bool flag4 = false;
		SpawnVFXNode spawnVFXNode = n as SpawnVFXNode;
		if (spawnVFXNode == null)
		{
			SpawnAoENode spawnAoENode = n as SpawnAoENode;
			if (spawnAoENode == null)
			{
				SpawnBeamNode spawnBeamNode = n as SpawnBeamNode;
				if (spawnBeamNode == null)
				{
					AudioEffectNode audioEffectNode = n as AudioEffectNode;
					if (audioEffectNode != null)
					{
						flag = (audioEffectNode.Loc != null);
					}
				}
				else
				{
					flag = (spawnBeamNode.LocOverride != null);
					flag2 = spawnBeamNode.FollowSpawnRotation;
					rotFollowSpeed = spawnBeamNode.RotationSpeed;
				}
			}
			else
			{
				flag = (spawnAoENode.LocOverride != null);
				flag2 = spawnAoENode.FollowSpawnRotation;
				vector = spawnAoENode.RotationOffset;
				flag4 = spawnAoENode.UseUpAxis;
			}
		}
		else
		{
			flag = (spawnVFXNode.LocOverride != null);
			flag2 = spawnVFXNode.FollowSpawnRotation;
			flag3 = spawnVFXNode.ExpireWithFollow;
			rotFollowSpeed = spawnVFXNode.RotationSpeed;
			locFollowSpeed = spawnVFXNode.FollowSpeed;
		}
		Transform transform = props.SourceLocation;
		if (transform == null || flag)
		{
			transform = props.StartLoc.GetTransform(props);
		}
		if (flag && props.StartLoc.GetLookAt().LocType == LocationType.Transform && transform != null && props.StartLoc.GetLookAt().Offsets.Count == 0)
		{
			flag = false;
		}
		if (!flag && transform == null)
		{
			return null;
		}
		LockFollow orAddComponent = toAffect.GetOrAddComponent<LockFollow>();
		orAddComponent.enabled = true;
		orAddComponent.offsetAngles = vector;
		orAddComponent.RotFollowSpeed = rotFollowSpeed;
		orAddComponent.LocFollowSpeed = locFollowSpeed;
		orAddComponent.UpdateType = ChestFollow.UpdateType.LateUpdate;
		orAddComponent.worldUpAxis = flag4;
		if (poseRef != null)
		{
			PoseNode poseNode = poseRef as PoseNode;
			if (poseNode != null)
			{
				orAddComponent.poseInfo = poseNode;
			}
		}
		if (flag)
		{
			Transform transform2 = props.StartLoc.GetTransform(props);
			GameObject expireWithFollow = (transform2 != null) ? transform2.gameObject : null;
			orAddComponent.Setup(props, flag2, flag3, expireWithFollow);
		}
		else
		{
			orAddComponent.Setup(transform, flag2, flag3);
		}
		orAddComponent.Follow();
		return orAddComponent;
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x00092C32 File Offset: 0x00090E32
	public void Release(float delay)
	{
		base.Invoke("ReleaseImmediate", delay);
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x00092C40 File Offset: 0x00090E40
	public void ReleaseImmediate()
	{
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x00092C48 File Offset: 0x00090E48
	private void Setup(Transform t, bool lockRotation, bool expireWithFollow)
	{
		this.usesLocRot = false;
		this.followObj = t;
		this.lockRotation = lockRotation;
		if (expireWithFollow)
		{
			this.entityFollow = this.followObj.GetComponentInParent<EntityControl>();
		}
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x00092C74 File Offset: 0x00090E74
	private void Setup(EffectProperties props, bool lockRotation, bool expireFollow, GameObject expireWithFollow = null)
	{
		this.usesLocRot = true;
		this.lockRotation = lockRotation;
		this.properties = props;
		if (expireFollow)
		{
			if (expireWithFollow != null)
			{
				this.entityFollow = ((expireWithFollow != null) ? expireWithFollow.GetComponentInParent<EntityControl>() : null);
			}
			else if (props.SourceControl != null)
			{
				this.entityFollow = props.SourceControl;
			}
			else
			{
				Debug.LogError("Follow Object set to expire with entity, but no entity provided");
			}
			this.isFollowingEntity = true;
		}
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x00092CE6 File Offset: 0x00090EE6
	private void LateUpdate()
	{
		if (this.UpdateType == ChestFollow.UpdateType.LateUpdate)
		{
			this.Follow();
		}
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x00092CF8 File Offset: 0x00090EF8
	internal void Follow()
	{
		if (this.usesLocRot)
		{
			this.UpdateLocationRotation();
		}
		else if (this.followObj != null)
		{
			this.UpdateToTransform();
		}
		if (this.isFollowingEntity && (this.entityFollow == null || this.entityFollow.IsDead))
		{
			ActionPool.ReleaseObject(base.gameObject);
		}
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x00092D58 File Offset: 0x00090F58
	private void UpdateLocationRotation()
	{
		Vector3 vector = Vector3.one.INVALID();
		Vector3 vector2 = Vector3.one.INVALID();
		if (this.poseInfo != null)
		{
			if (this.pose == null || this.poseInfo.IsDynamic)
			{
				this.pose = this.poseInfo.GetPose(this.properties);
			}
			ValueTuple<Vector3, Vector3> data = this.pose.GetData(this.properties);
			vector = data.Item1;
			vector2 = data.Item2;
		}
		else if (this.lockRotation)
		{
			ValueTuple<Vector3, Vector3> originVectors = this.properties.GetOriginVectors();
			vector = originVectors.Item1;
			vector2 = originVectors.Item2;
		}
		else
		{
			vector = this.properties.GetOrigin();
		}
		Vector3 vector3 = vector.IsValid() ? vector : Vector3.zero;
		if (this.LocFollowSpeed == 0f)
		{
			base.transform.position = vector3;
		}
		else
		{
			base.transform.position = Vector3.Lerp(base.transform.position, vector3, Time.deltaTime * this.LocFollowSpeed);
		}
		if (this.lockRotation && vector.IsValid())
		{
			if (!vector2.IsValid() || vector2 == Vector3.zero)
			{
				return;
			}
			if (this.worldUpAxis)
			{
				if (this.RotFollowSpeed == 0f)
				{
					base.transform.rotation = Quaternion.LookRotation(vector2, Vector3.up);
					return;
				}
				Vector3 forward = base.transform.forward;
				float num = Vector3.Angle(forward, vector2) * 0.017453292f;
				Vector3 forward2 = Vector3.Lerp(forward, vector2, num * Time.deltaTime * this.RotFollowSpeed);
				base.transform.rotation = Quaternion.LookRotation(forward2, Vector3.up);
				return;
			}
			else
			{
				if (this.RotFollowSpeed == 0f)
				{
					base.transform.forward = vector2;
					return;
				}
				Vector3 forward3 = base.transform.forward;
				float num2 = Vector3.Angle(forward3, vector2) * 0.017453292f;
				base.transform.forward = Vector3.Lerp(forward3, vector2, num2 * Time.deltaTime * this.RotFollowSpeed);
			}
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x00092F50 File Offset: 0x00091150
	private void UpdateToTransform()
	{
		Vector3 vector = this.followObj.position + this.offsetPosition;
		Vector3 vector2 = (this.LocFollowSpeed == 0f) ? vector : Vector3.Lerp(base.transform.position, vector, Time.deltaTime * this.LocFollowSpeed);
		Quaternion rotation = base.transform.rotation;
		if (this.lockRotation)
		{
			Quaternion quaternion = this.worldUpAxis ? (Quaternion.LookRotation(this.followObj.forward, Vector3.up) * Quaternion.Euler(this.offsetAngles)) : (this.followObj.rotation * Quaternion.Euler(this.offsetAngles));
			if (this.RotFollowSpeed == 0f)
			{
				rotation = quaternion;
			}
			else
			{
				rotation = Quaternion.Lerp(base.transform.rotation, quaternion, Time.deltaTime * this.RotFollowSpeed);
			}
		}
		Debug.DrawLine(vector2, vector2 + Vector3.up * 2f, Color.red);
		Debug.DrawLine(this.followObj.position, this.followObj.position + Vector3.up * 2f, Color.red);
		base.transform.SetPositionAndRotation(vector2, rotation);
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x00093091 File Offset: 0x00091291
	public LockFollow()
	{
	}

	// Token: 0x040016F5 RID: 5877
	public Transform followObj;

	// Token: 0x040016F6 RID: 5878
	public bool lockRotation = true;

	// Token: 0x040016F7 RID: 5879
	public Vector3 offsetPosition;

	// Token: 0x040016F8 RID: 5880
	public Vector3 offsetAngles;

	// Token: 0x040016F9 RID: 5881
	private float RotFollowSpeed;

	// Token: 0x040016FA RID: 5882
	private float LocFollowSpeed;

	// Token: 0x040016FB RID: 5883
	public ChestFollow.UpdateType UpdateType = ChestFollow.UpdateType.LateUpdate;

	// Token: 0x040016FC RID: 5884
	public bool worldUpAxis;

	// Token: 0x040016FD RID: 5885
	public bool usesLocRot;

	// Token: 0x040016FE RID: 5886
	private EffectProperties properties;

	// Token: 0x040016FF RID: 5887
	private PoseNode poseInfo;

	// Token: 0x04001700 RID: 5888
	private global::Pose pose;

	// Token: 0x04001701 RID: 5889
	public EntityControl entityFollow;

	// Token: 0x04001702 RID: 5890
	public bool isFollowingEntity;
}
