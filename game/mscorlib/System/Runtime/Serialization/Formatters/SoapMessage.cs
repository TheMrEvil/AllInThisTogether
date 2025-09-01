using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Holds the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x02000682 RID: 1666
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		/// <summary>Gets or sets the parameter names for the called method.</summary>
		/// <returns>The parameter names for the called method.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000D5CAC File Offset: 0x000D3EAC
		// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x000D5CB4 File Offset: 0x000D3EB4
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		/// <summary>Gets or sets the parameter values for the called method.</summary>
		/// <returns>Parameter values for the called method.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000D5CBD File Offset: 0x000D3EBD
		// (set) Token: 0x06003DFA RID: 15866 RVA: 0x000D5CC5 File Offset: 0x000D3EC5
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		/// <summary>This property is reserved. Use the <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> and/or <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> properties instead.</summary>
		/// <returns>Parameter types for the called method.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000D5CCE File Offset: 0x000D3ECE
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000D5CD6 File Offset: 0x000D3ED6
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000D5CDF File Offset: 0x000D3EDF
		// (set) Token: 0x06003DFE RID: 15870 RVA: 0x000D5CE7 File Offset: 0x000D3EE7
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name where the object that contains the called method is located.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000D5CF0 File Offset: 0x000D3EF0
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x000D5CF8 File Offset: 0x000D3EF8
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		/// <summary>Gets or sets the out-of-band data of the called method.</summary>
		/// <returns>The out-of-band data of the called method.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000D5D01 File Offset: 0x000D3F01
		// (set) Token: 0x06003E02 RID: 15874 RVA: 0x000D5D09 File Offset: 0x000D3F09
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapMessage" /> class.</summary>
		// Token: 0x06003E03 RID: 15875 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapMessage()
		{
		}

		// Token: 0x040027B7 RID: 10167
		internal string[] paramNames;

		// Token: 0x040027B8 RID: 10168
		internal object[] paramValues;

		// Token: 0x040027B9 RID: 10169
		internal Type[] paramTypes;

		// Token: 0x040027BA RID: 10170
		internal string methodName;

		// Token: 0x040027BB RID: 10171
		internal string xmlNameSpace;

		// Token: 0x040027BC RID: 10172
		internal Header[] headers;
	}
}
