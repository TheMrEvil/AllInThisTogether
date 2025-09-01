using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows the use of declarative security actions to determine host protection requirements. This class cannot be inherited.</summary>
	// Token: 0x0200043D RID: 1085
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with default values.</summary>
		// Token: 0x06002C0D RID: 11277 RVA: 0x0009ECAC File Offset: 0x0009CEAC
		public HostProtectionAttribute() : base(SecurityAction.LinkDemand)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />.</exception>
		// Token: 0x06002C0E RID: 11278 RVA: 0x0009ECB5 File Offset: 0x0009CEB5
		public HostProtectionAttribute(SecurityAction action) : base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Only {0} is accepted."), SecurityAction.LinkDemand), "action");
			}
		}

		/// <summary>Gets or sets a value indicating whether external process management is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if external process management is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x0009ECE2 File Offset: 0x0009CEE2
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x0009ECEF File Offset: 0x0009CEEF
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalProcessMgmt;
			}
		}

		/// <summary>Gets or sets a value indicating whether external threading is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if external threading is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x0009ED12 File Offset: 0x0009CF12
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x0009ED20 File Offset: 0x0009CF20
		public bool ExternalThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalThreading;
			}
		}

		/// <summary>Gets or sets a value indicating whether resources might leak memory if the operation is terminated.</summary>
		/// <returns>
		///   <see langword="true" /> if resources might leak memory on termination; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x0009ED44 File Offset: 0x0009CF44
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x0009ED55 File Offset: 0x0009CF55
		public bool MayLeakOnAbort
		{
			get
			{
				return (this._resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.MayLeakOnAbort;
					return;
				}
				this._resources &= ~HostProtectionResource.MayLeakOnAbort;
			}
		}

		/// <summary>Gets or sets a value indicating whether the security infrastructure is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if the security infrastructure is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x0009ED7F File Offset: 0x0009CF7F
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x0009ED8D File Offset: 0x0009CF8D
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this._resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SecurityInfrastructure;
					return;
				}
				this._resources &= ~HostProtectionResource.SecurityInfrastructure;
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting process management is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if self-affecting process management is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x0009EDB1 File Offset: 0x0009CFB1
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x0009EDBE File Offset: 0x0009CFBE
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingProcessMgmt;
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting threading is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if self-affecting threading is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x0009EDE1 File Offset: 0x0009CFE1
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x0009EDEF File Offset: 0x0009CFEF
		public bool SelfAffectingThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingThreading;
			}
		}

		/// <summary>Gets or sets a value indicating whether shared state is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if shared state is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0009EE13 File Offset: 0x0009D013
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x0009EE20 File Offset: 0x0009D020
		public bool SharedState
		{
			get
			{
				return (this._resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SharedState;
					return;
				}
				this._resources &= ~HostProtectionResource.SharedState;
			}
		}

		/// <summary>Gets or sets a value indicating whether synchronization is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if synchronization is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x0009EE43 File Offset: 0x0009D043
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x0009EE50 File Offset: 0x0009D050
		public bool Synchronization
		{
			get
			{
				return (this._resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.Synchronization;
					return;
				}
				this._resources &= ~HostProtectionResource.Synchronization;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user interface is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if the user interface is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0009EE73 File Offset: 0x0009D073
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0009EE84 File Offset: 0x0009D084
		public bool UI
		{
			get
			{
				return (this._resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.UI;
					return;
				}
				this._resources &= ~HostProtectionResource.UI;
			}
		}

		/// <summary>Gets or sets flags specifying categories of functionality that are potentially harmful to the host.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0009EEAE File Offset: 0x0009D0AE
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x0009EEB6 File Offset: 0x0009D0B6
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		/// <summary>Creates and returns a new host protection permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that corresponds to the current attribute.</returns>
		// Token: 0x06002C23 RID: 11299 RVA: 0x0009EEBF File Offset: 0x0009D0BF
		public override IPermission CreatePermission()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x0400202D RID: 8237
		private HostProtectionResource _resources;
	}
}
