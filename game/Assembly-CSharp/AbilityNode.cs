using System;

// Token: 0x0200029B RID: 667
public class AbilityNode : Node
{
	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06001961 RID: 6497 RVA: 0x0009E7B9 File Offset: 0x0009C9B9
	public AbilityRootNode AbilityRoot
	{
		get
		{
			return this.RootNode as AbilityRootNode;
		}
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x0009E7C6 File Offset: 0x0009C9C6
	internal virtual AbilityState Run(EffectProperties props)
	{
		return AbilityState.Finished;
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x0009E7CC File Offset: 0x0009C9CC
	public AbilityState DoUpdate(EffectProperties props)
	{
		if (!this.started)
		{
			this.CurrentState = AbilityState.Running;
			this.started = true;
		}
		if (this.CurrentState == AbilityState.Finished || props.SourceControl == null)
		{
			return this.CurrentState;
		}
		this.CurrentState = this.Run(props);
		return this.CurrentState;
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x0009E820 File Offset: 0x0009CA20
	internal void Completed()
	{
		this.started = false;
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x0009E829 File Offset: 0x0009CA29
	public void Cancel(EffectProperties props)
	{
		this.started = false;
		this.OnCancel(props);
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x0009E839 File Offset: 0x0009CA39
	internal virtual void OnCancel(EffectProperties props)
	{
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x0009E83B File Offset: 0x0009CA3B
	public AbilityNode()
	{
	}

	// Token: 0x04001991 RID: 6545
	[NonSerialized]
	public AbilityState CurrentState;

	// Token: 0x04001992 RID: 6546
	[NonSerialized]
	public bool started;
}
