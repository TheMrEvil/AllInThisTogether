using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000045 RID: 69
	internal sealed class SByteNormalizer : Normalizer
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte b = (byte)((sbyte)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				b ^= 128;
			}
			s.WriteByte(b);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010014 File Offset: 0x0000E214
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			if (!this._skipNormalize)
			{
				b ^= 128;
			}
			sbyte b2 = (sbyte)b;
			base.SetValue(fi, recvr, b2);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override int Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public SByteNormalizer()
		{
		}
	}
}
