using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a method. This class cannot be inherited.</summary>
	// Token: 0x020005DA RID: 1498
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Metadata.SoapMethodAttribute" />.</summary>
		// Token: 0x06003903 RID: 14595 RVA: 0x000CAFB2 File Offset: 0x000C91B2
		public SoapMethodAttribute()
		{
		}

		/// <summary>Gets or sets the XML element name to use for the method response to the target method.</summary>
		/// <returns>The XML element name to use for the method response to the target method.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000CB018 File Offset: 0x000C9218
		// (set) Token: 0x06003905 RID: 14597 RVA: 0x000CB020 File Offset: 0x000C9220
		public string ResponseXmlElementName
		{
			get
			{
				return this._responseElement;
			}
			set
			{
				this._responseElement = value;
			}
		}

		/// <summary>Gets or sets the XML element namesapce used for method response to the target method.</summary>
		/// <returns>The XML element namesapce used for method response to the target method.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000CB029 File Offset: 0x000C9229
		// (set) Token: 0x06003907 RID: 14599 RVA: 0x000CB031 File Offset: 0x000C9231
		public string ResponseXmlNamespace
		{
			get
			{
				return this._responseNamespace;
			}
			set
			{
				this._responseNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element name used for the return value from the target method.</summary>
		/// <returns>The XML element name used for the return value from the target method.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x000CB03A File Offset: 0x000C923A
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x000CB042 File Offset: 0x000C9242
		public string ReturnXmlElementName
		{
			get
			{
				return this._returnElement;
			}
			set
			{
				this._returnElement = value;
			}
		}

		/// <summary>Gets or sets the SOAPAction header field used with HTTP requests sent with this method. This property is currently not implemented.</summary>
		/// <returns>The SOAPAction header field used with HTTP requests sent with this method.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000CB04B File Offset: 0x000C924B
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x000CB053 File Offset: 0x000C9253
		public string SoapAction
		{
			get
			{
				return this._soapAction;
			}
			set
			{
				this._soapAction = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property.</exception>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000CB05C File Offset: 0x000C925C
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x000CB064 File Offset: 0x000C9264
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of remote method calls of the target method.</summary>
		/// <returns>The XML namespace that is used during serialization of remote method calls of the target method.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000CB06D File Offset: 0x000C926D
		// (set) Token: 0x0600390F RID: 14607 RVA: 0x000CB075 File Offset: 0x000C9275
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000CB080 File Offset: 0x000C9280
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x04002605 RID: 9733
		private string _responseElement;

		// Token: 0x04002606 RID: 9734
		private string _responseNamespace;

		// Token: 0x04002607 RID: 9735
		private string _returnElement;

		// Token: 0x04002608 RID: 9736
		private string _soapAction;

		// Token: 0x04002609 RID: 9737
		private bool _useAttribute;

		// Token: 0x0400260A RID: 9738
		private string _namespace;
	}
}
