using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides data for manifest-based activation of an application. This class cannot be inherited.</summary>
	// Token: 0x02000555 RID: 1365
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context.</summary>
		/// <param name="activationData">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationData" /> is <see langword="null" />.</exception>
		// Token: 0x060035C1 RID: 13761 RVA: 0x000C2056 File Offset: 0x000C0256
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is <see langword="null" />.</exception>
		// Token: 0x060035C2 RID: 13762 RVA: 0x000C207F File Offset: 0x000C027F
		public ActivationArguments(ApplicationIdentity applicationIdentity)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context and activation data.</summary>
		/// <param name="activationContext">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060035C3 RID: 13763 RVA: 0x000C209C File Offset: 0x000C029C
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this._context = activationContext;
			this._identity = activationContext.Identity;
			this._data = activationData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity and activation data.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is <see langword="null" />.</exception>
		// Token: 0x060035C4 RID: 13764 RVA: 0x000C20CC File Offset: 0x000C02CC
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
			this._data = activationData;
		}

		/// <summary>Gets the activation context for manifest-based activation of an application.</summary>
		/// <returns>An object that identifies a manifest-based activation application.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000C20F0 File Offset: 0x000C02F0
		public ActivationContext ActivationContext
		{
			get
			{
				return this._context;
			}
		}

		/// <summary>Gets activation data from the host.</summary>
		/// <returns>An array of strings containing host-provided activation data.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000C20F8 File Offset: 0x000C02F8
		public string[] ActivationData
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>Gets the application identity for a manifest-activated application.</summary>
		/// <returns>An object that identifies an application for manifest-based activation.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x000C2100 File Offset: 0x000C0300
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x0400250F RID: 9487
		private ActivationContext _context;

		// Token: 0x04002510 RID: 9488
		private ApplicationIdentity _identity;

		// Token: 0x04002511 RID: 9489
		private string[] _data;
	}
}
