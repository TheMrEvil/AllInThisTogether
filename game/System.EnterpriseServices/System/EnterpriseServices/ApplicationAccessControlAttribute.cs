using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies access controls to an assembly containing <see cref="T:System.EnterpriseServices.ServicedComponent" /> classes.</summary>
	// Token: 0x0200000C RID: 12
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ApplicationAccessControlAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class, enabling the COM+ security configuration.</summary>
		// Token: 0x0600000E RID: 14 RVA: 0x0000208C File Offset: 0x0000028C
		public ApplicationAccessControlAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ApplicationAccessControlAttribute.Value" /> property indicating whether to enable COM security configuration.</summary>
		/// <param name="val">
		///   <see langword="true" /> to allow configuration of security; otherwise, <see langword="false" />.</param>
		// Token: 0x0600000F RID: 15 RVA: 0x0000209B File Offset: 0x0000029B
		public ApplicationAccessControlAttribute(bool val)
		{
			this.val = val;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020AA File Offset: 0x000002AA
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020AD File Offset: 0x000002AD
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets or sets the access checking level to process level or to component level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AccessChecksLevelOption" /> values.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020BA File Offset: 0x000002BA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020C2 File Offset: 0x000002C2
		public AccessChecksLevelOption AccessChecksLevel
		{
			get
			{
				return this.accessChecksLevel;
			}
			set
			{
				this.accessChecksLevel = value;
			}
		}

		/// <summary>Gets or sets the remote procedure call (RPC) authentication level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020CB File Offset: 0x000002CB
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000020D3 File Offset: 0x000002D3
		public AuthenticationOption Authentication
		{
			get
			{
				return this.authentication;
			}
			set
			{
				this.authentication = value;
			}
		}

		/// <summary>Gets or sets the impersonation level that is allowed for calling targets of this application.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> values.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000020DC File Offset: 0x000002DC
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000020E4 File Offset: 0x000002E4
		public ImpersonationLevelOption ImpersonationLevel
		{
			get
			{
				return this.impersonation;
			}
			set
			{
				this.impersonation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable COM+ security configuration.</summary>
		/// <returns>
		///   <see langword="true" /> if COM+ security configuration is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000020ED File Offset: 0x000002ED
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000020F5 File Offset: 0x000002F5
		public bool Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x04000031 RID: 49
		private AccessChecksLevelOption accessChecksLevel;

		// Token: 0x04000032 RID: 50
		private AuthenticationOption authentication;

		// Token: 0x04000033 RID: 51
		private ImpersonationLevelOption impersonation;

		// Token: 0x04000034 RID: 52
		private bool val;
	}
}
