using System;
using System.Collections;

namespace System.Xml.Xsl
{
	/// <summary>Contains a variable number of arguments which are either XSLT parameters or extension objects.</summary>
	// Token: 0x02000348 RID: 840
	public class XsltArgumentList
	{
		/// <summary>Implements a new instance of the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		// Token: 0x060022BE RID: 8894 RVA: 0x000DA5E4 File Offset: 0x000D87E4
		public XsltArgumentList()
		{
		}

		/// <summary>Gets the parameter associated with the namespace qualified name.</summary>
		/// <param name="name">The name of the parameter. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be <see langword="null" />. </param>
		/// <param name="namespaceUri">The namespace URI associated with the parameter. </param>
		/// <returns>The parameter object or <see langword="null" /> if one was not found.</returns>
		// Token: 0x060022BF RID: 8895 RVA: 0x000DA602 File Offset: 0x000D8802
		public object GetParam(string name, string namespaceUri)
		{
			return this.parameters[new XmlQualifiedName(name, namespaceUri)];
		}

		/// <summary>Gets the object associated with the given namespace.</summary>
		/// <param name="namespaceUri">The namespace URI of the object. </param>
		/// <returns>The namespace URI object or <see langword="null" /> if one was not found.</returns>
		// Token: 0x060022C0 RID: 8896 RVA: 0x000DA616 File Offset: 0x000D8816
		public object GetExtensionObject(string namespaceUri)
		{
			return this.extensions[namespaceUri];
		}

		/// <summary>Adds a parameter to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace qualified name.</summary>
		/// <param name="name">The name to associate with the parameter. </param>
		/// <param name="namespaceUri">The namespace URI to associate with the parameter. To use the default namespace, specify an empty string. </param>
		/// <param name="parameter">The parameter value or object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either <see langword="null" /> or http://www.w3.org/1999/XSL/Transform.The <paramref name="name" /> is not a valid name according to the W3C XML specification.The <paramref name="namespaceUri" /> already has a parameter associated with it. </exception>
		// Token: 0x060022C1 RID: 8897 RVA: 0x000DA624 File Offset: 0x000D8824
		public void AddParam(string name, string namespaceUri, object parameter)
		{
			XsltArgumentList.CheckArgumentNull(name, "name");
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(parameter, "parameter");
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, namespaceUri);
			xmlQualifiedName.Verify();
			this.parameters.Add(xmlQualifiedName, parameter);
		}

		/// <summary>Adds a new object to the <see cref="T:System.Xml.Xsl.XsltArgumentList" /> and associates it with the namespace URI.</summary>
		/// <param name="namespaceUri">The namespace URI to associate with the object. To use the default namespace, specify an empty string. </param>
		/// <param name="extension">The object to add to the list. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceUri" /> is either <see langword="null" /> or http://www.w3.org/1999/XSL/Transform The <paramref name="namespaceUri" /> already has an extension object associated with it. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient permissions to call this method. </exception>
		// Token: 0x060022C2 RID: 8898 RVA: 0x000DA66D File Offset: 0x000D886D
		public void AddExtensionObject(string namespaceUri, object extension)
		{
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(extension, "extension");
			this.extensions.Add(namespaceUri, extension);
		}

		/// <summary>Removes the parameter from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <param name="name">The name of the parameter to remove. <see cref="T:System.Xml.Xsl.XsltArgumentList" /> does not check to ensure the name passed is a valid local name; however, the name cannot be <see langword="null" />. </param>
		/// <param name="namespaceUri">The namespace URI of the parameter to remove. </param>
		/// <returns>The parameter object or <see langword="null" /> if one was not found.</returns>
		// Token: 0x060022C3 RID: 8899 RVA: 0x000DA694 File Offset: 0x000D8894
		public object RemoveParam(string name, string namespaceUri)
		{
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			object result = this.parameters[key];
			this.parameters.Remove(key);
			return result;
		}

		/// <summary>Removes the object with the namespace URI from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		/// <param name="namespaceUri">The namespace URI associated with the object to remove. </param>
		/// <returns>The object with the namespace URI or <see langword="null" /> if one was not found.</returns>
		// Token: 0x060022C4 RID: 8900 RVA: 0x000DA6C1 File Offset: 0x000D88C1
		public object RemoveExtensionObject(string namespaceUri)
		{
			object result = this.extensions[namespaceUri];
			this.extensions.Remove(namespaceUri);
			return result;
		}

		/// <summary>Occurs when a message is specified in the style sheet by the xsl:message element. </summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060022C5 RID: 8901 RVA: 0x000DA6DB File Offset: 0x000D88DB
		// (remove) Token: 0x060022C6 RID: 8902 RVA: 0x000DA6F4 File Offset: 0x000D88F4
		public event XsltMessageEncounteredEventHandler XsltMessageEncountered
		{
			add
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Combine(this.xsltMessageEncountered, value);
			}
			remove
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Remove(this.xsltMessageEncountered, value);
			}
		}

		/// <summary>Removes all parameters and extension objects from the <see cref="T:System.Xml.Xsl.XsltArgumentList" />.</summary>
		// Token: 0x060022C7 RID: 8903 RVA: 0x000DA70D File Offset: 0x000D890D
		public void Clear()
		{
			this.parameters.Clear();
			this.extensions.Clear();
			this.xsltMessageEncountered = null;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000DA72C File Offset: 0x000D892C
		private static void CheckArgumentNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x04001C4F RID: 7247
		private Hashtable parameters = new Hashtable();

		// Token: 0x04001C50 RID: 7248
		private Hashtable extensions = new Hashtable();

		// Token: 0x04001C51 RID: 7249
		internal XsltMessageEncounteredEventHandler xsltMessageEncountered;
	}
}
