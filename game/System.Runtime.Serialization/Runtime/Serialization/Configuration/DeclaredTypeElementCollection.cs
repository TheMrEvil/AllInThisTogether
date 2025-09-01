using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization using the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A2 RID: 418
	[ConfigurationCollection(typeof(DeclaredTypeElement))]
	public sealed class DeclaredTypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElementCollection" /> class.</summary>
		// Token: 0x06001526 RID: 5414 RVA: 0x00053F24 File Offset: 0x00052124
		public DeclaredTypeElementCollection()
		{
		}

		/// <summary>Gets or sets the configuration element at the specified index location.</summary>
		/// <param name="index">The index location of the configuration element to return.</param>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> at the specified index.</returns>
		// Token: 0x17000464 RID: 1124
		public DeclaredTypeElement this[int index]
		{
			get
			{
				return (DeclaredTypeElement)base.BaseGet(index);
			}
			set
			{
				if (!this.IsReadOnly())
				{
					if (value == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
					}
					if (base.BaseGet(index) != null)
					{
						base.BaseRemoveAt(index);
					}
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the element in the collection of types by its key.</summary>
		/// <param name="typeName">The name (that functions as a key) of the type to get or set.</param>
		/// <returns>The specified element (when used to get the element).</returns>
		// Token: 0x17000465 RID: 1125
		public DeclaredTypeElement this[string typeName]
		{
			get
			{
				if (string.IsNullOrEmpty(typeName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
				}
				return (DeclaredTypeElement)base.BaseGet(typeName);
			}
			set
			{
				if (!this.IsReadOnly())
				{
					if (string.IsNullOrEmpty(typeName))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
					}
					if (value == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
					}
					if (base.BaseGet(typeName) == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new IndexOutOfRangeException(SR.GetString("For type '{0}', configuration index is out of range.", new object[]
						{
							typeName
						})));
					}
					base.BaseRemove(typeName);
				}
				this.Add(value);
			}
		}

		/// <summary>Adds a specified configuration element to the collection.</summary>
		/// <param name="element">The configuration element to add.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x00053FFA File Offset: 0x000521FA
		public void Add(DeclaredTypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Removes all members of the collection.</summary>
		// Token: 0x0600152C RID: 5420 RVA: 0x00054019 File Offset: 0x00052219
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a value that specifies whether the element is in the collection.</summary>
		/// <param name="typeName">The name of the type to check for.</param>
		/// <returns>
		///   <see langword="true" /> if the element is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600152D RID: 5421 RVA: 0x00054021 File Offset: 0x00052221
		public bool Contains(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			return base.BaseGet(typeName) != null;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00054040 File Offset: 0x00052240
		protected override ConfigurationElement CreateNewElement()
		{
			return new DeclaredTypeElement();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00054047 File Offset: 0x00052247
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return ((DeclaredTypeElement)element).Type;
		}

		/// <summary>Returns the position of the specified configuration element.</summary>
		/// <param name="element">The element to find in the collection.</param>
		/// <returns>The index of the specified configuration element; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> argument is <see langword="null" />.</exception>
		// Token: 0x06001530 RID: 5424 RVA: 0x00054062 File Offset: 0x00052262
		public int IndexOf(DeclaredTypeElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> to remove.</param>
		// Token: 0x06001531 RID: 5425 RVA: 0x00054079 File Offset: 0x00052279
		public void Remove(DeclaredTypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the element specified by its key from the collection.</summary>
		/// <param name="typeName">The name of the type (which functions as a key) to remove from the collection.</param>
		// Token: 0x06001532 RID: 5426 RVA: 0x0005409E File Offset: 0x0005229E
		public void Remove(string typeName)
		{
			if (!this.IsReadOnly() && string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			base.BaseRemove(typeName);
		}

		/// <summary>Removes the configuration element found at the specified position.</summary>
		/// <param name="index">The position of the configuration element to remove.</param>
		// Token: 0x06001533 RID: 5427 RVA: 0x000540C2 File Offset: 0x000522C2
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
