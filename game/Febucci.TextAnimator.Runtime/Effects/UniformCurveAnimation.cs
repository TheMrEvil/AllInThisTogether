using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000022 RID: 34
	[Preserve]
	[CreateAssetMenu(fileName = "Uniform Curve Animation", menuName = "Text Animator/Animations/Special/Uniform Curve")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class UniformCurveAnimation : AnimationScriptableBase
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003F5B File Offset: 0x0000215B
		public override void ResetContext(TAnimCore animator)
		{
			this.weightMult = 1f;
			this.timeSpeed = 1f;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003F74 File Offset: 0x00002174
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.timeSpeed = modifier.value;
				return;
			}
			if (!(name == "a"))
			{
				return;
			}
			this.weightMult = modifier.value;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003FBC File Offset: 0x000021BC
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = this.timeMode.GetTime(animator.time.timeSinceStart * this.timeSpeed, character.passedTime * this.timeSpeed, character.index);
			if (this.timePassed < 0f)
			{
				return;
			}
			float num = this.weightMult * this.emissionCurve.Evaluate(this.timePassed);
			Matrix4x4 matrix4x;
			Vector3 b;
			if (this.animationData.TryCalculatingMatrix(character, this.timePassed, num, out matrix4x, out b))
			{
				for (byte b2 = 0; b2 < 4; b2 += 1)
				{
					character.current.positions[(int)b2] = matrix4x.MultiplyPoint3x4(character.current.positions[(int)b2] - b) + b;
				}
			}
			Color32 target;
			if (this.animationData.TryCalculatingColor(character, this.timePassed, num, out target))
			{
				character.current.colors.LerpUnclamped(target, Mathf.Clamp01(num));
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000040BF File Offset: 0x000022BF
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000040CC File Offset: 0x000022CC
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000040CF File Offset: 0x000022CF
		public UniformCurveAnimation()
		{
		}

		// Token: 0x0400006B RID: 107
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x0400006C RID: 108
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x0400006D RID: 109
		public AnimationData animationData = new AnimationData();

		// Token: 0x0400006E RID: 110
		private float weightMult;

		// Token: 0x0400006F RID: 111
		private float timeSpeed;

		// Token: 0x04000070 RID: 112
		private bool hasTransformEffects;

		// Token: 0x04000071 RID: 113
		private float timePassed;
	}
}
