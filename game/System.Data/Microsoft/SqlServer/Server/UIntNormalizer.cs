using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004A RID: 74
	internal sealed class UIntNormalizer : Normalizer
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0001025C File Offset: 0x0000E45C
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((uint)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00010298 File Offset: 0x0000E498
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt32(array, 0));
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00010257 File Offset: 0x0000E457
		internal override int Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public UIntNormalizer()
		{
		}
	}
}
