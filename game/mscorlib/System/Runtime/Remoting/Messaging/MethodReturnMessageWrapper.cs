using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a response to a method call on a remote object.</summary>
	// Token: 0x0200062D RID: 1581
	[ComVisible(true)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Wraps an <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> to create a <see cref="T:System.Runtime.Remoting.Messaging.MethodReturnMessageWrapper" />.</summary>
		/// <param name="msg">A message that acts as an outgoing method call on a remote object.</param>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000D00E8 File Offset: 0x000CE2E8
		public MethodReturnMessageWrapper(IMethodReturnMessage msg) : base(msg)
		{
			if (msg.Exception != null)
			{
				this._exception = msg.Exception;
				this._args = new object[0];
				return;
			}
			this._args = msg.Args;
			this._return = msg.ReturnValue;
			if (msg.MethodBase != null)
			{
				this._outArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.Out);
			}
		}

		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000D0155 File Offset: 0x000CE355
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000D015F File Offset: 0x000CE35F
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000D0167 File Offset: 0x000CE367
		public virtual object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		/// <summary>Gets the exception thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</returns>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000D0170 File Offset: 0x000CE370
		// (set) Token: 0x06003BA7 RID: 15271 RVA: 0x000D0178 File Offset: 0x000CE378
		public virtual Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		/// <summary>Gets a flag that indicates whether the method can accept a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000D0181 File Offset: 0x000CE381
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000D0193 File Offset: 0x000CE393
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000D01A5 File Offset: 0x000CE3A5
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodBase;
			}
		}

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000D01B7 File Offset: 0x000CE3B7
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodName;
			}
		}

		/// <summary>Gets an object that contains the method signature.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000D01C9 File Offset: 0x000CE3C9
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodSignature;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000D01DB File Offset: 0x000CE3DB
		public virtual int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._outArgInfo == null)
				{
					return 0;
				}
				return this._outArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as <see langword="ref" /> parameters or <see langword="out" /> parameters.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000D01F2 File Offset: 0x000CE3F2
		public virtual object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._outArgInfo == null)
				{
					return this._args;
				}
				return this._outArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x000D0214 File Offset: 0x000CE414
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000D023B File Offset: 0x000CE43B
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000D0243 File Offset: 0x000CE443
		public virtual object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._return;
			}
			set
			{
				this._return = value;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000D024C File Offset: 0x000CE44C
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).TypeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000D025E File Offset: 0x000CE45E
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x000D0270 File Offset: 0x000CE470
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).Uri;
			}
			set
			{
				this.Properties["__Uri"] = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument as an object.</returns>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D0283 File Offset: 0x000CE483
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument.</returns>
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000D028D File Offset: 0x000CE48D
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return ((IMethodReturnMessage)this.WrappedMessage).GetArgName(index);
		}

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</returns>
		// Token: 0x06003BB7 RID: 15287 RVA: 0x000D02A0 File Offset: 0x000CE4A0
		[SecurityCritical]
		public virtual object GetOutArg(int argNum)
		{
			return this._args[this._outArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The argument name, or <see langword="null" /> if the current method is not implemented.</returns>
		// Token: 0x06003BB8 RID: 15288 RVA: 0x000D02B5 File Offset: 0x000CE4B5
		[SecurityCritical]
		public virtual string GetOutArgName(int index)
		{
			return this._outArgInfo.GetInOutArgName(index);
		}

		// Token: 0x040026BD RID: 9917
		private object[] _args;

		// Token: 0x040026BE RID: 9918
		private ArgInfo _outArgInfo;

		// Token: 0x040026BF RID: 9919
		private MethodReturnMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x040026C0 RID: 9920
		private Exception _exception;

		// Token: 0x040026C1 RID: 9921
		private object _return;

		// Token: 0x0200062E RID: 1582
		private class DictionaryWrapper : MethodReturnDictionary
		{
			// Token: 0x06003BB9 RID: 15289 RVA: 0x000D02C3 File Offset: 0x000CE4C3
			public DictionaryWrapper(IMethodReturnMessage message, IDictionary wrappedDictionary) : base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodReturnMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x06003BBA RID: 15290 RVA: 0x000D02DE File Offset: 0x000CE4DE
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x06003BBB RID: 15291 RVA: 0x000D02E8 File Offset: 0x000CE4E8
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodReturnMessageWrapper)this._message)._args = (object[])value;
					return;
				}
				if (key == "__Return")
				{
					((MethodReturnMessageWrapper)this._message)._return = value;
					return;
				}
				base.SetMethodProperty(key, value);
			}

			// Token: 0x06003BBC RID: 15292 RVA: 0x000D0340 File Offset: 0x000CE540
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodReturnMessageWrapper)this._message)._args;
				}
				if (key == "__Return")
				{
					return ((MethodReturnMessageWrapper)this._message)._return;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x06003BBD RID: 15293 RVA: 0x000D0390 File Offset: 0x000CE590
			// Note: this type is marked as 'beforefieldinit'.
			static DictionaryWrapper()
			{
			}

			// Token: 0x040026C2 RID: 9922
			private IDictionary _wrappedDictionary;

			// Token: 0x040026C3 RID: 9923
			private static string[] _keys = new string[]
			{
				"__Args",
				"__Return"
			};
		}
	}
}
