using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.Basics
{
	// Token: 0x02000048 RID: 72
	public class FAnimator
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00010145 File Offset: 0x0000E345
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0001014D File Offset: 0x0000E34D
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00010156 File Offset: 0x0000E356
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0001015E File Offset: 0x0000E35E
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00010167 File Offset: 0x0000E367
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0001016F File Offset: 0x0000E36F
		public int Layer
		{
			[CompilerGenerated]
			get
			{
				return this.<Layer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Layer>k__BackingField = value;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00010178 File Offset: 0x0000E378
		public FAnimator(Animator animator, int layer = 0)
		{
			this.Animator = animator;
			this.CurrentAnimation = "";
			this.PreviousAnimation = "";
			this.Layer = layer;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public bool ContainsClip(string clipName, bool exactClipName = false)
		{
			if (!this.Animator)
			{
				Debug.LogError("No animator!");
				return false;
			}
			string a = "";
			if (!exactClipName)
			{
				if (this.Animator.StateExists(clipName, this.Layer))
				{
					a = clipName;
				}
				else if (this.Animator.StateExists(clipName.CapitalizeFirstLetter(), 0))
				{
					a = clipName.CapitalizeFirstLetter();
				}
				else if (this.Animator.StateExists(clipName.ToLower(), this.Layer))
				{
					a = clipName.ToLower();
				}
				else if (this.Animator.StateExists(clipName.ToUpper(), this.Layer))
				{
					a = clipName.ToUpper();
				}
			}
			else if (this.Animator.StateExists(clipName, this.Layer))
			{
				a = clipName;
			}
			if (a == "")
			{
				Debug.LogWarning("Clip with name " + clipName + " not exists in animator from game object " + this.Animator.gameObject.name);
				return false;
			}
			return true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010297 File Offset: 0x0000E497
		public void CrossFadeInFixedTime(string clip, float transitionTime = 0.25f, float timeOffset = 0f, bool startOver = false)
		{
			this.RefreshClipMemory(clip);
			if (startOver)
			{
				this.Animator.CrossFadeInFixedTime(clip, transitionTime, this.Layer, timeOffset);
				return;
			}
			if (!this.IsPlaying(clip))
			{
				this.Animator.CrossFadeInFixedTime(clip, transitionTime, this.Layer, timeOffset);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000102D6 File Offset: 0x0000E4D6
		public void CrossFade(string clip, float transitionTime = 0.25f, float timeOffset = 0f, bool startOver = false)
		{
			this.RefreshClipMemory(clip);
			if (startOver)
			{
				this.Animator.CrossFade(clip, transitionTime, this.Layer, timeOffset);
				return;
			}
			if (!this.IsPlaying(clip))
			{
				this.Animator.CrossFade(clip, transitionTime, this.Layer, timeOffset);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00010315 File Offset: 0x0000E515
		private void RefreshClipMemory(string name)
		{
			if (name != this.CurrentAnimation)
			{
				this.PreviousAnimation = this.CurrentAnimation;
				this.CurrentAnimation = name;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00010338 File Offset: 0x0000E538
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

		// Token: 0x060001FD RID: 509 RVA: 0x00010380 File Offset: 0x0000E580
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

		// Token: 0x060001FE RID: 510 RVA: 0x000103C8 File Offset: 0x0000E5C8
		internal bool IsPlaying(string clip)
		{
			if (this.Animator.IsInTransition(this.Layer))
			{
				if (this.Animator.GetNextAnimatorStateInfo(this.Layer).shortNameHash == Animator.StringToHash(clip))
				{
					return true;
				}
			}
			else if (this.Animator.GetCurrentAnimatorStateInfo(this.Layer).shortNameHash == Animator.StringToHash(clip))
			{
				return true;
			}
			return false;
		}

		// Token: 0x040001FE RID: 510
		public readonly Animator Animator;

		// Token: 0x040001FF RID: 511
		[CompilerGenerated]
		private string <CurrentAnimation>k__BackingField;

		// Token: 0x04000200 RID: 512
		[CompilerGenerated]
		private string <PreviousAnimation>k__BackingField;

		// Token: 0x04000201 RID: 513
		[CompilerGenerated]
		private int <Layer>k__BackingField;
	}
}
