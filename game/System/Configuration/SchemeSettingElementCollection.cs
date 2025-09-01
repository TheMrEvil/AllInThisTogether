using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.SchemeSettingElement" /> objects.</summary>
	// Token: 0x02000879 RID: 2169
	[ConfigurationCollection(typeof(SchemeSettingElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
	public sealed class SchemeSettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class.</summary>
		// Token: 0x060044BA RID: 17594 RVA: 0x00013BCA File Offset: 0x00011DCA
		public SchemeSettingElementCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an item at the specified index in the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.SchemeSettingElement" /> to return.</param>
		/// <returns>The specified <see cref="T:System.Configuration.SchemeSettingElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="index" /> parameter is less than zero.  
		///  -or-  
		///  The item specified by the parameter is <see langword="null" /> or has been removed.</exception>
		// Token: 0x17000F88 RID: 3976
		public SchemeSettingElement this[int index]
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x00032884 File Offset: 0x00030A84
		protected override ConfigurationElement CreateNewElement()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x00032884 File Offset: 0x00030A84
		protected override object GetElementKey(ConfigurationElement element)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SchemeSettingElement" /> for the specified index location.</param>
		/// <returns>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />; otherwise, -1.</returns>
		// Token: 0x060044BE RID: 17598 RVA: 0x000ED1D4 File Offset: 0x000EB3D4
		public int IndexOf(SchemeSettingElement element)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
