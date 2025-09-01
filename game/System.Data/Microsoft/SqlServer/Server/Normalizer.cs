using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000043 RID: 67
	internal abstract class Normalizer
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		internal static Normalizer GetNormalizer(Type t)
		{
			Normalizer normalizer = null;
			if (t.IsPrimitive)
			{
				if (t == typeof(byte))
				{
					normalizer = new ByteNormalizer();
				}
				else if (t == typeof(sbyte))
				{
					normalizer = new SByteNormalizer();
				}
				else if (t == typeof(bool))
				{
					normalizer = new BooleanNormalizer();
				}
				else if (t == typeof(short))
				{
					normalizer = new ShortNormalizer();
				}
				else if (t == typeof(ushort))
				{
					normalizer = new UShortNormalizer();
				}
				else if (t == typeof(int))
				{
					normalizer = new IntNormalizer();
				}
				else if (t == typeof(uint))
				{
					normalizer = new UIntNormalizer();
				}
				else if (t == typeof(float))
				{
					normalizer = new FloatNormalizer();
				}
				else if (t == typeof(double))
				{
					normalizer = new DoubleNormalizer();
				}
				else if (t == typeof(long))
				{
					normalizer = new LongNormalizer();
				}
				else if (t == typeof(ulong))
				{
					normalizer = new ULongNormalizer();
				}
			}
			else if (t.IsValueType)
			{
				normalizer = new BinaryOrderedUdtNormalizer(t, false);
			}
			if (normalizer == null)
			{
				throw new Exception(SR.GetString("Cannot create normalizer for '{0}'.", new object[]
				{
					t.FullName
				}));
			}
			normalizer._skipNormalize = false;
			return normalizer;
		}

		// Token: 0x060003FC RID: 1020
		internal abstract void Normalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x060003FD RID: 1021
		internal abstract void DeNormalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x060003FE RID: 1022 RVA: 0x0000FF4C File Offset: 0x0000E14C
		protected void FlipAllBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = ~b[i];
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000FF6F File Offset: 0x0000E16F
		protected object GetValue(FieldInfo fi, object obj)
		{
			return fi.GetValue(obj);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000FF78 File Offset: 0x0000E178
		protected void SetValue(FieldInfo fi, object recvr, object value)
		{
			fi.SetValue(recvr, value);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000401 RID: 1025
		internal abstract int Size { get; }

		// Token: 0x06000402 RID: 1026 RVA: 0x00003D93 File Offset: 0x00001F93
		protected Normalizer()
		{
		}

		// Token: 0x04000533 RID: 1331
		protected bool _skipNormalize;
	}
}
