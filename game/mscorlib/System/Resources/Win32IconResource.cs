using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000876 RID: 2166
	internal class Win32IconResource : Win32Resource
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x000ED156 File Offset: 0x000EB356
		public Win32IconResource(int id, int language, ICONDIRENTRY icon) : base(Win32ResourceType.RT_ICON, id, language)
		{
			this.icon = icon;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x000ED168 File Offset: 0x000EB368
		public ICONDIRENTRY Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x000ED170 File Offset: 0x000EB370
		public override void WriteTo(Stream s)
		{
			s.Write(this.icon.image, 0, this.icon.image.Length);
		}

		// Token: 0x04002E2E RID: 11822
		private ICONDIRENTRY icon;
	}
}
