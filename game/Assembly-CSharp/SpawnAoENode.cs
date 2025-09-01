using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class SpawnAoENode : EffectNode
{
	// Token: 0x06001A8F RID: 6799 RVA: 0x000A5094 File Offset: 0x000A3294
	internal override void Apply(EffectProperties properties)
	{
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.StartLoc = effectProperties.OutLoc.Copy();
		if (this.LocOverride != null)
		{
			effectProperties.StartLoc = (this.LocOverride as PoseNode).GetPose(effectProperties);
		}
		else if (this.FaceUp)
		{
			effectProperties.StartLoc.OverrideLookAt(Location.WorldUp());
			effectProperties.OutLoc.OverrideLookAt(Location.WorldUp());
		}
		ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
		Vector3 item = originVectors.Item1;
		Vector3 vector = originVectors.Item2;
		if (vector.magnitude == 0f || (this.FaceUp && this.LocOverride == null))
		{
			vector = Vector3.up;
		}
		if (Mathf.Acos(Vector3.Dot(vector, Vector3.up)) * 57.29578f > this.ValidSpawnAngle && this.ValidSpawnAngle > 0f)
		{
			return;
		}
		effectProperties.LocalIndex = UnityEngine.Random.Range(1, 99999999);
		GameObject gameObject = ActionPool.SpawnObject(this.SpawnRef, item, this.SpawnRef.transform.rotation);
		if (this.UseUpAxis && vector != Vector3.zero)
		{
			gameObject.transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
		}
		else
		{
			gameObject.transform.forward = vector;
		}
		if (this.FollowSpawnLocation)
		{
			LockFollow.Follow(gameObject, this, effectProperties, this.LocOverride);
		}
		else
		{
			LockFollow component = gameObject.GetComponent<LockFollow>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
		EntityControl sourceControl = properties.SourceControl;
		bool flag = sourceControl != null && sourceControl.CanTriggerSnippets(EventTrigger.AoESpawned, true, 1f);
		EffectProperties effectProperties2 = effectProperties;
		if (flag)
		{
			effectProperties2 = properties.Copy(false);
		}
		AreaOfEffect component2 = gameObject.GetComponent<AreaOfEffect>();
		if (component2 == null)
		{
			Debug.LogError("Spawned AoE " + this.SpawnRef.name + " did not have an AoE Component!");
			return;
		}
		component2.Setup(this.FollowSpawnLocation ? effectProperties.Copy(false) : effectProperties);
		component2.sourceGUID = this.guid;
		component2.Activate(this.AoESpawn as AoENode);
		if (flag)
		{
			effectProperties2.SourceLocation = component2.transform;
			EntityControl sourceControl2 = properties.SourceControl;
			if (sourceControl2 == null)
			{
				return;
			}
			sourceControl2.TriggerSnippets(EventTrigger.AoESpawned, effectProperties2, 1f);
		}
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x000A52D0 File Offset: 0x000A34D0
	public override void TryCancel(EffectProperties props)
	{
		List<ActionEffect> list = GameplayManager.WorldEffects;
		if (props.SourceControl != null)
		{
			list = props.SourceControl.OwnedEffects;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			ActionEffect actionEffect = list[i];
			if (actionEffect.sourceGUID == this.guid)
			{
				AreaOfEffect areaOfEffect = actionEffect as AreaOfEffect;
				if (areaOfEffect != null && (actionEffect.sourceInstanceID == props.RandSeed || (this.AoESpawn as AoENode).Singleton))
				{
					areaOfEffect.Expire(props, ActionEffect.EffectExpireReason.Cancel, false);
					if (this.StopFollowWhenCanceled)
					{
						areaOfEffect.GetComponent<LockFollow>().enabled = false;
					}
				}
			}
		}
		base.TryCancel(props);
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x000A5379 File Offset: 0x000A3579
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn AoE",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000A53A0 File Offset: 0x000A35A0
	public SpawnAoENode()
	{
	}

	// Token: 0x04001B0F RID: 6927
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001B10 RID: 6928
	public GameObject SpawnRef;

	// Token: 0x04001B11 RID: 6929
	public bool FaceUp;

	// Token: 0x04001B12 RID: 6930
	[Tooltip("Can prevent spawning on walls/roof\n0 - Floor only\n45 - Ramps\n95 - Walls\n180 - Cieling")]
	[Range(0f, 180f)]
	public float ValidSpawnAngle;

	// Token: 0x04001B13 RID: 6931
	[Tooltip("This will force the yellow axis to point up for use with ground effects")]
	public bool UseUpAxis;

	// Token: 0x04001B14 RID: 6932
	public bool FollowSpawnLocation;

	// Token: 0x04001B15 RID: 6933
	public bool FollowSpawnRotation;

	// Token: 0x04001B16 RID: 6934
	public Vector3 RotationOffset;

	// Token: 0x04001B17 RID: 6935
	public bool StopFollowWhenCanceled;

	// Token: 0x04001B18 RID: 6936
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AoENode), false, "AoE Properties", PortLocation.Default)]
	public Node AoESpawn;
}
