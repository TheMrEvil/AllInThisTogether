using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_NeedsPaint_t : ICallbackData
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000168B9 File Offset: 0x00014AB9
		public int DataSize
		{
			get
			{
				return HTML_NeedsPaint_t._datasize;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000168C0 File Offset: 0x00014AC0
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_NeedsPaint;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000168C7 File Offset: 0x00014AC7
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_NeedsPaint_t()
		{
		}

		// Token: 0x04000A07 RID: 2567
		internal uint UnBrowserHandle;

		// Token: 0x04000A08 RID: 2568
		internal string PBGRA;

		// Token: 0x04000A09 RID: 2569
		internal uint UnWide;

		// Token: 0x04000A0A RID: 2570
		internal uint UnTall;

		// Token: 0x04000A0B RID: 2571
		internal uint UnUpdateX;

		// Token: 0x04000A0C RID: 2572
		internal uint UnUpdateY;

		// Token: 0x04000A0D RID: 2573
		internal uint UnUpdateWide;

		// Token: 0x04000A0E RID: 2574
		internal uint UnUpdateTall;

		// Token: 0x04000A0F RID: 2575
		internal uint UnScrollX;

		// Token: 0x04000A10 RID: 2576
		internal uint UnScrollY;

		// Token: 0x04000A11 RID: 2577
		internal float FlPageScale;

		// Token: 0x04000A12 RID: 2578
		internal uint UnPageSerial;

		// Token: 0x04000A13 RID: 2579
		public static int _datasize = Marshal.SizeOf(typeof(HTML_NeedsPaint_t));
	}
}
