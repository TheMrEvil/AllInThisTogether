using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> interface to create a request message that constitutes a constructor call on a remote object.</summary>
	// Token: 0x02000613 RID: 1555
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class by copying an existing message.</summary>
		/// <param name="m">A remoting message.</param>
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000CDEAF File Offset: 0x000CC0AF
		public ConstructionCall(IMessage m) : base(m)
		{
			this._activationTypeName = base.TypeName;
			this._isContextOk = true;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000CDECB File Offset: 0x000CC0CB
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class from an array of remoting headers.</summary>
		/// <param name="headers">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06003ABB RID: 15035 RVA: 0x000CDEED File Offset: 0x000CC0ED
		public ConstructionCall(Header[] headers) : base(headers)
		{
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000CDEF6 File Offset: 0x000CC0F6
		internal ConstructionCall(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000CDF00 File Offset: 0x000CC100
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000CDF27 File Offset: 0x000CC127
		// (set) Token: 0x06003ABF RID: 15039 RVA: 0x000CDF2F File Offset: 0x000CC12F
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		/// <summary>Gets the type of the remote object to activate.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the remote object to activate.</returns>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000CDF38 File Offset: 0x000CC138
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		/// <summary>Gets the full type name of the remote object to activate.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full type name of the remote object to activate.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000CDF5F File Offset: 0x000CC15F
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		/// <summary>Gets or sets the activator that activates the remote object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> that activates the remote object.</returns>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000CDF67 File Offset: 0x000CC167
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000CDF6F File Offset: 0x000CC16F
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		/// <summary>Gets the call site activation attributes for the remote object.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the call site activation attributes for the remote object.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000CDF78 File Offset: 0x000CC178
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000CDF80 File Offset: 0x000CC180
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		/// <summary>Gets a list of properties that define the context in which the remote object is to be created.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> that contains a list of properties that define the context in which the remote object is to be created.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000CDF89 File Offset: 0x000CC189
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
		internal override void InitMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				this._activator = (IActivator)value;
				return;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				this._activationAttributes = (object[])value;
				return;
			}
			if (key == "__ActivationType")
			{
				this._activationType = (Type)value;
				return;
			}
			if (key == "__ContextProperties")
			{
				this._contextProperties = (IList)value;
				return;
			}
			if (!(key == "__ActivationTypeName"))
			{
				base.InitMethodProperty(key, value);
				return;
			}
			this._activationTypeName = (string)value;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000CE040 File Offset: 0x000CC240
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000CE0BC File Offset: 0x000CC2BC
		// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000CE0C4 File Offset: 0x000CC2C4
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
			set
			{
				this._sourceProxy = value;
			}
		}

		// Token: 0x04002684 RID: 9860
		private IActivator _activator;

		// Token: 0x04002685 RID: 9861
		private object[] _activationAttributes;

		// Token: 0x04002686 RID: 9862
		private IList _contextProperties;

		// Token: 0x04002687 RID: 9863
		private Type _activationType;

		// Token: 0x04002688 RID: 9864
		private string _activationTypeName;

		// Token: 0x04002689 RID: 9865
		private bool _isContextOk;

		// Token: 0x0400268A RID: 9866
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
