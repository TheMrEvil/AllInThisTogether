using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class GenreGoal_Zones : GenreGoalNode
{
	// Token: 0x06001CA5 RID: 7333 RVA: 0x000AE614 File Offset: 0x000AC814
	public override void Setup()
	{
		base.Setup();
		for (int i = 0; i < this.NumZones; i++)
		{
			ZoneManager.CreateNewZone(this.ZoneProps);
		}
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x000AE644 File Offset: 0x000AC844
	public override void TickUpdate()
	{
		if (ZoneManager.Zones.Count == 0)
		{
			return;
		}
		this.Progress = 0f;
		foreach (ZoneArea zoneArea in ZoneManager.Zones)
		{
			this.Progress += zoneArea.Progress;
		}
		this.Progress /= (float)ZoneManager.Zones.Count;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x000AE6D4 File Offset: 0x000AC8D4
	public override bool IsFinished()
	{
		bool flag = false;
		using (List<ZoneArea>.Enumerator enumerator = ZoneManager.Zones.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsCaptured)
				{
					flag = true;
				}
			}
		}
		return !flag;
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x000AE730 File Offset: 0x000AC930
	public override string GetGoalInfo()
	{
		return "- Hold the Zones -";
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x000AE737 File Offset: 0x000AC937
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Zones",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x000AE75E File Offset: 0x000AC95E
	public GenreGoal_Zones()
	{
	}

	// Token: 0x04001D61 RID: 7521
	public int NumZones = 1;

	// Token: 0x04001D62 RID: 7522
	public ZoneProperties ZoneProps;
}
