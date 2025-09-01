using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000048 RID: 72
	internal sealed class UShortNormalizer : Normalizer
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00010138 File Offset: 0x0000E338
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010174 File Offset: 0x0000E374
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt16(array, 0));
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00010133 File Offset: 0x0000E333
		internal override int Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public UShortNormalizer()
		{
		}
	}
}
