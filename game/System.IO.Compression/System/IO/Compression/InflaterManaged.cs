using System;

namespace System.IO.Compression
{
	// Token: 0x02000020 RID: 32
	internal sealed class InflaterManaged
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00005078 File Offset: 0x00003278
		internal InflaterManaged(IFileFormatReader reader, bool deflate64)
		{
			this._output = new OutputWindow();
			this._input = new InputBuffer();
			this._codeList = new byte[320];
			this._codeLengthTreeCodeLength = new byte[19];
			this._deflate64 = deflate64;
			if (reader != null)
			{
				this._formatReader = reader;
				this._hasFormatReader = true;
			}
			this.Reset();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000050E8 File Offset: 0x000032E8
		private void Reset()
		{
			this._state = (this._hasFormatReader ? InflaterState.ReadingHeader : InflaterState.ReadingBFinal);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000050FC File Offset: 0x000032FC
		public void SetInput(byte[] inputBytes, int offset, int length)
		{
			this._input.SetInput(inputBytes, offset, length);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000510C File Offset: 0x0000330C
		public bool Finished()
		{
			return this._state == InflaterState.Done || this._state == InflaterState.VerifyingFooter;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005124 File Offset: 0x00003324
		public int AvailableOutput
		{
			get
			{
				return this._output.AvailableBytes;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005134 File Offset: 0x00003334
		public int Inflate(byte[] bytes, int offset, int length)
		{
			int num = 0;
			do
			{
				int num2 = this._output.CopyTo(bytes, offset, length);
				if (num2 > 0)
				{
					if (this._hasFormatReader)
					{
						this._formatReader.UpdateWithBytesRead(bytes, offset, num2);
					}
					offset += num2;
					num += num2;
					length -= num2;
				}
			}
			while (length != 0 && !this.Finished() && this.Decode());
			if (this._state == InflaterState.VerifyingFooter && this._output.AvailableBytes == 0)
			{
				this._formatReader.Validate();
			}
			return num;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000051B0 File Offset: 0x000033B0
		private bool Decode()
		{
			bool flag = false;
			if (this.Finished())
			{
				return true;
			}
			if (this._hasFormatReader)
			{
				if (this._state == InflaterState.ReadingHeader)
				{
					if (!this._formatReader.ReadHeader(this._input))
					{
						return false;
					}
					this._state = InflaterState.ReadingBFinal;
				}
				else if (this._state == InflaterState.StartReadingFooter || this._state == InflaterState.ReadingFooter)
				{
					if (!this._formatReader.ReadFooter(this._input))
					{
						return false;
					}
					this._state = InflaterState.VerifyingFooter;
					return true;
				}
			}
			if (this._state == InflaterState.ReadingBFinal)
			{
				if (!this._input.EnsureBitsAvailable(1))
				{
					return false;
				}
				this._bfinal = this._input.GetBits(1);
				this._state = InflaterState.ReadingBType;
			}
			if (this._state == InflaterState.ReadingBType)
			{
				if (!this._input.EnsureBitsAvailable(2))
				{
					this._state = InflaterState.ReadingBType;
					return false;
				}
				this._blockType = (BlockType)this._input.GetBits(2);
				if (this._blockType == BlockType.Dynamic)
				{
					this._state = InflaterState.ReadingNumLitCodes;
				}
				else if (this._blockType == BlockType.Static)
				{
					this._literalLengthTree = HuffmanTree.StaticLiteralLengthTree;
					this._distanceTree = HuffmanTree.StaticDistanceTree;
					this._state = InflaterState.DecodeTop;
				}
				else
				{
					if (this._blockType != BlockType.Uncompressed)
					{
						throw new InvalidDataException("Unknown block type. Stream might be corrupted.");
					}
					this._state = InflaterState.UncompressedAligning;
				}
			}
			bool result;
			if (this._blockType == BlockType.Dynamic)
			{
				if (this._state < InflaterState.DecodeTop)
				{
					result = this.DecodeDynamicBlockHeader();
				}
				else
				{
					result = this.DecodeBlock(out flag);
				}
			}
			else if (this._blockType == BlockType.Static)
			{
				result = this.DecodeBlock(out flag);
			}
			else
			{
				if (this._blockType != BlockType.Uncompressed)
				{
					throw new InvalidDataException("Unknown block type. Stream might be corrupted.");
				}
				result = this.DecodeUncompressedBlock(out flag);
			}
			if (flag && this._bfinal != 0)
			{
				if (this._hasFormatReader)
				{
					this._state = InflaterState.StartReadingFooter;
				}
				else
				{
					this._state = InflaterState.Done;
				}
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000536C File Offset: 0x0000356C
		private bool DecodeUncompressedBlock(out bool end_of_block)
		{
			end_of_block = false;
			for (;;)
			{
				switch (this._state)
				{
				case InflaterState.UncompressedAligning:
					this._input.SkipToByteBoundary();
					this._state = InflaterState.UncompressedByte1;
					goto IL_43;
				case InflaterState.UncompressedByte1:
				case InflaterState.UncompressedByte2:
				case InflaterState.UncompressedByte3:
				case InflaterState.UncompressedByte4:
					goto IL_43;
				case InflaterState.DecodingUncompressed:
					goto IL_D1;
				}
				break;
				IL_43:
				int bits = this._input.GetBits(8);
				if (bits < 0)
				{
					return false;
				}
				this._blockLengthBuffer[this._state - InflaterState.UncompressedByte1] = (byte)bits;
				if (this._state == InflaterState.UncompressedByte4)
				{
					this._blockLength = (int)this._blockLengthBuffer[0] + (int)this._blockLengthBuffer[1] * 256;
					int num = (int)this._blockLengthBuffer[2] + (int)this._blockLengthBuffer[3] * 256;
					if ((ushort)this._blockLength != (ushort)(~(ushort)num))
					{
						goto Block_4;
					}
				}
				this._state++;
			}
			throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
			Block_4:
			throw new InvalidDataException("Block length does not match with its complement.");
			IL_D1:
			int num2 = this._output.CopyFrom(this._input, this._blockLength);
			this._blockLength -= num2;
			if (this._blockLength == 0)
			{
				this._state = InflaterState.ReadingBFinal;
				end_of_block = true;
				return true;
			}
			return this._output.FreeBytes == 0;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000054A0 File Offset: 0x000036A0
		private bool DecodeBlock(out bool end_of_block_code_seen)
		{
			end_of_block_code_seen = false;
			int i = this._output.FreeBytes;
			while (i > 65536)
			{
				switch (this._state)
				{
				case InflaterState.DecodeTop:
				{
					int num = this._literalLengthTree.GetNextSymbol(this._input);
					if (num < 0)
					{
						return false;
					}
					if (num < 256)
					{
						this._output.Write((byte)num);
						i--;
						continue;
					}
					if (num == 256)
					{
						end_of_block_code_seen = true;
						this._state = InflaterState.ReadingBFinal;
						return true;
					}
					num -= 257;
					if (num < 8)
					{
						num += 3;
						this._extraBits = 0;
					}
					else if (!this._deflate64 && num == 28)
					{
						num = 258;
						this._extraBits = 0;
					}
					else
					{
						if (num < 0 || num >= InflaterManaged.s_extraLengthBits.Length)
						{
							throw new InvalidDataException("Found invalid data while decoding.");
						}
						this._extraBits = (int)InflaterManaged.s_extraLengthBits[num];
					}
					this._length = num;
					goto IL_E5;
				}
				case InflaterState.HaveInitialLength:
					goto IL_E5;
				case InflaterState.HaveFullLength:
					goto IL_150;
				case InflaterState.HaveDistCode:
					break;
				default:
					throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
				}
				IL_1B2:
				int distance;
				if (this._distanceCode > 3)
				{
					this._extraBits = this._distanceCode - 2 >> 1;
					int bits = this._input.GetBits(this._extraBits);
					if (bits < 0)
					{
						return false;
					}
					distance = InflaterManaged.s_distanceBasePosition[this._distanceCode] + bits;
				}
				else
				{
					distance = this._distanceCode + 1;
				}
				this._output.WriteLengthDistance(this._length, distance);
				i -= this._length;
				this._state = InflaterState.DecodeTop;
				continue;
				IL_150:
				if (this._blockType == BlockType.Dynamic)
				{
					this._distanceCode = this._distanceTree.GetNextSymbol(this._input);
				}
				else
				{
					this._distanceCode = this._input.GetBits(5);
					if (this._distanceCode >= 0)
					{
						this._distanceCode = (int)InflaterManaged.s_staticDistanceTreeTable[this._distanceCode];
					}
				}
				if (this._distanceCode < 0)
				{
					return false;
				}
				this._state = InflaterState.HaveDistCode;
				goto IL_1B2;
				IL_E5:
				if (this._extraBits > 0)
				{
					this._state = InflaterState.HaveInitialLength;
					int bits2 = this._input.GetBits(this._extraBits);
					if (bits2 < 0)
					{
						return false;
					}
					if (this._length < 0 || this._length >= InflaterManaged.s_lengthBase.Length)
					{
						throw new InvalidDataException("Found invalid data while decoding.");
					}
					this._length = InflaterManaged.s_lengthBase[this._length] + bits2;
				}
				this._state = InflaterState.HaveFullLength;
				goto IL_150;
			}
			return true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000056EC File Offset: 0x000038EC
		private bool DecodeDynamicBlockHeader()
		{
			switch (this._state)
			{
			case InflaterState.ReadingNumLitCodes:
				this._literalLengthCodeCount = this._input.GetBits(5);
				if (this._literalLengthCodeCount < 0)
				{
					return false;
				}
				this._literalLengthCodeCount += 257;
				this._state = InflaterState.ReadingNumDistCodes;
				goto IL_62;
			case InflaterState.ReadingNumDistCodes:
				goto IL_62;
			case InflaterState.ReadingNumCodeLengthCodes:
				goto IL_94;
			case InflaterState.ReadingCodeLengthCodes:
				break;
			case InflaterState.ReadingTreeCodesBefore:
			case InflaterState.ReadingTreeCodesAfter:
				goto IL_35A;
			default:
				throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
			}
			IL_105:
			while (this._loopCounter < this._codeLengthCodeCount)
			{
				int bits = this._input.GetBits(3);
				if (bits < 0)
				{
					return false;
				}
				this._codeLengthTreeCodeLength[(int)InflaterManaged.s_codeOrder[this._loopCounter]] = (byte)bits;
				this._loopCounter++;
			}
			for (int i = this._codeLengthCodeCount; i < InflaterManaged.s_codeOrder.Length; i++)
			{
				this._codeLengthTreeCodeLength[(int)InflaterManaged.s_codeOrder[i]] = 0;
			}
			this._codeLengthTree = new HuffmanTree(this._codeLengthTreeCodeLength);
			this._codeArraySize = this._literalLengthCodeCount + this._distanceCodeCount;
			this._loopCounter = 0;
			this._state = InflaterState.ReadingTreeCodesBefore;
			IL_35A:
			while (this._loopCounter < this._codeArraySize)
			{
				if (this._state == InflaterState.ReadingTreeCodesBefore && (this._lengthCode = this._codeLengthTree.GetNextSymbol(this._input)) < 0)
				{
					return false;
				}
				if (this._lengthCode <= 15)
				{
					byte[] codeList = this._codeList;
					int loopCounter = this._loopCounter;
					this._loopCounter = loopCounter + 1;
					codeList[loopCounter] = (byte)this._lengthCode;
				}
				else if (this._lengthCode == 16)
				{
					if (!this._input.EnsureBitsAvailable(2))
					{
						this._state = InflaterState.ReadingTreeCodesAfter;
						return false;
					}
					if (this._loopCounter == 0)
					{
						throw new InvalidDataException();
					}
					byte b = this._codeList[this._loopCounter - 1];
					int num = this._input.GetBits(2) + 3;
					if (this._loopCounter + num > this._codeArraySize)
					{
						throw new InvalidDataException();
					}
					for (int j = 0; j < num; j++)
					{
						byte[] codeList2 = this._codeList;
						int loopCounter = this._loopCounter;
						this._loopCounter = loopCounter + 1;
						codeList2[loopCounter] = b;
					}
				}
				else if (this._lengthCode == 17)
				{
					if (!this._input.EnsureBitsAvailable(3))
					{
						this._state = InflaterState.ReadingTreeCodesAfter;
						return false;
					}
					int num = this._input.GetBits(3) + 3;
					if (this._loopCounter + num > this._codeArraySize)
					{
						throw new InvalidDataException();
					}
					for (int k = 0; k < num; k++)
					{
						byte[] codeList3 = this._codeList;
						int loopCounter = this._loopCounter;
						this._loopCounter = loopCounter + 1;
						codeList3[loopCounter] = 0;
					}
				}
				else
				{
					if (!this._input.EnsureBitsAvailable(7))
					{
						this._state = InflaterState.ReadingTreeCodesAfter;
						return false;
					}
					int num = this._input.GetBits(7) + 11;
					if (this._loopCounter + num > this._codeArraySize)
					{
						throw new InvalidDataException();
					}
					for (int l = 0; l < num; l++)
					{
						byte[] codeList4 = this._codeList;
						int loopCounter = this._loopCounter;
						this._loopCounter = loopCounter + 1;
						codeList4[loopCounter] = 0;
					}
				}
				this._state = InflaterState.ReadingTreeCodesBefore;
			}
			byte[] array = new byte[288];
			byte[] array2 = new byte[32];
			Array.Copy(this._codeList, 0, array, 0, this._literalLengthCodeCount);
			Array.Copy(this._codeList, this._literalLengthCodeCount, array2, 0, this._distanceCodeCount);
			if (array[256] == 0)
			{
				throw new InvalidDataException();
			}
			this._literalLengthTree = new HuffmanTree(array);
			this._distanceTree = new HuffmanTree(array2);
			this._state = InflaterState.DecodeTop;
			return true;
			IL_62:
			this._distanceCodeCount = this._input.GetBits(5);
			if (this._distanceCodeCount < 0)
			{
				return false;
			}
			this._distanceCodeCount++;
			this._state = InflaterState.ReadingNumCodeLengthCodes;
			IL_94:
			this._codeLengthCodeCount = this._input.GetBits(4);
			if (this._codeLengthCodeCount < 0)
			{
				return false;
			}
			this._codeLengthCodeCount += 4;
			this._loopCounter = 0;
			this._state = InflaterState.ReadingCodeLengthCodes;
			goto IL_105;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000042E9 File Offset: 0x000024E9
		public void Dispose()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// Note: this type is marked as 'beforefieldinit'.
		static InflaterManaged()
		{
		}

		// Token: 0x0400011B RID: 283
		private static readonly byte[] s_extraLengthBits = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			16
		};

		// Token: 0x0400011C RID: 284
		private static readonly int[] s_lengthBase = new int[]
		{
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			13,
			15,
			17,
			19,
			23,
			27,
			31,
			35,
			43,
			51,
			59,
			67,
			83,
			99,
			115,
			131,
			163,
			195,
			227,
			3
		};

		// Token: 0x0400011D RID: 285
		private static readonly int[] s_distanceBasePosition = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			7,
			9,
			13,
			17,
			25,
			33,
			49,
			65,
			97,
			129,
			193,
			257,
			385,
			513,
			769,
			1025,
			1537,
			2049,
			3073,
			4097,
			6145,
			8193,
			12289,
			16385,
			24577,
			32769,
			49153
		};

		// Token: 0x0400011E RID: 286
		private static readonly byte[] s_codeOrder = new byte[]
		{
			16,
			17,
			18,
			0,
			8,
			7,
			9,
			6,
			10,
			5,
			11,
			4,
			12,
			3,
			13,
			2,
			14,
			1,
			15
		};

		// Token: 0x0400011F RID: 287
		private static readonly byte[] s_staticDistanceTreeTable = new byte[]
		{
			0,
			16,
			8,
			24,
			4,
			20,
			12,
			28,
			2,
			18,
			10,
			26,
			6,
			22,
			14,
			30,
			1,
			17,
			9,
			25,
			5,
			21,
			13,
			29,
			3,
			19,
			11,
			27,
			7,
			23,
			15,
			31
		};

		// Token: 0x04000120 RID: 288
		private readonly OutputWindow _output;

		// Token: 0x04000121 RID: 289
		private readonly InputBuffer _input;

		// Token: 0x04000122 RID: 290
		private HuffmanTree _literalLengthTree;

		// Token: 0x04000123 RID: 291
		private HuffmanTree _distanceTree;

		// Token: 0x04000124 RID: 292
		private InflaterState _state;

		// Token: 0x04000125 RID: 293
		private bool _hasFormatReader;

		// Token: 0x04000126 RID: 294
		private int _bfinal;

		// Token: 0x04000127 RID: 295
		private BlockType _blockType;

		// Token: 0x04000128 RID: 296
		private readonly byte[] _blockLengthBuffer = new byte[4];

		// Token: 0x04000129 RID: 297
		private int _blockLength;

		// Token: 0x0400012A RID: 298
		private int _length;

		// Token: 0x0400012B RID: 299
		private int _distanceCode;

		// Token: 0x0400012C RID: 300
		private int _extraBits;

		// Token: 0x0400012D RID: 301
		private int _loopCounter;

		// Token: 0x0400012E RID: 302
		private int _literalLengthCodeCount;

		// Token: 0x0400012F RID: 303
		private int _distanceCodeCount;

		// Token: 0x04000130 RID: 304
		private int _codeLengthCodeCount;

		// Token: 0x04000131 RID: 305
		private int _codeArraySize;

		// Token: 0x04000132 RID: 306
		private int _lengthCode;

		// Token: 0x04000133 RID: 307
		private readonly byte[] _codeList;

		// Token: 0x04000134 RID: 308
		private readonly byte[] _codeLengthTreeCodeLength;

		// Token: 0x04000135 RID: 309
		private readonly bool _deflate64;

		// Token: 0x04000136 RID: 310
		private HuffmanTree _codeLengthTree;

		// Token: 0x04000137 RID: 311
		private IFileFormatReader _formatReader;
	}
}
