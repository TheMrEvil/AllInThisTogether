using System;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000021 RID: 33
	[Preserve]
	[CreateAssetMenu(fileName = "Composite With Emission", menuName = "Text Animator/Animations/Special/Composite With Emission")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class CompositeWithEmission : AnimationScriptableBase
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003CA8 File Offset: 0x00001EA8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.ValidateArray();
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].InitializeOnce();
			}
			this.prev = default(MeshData);
			this.prev.colors = new Color32[4];
			this.prev.positions = new Vector3[4];
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D0C File Offset: 0x00001F0C
		public override void ResetContext(TAnimCore animator)
		{
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetContext(animator);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D38 File Offset: 0x00001F38
		public override void SetModifier(ModifierInfo modifier)
		{
			base.SetModifier(modifier);
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetModifier(modifier);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003D6C File Offset: 0x00001F6C
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			float time = this.timeMode.GetTime(animator.time.timeSinceStart, character.passedTime, character.index);
			if (time < 0f)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				this.prev.positions[i] = character.current.positions[i];
				this.prev.colors[i] = character.current.colors[i];
			}
			float t = this.emissionCurve.Evaluate(time);
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				if (animationScriptableBase.CanApplyEffectTo(character, animator))
				{
					animationScriptableBase.ApplyEffectTo(ref character, animator);
				}
			}
			for (int k = 0; k < 4; k++)
			{
				character.current.positions[k] = Vector3.LerpUnclamped(this.prev.positions[k], character.current.positions[k], t);
				character.current.colors[k] = Color32.LerpUnclamped(this.prev.colors[k], character.current.colors[k], t);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003EC5 File Offset: 0x000020C5
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003EC8 File Offset: 0x000020C8
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003ED8 File Offset: 0x000020D8
		private void ValidateArray()
		{
			List<AnimationScriptableBase> list = new List<AnimationScriptableBase>();
			for (int i = 0; i < this.animations.Length; i++)
			{
				if (this.animations[i] != this)
				{
					list.Add(this.animations[i]);
				}
			}
			this.animations = list.ToArray();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003F28 File Offset: 0x00002128
		private void OnValidate()
		{
			this.ValidateArray();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F30 File Offset: 0x00002130
		public CompositeWithEmission()
		{
		}

		// Token: 0x04000067 RID: 103
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x04000068 RID: 104
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x04000069 RID: 105
		public AnimationScriptableBase[] animations = new AnimationScriptableBase[0];

		// Token: 0x0400006A RID: 106
		private MeshData prev;
	}
}
