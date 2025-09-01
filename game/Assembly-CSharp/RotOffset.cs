using System;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200038D RID: 909
[Serializable]
public class RotOffset
{
	// Token: 0x06001DAB RID: 7595 RVA: 0x000B4185 File Offset: 0x000B2385
	public RotOffset()
	{
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x000B4190 File Offset: 0x000B2390
	public RotOffset(string json)
	{
		JSONNode jsonnode = JSON.Parse(json.TrimQuotes());
		this.Offset = jsonnode.GetValueOrDefault("o", "").ToString().ToVector3();
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x000B41D4 File Offset: 0x000B23D4
	public JSONNode ToJSON()
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("o", this.Offset.ToDetailedString());
		return jsonobject;
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x000B41F6 File Offset: 0x000B23F6
	public void ApplyOffset(ref Vector3 dir, EffectProperties props, Location l)
	{
		dir = Quaternion.Euler(this.Offset.x, this.Offset.y, this.Offset.z) * dir.normalized;
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x000B422F File Offset: 0x000B242F
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

	// Token: 0x06001DB0 RID: 7600 RVA: 0x000B4266 File Offset: 0x000B2466
	public RotOffset Copy()
	{
		return base.MemberwiseClone() as RotOffset;
	}

	// Token: 0x04001E56 RID: 7766
	public Vector3 Offset;
}
