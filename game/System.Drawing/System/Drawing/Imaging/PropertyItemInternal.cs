using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000116 RID: 278
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class PropertyItemInternal : IDisposable
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		internal PropertyItemInternal()
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		~PropertyItemInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0001DE10 File Offset: 0x0001C010
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0001DE19 File Offset: 0x0001C019
		private void Dispose(bool disposing)
		{
			if (this.value != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.value);
				this.value = IntPtr.Zero;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0001DE4C File Offset: 0x0001C04C
		internal static PropertyItemInternal ConvertFromPropertyItem(PropertyItem propItem)
		{
			PropertyItemInternal propertyItemInternal = new PropertyItemInternal();
			propertyItemInternal.id = propItem.Id;
			propertyItemInternal.len = 0;
			propertyItemInternal.type = propItem.Type;
			byte[] array = propItem.Value;
			if (array != null)
			{
				int num = array.Length;
				propertyItemInternal.len = num;
				propertyItemInternal.value = Marshal.AllocHGlobal(num);
				Marshal.Copy(array, 0, propertyItemInternal.value, num);
			}
			return propertyItemInternal;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		internal static PropertyItem[] ConvertFromMemory(IntPtr propdata, int count)
		{
			PropertyItem[] array = new PropertyItem[count];
			for (int i = 0; i < count; i++)
			{
				PropertyItemInternal propertyItemInternal = null;
				try
				{
					propertyItemInternal = (PropertyItemInternal)Marshal.PtrToStructure(propdata, typeof(PropertyItemInternal));
					array[i] = new PropertyItem();
					array[i].Id = propertyItemInternal.id;
					array[i].Len = propertyItemInternal.len;
					array[i].Type = propertyItemInternal.type;
					array[i].Value = propertyItemInternal.Value;
					propertyItemInternal.value = IntPtr.Zero;
				}
				finally
				{
					if (propertyItemInternal != null)
					{
						propertyItemInternal.Dispose();
					}
				}
				propdata = (IntPtr)((long)propdata + (long)Marshal.SizeOf(typeof(PropertyItemInternal)));
			}
			return array;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0001DF74 File Offset: 0x0001C174
		public byte[] Value
		{
			get
			{
				if (this.len == 0)
				{
					return null;
				}
				byte[] array = new byte[this.len];
				Marshal.Copy(this.value, array, 0, this.len);
				return array;
			}
		}

		// Token: 0x04000A44 RID: 2628
		public int id;

		// Token: 0x04000A45 RID: 2629
		public int len;

		// Token: 0x04000A46 RID: 2630
		public short type;

		// Token: 0x04000A47 RID: 2631
		public IntPtr value = IntPtr.Zero;
	}
}
