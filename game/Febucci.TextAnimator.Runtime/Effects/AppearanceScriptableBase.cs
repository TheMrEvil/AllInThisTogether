using System;
using Febucci.UI.Core;

namespace Febucci.UI.Effects
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public abstract class AppearanceScriptableBase : AnimationScriptableBase
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002E62 File Offset: 0x00001062
		public override void ResetContext(TAnimCore animator)
		{
			this.duration = this.baseDuration;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E70 File Offset: 0x00001070
		public override float GetMaxDuration()
		{
			return this.duration;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E78 File Offset: 0x00001078
		public override void SetModifier(ModifierInfo modifier)
		{
			if (modifier.name == "d")
			{
				this.duration = this.baseDuration * modifier.value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E9F File Offset: 0x0000109F
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return character.passedTime <= this.duration;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EB2 File Offset: 0x000010B2
		protected AppearanceScriptableBase()
		{
		}

		// Token: 0x04000037 RID: 55
		public float baseDuration = 0.5f;

		// Token: 0x04000038 RID: 56
		protected float duration;
	}
}
