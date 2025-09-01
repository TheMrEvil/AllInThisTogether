using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
	// Token: 0x02000104 RID: 260
	public sealed class EncoderParameters : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain the specified number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <param name="count">An integer that specifies the number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects that the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object can contain.</param>
		// Token: 0x06000C6C RID: 3180 RVA: 0x0001CF58 File Offset: 0x0001B158
		public EncoderParameters(int count)
		{
			this._param = new EncoderParameter[count];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain one <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		// Token: 0x06000C6D RID: 3181 RVA: 0x0001CF6C File Offset: 0x0001B16C
		public EncoderParameters()
		{
			this._param = new EncoderParameter[1];
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001CF80 File Offset: 0x0001B180
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0001CF88 File Offset: 0x0001B188
		public EncoderParameter[] Param
		{
			get
			{
				return this._param;
			}
			set
			{
				this._param = value;
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0001CF94 File Offset: 0x0001B194
		internal IntPtr ConvertToMemory()
		{
			int num = Marshal.SizeOf(typeof(EncoderParameter));
			int num2 = this._param.Length;
			IntPtr intPtr;
			long num3;
			checked
			{
				intPtr = Marshal.AllocHGlobal(num2 * num + Marshal.SizeOf(typeof(IntPtr)));
				if (intPtr == IntPtr.Zero)
				{
					throw SafeNativeMethods.Gdip.StatusException(3);
				}
				Marshal.WriteIntPtr(intPtr, (IntPtr)num2);
				num3 = (long)intPtr + unchecked((long)Marshal.SizeOf(typeof(IntPtr)));
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.StructureToPtr<EncoderParameter>(this._param[i], (IntPtr)(num3 + (long)(i * num)), false);
			}
			return intPtr;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001D038 File Offset: 0x0001B238
		internal static EncoderParameters ConvertFromMemory(IntPtr memory)
		{
			if (memory == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = Marshal.ReadIntPtr(memory).ToInt32();
			EncoderParameters encoderParameters = new EncoderParameters(num);
			int num2 = Marshal.SizeOf(typeof(EncoderParameter));
			long num3 = (long)memory + (long)Marshal.SizeOf(typeof(IntPtr));
			for (int i = 0; i < num; i++)
			{
				Guid guid = (Guid)Marshal.PtrToStructure((IntPtr)((long)(i * num2) + num3), typeof(Guid));
				int numberValues = Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 16L));
				EncoderParameterValueType type = (EncoderParameterValueType)Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 20L));
				IntPtr value = Marshal.ReadIntPtr((IntPtr)((long)(i * num2) + num3 + 24L));
				encoderParameters._param[i] = new EncoderParameter(new Encoder(guid), numberValues, type, value);
			}
			return encoderParameters;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object.</summary>
		// Token: 0x06000C72 RID: 3186 RVA: 0x0001D134 File Offset: 0x0001B334
		public void Dispose()
		{
			foreach (EncoderParameter encoderParameter in this._param)
			{
				if (encoderParameter != null)
				{
					encoderParameter.Dispose();
				}
			}
			this._param = null;
		}

		// Token: 0x0400098C RID: 2444
		private EncoderParameter[] _param;
	}
}
