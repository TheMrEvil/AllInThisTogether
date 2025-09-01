using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000046 RID: 70
	internal sealed class ByteNormalizer : Normalizer
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0001004C File Offset: 0x0000E24C
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte value = (byte)base.GetValue(fi, obj);
			s.WriteByte(value);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010070 File Offset: 0x0000E270
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override int Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public ByteNormalizer()
		{
		}
	}
}
