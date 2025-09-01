using System;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000020 RID: 32
	[Preserve]
	[CreateAssetMenu(fileName = "Composite Animation", menuName = "Text Animator/Animations/Special/Composite")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class CompositeAnimation : AnimationScriptableBase
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003B28 File Offset: 0x00001D28
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.ValidateArray();
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].InitializeOnce();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B60 File Offset: 0x00001D60
		public override void ResetContext(TAnimCore animator)
		{
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetContext(animator);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B8C File Offset: 0x00001D8C
		public override void SetModifier(ModifierInfo modifier)
		{
			base.SetModifier(modifier);
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetModifier(modifier);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				if (animationScriptableBase.CanApplyEffectTo(character, animator))
				{
					animationScriptableBase.ApplyEffectTo(ref character, animator);
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003BFD File Offset: 0x00001DFD
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C00 File Offset: 0x00001E00
		public override float GetMaxDuration()
		{
			float num = -1f;
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				num = Mathf.Max(num, animationScriptableBase.GetMaxDuration());
			}
			return num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003C3C File Offset: 0x00001E3C
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

		// Token: 0x06000077 RID: 119 RVA: 0x00003C8C File Offset: 0x00001E8C
		private void OnValidate()
		{
			this.ValidateArray();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C94 File Offset: 0x00001E94
		public CompositeAnimation()
		{
		}

		// Token: 0x04000066 RID: 102
		public AnimationScriptableBase[] animations = new AnimationScriptableBase[0];
	}
}
