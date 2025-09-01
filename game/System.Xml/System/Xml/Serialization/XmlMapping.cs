using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Supports mappings between .NET Framework types and XML Schema data types. </summary>
	// Token: 0x020002D6 RID: 726
	public abstract class XmlMapping
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x0009E73E File Offset: 0x0009C93E
		internal XmlMapping(TypeScope scope, ElementAccessor accessor) : this(scope, accessor, XmlMappingAccess.Read | XmlMappingAccess.Write)
		{
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0009E749 File Offset: 0x0009C949
		internal XmlMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
		{
			this.scope = scope;
			this.accessor = accessor;
			this.access = access;
			this.shallow = (scope == null);
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0009E770 File Offset: 0x0009C970
		internal ElementAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0009E778 File Offset: 0x0009C978
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		/// <summary>Get the name of the mapped element.</summary>
		/// <returns>The name of the mapped element.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0009E780 File Offset: 0x0009C980
		public string ElementName
		{
			get
			{
				return System.Xml.Serialization.Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		/// <summary>Gets the name of the XSD element of the mapping.</summary>
		/// <returns>The XSD element name.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0009E792 File Offset: 0x0009C992
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		/// <summary>Gets the namespace of the mapped element.</summary>
		/// <returns>The namespace of the mapped element.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0009E79F File Offset: 0x0009C99F
		public string Namespace
		{
			get
			{
				return this.accessor.Namespace;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0009E7AC File Offset: 0x0009C9AC
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0009E7B4 File Offset: 0x0009C9B4
		internal bool GenerateSerializer
		{
			get
			{
				return this.generateSerializer;
			}
			set
			{
				this.generateSerializer = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0009E7BD File Offset: 0x0009C9BD
		internal bool IsReadable
		{
			get
			{
				return (this.access & XmlMappingAccess.Read) > XmlMappingAccess.None;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0009E7CA File Offset: 0x0009C9CA
		internal bool IsWriteable
		{
			get
			{
				return (this.access & XmlMappingAccess.Write) > XmlMappingAccess.None;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0009E7D7 File Offset: 0x0009C9D7
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0009E7DF File Offset: 0x0009C9DF
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		/// <summary>Sets the key used to look up the mapping.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the lookup key.</param>
		// Token: 0x06001C0D RID: 7181 RVA: 0x0009E7E8 File Offset: 0x0009C9E8
		public void SetKey(string key)
		{
			this.SetKeyInternal(key);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		internal void SetKeyInternal(string key)
		{
			this.key = key;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0009E7FC File Offset: 0x0009C9FC
		internal static string GenerateKey(Type type, XmlRootAttribute root, string ns)
		{
			if (root == null)
			{
				root = (XmlRootAttribute)XmlAttributes.GetAttr(type, typeof(XmlRootAttribute));
			}
			return string.Concat(new string[]
			{
				type.FullName,
				":",
				(root == null) ? string.Empty : root.Key,
				":",
				(ns == null) ? string.Empty : ns
			});
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0009E868 File Offset: 0x0009CA68
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0009E870 File Offset: 0x0009CA70
		internal void CheckShallow()
		{
			if (this.shallow)
			{
				throw new InvalidOperationException(Res.GetString("This mapping was not crated by reflection importer and cannot be used in this context."));
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0009E88C File Offset: 0x0009CA8C
		internal static bool IsShallow(XmlMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i] == null || mappings[i].shallow)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019F9 RID: 6649
		private TypeScope scope;

		// Token: 0x040019FA RID: 6650
		private bool generateSerializer;

		// Token: 0x040019FB RID: 6651
		private bool isSoap;

		// Token: 0x040019FC RID: 6652
		private ElementAccessor accessor;

		// Token: 0x040019FD RID: 6653
		private string key;

		// Token: 0x040019FE RID: 6654
		private bool shallow;

		// Token: 0x040019FF RID: 6655
		private XmlMappingAccess access;
	}
}
