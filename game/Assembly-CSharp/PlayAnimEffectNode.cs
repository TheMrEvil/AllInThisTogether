using System;

// Token: 0x020002D2 RID: 722
public class PlayAnimEffectNode : EffectNode
{
	// Token: 0x06001A61 RID: 6753 RVA: 0x000A422E File Offset: 0x000A242E
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Play Animation"
		};
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000A4240 File Offset: 0x000A2440
	internal override void Apply(EffectProperties properties)
	{
		EntityDisplay display = properties.GetApplicationEntity(this.ApplyTo).display;
		if (!display.IsPlayingAbilityAnim(this.AnimName) || this.ForcePlay)
		{
			PlayerDisplay playerDisplay = display as PlayerDisplay;
			if (playerDisplay != null)
			{
				playerDisplay.PlayAbilityAnim(this.AnimName, this.crossfadeTime, properties.AbilityType, false);
				return;
			}
			display.PlayAbilityAnim(this.AnimName, this.crossfadeTime, false);
		}
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000A42AC File Offset: 0x000A24AC
	public PlayAnimEffectNode()
	{
	}

	// Token: 0x04001AD5 RID: 6869
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001AD6 RID: 6870
	public string AnimName;

	// Token: 0x04001AD7 RID: 6871
	public float crossfadeTime = 0.25f;

	// Token: 0x04001AD8 RID: 6872
	public bool ForcePlay;
}
