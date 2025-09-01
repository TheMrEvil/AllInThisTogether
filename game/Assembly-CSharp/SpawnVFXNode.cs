using System;
using Sirenix.OdinInspector;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class SpawnVFXNode : EffectNode
{
	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000A5B6F File Offset: 0x000A3D6F
	internal override bool CopyProps
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000A5B72 File Offset: 0x000A3D72
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn VFX",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x000A5B9C File Offset: 0x000A3D9C
	internal override void Apply(EffectProperties properties)
	{
		if (this.VFXObj == null)
		{
			return;
		}
		Vector3 position = Vector3.zero;
		Vector3 vector = Vector3.up;
		if (this.LocOverride != null)
		{
			ValueTuple<Vector3, Vector3> vectors = (this.LocOverride as PoseNode).GetVectors(properties);
			position = vectors.Item1;
			vector = vectors.Item2;
		}
		else
		{
			ValueTuple<Vector3, Vector3> originVectors = properties.GetOriginVectors();
			position = originVectors.Item1;
			vector = originVectors.Item2;
		}
		GameObject gameObject = ActionPool.SpawnObject(this.VFXObj, position, this.VFXObj.transform.rotation);
		if (this.UseUpAxis && vector.IsValid() && vector != Vector3.zero)
		{
			gameObject.transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
		}
		else
		{
			gameObject.transform.forward = ((vector == Vector3.zero) ? Vector3.up : vector);
		}
		object obj = this.DynamicScale != null && this.DynamicScale is NumberNode;
		EffectProperties effectProperties = properties;
		object obj2 = obj;
		if (obj2 != null || this.FollowSpawnLocation)
		{
			effectProperties = properties.Copy(false);
			if (this.LocOverride != null)
			{
				effectProperties.StartLoc = (effectProperties.OutLoc = (this.LocOverride as PoseNode).GetPose(effectProperties));
			}
			else
			{
				effectProperties.StartLoc = effectProperties.OutLoc.Copy();
			}
		}
		if (obj2 != null)
		{
			NumberNode numberNode = this.DynamicScale as NumberNode;
			if (numberNode != null)
			{
				if (this.UpdateScale)
				{
					ActionScaler.AddScalar(gameObject, effectProperties, numberNode);
					goto IL_1E4;
				}
				gameObject.transform.localScale *= Mathf.Max(numberNode.Evaluate(properties), 0.05f);
				goto IL_1E4;
			}
		}
		if (this.MultiplyEntityScale)
		{
			EntityControl applicationEntity = properties.GetApplicationEntity(this.ScaleTarget);
			if (applicationEntity != null)
			{
				gameObject.transform.localScale *= applicationEntity.display.VFXScaleFactor * applicationEntity.display.Size;
			}
		}
		IL_1E4:
		AudioManager.SetupSFXObject(gameObject, properties.SourceControl is PlayerControl && properties.SourceControl != PlayerControl.myInstance);
		if (this.FollowSpawnLocation)
		{
			LockFollow.Follow(gameObject, this, effectProperties, this.LocOverride);
		}
		foreach (EffectBase effectBase in gameObject.GetAllComponents<EffectBase>())
		{
			effectBase.SetupInfo(effectProperties);
		}
		foreach (DynamicDecal dynamicDecal in gameObject.GetAllComponents<DynamicDecal>())
		{
			dynamicDecal.Show();
		}
		if (this.CanCancel)
		{
			gameObject.GetOrAddComponent<VFXSpawnRef>().Setup(effectProperties, this.guid, this.RequireIndexMatch);
		}
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x000A5E70 File Offset: 0x000A4070
	public override void TryCancel(EffectProperties props)
	{
		this.OnCancel(props);
		base.TryCancel(props);
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000A5E80 File Offset: 0x000A4080
	internal override void OnCancel(EffectProperties props)
	{
		if (!this.CanCancel)
		{
			return;
		}
		VFXSpawnRef vfx = VFXSpawnRef.GetVFX(props, this.guid);
		if (vfx == null || !vfx.gameObject.activeSelf)
		{
			return;
		}
		vfx.Release();
		GameObject gameObject = vfx.gameObject;
		foreach (ActionEffect actionEffect in gameObject.GetAllComponents<ActionEffect>())
		{
			actionEffect.Expire(props, ActionEffect.EffectExpireReason.Cancel, false);
		}
		if (this.FollowSpawnLocation && this.StopFollowOnCancel)
		{
			LockFollow component = gameObject.GetComponent<LockFollow>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
			}
		}
		vfx.CancelEffect(this.DestroyTime);
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000A5F40 File Offset: 0x000A4140
	private bool ValidateObject(GameObject gameObject, ref string errorMessage, ref InfoMessageType? messageType)
	{
		if (gameObject == null || (this.FollowSpawnLocation && this.ExpireWithFollow))
		{
			return true;
		}
		if (this.CalledFrom == null || (!(this.CalledFrom is ActionRootNode) && !(this.CalledFrom is AbilityRootNode)))
		{
			return true;
		}
		if (this.CanCancel)
		{
			return true;
		}
		if (gameObject.GetComponent<DestroyTimed>() != null)
		{
			return true;
		}
		errorMessage = "Effect has no DestroyTimed component!\nIt will never get removed, use an empty AoE or Status Effect.";
		return false;
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x000A5FB4 File Offset: 0x000A41B4
	public SpawnVFXNode()
	{
	}

	// Token: 0x04001B31 RID: 6961
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Header)]
	public Node LocOverride;

	// Token: 0x04001B32 RID: 6962
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Scale", PortLocation.Default)]
	public Node DynamicScale;

	// Token: 0x04001B33 RID: 6963
	public GameObject VFXObj;

	// Token: 0x04001B34 RID: 6964
	[Tooltip("This will force the yellow axis to point up for use with ground effects")]
	public bool UseUpAxis;

	// Token: 0x04001B35 RID: 6965
	public bool FollowSpawnLocation;

	// Token: 0x04001B36 RID: 6966
	public float FollowSpeed;

	// Token: 0x04001B37 RID: 6967
	public bool FollowSpawnRotation;

	// Token: 0x04001B38 RID: 6968
	public float RotationSpeed;

	// Token: 0x04001B39 RID: 6969
	public bool ExpireWithFollow;

	// Token: 0x04001B3A RID: 6970
	public bool MultiplyEntityScale;

	// Token: 0x04001B3B RID: 6971
	public ApplyOn ScaleTarget;

	// Token: 0x04001B3C RID: 6972
	public bool CanCancel;

	// Token: 0x04001B3D RID: 6973
	public float DestroyTime = 3f;

	// Token: 0x04001B3E RID: 6974
	public bool RequireIndexMatch;

	// Token: 0x04001B3F RID: 6975
	public bool StopFollowOnCancel;

	// Token: 0x04001B40 RID: 6976
	public bool UpdateScale;
}
