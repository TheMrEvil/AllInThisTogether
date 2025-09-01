using System;
using System.Collections.Generic;
using System.Diagnostics;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Impl;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008F RID: 143
	internal sealed class TextSection
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00017218 File Offset: 0x00015418
		internal TextSection(PEWriter peWriter, CliHeader cliHeader, ModuleBuilder moduleBuilder, int strongNameSignatureLength)
		{
			this.peWriter = peWriter;
			this.cliHeader = cliHeader;
			this.moduleBuilder = moduleBuilder;
			this.strongNameSignatureLength = (uint)strongNameSignatureLength;
			this.manifestResourcesLength = (uint)moduleBuilder.GetManifestResourcesLength();
			if (moduleBuilder.unmanagedExports.Count != 0)
			{
				this.exportTables = new TextSection.ExportTables(this);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00017278 File Offset: 0x00015478
		internal uint PointerToRawData
		{
			get
			{
				return this.peWriter.ToFileAlignment(this.peWriter.HeaderSize);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00017290 File Offset: 0x00015490
		internal uint BaseRVA
		{
			get
			{
				return this.peWriter.Headers.OptionalHeader.SectionAlignment;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000172A7 File Offset: 0x000154A7
		internal uint ImportAddressTableRVA
		{
			get
			{
				return this.BaseRVA;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000172AF File Offset: 0x000154AF
		internal uint ImportAddressTableLength
		{
			get
			{
				if (!this.peWriter.Is32Bit)
				{
					return 16U;
				}
				return 8U;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000172C2 File Offset: 0x000154C2
		internal uint ComDescriptorRVA
		{
			get
			{
				return this.ImportAddressTableRVA + this.ImportAddressTableLength;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x000172D1 File Offset: 0x000154D1
		internal uint ComDescriptorLength
		{
			get
			{
				return this.cliHeader.Cb;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000172DE File Offset: 0x000154DE
		internal uint MethodBodiesRVA
		{
			get
			{
				return this.ComDescriptorRVA + this.ComDescriptorLength + 7U & 4294967288U;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x000172F2 File Offset: 0x000154F2
		private uint MethodBodiesLength
		{
			get
			{
				return (uint)this.moduleBuilder.methodBodies.Length;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00017304 File Offset: 0x00015504
		private uint ResourcesRVA
		{
			get
			{
				ushort machine = this.peWriter.Headers.FileHeader.Machine;
				if (machine == 332 || machine == 452)
				{
					return this.MethodBodiesRVA + this.MethodBodiesLength + 3U & 4294967292U;
				}
				return this.MethodBodiesRVA + this.MethodBodiesLength + 15U & 4294967280U;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001735D File Offset: 0x0001555D
		private uint ResourcesLength
		{
			get
			{
				return this.manifestResourcesLength;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00017365 File Offset: 0x00015565
		internal uint StrongNameSignatureRVA
		{
			get
			{
				return this.ResourcesRVA + this.ResourcesLength + 3U & 4294967292U;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00017379 File Offset: 0x00015579
		internal uint StrongNameSignatureLength
		{
			get
			{
				return this.strongNameSignatureLength;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00017381 File Offset: 0x00015581
		private uint MetadataRVA
		{
			get
			{
				return this.StrongNameSignatureRVA + this.StrongNameSignatureLength + 3U & 4294967292U;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00017395 File Offset: 0x00015595
		private uint MetadataLength
		{
			get
			{
				return (uint)this.moduleBuilder.MetadataLength;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000173A2 File Offset: 0x000155A2
		private uint VTableFixupsRVA
		{
			get
			{
				return this.MetadataRVA + this.MetadataLength + 7U & 4294967288U;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000173B6 File Offset: 0x000155B6
		private uint VTableFixupsLength
		{
			get
			{
				return (uint)(this.moduleBuilder.vtablefixups.Count * 8);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000173CA File Offset: 0x000155CA
		internal uint DebugDirectoryRVA
		{
			get
			{
				return this.VTableFixupsRVA + this.VTableFixupsLength;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000173D9 File Offset: 0x000155D9
		internal uint DebugDirectoryLength
		{
			get
			{
				if (this.DebugDirectoryContentsLength != 0U)
				{
					return 28U;
				}
				return 0U;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000173E8 File Offset: 0x000155E8
		private uint DebugDirectoryContentsLength
		{
			get
			{
				if (this.moduleBuilder.symbolWriter != null)
				{
					IMAGE_DEBUG_DIRECTORY image_DEBUG_DIRECTORY = default(IMAGE_DEBUG_DIRECTORY);
					return (uint)SymbolSupport.GetDebugInfo(this.moduleBuilder.symbolWriter, ref image_DEBUG_DIRECTORY).Length;
				}
				return 0U;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00017420 File Offset: 0x00015620
		internal uint ExportDirectoryRVA
		{
			get
			{
				return this.DebugDirectoryRVA + this.DebugDirectoryLength + this.DebugDirectoryContentsLength + 15U & 4294967280U;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001743C File Offset: 0x0001563C
		internal uint ExportDirectoryLength
		{
			get
			{
				if (this.moduleBuilder.unmanagedExports.Count != 0)
				{
					return 40U;
				}
				return 0U;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00017454 File Offset: 0x00015654
		private uint ExportTablesRVA
		{
			get
			{
				return this.ExportDirectoryRVA + this.ExportDirectoryLength;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00017463 File Offset: 0x00015663
		private uint ExportTablesLength
		{
			get
			{
				if (this.exportTables != null)
				{
					return this.exportTables.Length;
				}
				return 0U;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001747A File Offset: 0x0001567A
		internal uint ImportDirectoryRVA
		{
			get
			{
				return this.ExportTablesRVA + this.ExportTablesLength + 15U & 4294967280U;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001748F File Offset: 0x0001568F
		internal uint ImportDirectoryLength
		{
			get
			{
				return this.ImportHintNameTableRVA - this.ImportDirectoryRVA + 27U;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000174A1 File Offset: 0x000156A1
		private uint ImportHintNameTableRVA
		{
			get
			{
				if (!this.peWriter.Is32Bit)
				{
					return this.ImportDirectoryRVA + 52U + 15U & 4294967280U;
				}
				return this.ImportDirectoryRVA + 48U + 15U & 4294967280U;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000174D0 File Offset: 0x000156D0
		internal uint StartupStubRVA
		{
			get
			{
				if (this.peWriter.Headers.FileHeader.Machine == 512)
				{
					return this.ImportDirectoryRVA + this.ImportDirectoryLength + 15U & 4294967280U;
				}
				return 2U + (this.ImportDirectoryRVA + this.ImportDirectoryLength + 3U & 4294967292U);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00017524 File Offset: 0x00015724
		internal uint StartupStubLength
		{
			get
			{
				ushort machine = this.peWriter.Headers.FileHeader.Machine;
				if (machine <= 452)
				{
					if (machine == 332)
					{
						return 6U;
					}
					if (machine != 452)
					{
						goto IL_4A;
					}
				}
				else
				{
					if (machine == 512)
					{
						return 48U;
					}
					if (machine != 34404)
					{
						goto IL_4A;
					}
				}
				return 12U;
				IL_4A:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00017580 File Offset: 0x00015780
		private void WriteRVA(MetadataWriter mw, uint rva)
		{
			ushort machine = this.peWriter.Headers.FileHeader.Machine;
			if (machine == 332 || machine == 452)
			{
				mw.Write(rva);
				return;
			}
			mw.Write((ulong)rva);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000175C4 File Offset: 0x000157C4
		internal void Write(MetadataWriter mw, uint sdataRVA, out int guidHeapOffset)
		{
			this.moduleBuilder.TypeRef.Fixup(this.moduleBuilder);
			this.moduleBuilder.MethodDef.Fixup(this);
			this.moduleBuilder.MethodImpl.Fixup(this.moduleBuilder);
			this.moduleBuilder.MethodSemantics.Fixup(this.moduleBuilder);
			this.moduleBuilder.InterfaceImpl.Fixup();
			this.moduleBuilder.ResolveInterfaceImplPseudoTokens();
			this.moduleBuilder.MemberRef.Fixup(this.moduleBuilder);
			this.moduleBuilder.Constant.Fixup(this.moduleBuilder);
			this.moduleBuilder.FieldMarshal.Fixup(this.moduleBuilder);
			this.moduleBuilder.DeclSecurity.Fixup(this.moduleBuilder);
			this.moduleBuilder.GenericParam.Fixup(this.moduleBuilder);
			this.moduleBuilder.CustomAttribute.Fixup(this.moduleBuilder);
			this.moduleBuilder.FieldLayout.Fixup(this.moduleBuilder);
			this.moduleBuilder.FieldRVA.Fixup(this.moduleBuilder, (int)sdataRVA, (int)this.MethodBodiesRVA);
			this.moduleBuilder.ImplMap.Fixup(this.moduleBuilder);
			this.moduleBuilder.ExportedType.Fixup(this.moduleBuilder);
			this.moduleBuilder.ManifestResource.Fixup(this.moduleBuilder);
			this.moduleBuilder.MethodSpec.Fixup(this.moduleBuilder);
			this.moduleBuilder.GenericParamConstraint.Fixup(this.moduleBuilder);
			if (this.ImportAddressTableLength != 0U)
			{
				this.WriteRVA(mw, this.ImportHintNameTableRVA);
				this.WriteRVA(mw, 0U);
			}
			this.cliHeader.MetaData.VirtualAddress = this.MetadataRVA;
			this.cliHeader.MetaData.Size = this.MetadataLength;
			if (this.ResourcesLength != 0U)
			{
				this.cliHeader.Resources.VirtualAddress = this.ResourcesRVA;
				this.cliHeader.Resources.Size = this.ResourcesLength;
			}
			if (this.StrongNameSignatureLength != 0U)
			{
				this.cliHeader.StrongNameSignature.VirtualAddress = this.StrongNameSignatureRVA;
				this.cliHeader.StrongNameSignature.Size = this.StrongNameSignatureLength;
			}
			if (this.VTableFixupsLength != 0U)
			{
				this.cliHeader.VTableFixups.VirtualAddress = this.VTableFixupsRVA;
				this.cliHeader.VTableFixups.Size = this.VTableFixupsLength;
			}
			this.cliHeader.Write(mw);
			for (int i = (int)(this.MethodBodiesRVA - (this.ComDescriptorRVA + this.ComDescriptorLength)); i > 0; i--)
			{
				mw.Write(0);
			}
			mw.Write(this.moduleBuilder.methodBodies);
			for (int j = (int)(this.ResourcesRVA - (this.MethodBodiesRVA + this.MethodBodiesLength)); j > 0; j--)
			{
				mw.Write(0);
			}
			this.moduleBuilder.WriteResources(mw);
			for (int k = (int)(this.MetadataRVA - (this.ResourcesRVA + this.ResourcesLength)); k > 0; k--)
			{
				mw.Write(0);
			}
			this.moduleBuilder.WriteMetadata(mw, out guidHeapOffset);
			for (int l = (int)(this.VTableFixupsRVA - (this.MetadataRVA + this.MetadataLength)); l > 0; l--)
			{
				mw.Write(0);
			}
			this.WriteVTableFixups(mw, sdataRVA);
			this.WriteDebugDirectory(mw);
			for (int m = (int)(this.ExportDirectoryRVA - (this.DebugDirectoryRVA + this.DebugDirectoryLength + this.DebugDirectoryContentsLength)); m > 0; m--)
			{
				mw.Write(0);
			}
			this.WriteExportDirectory(mw);
			this.WriteExportTables(mw, sdataRVA);
			for (int n = (int)(this.ImportDirectoryRVA - (this.ExportTablesRVA + this.ExportTablesLength)); n > 0; n--)
			{
				mw.Write(0);
			}
			if (this.ImportDirectoryLength != 0U)
			{
				this.WriteImportDirectory(mw);
			}
			for (int num = (int)(this.StartupStubRVA - (this.ImportDirectoryRVA + this.ImportDirectoryLength)); num > 0; num--)
			{
				mw.Write(0);
			}
			if (this.peWriter.Headers.FileHeader.Machine == 34404)
			{
				mw.Write(41288);
				mw.Write(this.peWriter.Headers.OptionalHeader.ImageBase + (ulong)this.ImportAddressTableRVA);
				mw.Write(57599);
				return;
			}
			if (this.peWriter.Headers.FileHeader.Machine == 512)
			{
				mw.Write(new byte[]
				{
					11,
					72,
					0,
					2,
					24,
					16,
					160,
					64,
					36,
					48,
					40,
					0,
					0,
					0,
					4,
					0,
					16,
					8,
					0,
					18,
					24,
					16,
					96,
					80,
					4,
					128,
					3,
					0,
					96,
					0,
					128,
					0
				});
				mw.Write(this.peWriter.Headers.OptionalHeader.ImageBase + (ulong)this.StartupStubRVA);
				mw.Write(this.peWriter.Headers.OptionalHeader.ImageBase + (ulong)this.BaseRVA);
				return;
			}
			if (this.peWriter.Headers.FileHeader.Machine == 332)
			{
				mw.Write(9727);
				mw.Write((uint)this.peWriter.Headers.OptionalHeader.ImageBase + this.ImportAddressTableRVA);
				return;
			}
			if (this.peWriter.Headers.FileHeader.Machine == 452)
			{
				uint num2 = (uint)this.peWriter.Headers.OptionalHeader.ImageBase + this.ImportAddressTableRVA;
				ushort num3 = (ushort)num2;
				ushort num4 = (ushort)(num2 >> 16);
				mw.Write((ushort)(62016 + (num3 >> 12)));
				mw.Write((ushort)(3072 + ((int)num3 << 4 & 61440) + (int)(num3 & 255)));
				mw.Write((ushort)(62144 + (num4 >> 12)));
				mw.Write((ushort)(3072 + ((int)num4 << 4 & 61440) + (int)(num4 & 255)));
				mw.Write(63708);
				mw.Write(61440);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000AF70 File Offset: 0x00009170
		[Conditional("DEBUG")]
		private void AssertRVA(MetadataWriter mw, uint rva)
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00017BAC File Offset: 0x00015DAC
		private void WriteVTableFixups(MetadataWriter mw, uint sdataRVA)
		{
			foreach (ModuleBuilder.VTableFixups vtableFixups in this.moduleBuilder.vtablefixups)
			{
				mw.Write(vtableFixups.initializedDataOffset + sdataRVA);
				mw.Write(vtableFixups.count);
				mw.Write(vtableFixups.type);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00017C24 File Offset: 0x00015E24
		private void WriteDebugDirectory(MetadataWriter mw)
		{
			if (this.DebugDirectoryLength != 0U)
			{
				IMAGE_DEBUG_DIRECTORY image_DEBUG_DIRECTORY = default(IMAGE_DEBUG_DIRECTORY);
				image_DEBUG_DIRECTORY.Characteristics = 0U;
				image_DEBUG_DIRECTORY.TimeDateStamp = this.peWriter.Headers.FileHeader.TimeDateStamp;
				byte[] debugInfo = SymbolSupport.GetDebugInfo(this.moduleBuilder.symbolWriter, ref image_DEBUG_DIRECTORY);
				image_DEBUG_DIRECTORY.PointerToRawData = this.DebugDirectoryRVA - this.BaseRVA + this.DebugDirectoryLength + this.PointerToRawData;
				image_DEBUG_DIRECTORY.AddressOfRawData = this.DebugDirectoryRVA + this.DebugDirectoryLength;
				mw.Write(image_DEBUG_DIRECTORY.Characteristics);
				mw.Write(image_DEBUG_DIRECTORY.TimeDateStamp);
				mw.Write(image_DEBUG_DIRECTORY.MajorVersion);
				mw.Write(image_DEBUG_DIRECTORY.MinorVersion);
				mw.Write(image_DEBUG_DIRECTORY.Type);
				mw.Write(image_DEBUG_DIRECTORY.SizeOfData);
				mw.Write(image_DEBUG_DIRECTORY.AddressOfRawData);
				mw.Write(image_DEBUG_DIRECTORY.PointerToRawData);
				mw.Write(debugInfo);
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00017D18 File Offset: 0x00015F18
		private uint GetOrdinalBase(out uint entries)
		{
			uint num = uint.MaxValue;
			uint num2 = 0U;
			foreach (UnmanagedExport unmanagedExport in this.moduleBuilder.unmanagedExports)
			{
				uint ordinal = (uint)unmanagedExport.ordinal;
				num = Math.Min(num, ordinal);
				num2 = Math.Max(num2, ordinal);
			}
			entries = 1U + (num2 - num);
			return num;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00017D8C File Offset: 0x00015F8C
		private uint GetExportNamesLength(out uint nameCount)
		{
			nameCount = 0U;
			uint num = 0U;
			foreach (UnmanagedExport unmanagedExport in this.moduleBuilder.unmanagedExports)
			{
				if (unmanagedExport.name != null)
				{
					nameCount += 1U;
					num += (uint)(unmanagedExport.name.Length + 1);
				}
			}
			return num;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00017E04 File Offset: 0x00016004
		private void WriteExportDirectory(MetadataWriter mw)
		{
			if (this.ExportDirectoryLength != 0U)
			{
				mw.Write(0);
				mw.Write(this.peWriter.Headers.FileHeader.TimeDateStamp);
				mw.Write(0);
				mw.Write(0);
				mw.Write(this.exportTables.namesRVA);
				mw.Write(this.exportTables.ordinalBase);
				mw.Write(this.exportTables.entries);
				mw.Write(this.exportTables.nameCount);
				mw.Write(this.exportTables.exportAddressTableRVA);
				mw.Write(this.exportTables.exportNamePointerTableRVA);
				mw.Write(this.exportTables.exportOrdinalTableRVA);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00017EC3 File Offset: 0x000160C3
		private void WriteExportTables(MetadataWriter mw, uint sdataRVA)
		{
			if (this.exportTables != null)
			{
				this.exportTables.Write(mw, sdataRVA);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00017EDC File Offset: 0x000160DC
		private void WriteImportDirectory(MetadataWriter mw)
		{
			mw.Write(this.ImportDirectoryRVA + 40U);
			mw.Write(0);
			mw.Write(0);
			mw.Write(this.ImportHintNameTableRVA + 14U);
			mw.Write(this.ImportAddressTableRVA);
			mw.Write(new byte[20]);
			mw.Write(this.ImportHintNameTableRVA);
			int num = 48;
			if (!this.peWriter.Is32Bit)
			{
				num += 4;
				mw.Write(0);
			}
			mw.Write(0);
			for (int i = (int)((ulong)this.ImportHintNameTableRVA - ((ulong)this.ImportDirectoryRVA + (ulong)((long)num))); i > 0; i--)
			{
				mw.Write(0);
			}
			mw.Write(0);
			if ((this.peWriter.Headers.FileHeader.Characteristics & 8192) != 0)
			{
				mw.WriteAsciiz("_CorDllMain");
			}
			else
			{
				mw.WriteAsciiz("_CorExeMain");
			}
			mw.WriteAsciiz("mscoree.dll");
			mw.Write(0);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00017FCF File Offset: 0x000161CF
		internal int Length
		{
			get
			{
				return (int)(this.StartupStubRVA - this.BaseRVA + this.StartupStubLength);
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00017FE8 File Offset: 0x000161E8
		internal void WriteRelocations(MetadataWriter mw)
		{
			foreach (TextSection.RelocationBlock relocationBlock in this.relocations)
			{
				mw.Write(relocationBlock.PageRVA);
				mw.Write(8 + relocationBlock.TypeOffset.Length * 2);
				foreach (ushort value in relocationBlock.TypeOffset)
				{
					mw.Write(value);
				}
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00018074 File Offset: 0x00016274
		internal uint PackRelocations()
		{
			List<TextSection.Relocation> list = new List<TextSection.Relocation>();
			ushort machine = this.peWriter.Headers.FileHeader.Machine;
			if (machine <= 452)
			{
				if (machine == 332)
				{
					list.Add(new TextSection.Relocation(12288, this.StartupStubRVA + 2U));
					goto IL_D7;
				}
				if (machine == 452)
				{
					list.Add(new TextSection.Relocation(28672, this.StartupStubRVA));
					goto IL_D7;
				}
			}
			else
			{
				if (machine == 512)
				{
					list.Add(new TextSection.Relocation(40960, this.StartupStubRVA + 32U));
					list.Add(new TextSection.Relocation(40960, this.StartupStubRVA + 40U));
					goto IL_D7;
				}
				if (machine == 34404)
				{
					list.Add(new TextSection.Relocation(40960, this.StartupStubRVA + 2U));
					goto IL_D7;
				}
			}
			throw new NotSupportedException();
			IL_D7:
			if (this.exportTables != null)
			{
				this.exportTables.GetRelocations(list);
			}
			list.Sort();
			uint num = 0U;
			int i = 0;
			while (i < list.Count)
			{
				uint num2 = list[i].rva & 4294963200U;
				int num3 = 1;
				while (i + num3 < list.Count && (list[i + num3].rva & 4294963200U) == num2)
				{
					num3++;
				}
				ushort[] array = new ushort[num3 + 1 & -2];
				int j = 0;
				while (j < num3)
				{
					array[j] = (ushort)((uint)list[i].type + (list[i].rva - num2));
					j++;
					i++;
				}
				this.relocations.Add(new TextSection.RelocationBlock(num2, array));
				num += (uint)(8 + array.Length * 2);
			}
			return num;
		}

		// Token: 0x040002EF RID: 751
		private readonly PEWriter peWriter;

		// Token: 0x040002F0 RID: 752
		private readonly CliHeader cliHeader;

		// Token: 0x040002F1 RID: 753
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x040002F2 RID: 754
		private readonly uint strongNameSignatureLength;

		// Token: 0x040002F3 RID: 755
		private readonly uint manifestResourcesLength;

		// Token: 0x040002F4 RID: 756
		private readonly TextSection.ExportTables exportTables;

		// Token: 0x040002F5 RID: 757
		private readonly List<TextSection.RelocationBlock> relocations = new List<TextSection.RelocationBlock>();

		// Token: 0x0200033A RID: 826
		private sealed class ExportTables
		{
			// Token: 0x060025D9 RID: 9689 RVA: 0x000B46EC File Offset: 0x000B28EC
			internal ExportTables(TextSection text)
			{
				this.text = text;
				this.ordinalBase = this.GetOrdinalBase(out this.entries);
				this.namesLength = this.GetExportNamesLength(out this.nameCount);
				this.exportAddressTableRVA = text.ExportTablesRVA;
				this.exportNamePointerTableRVA = this.exportAddressTableRVA + 4U * this.entries;
				this.exportOrdinalTableRVA = this.exportNamePointerTableRVA + 4U * this.nameCount;
				this.namesRVA = this.exportOrdinalTableRVA + 2U * this.nameCount;
				this.stubsRVA = (this.namesRVA + this.namesLength + 15U & 4294967280U);
				ushort machine = text.peWriter.Headers.FileHeader.Machine;
				if (machine == 332)
				{
					this.stubLength = 8U;
					return;
				}
				if (machine != 452 && machine != 34404)
				{
					throw new NotSupportedException();
				}
				this.stubLength = 16U;
			}

			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x060025DA RID: 9690 RVA: 0x000B47D4 File Offset: 0x000B29D4
			internal uint Length
			{
				get
				{
					return this.stubsRVA + this.stubLength * (uint)this.text.moduleBuilder.unmanagedExports.Count - this.text.ExportTablesRVA;
				}
			}

			// Token: 0x060025DB RID: 9691 RVA: 0x000B4808 File Offset: 0x000B2A08
			private uint GetOrdinalBase(out uint entries)
			{
				uint num = uint.MaxValue;
				uint num2 = 0U;
				foreach (UnmanagedExport unmanagedExport in this.text.moduleBuilder.unmanagedExports)
				{
					uint ordinal = (uint)unmanagedExport.ordinal;
					num = Math.Min(num, ordinal);
					num2 = Math.Max(num2, ordinal);
				}
				entries = 1U + (num2 - num);
				return num;
			}

			// Token: 0x060025DC RID: 9692 RVA: 0x000B4880 File Offset: 0x000B2A80
			private uint GetExportNamesLength(out uint nameCount)
			{
				nameCount = 0U;
				uint num = (uint)(this.text.moduleBuilder.fileName.Length + 1);
				foreach (UnmanagedExport unmanagedExport in this.text.moduleBuilder.unmanagedExports)
				{
					if (unmanagedExport.name != null)
					{
						nameCount += 1U;
						num += (uint)(unmanagedExport.name.Length + 1);
					}
				}
				return num;
			}

			// Token: 0x060025DD RID: 9693 RVA: 0x000B4910 File Offset: 0x000B2B10
			internal void Write(MetadataWriter mw, uint sdataRVA)
			{
				this.text.moduleBuilder.unmanagedExports.Sort(new Comparison<UnmanagedExport>(TextSection.ExportTables.CompareUnmanagedExportOrdinals));
				int num = 0;
				int num2 = 0;
				while ((long)num < (long)((ulong)this.entries))
				{
					if ((long)this.text.moduleBuilder.unmanagedExports[num2].ordinal == (long)num + (long)((ulong)this.ordinalBase))
					{
						mw.Write(this.text.peWriter.Thumb + this.stubsRVA + (uint)(num2 * (int)this.stubLength));
						num2++;
					}
					else
					{
						mw.Write(0);
					}
					num++;
				}
				this.text.moduleBuilder.unmanagedExports.Sort(new Comparison<UnmanagedExport>(TextSection.ExportTables.CompareUnmanagedExportNames));
				uint num3 = (uint)(this.text.moduleBuilder.fileName.Length + 1);
				foreach (UnmanagedExport unmanagedExport in this.text.moduleBuilder.unmanagedExports)
				{
					if (unmanagedExport.name != null)
					{
						mw.Write(this.namesRVA + num3);
						num3 += (uint)(unmanagedExport.name.Length + 1);
					}
				}
				foreach (UnmanagedExport unmanagedExport2 in this.text.moduleBuilder.unmanagedExports)
				{
					if (unmanagedExport2.name != null)
					{
						mw.Write((ushort)((long)unmanagedExport2.ordinal - (long)((ulong)this.ordinalBase)));
					}
				}
				mw.WriteAsciiz(this.text.moduleBuilder.fileName);
				foreach (UnmanagedExport unmanagedExport3 in this.text.moduleBuilder.unmanagedExports)
				{
					if (unmanagedExport3.name != null)
					{
						mw.WriteAsciiz(unmanagedExport3.name);
					}
				}
				for (int i = (int)(this.stubsRVA - (this.namesRVA + this.namesLength)); i > 0; i--)
				{
					mw.Write(0);
				}
				this.text.moduleBuilder.unmanagedExports.Sort(new Comparison<UnmanagedExport>(TextSection.ExportTables.CompareUnmanagedExportOrdinals));
				int num4 = 0;
				int num5 = 0;
				while ((long)num4 < (long)((ulong)this.entries))
				{
					if ((long)this.text.moduleBuilder.unmanagedExports[num5].ordinal == (long)num4 + (long)((ulong)this.ordinalBase))
					{
						ushort machine = this.text.peWriter.Headers.FileHeader.Machine;
						if (machine != 332)
						{
							if (machine != 452)
							{
								if (machine != 34404)
								{
									throw new NotSupportedException();
								}
								mw.Write(72);
								mw.Write(161);
								mw.Write(this.text.peWriter.Headers.OptionalHeader.ImageBase + (ulong)this.text.moduleBuilder.unmanagedExports[num5].rva.initializedDataOffset + (ulong)sdataRVA);
								mw.Write(byte.MaxValue);
								mw.Write(224);
								mw.Write(0);
							}
							else
							{
								mw.Write(63711);
								mw.Write(49160);
								mw.Write(63708);
								mw.Write(49152);
								mw.Write(18272);
								mw.Write(57086);
								mw.Write((uint)this.text.peWriter.Headers.OptionalHeader.ImageBase + this.text.moduleBuilder.unmanagedExports[num5].rva.initializedDataOffset + sdataRVA);
							}
						}
						else
						{
							mw.Write(byte.MaxValue);
							mw.Write(37);
							mw.Write((uint)this.text.peWriter.Headers.OptionalHeader.ImageBase + this.text.moduleBuilder.unmanagedExports[num5].rva.initializedDataOffset + sdataRVA);
							mw.Write(0);
						}
						num5++;
					}
					num4++;
				}
			}

			// Token: 0x060025DE RID: 9694 RVA: 0x000B4D7C File Offset: 0x000B2F7C
			private static int CompareUnmanagedExportNames(UnmanagedExport x, UnmanagedExport y)
			{
				if (x.name == null)
				{
					if (y.name != null)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (y.name == null)
					{
						return -1;
					}
					return string.CompareOrdinal(x.name, y.name);
				}
			}

			// Token: 0x060025DF RID: 9695 RVA: 0x000B4DAD File Offset: 0x000B2FAD
			private static int CompareUnmanagedExportOrdinals(UnmanagedExport x, UnmanagedExport y)
			{
				return x.ordinal.CompareTo(y.ordinal);
			}

			// Token: 0x060025E0 RID: 9696 RVA: 0x000B4DC4 File Offset: 0x000B2FC4
			internal void GetRelocations(List<TextSection.Relocation> list)
			{
				ushort machine = this.text.peWriter.Headers.FileHeader.Machine;
				ushort type;
				uint num;
				if (machine != 332)
				{
					if (machine != 452)
					{
						if (machine != 34404)
						{
							throw new NotSupportedException();
						}
						type = 40960;
						num = this.stubsRVA + 2U;
					}
					else
					{
						type = 12288;
						num = this.stubsRVA + 12U;
					}
				}
				else
				{
					type = 12288;
					num = this.stubsRVA + 2U;
				}
				int num2 = 0;
				int num3 = 0;
				while ((long)num2 < (long)((ulong)this.entries))
				{
					if ((long)this.text.moduleBuilder.unmanagedExports[num3].ordinal == (long)num2 + (long)((ulong)this.ordinalBase))
					{
						list.Add(new TextSection.Relocation(type, num + (uint)(num3 * (int)this.stubLength)));
						num3++;
					}
					num2++;
				}
			}

			// Token: 0x04000E7A RID: 3706
			private readonly TextSection text;

			// Token: 0x04000E7B RID: 3707
			internal readonly uint entries;

			// Token: 0x04000E7C RID: 3708
			internal readonly uint ordinalBase;

			// Token: 0x04000E7D RID: 3709
			internal readonly uint nameCount;

			// Token: 0x04000E7E RID: 3710
			internal readonly uint namesLength;

			// Token: 0x04000E7F RID: 3711
			internal readonly uint exportAddressTableRVA;

			// Token: 0x04000E80 RID: 3712
			internal readonly uint exportNamePointerTableRVA;

			// Token: 0x04000E81 RID: 3713
			internal readonly uint exportOrdinalTableRVA;

			// Token: 0x04000E82 RID: 3714
			internal readonly uint namesRVA;

			// Token: 0x04000E83 RID: 3715
			internal readonly uint stubsRVA;

			// Token: 0x04000E84 RID: 3716
			private readonly uint stubLength;
		}

		// Token: 0x0200033B RID: 827
		private struct Relocation : IComparable<TextSection.Relocation>
		{
			// Token: 0x060025E1 RID: 9697 RVA: 0x000B4E9C File Offset: 0x000B309C
			internal Relocation(ushort type, uint rva)
			{
				this.type = type;
				this.rva = rva;
			}

			// Token: 0x060025E2 RID: 9698 RVA: 0x000B4EAC File Offset: 0x000B30AC
			int IComparable<TextSection.Relocation>.CompareTo(TextSection.Relocation other)
			{
				return this.rva.CompareTo(other.rva);
			}

			// Token: 0x04000E85 RID: 3717
			internal readonly uint rva;

			// Token: 0x04000E86 RID: 3718
			internal readonly ushort type;
		}

		// Token: 0x0200033C RID: 828
		private struct RelocationBlock
		{
			// Token: 0x060025E3 RID: 9699 RVA: 0x000B4ECD File Offset: 0x000B30CD
			internal RelocationBlock(uint pageRva, ushort[] typeOffset)
			{
				this.PageRVA = pageRva;
				this.TypeOffset = typeOffset;
			}

			// Token: 0x04000E87 RID: 3719
			internal readonly uint PageRVA;

			// Token: 0x04000E88 RID: 3720
			internal readonly ushort[] TypeOffset;
		}
	}
}
