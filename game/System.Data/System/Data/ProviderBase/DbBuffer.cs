using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x02000359 RID: 857
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06002735 RID: 10037 RVA: 0x000AE4B0 File Offset: 0x000AC6B0
		protected DbBuffer(int initialSize) : base(IntPtr.Zero, true)
		{
			if (0 < initialSize)
			{
				this._bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = SafeNativeMethods.LocalAlloc((IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000AE518 File Offset: 0x000AC718
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle) : base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x00006D64 File Offset: 0x00004F64
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x00089910 File Offset: 0x00087B10
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000AE522 File Offset: 0x000AC722
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000AE52C File Offset: 0x000AC72C
		internal string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset));
				this.Validate(offset, 2 * (text.Length + 1));
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000AE59C File Offset: 0x000AC79C
		internal string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.PtrToStringUni(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000AE600 File Offset: 0x000AC800
		internal byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.ReadByte(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000AE650 File Offset: 0x000AC850
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] destination = new byte[length];
			return this.ReadBytes(offset, destination, 0, length);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000AE670 File Offset: 0x000AC870
		internal byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000AE6D4 File Offset: 0x000AC8D4
		internal char ReadChar(int offset)
		{
			return (char)this.ReadInt16(offset);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000AE6E0 File Offset: 0x000AC8E0
		internal char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000AE744 File Offset: 0x000AC944
		internal double ReadDouble(int offset)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(offset));
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000AE754 File Offset: 0x000AC954
		internal short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.ReadInt16(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000AE7A4 File Offset: 0x000AC9A4
		internal void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000AE808 File Offset: 0x000ACA08
		internal int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.ReadInt32(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000AE858 File Offset: 0x000ACA58
		internal void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(ADP.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000AE8BC File Offset: 0x000ACABC
		internal long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.ReadInt64(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000AE90C File Offset: 0x000ACB0C
		internal IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr result;
			try
			{
				base.DangerousAddRef(ref flag);
				result = Marshal.ReadIntPtr(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000AE95C File Offset: 0x000ACB5C
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000AE978 File Offset: 0x000ACB78
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000AE9AC File Offset: 0x000ACBAC
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.StructureToPtr(structure, ptr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000AEA04 File Offset: 0x000ACC04
		internal void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteByte(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000AEA54 File Offset: 0x000ACC54
		internal void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000AEAB8 File Offset: 0x000ACCB8
		internal void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000AEB1C File Offset: 0x000ACD1C
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000AEB2C File Offset: 0x000ACD2C
		internal void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt16(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000AEB7C File Offset: 0x000ACD7C
		internal void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000AEBE0 File Offset: 0x000ACDE0
		internal void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt32(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000AEC30 File Offset: 0x000ACE30
		internal void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000AEC94 File Offset: 0x000ACE94
		internal void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt64(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000AECE4 File Offset: 0x000ACEE4
		internal void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteIntPtr(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000AED34 File Offset: 0x000ACF34
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000AED44 File Offset: 0x000ACF44
		internal void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafeNativeMethods.ZeroMemory(base.DangerousGetHandle(), this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000AED90 File Offset: 0x000ACF90
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000AEDB7 File Offset: 0x000ACFB7
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000AEDC8 File Offset: 0x000ACFC8
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000AEDF8 File Offset: 0x000ACFF8
		internal void WriteDate(int offset, DateTime value)
		{
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, source, 0, 3);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000AEE38 File Offset: 0x000AD038
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000AEE68 File Offset: 0x000AD068
		internal void WriteTime(int offset, TimeSpan value)
		{
			short[] source = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, source, 0, 3);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000AEEFC File Offset: 0x000AD0FC
		internal void WriteDateTime(int offset, DateTime value)
		{
			int value2 = (int)(value.Ticks % 10000000L) * 100;
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			this.WriteInt16Array(offset, source, 0, 6);
			this.WriteInt32(offset + 12, value2);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000AEF7C File Offset: 0x000AD17C
		internal decimal ReadNumeric(int offset)
		{
			byte[] array = new byte[20];
			this.ReadBytes(offset, array, 1, 19);
			int[] array2 = new int[]
			{
				0,
				0,
				0,
				(int)array[2] << 16
			};
			if (array[3] == 0)
			{
				array2[3] |= int.MinValue;
			}
			array2[0] = BitConverter.ToInt32(array, 4);
			array2[1] = BitConverter.ToInt32(array, 8);
			array2[2] = BitConverter.ToInt32(array, 12);
			if (BitConverter.ToInt32(array, 16) != 0)
			{
				throw ADP.NumericToDecimalOverflow();
			}
			return new decimal(array2);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000AEFF8 File Offset: 0x000AD1F8
		internal void WriteNumeric(int offset, decimal value, byte precision)
		{
			int[] bits = decimal.GetBits(value);
			byte[] array = new byte[20];
			array[1] = precision;
			Buffer.BlockCopy(bits, 14, array, 2, 2);
			array[3] = ((array[3] == 0) ? 1 : 0);
			Buffer.BlockCopy(bits, 0, array, 4, 12);
			array[16] = 0;
			array[17] = 0;
			array[18] = 0;
			array[19] = 0;
			this.WriteBytes(offset, array, 1, 19);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000AF058 File Offset: 0x000AD258
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000AF062 File Offset: 0x000AD262
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x04001994 RID: 6548
		private readonly int _bufferLength;
	}
}
