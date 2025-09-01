using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000D96E0 File Offset: 0x000D78E0
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x000D974E File Offset: 0x000D794E
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000D9756 File Offset: 0x000D7956
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x000D975E File Offset: 0x000D795E
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000D9766 File Offset: 0x000D7966
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000D976E File Offset: 0x000D796E
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000D9776 File Offset: 0x000D7976
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x000D977E File Offset: 0x000D797E
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000D978C File Offset: 0x000D798C
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040028EF RID: 10479
		private object[] _inargs;

		// Token: 0x040028F0 RID: 10480
		private string _methodName;

		// Token: 0x040028F1 RID: 10481
		private string _typeName;

		// Token: 0x040028F2 RID: 10482
		private object _methodSignature;

		// Token: 0x040028F3 RID: 10483
		private Type[] _instArgs;

		// Token: 0x040028F4 RID: 10484
		private object[] _args;

		// Token: 0x040028F5 RID: 10485
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040028F6 RID: 10486
		private object[] _properties;
	}
}
