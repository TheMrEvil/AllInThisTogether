using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200060F RID: 1551
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000CDAFF File Offset: 0x000CBCFF
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000CDB08 File Offset: 0x000CBD08
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000CDB28 File Offset: 0x000CBD28
		internal CADMethodCallMessage(IMethodCallMessage callMsg) : base(callMsg)
		{
			this._uri = callMsg.Uri;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000CDB98 File Offset: 0x000CBD98
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

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000CDBEF File Offset: 0x000CBDEF
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000CDBFE File Offset: 0x000CBDFE
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x0400267D RID: 9853
		private string _uri;
	}
}
