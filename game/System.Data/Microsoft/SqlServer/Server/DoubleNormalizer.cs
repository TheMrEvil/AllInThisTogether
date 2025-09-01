using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004E RID: 78
	internal sealed class DoubleNormalizer : Normalizer
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x000104D0 File Offset: 0x0000E6D0
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			double num = (double)base.GetValue(fi, obj);
			byte[] bytes = BitConverter.GetBytes(num);
			if (!this._skipNormalize)
			{
				Array.Reverse<byte>(bytes);
				if ((bytes[0] & 128) == 0)
				{
					byte[] array = bytes;
					int num2 = 0;
					array[num2] ^= 128;
				}
				else if (num < 0.0)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001053C File Offset: 0x0000E73C
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				if ((array[0] & 128) > 0)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] ^= 128;
				}
				else
				{
					base.FlipAllBits(array);
				}
				Array.Reverse<byte>(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToDouble(array, 0));
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001037B File Offset: 0x0000E57B
		internal override int Size
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public DoubleNormalizer()
		{
		}
	}
}
