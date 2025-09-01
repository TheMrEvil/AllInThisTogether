using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Provides a default implementation for a lifetime sponsor class.</summary>
	// Token: 0x02000582 RID: 1410
	[ComVisible(true)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with default values.</summary>
		// Token: 0x06003746 RID: 14150 RVA: 0x000C7830 File Offset: 0x000C5A30
		public ClientSponsor()
		{
			this.renewal_time = new TimeSpan(0, 2, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with the renewal time of the sponsored object.</summary>
		/// <param name="renewalTime">The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</param>
		// Token: 0x06003747 RID: 14151 RVA: 0x000C7851 File Offset: 0x000C5A51
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.renewal_time = renewalTime;
		}

		/// <summary>Gets or sets the <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000C786B File Offset: 0x000C5A6B
		// (set) Token: 0x06003749 RID: 14153 RVA: 0x000C7873 File Offset: 0x000C5A73
		public TimeSpan RenewalTime
		{
			get
			{
				return this.renewal_time;
			}
			set
			{
				this.renewal_time = value;
			}
		}

		/// <summary>Empties the list objects registered with the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		// Token: 0x0600374A RID: 14154 RVA: 0x000C787C File Offset: 0x000C5A7C
		public void Close()
		{
			foreach (object obj in this.registered_objects.Values)
			{
				(((MarshalByRefObject)obj).GetLifetimeService() as ILease).Unregister(this);
			}
			this.registered_objects.Clear();
		}

		/// <summary>Frees the resources of the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> before the garbage collector reclaims them.</summary>
		// Token: 0x0600374B RID: 14155 RVA: 0x000C78F0 File Offset: 0x000C5AF0
		~ClientSponsor()
		{
			this.Close();
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, providing a lease for the current object.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> for the current object.</returns>
		// Token: 0x0600374C RID: 14156 RVA: 0x000C2AD4 File Offset: 0x000C0CD4
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		/// <summary>Registers the specified <see cref="T:System.MarshalByRefObject" /> for sponsorship.</summary>
		/// <param name="obj">The object to register for sponsorship with the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</param>
		/// <returns>
		///   <see langword="true" /> if registration succeeded; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600374D RID: 14157 RVA: 0x000C791C File Offset: 0x000C5B1C
		public bool Register(MarshalByRefObject obj)
		{
			if (this.registered_objects.ContainsKey(obj))
			{
				return false;
			}
			ILease lease = obj.GetLifetimeService() as ILease;
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			this.registered_objects.Add(obj, obj);
			return true;
		}

		/// <summary>Requests a sponsoring client to renew the lease for the specified object.</summary>
		/// <param name="lease">The lifetime lease of the object that requires lease renewal.</param>
		/// <returns>The additional lease time for the specified object.</returns>
		// Token: 0x0600374E RID: 14158 RVA: 0x000C786B File Offset: 0x000C5A6B
		[SecurityCritical]
		public TimeSpan Renewal(ILease lease)
		{
			return this.renewal_time;
		}

		/// <summary>Unregisters the specified <see cref="T:System.MarshalByRefObject" /> from the list of objects sponsored by the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <param name="obj">The object to unregister.</param>
		// Token: 0x0600374F RID: 14159 RVA: 0x000C795F File Offset: 0x000C5B5F
		public void Unregister(MarshalByRefObject obj)
		{
			if (!this.registered_objects.ContainsKey(obj))
			{
				return;
			}
			(obj.GetLifetimeService() as ILease).Unregister(this);
			this.registered_objects.Remove(obj);
		}

		// Token: 0x04002587 RID: 9607
		private TimeSpan renewal_time;

		// Token: 0x04002588 RID: 9608
		private Hashtable registered_objects = new Hashtable();
	}
}
