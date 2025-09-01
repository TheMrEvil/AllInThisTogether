using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StorePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000299 RID: 665
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06001506 RID: 5382 RVA: 0x00055551 File Offset: 0x00053751
		public StorePermissionAttribute(SecurityAction action) : base(action)
		{
			this._flags = StorePermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the store permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.StorePermissionFlags.NoFlags" />.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00055561 File Offset: 0x00053761
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00055569 File Offset: 0x00053769
		public StorePermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & StorePermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "StorePermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to add to a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to add to a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0005559C File Offset: 0x0005379C
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x000555AA File Offset: 0x000537AA
		public bool AddToStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.AddToStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.AddToStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.AddToStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to create a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to create a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000555CE File Offset: 0x000537CE
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x000555DB File Offset: 0x000537DB
		public bool CreateStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.CreateStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.CreateStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.CreateStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to delete a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to delete a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x000555FE File Offset: 0x000537FE
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0005560B File Offset: 0x0005380B
		public bool DeleteStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.DeleteStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.DeleteStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.DeleteStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate the certificates in a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to enumerate certificates is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0005562E File Offset: 0x0005382E
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x0005563F File Offset: 0x0005383F
		public bool EnumerateCertificates
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateCertificates) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateCertificates;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateCertificates;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate stores.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to enumerate stores is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00055669 File Offset: 0x00053869
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00055676 File Offset: 0x00053876
		public bool EnumerateStores
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateStores) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateStores;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateStores;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to open a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to open a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00055699 File Offset: 0x00053899
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000556A7 File Offset: 0x000538A7
		public bool OpenStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.OpenStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.OpenStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.OpenStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to remove a certificate from a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to remove a certificate from a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000556CB File Offset: 0x000538CB
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x000556D9 File Offset: 0x000538D9
		public bool RemoveFromStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.RemoveFromStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.RemoveFromStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.RemoveFromStore;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StorePermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StorePermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06001517 RID: 5399 RVA: 0x00055700 File Offset: 0x00053900
		public override IPermission CreatePermission()
		{
			StorePermission result;
			if (base.Unrestricted)
			{
				result = new StorePermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new StorePermission(this._flags);
			}
			return result;
		}

		// Token: 0x04000BC5 RID: 3013
		private StorePermissionFlags _flags;
	}
}
