using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000079 RID: 121
	public class UnitAttribute : Attribute
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00003F0A File Offset: 0x0000210A
		public UnitAttribute(Units unit)
		{
			this.Base = unit;
			this.Display = unit;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003F2E File Offset: 0x0000212E
		public UnitAttribute(string unit)
		{
			this.BaseName = unit;
			this.DisplayName = unit;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003F52 File Offset: 0x00002152
		public UnitAttribute(Units @base, Units display)
		{
			this.Base = @base;
			this.Display = display;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003F76 File Offset: 0x00002176
		public UnitAttribute(Units @base, string display)
		{
			this.Base = @base;
			this.DisplayName = display;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003F9A File Offset: 0x0000219A
		public UnitAttribute(string @base, Units display)
		{
			this.BaseName = @base;
			this.Display = display;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003FBE File Offset: 0x000021BE
		public UnitAttribute(string @base, string display)
		{
			this.BaseName = @base;
			this.DisplayName = display;
		}

		// Token: 0x0400015A RID: 346
		public Units Base = Units.Unset;

		// Token: 0x0400015B RID: 347
		public Units Display = Units.Unset;

		// Token: 0x0400015C RID: 348
		public string BaseName;

		// Token: 0x0400015D RID: 349
		public string DisplayName;

		// Token: 0x0400015E RID: 350
		public bool DisplayAsString;

		// Token: 0x0400015F RID: 351
		public bool ForceDisplayUnit;
	}
}
