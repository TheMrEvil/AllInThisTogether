using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001C0 RID: 448
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElementCollection" /> class.</summary>
		// Token: 0x06000BBE RID: 3006 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public SettingElementCollection()
		{
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
		// Token: 0x06000BBF RID: 3007 RVA: 0x000316DB File Offset: 0x0002F8DB
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
		// Token: 0x06000BC0 RID: 3008 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		// Token: 0x06000BC1 RID: 3009 RVA: 0x000316EC File Offset: 0x0002F8EC
		public SettingElement Get(string elementKey)
		{
			foreach (object obj in this)
			{
				SettingElement settingElement = (SettingElement)obj;
				if (settingElement.Name == elementKey)
				{
					return settingElement;
				}
			}
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
		// Token: 0x06000BC2 RID: 3010 RVA: 0x00031750 File Offset: 0x0002F950
		public void Remove(SettingElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Name);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003176C File Offset: 0x0002F96C
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00031773 File Offset: 0x0002F973
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Name;
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00031780 File Offset: 0x0002F980
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}
	}
}
