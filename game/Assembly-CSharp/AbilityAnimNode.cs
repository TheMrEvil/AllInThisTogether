using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000293 RID: 659
public class AbilityAnimNode : AbilityNode
{
	// Token: 0x06001938 RID: 6456 RVA: 0x0009D378 File Offset: 0x0009B578
	internal override AbilityState Run(EffectProperties props)
	{
		EntityDisplay display = props.SourceControl.display;
		if (this.startedAnim)
		{
			if (display.IsPlayingAbilityAnim(this.AnimName))
			{
				if (!(display is PlayerDisplay))
				{
					display.AbilityAnimNormalizedTime();
				}
				else
				{
					(display as PlayerDisplay).AbilityAnimNormalizedTime(props.AbilityType);
				}
				AbilityState abilityState = AbilityState.Finished;
				using (List<Node>.Enumerator enumerator = this.OnStart.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
						{
							abilityState = AbilityState.Running;
						}
					}
				}
				if (!this.StopOnFinishedEvents || abilityState == AbilityState.Running)
				{
					return AbilityState.Running;
				}
				if (display is PlayerDisplay)
				{
					(display as PlayerDisplay).StopCurrentAbilityAnim(props.AbilityType);
				}
				else
				{
					display.StopCurrentAbilityAnim();
				}
			}
			AbilityState result = AbilityState.Finished;
			using (List<Node>.Enumerator enumerator = this.OnFinish.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
					{
						result = AbilityState.Running;
					}
				}
			}
			using (List<Node>.Enumerator enumerator = this.OnStart.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
					{
						result = AbilityState.Running;
					}
				}
			}
			return result;
		}
		if (display.IsPlayingAbilityAnim(this.AnimName) && !this.Force)
		{
			this.startedAnim = true;
			return AbilityState.Running;
		}
		PlayerDisplay playerDisplay = display as PlayerDisplay;
		if (playerDisplay != null)
		{
			playerDisplay.PlayAbilityAnim(this.AnimName, this.crossfadeTime, props.AbilityType, this.StopBonePhysics);
		}
		else
		{
			display.PlayAbilityAnim(this.AnimName, this.crossfadeTime, this.StopBonePhysics);
		}
		this.startedAnim = true;
		if (this.OneShot)
		{
			return AbilityState.Finished;
		}
		return AbilityState.Running;
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x0009D558 File Offset: 0x0009B758
	internal override void OnCancel(EffectProperties props)
	{
		this.startedAnim = false;
		EntityControl sourceControl = props.SourceControl;
		if (sourceControl.display.IsPlayingAbilityAnim(this.AnimName) && this.StopOnCancel)
		{
			if (sourceControl.display is PlayerDisplay)
			{
				(sourceControl.display as PlayerDisplay).StopCurrentAbilityAnim(props.AbilityType);
			}
			else
			{
				sourceControl.display.StopCurrentAbilityAnim();
			}
		}
		foreach (Node node in this.OnFinish)
		{
			((AbilityNode)node).Cancel(props);
		}
		foreach (Node node2 in this.OnStart)
		{
			((AbilityNode)node2).Cancel(props);
		}
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x0009D64C File Offset: 0x0009B84C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Play Animation",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x0009D67A File Offset: 0x0009B87A
	public AbilityAnimNode()
	{
	}

	// Token: 0x04001959 RID: 6489
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Start", PortLocation.Default)]
	public List<Node> OnStart = new List<Node>();

	// Token: 0x0400195A RID: 6490
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Finished", PortLocation.Default)]
	public List<Node> OnFinish = new List<Node>();

	// Token: 0x0400195B RID: 6491
	public string AnimName;

	// Token: 0x0400195C RID: 6492
	public float crossfadeTime = 0.25f;

	// Token: 0x0400195D RID: 6493
	public bool StopOnFinishedEvents;

	// Token: 0x0400195E RID: 6494
	public bool StopOnCancel = true;

	// Token: 0x0400195F RID: 6495
	public bool Force;

	// Token: 0x04001960 RID: 6496
	public bool OneShot;

	// Token: 0x04001961 RID: 6497
	public bool StopBonePhysics;

	// Token: 0x04001962 RID: 6498
	private bool startedAnim;
}
