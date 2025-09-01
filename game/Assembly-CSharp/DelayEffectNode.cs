using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MEC;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class DelayEffectNode : EffectNode
{
	// Token: 0x06001A2C RID: 6700 RVA: 0x000A2E6C File Offset: 0x000A106C
	internal override void Apply(EffectProperties properties)
	{
		if (this.CanCancel)
		{
			this.TryCancel(properties);
		}
		float num = this.Delay;
		if (this.Value != null)
		{
			num = (this.Value as NumberNode).Evaluate(properties);
		}
		bool flag = this.Effects.Count > 1;
		if (num <= 0f)
		{
			using (List<Node>.Enumerator enumerator = this.Effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Node node = enumerator.Current;
					((EffectNode)node).Invoke(flag ? properties.Copy(false) : properties);
				}
				return;
			}
		}
		CoroutineHandle handle = Timing.RunCoroutine(this.ApplyDelayed(flag ? properties.Copy(false) : properties, num));
		if (this.CanCancel)
		{
			this.AddRoutineToList(properties, handle);
		}
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x000A2F48 File Offset: 0x000A1148
	private void AddRoutineToList(EffectProperties props, CoroutineHandle handle)
	{
		EntityControl sourceControl = props.SourceControl;
		DelayEffectNode.DelayInstance item = new DelayEffectNode.DelayInstance((sourceControl != null) ? sourceControl.ViewID : 0, this.guid, handle);
		DelayEffectNode.RunningDelays.Add(item);
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x000A2F7F File Offset: 0x000A117F
	public override void TryCancel(EffectProperties props)
	{
		this.OnCancel(props);
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x000A2F88 File Offset: 0x000A1188
	internal override void OnCancel(EffectProperties props)
	{
		for (int i = DelayEffectNode.RunningDelays.Count - 1; i >= 0; i--)
		{
			int num = -1;
			if (props.SourceControl != null)
			{
				num = props.SourceControl.ViewID;
			}
			if (DelayEffectNode.RunningDelays[i].SourceID == num && !(DelayEffectNode.RunningDelays[i].GraphID != this.guid))
			{
				DelayEffectNode.RunningDelays[i].Cancel();
				DelayEffectNode.RunningDelays.RemoveAt(i);
			}
		}
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x000A3013 File Offset: 0x000A1213
	public static void CleanDelayList()
	{
		DelayEffectNode.RunningDelays = new List<DelayEffectNode.DelayInstance>();
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x000A301F File Offset: 0x000A121F
	private IEnumerator<float> ApplyDelayed(EffectProperties props, float delay)
	{
		float t = 0f;
		while (t < delay)
		{
			t += GameplayManager.deltaTime;
			yield return 0f;
		}
		using (List<Node>.Enumerator enumerator = this.Effects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Node node = enumerator.Current;
				((EffectNode)node).Invoke(props.Copy(false));
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x000A303C File Offset: 0x000A123C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Delay Effect",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x000A3063 File Offset: 0x000A1263
	public DelayEffectNode()
	{
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000A3076 File Offset: 0x000A1276
	// Note: this type is marked as 'beforefieldinit'.
	static DelayEffectNode()
	{
	}

	// Token: 0x04001A9E RID: 6814
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Dynamic Delay", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001A9F RID: 6815
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Effects", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001AA0 RID: 6816
	public float Delay;

	// Token: 0x04001AA1 RID: 6817
	public bool CanCancel;

	// Token: 0x04001AA2 RID: 6818
	private static List<DelayEffectNode.DelayInstance> RunningDelays = new List<DelayEffectNode.DelayInstance>();

	// Token: 0x02000649 RID: 1609
	private class DelayInstance
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x000D6CE2 File Offset: 0x000D4EE2
		public DelayInstance(int src, string graph, CoroutineHandle handle)
		{
			this.SourceID = src;
			this.GraphID = graph;
			this.StartTime = Time.realtimeSinceStartup;
			this.Handle = handle;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000D6D0A File Offset: 0x000D4F0A
		public void Cancel()
		{
			if (this.Handle.IsValid && this.Handle.IsRunning)
			{
				Timing.KillCoroutines(this.Handle);
			}
		}

		// Token: 0x04002AD0 RID: 10960
		public int SourceID;

		// Token: 0x04002AD1 RID: 10961
		public string GraphID;

		// Token: 0x04002AD2 RID: 10962
		public float StartTime;

		// Token: 0x04002AD3 RID: 10963
		public float Delay;

		// Token: 0x04002AD4 RID: 10964
		private CoroutineHandle Handle;
	}

	// Token: 0x0200064A RID: 1610
	[CompilerGenerated]
	private sealed class <ApplyDelayed>d__10 : IEnumerator<float>, IEnumerator, IDisposable
	{
		// Token: 0x060027B4 RID: 10164 RVA: 0x000D6D32 File Offset: 0x000D4F32
		[DebuggerHidden]
		public <ApplyDelayed>d__10(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000D6D41 File Offset: 0x000D4F41
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000D6D44 File Offset: 0x000D4F44
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			DelayEffectNode delayEffectNode = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
			}
			if (t >= delay)
			{
				foreach (Node node in delayEffectNode.Effects)
				{
					((EffectNode)node).Invoke(props.Copy(false));
				}
				return false;
			}
			t += GameplayManager.deltaTime;
			this.<>2__current = 0f;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000D6E0C File Offset: 0x000D500C
		float IEnumerator<float>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000D6E14 File Offset: 0x000D5014
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000D6E1B File Offset: 0x000D501B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002AD5 RID: 10965
		private int <>1__state;

		// Token: 0x04002AD6 RID: 10966
		private float <>2__current;

		// Token: 0x04002AD7 RID: 10967
		public float delay;

		// Token: 0x04002AD8 RID: 10968
		public DelayEffectNode <>4__this;

		// Token: 0x04002AD9 RID: 10969
		public EffectProperties props;

		// Token: 0x04002ADA RID: 10970
		private float <t>5__2;
	}
}
