using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000E2 RID: 226
	internal class ES3ResourcesStream : MemoryStream
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001E332 File Offset: 0x0001C532
		public bool Exists
		{
			get
			{
				return this.Length > 0L;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001E33E File Offset: 0x0001C53E
		public ES3ResourcesStream(string path) : base(ES3ResourcesStream.GetData(path))
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001E34C File Offset: 0x0001C54C
		private static byte[] GetData(string path)
		{
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			if (textAsset == null)
			{
				return new byte[0];
			}
			return textAsset.bytes;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001E37B File Offset: 0x0001C57B
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
