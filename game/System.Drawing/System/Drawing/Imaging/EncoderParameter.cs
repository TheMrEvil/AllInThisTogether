using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Used to pass a value, or an array of values, to an image encoder.</summary>
	// Token: 0x02000102 RID: 258
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EncoderParameter : IDisposable
	{
		/// <summary>Allows an <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object is reclaimed by garbage collection.</summary>
		// Token: 0x06000C51 RID: 3153 RVA: 0x0001C420 File Offset: 0x0001A620
		~EncoderParameter()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Imaging.Encoder" /> object associated with this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The <see cref="T:System.Drawing.Imaging.Encoder" /> object encapsulates the globally unique identifier (GUID) that specifies the category (for example <see cref="F:System.Drawing.Imaging.Encoder.Quality" />, <see cref="F:System.Drawing.Imaging.Encoder.ColorDepth" />, or <see cref="F:System.Drawing.Imaging.Encoder.Compression" />) of the parameter stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the GUID that specifies the category of the parameter stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</returns>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001C450 File Offset: 0x0001A650
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0001C45D File Offset: 0x0001A65D
		public Encoder Encoder
		{
			get
			{
				return new Encoder(this._parameterGuid);
			}
			set
			{
				this._parameterGuid = value.Guid;
			}
		}

		/// <summary>Gets the data type of the values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <returns>A member of the <see cref="T:System.Drawing.Imaging.EncoderParameterValueType" /> enumeration that indicates the data type of the values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001C46B File Offset: 0x0001A66B
		public EncoderParameterValueType Type
		{
			get
			{
				return this._parameterValueType;
			}
		}

		/// <summary>Gets the data type of the values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <returns>A member of the <see cref="T:System.Drawing.Imaging.EncoderParameterValueType" /> enumeration that indicates the data type of the values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001C46B File Offset: 0x0001A66B
		public EncoderParameterValueType ValueType
		{
			get
			{
				return this._parameterValueType;
			}
		}

		/// <summary>Gets the number of elements in the array of values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <returns>An integer that indicates the number of elements in the array of values stored in this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001C473 File Offset: 0x0001A673
		public int NumberOfValues
		{
			get
			{
				return this._numberOfValues;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		// Token: 0x06000C57 RID: 3159 RVA: 0x0001C47B File Offset: 0x0001A67B
		public void Dispose()
		{
			this.Dispose(true);
			GC.KeepAlive(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001C490 File Offset: 0x0001A690
		private void Dispose(bool disposing)
		{
			if (this._parameterValue != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this._parameterValue);
			}
			this._parameterValue = IntPtr.Zero;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and one unsigned 8-bit integer. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">An 8-bit unsigned integer that specifies the value stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public EncoderParameter(Encoder encoder, byte value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeByte;
			this._numberOfValues = 1;
			this._parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteByte(this._parameterValue, value);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and one 8-bit value. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" /> or <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">A byte that specifies the value stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		/// <param name="undefined">If <see langword="true" />, the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" />; otherwise, the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />.</param>
		// Token: 0x06000C5A RID: 3162 RVA: 0x0001C530 File Offset: 0x0001A730
		public EncoderParameter(Encoder encoder, byte value, bool undefined)
		{
			this._parameterGuid = encoder.Guid;
			if (undefined)
			{
				this._parameterValueType = EncoderParameterValueType.ValueTypeUndefined;
			}
			else
			{
				this._parameterValueType = EncoderParameterValueType.ValueTypeByte;
			}
			this._numberOfValues = 1;
			this._parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteByte(this._parameterValue, value);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and one, 16-bit integer. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeShort" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">A 16-bit integer that specifies the value stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. Must be nonnegative.</param>
		// Token: 0x06000C5B RID: 3163 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public EncoderParameter(Encoder encoder, short value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeShort;
			this._numberOfValues = 1;
			this._parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(short)));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt16(this._parameterValue, value);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and one 64-bit integer. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeLong" /> (32 bits), and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">A 64-bit integer that specifies the value stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. Must be nonnegative. This parameter is converted to a 32-bit integer before it is stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C5C RID: 3164 RVA: 0x0001C624 File Offset: 0x0001A824
		public EncoderParameter(Encoder encoder, long value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeLong;
			this._numberOfValues = 1;
			this._parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this._parameterValue, (int)value);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and a pair of 32-bit integers. The pair of integers represents a fraction, the first integer being the numerator, and the second integer being the denominator. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeRational" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="numerator">A 32-bit integer that represents the numerator of a fraction. Must be nonnegative.</param>
		/// <param name="denominator">A 32-bit integer that represents the denominator of a fraction. Must be nonnegative.</param>
		// Token: 0x06000C5D RID: 3165 RVA: 0x0001C698 File Offset: 0x0001A898
		public EncoderParameter(Encoder encoder, int numerator, int denominator)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeRational;
			this._numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(2 * num);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this._parameterValue, numerator);
			Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, num), denominator);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and a pair of 64-bit integers. The pair of integers represents a range of integers, the first integer being the smallest number in the range, and the second integer being the largest number in the range. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeLongRange" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="rangebegin">A 64-bit integer that represents the smallest number in a range of integers. Must be nonnegative. This parameter is converted to a 32-bit integer before it is stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		/// <param name="rangeend">A 64-bit integer that represents the largest number in a range of integers. Must be nonnegative. This parameter is converted to a 32-bit integer before it is stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C5E RID: 3166 RVA: 0x0001C720 File Offset: 0x0001A920
		public EncoderParameter(Encoder encoder, long rangebegin, long rangeend)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeLongRange;
			this._numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(2 * num);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this._parameterValue, (int)rangebegin);
			Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, num), (int)rangeend);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and four, 32-bit integers. The four integers represent a range of fractions. The first two integers represent the smallest fraction in the range, and the remaining two integers represent the largest fraction in the range. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeRationalRange" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to 1.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="numerator1">A 32-bit integer that represents the numerator of the smallest fraction in the range. Must be nonnegative.</param>
		/// <param name="demoninator1">A 32-bit integer that represents the denominator of the smallest fraction in the range. Must be nonnegative.</param>
		/// <param name="numerator2">A 32-bit integer that represents the denominator of the smallest fraction in the range. Must be nonnegative.</param>
		/// <param name="demoninator2">A 32-bit integer that represents the numerator of the largest fraction in the range. Must be nonnegative.</param>
		// Token: 0x06000C5F RID: 3167 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public EncoderParameter(Encoder encoder, int numerator1, int demoninator1, int numerator2, int demoninator2)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeRationalRange;
			this._numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(4 * num);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this._parameterValue, numerator1);
			Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, num), demoninator1);
			Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, 2 * num), numerator2);
			Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, 3 * num), demoninator2);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and a character string. The string is converted to a null-terminated ASCII string before it is stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeAscii" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the length of the ASCII string including the NULL terminator.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">A <see cref="T:System.String" /> that specifies the value stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C60 RID: 3168 RVA: 0x0001C860 File Offset: 0x0001AA60
		public EncoderParameter(Encoder encoder, string value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeAscii;
			this._numberOfValues = value.Length;
			this._parameterValue = Marshal.StringToHGlobalAnsi(value);
			GC.KeepAlive(this);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and an array of unsigned 8-bit integers. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">An array of 8-bit unsigned integers that specifies the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C61 RID: 3169 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public EncoderParameter(Encoder encoder, byte[] value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeByte;
			this._numberOfValues = value.Length;
			this._parameterValue = Marshal.AllocHGlobal(this._numberOfValues);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this._parameterValue, this._numberOfValues);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and an array of bytes. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" /> or <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">An array of bytes that specifies the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		/// <param name="undefined">If <see langword="true" />, the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeUndefined" />; otherwise, the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property is set to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeByte" />.</param>
		// Token: 0x06000C62 RID: 3170 RVA: 0x0001C934 File Offset: 0x0001AB34
		public EncoderParameter(Encoder encoder, byte[] value, bool undefined)
		{
			this._parameterGuid = encoder.Guid;
			if (undefined)
			{
				this._parameterValueType = EncoderParameterValueType.ValueTypeUndefined;
			}
			else
			{
				this._parameterValueType = EncoderParameterValueType.ValueTypeByte;
			}
			this._numberOfValues = value.Length;
			this._parameterValue = Marshal.AllocHGlobal(this._numberOfValues);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this._parameterValue, this._numberOfValues);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and an array of 16-bit integers. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeShort" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">An array of 16-bit integers that specifies the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The integers in the array must be nonnegative.</param>
		// Token: 0x06000C63 RID: 3171 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public EncoderParameter(Encoder encoder, short[] value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeShort;
			this._numberOfValues = value.Length;
			int num = Marshal.SizeOf(typeof(short));
			this._parameterValue = Marshal.AllocHGlobal(checked(this._numberOfValues * num));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this._parameterValue, this._numberOfValues);
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and an array of 64-bit integers. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeLong" /> (32-bit), and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="value">An array of 64-bit integers that specifies the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The integers in the array must be nonnegative. The 64-bit integers are converted to 32-bit integers before they are stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		// Token: 0x06000C64 RID: 3172 RVA: 0x0001CA38 File Offset: 0x0001AC38
		public unsafe EncoderParameter(Encoder encoder, long[] value)
		{
			this._parameterGuid = encoder.Guid;
			this._parameterValueType = EncoderParameterValueType.ValueTypeLong;
			this._numberOfValues = value.Length;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(checked(this._numberOfValues * num));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			int* ptr = (int*)((void*)this._parameterValue);
			fixed (long[] array = value)
			{
				long* ptr2;
				if (value == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				for (int i = 0; i < value.Length; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
			}
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and two arrays of 32-bit integers. The two arrays represent an array of fractions. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeRational" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the <paramref name="numerator" /> array, which must be the same as the number of elements in the <paramref name="denominator" /> array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="numerator">An array of 32-bit integers that specifies the numerators of the fractions. The integers in the array must be nonnegative.</param>
		/// <param name="denominator">An array of 32-bit integers that specifies the denominators of the fractions. The integers in the array must be nonnegative. A denominator of a given index is paired with the numerator of the same index.</param>
		// Token: 0x06000C65 RID: 3173 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
		public EncoderParameter(Encoder encoder, int[] numerator, int[] denominator)
		{
			this._parameterGuid = encoder.Guid;
			if (numerator.Length != denominator.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this._parameterValueType = EncoderParameterValueType.ValueTypeRational;
			this._numberOfValues = numerator.Length;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(checked(this._numberOfValues * 2 * num));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this._numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(i * 2 * num, this._parameterValue), numerator[i]);
				Marshal.WriteInt32(EncoderParameter.Add((i * 2 + 1) * num, this._parameterValue), denominator[i]);
			}
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and two arrays of 64-bit integers. The two arrays represent an array integer ranges. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeLongRange" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the <paramref name="rangebegin" /> array, which must be the same as the number of elements in the <paramref name="rangeend" /> array.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="rangebegin">An array of 64-bit integers that specifies the minimum values for the integer ranges. The integers in the array must be nonnegative. The 64-bit integers are converted to 32-bit integers before they are stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</param>
		/// <param name="rangeend">An array of 64-bit integers that specifies the maximum values for the integer ranges. The integers in the array must be nonnegative. The 64-bit integers are converted to 32-bit integers before they are stored in the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object. A maximum value of a given index is paired with the minimum value of the same index.</param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public EncoderParameter(Encoder encoder, long[] rangebegin, long[] rangeend)
		{
			this._parameterGuid = encoder.Guid;
			if (rangebegin.Length != rangeend.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this._parameterValueType = EncoderParameterValueType.ValueTypeLongRange;
			this._numberOfValues = rangebegin.Length;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(checked(this._numberOfValues * 2 * num));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this._numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(i * 2 * num, this._parameterValue), (int)rangebegin[i]);
				Marshal.WriteInt32(EncoderParameter.Add((i * 2 + 1) * num, this._parameterValue), (int)rangeend[i]);
			}
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and four arrays of 32-bit integers. The four arrays represent an array rational ranges. A rational range is the set of all fractions from a minimum fractional value through a maximum fractional value. Sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> property to <see cref="F:System.Drawing.Imaging.EncoderParameterValueType.ValueTypeRationalRange" />, and sets the <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property to the number of elements in the <paramref name="numerator1" /> array, which must be the same as the number of elements in the other three arrays.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="numerator1">An array of 32-bit integers that specifies the numerators of the minimum values for the ranges. The integers in the array must be nonnegative.</param>
		/// <param name="denominator1">An array of 32-bit integers that specifies the denominators of the minimum values for the ranges. The integers in the array must be nonnegative.</param>
		/// <param name="numerator2">An array of 32-bit integers that specifies the numerators of the maximum values for the ranges. The integers in the array must be nonnegative.</param>
		/// <param name="denominator2">An array of 32-bit integers that specifies the denominators of the maximum values for the ranges. The integers in the array must be nonnegative.</param>
		// Token: 0x06000C67 RID: 3175 RVA: 0x0001CC84 File Offset: 0x0001AE84
		public EncoderParameter(Encoder encoder, int[] numerator1, int[] denominator1, int[] numerator2, int[] denominator2)
		{
			this._parameterGuid = encoder.Guid;
			if (numerator1.Length != denominator1.Length || numerator1.Length != denominator2.Length || denominator1.Length != denominator2.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this._parameterValueType = EncoderParameterValueType.ValueTypeRationalRange;
			this._numberOfValues = numerator1.Length;
			int num = Marshal.SizeOf(typeof(int));
			this._parameterValue = Marshal.AllocHGlobal(checked(this._numberOfValues * 4 * num));
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this._numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, 4 * i * num), numerator1[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, (4 * i + 1) * num), denominator1[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, (4 * i + 2) * num), numerator2[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this._parameterValue, (4 * i + 3) * num), denominator2[i]);
			}
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object and three integers that specify the number of values, the data type of the values, and a pointer to the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="NumberOfValues">An integer that specifies the number of values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property is set to this value.</param>
		/// <param name="Type">A member of the <see cref="T:System.Drawing.Imaging.EncoderParameterValueType" /> enumeration that specifies the data type of the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The <see cref="T:System.Type" /> and <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> properties are set to this value.</param>
		/// <param name="Value">A pointer to an array of values of the type specified by the <paramref name="Type" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">Type is not a valid <see cref="T:System.Drawing.Imaging.EncoderParameterValueType" />.</exception>
		// Token: 0x06000C68 RID: 3176 RVA: 0x0001CD94 File Offset: 0x0001AF94
		[Obsolete("This constructor has been deprecated. Use EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public EncoderParameter(Encoder encoder, int NumberOfValues, int Type, int Value)
		{
			int num;
			switch (Type)
			{
			case 1:
			case 2:
				num = 1;
				break;
			case 3:
				num = 2;
				break;
			case 4:
				num = 4;
				break;
			case 5:
			case 6:
				num = 8;
				break;
			case 7:
				num = 1;
				break;
			case 8:
				num = 16;
				break;
			default:
				throw SafeNativeMethods.Gdip.StatusException(8);
			}
			int num2 = checked(num * NumberOfValues);
			this._parameterValue = Marshal.AllocHGlobal(num2);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.WriteByte(EncoderParameter.Add(this._parameterValue, i), Marshal.ReadByte((IntPtr)(Value + i)));
			}
			this._parameterValueType = (EncoderParameterValueType)Type;
			this._numberOfValues = NumberOfValues;
			this._parameterGuid = encoder.Guid;
			GC.KeepAlive(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> class with the specified <see cref="T:System.Drawing.Imaging.Encoder" /> object, number of values, data type of the values, and a pointer to the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		/// <param name="encoder">An <see cref="T:System.Drawing.Imaging.Encoder" /> object that encapsulates the globally unique identifier of the parameter category.</param>
		/// <param name="numberValues">An integer that specifies the number of values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The <see cref="P:System.Drawing.Imaging.EncoderParameter.NumberOfValues" /> property is set to this value.</param>
		/// <param name="type">A member of the <see cref="T:System.Drawing.Imaging.EncoderParameterValueType" /> enumeration that specifies the data type of the values stored in the <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object. The <see cref="T:System.Type" /> and <see cref="P:System.Drawing.Imaging.EncoderParameter.ValueType" /> properties are set to this value.</param>
		/// <param name="value">A pointer to an array of values of the type specified by the <paramref name="Type" /> parameter.</param>
		// Token: 0x06000C69 RID: 3177 RVA: 0x0001CE68 File Offset: 0x0001B068
		public EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value)
		{
			int num;
			switch (type)
			{
			case EncoderParameterValueType.ValueTypeByte:
			case EncoderParameterValueType.ValueTypeAscii:
				num = 1;
				break;
			case EncoderParameterValueType.ValueTypeShort:
				num = 2;
				break;
			case EncoderParameterValueType.ValueTypeLong:
				num = 4;
				break;
			case EncoderParameterValueType.ValueTypeRational:
			case EncoderParameterValueType.ValueTypeLongRange:
				num = 8;
				break;
			case EncoderParameterValueType.ValueTypeUndefined:
				num = 1;
				break;
			case EncoderParameterValueType.ValueTypeRationalRange:
				num = 16;
				break;
			default:
				throw SafeNativeMethods.Gdip.StatusException(8);
			}
			int num2 = checked(num * numberValues);
			this._parameterValue = Marshal.AllocHGlobal(num2);
			if (this._parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.WriteByte(EncoderParameter.Add(this._parameterValue, i), Marshal.ReadByte(value + i));
			}
			this._parameterValueType = type;
			this._numberOfValues = numberValues;
			this._parameterGuid = encoder.Guid;
			GC.KeepAlive(this);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0001CF38 File Offset: 0x0001B138
		private static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001CF48 File Offset: 0x0001B148
		private static IntPtr Add(int a, IntPtr b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x0400097F RID: 2431
		[MarshalAs(UnmanagedType.Struct)]
		private Guid _parameterGuid;

		// Token: 0x04000980 RID: 2432
		private int _numberOfValues;

		// Token: 0x04000981 RID: 2433
		private EncoderParameterValueType _parameterValueType;

		// Token: 0x04000982 RID: 2434
		private IntPtr _parameterValue;
	}
}
