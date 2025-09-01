using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000610 RID: 1552
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06003AAA RID: 15018 RVA: 0x000CDC08 File Offset: 0x000CBE08
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000CDC28 File Offset: 0x000CBE28
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg) : base(retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			this._sig = CADMessageBase.GetSignature(base.GetMethod(), true);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000CDCE0 File Offset: 0x000CBEE0
		internal ArrayList GetArguments()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				byte[] array = new byte[this._serializedArgs.Length];
				Array.Copy(this._serializedArgs, array, this._serializedArgs.Length);
				result = new ArrayList((object[])CADSerializer.DeserializeObject(new MemoryStream(array)));
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000CDBEF File Offset: 0x000CBDEF
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000CDD37 File Offset: 0x000CBF37
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000CDD46 File Offset: 0x000CBF46
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000CDBFE File Offset: 0x000CBDFE
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x0400267E RID: 9854
		private object _returnValue;

		// Token: 0x0400267F RID: 9855
		private CADArgHolder _exception;

		// Token: 0x04002680 RID: 9856
		private Type[] _sig;
	}
}
