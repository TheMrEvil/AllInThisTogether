using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Photon.Realtime
{
	// Token: 0x02000034 RID: 52
	public class AuthenticationValues
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000855C File Offset: 0x0000675C
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00008564 File Offset: 0x00006764
		public CustomAuthenticationType AuthType
		{
			get
			{
				return this.authType;
			}
			set
			{
				this.authType = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000856D File Offset: 0x0000676D
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00008575 File Offset: 0x00006775
		public string AuthGetParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<AuthGetParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AuthGetParameters>k__BackingField = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000857E File Offset: 0x0000677E
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00008586 File Offset: 0x00006786
		public object AuthPostData
		{
			[CompilerGenerated]
			get
			{
				return this.<AuthPostData>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AuthPostData>k__BackingField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000858F File Offset: 0x0000678F
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00008597 File Offset: 0x00006797
		public object Token
		{
			[CompilerGenerated]
			get
			{
				return this.<Token>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<Token>k__BackingField = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000085A0 File Offset: 0x000067A0
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000085A8 File Offset: 0x000067A8
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				return this.<UserId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserId>k__BackingField = value;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000085B1 File Offset: 0x000067B1
		public AuthenticationValues()
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000085C4 File Offset: 0x000067C4
		public AuthenticationValues(string userId)
		{
			this.UserId = userId;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000085DE File Offset: 0x000067DE
		public virtual void SetAuthPostData(string stringData)
		{
			this.AuthPostData = (string.IsNullOrEmpty(stringData) ? null : stringData);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000085F2 File Offset: 0x000067F2
		public virtual void SetAuthPostData(byte[] byteData)
		{
			this.AuthPostData = byteData;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000085FB File Offset: 0x000067FB
		public virtual void SetAuthPostData(Dictionary<string, object> dictData)
		{
			this.AuthPostData = dictData;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008604 File Offset: 0x00006804
		public virtual void AddAuthParameter(string key, string value)
		{
			string text = string.IsNullOrEmpty(this.AuthGetParameters) ? "" : "&";
			this.AuthGetParameters = string.Format("{0}{1}{2}={3}", new object[]
			{
				this.AuthGetParameters,
				text,
				Uri.EscapeDataString(key),
				Uri.EscapeDataString(value)
			});
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008660 File Offset: 0x00006860
		public override string ToString()
		{
			return string.Format("AuthenticationValues = AuthType: {0} UserId: {1}{2}{3}{4}", new object[]
			{
				this.AuthType,
				this.UserId,
				string.IsNullOrEmpty(this.AuthGetParameters) ? " GetParameters: yes" : "",
				(this.AuthPostData == null) ? "" : " PostData: yes",
				(this.Token == null) ? "" : " Token: yes"
			});
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000086DE File Offset: 0x000068DE
		public AuthenticationValues CopyTo(AuthenticationValues copy)
		{
			copy.AuthType = this.AuthType;
			copy.AuthGetParameters = this.AuthGetParameters;
			copy.AuthPostData = this.AuthPostData;
			copy.UserId = this.UserId;
			return copy;
		}

		// Token: 0x040001B4 RID: 436
		private CustomAuthenticationType authType = CustomAuthenticationType.None;

		// Token: 0x040001B5 RID: 437
		[CompilerGenerated]
		private string <AuthGetParameters>k__BackingField;

		// Token: 0x040001B6 RID: 438
		[CompilerGenerated]
		private object <AuthPostData>k__BackingField;

		// Token: 0x040001B7 RID: 439
		[CompilerGenerated]
		private object <Token>k__BackingField;

		// Token: 0x040001B8 RID: 440
		[CompilerGenerated]
		private string <UserId>k__BackingField;
	}
}
