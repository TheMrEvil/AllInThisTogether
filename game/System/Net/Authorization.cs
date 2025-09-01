using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020005C4 RID: 1476
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		// Token: 0x06002FD6 RID: 12246 RVA: 0x000A51F9 File Offset: 0x000A33F9
		public Authorization(string token)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		/// <param name="finished">The completion status of the authorization attempt. <see langword="true" /> if the authorization attempt is complete; otherwise, <see langword="false" />.</param>
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000A5214 File Offset: 0x000A3414
		public Authorization(string token, bool finished)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = finished;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server.</param>
		/// <param name="finished">The completion status of the authorization attempt. <see langword="true" /> if the authorization attempt is complete; otherwise, <see langword="false" />.</param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme.</param>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000A522F File Offset: 0x000A342F
		public Authorization(string token, bool finished, string connectionGroupId) : this(token, finished, connectionGroupId, false)
		{
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000A523B File Offset: 0x000A343B
		internal Authorization(string token, bool finished, string connectionGroupId, bool mutualAuth)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_ConnectionGroupId = ValidationHelper.MakeStringNull(connectionGroupId);
			this.m_Complete = finished;
			this.m_MutualAuth = mutualAuth;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000A526A File Offset: 0x000A346A
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000A5272 File Offset: 0x000A3472
		public string ConnectionGroupId
		{
			get
			{
				return this.m_ConnectionGroupId;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>
		///   <see langword="true" /> if the authentication process is complete; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000A527A File Offset: 0x000A347A
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000A5282 File Offset: 0x000A3482
		internal void SetComplete(bool complete)
		{
			this.m_Complete = complete;
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000A528B File Offset: 0x000A348B
		// (set) Token: 0x06002FDF RID: 12255 RVA: 0x000A5294 File Offset: 0x000A3494
		public string[] ProtectionRealm
		{
			get
			{
				return this.m_ProtectionRealm;
			}
			set
			{
				string[] protectionRealm = ValidationHelper.MakeEmptyArrayNull(value);
				this.m_ProtectionRealm = protectionRealm;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if both client and server were authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000A52AF File Offset: 0x000A34AF
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x000A52C1 File Offset: 0x000A34C1
		public bool MutuallyAuthenticated
		{
			get
			{
				return this.Complete && this.m_MutualAuth;
			}
			set
			{
				this.m_MutualAuth = value;
			}
		}

		// Token: 0x04001A5B RID: 6747
		private string m_Message;

		// Token: 0x04001A5C RID: 6748
		private bool m_Complete;

		// Token: 0x04001A5D RID: 6749
		private string[] m_ProtectionRealm;

		// Token: 0x04001A5E RID: 6750
		private string m_ConnectionGroupId;

		// Token: 0x04001A5F RID: 6751
		private bool m_MutualAuth;

		// Token: 0x04001A60 RID: 6752
		internal string ModuleAuthenticationType;
	}
}
