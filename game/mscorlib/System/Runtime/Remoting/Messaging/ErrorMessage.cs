using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000617 RID: 1559
	[Serializable]
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06003ADA RID: 15066 RVA: 0x000CE2E2 File Offset: 0x000CC4E2
		public ErrorMessage()
		{
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x0000AF5E File Offset: 0x0000915E
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000CE2F5 File Offset: 0x000CC4F5
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x0000AF5E File Offset: 0x0000915E
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000CE2F5 File Offset: 0x000CC4F5
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000CE2FC File Offset: 0x000CC4FC
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x000CE304 File Offset: 0x000CC504
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x000CE2F5 File Offset: 0x000CC4F5
		public string GetArgName(int arg_num)
		{
			return "unknown";
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int InArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x0000AF5E File Offset: 0x0000915E
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x0000AF5E File Offset: 0x0000915E
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400268D RID: 9869
		private string _uri = "Exception";
	}
}
