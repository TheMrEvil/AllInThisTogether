using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009C RID: 156
	internal sealed class ModuleReader : Module
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0001ACAC File Offset: 0x00018EAC
		internal ModuleReader(AssemblyReader assembly, Universe universe, Stream stream, string location, bool mapped) : base(universe)
		{
			this.stream = ((universe != null && universe.MetadataOnly) ? null : stream);
			this.location = location;
			this.Read(stream, mapped);
			if (universe != null && universe.WindowsRuntimeProjection && this.imageRuntimeVersion.StartsWith("WindowsRuntime ", StringComparison.Ordinal))
			{
				WindowsRuntimeProjection.Patch(this, this.strings, ref this.imageRuntimeVersion, ref this.blobHeap);
			}
			if (assembly == null && this.AssemblyTable.records.Length != 0)
			{
				assembly = new AssemblyReader(location, this);
			}
			this.assembly = assembly;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001AD78 File Offset: 0x00018F78
		private void Read(Stream stream, bool mapped)
		{
			BinaryReader br = new BinaryReader(stream);
			this.peFile.Read(br, mapped);
			stream.Seek(this.peFile.RvaToFileOffset(this.peFile.GetComDescriptorVirtualAddress()), SeekOrigin.Begin);
			this.cliHeader.Read(br);
			stream.Seek(this.peFile.RvaToFileOffset(this.cliHeader.MetaData.VirtualAddress), SeekOrigin.Begin);
			foreach (StreamHeader streamHeader in ModuleReader.ReadStreamHeaders(br, out this.imageRuntimeVersion))
			{
				string name = streamHeader.Name;
				if (!(name == "#Strings"))
				{
					if (!(name == "#Blob"))
					{
						if (!(name == "#US"))
						{
							if (!(name == "#GUID"))
							{
								if (name == "#~" || name == "#-")
								{
									stream.Seek(this.peFile.RvaToFileOffset(this.cliHeader.MetaData.VirtualAddress + streamHeader.Offset), SeekOrigin.Begin);
									this.ReadTables(br);
								}
							}
							else
							{
								this.guidHeap = this.ReadHeap(stream, streamHeader.Offset, streamHeader.Size);
							}
						}
						else
						{
							this.userStringHeapOffset = streamHeader.Offset;
							this.userStringHeapSize = streamHeader.Size;
						}
					}
					else
					{
						this.blobHeap = this.ReadHeap(stream, streamHeader.Offset, streamHeader.Size);
					}
				}
				else
				{
					this.stringHeap = this.ReadHeap(stream, streamHeader.Offset, streamHeader.Size);
				}
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001AF0E File Offset: 0x0001910E
		internal void SetAssembly(Assembly assembly)
		{
			this.assembly = assembly;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001AF18 File Offset: 0x00019118
		private static StreamHeader[] ReadStreamHeaders(BinaryReader br, out string Version)
		{
			if (br.ReadUInt32() != 1112167234U)
			{
				throw new BadImageFormatException("Invalid metadata signature");
			}
			br.ReadUInt16();
			br.ReadUInt16();
			br.ReadUInt32();
			uint count = br.ReadUInt32();
			byte[] bytes = br.ReadBytes((int)count);
			Version = Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
			br.ReadUInt16();
			StreamHeader[] array = new StreamHeader[(int)br.ReadUInt16()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new StreamHeader();
				array[i].Read(br);
			}
			return array;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AFAC File Offset: 0x000191AC
		private void ReadTables(BinaryReader br)
		{
			Table[] tables = base.GetTables();
			br.ReadUInt32();
			byte b = br.ReadByte();
			byte b2 = br.ReadByte();
			this.metadataStreamVersion = ((int)b << 16 | (int)b2);
			byte heapSizes = br.ReadByte();
			br.ReadByte();
			ulong num = br.ReadUInt64();
			ulong num2 = br.ReadUInt64();
			for (int i = 0; i < 64; i++)
			{
				if ((num & 1UL << i) != 0UL)
				{
					tables[i].Sorted = ((num2 & 1UL << i) > 0UL);
					tables[i].RowCount = br.ReadInt32();
				}
			}
			MetadataReader mr = new MetadataReader(this, br.BaseStream, heapSizes);
			for (int j = 0; j < 64; j++)
			{
				if ((num & 1UL << j) != 0UL)
				{
					tables[j].Read(mr);
				}
			}
			if (this.ParamPtr.RowCount != 0)
			{
				throw new NotImplementedException("ParamPtr table support has not yet been implemented.");
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001B094 File Offset: 0x00019294
		private byte[] ReadHeap(Stream stream, uint offset, uint size)
		{
			byte[] array = new byte[size];
			stream.Seek(this.peFile.RvaToFileOffset(this.cliHeader.MetaData.VirtualAddress + offset), SeekOrigin.Begin);
			int num;
			for (int i = 0; i < array.Length; i += num)
			{
				num = stream.Read(array, i, array.Length - i);
				if (num == 0)
				{
					throw new BadImageFormatException();
				}
			}
			return array;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001B0F3 File Offset: 0x000192F3
		internal void SeekRVA(int rva)
		{
			this.GetStream().Seek(this.peFile.RvaToFileOffset((uint)rva), SeekOrigin.Begin);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001B10E File Offset: 0x0001930E
		internal Stream GetStream()
		{
			if (this.stream == null)
			{
				throw new InvalidOperationException("Operation not available when UniverseOptions.MetadataOnly is enabled.");
			}
			return this.stream;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001B12C File Offset: 0x0001932C
		internal override void GetTypesImpl(List<Type> list)
		{
			this.PopulateTypeDef();
			foreach (TypeDefImpl typeDefImpl in this.typeDefs)
			{
				if (typeDefImpl != this.moduleType)
				{
					list.Add(typeDefImpl);
				}
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001B170 File Offset: 0x00019370
		private void PopulateTypeDef()
		{
			if (this.typeDefs == null)
			{
				this.typeDefs = new TypeDefImpl[this.TypeDef.records.Length];
				for (int i = 0; i < this.typeDefs.Length; i++)
				{
					TypeDefImpl typeDefImpl = new TypeDefImpl(this, i);
					this.typeDefs[i] = typeDefImpl;
					if (typeDefImpl.IsModulePseudoType)
					{
						this.moduleType = typeDefImpl;
					}
					else if (!typeDefImpl.IsNestedByFlags)
					{
						this.types.Add(typeDefImpl.TypeName, typeDefImpl);
					}
				}
				for (int j = 0; j < this.ExportedType.records.Length; j++)
				{
					if (this.ExportedType.records[j].Implementation >> 24 == 35)
					{
						TypeName typeName = this.GetTypeName(this.ExportedType.records[j].TypeNamespace, this.ExportedType.records[j].TypeName);
						this.forwardedTypes.Add(typeName, new ModuleReader.LazyForwardedType(j));
					}
				}
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001B26C File Offset: 0x0001946C
		internal override string GetString(int index)
		{
			if (index == 0)
			{
				return null;
			}
			string @string;
			if (!this.strings.TryGetValue(index, out @string))
			{
				int num = 0;
				while (this.stringHeap[index + num] != 0)
				{
					num++;
				}
				@string = Encoding.UTF8.GetString(this.stringHeap, index, num);
				this.strings.Add(index, @string);
			}
			return @string;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B2C4 File Offset: 0x000194C4
		private static int ReadCompressedUInt(byte[] buffer, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			byte b = buffer[num];
			if (b <= 127)
			{
				return (int)b;
			}
			if ((b & 192) == 128)
			{
				num = offset;
				offset = num + 1;
				byte b2 = buffer[num];
				return (int)(b & 63) << 8 | (int)b2;
			}
			num = offset;
			offset = num + 1;
			byte b3 = buffer[num];
			num = offset;
			offset = num + 1;
			byte b4 = buffer[num];
			num = offset;
			offset = num + 1;
			byte b5 = buffer[num];
			return ((int)(b & 63) << 24) + ((int)b3 << 16) + ((int)b4 << 8) + (int)b5;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001B344 File Offset: 0x00019544
		internal byte[] GetBlobCopy(int blobIndex)
		{
			int num = ModuleReader.ReadCompressedUInt(this.blobHeap, ref blobIndex);
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.blobHeap, blobIndex, array, 0, num);
			return array;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B376 File Offset: 0x00019576
		internal override ByteReader GetBlob(int blobIndex)
		{
			return ByteReader.FromBlob(this.blobHeap, blobIndex);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B384 File Offset: 0x00019584
		public override string ResolveString(int metadataToken)
		{
			string text;
			if (!this.strings.TryGetValue(metadataToken, out text))
			{
				if (metadataToken >> 24 != 112)
				{
					throw this.TokenOutOfRangeException(metadataToken);
				}
				if (this.lazyUserStringHeap == null)
				{
					this.lazyUserStringHeap = this.ReadHeap(this.GetStream(), this.userStringHeapOffset, this.userStringHeapSize);
				}
				int num = metadataToken & 16777215;
				int num2 = ModuleReader.ReadCompressedUInt(this.lazyUserStringHeap, ref num) & -2;
				StringBuilder stringBuilder = new StringBuilder(num2 / 2);
				for (int i = 0; i < num2; i += 2)
				{
					char value = (char)((int)this.lazyUserStringHeap[num + i] | (int)this.lazyUserStringHeap[num + i + 1] << 8);
					stringBuilder.Append(value);
				}
				text = stringBuilder.ToString();
				this.strings.Add(metadataToken, text);
			}
			return text;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B448 File Offset: 0x00019648
		internal override Type ResolveType(int metadataToken, IGenericContext context)
		{
			int num = (metadataToken & 16777215) - 1;
			if (num < 0)
			{
				throw this.TokenOutOfRangeException(metadataToken);
			}
			if (metadataToken >> 24 == 2 && num < this.TypeDef.RowCount)
			{
				this.PopulateTypeDef();
				return this.typeDefs[num];
			}
			if (metadataToken >> 24 == 1 && num < this.TypeRef.RowCount)
			{
				if (this.typeRefs == null)
				{
					this.typeRefs = new Type[this.TypeRef.records.Length];
				}
				if (this.typeRefs[num] == null)
				{
					int resolutionScope = this.TypeRef.records[num].ResolutionScope;
					int num2 = resolutionScope >> 24;
					if (num2 <= 1)
					{
						if (num2 != 0)
						{
							if (num2 != 1)
							{
								goto IL_20D;
							}
							Type type = this.ResolveType(resolutionScope, null);
							TypeName typeName = this.GetTypeName(this.TypeRef.records[num].TypeNamespace, this.TypeRef.records[num].TypeName);
							this.typeRefs[num] = type.ResolveNestedType(this, typeName);
							goto IL_229;
						}
					}
					else if (num2 != 26)
					{
						if (num2 == 35)
						{
							Assembly assembly = this.ResolveAssemblyRef((resolutionScope & 16777215) - 1);
							TypeName typeName2 = this.GetTypeName(this.TypeRef.records[num].TypeNamespace, this.TypeRef.records[num].TypeName);
							this.typeRefs[num] = assembly.ResolveType(this, typeName2);
							goto IL_229;
						}
						goto IL_20D;
					}
					Module module;
					if (resolutionScope >> 24 == 0)
					{
						if (resolutionScope != 0 && resolutionScope != 1)
						{
							throw new NotImplementedException("self reference scope?");
						}
						module = this;
					}
					else
					{
						module = this.ResolveModuleRef(this.ModuleRef.records[(resolutionScope & 16777215) - 1]);
					}
					TypeName typeName3 = this.GetTypeName(this.TypeRef.records[num].TypeNamespace, this.TypeRef.records[num].TypeName);
					this.typeRefs[num] = (module.FindType(typeName3) ?? module.universe.GetMissingTypeOrThrow(this, module, null, typeName3));
					goto IL_229;
					IL_20D:
					throw new NotImplementedException("ResolutionScope = " + resolutionScope.ToString("X"));
				}
				IL_229:
				return this.typeRefs[num];
			}
			if (metadataToken >> 24 == 27 && num < this.TypeSpec.RowCount)
			{
				if (this.typeSpecs == null)
				{
					this.typeSpecs = new Type[this.TypeSpec.records.Length];
				}
				Type type2 = this.typeSpecs[num];
				if (type2 == null)
				{
					ModuleReader.TrackingGenericContext trackingGenericContext = (context == null) ? null : new ModuleReader.TrackingGenericContext(context);
					type2 = Signature.ReadTypeSpec(this, ByteReader.FromBlob(this.blobHeap, this.TypeSpec.records[num]), trackingGenericContext);
					if (trackingGenericContext == null || !trackingGenericContext.IsUsed)
					{
						this.typeSpecs[num] = type2;
					}
				}
				return type2;
			}
			throw this.TokenOutOfRangeException(metadataToken);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B728 File Offset: 0x00019928
		private Module ResolveModuleRef(int moduleNameIndex)
		{
			string @string = this.GetString(moduleNameIndex);
			Module module = this.assembly.GetModule(@string);
			if (module == null)
			{
				throw new FileNotFoundException(@string);
			}
			return module;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B753 File Offset: 0x00019953
		private TypeName GetTypeName(int typeNamespace, int typeName)
		{
			return new TypeName(this.GetString(typeNamespace), this.GetString(typeName));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B768 File Offset: 0x00019968
		internal Assembly ResolveAssemblyRef(int index)
		{
			if (this.assemblyRefs == null)
			{
				this.assemblyRefs = new Assembly[this.AssemblyRef.RowCount];
			}
			if (this.assemblyRefs[index] == null)
			{
				this.assemblyRefs[index] = this.ResolveAssemblyRefImpl(ref this.AssemblyRef.records[index]);
			}
			return this.assemblyRefs[index];
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B7C4 File Offset: 0x000199C4
		private Assembly ResolveAssemblyRefImpl(ref AssemblyRefTable.Record rec)
		{
			string fullName = AssemblyName.GetFullName(this.GetString(rec.Name), rec.MajorVersion, rec.MinorVersion, rec.BuildNumber, rec.RevisionNumber, (rec.Culture == 0) ? "neutral" : this.GetString(rec.Culture), (rec.PublicKeyOrToken == 0) ? Empty<byte>.Array : (((rec.Flags & 1) == 0) ? this.GetBlobCopy(rec.PublicKeyOrToken) : AssemblyName.ComputePublicKeyToken(this.GetBlobCopy(rec.PublicKeyOrToken))), rec.Flags);
			return this.universe.Load(fullName, this, true);
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001B864 File Offset: 0x00019A64
		public override Guid ModuleVersionId
		{
			get
			{
				byte[] array = new byte[16];
				Buffer.BlockCopy(this.guidHeap, 16 * (this.ModuleTable.records[0].Mvid - 1), array, 0, 16);
				return new Guid(array);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001B8A9 File Offset: 0x00019AA9
		public override string FullyQualifiedName
		{
			get
			{
				return this.location ?? "<Unknown>";
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001B8BA File Offset: 0x00019ABA
		public override string Name
		{
			get
			{
				if (this.location != null)
				{
					return Path.GetFileName(this.location);
				}
				return "<Unknown>";
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001B8D5 File Offset: 0x00019AD5
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		internal override Type FindType(TypeName typeName)
		{
			this.PopulateTypeDef();
			Type result;
			ModuleReader.LazyForwardedType lazyForwardedType;
			if (!this.types.TryGetValue(typeName, out result) && this.forwardedTypes.TryGetValue(typeName, out lazyForwardedType))
			{
				return lazyForwardedType.GetType(this);
			}
			return result;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B91C File Offset: 0x00019B1C
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			this.PopulateTypeDef();
			foreach (Type type in this.types.Values)
			{
				if (type.TypeName.ToLowerInvariant() == lowerCaseName)
				{
					return type;
				}
			}
			foreach (TypeName key in this.forwardedTypes.Keys)
			{
				if (key.ToLowerInvariant() == lowerCaseName)
				{
					return this.forwardedTypes[key].GetType(this);
				}
			}
			return null;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		private Exception TokenOutOfRangeException(int metadataToken)
		{
			return new ArgumentOutOfRangeException("metadataToken", string.Format("Token 0x{0:x8} is not valid in the scope of module {1}.", metadataToken, this.Name));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001BA1C File Offset: 0x00019C1C
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			int num = metadataToken >> 24;
			if (num <= 10)
			{
				switch (num)
				{
				case 1:
				case 2:
					goto IL_77;
				case 3:
				case 5:
					goto IL_81;
				case 4:
					return this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
				case 6:
					break;
				default:
				{
					if (num != 10)
					{
						goto IL_81;
					}
					int num2 = (metadataToken & 16777215) - 1;
					if (num2 >= 0 && num2 < this.MemberRef.RowCount)
					{
						return this.GetMemberRef(num2, genericTypeArguments, genericMethodArguments);
					}
					goto IL_81;
				}
				}
			}
			else
			{
				if (num == 27)
				{
					goto IL_77;
				}
				if (num != 43)
				{
					goto IL_81;
				}
			}
			return this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			IL_77:
			return base.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			IL_81:
			throw this.TokenOutOfRangeException(metadataToken);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		internal FieldInfo GetFieldAt(TypeDefImpl owner, int index)
		{
			if (this.fields == null)
			{
				this.fields = new FieldInfo[this.Field.records.Length];
			}
			if (this.fields[index] == null)
			{
				this.fields[index] = new FieldDefImpl(this, owner ?? this.FindFieldOwner(index), index);
			}
			return this.fields[index];
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001BB14 File Offset: 0x00019D14
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			int num = (metadataToken & 16777215) - 1;
			if (num < 0)
			{
				throw this.TokenOutOfRangeException(metadataToken);
			}
			if (metadataToken >> 24 == 4 && num < this.Field.RowCount)
			{
				return this.GetFieldAt(null, num);
			}
			if (metadataToken >> 24 != 10 || num >= this.MemberRef.RowCount)
			{
				throw this.TokenOutOfRangeException(metadataToken);
			}
			FieldInfo fieldInfo = this.GetMemberRef(num, genericTypeArguments, genericMethodArguments) as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo;
			}
			throw new ArgumentException(string.Format("Token 0x{0:x8} is not a valid FieldInfo token in the scope of module {1}.", metadataToken, this.Name), "metadataToken");
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		private TypeDefImpl FindFieldOwner(int fieldIndex)
		{
			for (int i = 0; i < this.TypeDef.records.Length; i++)
			{
				int num = this.TypeDef.records[i].FieldList - 1;
				int num2 = (this.TypeDef.records.Length > i + 1) ? (this.TypeDef.records[i + 1].FieldList - 1) : this.Field.records.Length;
				if (num <= fieldIndex && fieldIndex < num2)
				{
					this.PopulateTypeDef();
					return this.typeDefs[i];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001BC44 File Offset: 0x00019E44
		internal MethodBase GetMethodAt(TypeDefImpl owner, int index)
		{
			if (this.methods == null)
			{
				this.methods = new MethodBase[this.MethodDef.records.Length];
			}
			if (this.methods[index] == null)
			{
				MethodDefImpl methodDefImpl = new MethodDefImpl(this, owner ?? this.FindMethodOwner(index), index);
				this.methods[index] = (methodDefImpl.IsConstructor ? new ConstructorInfoImpl(methodDefImpl) : methodDefImpl);
			}
			return this.methods[index];
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			int num = (metadataToken & 16777215) - 1;
			if (num < 0)
			{
				throw this.TokenOutOfRangeException(metadataToken);
			}
			if (metadataToken >> 24 == 6 && num < this.MethodDef.RowCount)
			{
				return this.GetMethodAt(null, num);
			}
			if (metadataToken >> 24 == 10 && num < this.MemberRef.RowCount)
			{
				MethodBase methodBase = this.GetMemberRef(num, genericTypeArguments, genericMethodArguments) as MethodBase;
				if (methodBase != null)
				{
					return methodBase;
				}
				throw new ArgumentException(string.Format("Token 0x{0:x8} is not a valid MethodBase token in the scope of module {1}.", metadataToken, this.Name), "metadataToken");
			}
			else
			{
				if (metadataToken >> 24 == 43 && num < this.MethodSpec.RowCount)
				{
					MethodInfo methodInfo = (MethodInfo)this.ResolveMethod(this.MethodSpec.records[num].Method, genericTypeArguments, genericMethodArguments);
					ByteReader br = ByteReader.FromBlob(this.blobHeap, this.MethodSpec.records[num].Instantiation);
					return methodInfo.MakeGenericMethod(Signature.ReadMethodSpec(this, br, new Module.GenericContext(genericTypeArguments, genericMethodArguments)));
				}
				throw this.TokenOutOfRangeException(metadataToken);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public override Type[] __ResolveOptionalParameterTypes(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments, out CustomModifiers[] customModifiers)
		{
			int num = (metadataToken & 16777215) - 1;
			if (num < 0)
			{
				throw this.TokenOutOfRangeException(metadataToken);
			}
			if (metadataToken >> 24 == 10 && num < this.MemberRef.RowCount)
			{
				int signature = this.MemberRef.records[num].Signature;
				return Signature.ReadOptionalParameterTypes(this, this.GetBlob(signature), new Module.GenericContext(genericTypeArguments, genericMethodArguments), out customModifiers);
			}
			if (metadataToken >> 24 == 6 && num < this.MethodDef.RowCount)
			{
				customModifiers = Empty<CustomModifiers>.Array;
				return Type.EmptyTypes;
			}
			throw this.TokenOutOfRangeException(metadataToken);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001BE54 File Offset: 0x0001A054
		public override CustomModifiers __ResolveTypeSpecCustomModifiers(int typeSpecToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			int num = (typeSpecToken & 16777215) - 1;
			if (typeSpecToken >> 24 != 27 || num < 0 || num >= this.TypeSpec.RowCount)
			{
				throw this.TokenOutOfRangeException(typeSpecToken);
			}
			return CustomModifiers.Read(this, ByteReader.FromBlob(this.blobHeap, this.TypeSpec.records[num]), new Module.GenericContext(genericTypeArguments, genericMethodArguments));
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001BEB2 File Offset: 0x0001A0B2
		public override string ScopeName
		{
			get
			{
				return this.GetString(this.ModuleTable.records[0].Name);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		private TypeDefImpl FindMethodOwner(int methodIndex)
		{
			for (int i = 0; i < this.TypeDef.records.Length; i++)
			{
				int num = this.TypeDef.records[i].MethodList - 1;
				int num2 = (this.TypeDef.records.Length > i + 1) ? (this.TypeDef.records[i + 1].MethodList - 1) : this.MethodDef.records.Length;
				if (num <= methodIndex && methodIndex < num2)
				{
					this.PopulateTypeDef();
					return this.typeDefs[i];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001BF64 File Offset: 0x0001A164
		private MemberInfo GetMemberRef(int index, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			if (this.memberRefs == null)
			{
				this.memberRefs = new MemberInfo[this.MemberRef.records.Length];
			}
			if (this.memberRefs[index] == null)
			{
				int @class = this.MemberRef.records[index].Class;
				int signature = this.MemberRef.records[index].Signature;
				string @string = this.GetString(this.MemberRef.records[index].Name);
				int num = @class >> 24;
				if (num <= 2)
				{
					if (num == 1 || num == 2)
					{
						this.memberRefs[index] = this.ResolveTypeMemberRef(base.ResolveType(@class), @string, ByteReader.FromBlob(this.blobHeap, signature));
						goto IL_1D5;
					}
				}
				else
				{
					if (num == 6)
					{
						return this.GetMethodAt(null, (@class & 16777215) - 1);
					}
					if (num == 26)
					{
						this.memberRefs[index] = this.ResolveTypeMemberRef(this.ResolveModuleType(@class), @string, ByteReader.FromBlob(this.blobHeap, signature));
						goto IL_1D5;
					}
					if (num == 27)
					{
						Type type = base.ResolveType(@class, genericTypeArguments, genericMethodArguments);
						if (type.IsArray)
						{
							MethodSignature signature2 = MethodSignature.ReadSig(this, ByteReader.FromBlob(this.blobHeap, signature), new Module.GenericContext(genericTypeArguments, genericMethodArguments));
							return type.FindMethod(@string, signature2) ?? this.universe.GetMissingMethodOrThrow(this, type, @string, signature2);
						}
						if (type.IsConstructedGenericType)
						{
							MemberInfo memberInfo = this.ResolveTypeMemberRef(type.GetGenericTypeDefinition(), @string, ByteReader.FromBlob(this.blobHeap, signature));
							MethodBase methodBase = memberInfo as MethodBase;
							if (methodBase != null)
							{
								memberInfo = methodBase.BindTypeParameters(type);
							}
							FieldInfo fieldInfo = memberInfo as FieldInfo;
							if (fieldInfo != null)
							{
								memberInfo = fieldInfo.BindTypeParameters(type);
							}
							return memberInfo;
						}
						return this.ResolveTypeMemberRef(type, @string, ByteReader.FromBlob(this.blobHeap, signature));
					}
				}
				throw new BadImageFormatException();
			}
			IL_1D5:
			return this.memberRefs[index];
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001C150 File Offset: 0x0001A350
		private Type ResolveModuleType(int token)
		{
			int num = (token & 16777215) - 1;
			string @string = this.GetString(this.ModuleRef.records[num]);
			Module module = this.assembly.GetModule(@string);
			if (module == null || module.IsResource())
			{
				throw new BadImageFormatException();
			}
			return module.GetModuleType();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		private MemberInfo ResolveTypeMemberRef(Type type, string name, ByteReader sig)
		{
			if (sig.PeekByte() == 6)
			{
				Type type2 = type;
				FieldSignature signature = FieldSignature.ReadSig(this, sig, type);
				FieldInfo fieldInfo = type.FindField(name, signature);
				if (fieldInfo == null && this.universe.MissingMemberResolution)
				{
					return this.universe.GetMissingFieldOrThrow(this, type, name, signature);
				}
				while (fieldInfo == null && (type = type.BaseType) != null)
				{
					fieldInfo = type.FindField(name, signature);
				}
				if (fieldInfo != null)
				{
					return fieldInfo;
				}
				throw new MissingFieldException(type2.ToString(), name);
			}
			else
			{
				Type type3 = type;
				MethodSignature signature2 = MethodSignature.ReadSig(this, sig, type);
				MethodBase methodBase = type.FindMethod(name, signature2);
				if (methodBase == null && this.universe.MissingMemberResolution)
				{
					return this.universe.GetMissingMethodOrThrow(this, type, name, signature2);
				}
				while (methodBase == null && (type = type.BaseType) != null)
				{
					methodBase = type.FindMethod(name, signature2);
				}
				if (methodBase != null)
				{
					return methodBase;
				}
				throw new MissingMethodException(type3.ToString(), name);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001C2A9 File Offset: 0x0001A4A9
		internal ByteReader GetStandAloneSig(int index)
		{
			return ByteReader.FromBlob(this.blobHeap, this.StandAloneSig.records[index]);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public override byte[] ResolveSignature(int metadataToken)
		{
			int num = (metadataToken & 16777215) - 1;
			if (metadataToken >> 24 == 17 && num >= 0 && num < this.StandAloneSig.RowCount)
			{
				ByteReader standAloneSig = this.GetStandAloneSig(num);
				return standAloneSig.ReadBytes(standAloneSig.Length);
			}
			throw this.TokenOutOfRangeException(metadataToken);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001C310 File Offset: 0x0001A510
		public override __StandAloneMethodSig __ResolveStandAloneMethodSig(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			int num = (metadataToken & 16777215) - 1;
			if (metadataToken >> 24 == 17 && num >= 0 && num < this.StandAloneSig.RowCount)
			{
				return MethodSignature.ReadStandAloneMethodSig(this, this.GetStandAloneSig(num), new Module.GenericContext(genericTypeArguments, genericMethodArguments));
			}
			throw this.TokenOutOfRangeException(metadataToken);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001C35D File Offset: 0x0001A55D
		internal MethodInfo GetEntryPoint()
		{
			if (this.cliHeader.EntryPointToken != 0U && (this.cliHeader.Flags & 16U) == 0U)
			{
				return (MethodInfo)base.ResolveMethod((int)this.cliHeader.EntryPointToken);
			}
			return null;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001C394 File Offset: 0x0001A594
		internal string[] GetManifestResourceNames()
		{
			string[] array = new string[this.ManifestResource.records.Length];
			for (int i = 0; i < this.ManifestResource.records.Length; i++)
			{
				array[i] = this.GetString(this.ManifestResource.records[i].Name);
			}
			return array;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		internal ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			int i = 0;
			while (i < this.ManifestResource.records.Length)
			{
				if (resourceName == this.GetString(this.ManifestResource.records[i].Name))
				{
					ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(this, i);
					Assembly referencedAssembly = manifestResourceInfo.ReferencedAssembly;
					if (referencedAssembly != null && !referencedAssembly.__IsMissing && referencedAssembly.GetManifestResourceInfo(resourceName) == null)
					{
						return null;
					}
					return manifestResourceInfo;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001C460 File Offset: 0x0001A660
		internal Stream GetManifestResourceStream(string resourceName)
		{
			int i = 0;
			while (i < this.ManifestResource.records.Length)
			{
				if (resourceName == this.GetString(this.ManifestResource.records[i].Name))
				{
					if (this.ManifestResource.records[i].Implementation == 637534208)
					{
						this.SeekRVA((int)(this.cliHeader.Resources.VirtualAddress + (uint)this.ManifestResource.records[i].Offset));
						BinaryReader binaryReader = new BinaryReader(this.stream);
						int count = binaryReader.ReadInt32();
						return new MemoryStream(binaryReader.ReadBytes(count));
					}
					ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(this, i);
					int num = this.ManifestResource.records[i].Implementation >> 24;
					if (num != 35)
					{
						if (num != 38)
						{
							throw new BadImageFormatException();
						}
						string path = Path.Combine(Path.GetDirectoryName(this.location), manifestResourceInfo.FileName);
						if (!System.IO.File.Exists(path))
						{
							return null;
						}
						FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete);
						if (fileStream.Length == 0L)
						{
							fileStream.Close();
							return null;
						}
						return fileStream;
					}
					else
					{
						Assembly referencedAssembly = manifestResourceInfo.ReferencedAssembly;
						if (referencedAssembly.__IsMissing)
						{
							return null;
						}
						return referencedAssembly.GetManifestResourceStream(resourceName);
					}
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public override AssemblyName[] __GetReferencedAssemblies()
		{
			List<AssemblyName> list = new List<AssemblyName>();
			for (int i = 0; i < this.AssemblyRef.records.Length; i++)
			{
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = this.GetString(this.AssemblyRef.records[i].Name);
				assemblyName.Version = new Version((int)this.AssemblyRef.records[i].MajorVersion, (int)this.AssemblyRef.records[i].MinorVersion, (int)this.AssemblyRef.records[i].BuildNumber, (int)this.AssemblyRef.records[i].RevisionNumber);
				if (this.AssemblyRef.records[i].PublicKeyOrToken != 0)
				{
					byte[] blobCopy = this.GetBlobCopy(this.AssemblyRef.records[i].PublicKeyOrToken);
					if ((this.AssemblyRef.records[i].Flags & 1) != 0)
					{
						assemblyName.SetPublicKey(blobCopy);
					}
					else
					{
						assemblyName.SetPublicKeyToken(blobCopy);
					}
				}
				else
				{
					assemblyName.SetPublicKeyToken(Empty<byte>.Array);
				}
				if (this.AssemblyRef.records[i].Culture != 0)
				{
					assemblyName.Culture = this.GetString(this.AssemblyRef.records[i].Culture);
				}
				else
				{
					assemblyName.Culture = "";
				}
				if (this.AssemblyRef.records[i].HashValue != 0)
				{
					assemblyName.hash = this.GetBlobCopy(this.AssemblyRef.records[i].HashValue);
				}
				assemblyName.RawFlags = (AssemblyNameFlags)this.AssemblyRef.records[i].Flags;
				list.Add(assemblyName);
			}
			return list.ToArray();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001C784 File Offset: 0x0001A984
		public override void __ResolveReferencedAssemblies(Assembly[] assemblies)
		{
			if (this.assemblyRefs == null)
			{
				this.assemblyRefs = new Assembly[this.AssemblyRef.RowCount];
			}
			for (int i = 0; i < assemblies.Length; i++)
			{
				if (this.assemblyRefs[i] == null)
				{
					this.assemblyRefs[i] = assemblies[i];
				}
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		public override string[] __GetReferencedModules()
		{
			string[] array = new string[this.ModuleRef.RowCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetString(this.ModuleRef.records[i]);
			}
			return array;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001C818 File Offset: 0x0001AA18
		public override Type[] __GetReferencedTypes()
		{
			Type[] array = new Type[this.TypeRef.RowCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.ResolveType((1 << 24) + i + 1);
			}
			return array;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001C858 File Offset: 0x0001AA58
		public override Type[] __GetExportedTypes()
		{
			Type[] array = new Type[this.ExportedType.RowCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ResolveExportedType(i);
			}
			return array;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001C890 File Offset: 0x0001AA90
		private Type ResolveExportedType(int index)
		{
			TypeName typeName = this.GetTypeName(this.ExportedType.records[index].TypeNamespace, this.ExportedType.records[index].TypeName);
			int implementation = this.ExportedType.records[index].Implementation;
			int typeDefId = this.ExportedType.records[index].TypeDefId;
			int flags = this.ExportedType.records[index].Flags;
			switch (implementation >> 24)
			{
			case 35:
				return this.ResolveAssemblyRef((implementation & 16777215) - 1).ResolveType(this, typeName).SetMetadataTokenForMissing(typeDefId, flags);
			case 38:
			{
				Module module = this.assembly.GetModule(this.GetString(this.File.records[(implementation & 16777215) - 1].Name));
				return module.FindType(typeName) ?? module.universe.GetMissingTypeOrThrow(this, module, null, typeName).SetMetadataTokenForMissing(typeDefId, flags);
			}
			case 39:
				return this.ResolveExportedType((implementation & 16777215) - 1).ResolveNestedType(this, typeName).SetMetadataTokenForMissing(typeDefId, flags);
			}
			throw new BadImageFormatException();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001C9D5 File Offset: 0x0001ABD5
		internal override Type GetModuleType()
		{
			this.PopulateTypeDef();
			return this.moduleType;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001C9E3 File Offset: 0x0001ABE3
		public override string __ImageRuntimeVersion
		{
			get
			{
				return this.imageRuntimeVersion;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001C9EB File Offset: 0x0001ABEB
		public override int MDStreamVersion
		{
			get
			{
				return this.metadataStreamVersion;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public override void __GetDataDirectoryEntry(int index, out int rva, out int length)
		{
			this.peFile.GetDataDirectoryEntry(index, out rva, out length);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001CA03 File Offset: 0x0001AC03
		public override long __RelativeVirtualAddressToFileOffset(int rva)
		{
			return this.peFile.RvaToFileOffset((uint)rva);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001CA11 File Offset: 0x0001AC11
		public override bool __GetSectionInfo(int rva, out string name, out int characteristics, out int virtualAddress, out int virtualSize, out int pointerToRawData, out int sizeOfRawData)
		{
			return this.peFile.GetSectionInfo(rva, out name, out characteristics, out virtualAddress, out virtualSize, out pointerToRawData, out sizeOfRawData);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		public override int __ReadDataFromRVA(int rva, byte[] data, int offset, int length)
		{
			this.SeekRVA(rva);
			int num = 0;
			while (length > 0)
			{
				int num2 = this.stream.Read(data, offset, length);
				if (num2 == 0)
				{
					break;
				}
				offset += num2;
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			peKind = PortableExecutableKinds.NotAPortableExecutableImage;
			if ((this.cliHeader.Flags & 1U) != 0U)
			{
				peKind |= PortableExecutableKinds.ILOnly;
			}
			uint num = this.cliHeader.Flags & 131074U;
			if (num != 2U)
			{
				if (num == 131074U)
				{
					peKind |= PortableExecutableKinds.Preferred32Bit;
				}
			}
			else
			{
				peKind |= PortableExecutableKinds.Required32Bit;
			}
			if (this.peFile.OptionalHeader.Magic == 523)
			{
				peKind |= PortableExecutableKinds.PE32Plus;
			}
			machine = (ImageFileMachine)this.peFile.FileHeader.Machine;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001CAEF File Offset: 0x0001ACEF
		public override int __Subsystem
		{
			get
			{
				return (int)this.peFile.OptionalHeader.Subsystem;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public override IList<CustomAttributeData> __GetPlaceholderAssemblyCustomAttributes(bool multiple, bool security)
		{
			TypeName o;
			switch ((multiple ? 1 : 0) + (security ? 2 : 0))
			{
			case 0:
				o = new TypeName("System.Runtime.CompilerServices", "AssemblyAttributesGoHere");
				goto IL_72;
			case 1:
				o = new TypeName("System.Runtime.CompilerServices", "AssemblyAttributesGoHereM");
				goto IL_72;
			case 2:
				o = new TypeName("System.Runtime.CompilerServices", "AssemblyAttributesGoHereS");
				goto IL_72;
			}
			o = new TypeName("System.Runtime.CompilerServices", "AssemblyAttributesGoHereSM");
			IL_72:
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			for (int i = 0; i < this.CustomAttribute.records.Length; i++)
			{
				if (this.CustomAttribute.records[i].Parent >> 24 == 1)
				{
					int num = (this.CustomAttribute.records[i].Parent & 16777215) - 1;
					if (o == this.GetTypeName(this.TypeRef.records[num].TypeNamespace, this.TypeRef.records[num].TypeName))
					{
						list.Add(new CustomAttributeData(this, i));
					}
				}
			}
			return list;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001CC2D File Offset: 0x0001AE2D
		internal override void Dispose()
		{
			if (this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001CC42 File Offset: 0x0001AE42
		internal override void ExportTypes(int fileToken, ModuleBuilder manifestModule)
		{
			this.PopulateTypeDef();
			manifestModule.ExportTypes(this.typeDefs, fileToken);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001CC57 File Offset: 0x0001AE57
		protected override long GetImageBaseImpl()
		{
			return (long)this.peFile.OptionalHeader.ImageBase;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001CC69 File Offset: 0x0001AE69
		protected override long GetStackReserveImpl()
		{
			return (long)this.peFile.OptionalHeader.SizeOfStackReserve;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001CC7B File Offset: 0x0001AE7B
		protected override int GetFileAlignmentImpl()
		{
			return (int)this.peFile.OptionalHeader.FileAlignment;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001CC8D File Offset: 0x0001AE8D
		protected override DllCharacteristics GetDllCharacteristicsImpl()
		{
			return (DllCharacteristics)this.peFile.OptionalHeader.DllCharacteristics;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001CC9F File Offset: 0x0001AE9F
		public override int __EntryPointRVA
		{
			get
			{
				if ((this.cliHeader.Flags & 16U) == 0U)
				{
					return 0;
				}
				return (int)this.cliHeader.EntryPointToken;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001CCBE File Offset: 0x0001AEBE
		public override int __EntryPointToken
		{
			get
			{
				if ((this.cliHeader.Flags & 16U) != 0U)
				{
					return 0;
				}
				return (int)this.cliHeader.EntryPointToken;
			}
		}

		// Token: 0x04000327 RID: 807
		private readonly Stream stream;

		// Token: 0x04000328 RID: 808
		private readonly string location;

		// Token: 0x04000329 RID: 809
		private Assembly assembly;

		// Token: 0x0400032A RID: 810
		private readonly PEReader peFile = new PEReader();

		// Token: 0x0400032B RID: 811
		private readonly CliHeader cliHeader = new CliHeader();

		// Token: 0x0400032C RID: 812
		private string imageRuntimeVersion;

		// Token: 0x0400032D RID: 813
		private int metadataStreamVersion;

		// Token: 0x0400032E RID: 814
		private byte[] stringHeap;

		// Token: 0x0400032F RID: 815
		private byte[] blobHeap;

		// Token: 0x04000330 RID: 816
		private byte[] guidHeap;

		// Token: 0x04000331 RID: 817
		private uint userStringHeapOffset;

		// Token: 0x04000332 RID: 818
		private uint userStringHeapSize;

		// Token: 0x04000333 RID: 819
		private byte[] lazyUserStringHeap;

		// Token: 0x04000334 RID: 820
		private TypeDefImpl[] typeDefs;

		// Token: 0x04000335 RID: 821
		private TypeDefImpl moduleType;

		// Token: 0x04000336 RID: 822
		private Assembly[] assemblyRefs;

		// Token: 0x04000337 RID: 823
		private Type[] typeRefs;

		// Token: 0x04000338 RID: 824
		private Type[] typeSpecs;

		// Token: 0x04000339 RID: 825
		private FieldInfo[] fields;

		// Token: 0x0400033A RID: 826
		private MethodBase[] methods;

		// Token: 0x0400033B RID: 827
		private MemberInfo[] memberRefs;

		// Token: 0x0400033C RID: 828
		private Dictionary<int, string> strings = new Dictionary<int, string>();

		// Token: 0x0400033D RID: 829
		private Dictionary<TypeName, Type> types = new Dictionary<TypeName, Type>();

		// Token: 0x0400033E RID: 830
		private Dictionary<TypeName, ModuleReader.LazyForwardedType> forwardedTypes = new Dictionary<TypeName, ModuleReader.LazyForwardedType>();

		// Token: 0x0200033E RID: 830
		private sealed class LazyForwardedType
		{
			// Token: 0x060025F3 RID: 9715 RVA: 0x000B4EEA File Offset: 0x000B30EA
			internal LazyForwardedType(int index)
			{
				this.index = index;
			}

			// Token: 0x060025F4 RID: 9716 RVA: 0x000B4EFC File Offset: 0x000B30FC
			internal Type GetType(ModuleReader module)
			{
				if (this.type == MarkerType.Pinned)
				{
					TypeName typeName = module.GetTypeName(module.ExportedType.records[this.index].TypeNamespace, module.ExportedType.records[this.index].TypeName);
					return module.universe.GetMissingTypeOrThrow(module, module, null, typeName).SetCyclicTypeForwarder();
				}
				if (this.type == null)
				{
					this.type = MarkerType.Pinned;
					this.type = module.ResolveExportedType(this.index);
				}
				return this.type;
			}

			// Token: 0x04000E89 RID: 3721
			private readonly int index;

			// Token: 0x04000E8A RID: 3722
			private Type type;
		}

		// Token: 0x0200033F RID: 831
		private sealed class TrackingGenericContext : IGenericContext
		{
			// Token: 0x060025F5 RID: 9717 RVA: 0x000B4F9E File Offset: 0x000B319E
			internal TrackingGenericContext(IGenericContext context)
			{
				this.context = context;
			}

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000B4FAD File Offset: 0x000B31AD
			internal bool IsUsed
			{
				get
				{
					return this.used;
				}
			}

			// Token: 0x060025F7 RID: 9719 RVA: 0x000B4FB5 File Offset: 0x000B31B5
			public Type GetGenericTypeArgument(int index)
			{
				this.used = true;
				return this.context.GetGenericTypeArgument(index);
			}

			// Token: 0x060025F8 RID: 9720 RVA: 0x000B4FCA File Offset: 0x000B31CA
			public Type GetGenericMethodArgument(int index)
			{
				this.used = true;
				return this.context.GetGenericMethodArgument(index);
			}

			// Token: 0x04000E8B RID: 3723
			private readonly IGenericContext context;

			// Token: 0x04000E8C RID: 3724
			private bool used;
		}
	}
}
