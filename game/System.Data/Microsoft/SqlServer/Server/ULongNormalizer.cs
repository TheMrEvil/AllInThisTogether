using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004C RID: 76
	internal sealed class ULongNormalizer : Normalizer
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00010380 File Offset: 0x0000E580
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ulong)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000103BC File Offset: 0x0000E5BC
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt64(array, 0));
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001037B File Offset: 0x0000E57B
		internal override int Size
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public ULongNormalizer()
		{
		}
	}
}
