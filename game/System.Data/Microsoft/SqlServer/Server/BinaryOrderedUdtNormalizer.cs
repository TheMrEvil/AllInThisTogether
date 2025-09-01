using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000042 RID: 66
	internal sealed class BinaryOrderedUdtNormalizer : Normalizer
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000FA9A File Offset: 0x0000DC9A
		private FieldInfo[] GetFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		internal BinaryOrderedUdtNormalizer(Type t, bool isTopLevelUdt)
		{
			this._skipNormalize = false;
			if (this._skipNormalize)
			{
				this._isTopLevelUdt = true;
			}
			this._isTopLevelUdt = true;
			FieldInfo[] fields = this.GetFields(t);
			this.FieldsToNormalize = new FieldInfoEx[fields.Length];
			int num = 0;
			foreach (FieldInfo fieldInfo in fields)
			{
				int offset = Marshal.OffsetOf(fieldInfo.DeclaringType, fieldInfo.Name).ToInt32();
				this.FieldsToNormalize[num++] = new FieldInfoEx(fieldInfo, offset, Normalizer.GetNormalizer(fieldInfo.FieldType));
			}
			Array.Sort<FieldInfoEx>(this.FieldsToNormalize);
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t))
			{
				PropertyInfo property = t.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property == null || property.PropertyType != t)
				{
					FieldInfo field = t.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (field == null || field.FieldType != t)
					{
						throw new Exception("could not find Null field/property in nullable type " + ((t != null) ? t.ToString() : null));
					}
					this.NullInstance = field.GetValue(null);
				}
				else
				{
					this.NullInstance = property.GetValue(null, null);
				}
				this._padBuffer = new byte[this.Size - 1];
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000FC0A File Offset: 0x0000DE0A
		internal bool IsNullable
		{
			get
			{
				return this.NullInstance != null;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000FC15 File Offset: 0x0000DE15
		internal void NormalizeTopObject(object udt, Stream s)
		{
			this.Normalize(null, udt, s);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000FC20 File Offset: 0x0000DE20
		internal object DeNormalizeTopObject(Type t, Stream s)
		{
			return this.DeNormalizeInternal(t, s);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object DeNormalizeInternal(Type t, Stream s)
		{
			object obj = null;
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t) && (byte)s.ReadByte() == 0)
			{
				obj = this.NullInstance;
				s.Read(this._padBuffer, 0, this._padBuffer.Length);
				return obj;
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(t);
			}
			foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
			{
				fieldInfoEx.Normalizer.DeNormalize(fieldInfoEx.FieldInfo, obj, s);
			}
			return obj;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			object obj2;
			if (fi == null)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = base.GetValue(fi, obj);
			}
			INullable nullable = obj2 as INullable;
			if (nullable != null && !this._isTopLevelUdt)
			{
				if (nullable.IsNull)
				{
					s.WriteByte(0);
					s.Write(this._padBuffer, 0, this._padBuffer.Length);
					return;
				}
				s.WriteByte(1);
			}
			foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
			{
				fieldInfoEx.Normalizer.Normalize(fieldInfoEx.FieldInfo, obj2, s);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000FD44 File Offset: 0x0000DF44
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			base.SetValue(fi, recvr, this.DeNormalizeInternal(fi.FieldType, s));
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		internal override int Size
		{
			get
			{
				if (this._size != 0)
				{
					return this._size;
				}
				if (this.IsNullable && !this._isTopLevelUdt)
				{
					this._size = 1;
				}
				foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
				{
					this._size += fieldInfoEx.Normalizer.Size;
				}
				return this._size;
			}
		}

		// Token: 0x0400052E RID: 1326
		internal readonly FieldInfoEx[] FieldsToNormalize;

		// Token: 0x0400052F RID: 1327
		private int _size;

		// Token: 0x04000530 RID: 1328
		private byte[] _padBuffer;

		// Token: 0x04000531 RID: 1329
		internal readonly object NullInstance;

		// Token: 0x04000532 RID: 1330
		private bool _isTopLevelUdt;
	}
}
