using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Provides an interface for an object that contains the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x0200067C RID: 1660
	[ComVisible(true)]
	public interface ISoapMessage
	{
		/// <summary>Gets or sets the parameter names of the method call.</summary>
		/// <returns>The parameter names of the method call.</returns>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003DCA RID: 15818
		// (set) Token: 0x06003DCB RID: 15819
		string[] ParamNames { get; set; }

		/// <summary>Gets or sets the parameter values of a method call.</summary>
		/// <returns>The parameter values of a method call.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003DCC RID: 15820
		// (set) Token: 0x06003DCD RID: 15821
		object[] ParamValues { get; set; }

		/// <summary>Gets or sets the parameter types of a method call.</summary>
		/// <returns>The parameter types of a method call.</returns>
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003DCE RID: 15822
		// (set) Token: 0x06003DCF RID: 15823
		Type[] ParamTypes { get; set; }

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003DD0 RID: 15824
		// (set) Token: 0x06003DD1 RID: 15825
		string MethodName { get; set; }

		/// <summary>Gets or sets the XML namespace of the SOAP RPC (Remote Procedure Call) <see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> element.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003DD2 RID: 15826
		// (set) Token: 0x06003DD3 RID: 15827
		string XmlNameSpace { get; set; }

		/// <summary>Gets or sets the out-of-band data of the method call.</summary>
		/// <returns>The out-of-band data of the method call.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003DD4 RID: 15828
		// (set) Token: 0x06003DD5 RID: 15829
		Header[] Headers { get; set; }
	}
}
