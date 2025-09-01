using System;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000024 RID: 36
[Serializable]
public class Pose
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000105 RID: 261 RVA: 0x0000C650 File Offset: 0x0000A850
	public static global::Pose INVALID
	{
		get
		{
			return global::Pose.WorldPoint(Vector3.zero.INVALID(), Vector3.up);
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000C666 File Offset: 0x0000A866
	public Pose()
	{
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000C66E File Offset: 0x0000A86E
	public Pose(Transform t)
	{
		this.fastPathWorld = true;
		this.pt = t.position;
		this.look = this.pt + t.forward;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
	public Pose(Location l, Location f)
	{
		this.location = l;
		this.lookAt = f;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
	public Pose(string lstr)
	{
		JSONNode jsonnode = JSON.Parse(lstr.TrimQuotes());
		if (jsonnode.HasKey("x"))
		{
			this.pt = jsonnode.GetValueOrDefault("x", "").ToString().ToVector3();
			this.look = jsonnode.GetValueOrDefault("y", "").ToString().ToVector3();
			return;
		}
		this.location = new Location(jsonnode["l"].ToString());
		this.lookAt = new Location(jsonnode["f"].ToString());
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000C768 File Offset: 0x0000A968
	public JSONObject ToJSON()
	{
		JSONObject jsonobject = new JSONObject();
		if (this.fastPathWorld)
		{
			jsonobject.Add("x", this.pt.ToDetailedString());
			jsonobject.Add("y", this.look.ToDetailedString());
			return jsonobject;
		}
		jsonobject.Add("l", this.GetLocation().ToJSON());
		jsonobject.Add("f", this.GetLookAt().ToJSON());
		return jsonobject;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
	public Location GetLocation()
	{
		if (this.location == null)
		{
			this.location = (this.fastPathWorld ? Location.AtWorldPoint(this.pt) : new Location());
		}
		return this.location;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000C818 File Offset: 0x0000AA18
	public Location GetLookAt()
	{
		if (this.lookAt == null)
		{
			this.lookAt = (this.fastPathWorld ? Location.AtWorldPoint(this.look) : new Location());
		}
		return this.lookAt;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000C848 File Offset: 0x0000AA48
	public void OverrideLocation(Location loc)
	{
		this.location = loc;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000C851 File Offset: 0x0000AA51
	public void OverrideLookAt(Location loc)
	{
		this.lookAt = loc;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000C85A File Offset: 0x0000AA5A
	public Vector3 GetPosition(EffectProperties props)
	{
		if (this.fastPathWorld)
		{
			return this.pt;
		}
		return this.GetLocation().GetPosition(props);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000C878 File Offset: 0x0000AA78
	public Vector3 GetLookDirection(EffectProperties props)
	{
		if (this.fastPathWorld)
		{
			return this.look - this.pt;
		}
		return (this.GetLookAt().GetPosition(props) - this.GetLocation().GetPosition(props)).normalized;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
	[return: TupleElementNames(new string[]
	{
		"position",
		"lookAt"
	})]
	public ValueTuple<Vector3, Vector3> GetData(EffectProperties props)
	{
		if (this.fastPathWorld)
		{
			return new ValueTuple<Vector3, Vector3>(this.pt, this.look);
		}
		Vector3 position = this.GetPosition(props);
		Vector3 vector = Vector3.up;
		if (position.IsValid())
		{
			vector = this.GetLookAt().GetPosition(props);
			vector = (vector - position).normalized;
		}
		return new ValueTuple<Vector3, Vector3>(position, vector);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000C925 File Offset: 0x0000AB25
	public Vector3 GetLookAtPoint(EffectProperties props)
	{
		if (this.fastPathWorld)
		{
			return this.look;
		}
		return this.GetLookAt().GetPosition(props);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000C942 File Offset: 0x0000AB42
	public override string ToString()
	{
		return this.ToJSON().ToString();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000C94F File Offset: 0x0000AB4F
	public static global::Pose FromEntity(EntityControl c)
	{
		if (c == null)
		{
			return global::Pose.INVALID;
		}
		return global::Pose.WorldPoint(c.display.CenterOfMass.position, c.display.CenterOfMass.forward);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000C985 File Offset: 0x0000AB85
	public Transform GetTransform(EffectProperties props)
	{
		Location location = this.location;
		if (location == null)
		{
			return null;
		}
		return location.GetTransform(props);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000C999 File Offset: 0x0000AB99
	public static global::Pose WorldPoint(Vector3 point, Vector3 facing)
	{
		return new global::Pose
		{
			fastPathWorld = true,
			pt = point,
			look = point + facing
		};
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000C9BB File Offset: 0x0000ABBB
	public void UpdateWorldPoint(Vector3 point, Vector3 facing)
	{
		this.fastPathWorld = true;
		this.pt = point;
		this.look = point + facing;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
	public global::Pose Copy()
	{
		if (this.fastPathWorld)
		{
			return global::Pose.WorldPoint(this.pt, this.look);
		}
		if (this.location == null || this.lookAt == null)
		{
			return new global::Pose();
		}
		return new global::Pose(this.location.Copy(), this.lookAt.Copy());
	}

	// Token: 0x04000119 RID: 281
	private Location location;

	// Token: 0x0400011A RID: 282
	private Location lookAt;

	// Token: 0x0400011B RID: 283
	private bool fastPathWorld;

	// Token: 0x0400011C RID: 284
	private Vector3 pt;

	// Token: 0x0400011D RID: 285
	private Vector3 look;
}
