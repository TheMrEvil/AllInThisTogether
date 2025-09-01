using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the context for the trust manager to consider when making the decision to run an application, and when setting up the security on a new <see cref="T:System.AppDomain" /> in which to run an application.</summary>
	// Token: 0x02000422 RID: 1058
	[ComVisible(true)]
	public class TrustManagerContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class.</summary>
		// Token: 0x06002B51 RID: 11089 RVA: 0x0009CB36 File Offset: 0x0009AD36
		public TrustManagerContext() : this(TrustManagerUIContext.Run)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class using the specified <see cref="T:System.Security.Policy.TrustManagerUIContext" /> object.</summary>
		/// <param name="uiContext">One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values that specifies the type of trust manager user interface to use.</param>
		// Token: 0x06002B52 RID: 11090 RVA: 0x0009CB3F File Offset: 0x0009AD3F
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this._ignorePersistedDecision = false;
			this._noPrompt = false;
			this._keepAlive = false;
			this._persist = false;
			this._ui = uiContext;
		}

		/// <summary>Gets or sets a value indicating whether the application security manager should ignore any persisted decisions and call the trust manager.</summary>
		/// <returns>
		///   <see langword="true" /> to call the trust manager; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0009CB6A File Offset: 0x0009AD6A
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x0009CB72 File Offset: 0x0009AD72
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this._ignorePersistedDecision;
			}
			set
			{
				this._ignorePersistedDecision = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should cache state for this application, to facilitate future requests to determine application trust.</summary>
		/// <returns>
		///   <see langword="true" /> to cache state data; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x0009CB7B File Offset: 0x0009AD7B
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x0009CB83 File Offset: 0x0009AD83
		public virtual bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				this._keepAlive = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should prompt the user for trust decisions.</summary>
		/// <returns>
		///   <see langword="true" /> to not prompt the user; <see langword="false" /> to prompt the user. The default is <see langword="false" />.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x0009CB8C File Offset: 0x0009AD8C
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x0009CB94 File Offset: 0x0009AD94
		public virtual bool NoPrompt
		{
			get
			{
				return this._noPrompt;
			}
			set
			{
				this._noPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user's response to the consent dialog should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> to cache state data; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x0009CB9D File Offset: 0x0009AD9D
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x0009CBA5 File Offset: 0x0009ADA5
		public virtual bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		/// <summary>Gets or sets the identity of the previous application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object representing the previous <see cref="T:System.ApplicationIdentity" />.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x0009CBAE File Offset: 0x0009ADAE
		// (set) Token: 0x06002B5C RID: 11100 RVA: 0x0009CBB6 File Offset: 0x0009ADB6
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this._previousId;
			}
			set
			{
				this._previousId = value;
			}
		}

		/// <summary>Gets or sets the type of user interface the trust manager should display.</summary>
		/// <returns>One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values. The default is <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x0009CBC7 File Offset: 0x0009ADC7
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this._ui;
			}
			set
			{
				this._ui = value;
			}
		}

		// Token: 0x04001FBC RID: 8124
		private bool _ignorePersistedDecision;

		// Token: 0x04001FBD RID: 8125
		private bool _noPrompt;

		// Token: 0x04001FBE RID: 8126
		private bool _keepAlive;

		// Token: 0x04001FBF RID: 8127
		private bool _persist;

		// Token: 0x04001FC0 RID: 8128
		private ApplicationIdentity _previousId;

		// Token: 0x04001FC1 RID: 8129
		private TrustManagerUIContext _ui;
	}
}
