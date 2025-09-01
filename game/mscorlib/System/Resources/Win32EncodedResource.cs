using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000875 RID: 2165
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x0600481B RID: 18459 RVA: 0x000ED124 File Offset: 0x000EB324
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data) : base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x000ED137 File Offset: 0x000EB337
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x000ED13F File Offset: 0x000EB33F
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04002E2D RID: 11821
		private byte[] data;
	}
}
