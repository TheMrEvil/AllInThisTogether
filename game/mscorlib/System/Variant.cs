using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000268 RID: 616
	[StructLayout(LayoutKind.Explicit)]
	internal struct Variant
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x00068BC4 File Offset: 0x00066DC4
		public void SetValue(object obj)
		{
			this.vt = 0;
			if (obj == null)
			{
				return;
			}
			Type type = obj.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(sbyte))
			{
				this.vt = 16;
				this.cVal = (sbyte)obj;
				return;
			}
			if (type == typeof(byte))
			{
				this.vt = 17;
				this.bVal = (byte)obj;
				return;
			}
			if (type == typeof(short))
			{
				this.vt = 2;
				this.iVal = (short)obj;
				return;
			}
			if (type == typeof(ushort))
			{
				this.vt = 18;
				this.uiVal = (ushort)obj;
				return;
			}
			if (type == typeof(int))
			{
				this.vt = 3;
				this.lVal = (int)obj;
				return;
			}
			if (type == typeof(uint))
			{
				this.vt = 19;
				this.ulVal = (uint)obj;
				return;
			}
			if (type == typeof(long))
			{
				this.vt = 20;
				this.llVal = (long)obj;
				return;
			}
			if (type == typeof(ulong))
			{
				this.vt = 21;
				this.ullVal = (ulong)obj;
				return;
			}
			if (type == typeof(float))
			{
				this.vt = 4;
				this.fltVal = (float)obj;
				return;
			}
			if (type == typeof(double))
			{
				this.vt = 5;
				this.dblVal = (double)obj;
				return;
			}
			if (type == typeof(string))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR((string)obj);
				return;
			}
			if (type == typeof(bool))
			{
				this.vt = 11;
				this.lVal = (((bool)obj) ? -1 : 0);
				return;
			}
			if (type == typeof(BStrWrapper))
			{
				this.vt = 8;
				this.bstrVal = Marshal.StringToBSTR(((BStrWrapper)obj).WrappedObject);
				return;
			}
			if (type == typeof(UnknownWrapper))
			{
				this.vt = 13;
				this.pdispVal = Marshal.GetIUnknownForObject(((UnknownWrapper)obj).WrappedObject);
				return;
			}
			if (type == typeof(DispatchWrapper))
			{
				this.vt = 9;
				this.pdispVal = Marshal.GetIDispatchForObject(((DispatchWrapper)obj).WrappedObject);
				return;
			}
			try
			{
				this.pdispVal = Marshal.GetIDispatchForObject(obj);
				this.vt = 9;
				return;
			}
			catch
			{
			}
			try
			{
				this.vt = 13;
				this.pdispVal = Marshal.GetIUnknownForObject(obj);
			}
			catch (Exception inner)
			{
				throw new NotImplementedException(string.Format("Variant couldn't handle object of type {0}", obj.GetType()), inner);
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00068EC4 File Offset: 0x000670C4
		public static object GetValueAt(int vt, IntPtr addr)
		{
			object result = null;
			switch (vt)
			{
			case 2:
				result = Marshal.ReadInt16(addr);
				break;
			case 3:
				result = Marshal.ReadInt32(addr);
				break;
			case 4:
				result = Marshal.PtrToStructure(addr, typeof(float));
				break;
			case 5:
				result = Marshal.PtrToStructure(addr, typeof(double));
				break;
			case 8:
				result = Marshal.PtrToStringBSTR(Marshal.ReadIntPtr(addr));
				break;
			case 9:
			case 13:
			{
				IntPtr intPtr = Marshal.ReadIntPtr(addr);
				if (intPtr != IntPtr.Zero)
				{
					result = Marshal.GetObjectForIUnknown(intPtr);
				}
				break;
			}
			case 11:
				result = (Marshal.ReadInt16(addr) != 0);
				break;
			case 16:
				result = (sbyte)Marshal.ReadByte(addr);
				break;
			case 17:
				result = Marshal.ReadByte(addr);
				break;
			case 18:
				result = (ushort)Marshal.ReadInt16(addr);
				break;
			case 19:
				result = (uint)Marshal.ReadInt32(addr);
				break;
			case 20:
				result = Marshal.ReadInt64(addr);
				break;
			case 21:
				result = (ulong)Marshal.ReadInt64(addr);
				break;
			}
			return result;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00069014 File Offset: 0x00067214
		public object GetValue()
		{
			object result = null;
			switch (this.vt)
			{
			case 2:
				return this.iVal;
			case 3:
				return this.lVal;
			case 4:
				return this.fltVal;
			case 5:
				return this.dblVal;
			case 8:
				return Marshal.PtrToStringBSTR(this.bstrVal);
			case 9:
			case 13:
				if (this.pdispVal != IntPtr.Zero)
				{
					return Marshal.GetObjectForIUnknown(this.pdispVal);
				}
				return result;
			case 11:
				return this.boolVal != 0;
			case 16:
				return this.cVal;
			case 17:
				return this.bVal;
			case 18:
				return this.uiVal;
			case 19:
				return this.ulVal;
			case 20:
				return this.llVal;
			case 21:
				return this.ullVal;
			}
			if ((this.vt & 16384) == 16384 && this.pdispVal != IntPtr.Zero)
			{
				result = Variant.GetValueAt((int)(this.vt & -16385), this.pdispVal);
			}
			return result;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000691AC File Offset: 0x000673AC
		public void Clear()
		{
			if (this.vt == 8)
			{
				Marshal.FreeBSTR(this.bstrVal);
				return;
			}
			if ((this.vt == 9 || this.vt == 13) && this.pdispVal != IntPtr.Zero)
			{
				Marshal.Release(this.pdispVal);
			}
		}

		// Token: 0x040019A6 RID: 6566
		[FieldOffset(0)]
		public short vt;

		// Token: 0x040019A7 RID: 6567
		[FieldOffset(2)]
		public ushort wReserved1;

		// Token: 0x040019A8 RID: 6568
		[FieldOffset(4)]
		public ushort wReserved2;

		// Token: 0x040019A9 RID: 6569
		[FieldOffset(6)]
		public ushort wReserved3;

		// Token: 0x040019AA RID: 6570
		[FieldOffset(8)]
		public long llVal;

		// Token: 0x040019AB RID: 6571
		[FieldOffset(8)]
		public int lVal;

		// Token: 0x040019AC RID: 6572
		[FieldOffset(8)]
		public byte bVal;

		// Token: 0x040019AD RID: 6573
		[FieldOffset(8)]
		public short iVal;

		// Token: 0x040019AE RID: 6574
		[FieldOffset(8)]
		public float fltVal;

		// Token: 0x040019AF RID: 6575
		[FieldOffset(8)]
		public double dblVal;

		// Token: 0x040019B0 RID: 6576
		[FieldOffset(8)]
		public short boolVal;

		// Token: 0x040019B1 RID: 6577
		[FieldOffset(8)]
		public IntPtr bstrVal;

		// Token: 0x040019B2 RID: 6578
		[FieldOffset(8)]
		public sbyte cVal;

		// Token: 0x040019B3 RID: 6579
		[FieldOffset(8)]
		public ushort uiVal;

		// Token: 0x040019B4 RID: 6580
		[FieldOffset(8)]
		public uint ulVal;

		// Token: 0x040019B5 RID: 6581
		[FieldOffset(8)]
		public ulong ullVal;

		// Token: 0x040019B6 RID: 6582
		[FieldOffset(8)]
		public int intVal;

		// Token: 0x040019B7 RID: 6583
		[FieldOffset(8)]
		public uint uintVal;

		// Token: 0x040019B8 RID: 6584
		[FieldOffset(8)]
		public IntPtr pdispVal;

		// Token: 0x040019B9 RID: 6585
		[FieldOffset(8)]
		public BRECORD bRecord;
	}
}
