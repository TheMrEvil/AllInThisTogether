using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Prepares performance data for the performance.dll the system loads when working with performance counters.</summary>
	// Token: 0x02000275 RID: 629
	[Obsolete("use PerformanceCounter")]
	[MonoTODO("not implemented")]
	[Guid("82840be1-d273-11d2-b94a-00600893b17a")]
	[ComVisible(true)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class PerformanceCounterManager : ICollectData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterManager" /> class.</summary>
		// Token: 0x06001414 RID: 5140 RVA: 0x0000219B File Offset: 0x0000039B
		[Obsolete("use PerformanceCounter")]
		public PerformanceCounterManager()
		{
		}

		/// <summary>Called by the perf dll's close performance data</summary>
		// Token: 0x06001415 RID: 5141 RVA: 0x0000829A File Offset: 0x0000649A
		void ICollectData.CloseData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Performance data collection routine. Called by the PerfCount perf dll.</summary>
		/// <param name="callIdx">The call index.</param>
		/// <param name="valueNamePtr">A pointer to a Unicode string list with the requested Object identifiers.</param>
		/// <param name="dataPtr">A pointer to the data buffer.</param>
		/// <param name="totalBytes">A pointer to a number of bytes.</param>
		/// <param name="res">When this method returns, contains a <see cref="T:System.IntPtr" /> with a value of -1.</param>
		// Token: 0x06001416 RID: 5142 RVA: 0x0000829A File Offset: 0x0000649A
		void ICollectData.CollectData(int callIdx, IntPtr valueNamePtr, IntPtr dataPtr, int totalBytes, out IntPtr res)
		{
			throw new NotImplementedException();
		}
	}
}
