using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000911 RID: 2321
	internal struct MonoWin32Resource
	{
		// Token: 0x06004E93 RID: 20115 RVA: 0x000F65EE File Offset: 0x000F47EE
		public MonoWin32Resource(int res_type, int res_id, int lang_id, byte[] data)
		{
			this.res_type = res_type;
			this.res_id = res_id;
			this.lang_id = lang_id;
			this.data = data;
		}

		// Token: 0x040030DE RID: 12510
		public int res_type;

		// Token: 0x040030DF RID: 12511
		public int res_id;

		// Token: 0x040030E0 RID: 12512
		public int lang_id;

		// Token: 0x040030E1 RID: 12513
		public byte[] data;
	}
}
