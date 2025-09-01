using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Controls the.NET remoting lifetime services.</summary>
	// Token: 0x0200058A RID: 1418
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x0600377E RID: 14206 RVA: 0x000C802C File Offset: 0x000C622C
		static LifetimeServices()
		{
			LifetimeServices._leaseManagerPollTime = TimeSpan.FromSeconds(10.0);
			LifetimeServices._leaseTime = TimeSpan.FromMinutes(5.0);
			LifetimeServices._renewOnCallTime = TimeSpan.FromMinutes(2.0);
			LifetimeServices._sponsorshipTimeout = TimeSpan.FromMinutes(2.0);
		}

		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Lifetime.LifetimeServices" />.</summary>
		// Token: 0x0600377F RID: 14207 RVA: 0x0000259F File Offset: 0x0000079F
		[Obsolete("Call the static methods directly on this type instead", true)]
		public LifetimeServices()
		{
		}

		/// <summary>Gets or sets the time interval between each activation of the lease manager to clean up expired leases.</summary>
		/// <returns>The default amount of time the lease manager sleeps after checking for expired leases.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000C808F File Offset: 0x000C628F
		// (set) Token: 0x06003781 RID: 14209 RVA: 0x000C8096 File Offset: 0x000C6296
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices._leaseManagerPollTime;
			}
			set
			{
				LifetimeServices._leaseManagerPollTime = value;
				LifetimeServices._leaseManager.SetPollTime(value);
			}
		}

		/// <summary>Gets or sets the initial lease time span for an <see cref="T:System.AppDomain" />.</summary>
		/// <returns>The initial lease <see cref="T:System.TimeSpan" /> for objects that can have leases in the <see cref="T:System.AppDomain" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000C80A9 File Offset: 0x000C62A9
		// (set) Token: 0x06003783 RID: 14211 RVA: 0x000C80B0 File Offset: 0x000C62B0
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices._leaseTime;
			}
			set
			{
				LifetimeServices._leaseTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time by which the lease is extended every time a call comes in on the server object.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which a lifetime lease in the current <see cref="T:System.AppDomain" /> is extended after each call.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000C80B8 File Offset: 0x000C62B8
		// (set) Token: 0x06003785 RID: 14213 RVA: 0x000C80BF File Offset: 0x000C62BF
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices._renewOnCallTime;
			}
			set
			{
				LifetimeServices._renewOnCallTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time the lease manager waits for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The initial sponsorship time-out.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x000C80C7 File Offset: 0x000C62C7
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x000C80CE File Offset: 0x000C62CE
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices._sponsorshipTimeout;
			}
			set
			{
				LifetimeServices._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000C80D6 File Offset: 0x000C62D6
		internal static void TrackLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.TrackLifetime(identity);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000C80E3 File Offset: 0x000C62E3
		internal static void StopTrackingLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.StopTrackingLifetime(identity);
		}

		// Token: 0x0400259A RID: 9626
		private static TimeSpan _leaseManagerPollTime;

		// Token: 0x0400259B RID: 9627
		private static TimeSpan _leaseTime;

		// Token: 0x0400259C RID: 9628
		private static TimeSpan _renewOnCallTime;

		// Token: 0x0400259D RID: 9629
		private static TimeSpan _sponsorshipTimeout;

		// Token: 0x0400259E RID: 9630
		private static LeaseManager _leaseManager = new LeaseManager();
	}
}
