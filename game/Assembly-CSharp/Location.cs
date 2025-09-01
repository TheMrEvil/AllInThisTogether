using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000023 RID: 35
[Serializable]
public class Location
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000BD4B File Offset: 0x00009F4B
	public static Location INVALID
	{
		get
		{
			return Location.AtWorldPoint(Vector3.zero.INVALID());
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000BD5C File Offset: 0x00009F5C
	public Location()
	{
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0000BD8C File Offset: 0x00009F8C
	public Location(string lstr)
	{
		JSONNode jsonnode = JSON.Parse(lstr.TrimQuotes());
		this.LocType = (LocationType)jsonnode.GetValueOrDefault("t", 0);
		this.AtPoint = (ActionLocation)jsonnode.GetValueOrDefault("p", 0);
		this.EntityRef = (CenteredOn)jsonnode.GetValueOrDefault("e", 0);
		this.SpecialLoc = (SpecialLocation)jsonnode.GetValueOrDefault("s", 0);
		this.WorldPoint = jsonnode.GetValueOrDefault("w", "").ToString().ToVector3();
		this.CacheID = jsonnode.GetValueOrDefault("c", "").ToString().Replace("\"", "");
		if (!jsonnode.HasKey("o"))
		{
			return;
		}
		foreach (KeyValuePair<string, JSONNode> aKeyValue in (jsonnode["o"] as JSONArray))
		{
			JSONObject jsonobject = (JSONObject)aKeyValue;
			this.Offsets.Add(new LocOffset(jsonobject.ToString()));
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000BEEE File Offset: 0x0000A0EE
	public static Location WorldUp()
	{
		return new Location
		{
			LocType = LocationType.Special,
			SpecialLoc = SpecialLocation.WorldUp
		};
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000BF03 File Offset: 0x0000A103
	public static Location AtWorldPoint(Vector3 pos)
	{
		return new Location
		{
			LocType = LocationType.WorldPoint,
			WorldPoint = pos
		};
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000BF18 File Offset: 0x0000A118
	public JSONObject ToJSON()
	{
		JSONObject jsonobject = new JSONObject();
		if (this.LocType != LocationType.Entity)
		{
			jsonobject.Add("t", (int)this.LocType);
		}
		if (this.AtPoint != ActionLocation._)
		{
			jsonobject.Add("p", (int)this.AtPoint);
		}
		if (this.EntityRef != CenteredOn.Source)
		{
			jsonobject.Add("e", (int)this.EntityRef);
		}
		if (this.SpecialLoc != SpecialLocation.PlayerAimPt)
		{
			jsonobject.Add("s", (int)this.SpecialLoc);
		}
		if (this.WorldPoint.sqrMagnitude > 0f)
		{
			jsonobject.Add("w", this.WorldPoint.ToDetailedString());
		}
		if (!string.IsNullOrEmpty(this.CacheID))
		{
			jsonobject.Add("c", this.CacheID);
		}
		if (this.Offsets.Count == 0)
		{
			return jsonobject;
		}
		JSONArray jsonarray = new JSONArray();
		foreach (LocOffset locOffset in this.Offsets)
		{
			jsonarray.Add(locOffset.ToJSON());
		}
		jsonobject.Add("o", jsonarray);
		return jsonobject;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000C060 File Offset: 0x0000A260
	public override string ToString()
	{
		return this.ToJSON().ToString();
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000C070 File Offset: 0x0000A270
	public Location Copy()
	{
		Location location = base.MemberwiseClone() as Location;
		if (this.Offsets.Count == 0)
		{
			location.Offsets = new List<LocOffset>();
		}
		else
		{
			location.Offsets = this.Offsets;
		}
		return location;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
	public Vector3 GetPosition(EffectProperties props)
	{
		switch (this.LocType)
		{
		case LocationType.WorldPoint:
		{
			Vector3 worldPoint = this.WorldPoint;
			if (this.Offsets.Count > 0)
			{
				this.ApplyOffset(ref worldPoint, props, null);
			}
			return worldPoint;
		}
		case LocationType.Special:
		{
			if (this.SpecialLoc == SpecialLocation.OriginForward && props.StartLoc.GetLookAt() == this)
			{
				Debug.LogError("Infinite Input Direction Loop");
				return Vector3.one.INVALID();
			}
			if (this.SpecialLoc == SpecialLocation.OutputForward && props.OutLoc.GetLookAt() == this)
			{
				Debug.LogError("Infinite Output Direction Loop");
				return Vector3.one.INVALID();
			}
			Vector3 specialPoint = Location.GetSpecialPoint(this.SpecialLoc, props);
			this.ApplyOffset(ref specialPoint, props, null);
			return specialPoint;
		}
		case LocationType.Maintain_Input:
		{
			if (props.StartLoc.GetLocation() == this)
			{
				Debug.LogError("Infinite Input Location Loop - " + ((props != null) ? props.CauseName : null));
				return Vector3.one.INVALID();
			}
			Vector3 origin = props.GetOrigin();
			this.ApplyOffset(ref origin, props, null);
			return origin;
		}
		case LocationType.Maintain_Output:
		{
			if (props.OutLoc.GetLocation() == this)
			{
				Debug.LogError("Infinite Output Location Loop");
				return Vector3.one.INVALID();
			}
			Vector3 outputPoint = props.GetOutputPoint();
			this.ApplyOffset(ref outputPoint, props, null);
			return outputPoint;
		}
		case LocationType.EffectCache:
		{
			Vector3 cachedLocation = props.GetCachedLocation(this.CacheID);
			this.ApplyOffset(ref cachedLocation, props, null);
			return cachedLocation;
		}
		default:
		{
			Transform transform = this.GetTransform(props);
			if (!(transform == null))
			{
				return this.GetPosition(transform, props);
			}
			return Vector3.one.INVALID();
		}
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000C23C File Offset: 0x0000A43C
	public Vector3 GetPosition(Transform t, EffectProperties props)
	{
		if (t == null)
		{
			return this.GetPosition(props);
		}
		if (this.LocType == LocationType.WorldPoint && this.WorldPoint.magnitude > 0f)
		{
			return this.WorldPoint;
		}
		Vector3 result = this.RootPosition(t);
		this.ApplyOffset(ref result, props, t);
		return result;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000C290 File Offset: 0x0000A490
	private void ApplyOffset(ref Vector3 origin, EffectProperties props, Transform t = null)
	{
		foreach (LocOffset locOffset in this.Offsets)
		{
			locOffset.ApplyOffset(ref origin, props, t);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
	public static Vector3 GetSpecialPoint(SpecialLocation loc, EffectProperties props)
	{
		switch (loc)
		{
		case SpecialLocation.PlayerAimPt:
			return Location.GetPlayerPoint(props.SourceControl, loc);
		case SpecialLocation.Fountain:
		{
			Fountain instance = Fountain.instance;
			return (instance != null) ? instance.transform.position : Vector3.zero;
		}
		case SpecialLocation.MapOrigin:
		{
			Scene_Settings instance2 = Scene_Settings.instance;
			return (instance2 != null) ? instance2.MapOrigin : Vector3.zero;
		}
		case SpecialLocation.MapObjective:
			return SpawnPoint.GetObjectiveLocation(props);
		case SpecialLocation.WorldUp:
			return new Vector3(0f, 2.1474836E+09f, 0f);
		case SpecialLocation.CameraAimPoint:
			return Location.GetPlayerPoint(props.SourceControl, loc);
		case SpecialLocation.AIMoveTarget:
		{
			Vector3 result;
			if (props.SourceControl != null)
			{
				AIControl aicontrol = props.SourceControl as AIControl;
				if (aicontrol != null)
				{
					result = aicontrol.Movement.GetTargetPoint();
					goto IL_148;
				}
			}
			result = Vector3.zero;
			IL_148:
			return result;
		}
		case SpecialLocation.AISpawnPoint:
		{
			SpawnPoint randomSpawnPoint = SpawnPoint.GetRandomSpawnPoint(SpawnType.AI_Ground, props);
			return (randomSpawnPoint != null) ? randomSpawnPoint.point : Vector3.zero;
		}
		case SpecialLocation.OriginForward:
			return props.StartLoc.GetLookAtPoint(props);
		case SpecialLocation.OutputForward:
			return props.OutLoc.GetLookAtPoint(props);
		case SpecialLocation.AimPoint_Smooth:
			return Location.GetPlayerPoint(props.SourceControl, loc);
		case SpecialLocation.LocalPlayerPos:
		{
			PlayerControl myInstance = PlayerControl.myInstance;
			return (myInstance != null) ? myInstance.display.CenterOfMass.position : Vector3.zero;
		}
		case SpecialLocation.RandomExplorePt:
		{
			SpawnPoint randomSpawnPoint2 = SpawnPoint.GetRandomSpawnPoint(SpawnType.Map_Explore, props);
			return (randomSpawnPoint2 != null) ? randomSpawnPoint2.point : Vector3.zero;
		}
		}
		return Vector3.zero;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000C47C File Offset: 0x0000A67C
	private static Vector3 GetPlayerPoint(EntityControl source, SpecialLocation loc)
	{
		if (source == null)
		{
			return Vector3.zero;
		}
		PlayerControl playerControl = source as PlayerControl;
		if (playerControl != null)
		{
			if (loc == SpecialLocation.PlayerAimPt)
			{
				return playerControl.AimPoint;
			}
			if (loc == SpecialLocation.CameraAimPoint)
			{
				return playerControl.CameraAimPoint;
			}
			if (loc == SpecialLocation.AimPoint_Smooth)
			{
				return playerControl.AimPointSmooth;
			}
		}
		AIControl aicontrol = source as AIControl;
		if (aicontrol != null && aicontrol.PetOwnerID >= 0)
		{
			EntityControl entity = EntityControl.GetEntity(aicontrol.PetOwnerID);
			if (entity != null)
			{
				PlayerControl playerControl2 = entity as PlayerControl;
				if (playerControl2 != null)
				{
					if (loc == SpecialLocation.PlayerAimPt)
					{
						return playerControl2.AimPoint;
					}
					if (loc == SpecialLocation.CameraAimPoint)
					{
						return playerControl2.CameraAimPoint;
					}
					if (loc == SpecialLocation.AimPoint_Smooth)
					{
						return playerControl2.AimPointSmooth;
					}
				}
			}
		}
		Debug.Log("Failed to find plr for aim offset");
		return Vector3.zero;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000C525 File Offset: 0x0000A725
	private Vector3 RootPosition(Transform t)
	{
		if (!(t == null))
		{
			return t.position;
		}
		return Vector3.zero;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000C53C File Offset: 0x0000A73C
	public Transform GetTransform(EffectProperties props)
	{
		if (this.LocType == LocationType.WorldPoint)
		{
			return null;
		}
		if (this.LocType == LocationType.Transform)
		{
			if (this.transformOverride != null)
			{
				return this.transformOverride;
			}
			if (props.SourceLocation != null)
			{
				return props.SourceLocation;
			}
		}
		if (this.AtPoint == ActionLocation._)
		{
			return null;
		}
		EntityControl entity = this.GetEntity(this.EntityRef, props);
		if (entity == null)
		{
			return null;
		}
		return entity.display.GetLocation(this.AtPoint);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000C5BB File Offset: 0x0000A7BB
	public bool NeedsTarget()
	{
		return this.LocType == LocationType.Entity && this.EntityRef == CenteredOn.Target;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
	private bool HasOffset()
	{
		return this.Offsets.Count > 0;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
	private EntityControl GetEntity(CenteredOn centeredOn, EffectProperties props)
	{
		switch (centeredOn)
		{
		case CenteredOn.Source:
			return props.SourceControl;
		case CenteredOn.Target:
			return props.SeekTargetControl;
		case CenteredOn.Affected:
			return props.AffectedControl;
		case CenteredOn.AllyTarget:
			return props.AllyTargetControl;
		default:
			return null;
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000C618 File Offset: 0x0000A818
	public static ApplyOn GetApplyOn(CenteredOn c)
	{
		ApplyOn result;
		switch (c)
		{
		case CenteredOn.Source:
			result = ApplyOn.Source;
			break;
		case CenteredOn.Target:
			result = ApplyOn.SeekTarget;
			break;
		case CenteredOn.Affected:
			result = ApplyOn.Affected;
			break;
		case CenteredOn.AllyTarget:
			result = ApplyOn.AllyTarget;
			break;
		default:
			result = ApplyOn.Source;
			break;
		}
		return result;
	}

	// Token: 0x04000111 RID: 273
	public LocationType LocType;

	// Token: 0x04000112 RID: 274
	public CenteredOn EntityRef;

	// Token: 0x04000113 RID: 275
	public ActionLocation AtPoint;

	// Token: 0x04000114 RID: 276
	public SpecialLocation SpecialLoc = SpecialLocation.Fountain;

	// Token: 0x04000115 RID: 277
	public Vector3 WorldPoint = Vector3.zero;

	// Token: 0x04000116 RID: 278
	public string CacheID = "";

	// Token: 0x04000117 RID: 279
	[HideInInspector]
	public List<LocOffset> Offsets = new List<LocOffset>();

	// Token: 0x04000118 RID: 280
	[NonSerialized]
	public Transform transformOverride;
}
