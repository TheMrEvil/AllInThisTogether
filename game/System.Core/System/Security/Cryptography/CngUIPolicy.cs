using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates optional configuration parameters for the user interface (UI) that Cryptography Next Generation (CNG) displays when you access a protected key.</summary>
	// Token: 0x02000047 RID: 71
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngUIPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.</param>
		// Token: 0x06000155 RID: 341 RVA: 0x000038A2 File Offset: 0x00001AA2
		public CngUIPolicy(CngUIProtectionLevels protectionLevel) : this(protectionLevel, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level and friendly name.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		// Token: 0x06000156 RID: 342 RVA: 0x000038AC File Offset: 0x00001AAC
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName) : this(protectionLevel, friendlyName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, and description.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		// Token: 0x06000157 RID: 343 RVA: 0x000038B7 File Offset: 0x00001AB7
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description) : this(protectionLevel, friendlyName, description, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, description string, and use context.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		/// <param name="useContext">A description of how the key will be used. Specify a null string to use the default description.</param>
		// Token: 0x06000158 RID: 344 RVA: 0x000038C3 File Offset: 0x00001AC3
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext) : this(protectionLevel, friendlyName, description, useContext, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngUIPolicy" /> class by using the specified protection level, friendly name, description string, use context, and title.</summary>
		/// <param name="protectionLevel">A bitwise combination of the enumeration values that specify the protection level.  </param>
		/// <param name="friendlyName">A friendly name for the key to be used in the UI prompt. Specify a null string to use the default name.</param>
		/// <param name="description">The full-text description of the key. Specify a null string to use the default description.</param>
		/// <param name="useContext">A description of how the key will be used. Specify a null string to use the default description.</param>
		/// <param name="creationTitle">The title for the dialog box that provides the UI prompt. Specify a null string to use the default title.</param>
		// Token: 0x06000159 RID: 345 RVA: 0x000038D1 File Offset: 0x00001AD1
		public CngUIPolicy(CngUIProtectionLevels protectionLevel, string friendlyName, string description, string useContext, string creationTitle)
		{
			this.m_creationTitle = creationTitle;
			this.m_description = description;
			this.m_friendlyName = friendlyName;
			this.m_protectionLevel = protectionLevel;
			this.m_useContext = useContext;
		}

		/// <summary>Gets the title that is displayed by the UI prompt.</summary>
		/// <returns>The title of the dialog box that appears when the key is accessed.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000038FE File Offset: 0x00001AFE
		public string CreationTitle
		{
			get
			{
				return this.m_creationTitle;
			}
		}

		/// <summary>Gets the description string that is displayed by the UI prompt.</summary>
		/// <returns>The description text for the dialog box that appears when the key is accessed.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003906 File Offset: 0x00001B06
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		/// <summary>Gets the friendly name that is displayed by the UI prompt.</summary>
		/// <returns>The friendly name that is used to describe the key in the dialog box that appears when the key is accessed.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000390E File Offset: 0x00001B0E
		public string FriendlyName
		{
			get
			{
				return this.m_friendlyName;
			}
		}

		/// <summary>Gets the UI protection level for the key.</summary>
		/// <returns>An object that describes the level of UI protection to apply to the key.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003916 File Offset: 0x00001B16
		public CngUIProtectionLevels ProtectionLevel
		{
			get
			{
				return this.m_protectionLevel;
			}
		}

		/// <summary>Gets the description of how the key will be used.</summary>
		/// <returns>The description of how the key will be used.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000391E File Offset: 0x00001B1E
		public string UseContext
		{
			get
			{
				return this.m_useContext;
			}
		}

		// Token: 0x04000324 RID: 804
		private string m_creationTitle;

		// Token: 0x04000325 RID: 805
		private string m_description;

		// Token: 0x04000326 RID: 806
		private string m_friendlyName;

		// Token: 0x04000327 RID: 807
		private CngUIProtectionLevels m_protectionLevel;

		// Token: 0x04000328 RID: 808
		private string m_useContext;
	}
}
