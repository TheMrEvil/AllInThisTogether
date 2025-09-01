using System;
using System.IO;
using System.Security.Cryptography;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000085 RID: 133
	internal static class ModuleWriter
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00015450 File Offset: 0x00013650
		internal static void WriteModule(StrongNameKeyPair keyPair, byte[] publicKey, ModuleBuilder moduleBuilder, PEFileKinds fileKind, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine, ResourceSection resources, int entryPointToken)
		{
			ModuleWriter.WriteModule(keyPair, publicKey, moduleBuilder, fileKind, portableExecutableKind, imageFileMachine, resources, entryPointToken, null);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00015470 File Offset: 0x00013670
		internal static void WriteModule(StrongNameKeyPair keyPair, byte[] publicKey, ModuleBuilder moduleBuilder, PEFileKinds fileKind, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine, ResourceSection resources, int entryPointToken, Stream stream)
		{
			if (stream == null)
			{
				string fullyQualifiedName = moduleBuilder.FullyQualifiedName;
				bool flag = Type.GetType("Mono.Runtime") != null;
				if (flag)
				{
					try
					{
						File.Delete(fullyQualifiedName);
					}
					catch
					{
					}
				}
				using (FileStream fileStream = new FileStream(fullyQualifiedName, FileMode.Create))
				{
					ModuleWriter.WriteModuleImpl(keyPair, publicKey, moduleBuilder, fileKind, portableExecutableKind, imageFileMachine, resources, entryPointToken, fileStream);
				}
				if (flag)
				{
					File.SetAttributes(fullyQualifiedName, (FileAttributes)(-2147483648));
					return;
				}
			}
			else
			{
				ModuleWriter.WriteModuleImpl(keyPair, publicKey, moduleBuilder, fileKind, portableExecutableKind, imageFileMachine, resources, entryPointToken, stream);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00015508 File Offset: 0x00013708
		private static void WriteModuleImpl(StrongNameKeyPair keyPair, byte[] publicKey, ModuleBuilder moduleBuilder, PEFileKinds fileKind, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine, ResourceSection resources, int entryPointToken, Stream stream)
		{
			moduleBuilder.ApplyUnmanagedExports(imageFileMachine);
			moduleBuilder.FixupMethodBodyTokens();
			int num = moduleBuilder.Guids.Add(moduleBuilder.GetModuleVersionIdOrEmpty());
			moduleBuilder.ModuleTable.Add(0, moduleBuilder.Strings.Add(moduleBuilder.moduleName), num, 0, 0);
			if (moduleBuilder.UserStrings.IsEmpty)
			{
				moduleBuilder.UserStrings.Add(" ");
			}
			if (resources != null)
			{
				resources.Finish();
			}
			PEWriter pewriter = new PEWriter(stream);
			pewriter.Headers.OptionalHeader.FileAlignment = (uint)moduleBuilder.__FileAlignment;
			if (imageFileMachine <= ImageFileMachine.ARM)
			{
				if (imageFileMachine == ImageFileMachine.I386)
				{
					pewriter.Headers.FileHeader.Machine = 332;
					IMAGE_FILE_HEADER fileHeader = pewriter.Headers.FileHeader;
					fileHeader.Characteristics |= 256;
					pewriter.Headers.OptionalHeader.SizeOfStackReserve = moduleBuilder.GetStackReserve(1048576UL);
					goto IL_2D3;
				}
				if (imageFileMachine == ImageFileMachine.ARM)
				{
					pewriter.Headers.FileHeader.Machine = 452;
					IMAGE_FILE_HEADER fileHeader2 = pewriter.Headers.FileHeader;
					fileHeader2.Characteristics |= 288;
					pewriter.Headers.OptionalHeader.SizeOfStackReserve = moduleBuilder.GetStackReserve(1048576UL);
					pewriter.Headers.OptionalHeader.SectionAlignment = 4096U;
					goto IL_2D3;
				}
			}
			else
			{
				if (imageFileMachine == ImageFileMachine.IA64)
				{
					pewriter.Headers.FileHeader.Machine = 512;
					IMAGE_FILE_HEADER fileHeader3 = pewriter.Headers.FileHeader;
					fileHeader3.Characteristics |= 32;
					pewriter.Headers.FileHeader.SizeOfOptionalHeader = 240;
					pewriter.Headers.OptionalHeader.Magic = 523;
					pewriter.Headers.OptionalHeader.SizeOfStackReserve = moduleBuilder.GetStackReserve(4194304UL);
					pewriter.Headers.OptionalHeader.SizeOfStackCommit = 16384UL;
					pewriter.Headers.OptionalHeader.SizeOfHeapCommit = 8192UL;
					goto IL_2D3;
				}
				if (imageFileMachine == ImageFileMachine.AMD64)
				{
					pewriter.Headers.FileHeader.Machine = 34404;
					IMAGE_FILE_HEADER fileHeader4 = pewriter.Headers.FileHeader;
					fileHeader4.Characteristics |= 32;
					pewriter.Headers.FileHeader.SizeOfOptionalHeader = 240;
					pewriter.Headers.OptionalHeader.Magic = 523;
					pewriter.Headers.OptionalHeader.SizeOfStackReserve = moduleBuilder.GetStackReserve(4194304UL);
					pewriter.Headers.OptionalHeader.SizeOfStackCommit = 16384UL;
					pewriter.Headers.OptionalHeader.SizeOfHeapCommit = 8192UL;
					goto IL_2D3;
				}
			}
			throw new ArgumentOutOfRangeException("imageFileMachine");
			IL_2D3:
			if (fileKind == PEFileKinds.Dll)
			{
				IMAGE_FILE_HEADER fileHeader5 = pewriter.Headers.FileHeader;
				fileHeader5.Characteristics |= 8192;
			}
			if (fileKind == PEFileKinds.WindowApplication)
			{
				pewriter.Headers.OptionalHeader.Subsystem = 2;
			}
			else
			{
				pewriter.Headers.OptionalHeader.Subsystem = 3;
			}
			pewriter.Headers.OptionalHeader.DllCharacteristics = (ushort)moduleBuilder.__DllCharacteristics;
			CliHeader cliHeader = new CliHeader();
			cliHeader.Cb = 72U;
			cliHeader.MajorRuntimeVersion = 2;
			cliHeader.MinorRuntimeVersion = ((moduleBuilder.MDStreamVersion < 131072) ? 0 : 5);
			if ((portableExecutableKind & PortableExecutableKinds.ILOnly) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				cliHeader.Flags |= 1U;
			}
			if ((portableExecutableKind & PortableExecutableKinds.Required32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				cliHeader.Flags |= 2U;
			}
			if ((portableExecutableKind & PortableExecutableKinds.Preferred32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				cliHeader.Flags |= 131074U;
			}
			if (keyPair != null)
			{
				cliHeader.Flags |= 8U;
			}
			if (ModuleBuilder.IsPseudoToken(entryPointToken))
			{
				entryPointToken = moduleBuilder.ResolvePseudoToken(entryPointToken);
			}
			cliHeader.EntryPointToken = (uint)entryPointToken;
			moduleBuilder.Strings.Freeze();
			moduleBuilder.UserStrings.Freeze();
			moduleBuilder.Guids.Freeze();
			moduleBuilder.Blobs.Freeze();
			MetadataWriter metadataWriter = new MetadataWriter(moduleBuilder, stream);
			moduleBuilder.Tables.Freeze(metadataWriter);
			TextSection textSection = new TextSection(pewriter, cliHeader, moduleBuilder, ModuleWriter.ComputeStrongNameSignatureLength(publicKey));
			if (textSection.ExportDirectoryLength != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[0].VirtualAddress = textSection.ExportDirectoryRVA;
				pewriter.Headers.OptionalHeader.DataDirectory[0].Size = textSection.ExportDirectoryLength;
			}
			if (textSection.ImportDirectoryLength != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[1].VirtualAddress = textSection.ImportDirectoryRVA;
				pewriter.Headers.OptionalHeader.DataDirectory[1].Size = textSection.ImportDirectoryLength;
			}
			if (textSection.ImportAddressTableLength != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[12].VirtualAddress = textSection.ImportAddressTableRVA;
				pewriter.Headers.OptionalHeader.DataDirectory[12].Size = textSection.ImportAddressTableLength;
			}
			pewriter.Headers.OptionalHeader.DataDirectory[14].VirtualAddress = textSection.ComDescriptorRVA;
			pewriter.Headers.OptionalHeader.DataDirectory[14].Size = textSection.ComDescriptorLength;
			if (textSection.DebugDirectoryLength != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[6].VirtualAddress = textSection.DebugDirectoryRVA;
				pewriter.Headers.OptionalHeader.DataDirectory[6].Size = textSection.DebugDirectoryLength;
			}
			pewriter.Headers.FileHeader.TimeDateStamp = moduleBuilder.GetTimeDateStamp();
			pewriter.Headers.FileHeader.NumberOfSections = 2;
			if (moduleBuilder.initializedData.Length != 0)
			{
				IMAGE_FILE_HEADER fileHeader6 = pewriter.Headers.FileHeader;
				fileHeader6.NumberOfSections += 1;
			}
			if (resources != null)
			{
				IMAGE_FILE_HEADER fileHeader7 = pewriter.Headers.FileHeader;
				fileHeader7.NumberOfSections += 1;
			}
			SectionHeader sectionHeader = new SectionHeader();
			sectionHeader.Name = ".text";
			sectionHeader.VirtualAddress = textSection.BaseRVA;
			sectionHeader.VirtualSize = (uint)textSection.Length;
			sectionHeader.PointerToRawData = textSection.PointerToRawData;
			sectionHeader.SizeOfRawData = pewriter.ToFileAlignment((uint)textSection.Length);
			sectionHeader.Characteristics = 1610612768U;
			SectionHeader sectionHeader2 = new SectionHeader();
			sectionHeader2.Name = ".sdata";
			sectionHeader2.VirtualAddress = sectionHeader.VirtualAddress + pewriter.ToSectionAlignment(sectionHeader.VirtualSize);
			sectionHeader2.VirtualSize = (uint)moduleBuilder.initializedData.Length;
			sectionHeader2.PointerToRawData = sectionHeader.PointerToRawData + sectionHeader.SizeOfRawData;
			sectionHeader2.SizeOfRawData = pewriter.ToFileAlignment((uint)moduleBuilder.initializedData.Length);
			sectionHeader2.Characteristics = 3221225536U;
			SectionHeader sectionHeader3 = new SectionHeader();
			sectionHeader3.Name = ".rsrc";
			sectionHeader3.VirtualAddress = sectionHeader2.VirtualAddress + pewriter.ToSectionAlignment(sectionHeader2.VirtualSize);
			sectionHeader3.PointerToRawData = sectionHeader2.PointerToRawData + sectionHeader2.SizeOfRawData;
			sectionHeader3.VirtualSize = (uint)((resources == null) ? 0 : resources.Length);
			sectionHeader3.SizeOfRawData = pewriter.ToFileAlignment(sectionHeader3.VirtualSize);
			sectionHeader3.Characteristics = 1073741888U;
			if (sectionHeader3.SizeOfRawData != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[2].VirtualAddress = sectionHeader3.VirtualAddress;
				pewriter.Headers.OptionalHeader.DataDirectory[2].Size = sectionHeader3.VirtualSize;
			}
			SectionHeader sectionHeader4 = new SectionHeader();
			sectionHeader4.Name = ".reloc";
			sectionHeader4.VirtualAddress = sectionHeader3.VirtualAddress + pewriter.ToSectionAlignment(sectionHeader3.VirtualSize);
			sectionHeader4.VirtualSize = textSection.PackRelocations();
			sectionHeader4.PointerToRawData = sectionHeader3.PointerToRawData + sectionHeader3.SizeOfRawData;
			sectionHeader4.SizeOfRawData = pewriter.ToFileAlignment(sectionHeader4.VirtualSize);
			sectionHeader4.Characteristics = 1107296320U;
			if (sectionHeader4.SizeOfRawData != 0U)
			{
				pewriter.Headers.OptionalHeader.DataDirectory[5].VirtualAddress = sectionHeader4.VirtualAddress;
				pewriter.Headers.OptionalHeader.DataDirectory[5].Size = sectionHeader4.VirtualSize;
			}
			pewriter.Headers.OptionalHeader.SizeOfCode = sectionHeader.SizeOfRawData;
			pewriter.Headers.OptionalHeader.SizeOfInitializedData = sectionHeader2.SizeOfRawData + sectionHeader3.SizeOfRawData + sectionHeader4.SizeOfRawData;
			pewriter.Headers.OptionalHeader.SizeOfUninitializedData = 0U;
			pewriter.Headers.OptionalHeader.SizeOfImage = sectionHeader4.VirtualAddress + pewriter.ToSectionAlignment(sectionHeader4.VirtualSize);
			pewriter.Headers.OptionalHeader.SizeOfHeaders = sectionHeader.PointerToRawData;
			pewriter.Headers.OptionalHeader.BaseOfCode = textSection.BaseRVA;
			pewriter.Headers.OptionalHeader.BaseOfData = sectionHeader2.VirtualAddress;
			pewriter.Headers.OptionalHeader.ImageBase = (ulong)moduleBuilder.__ImageBase;
			if (imageFileMachine == ImageFileMachine.IA64)
			{
				pewriter.Headers.OptionalHeader.AddressOfEntryPoint = textSection.StartupStubRVA + 32U;
			}
			else
			{
				pewriter.Headers.OptionalHeader.AddressOfEntryPoint = textSection.StartupStubRVA + pewriter.Thumb;
			}
			pewriter.WritePEHeaders();
			pewriter.WriteSectionHeader(sectionHeader);
			if (sectionHeader2.SizeOfRawData != 0U)
			{
				pewriter.WriteSectionHeader(sectionHeader2);
			}
			if (sectionHeader3.SizeOfRawData != 0U)
			{
				pewriter.WriteSectionHeader(sectionHeader3);
			}
			if (sectionHeader4.SizeOfRawData != 0U)
			{
				pewriter.WriteSectionHeader(sectionHeader4);
			}
			stream.Seek((long)((ulong)sectionHeader.PointerToRawData), SeekOrigin.Begin);
			int num2;
			textSection.Write(metadataWriter, sectionHeader2.VirtualAddress, out num2);
			if (sectionHeader2.SizeOfRawData != 0U)
			{
				stream.Seek((long)((ulong)sectionHeader2.PointerToRawData), SeekOrigin.Begin);
				metadataWriter.Write(moduleBuilder.initializedData);
			}
			if (sectionHeader3.SizeOfRawData != 0U)
			{
				stream.Seek((long)((ulong)sectionHeader3.PointerToRawData), SeekOrigin.Begin);
				resources.Write(metadataWriter, sectionHeader3.VirtualAddress);
			}
			if (sectionHeader4.SizeOfRawData != 0U)
			{
				stream.Seek((long)((ulong)sectionHeader4.PointerToRawData), SeekOrigin.Begin);
				textSection.WriteRelocations(metadataWriter);
			}
			stream.SetLength((long)((ulong)(sectionHeader4.PointerToRawData + sectionHeader4.SizeOfRawData)));
			if (moduleBuilder.universe.Deterministic && moduleBuilder.GetModuleVersionIdOrEmpty() == Guid.Empty)
			{
				Guid guid = ModuleWriter.GenerateModuleVersionId(stream);
				stream.Position = (long)(num2 + (num - 1) * 16);
				stream.Write(guid.ToByteArray(), 0, 16);
				moduleBuilder.__SetModuleVersionId(guid);
			}
			if (keyPair != null)
			{
				ModuleWriter.StrongName(stream, keyPair, pewriter.HeaderSize, sectionHeader.PointerToRawData, textSection.StrongNameSignatureRVA - sectionHeader.VirtualAddress + sectionHeader.PointerToRawData, textSection.StrongNameSignatureLength);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00015FFC File Offset: 0x000141FC
		private static int ComputeStrongNameSignatureLength(byte[] publicKey)
		{
			if (publicKey == null)
			{
				return 0;
			}
			if (publicKey.Length == 16)
			{
				return 128;
			}
			return publicKey.Length - 32;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00016018 File Offset: 0x00014218
		private static void StrongName(Stream stream, StrongNameKeyPair keyPair, uint headerLength, uint textSectionFileOffset, uint strongNameSignatureFileOffset, uint strongNameSignatureLength)
		{
			SHA1Managed sha1Managed = new SHA1Managed();
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, CryptoStreamMode.Write))
			{
				stream.Seek(0L, SeekOrigin.Begin);
				byte[] buf = new byte[8192];
				ModuleWriter.HashChunk(stream, cryptoStream, buf, (int)headerLength);
				stream.Seek((long)((ulong)textSectionFileOffset), SeekOrigin.Begin);
				ModuleWriter.HashChunk(stream, cryptoStream, buf, (int)(strongNameSignatureFileOffset - textSectionFileOffset));
				stream.Seek((long)((ulong)strongNameSignatureLength), SeekOrigin.Current);
				ModuleWriter.HashChunk(stream, cryptoStream, buf, (int)(stream.Length - (long)((ulong)(strongNameSignatureFileOffset + strongNameSignatureLength))));
			}
			using (RSA rsa = keyPair.CreateRSA())
			{
				byte[] array = new RSAPKCS1SignatureFormatter(rsa).CreateSignature(sha1Managed);
				Array.Reverse(array);
				if ((long)array.Length != (long)((ulong)strongNameSignatureLength))
				{
					throw new InvalidOperationException("Signature length mismatch");
				}
				stream.Seek((long)((ulong)strongNameSignatureFileOffset), SeekOrigin.Begin);
				stream.Write(array, 0, array.Length);
			}
			stream.Seek(0L, SeekOrigin.Begin);
			int num = (int)stream.Length / 4;
			BinaryReader binaryReader = new BinaryReader(stream);
			long num2 = 0L;
			for (int i = 0; i < num; i++)
			{
				num2 += (long)((ulong)binaryReader.ReadUInt32());
				int num3 = (int)(num2 >> 32);
				num2 &= (long)((ulong)-1);
				num2 += (long)num3;
			}
			while (num2 >> 16 != 0L)
			{
				num2 = (num2 & 65535L) + (num2 >> 16);
			}
			num2 += stream.Length;
			ByteBuffer byteBuffer = new ByteBuffer(4);
			byteBuffer.Write((int)num2);
			stream.Seek(216L, SeekOrigin.Begin);
			byteBuffer.WriteTo(stream);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000161A4 File Offset: 0x000143A4
		internal static void HashChunk(Stream stream, CryptoStream cs, byte[] buf, int length)
		{
			while (length > 0)
			{
				int num = stream.Read(buf, 0, Math.Min(buf.Length, length));
				cs.Write(buf, 0, num);
				length -= num;
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000161D8 File Offset: 0x000143D8
		private static Guid GenerateModuleVersionId(Stream stream)
		{
			SHA1Managed sha1Managed = new SHA1Managed();
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, CryptoStreamMode.Write))
			{
				stream.Seek(0L, SeekOrigin.Begin);
				byte[] buf = new byte[8192];
				ModuleWriter.HashChunk(stream, cryptoStream, buf, (int)stream.Length);
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(sha1Managed.Hash, 0, array, 0, array.Length);
			byte[] array2 = array;
			int num = 7;
			array2[num] &= 15;
			byte[] array3 = array;
			int num2 = 7;
			array3[num2] |= 64;
			byte[] array4 = array;
			int num3 = 8;
			array4[num3] &= 63;
			byte[] array5 = array;
			int num4 = 8;
			array5[num4] |= 128;
			return new Guid(array);
		}
	}
}
