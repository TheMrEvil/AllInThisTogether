using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> interface to create a message that responds to a call to instantiate a remote object.</summary>
	// Token: 0x02000615 RID: 1557
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> class from an array of remoting headers and a request message.</summary>
		/// <param name="h">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that constitutes a constructor call on a remote object.</param>
		// Token: 0x06003AD0 RID: 15056 RVA: 0x000CE27F File Offset: 0x000CC47F
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm) : base(h, mcm)
		{
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000CE289 File Offset: 0x000CC489
		internal ConstructionResponse(object resultObject, LogicalCallContext callCtx, IMethodCallMessage msg) : base(resultObject, null, callCtx, msg)
		{
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000CE295 File Offset: 0x000CC495
		internal ConstructionResponse(Exception e, IMethodCallMessage msg) : base(e, msg)
		{
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000CE29F File Offset: 0x000CC49F
		internal ConstructionResponse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000CE2A9 File Offset: 0x000CC4A9
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return base.Properties;
			}
		}
	}
}
