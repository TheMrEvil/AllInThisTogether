using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017B RID: 379
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_FileOpenDialog_t : ICallbackData
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00016AB1 File Offset: 0x00014CB1
		public int DataSize
		{
			get
			{
				return HTML_FileOpenDialog_t._datasize;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00016AB8 File Offset: 0x00014CB8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_FileOpenDialog;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00016ABF File Offset: 0x00014CBF
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_FileOpenDialog_t()
		{
		}

		// Token: 0x04000A50 RID: 2640
		internal uint UnBrowserHandle;

		// Token: 0x04000A51 RID: 2641
		internal string PchTitle;

		// Token: 0x04000A52 RID: 2642
		internal string PchInitialFile;

		// Token: 0x04000A53 RID: 2643
		public static int _datasize = Marshal.SizeOf(typeof(HTML_FileOpenDialog_t));
	}
}
