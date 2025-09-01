using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006AD RID: 1709
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x06003EF2 RID: 16114 RVA: 0x000D97CC File Offset: 0x000D79CC
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
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

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x000D9827 File Offset: 0x000D7A27
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000D982F File Offset: 0x000D7A2F
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000D9837 File Offset: 0x000D7A37
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000D983F File Offset: 0x000D7A3F
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x000D9847 File Offset: 0x000D7A47
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000D9854 File Offset: 0x000D7A54
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040028F7 RID: 10487
		private object[] _outargs;

		// Token: 0x040028F8 RID: 10488
		private Exception _exception;

		// Token: 0x040028F9 RID: 10489
		private object _returnValue;

		// Token: 0x040028FA RID: 10490
		private object[] _args;

		// Token: 0x040028FB RID: 10491
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040028FC RID: 10492
		private object[] _properties;
	}
}
