using System;
using System.Collections.Generic;
using System.IO;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200003A RID: 58
	public sealed class MethodBody
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00008C28 File Offset: 0x00006E28
		internal MethodBody(ModuleReader module, int rva, IGenericContext context)
		{
			List<ExceptionHandlingClause> list = new List<ExceptionHandlingClause>();
			List<LocalVariableInfo> list2 = new List<LocalVariableInfo>();
			Stream stream = module.GetStream();
			module.SeekRVA(rva);
			BinaryReader binaryReader = new BinaryReader(stream);
			byte b = binaryReader.ReadByte();
			if ((b & 3) == 2)
			{
				this.initLocals = true;
				this.body = binaryReader.ReadBytes(b >> 2);
				this.maxStack = 8;
			}
			else
			{
				if ((b & 3) != 3)
				{
					throw new BadImageFormatException();
				}
				this.initLocals = ((b & 16) > 0);
				if ((short)((int)b | (int)binaryReader.ReadByte() << 8) >> 12 != 3)
				{
					throw new BadImageFormatException("Fat format method header size should be 3");
				}
				this.maxStack = (int)binaryReader.ReadUInt16();
				int count = binaryReader.ReadInt32();
				this.localVarSigTok = binaryReader.ReadInt32();
				this.body = binaryReader.ReadBytes(count);
				if ((b & 8) != 0)
				{
					Stream stream2 = stream;
					stream2.Position = (stream2.Position + 3L & -4L);
					int num = binaryReader.ReadInt32();
					if ((num & 128) != 0 || (num & 1) == 0)
					{
						throw new NotImplementedException();
					}
					if ((num & 64) != 0)
					{
						int num2 = MethodBody.ComputeExceptionCount(num >> 8 & 16777215, 24);
						for (int i = 0; i < num2; i++)
						{
							int flags = binaryReader.ReadInt32();
							int tryOffset = binaryReader.ReadInt32();
							int tryLength = binaryReader.ReadInt32();
							int handlerOffset = binaryReader.ReadInt32();
							int handlerLength = binaryReader.ReadInt32();
							int classTokenOrfilterOffset = binaryReader.ReadInt32();
							list.Add(new ExceptionHandlingClause(module, flags, tryOffset, tryLength, handlerOffset, handlerLength, classTokenOrfilterOffset, context));
						}
					}
					else
					{
						int num3 = MethodBody.ComputeExceptionCount(num >> 8 & 255, 12);
						for (int j = 0; j < num3; j++)
						{
							int flags2 = (int)binaryReader.ReadUInt16();
							int tryOffset2 = (int)binaryReader.ReadUInt16();
							int tryLength2 = (int)binaryReader.ReadByte();
							int handlerOffset2 = (int)binaryReader.ReadUInt16();
							int handlerLength2 = (int)binaryReader.ReadByte();
							int classTokenOrfilterOffset2 = binaryReader.ReadInt32();
							list.Add(new ExceptionHandlingClause(module, flags2, tryOffset2, tryLength2, handlerOffset2, handlerLength2, classTokenOrfilterOffset2, context));
						}
					}
				}
				if (this.localVarSigTok != 0)
				{
					ByteReader standAloneSig = module.GetStandAloneSig((this.localVarSigTok & 16777215) - 1);
					Signature.ReadLocalVarSig(module, standAloneSig, context, list2);
				}
			}
			this.exceptionClauses = list.AsReadOnly();
			this.locals = list2.AsReadOnly();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008E56 File Offset: 0x00007056
		private static int ComputeExceptionCount(int size, int itemLength)
		{
			return size / itemLength;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008E5B File Offset: 0x0000705B
		public IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return this.exceptionClauses;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008E63 File Offset: 0x00007063
		public bool InitLocals
		{
			get
			{
				return this.initLocals;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008E6B File Offset: 0x0000706B
		public IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008E73 File Offset: 0x00007073
		public byte[] GetILAsByteArray()
		{
			return this.body;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008E7B File Offset: 0x0000707B
		public int LocalSignatureMetadataToken
		{
			get
			{
				return this.localVarSigTok;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008E83 File Offset: 0x00007083
		public int MaxStackSize
		{
			get
			{
				return this.maxStack;
			}
		}

		// Token: 0x0400015B RID: 347
		private readonly IList<ExceptionHandlingClause> exceptionClauses;

		// Token: 0x0400015C RID: 348
		private readonly IList<LocalVariableInfo> locals;

		// Token: 0x0400015D RID: 349
		private readonly bool initLocals;

		// Token: 0x0400015E RID: 350
		private readonly int maxStack;

		// Token: 0x0400015F RID: 351
		private readonly int localVarSigTok;

		// Token: 0x04000160 RID: 352
		private byte[] body;
	}
}
