using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class SpawnBeamNode : EffectNode
{
	// Token: 0x06001A93 RID: 6803 RVA: 0x000A53A8 File Offset: 0x000A35A8
	internal override void Apply(EffectProperties properties)
	{
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.StartLoc = effectProperties.OutLoc.Copy();
		if (this.LocOverride != null)
		{
			effectProperties.StartLoc = (this.LocOverride as PoseNode).GetPose(properties);
			effectProperties.OutLoc = effectProperties.StartLoc;
		}
		ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
		Vector3 item = originVectors.Item1;
		Vector3 item2 = originVectors.Item2;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SpawnRef, item, this.SpawnRef.transform.rotation);
		gameObject.transform.forward = item2;
		Beam component = gameObject.GetComponent<Beam>();
		if (component == null)
		{
			Debug.LogError("Spawned Beam " + this.SpawnRef.name + " did not have an Beam Component!");
			return;
		}
		component.Setup(this.FollowSpawnLocation ? effectProperties.Copy(false) : effectProperties);
		component.sourceGUID = this.guid;
		component.Activate(this.BeamProps as BeamNode);
		if (this.FollowSpawnLocation)
		{
			LockFollow.Follow(gameObject, this, effectProperties, this.LocOverride);
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000A54BC File Offset: 0x000A36BC
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
			Beam beam = actionEffect as Beam;
			if (beam != null && actionEffect.sourceGUID == this.guid && actionEffect.sourceInstanceID == props.RandSeed)
			{
				beam.Expire();
			}
		}
		base.TryCancel(props);
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x000A553C File Offset: 0x000A373C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn Beam",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x000A5563 File Offset: 0x000A3763
	public SpawnBeamNode()
	{
	}

	// Token: 0x04001B19 RID: 6937
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001B1A RID: 6938
	public GameObject SpawnRef;

	// Token: 0x04001B1B RID: 6939
	public bool FollowSpawnLocation;

	// Token: 0x04001B1C RID: 6940
	public bool FollowSpawnRotation;

	// Token: 0x04001B1D RID: 6941
	public float RotationSpeed;

	// Token: 0x04001B1E RID: 6942
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(BeamNode), false, "Beam Properties", PortLocation.Default)]
	public Node BeamProps;
}
