using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a method response on a remote object.</summary>
	// Token: 0x0200062B RID: 1579
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> class from an array of remoting headers and a request message.</summary>
		/// <param name="h1">An array of remoting headers that contains key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that acts as a method call on a remote object.</param>
		// Token: 0x06003B7E RID: 15230 RVA: 0x000CF838 File Offset: 0x000CDA38
		public MethodResponse(Header[] h1, IMethodCallMessage mcm)
		{
			if (mcm != null)
			{
				this._methodName = mcm.MethodName;
				this._uri = mcm.Uri;
				this._typeName = mcm.TypeName;
				this._methodBase = mcm.MethodBase;
				this._methodSignature = (Type[])mcm.MethodSignature;
				this._args = mcm.Args;
			}
			if (h1 != null)
			{
				foreach (Header header in h1)
				{
					this.InitMethodProperty(header.Name, header.Value);
				}
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000CF8C4 File Offset: 0x000CDAC4
		internal MethodResponse(Exception e, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			if (msg != null)
			{
				this._uri = msg.Uri;
			}
			else
			{
				this._uri = string.Empty;
			}
			this._exception = e;
			this._returnValue = null;
			this._outArgs = new object[0];
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000CF914 File Offset: 0x000CDB14
		internal MethodResponse(object returnValue, object[] outArgs, LogicalCallContext callCtx, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			this._uri = msg.Uri;
			this._exception = null;
			this._returnValue = returnValue;
			this._args = outArgs;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000CF948 File Offset: 0x000CDB48
		internal MethodResponse(IMethodCallMessage msg, CADMethodReturnMessage retmsg)
		{
			this._callMsg = msg;
			this._methodBase = msg.MethodBase;
			this._uri = msg.Uri;
			this._methodName = msg.MethodName;
			ArrayList arguments = retmsg.GetArguments();
			this._exception = retmsg.GetException(arguments);
			this._returnValue = retmsg.GetReturnValue(arguments);
			this._args = retmsg.GetArgs(arguments);
			this._callContext = retmsg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			if (retmsg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, retmsg.PropertiesCount, arguments);
			}
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000CF9F0 File Offset: 0x000CDBF0
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this._methodBase = msg.MethodBase;
				this._methodName = msg.MethodName;
				this._uri = msg.Uri;
			}
			this._returnValue = smuggledMrm.ReturnValue;
			this._args = smuggledMrm.Args;
			this._exception = smuggledMrm.Exception;
			this._callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000CFA70 File Offset: 0x000CDC70
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000CFAB0 File Offset: 0x000CDCB0
		internal void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1960967436U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								this._callContext = (LogicalCallContext)value;
								return;
							}
						}
					}
					else if (key == "__Uri")
					{
						this._uri = (string)value;
						return;
					}
				}
				else if (num != 1637783905U)
				{
					if (num == 1960967436U)
					{
						if (key == "__OutArgs")
						{
							this._args = (object[])value;
							return;
						}
					}
				}
				else if (key == "__Return")
				{
					this._returnValue = value;
					return;
				}
			}
			else if (num <= 3166241401U)
			{
				if (num != 2010141056U)
				{
					if (num == 3166241401U)
					{
						if (key == "__MethodName")
						{
							this._methodName = (string)value;
							return;
						}
					}
				}
				else if (key == "__TypeName")
				{
					this._typeName = (string)value;
					return;
				}
			}
			else if (num != 3626951189U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						this._methodSignature = (Type[])value;
						return;
					}
				}
			}
			else if (key == "__fault")
			{
				this._exception = (Exception)value;
				return;
			}
			this.Properties[key] = value;
		}

		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000CFC36 File Offset: 0x000CDE36
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args == null)
				{
					return 0;
				}
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000CFC4A File Offset: 0x000CDE4A
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets the exception thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</returns>
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000CFC52 File Offset: 0x000CDE52
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets a value that indicates whether the method can accept a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000CFC5A File Offset: 0x000CDE5A
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000CFC6C File Offset: 0x000CDE6C
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x000CFC88 File Offset: 0x000CDE88
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (null == this._methodBase)
				{
					if (this._callMsg != null)
					{
						this._methodBase = this._callMsg.MethodBase;
					}
					else if (this.MethodName != null && this.TypeName != null)
					{
						this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
					}
				}
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000CFCE0 File Offset: 0x000CDEE0
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodName == null && this._callMsg != null)
				{
					this._methodName = this._callMsg.MethodName;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an object that contains the method signature.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003B8C RID: 15244 RVA: 0x000CFD09 File Offset: 0x000CDF09
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._methodSignature == null && this._callMsg != null)
				{
					this._methodSignature = (Type[])this._callMsg.MethodSignature;
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets the number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000CFD37 File Offset: 0x000CDF37
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as <see langword="ref" /> or <see langword="out" /> parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as <see langword="ref" /> or <see langword="out" /> parameters.</returns>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003B8E RID: 15246 RVA: 0x000CFD74 File Offset: 0x000CDF74
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000CFDC8 File Offset: 0x000CDFC8
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this.ExternalProperties == null)
				{
					MethodReturnDictionary methodReturnDictionary = new MethodReturnDictionary(this);
					this.ExternalProperties = methodReturnDictionary;
					this.InternalProperties = methodReturnDictionary.GetInternalProperties();
				}
				return this.ExternalProperties;
			}
		}

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000CFDFD File Offset: 0x000CDFFD
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000CFE05 File Offset: 0x000CE005
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._typeName == null && this._callMsg != null)
				{
					this._typeName = this._callMsg.TypeName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000CFE2E File Offset: 0x000CE02E
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x000CFE57 File Offset: 0x000CE057
		public string Uri
		{
			[SecurityCritical]
			get
			{
				if (this._uri == null && this._callMsg != null)
				{
					this._uri = this._callMsg.Uri;
				}
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000CFE60 File Offset: 0x000CE060
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000CFE68 File Offset: 0x000CE068
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument as an object.</returns>
		// Token: 0x06003B96 RID: 15254 RVA: 0x000CFE71 File Offset: 0x000CE071
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument.</returns>
		// Token: 0x06003B97 RID: 15255 RVA: 0x000CFE85 File Offset: 0x000CE085
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this.MethodBase.GetParameters()[index].Name;
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented.</summary>
		/// <param name="info">Data for serializing or deserializing the remote object.</param>
		/// <param name="context">Context of a certain serialized stream.</param>
		// Token: 0x06003B98 RID: 15256 RVA: 0x000CFE9C File Offset: 0x000CE09C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._exception == null)
			{
				info.AddValue("__TypeName", this._typeName);
				info.AddValue("__MethodName", this._methodName);
				info.AddValue("__MethodSignature", this._methodSignature);
				info.AddValue("__Uri", this._uri);
				info.AddValue("__Return", this._returnValue);
				info.AddValue("__OutArgs", this._args);
			}
			else
			{
				info.AddValue("__fault", this._exception);
			}
			info.AddValue("__CallContext", this._callContext);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</returns>
		// Token: 0x06003B99 RID: 15257 RVA: 0x000CFFA4 File Offset: 0x000CE1A4
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The argument name, or <see langword="null" /> if the current method is not implemented.</returns>
		// Token: 0x06003B9A RID: 15258 RVA: 0x000CFFE0 File Offset: 0x000CE1E0
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (null == this._methodBase)
			{
				return "__method_" + index.ToString();
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		/// <summary>Initializes an internal serialization handler from an array of remoting headers that are applied to a method.</summary>
		/// <param name="h">An array of remoting headers that contain key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <returns>An internal serialization handler.</returns>
		// Token: 0x06003B9B RID: 15259 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public virtual object HeaderHandler(Header[] h)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets method information from serialization settings.</summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="ctx">The context of a certain serialized stream.</param>
		// Token: 0x06003B9C RID: 15260 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000D0033 File Offset: 0x000CE233
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000D003B File Offset: 0x000CE23B
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000D0044 File Offset: 0x000CE244
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x040026AC RID: 9900
		private string _methodName;

		// Token: 0x040026AD RID: 9901
		private string _uri;

		// Token: 0x040026AE RID: 9902
		private string _typeName;

		// Token: 0x040026AF RID: 9903
		private MethodBase _methodBase;

		// Token: 0x040026B0 RID: 9904
		private object _returnValue;

		// Token: 0x040026B1 RID: 9905
		private Exception _exception;

		// Token: 0x040026B2 RID: 9906
		private Type[] _methodSignature;

		// Token: 0x040026B3 RID: 9907
		private ArgInfo _inArgInfo;

		// Token: 0x040026B4 RID: 9908
		private object[] _args;

		// Token: 0x040026B5 RID: 9909
		private object[] _outArgs;

		// Token: 0x040026B6 RID: 9910
		private IMethodCallMessage _callMsg;

		// Token: 0x040026B7 RID: 9911
		private LogicalCallContext _callContext;

		// Token: 0x040026B8 RID: 9912
		private Identity _targetIdentity;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x040026B9 RID: 9913
		protected IDictionary ExternalProperties;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x040026BA RID: 9914
		protected IDictionary InternalProperties;
	}
}
