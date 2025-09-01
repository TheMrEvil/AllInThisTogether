using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000042 RID: 66
	[AttributeUsage(AttributeTargets.Class)]
	public class EffectInfoAttribute : TagInfoAttribute
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public EffectInfoAttribute(string tagID, EffectCategory category) : base(tagID)
		{
			this.category = category;
		}

		// Token: 0x040000FB RID: 251
		public readonly EffectCategory category;
	}
}
