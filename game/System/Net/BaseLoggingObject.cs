using System;

namespace System.Net
{
	// Token: 0x0200062E RID: 1582
	internal class BaseLoggingObject
	{
		// Token: 0x060031F5 RID: 12789 RVA: 0x0000219B File Offset: 0x0000039B
		internal BaseLoggingObject()
		{
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush()
		{
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
