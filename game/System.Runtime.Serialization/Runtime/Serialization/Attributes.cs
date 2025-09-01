using System;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000AB RID: 171
	internal class Attributes
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x000255D8 File Offset: 0x000237D8
		[SecuritySafeCritical]
		static Attributes()
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002563C File Offset: 0x0002383C
		[SecuritySafeCritical]
		internal void Read(XmlReaderDelegator reader)
		{
			this.Reset();
			while (reader.MoveToNextAttribute())
			{
				switch (reader.IndexOfLocalName(Attributes.serializationLocalNames, DictionaryGlobals.SerializationNamespace))
				{
				case 0:
					this.ReadId(reader);
					break;
				case 1:
					this.ReadArraySize(reader);
					break;
				case 2:
					this.ReadRef(reader);
					break;
				case 3:
					this.ClrType = reader.Value;
					break;
				case 4:
					this.ClrAssembly = reader.Value;
					break;
				case 5:
					this.ReadFactoryType(reader);
					break;
				default:
				{
					int num = reader.IndexOfLocalName(Attributes.schemaInstanceLocalNames, DictionaryGlobals.SchemaInstanceNamespace);
					if (num != 0)
					{
						if (num != 1)
						{
							if (!reader.IsNamespaceUri(DictionaryGlobals.XmlnsNamespace))
							{
								this.UnrecognizedAttributesFound = true;
							}
						}
						else
						{
							this.ReadXsiType(reader);
						}
					}
					else
					{
						this.ReadXsiNil(reader);
					}
					break;
				}
				}
			}
			reader.MoveToElement();
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00025718 File Offset: 0x00023918
		internal void Reset()
		{
			this.Id = Globals.NewObjectId;
			this.Ref = Globals.NewObjectId;
			this.XsiTypeName = null;
			this.XsiTypeNamespace = null;
			this.XsiTypePrefix = null;
			this.XsiNil = false;
			this.ClrAssembly = null;
			this.ClrType = null;
			this.ArraySZSize = -1;
			this.FactoryTypeName = null;
			this.FactoryTypeNamespace = null;
			this.FactoryTypePrefix = null;
			this.UnrecognizedAttributesFound = false;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00025788 File Offset: 0x00023988
		private void ReadId(XmlReaderDelegator reader)
		{
			this.Id = reader.ReadContentAsString();
			if (string.IsNullOrEmpty(this.Id))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid Id '{0}'. Must not be null or empty.", new object[]
				{
					this.Id
				})));
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000257C7 File Offset: 0x000239C7
		private void ReadRef(XmlReaderDelegator reader)
		{
			this.Ref = reader.ReadContentAsString();
			if (string.IsNullOrEmpty(this.Ref))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid Ref '{0}'. Must not be null or empty.", new object[]
				{
					this.Ref
				})));
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00025806 File Offset: 0x00023A06
		private void ReadXsiNil(XmlReaderDelegator reader)
		{
			this.XsiNil = reader.ReadContentAsBoolean();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00025814 File Offset: 0x00023A14
		private void ReadArraySize(XmlReaderDelegator reader)
		{
			this.ArraySZSize = reader.ReadContentAsInt();
			if (this.ArraySZSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Invalid Size '{0}'. Must be non-negative integer.", new object[]
				{
					this.ArraySZSize
				})));
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00025854 File Offset: 0x00023A54
		private void ReadXsiType(XmlReaderDelegator reader)
		{
			string value = reader.Value;
			if (value != null && value.Length > 0)
			{
				XmlObjectSerializerReadContext.ParseQualifiedName(value, reader, out this.XsiTypeName, out this.XsiTypeNamespace, out this.XsiTypePrefix);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00025890 File Offset: 0x00023A90
		private void ReadFactoryType(XmlReaderDelegator reader)
		{
			string value = reader.Value;
			if (value != null && value.Length > 0)
			{
				XmlObjectSerializerReadContext.ParseQualifiedName(value, reader, out this.FactoryTypeName, out this.FactoryTypeNamespace, out this.FactoryTypePrefix);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000222F File Offset: 0x0000042F
		public Attributes()
		{
		}

		// Token: 0x040003EC RID: 1004
		[SecurityCritical]
		private static XmlDictionaryString[] serializationLocalNames = new XmlDictionaryString[]
		{
			DictionaryGlobals.IdLocalName,
			DictionaryGlobals.ArraySizeLocalName,
			DictionaryGlobals.RefLocalName,
			DictionaryGlobals.ClrTypeLocalName,
			DictionaryGlobals.ClrAssemblyLocalName,
			DictionaryGlobals.ISerializableFactoryTypeLocalName
		};

		// Token: 0x040003ED RID: 1005
		[SecurityCritical]
		private static XmlDictionaryString[] schemaInstanceLocalNames = new XmlDictionaryString[]
		{
			DictionaryGlobals.XsiNilLocalName,
			DictionaryGlobals.XsiTypeLocalName
		};

		// Token: 0x040003EE RID: 1006
		internal string Id;

		// Token: 0x040003EF RID: 1007
		internal string Ref;

		// Token: 0x040003F0 RID: 1008
		internal string XsiTypeName;

		// Token: 0x040003F1 RID: 1009
		internal string XsiTypeNamespace;

		// Token: 0x040003F2 RID: 1010
		internal string XsiTypePrefix;

		// Token: 0x040003F3 RID: 1011
		internal bool XsiNil;

		// Token: 0x040003F4 RID: 1012
		internal string ClrAssembly;

		// Token: 0x040003F5 RID: 1013
		internal string ClrType;

		// Token: 0x040003F6 RID: 1014
		internal int ArraySZSize;

		// Token: 0x040003F7 RID: 1015
		internal string FactoryTypeName;

		// Token: 0x040003F8 RID: 1016
		internal string FactoryTypeNamespace;

		// Token: 0x040003F9 RID: 1017
		internal string FactoryTypePrefix;

		// Token: 0x040003FA RID: 1018
		internal bool UnrecognizedAttributesFound;
	}
}
