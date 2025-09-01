using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.Basics
{
	// Token: 0x02000047 RID: 71
	public class FAnimationClips : Dictionary<string, int>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000FE20 File Offset: 0x0000E020
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000FE28 File Offset: 0x0000E028
		public string CurrentAnimation
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentAnimation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentAnimation>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000FE31 File Offset: 0x0000E031
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000FE39 File Offset: 0x0000E039
		public string PreviousAnimation
		{
			[CompilerGenerated]
			get
			{
				return this.<PreviousAnimation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PreviousAnimation>k__BackingField = value;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000FE42 File Offset: 0x0000E042
		public FAnimationClips(Animator animator)
		{
			this.Animator = animator;
			this.CurrentAnimation = "";
			this.PreviousAnimation = "";
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000FE67 File Offset: 0x0000E067
		public void Add(string clipName, bool exactClipName = false)
		{
			this.AddClip(clipName, exactClipName);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000FE71 File Offset: 0x0000E071
		public void AddClip(string clipName, bool exactClipName = false)
		{
			this.AddClip(this.Animator, clipName, exactClipName);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000FE84 File Offset: 0x0000E084
		public void AddClip(Animator animator, string clipName, bool exactClipName = false)
		{
			if (!animator)
			{
				Debug.LogError("No animator!");
				return;
			}
			string text = "";
			if (!exactClipName)
			{
				if (animator.StateExists(clipName, this.Layer))
				{
					text = clipName;
				}
				else if (animator.StateExists(clipName.CapitalizeFirstLetter(), 0))
				{
					text = clipName.CapitalizeFirstLetter();
				}
				else if (animator.StateExists(clipName.ToLower(), this.Layer))
				{
					text = clipName.ToLower();
				}
				else if (animator.StateExists(clipName.ToUpper(), this.Layer))
				{
					text = clipName.ToUpper();
				}
			}
			else if (animator.StateExists(clipName, this.Layer))
			{
				text = clipName;
			}
			if (text == "")
			{
				Debug.LogWarning("Clip with name " + clipName + " not exists in animator from game object " + animator.gameObject.name);
				return;
			}
			if (!base.ContainsKey(clipName))
			{
				base.Add(clipName, Animator.StringToHash(text));
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000FF68 File Offset: 0x0000E168
		public void CrossFadeInFixedTime(string clip, float transitionTime = 0.25f, float timeOffset = 0f, bool startOver = false)
		{
			if (base.ContainsKey(clip))
			{
				this.RefreshClipMemory(clip);
				if (startOver)
				{
					this.Animator.CrossFadeInFixedTime(base[clip], transitionTime, this.Layer, timeOffset);
					return;
				}
				if (!this.IsPlaying(clip))
				{
					this.Animator.CrossFadeInFixedTime(base[clip], transitionTime, this.Layer, timeOffset);
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public void CrossFade(string clip, float transitionTime = 0.25f, float timeOffset = 0f, bool startOver = false)
		{
			if (base.ContainsKey(clip))
			{
				this.RefreshClipMemory(clip);
				if (startOver)
				{
					this.Animator.CrossFade(base[clip], transitionTime, this.Layer, timeOffset);
					return;
				}
				if (!this.IsPlaying(clip))
				{
					this.Animator.CrossFade(base[clip], transitionTime, this.Layer, timeOffset);
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010027 File Offset: 0x0000E227
		private void RefreshClipMemory(string name)
		{
			if (name != this.CurrentAnimation)
			{
				this.PreviousAnimation = this.CurrentAnimation;
				this.CurrentAnimation = name;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001004C File Offset: 0x0000E24C
		public void SetFloat(string parameter, float value = 0f, float deltaSpeed = 60f)
		{
			float num = this.Animator.GetFloat(parameter);
			if (deltaSpeed >= 60f)
			{
				num = value;
			}
			else
			{
				num = FLogicMethods.FLerp(num, value, Time.deltaTime * deltaSpeed, 0.01f);
			}
			this.Animator.SetFloat(parameter, num);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010094 File Offset: 0x0000E294
		public void SetFloatUnscaledDelta(string parameter, float value = 0f, float deltaSpeed = 60f)
		{
			float num = this.Animator.GetFloat(parameter);
			if (deltaSpeed >= 60f)
			{
				num = value;
			}
			else
			{
				num = FLogicMethods.FLerp(num, value, Time.unscaledDeltaTime * deltaSpeed, 0.01f);
			}
			this.Animator.SetFloat(parameter, num);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000100DC File Offset: 0x0000E2DC
		internal bool IsPlaying(string clip)
		{
			if (this.Animator.IsInTransition(this.Layer))
			{
				if (this.Animator.GetNextAnimatorStateInfo(this.Layer).shortNameHash == base[clip])
				{
					return true;
				}
			}
			else if (this.Animator.GetCurrentAnimatorStateInfo(this.Layer).shortNameHash == base[clip])
			{
				return true;
			}
			return false;
		}

		// Token: 0x040001FA RID: 506
		public readonly Animator Animator;

		// Token: 0x040001FB RID: 507
		[CompilerGenerated]
		private string <CurrentAnimation>k__BackingField;

		// Token: 0x040001FC RID: 508
		[CompilerGenerated]
		private string <PreviousAnimation>k__BackingField;

		// Token: 0x040001FD RID: 509
		public int Layer;
	}
}
