using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Holds the security evidence for an application. This class cannot be inherited.</summary>
	// Token: 0x02000401 RID: 1025
	[ComVisible(true)]
	public sealed class ApplicationSecurityInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationSecurityInfo" /> class using the provided activation context.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object that uniquely identifies the target application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060029E4 RID: 10724 RVA: 0x000980F0 File Offset: 0x000962F0
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
		}

		/// <summary>Gets or sets the evidence for the application.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationEvidence" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x00098106 File Offset: 0x00096306
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x0009810E File Offset: 0x0009630E
		public Evidence ApplicationEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationEvidence");
				}
				this._evidence = value;
			}
		}

		/// <summary>Gets or sets the application identity information.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationId" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x00098125 File Offset: 0x00096325
		// (set) Token: 0x060029E8 RID: 10728 RVA: 0x0009812D File Offset: 0x0009632D
		public ApplicationId ApplicationId
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationId");
				}
				this._appid = value;
			}
		}

		/// <summary>Gets or sets the default permission set.</summary>
		/// <returns>A <see cref="T:System.Security.PermissionSet" /> object representing the default permissions for the application. The default is a <see cref="T:System.Security.PermissionSet" /> with a permission state of <see cref="F:System.Security.Permissions.PermissionState.None" /></returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DefaultRequestSet" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x00098144 File Offset: 0x00096344
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x0009815B File Offset: 0x0009635B
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this._defaultSet == null)
				{
					return new PermissionSet(PermissionState.None);
				}
				return this._defaultSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultRequestSet");
				}
				this._defaultSet = value;
			}
		}

		/// <summary>Gets or sets the top element in the application, which is described in the deployment identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object describing the top element of the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DeploymentId" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x00098172 File Offset: 0x00096372
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x0009817A File Offset: 0x0009637A
		public ApplicationId DeploymentId
		{
			get
			{
				return this._deployid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DeploymentId");
				}
				this._deployid = value;
			}
		}

		// Token: 0x04001F55 RID: 8021
		private Evidence _evidence;

		// Token: 0x04001F56 RID: 8022
		private ApplicationId _appid;

		// Token: 0x04001F57 RID: 8023
		private PermissionSet _defaultSet;

		// Token: 0x04001F58 RID: 8024
		private ApplicationId _deployid;
	}
}
