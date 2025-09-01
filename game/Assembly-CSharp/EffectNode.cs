using System;

// Token: 0x02000261 RID: 609
public class EffectNode : AbilityNode
{
	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06001873 RID: 6259 RVA: 0x00098F2B File Offset: 0x0009712B
	internal virtual bool CopyProps
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06001874 RID: 6260 RVA: 0x00098F2E File Offset: 0x0009712E
	public virtual bool HasState
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x00098F31 File Offset: 0x00097131
	internal override AbilityState Run(EffectProperties properties)
	{
		this.Invoke(properties);
		return AbilityState.Finished;
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x00098F3B File Offset: 0x0009713B
	public void Invoke(EffectProperties properties = null)
	{
		this.Apply(properties);
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x00098F44 File Offset: 0x00097144
	public virtual void TryCancel(EffectProperties props)
	{
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x00098F46 File Offset: 0x00097146
	internal virtual void Apply(EffectProperties properties)
	{
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x00098F48 File Offset: 0x00097148
	internal virtual bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x00098F6C File Offset: 0x0009716C
	public EffectNode()
	{
	}
}
