using System;
using Febucci.UI.Core;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001E RID: 30
	public abstract class BehaviorScriptableBase : AnimationScriptableBase
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003A4E File Offset: 0x00001C4E
		public override float GetMaxDuration()
		{
			return -1f;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A55 File Offset: 0x00001C55
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A58 File Offset: 0x00001C58
		protected BehaviorScriptableBase()
		{
		}
	}
}
