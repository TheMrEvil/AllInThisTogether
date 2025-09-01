using System;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000373 RID: 883
[Serializable]
public class LocOffset
{
	// Token: 0x06001D45 RID: 7493 RVA: 0x000B1CDD File Offset: 0x000AFEDD
	public LocOffset()
	{
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x000B1D08 File Offset: 0x000AFF08
	public LocOffset(string json)
	{
		JSONNode jsonnode = JSON.Parse(json.TrimQuotes());
		this.OffsetMode = (DirMode)jsonnode.GetValueOrDefault("m", 0);
		this.OffsetTarg = (ActionLocation)jsonnode.GetValueOrDefault("t", 0);
		this.OffsetType = (LocOffset.DirType)jsonnode.GetValueOrDefault("o", 0);
		this.DistMultiply = jsonnode.GetValueOrDefault("d", 0);
		this.MinDist = jsonnode.GetValueOrDefault("r", 0);
		this.Offset = jsonnode.GetValueOrDefault("f", "").ToString().ToVector3();
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x000B1DF8 File Offset: 0x000AFFF8
	public JSONNode ToJSON()
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("m", (int)this.OffsetMode);
		jsonobject.Add("t", (int)this.OffsetTarg);
		jsonobject.Add("o", (int)this.OffsetType);
		jsonobject.Add("d", this.DistMultiply);
		jsonobject.Add("r", this.MinDist);
		jsonobject.Add("f", this.Offset.ToDetailedString());
		return jsonobject;
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000B1E94 File Offset: 0x000B0094
	private bool NeedsEntity
	{
		get
		{
			DirMode offsetMode = this.OffsetMode;
			return offsetMode == DirMode.AtEntity || offsetMode == DirMode.Velocity || offsetMode == DirMode.EntityForward || offsetMode == DirMode.Velocity_Planar || offsetMode == DirMode.CameraAimPoint || offsetMode == DirMode.PlayerAimPoint;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06001D49 RID: 7497 RVA: 0x000B1ECC File Offset: 0x000B00CC
	private bool CanNormalize
	{
		get
		{
			DirMode offsetMode = this.OffsetMode;
			return offsetMode == DirMode.AtEntity || offsetMode == DirMode.Velocity || offsetMode == DirMode.Velocity_Planar;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06001D4A RID: 7498 RVA: 0x000B1EF5 File Offset: 0x000B00F5
	private bool CanBePlanar
	{
		get
		{
			return this.OffsetMode == DirMode.AtEntity;
		}
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x000B1F00 File Offset: 0x000B0100
	public void ApplyOffset(ref Vector3 origin, EffectProperties props, Transform t = null)
	{
		EntityControl entity = this.GetEntity(this.OffsetEntityRef, props);
		EntityMovement entityMovement = (entity != null) ? entity.movement : null;
		EntityControl sourceControl = props.SourceControl;
		Vector3 vector = this.Offset;
		float y = origin.y;
		float num = 1f;
		if (sourceControl != entity && this.DistMultiply != 0f && entity != null)
		{
			num = Mathf.Sqrt(Vector3.Distance(sourceControl.movement.GetPosition(), entity.movement.GetPosition()));
			if (this.DistMultiply < 0f && num > 0f)
			{
				num = Mathf.Clamp(20f - num, 0f, 20f);
				num *= -this.DistMultiply;
			}
			else
			{
				num *= this.DistMultiply;
			}
		}
		vector *= num;
		switch (this.OffsetMode)
		{
		case DirMode.LocForward:
			if (t == null)
			{
				return;
			}
			origin += t.up * vector.y;
			origin += t.right * vector.x;
			origin += t.forward * vector.z;
			goto IL_6D9;
		case DirMode.EntityForward:
		{
			if (entityMovement == null)
			{
				return;
			}
			Vector3 forward = entityMovement.GetForward();
			origin += MathHelper.GetRelativeDir(vector, forward, Vector3.up);
			goto IL_6D9;
		}
		case DirMode.AtEntity:
		{
			if (entity == null)
			{
				return;
			}
			Vector3 forward = (entity.display.GetLocation(this.OffsetTarg).position - origin).normalized;
			origin += MathHelper.GetRelativeDir(vector, forward, Vector3.up);
			goto IL_6D9;
		}
		case DirMode.Velocity:
		{
			if (entityMovement == null)
			{
				return;
			}
			Vector3 forward = entityMovement.GetVelocity();
			if (this.Normalize)
			{
				forward = forward.normalized;
			}
			if (forward.magnitude == 0f)
			{
				return;
			}
			origin += MathHelper.GetRelativeDir(vector, forward.normalized, Vector3.up) * forward.magnitude;
			goto IL_6D9;
		}
		case DirMode.WorldSpace:
			origin += vector;
			goto IL_6D9;
		case DirMode.ClosestNav:
		{
			Vector3 vector2 = AIManager.NearestNavPoint(origin, -1f);
			if (!vector2.IsValid())
			{
				return;
			}
			if (this.OffsetType == LocOffset.DirType.ModifyPoint)
			{
				origin = vector2;
				return;
			}
			origin = Vector3.MoveTowards(origin, vector2, vector.magnitude);
			return;
		}
		case DirMode.PlayerAimPoint:
		{
			Vector3 target = origin;
			if (sourceControl != null)
			{
				PlayerControl playerControl = sourceControl as PlayerControl;
				if (playerControl != null && playerControl.AimPoint.IsValid())
				{
					target = playerControl.AimPoint;
					goto IL_484;
				}
			}
			if (entity != null)
			{
				PlayerControl playerControl2 = entity as PlayerControl;
				if (playerControl2 != null && playerControl2.CameraAimPoint.IsValid())
				{
					target = playerControl2.CameraAimPoint;
				}
			}
			IL_484:
			if (this.OffsetType == LocOffset.DirType.Planar_XZ)
			{
				target.y = origin.y;
			}
			origin = Vector3.MoveTowards(origin, target, vector.z);
			goto IL_6D9;
		}
		case DirMode.Velocity_Planar:
		{
			if (entityMovement == null)
			{
				return;
			}
			Vector3 forward = Vector3.ProjectOnPlane(entityMovement.GetVelocity(), Vector3.up);
			if (this.Normalize)
			{
				forward = forward.normalized;
			}
			if (forward.magnitude == 0f)
			{
				return;
			}
			origin += MathHelper.GetRelativeDir(vector, forward.normalized, Vector3.up) * forward.magnitude;
			goto IL_6D9;
		}
		case DirMode.PlayerInputDir:
		{
			if (sourceControl != null)
			{
				PlayerControl playerControl3 = sourceControl as PlayerControl;
				if (playerControl3 != null)
				{
					Vector3 forward2 = playerControl3.Input.WorldInputAxis;
					if (forward2.magnitude < 0.01f)
					{
						forward2 = sourceControl.movement.GetForward();
					}
					origin += MathHelper.GetRelativeDir(vector, forward2, Vector3.up);
					goto IL_6D9;
				}
			}
			if (!(entity != null))
			{
				goto IL_6D9;
			}
			PlayerControl playerControl4 = entity as PlayerControl;
			if (playerControl4 != null)
			{
				Vector3 forward3 = playerControl4.Input.WorldInputAxis;
				if (forward3.magnitude < 0.01f)
				{
					forward3 = sourceControl.movement.GetForward();
				}
				origin += MathHelper.GetRelativeDir(vector, forward3, Vector3.up);
				goto IL_6D9;
			}
			goto IL_6D9;
		}
		case DirMode.Random_Spherical:
			origin += props.RandomInsideUnitSphere(this.MinDist).Multiply(vector);
			goto IL_6D9;
		case DirMode.Random_Circular:
		{
			Vector2 vector3 = props.RandomInsideUnitCircle(this.MinDist);
			origin += new Vector3(vector3.x * vector.x, 0f, vector3.y * vector.z);
			goto IL_6D9;
		}
		case DirMode.CameraAimPoint:
		{
			if (sourceControl != null)
			{
				PlayerControl playerControl5 = sourceControl as PlayerControl;
				if (playerControl5 != null)
				{
					origin = Vector3.MoveTowards(origin, playerControl5.CameraAimPoint, vector.z);
					goto IL_6D9;
				}
			}
			if (!(entity != null))
			{
				goto IL_6D9;
			}
			PlayerControl playerControl6 = entity as PlayerControl;
			if (playerControl6 != null)
			{
				origin = Vector3.MoveTowards(origin, playerControl6.CameraAimPoint, vector.z);
				goto IL_6D9;
			}
			goto IL_6D9;
		}
		case DirMode.TowardSpecial:
		{
			Vector3 specialPoint = Location.GetSpecialPoint(this.specialLoc, props);
			if (this.OffsetType == LocOffset.DirType.ModifyPoint)
			{
				origin = specialPoint;
				goto IL_6D9;
			}
			if (this.OffsetType == LocOffset.DirType.TowardPoint)
			{
				origin = Vector3.MoveTowards(origin, specialPoint, vector.z);
				goto IL_6D9;
			}
			if (this.OffsetType == LocOffset.DirType.Planar_XZ)
			{
				specialPoint.y = origin.y;
				origin = Vector3.MoveTowards(origin, specialPoint, vector.z);
				goto IL_6D9;
			}
			goto IL_6D9;
		}
		case DirMode.TowardCached:
		{
			Vector3 cachedLocation = props.GetCachedLocation(this.CacheID);
			if (cachedLocation.IsValid())
			{
				Transform directionTransform = MathHelper.GetDirectionTransform((cachedLocation - origin).normalized, Vector3.up);
				origin += directionTransform.up * vector.y;
				origin += directionTransform.right * vector.x;
				origin += directionTransform.forward * vector.z;
				goto IL_6D9;
			}
			goto IL_6D9;
		}
		}
		throw new ArgumentOutOfRangeException();
		IL_6D9:
		if (this.IsPlanar)
		{
			origin.y = y;
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x000B25F8 File Offset: 0x000B07F8
	private EntityControl GetEntity(CenteredOn centeredOn, EffectProperties props)
	{
		EntityControl result;
		switch (centeredOn)
		{
		case CenteredOn.Source:
			result = props.SourceControl;
			break;
		case CenteredOn.Target:
			result = props.SeekTargetControl;
			break;
		case CenteredOn.Affected:
			result = props.AffectedControl;
			break;
		case CenteredOn.AllyTarget:
			result = props.AllyTargetControl;
			break;
		default:
			result = null;
			break;
		}
		return result;
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x000B2644 File Offset: 0x000B0844
	public LocOffset Copy()
	{
		return base.MemberwiseClone() as LocOffset;
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x000B2654 File Offset: 0x000B0854
	private bool UsesOffsetType()
	{
		DirMode offsetMode = this.OffsetMode;
		if (offsetMode <= DirMode.PlayerAimPoint)
		{
			if (offsetMode == DirMode.ClosestNav)
			{
				return true;
			}
			if (offsetMode == DirMode.PlayerAimPoint)
			{
				return true;
			}
		}
		else
		{
			if (offsetMode == DirMode.CameraAimPoint)
			{
				return true;
			}
			if (offsetMode == DirMode.TowardSpecial)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001DE5 RID: 7653
	public Vector3 Offset = Vector3.zero;

	// Token: 0x04001DE6 RID: 7654
	public DirMode OffsetMode;

	// Token: 0x04001DE7 RID: 7655
	public CenteredOn OffsetEntityRef;

	// Token: 0x04001DE8 RID: 7656
	public LocOffset.DirType OffsetType;

	// Token: 0x04001DE9 RID: 7657
	public ActionLocation OffsetTarg = ActionLocation.CenterOfMass;

	// Token: 0x04001DEA RID: 7658
	public SpecialLocation specialLoc = SpecialLocation.Fountain;

	// Token: 0x04001DEB RID: 7659
	[Range(-1f, 1f)]
	public float DistMultiply;

	// Token: 0x04001DEC RID: 7660
	public bool Normalize = true;

	// Token: 0x04001DED RID: 7661
	public bool IsPlanar;

	// Token: 0x04001DEE RID: 7662
	[Range(0f, 1f)]
	public float MinDist;

	// Token: 0x04001DEF RID: 7663
	public string CacheID;

	// Token: 0x0200067D RID: 1661
	public enum DirType
	{
		// Token: 0x04002BBC RID: 11196
		ModifyPoint,
		// Token: 0x04002BBD RID: 11197
		TowardPoint,
		// Token: 0x04002BBE RID: 11198
		Planar_XZ
	}
}
