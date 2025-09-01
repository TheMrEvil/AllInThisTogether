using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000044 RID: 68
	internal sealed class BooleanNormalizer : Normalizer
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000FF84 File Offset: 0x0000E184
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			s.WriteByte(((bool)base.GetValue(fi, obj)) ? 1 : 0);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b == 1);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override int Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public BooleanNormalizer()
		{
		}
	}
}
