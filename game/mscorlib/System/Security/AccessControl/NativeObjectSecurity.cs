using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to native objects without direct manipulation of Access Control Lists (ACLs). Native object types are defined by the <see cref="T:System.Security.AccessControl.ResourceType" /> enumeration.</summary>
	// Token: 0x02000535 RID: 1333
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		// Token: 0x060034BC RID: 13500 RVA: 0x000BF9CB File Offset: 0x000BDBCB
		internal NativeObjectSecurity(CommonSecurityDescriptor securityDescriptor, ResourceType resourceType) : base(securityDescriptor)
		{
			this.resource_type = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		// Token: 0x060034BD RID: 13501 RVA: 0x000BF9DB File Offset: 0x000BDBDB
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType) : this(isContainer, resourceType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class by using the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060034BE RID: 13502 RVA: 0x000BF9E7 File Offset: 0x000BDBE7
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : base(isContainer)
		{
			this.exception_from_error_code = exceptionFromErrorCode;
			this.resource_type = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x060034BF RID: 13503 RVA: 0x000BF9FE File Offset: 0x000BDBFE
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections) : this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x060034C0 RID: 13504 RVA: 0x000BFA0D File Offset: 0x000BDC0D
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections) : this(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060034C1 RID: 13505 RVA: 0x000BFA1C File Offset: 0x000BDC1C
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(handle, includeSections), null, handle, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x060034C2 RID: 13506 RVA: 0x000BFA43 File Offset: 0x000BDC43
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(name, includeSections), name, null, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000BFA6C File Offset: 0x000BDC6C
		private void ClearAccessControlSectionsModified()
		{
			base.WriteLock();
			try
			{
				base.AccessControlSectionsModified = AccessControlSections.None;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend.persist that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060034C4 RID: 13508 RVA: 0x000BFAA0 File Offset: 0x000BDCA0
		protected sealed override void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			this.Persist(handle, includeSections, null);
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060034C5 RID: 13509 RVA: 0x000BFAAB File Offset: 0x000BDCAB
		protected sealed override void Persist(string name, AccessControlSections includeSections)
		{
			this.Persist(name, includeSections, null);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000BFAB8 File Offset: 0x000BDCB8
		internal void PersistModifications(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060034C7 RID: 13511 RVA: 0x000BFAF4 File Offset: 0x000BDCF4
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(handle, includeSections), null, handle, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000BFB40 File Offset: 0x000BDD40
		internal void PersistModifications(string name)
		{
			base.WriteLock();
			try
			{
				this.Persist(name, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x060034C9 RID: 13513 RVA: 0x000BFB7C File Offset: 0x000BDD7C
		protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(name, includeSections), name, null, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000BFBD8 File Offset: 0x000BDDD8
		internal static Exception DefaultExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			switch (errorCode)
			{
			case 2:
				return new FileNotFoundException();
			case 3:
				return new DirectoryNotFoundException();
			case 4:
				break;
			case 5:
				return new UnauthorizedAccessException();
			default:
				if (errorCode == 1314)
				{
					return new PrivilegeNotHeldException();
				}
				break;
			}
			return new InvalidOperationException("OS error code " + errorCode.ToString());
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000BFC35 File Offset: 0x000BDE35
		private void RaiseExceptionOnFailure(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 0)
			{
				return;
			}
			throw (this.exception_from_error_code ?? new NativeObjectSecurity.ExceptionFromErrorCode(NativeObjectSecurity.DefaultExceptionFromErrorCode))(errorCode, name, handle, context);
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000BFC5C File Offset: 0x000BDE5C
		internal virtual int InternalGet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(handle, this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000BFCA4 File Offset: 0x000BDEA4
		internal virtual int InternalGet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000BFCEC File Offset: 0x000BDEEC
		internal virtual int InternalSet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetSecurityInfo(handle, this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000BFD34 File Offset: 0x000BDF34
		internal virtual int InternalSet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000BFD7B File Offset: 0x000BDF7B
		internal ResourceType ResourceType
		{
			get
			{
				return this.resource_type;
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000BFD84 File Offset: 0x000BDF84
		private int Win32GetHelper(NativeObjectSecurity.GetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			bool flag = (includeSections & AccessControlSections.Owner) > AccessControlSections.None;
			bool flag2 = (includeSections & AccessControlSections.Group) > AccessControlSections.None;
			bool flag3 = (includeSections & AccessControlSections.Access) > AccessControlSections.None;
			bool flag4 = (includeSections & AccessControlSections.Audit) > AccessControlSections.None;
			SecurityInfos securityInfos = (SecurityInfos)0;
			if (flag)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if (flag2)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if (flag3)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			if (flag4)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			IntPtr intPtr5;
			int num = nativeCall(securityInfos, out intPtr, out intPtr2, out intPtr3, out intPtr4, out intPtr5);
			if (num != 0)
			{
				return num;
			}
			try
			{
				int num2 = 0;
				if (NativeObjectSecurity.IsValidSecurityDescriptor(intPtr5))
				{
					num2 = NativeObjectSecurity.GetSecurityDescriptorLength(intPtr5);
				}
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr5, array, 0, num2);
				base.SetSecurityDescriptorBinaryForm(array, includeSections);
			}
			finally
			{
				NativeObjectSecurity.LocalFree(intPtr5);
			}
			return 0;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000BFE34 File Offset: 0x000BE034
		private int Win32SetHelper(NativeObjectSecurity.SetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			if (includeSections == AccessControlSections.None)
			{
				return 0;
			}
			SecurityInfos securityInfos = (SecurityInfos)0;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)base.GetOwner(typeof(SecurityIdentifier));
				if (null != securityIdentifier)
				{
					array = new byte[securityIdentifier.BinaryLength];
					securityIdentifier.GetBinaryForm(array, 0);
				}
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)base.GetGroup(typeof(SecurityIdentifier));
				if (null != securityIdentifier2)
				{
					array2 = new byte[securityIdentifier2.BinaryLength];
					securityIdentifier2.GetBinaryForm(array2, 0);
				}
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
				if (base.AreAccessRulesProtected)
				{
					securityInfos |= (SecurityInfos)(-2147483648);
				}
				else
				{
					securityInfos |= (SecurityInfos)536870912;
				}
				array3 = new byte[this.descriptor.DiscretionaryAcl.BinaryLength];
				this.descriptor.DiscretionaryAcl.GetBinaryForm(array3, 0);
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.descriptor.SystemAcl != null)
			{
				securityInfos |= SecurityInfos.SystemAcl;
				if (base.AreAuditRulesProtected)
				{
					securityInfos |= (SecurityInfos)1073741824;
				}
				else
				{
					securityInfos |= (SecurityInfos)268435456;
				}
				array4 = new byte[this.descriptor.SystemAcl.BinaryLength];
				this.descriptor.SystemAcl.GetBinaryForm(array4, 0);
			}
			return nativeCall(securityInfos, array, array2, array3, array4);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000BFF82 File Offset: 0x000BE182
		private string Win32FixName(string name)
		{
			if (this.ResourceType == ResourceType.RegistryKey)
			{
				if (!name.StartsWith("HKEY_"))
				{
					throw new InvalidOperationException();
				}
				name = name.Substring("HKEY_".Length);
			}
			return name;
		}

		// Token: 0x060034D4 RID: 13524
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x060034D5 RID: 13525
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x060034D6 RID: 13526
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x060034D7 RID: 13527
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x060034D8 RID: 13528
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x060034D9 RID: 13529
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityDescriptorLength(IntPtr descriptor);

		// Token: 0x060034DA RID: 13530
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsValidSecurityDescriptor(IntPtr descriptor);

		// Token: 0x040024B8 RID: 9400
		private NativeObjectSecurity.ExceptionFromErrorCode exception_from_error_code;

		// Token: 0x040024B9 RID: 9401
		private ResourceType resource_type;

		/// <summary>Provides a way for integrators to map numeric error codes to specific exceptions that they create.</summary>
		/// <param name="errorCode">The numeric error code.</param>
		/// <param name="name">The name of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="context">An object that contains contextual information about the source or destination of the exception.</param>
		/// <returns>The <see cref="T:System.Exception" /> this delegate creates.</returns>
		// Token: 0x02000536 RID: 1334
		// (Invoke) Token: 0x060034DC RID: 13532
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);

		// Token: 0x02000537 RID: 1335
		// (Invoke) Token: 0x060034E0 RID: 13536
		private delegate int GetSecurityInfoNativeCall(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x02000538 RID: 1336
		// (Invoke) Token: 0x060034E4 RID: 13540
		private delegate int SetSecurityInfoNativeCall(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x02000539 RID: 1337
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x060034E7 RID: 13543 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060034E8 RID: 13544 RVA: 0x000BFFB3 File Offset: 0x000BE1B3
			internal int <InternalGet>b__0(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(this.handle, this.<>4__this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}

			// Token: 0x040024BA RID: 9402
			public SafeHandle handle;

			// Token: 0x040024BB RID: 9403
			public NativeObjectSecurity <>4__this;
		}

		// Token: 0x0200053A RID: 1338
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x060034E9 RID: 13545 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x060034EA RID: 13546 RVA: 0x000BFFD4 File Offset: 0x000BE1D4
			internal int <InternalGet>b__0(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetNamedSecurityInfo(this.<>4__this.Win32FixName(this.name), this.<>4__this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}

			// Token: 0x040024BC RID: 9404
			public NativeObjectSecurity <>4__this;

			// Token: 0x040024BD RID: 9405
			public string name;
		}

		// Token: 0x0200053B RID: 1339
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x060034EB RID: 13547 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x060034EC RID: 13548 RVA: 0x000C0000 File Offset: 0x000BE200
			internal int <InternalSet>b__0(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl)
			{
				return NativeObjectSecurity.SetSecurityInfo(this.handle, this.<>4__this.ResourceType, securityInfos, owner, group, dacl, sacl);
			}

			// Token: 0x040024BE RID: 9406
			public SafeHandle handle;

			// Token: 0x040024BF RID: 9407
			public NativeObjectSecurity <>4__this;
		}

		// Token: 0x0200053C RID: 1340
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060034ED RID: 13549 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060034EE RID: 13550 RVA: 0x000C001F File Offset: 0x000BE21F
			internal int <InternalSet>b__0(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl)
			{
				return NativeObjectSecurity.SetNamedSecurityInfo(this.<>4__this.Win32FixName(this.name), this.<>4__this.ResourceType, securityInfos, owner, group, dacl, sacl);
			}

			// Token: 0x040024C0 RID: 9408
			public NativeObjectSecurity <>4__this;

			// Token: 0x040024C1 RID: 9409
			public string name;
		}
	}
}
