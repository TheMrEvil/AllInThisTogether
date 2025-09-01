using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an array of strings that represent XPath queries for elements in the XML representation of an event, which is based on the Event Schema. The queries in this object are used to extract values from the event.</summary>
	// Token: 0x020003A6 RID: 934
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogPropertySelector : IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogPropertySelector" /> class instance.</summary>
		/// <param name="propertyQueries">XPath queries used to extract values from the XML representation of the event.</param>
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public EventLogPropertySelector(IEnumerable<string> propertyQueries)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001BC3 RID: 7107 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001BC4 RID: 7108 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
