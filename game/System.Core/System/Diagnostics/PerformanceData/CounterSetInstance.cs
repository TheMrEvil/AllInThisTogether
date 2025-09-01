using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Creates an instance of the logical counters defined in the <see cref="T:System.Diagnostics.PerformanceData.CounterSet" /> class.</summary>
	// Token: 0x02000391 RID: 913
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstance : IDisposable
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x0000235B File Offset: 0x0000055B
		internal CounterSetInstance()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the collection of counter data for the counter set instance.</summary>
		/// <returns>A collection of the counter data contained in the counter set instance.</returns>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0005A05A File Offset: 0x0005825A
		public CounterSetInstanceCounterDataSet Counters
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases all unmanaged resources used by this object.</summary>
		// Token: 0x06001B4B RID: 6987 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
