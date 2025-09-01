using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Contains the collection of counter values.</summary>
	// Token: 0x02000392 RID: 914
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstanceCounterDataSet : IDisposable
	{
		// Token: 0x06001B4C RID: 6988 RVA: 0x0000235B File Offset: 0x0000055B
		internal CounterSetInstanceCounterDataSet()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0005A05A File Offset: 0x0005825A
		public CounterData get_Item(int counterId)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Accesses a counter value in the collection by using the specified counter name.</summary>
		/// <param name="counterName">Name of the counter. This is the name that you used when you added the counter to the counter set.</param>
		/// <returns>The counter data.</returns>
		// Token: 0x170004CE RID: 1230
		public CounterData this[string counterName]
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases all unmanaged resources used by this object.</summary>
		// Token: 0x06001B4F RID: 6991 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
