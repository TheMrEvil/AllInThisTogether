using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents the abstract base class from which all isolated storage implementations must derive.</summary>
	// Token: 0x02000B71 RID: 2929
	[ComVisible(true)]
	public abstract class IsolatedStorage : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> class.</summary>
		// Token: 0x06006A78 RID: 27256 RVA: 0x00053949 File Offset: 0x00051B49
		protected IsolatedStorage()
		{
		}

		/// <summary>Gets an application identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the application <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />.</exception>
		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06006A79 RID: 27257 RVA: 0x0016C81C File Offset: 0x0016AA1C
		[ComVisible(false)]
		[MonoTODO("Does not currently use the manifest support")]
		public object ApplicationIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Application) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._applicationIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				throw new NotImplementedException(Locale.GetText("CAS related"));
			}
		}

		/// <summary>Gets an assembly identity used to scope isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="T:System.Reflection.Assembly" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object.</exception>
		/// <exception cref="T:System.InvalidOperationException">The assembly is not defined.</exception>
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06006A7A RID: 27258 RVA: 0x0016C86B File Offset: 0x0016AA6B
		public object AssemblyIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Assembly) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._assemblyIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._assemblyIdentity;
			}
		}

		/// <summary>Gets a value representing the current size of isolated storage.</summary>
		/// <returns>The number of storage units currently used within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current size of the isolated store is undefined.</exception>
		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0016C8A5 File Offset: 0x0016AAA5
		[Obsolete]
		[CLSCompliant(false)]
		public virtual ulong CurrentSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset CurrentSize."));
			}
		}

		/// <summary>Gets a domain identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the domain <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />.</exception>
		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x0016C8B6 File Offset: 0x0016AAB6
		public object DomainIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Domain) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._domainIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._domainIdentity;
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage. When overridden in a derived class, this value can take different units of measure.</summary>
		/// <returns>The maximum amount of isolated storage space in bytes. Derived classes can return different units of value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The quota has not been defined.</exception>
		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06006A7D RID: 27261 RVA: 0x0016C8F0 File Offset: 0x0016AAF0
		[Obsolete]
		[CLSCompliant(false)]
		public virtual ulong MaximumSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset MaximumSize."));
			}
		}

		/// <summary>Gets an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> enumeration value specifying the scope used to isolate the store.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values specifying the scope used to isolate the store.</returns>
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06006A7E RID: 27262 RVA: 0x0016C901 File Offset: 0x0016AB01
		public IsolatedStorageScope Scope
		{
			get
			{
				return this.storage_scope;
			}
		}

		/// <summary>When overridden in a derived class, gets the available free space for isolated storage, in bytes.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06006A7F RID: 27263 RVA: 0x0016C909 File Offset: 0x0016AB09
		[ComVisible(false)]
		public virtual long AvailableFreeSpace
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06006A80 RID: 27264 RVA: 0x0016C909 File Offset: 0x0016AB09
		[ComVisible(false)]
		public virtual long Quota
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used amount of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06006A81 RID: 27265 RVA: 0x0016C909 File Offset: 0x0016AB09
		[ComVisible(false)]
		public virtual long UsedSize
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		/// <summary>Gets a backslash character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '\' (backslash) character.</returns>
		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x0016C915 File Offset: 0x0016AB15
		protected virtual char SeparatorExternal
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		/// <summary>Gets a period character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '.' (period) character.</returns>
		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06006A83 RID: 27267 RVA: 0x0016C91C File Offset: 0x0016AB1C
		protected virtual char SeparatorInternal
		{
			get
			{
				return '.';
			}
		}

		/// <summary>When implemented by a derived class, returns a permission that represents access to isolated storage from within a permission set.</summary>
		/// <param name="ps">The <see cref="T:System.Security.PermissionSet" /> object that contains the set of permissions granted to code attempting to use isolated storage.</param>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> object.</returns>
		// Token: 0x06006A84 RID: 27268 RVA: 0x0000AF5E File Offset: 0x0000915E
		protected virtual IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			return null;
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <param name="domainEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <param name="assemblyEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the assembly of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores.</exception>
		// Token: 0x06006A85 RID: 27269 RVA: 0x0016C920 File Offset: 0x0016AB20
		protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
			{
				throw new NotImplementedException(scope.ToString());
			}
			throw new ArgumentException(scope.ToString());
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <param name="appEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> for the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores.</exception>
		// Token: 0x06006A86 RID: 27270 RVA: 0x0016C94F File Offset: 0x0016AB4F
		[MonoTODO("requires manifest support")]
		protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
		{
			if (AppDomain.CurrentDomain.ApplicationIdentity == null)
			{
				throw new IsolatedStorageException(Locale.GetText("No ApplicationIdentity available for AppDomain."));
			}
			appEvidenceType == null;
			this.storage_scope = scope;
		}

		/// <summary>When overridden in a derived class, removes the individual isolated store and all contained data.</summary>
		// Token: 0x06006A87 RID: 27271
		public abstract void Remove();

		/// <summary>When overridden in a derived class, prompts a user to approve a larger quota size, in bytes, for isolated storage.</summary>
		/// <param name="newQuotaSize">The requested new quota size, in bytes, for the user to approve.</param>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x06006A88 RID: 27272 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[ComVisible(false)]
		public virtual bool IncreaseQuotaTo(long newQuotaSize)
		{
			return false;
		}

		// Token: 0x04003D95 RID: 15765
		internal IsolatedStorageScope storage_scope;

		// Token: 0x04003D96 RID: 15766
		internal object _assemblyIdentity;

		// Token: 0x04003D97 RID: 15767
		internal object _domainIdentity;

		// Token: 0x04003D98 RID: 15768
		internal object _applicationIdentity;
	}
}
