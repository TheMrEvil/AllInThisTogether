using System;
using UnityEngine;

namespace ExitGames.Client.Photon.StructWrapping
{
	// Token: 0x02000041 RID: 65
	public abstract class StructWrapper : IDisposable
	{
		// Token: 0x06000362 RID: 866 RVA: 0x00019B1E File Offset: 0x00017D1E
		public StructWrapper(Type ttype, WrappedType wrappedType)
		{
			this.ttype = ttype;
			this.wrappedType = wrappedType;
		}

		// Token: 0x06000363 RID: 867
		public abstract object Box();

		// Token: 0x06000364 RID: 868
		public abstract void DisconnectFromPool();

		// Token: 0x06000365 RID: 869
		public abstract void Dispose();

		// Token: 0x06000366 RID: 870
		public abstract string ToString(bool writeType);

		// Token: 0x06000367 RID: 871 RVA: 0x00019B38 File Offset: 0x00017D38
		public static implicit operator StructWrapper(bool value)
		{
			return value.Wrap();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00019B50 File Offset: 0x00017D50
		public static implicit operator StructWrapper(byte value)
		{
			return value.Wrap();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00019B68 File Offset: 0x00017D68
		public static implicit operator StructWrapper(float value)
		{
			return value.Wrap<float>();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00019B80 File Offset: 0x00017D80
		public static implicit operator StructWrapper(double value)
		{
			return value.Wrap<double>();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00019B98 File Offset: 0x00017D98
		public static implicit operator StructWrapper(short value)
		{
			return value.Wrap<short>();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public static implicit operator StructWrapper(int value)
		{
			return value.Wrap<int>();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00019BC8 File Offset: 0x00017DC8
		public static implicit operator StructWrapper(long value)
		{
			return value.Wrap<long>();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public static implicit operator bool(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<bool>).Unwrap();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00019C00 File Offset: 0x00017E00
		public static implicit operator byte(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<byte>).Unwrap();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00019C20 File Offset: 0x00017E20
		public static implicit operator float(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<float>).Unwrap();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00019C40 File Offset: 0x00017E40
		public static implicit operator double(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<double>).Unwrap();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00019C60 File Offset: 0x00017E60
		public static implicit operator short(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<short>).Unwrap();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00019C80 File Offset: 0x00017E80
		public static implicit operator int(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<int>).Unwrap();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00019CA0 File Offset: 0x00017EA0
		public static implicit operator long(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<long>).Unwrap();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00019CC0 File Offset: 0x00017EC0
		public static implicit operator StructWrapper(Vector2 value)
		{
			return value.Wrap<Vector2>();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00019CD8 File Offset: 0x00017ED8
		public static implicit operator StructWrapper(Vector3 value)
		{
			return value.Wrap<Vector3>();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00019CF0 File Offset: 0x00017EF0
		public static implicit operator StructWrapper(Quaternion value)
		{
			return value.Wrap<Quaternion>();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00019D08 File Offset: 0x00017F08
		public static implicit operator Vector2(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<Vector2>).Unwrap();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00019D28 File Offset: 0x00017F28
		public static implicit operator Vector3(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<Vector3>).Unwrap();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00019D48 File Offset: 0x00017F48
		public static implicit operator Quaternion(StructWrapper wrapper)
		{
			return (wrapper as StructWrapper<Quaternion>).Unwrap();
		}

		// Token: 0x040001EB RID: 491
		public readonly WrappedType wrappedType;

		// Token: 0x040001EC RID: 492
		public readonly Type ttype;
	}
}
