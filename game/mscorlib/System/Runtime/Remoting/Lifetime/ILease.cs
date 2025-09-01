using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Defines a lifetime lease object that is used by the remoting lifetime service.</summary>
	// Token: 0x02000583 RID: 1411
	[ComVisible(true)]
	public interface ILease
	{
		/// <summary>Gets the amount of time remaining on the lease.</summary>
		/// <returns>The amount of time remaining on the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06003750 RID: 14160
		TimeSpan CurrentLeaseTime { get; }

		/// <summary>Gets the current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</summary>
		/// <returns>The current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06003751 RID: 14161
		LeaseState CurrentState { get; }

		/// <summary>Gets or sets the initial time for the lease.</summary>
		/// <returns>The initial time for the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06003752 RID: 14162
		// (set) Token: 0x06003753 RID: 14163
		TimeSpan InitialLeaseTime { get; set; }

		/// <summary>Gets or sets the amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</summary>
		/// <returns>The amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06003754 RID: 14164
		// (set) Token: 0x06003755 RID: 14165
		TimeSpan RenewOnCallTime { get; set; }

		/// <summary>Gets or sets the amount of time to wait for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The amount of time to wait for a sponsor to return with a lease renewal time.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06003756 RID: 14166
		// (set) Token: 0x06003757 RID: 14167
		TimeSpan SponsorshipTimeout { get; set; }

		/// <summary>Registers a sponsor for the lease without renewing the lease.</summary>
		/// <param name="obj">The callback object of the sponsor.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003758 RID: 14168
		void Register(ISponsor obj);

		/// <summary>Registers a sponsor for the lease, and renews it by the specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="obj">The callback object of the sponsor.</param>
		/// <param name="renewalTime">The length of time to renew the lease by.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003759 RID: 14169
		void Register(ISponsor obj, TimeSpan renewalTime);

		/// <summary>Renews a lease for the specified time.</summary>
		/// <param name="renewalTime">The length of time to renew the lease by.</param>
		/// <returns>The new expiration time of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x0600375A RID: 14170
		TimeSpan Renew(TimeSpan renewalTime);

		/// <summary>Removes a sponsor from the sponsor list.</summary>
		/// <param name="obj">The lease sponsor to unregister.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x0600375B RID: 14171
		void Unregister(ISponsor obj);
	}
}
