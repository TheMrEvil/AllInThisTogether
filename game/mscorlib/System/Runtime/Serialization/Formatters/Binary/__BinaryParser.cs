using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006B7 RID: 1719
	internal sealed class __BinaryParser
	{
		// Token: 0x06003F76 RID: 16246 RVA: 0x000DD698 File Offset: 0x000DB898
		internal __BinaryParser(Stream stream, ObjectReader objectReader)
		{
			this.input = stream;
			this.objectReader = objectReader;
			this.dataReader = new BinaryReader(this.input, __BinaryParser.encoding);
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000DD6E6 File Offset: 0x000DB8E6
		internal BinaryAssemblyInfo SystemAssemblyInfo
		{
			get
			{
				if (this.systemAssemblyInfo == null)
				{
					this.systemAssemblyInfo = new BinaryAssemblyInfo(Converter.urtAssemblyString, Converter.urtAssembly);
				}
				return this.systemAssemblyInfo;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000DD70B File Offset: 0x000DB90B
		internal SizedArray ObjectMapIdTable
		{
			get
			{
				if (this.objectMapIdTable == null)
				{
					this.objectMapIdTable = new SizedArray();
				}
				return this.objectMapIdTable;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000DD726 File Offset: 0x000DB926
		internal SizedArray AssemIdToAssemblyTable
		{
			get
			{
				if (this.assemIdToAssemblyTable == null)
				{
					this.assemIdToAssemblyTable = new SizedArray(2);
				}
				return this.assemIdToAssemblyTable;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000DD742 File Offset: 0x000DB942
		internal ParseRecord prs
		{
			get
			{
				if (this.PRS == null)
				{
					this.PRS = new ParseRecord();
				}
				return this.PRS;
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000DD760 File Offset: 0x000DB960
		[SecurityCritical]
		internal void Run()
		{
			try
			{
				bool flag = true;
				this.ReadBegin();
				this.ReadSerializationHeaderRecord();
				while (flag)
				{
					BinaryHeaderEnum binaryHeaderEnum = BinaryHeaderEnum.Object;
					BinaryTypeEnum binaryTypeEnum = this.expectedType;
					if (binaryTypeEnum != BinaryTypeEnum.Primitive)
					{
						if (binaryTypeEnum - BinaryTypeEnum.String > 6)
						{
							throw new SerializationException(Environment.GetResourceString("Invalid expected type."));
						}
						byte b = this.dataReader.ReadByte();
						binaryHeaderEnum = (BinaryHeaderEnum)b;
						switch (binaryHeaderEnum)
						{
						case BinaryHeaderEnum.Object:
							this.ReadObject();
							break;
						case BinaryHeaderEnum.ObjectWithMap:
						case BinaryHeaderEnum.ObjectWithMapAssemId:
							this.ReadObjectWithMap(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.ObjectWithMapTyped:
						case BinaryHeaderEnum.ObjectWithMapTypedAssemId:
							this.ReadObjectWithMapTyped(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.ObjectString:
						case BinaryHeaderEnum.CrossAppDomainString:
							this.ReadObjectString(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.Array:
						case BinaryHeaderEnum.ArraySinglePrimitive:
						case BinaryHeaderEnum.ArraySingleObject:
						case BinaryHeaderEnum.ArraySingleString:
							this.ReadArray(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.MemberPrimitiveTyped:
							this.ReadMemberPrimitiveTyped();
							break;
						case BinaryHeaderEnum.MemberReference:
							this.ReadMemberReference();
							break;
						case BinaryHeaderEnum.ObjectNull:
						case BinaryHeaderEnum.ObjectNullMultiple256:
						case BinaryHeaderEnum.ObjectNullMultiple:
							this.ReadObjectNull(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.MessageEnd:
							flag = false;
							this.ReadMessageEnd();
							this.ReadEnd();
							break;
						case BinaryHeaderEnum.Assembly:
						case BinaryHeaderEnum.CrossAppDomainAssembly:
							this.ReadAssembly(binaryHeaderEnum);
							break;
						case BinaryHeaderEnum.CrossAppDomainMap:
							this.ReadCrossAppDomainMap();
							break;
						case BinaryHeaderEnum.MethodCall:
						case BinaryHeaderEnum.MethodReturn:
							this.ReadMethodObject(binaryHeaderEnum);
							break;
						default:
							throw new SerializationException(Environment.GetResourceString("Binary stream '{0}' does not contain a valid BinaryHeader. Possible causes are invalid stream or object version change between serialization and deserialization.", new object[]
							{
								b
							}));
						}
					}
					else
					{
						this.ReadMemberPrimitiveUnTyped();
					}
					if (binaryHeaderEnum != BinaryHeaderEnum.Assembly)
					{
						bool flag2 = false;
						while (!flag2)
						{
							ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
							if (objectProgress == null)
							{
								this.expectedType = BinaryTypeEnum.ObjectUrt;
								this.expectedTypeInformation = null;
								flag2 = true;
							}
							else
							{
								flag2 = objectProgress.GetNext(out objectProgress.expectedType, out objectProgress.expectedTypeInformation);
								this.expectedType = objectProgress.expectedType;
								this.expectedTypeInformation = objectProgress.expectedTypeInformation;
								if (!flag2)
								{
									this.prs.Init();
									if (objectProgress.memberValueEnum == InternalMemberValueE.Nested)
									{
										this.prs.PRparseTypeEnum = InternalParseTypeE.MemberEnd;
										this.prs.PRmemberTypeEnum = objectProgress.memberTypeEnum;
										this.prs.PRmemberValueEnum = objectProgress.memberValueEnum;
										this.objectReader.Parse(this.prs);
									}
									else
									{
										this.prs.PRparseTypeEnum = InternalParseTypeE.ObjectEnd;
										this.prs.PRmemberTypeEnum = objectProgress.memberTypeEnum;
										this.prs.PRmemberValueEnum = objectProgress.memberValueEnum;
										this.objectReader.Parse(this.prs);
									}
									this.stack.Pop();
									this.PutOp(objectProgress);
								}
							}
						}
					}
				}
			}
			catch (EndOfStreamException)
			{
				throw new SerializationException(Environment.GetResourceString("End of Stream encountered before parsing was completed."));
			}
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void ReadBegin()
		{
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void ReadEnd()
		{
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000DDA18 File Offset: 0x000DBC18
		internal bool ReadBoolean()
		{
			return this.dataReader.ReadBoolean();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000DDA25 File Offset: 0x000DBC25
		internal byte ReadByte()
		{
			return this.dataReader.ReadByte();
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000DDA32 File Offset: 0x000DBC32
		internal byte[] ReadBytes(int length)
		{
			return this.dataReader.ReadBytes(length);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000DDA40 File Offset: 0x000DBC40
		internal void ReadBytes(byte[] byteA, int offset, int size)
		{
			while (size > 0)
			{
				int num = this.dataReader.Read(byteA, offset, size);
				if (num == 0)
				{
					__Error.EndOfFile();
				}
				offset += num;
				size -= num;
			}
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000DDA74 File Offset: 0x000DBC74
		internal char ReadChar()
		{
			return this.dataReader.ReadChar();
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000DDA81 File Offset: 0x000DBC81
		internal char[] ReadChars(int length)
		{
			return this.dataReader.ReadChars(length);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000DDA8F File Offset: 0x000DBC8F
		internal decimal ReadDecimal()
		{
			return decimal.Parse(this.dataReader.ReadString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000DDAA6 File Offset: 0x000DBCA6
		internal float ReadSingle()
		{
			return this.dataReader.ReadSingle();
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000DDAB3 File Offset: 0x000DBCB3
		internal double ReadDouble()
		{
			return this.dataReader.ReadDouble();
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x000DDAC0 File Offset: 0x000DBCC0
		internal short ReadInt16()
		{
			return this.dataReader.ReadInt16();
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000DDACD File Offset: 0x000DBCCD
		internal int ReadInt32()
		{
			return this.dataReader.ReadInt32();
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000DDADA File Offset: 0x000DBCDA
		internal long ReadInt64()
		{
			return this.dataReader.ReadInt64();
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000DDAE7 File Offset: 0x000DBCE7
		internal sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000DDAF0 File Offset: 0x000DBCF0
		internal string ReadString()
		{
			return this.dataReader.ReadString();
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000DDAFD File Offset: 0x000DBCFD
		internal TimeSpan ReadTimeSpan()
		{
			return new TimeSpan(this.ReadInt64());
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x000DDB0A File Offset: 0x000DBD0A
		internal DateTime ReadDateTime()
		{
			return DateTime.FromBinaryRaw(this.ReadInt64());
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000DDB17 File Offset: 0x000DBD17
		internal ushort ReadUInt16()
		{
			return this.dataReader.ReadUInt16();
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000DDB24 File Offset: 0x000DBD24
		internal uint ReadUInt32()
		{
			return this.dataReader.ReadUInt32();
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000DDB31 File Offset: 0x000DBD31
		internal ulong ReadUInt64()
		{
			return this.dataReader.ReadUInt64();
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000DDB40 File Offset: 0x000DBD40
		[SecurityCritical]
		internal void ReadSerializationHeaderRecord()
		{
			SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord();
			serializationHeaderRecord.Read(this);
			serializationHeaderRecord.Dump();
			this.topId = ((serializationHeaderRecord.topId > 0) ? this.objectReader.GetId((long)serializationHeaderRecord.topId) : ((long)serializationHeaderRecord.topId));
			this.headerId = ((serializationHeaderRecord.headerId > 0) ? this.objectReader.GetId((long)serializationHeaderRecord.headerId) : ((long)serializationHeaderRecord.headerId));
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000DDBB4 File Offset: 0x000DBDB4
		[SecurityCritical]
		internal void ReadAssembly(BinaryHeaderEnum binaryHeaderEnum)
		{
			BinaryAssembly binaryAssembly = new BinaryAssembly();
			if (binaryHeaderEnum == BinaryHeaderEnum.CrossAppDomainAssembly)
			{
				BinaryCrossAppDomainAssembly binaryCrossAppDomainAssembly = new BinaryCrossAppDomainAssembly();
				binaryCrossAppDomainAssembly.Read(this);
				binaryCrossAppDomainAssembly.Dump();
				binaryAssembly.assemId = binaryCrossAppDomainAssembly.assemId;
				binaryAssembly.assemblyString = (this.objectReader.CrossAppDomainArray(binaryCrossAppDomainAssembly.assemblyIndex) as string);
				if (binaryAssembly.assemblyString == null)
				{
					throw new SerializationException(Environment.GetResourceString("Cross-AppDomain BinaryFormatter error; expected '{0}' but received '{1}'.", new object[]
					{
						"String",
						binaryCrossAppDomainAssembly.assemblyIndex
					}));
				}
			}
			else
			{
				binaryAssembly.Read(this);
				binaryAssembly.Dump();
			}
			this.AssemIdToAssemblyTable[binaryAssembly.assemId] = new BinaryAssemblyInfo(binaryAssembly.assemblyString);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000DDC64 File Offset: 0x000DBE64
		[SecurityCritical]
		internal void ReadMethodObject(BinaryHeaderEnum binaryHeaderEnum)
		{
			if (binaryHeaderEnum == BinaryHeaderEnum.MethodCall)
			{
				BinaryMethodCall binaryMethodCall = new BinaryMethodCall();
				binaryMethodCall.Read(this);
				binaryMethodCall.Dump();
				this.objectReader.SetMethodCall(binaryMethodCall);
				return;
			}
			BinaryMethodReturn binaryMethodReturn = new BinaryMethodReturn();
			binaryMethodReturn.Read(this);
			binaryMethodReturn.Dump();
			this.objectReader.SetMethodReturn(binaryMethodReturn);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000DDCB8 File Offset: 0x000DBEB8
		[SecurityCritical]
		private void ReadObject()
		{
			if (this.binaryObject == null)
			{
				this.binaryObject = new BinaryObject();
			}
			this.binaryObject.Read(this);
			this.binaryObject.Dump();
			ObjectMap objectMap = (ObjectMap)this.ObjectMapIdTable[this.binaryObject.mapId];
			if (objectMap == null)
			{
				throw new SerializationException(Environment.GetResourceString("No map for object '{0}'.", new object[]
				{
					this.binaryObject.mapId
				}));
			}
			ObjectProgress op = this.GetOp();
			ParseRecord pr = op.pr;
			this.stack.Push(op);
			op.objectTypeEnum = InternalObjectTypeE.Object;
			op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
			op.memberNames = objectMap.memberNames;
			op.memberTypes = objectMap.memberTypes;
			op.typeInformationA = objectMap.typeInformationA;
			op.memberLength = op.binaryTypeEnumA.Length;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.PeekPeek();
			if (objectProgress == null || objectProgress.isInitial)
			{
				op.name = objectMap.objectName;
				pr.PRparseTypeEnum = InternalParseTypeE.Object;
				op.memberValueEnum = InternalMemberValueE.Empty;
			}
			else
			{
				pr.PRparseTypeEnum = InternalParseTypeE.Member;
				pr.PRmemberValueEnum = InternalMemberValueE.Nested;
				op.memberValueEnum = InternalMemberValueE.Nested;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("No map for object '{0}'.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
					op.memberTypeEnum = InternalMemberTypeE.Item;
				}
				else
				{
					pr.PRname = objectProgress.name;
					pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
					op.memberTypeEnum = InternalMemberTypeE.Field;
				}
			}
			pr.PRobjectId = this.objectReader.GetId((long)this.binaryObject.objectId);
			pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
			if (pr.PRobjectId == this.topId)
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
			}
			pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
			pr.PRkeyDt = objectMap.objectName;
			pr.PRdtType = objectMap.objectType;
			pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.objectReader.Parse(pr);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000DDEC8 File Offset: 0x000DC0C8
		[SecurityCritical]
		internal void ReadCrossAppDomainMap()
		{
			BinaryCrossAppDomainMap binaryCrossAppDomainMap = new BinaryCrossAppDomainMap();
			binaryCrossAppDomainMap.Read(this);
			binaryCrossAppDomainMap.Dump();
			object obj = this.objectReader.CrossAppDomainArray(binaryCrossAppDomainMap.crossAppDomainArrayIndex);
			BinaryObjectWithMap binaryObjectWithMap = obj as BinaryObjectWithMap;
			if (binaryObjectWithMap != null)
			{
				binaryObjectWithMap.Dump();
				this.ReadObjectWithMap(binaryObjectWithMap);
				return;
			}
			BinaryObjectWithMapTyped binaryObjectWithMapTyped = obj as BinaryObjectWithMapTyped;
			if (binaryObjectWithMapTyped != null)
			{
				this.ReadObjectWithMapTyped(binaryObjectWithMapTyped);
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Cross-AppDomain BinaryFormatter error; expected '{0}' but received '{1}'.", new object[]
			{
				"BinaryObjectMap",
				obj
			}));
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000DDF48 File Offset: 0x000DC148
		[SecurityCritical]
		internal void ReadObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			if (this.bowm == null)
			{
				this.bowm = new BinaryObjectWithMap(binaryHeaderEnum);
			}
			else
			{
				this.bowm.binaryHeaderEnum = binaryHeaderEnum;
			}
			this.bowm.Read(this);
			this.bowm.Dump();
			this.ReadObjectWithMap(this.bowm);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000DDF9C File Offset: 0x000DC19C
		[SecurityCritical]
		private void ReadObjectWithMap(BinaryObjectWithMap record)
		{
			BinaryAssemblyInfo binaryAssemblyInfo = null;
			ObjectProgress op = this.GetOp();
			ParseRecord pr = op.pr;
			this.stack.Push(op);
			if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				if (record.assemId < 1)
				{
					throw new SerializationException(Environment.GetResourceString("No assembly information is available for object on the wire, '{0}'.", new object[]
					{
						record.name
					}));
				}
				binaryAssemblyInfo = (BinaryAssemblyInfo)this.AssemIdToAssemblyTable[record.assemId];
				if (binaryAssemblyInfo == null)
				{
					throw new SerializationException(Environment.GetResourceString("No assembly information is available for object on the wire, '{0}'.", new object[]
					{
						record.assemId.ToString() + " " + record.name
					}));
				}
			}
			else if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMap)
			{
				binaryAssemblyInfo = this.SystemAssemblyInfo;
			}
			Type type = this.objectReader.GetType(binaryAssemblyInfo, record.name);
			ObjectMap objectMap = ObjectMap.Create(record.name, type, record.memberNames, this.objectReader, record.objectId, binaryAssemblyInfo);
			this.ObjectMapIdTable[record.objectId] = objectMap;
			op.objectTypeEnum = InternalObjectTypeE.Object;
			op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
			op.typeInformationA = objectMap.typeInformationA;
			op.memberLength = op.binaryTypeEnumA.Length;
			op.memberNames = objectMap.memberNames;
			op.memberTypes = objectMap.memberTypes;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.PeekPeek();
			if (objectProgress == null || objectProgress.isInitial)
			{
				op.name = record.name;
				pr.PRparseTypeEnum = InternalParseTypeE.Object;
				op.memberValueEnum = InternalMemberValueE.Empty;
			}
			else
			{
				pr.PRparseTypeEnum = InternalParseTypeE.Member;
				pr.PRmemberValueEnum = InternalMemberValueE.Nested;
				op.memberValueEnum = InternalMemberValueE.Nested;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid ObjectTypeEnum {0}.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
					op.memberTypeEnum = InternalMemberTypeE.Field;
				}
				else
				{
					pr.PRname = objectProgress.name;
					pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
					op.memberTypeEnum = InternalMemberTypeE.Field;
				}
			}
			pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
			pr.PRobjectId = this.objectReader.GetId((long)record.objectId);
			pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
			if (pr.PRobjectId == this.topId)
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
			}
			pr.PRkeyDt = record.name;
			pr.PRdtType = objectMap.objectType;
			pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.objectReader.Parse(pr);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000DE21C File Offset: 0x000DC41C
		[SecurityCritical]
		internal void ReadObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
		{
			if (this.bowmt == null)
			{
				this.bowmt = new BinaryObjectWithMapTyped(binaryHeaderEnum);
			}
			else
			{
				this.bowmt.binaryHeaderEnum = binaryHeaderEnum;
			}
			this.bowmt.Read(this);
			this.ReadObjectWithMapTyped(this.bowmt);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000DE258 File Offset: 0x000DC458
		[SecurityCritical]
		private void ReadObjectWithMapTyped(BinaryObjectWithMapTyped record)
		{
			BinaryAssemblyInfo binaryAssemblyInfo = null;
			ObjectProgress op = this.GetOp();
			ParseRecord pr = op.pr;
			this.stack.Push(op);
			if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTypedAssemId)
			{
				if (record.assemId < 1)
				{
					throw new SerializationException(Environment.GetResourceString("No assembly ID for object type '{0}'.", new object[]
					{
						record.name
					}));
				}
				binaryAssemblyInfo = (BinaryAssemblyInfo)this.AssemIdToAssemblyTable[record.assemId];
				if (binaryAssemblyInfo == null)
				{
					throw new SerializationException(Environment.GetResourceString("No assembly ID for object type '{0}'.", new object[]
					{
						record.assemId.ToString() + " " + record.name
					}));
				}
			}
			else if (record.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapTyped)
			{
				binaryAssemblyInfo = this.SystemAssemblyInfo;
			}
			ObjectMap objectMap = ObjectMap.Create(record.name, record.memberNames, record.binaryTypeEnumA, record.typeInformationA, record.memberAssemIds, this.objectReader, record.objectId, binaryAssemblyInfo, this.AssemIdToAssemblyTable);
			this.ObjectMapIdTable[record.objectId] = objectMap;
			op.objectTypeEnum = InternalObjectTypeE.Object;
			op.binaryTypeEnumA = objectMap.binaryTypeEnumA;
			op.typeInformationA = objectMap.typeInformationA;
			op.memberLength = op.binaryTypeEnumA.Length;
			op.memberNames = objectMap.memberNames;
			op.memberTypes = objectMap.memberTypes;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.PeekPeek();
			if (objectProgress == null || objectProgress.isInitial)
			{
				op.name = record.name;
				pr.PRparseTypeEnum = InternalParseTypeE.Object;
				op.memberValueEnum = InternalMemberValueE.Empty;
			}
			else
			{
				pr.PRparseTypeEnum = InternalParseTypeE.Member;
				pr.PRmemberValueEnum = InternalMemberValueE.Nested;
				op.memberValueEnum = InternalMemberValueE.Nested;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid ObjectTypeEnum {0}.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
					op.memberTypeEnum = InternalMemberTypeE.Item;
				}
				else
				{
					pr.PRname = objectProgress.name;
					pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
					op.memberTypeEnum = InternalMemberTypeE.Field;
				}
			}
			pr.PRobjectTypeEnum = InternalObjectTypeE.Object;
			pr.PRobjectInfo = objectMap.CreateObjectInfo(ref pr.PRsi, ref pr.PRmemberData);
			pr.PRobjectId = this.objectReader.GetId((long)record.objectId);
			if (pr.PRobjectId == this.topId)
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
			}
			pr.PRkeyDt = record.name;
			pr.PRdtType = objectMap.objectType;
			pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.objectReader.Parse(pr);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		[SecurityCritical]
		private void ReadObjectString(BinaryHeaderEnum binaryHeaderEnum)
		{
			if (this.objectString == null)
			{
				this.objectString = new BinaryObjectString();
			}
			if (binaryHeaderEnum == BinaryHeaderEnum.ObjectString)
			{
				this.objectString.Read(this);
				this.objectString.Dump();
			}
			else
			{
				if (this.crossAppDomainString == null)
				{
					this.crossAppDomainString = new BinaryCrossAppDomainString();
				}
				this.crossAppDomainString.Read(this);
				this.crossAppDomainString.Dump();
				this.objectString.value = (this.objectReader.CrossAppDomainArray(this.crossAppDomainString.value) as string);
				if (this.objectString.value == null)
				{
					throw new SerializationException(Environment.GetResourceString("Cross-AppDomain BinaryFormatter error; expected '{0}' but received '{1}'.", new object[]
					{
						"String",
						this.crossAppDomainString.value
					}));
				}
				this.objectString.objectId = this.crossAppDomainString.objectId;
			}
			this.prs.Init();
			this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
			this.prs.PRobjectId = this.objectReader.GetId((long)this.objectString.objectId);
			if (this.prs.PRobjectId == this.topId)
			{
				this.prs.PRobjectPositionEnum = InternalObjectPositionE.Top;
			}
			this.prs.PRobjectTypeEnum = InternalObjectTypeE.Object;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
			this.prs.PRvalue = this.objectString.value;
			this.prs.PRkeyDt = "System.String";
			this.prs.PRdtType = Converter.typeofString;
			this.prs.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.prs.PRvarValue = this.objectString.value;
			if (objectProgress == null)
			{
				this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
				this.prs.PRname = "System.String";
			}
			else
			{
				this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
				this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid ObjectTypeEnum {0}.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
				}
				else
				{
					this.prs.PRname = objectProgress.name;
					this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
				}
			}
			this.objectReader.Parse(this.prs);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000DE738 File Offset: 0x000DC938
		[SecurityCritical]
		private void ReadMemberPrimitiveTyped()
		{
			if (this.memberPrimitiveTyped == null)
			{
				this.memberPrimitiveTyped = new MemberPrimitiveTyped();
			}
			this.memberPrimitiveTyped.Read(this);
			this.memberPrimitiveTyped.Dump();
			this.prs.PRobjectTypeEnum = InternalObjectTypeE.Object;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
			this.prs.Init();
			this.prs.PRvarValue = this.memberPrimitiveTyped.value;
			this.prs.PRkeyDt = Converter.ToComType(this.memberPrimitiveTyped.primitiveTypeEnum);
			this.prs.PRdtType = Converter.ToType(this.memberPrimitiveTyped.primitiveTypeEnum);
			this.prs.PRdtTypeCode = this.memberPrimitiveTyped.primitiveTypeEnum;
			if (objectProgress == null)
			{
				this.prs.PRparseTypeEnum = InternalParseTypeE.Object;
				this.prs.PRname = "System.Variant";
			}
			else
			{
				this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
				this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid ObjectTypeEnum {0}.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
				}
				else
				{
					this.prs.PRname = objectProgress.name;
					this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
				}
			}
			this.objectReader.Parse(this.prs);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000DE8AC File Offset: 0x000DCAAC
		[SecurityCritical]
		private void ReadArray(BinaryHeaderEnum binaryHeaderEnum)
		{
			BinaryArray binaryArray = new BinaryArray(binaryHeaderEnum);
			binaryArray.Read(this);
			BinaryAssemblyInfo assemblyInfo;
			if (binaryArray.binaryTypeEnum == BinaryTypeEnum.ObjectUser)
			{
				if (binaryArray.assemId < 1)
				{
					throw new SerializationException(Environment.GetResourceString("No assembly ID for object type '{0}'.", new object[]
					{
						binaryArray.typeInformation
					}));
				}
				assemblyInfo = (BinaryAssemblyInfo)this.AssemIdToAssemblyTable[binaryArray.assemId];
			}
			else
			{
				assemblyInfo = this.SystemAssemblyInfo;
			}
			ObjectProgress op = this.GetOp();
			ParseRecord pr = op.pr;
			op.objectTypeEnum = InternalObjectTypeE.Array;
			op.binaryTypeEnum = binaryArray.binaryTypeEnum;
			op.typeInformation = binaryArray.typeInformation;
			ObjectProgress objectProgress = (ObjectProgress)this.stack.PeekPeek();
			if (objectProgress == null || binaryArray.objectId > 0)
			{
				op.name = "System.Array";
				pr.PRparseTypeEnum = InternalParseTypeE.Object;
				op.memberValueEnum = InternalMemberValueE.Empty;
			}
			else
			{
				pr.PRparseTypeEnum = InternalParseTypeE.Member;
				pr.PRmemberValueEnum = InternalMemberValueE.Nested;
				op.memberValueEnum = InternalMemberValueE.Nested;
				InternalObjectTypeE objectTypeEnum = objectProgress.objectTypeEnum;
				if (objectTypeEnum != InternalObjectTypeE.Object)
				{
					if (objectTypeEnum != InternalObjectTypeE.Array)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid ObjectTypeEnum {0}.", new object[]
						{
							objectProgress.objectTypeEnum.ToString()
						}));
					}
					pr.PRmemberTypeEnum = InternalMemberTypeE.Item;
					op.memberTypeEnum = InternalMemberTypeE.Item;
				}
				else
				{
					pr.PRname = objectProgress.name;
					pr.PRmemberTypeEnum = InternalMemberTypeE.Field;
					op.memberTypeEnum = InternalMemberTypeE.Field;
					pr.PRkeyDt = objectProgress.name;
					pr.PRdtType = objectProgress.dtType;
				}
			}
			pr.PRobjectId = this.objectReader.GetId((long)binaryArray.objectId);
			if (pr.PRobjectId == this.topId)
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Top;
			}
			else if (this.headerId > 0L && pr.PRobjectId == this.headerId)
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Headers;
			}
			else
			{
				pr.PRobjectPositionEnum = InternalObjectPositionE.Child;
			}
			pr.PRobjectTypeEnum = InternalObjectTypeE.Array;
			BinaryConverter.TypeFromInfo(binaryArray.binaryTypeEnum, binaryArray.typeInformation, this.objectReader, assemblyInfo, out pr.PRarrayElementTypeCode, out pr.PRarrayElementTypeString, out pr.PRarrayElementType, out pr.PRisArrayVariant);
			pr.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			pr.PRrank = binaryArray.rank;
			pr.PRlengthA = binaryArray.lengthA;
			pr.PRlowerBoundA = binaryArray.lowerBoundA;
			bool flag = false;
			switch (binaryArray.binaryArrayTypeEnum)
			{
			case BinaryArrayTypeEnum.Single:
			case BinaryArrayTypeEnum.SingleOffset:
				op.numItems = binaryArray.lengthA[0];
				pr.PRarrayTypeEnum = InternalArrayTypeE.Single;
				if (Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode) && binaryArray.lowerBoundA[0] == 0)
				{
					flag = true;
					this.ReadArrayAsBytes(pr);
				}
				break;
			case BinaryArrayTypeEnum.Jagged:
			case BinaryArrayTypeEnum.JaggedOffset:
				op.numItems = binaryArray.lengthA[0];
				pr.PRarrayTypeEnum = InternalArrayTypeE.Jagged;
				break;
			case BinaryArrayTypeEnum.Rectangular:
			case BinaryArrayTypeEnum.RectangularOffset:
			{
				int num = 1;
				for (int i = 0; i < binaryArray.rank; i++)
				{
					num *= binaryArray.lengthA[i];
				}
				op.numItems = num;
				pr.PRarrayTypeEnum = InternalArrayTypeE.Rectangular;
				break;
			}
			default:
				throw new SerializationException(Environment.GetResourceString("Invalid array type '{0}'.", new object[]
				{
					binaryArray.binaryArrayTypeEnum.ToString()
				}));
			}
			if (!flag)
			{
				this.stack.Push(op);
			}
			else
			{
				this.PutOp(op);
			}
			this.objectReader.Parse(pr);
			if (flag)
			{
				pr.PRparseTypeEnum = InternalParseTypeE.ObjectEnd;
				this.objectReader.Parse(pr);
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000DEBF8 File Offset: 0x000DCDF8
		[SecurityCritical]
		private void ReadArrayAsBytes(ParseRecord pr)
		{
			if (pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Byte)
			{
				pr.PRnewObj = this.ReadBytes(pr.PRlengthA[0]);
				return;
			}
			if (pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Char)
			{
				pr.PRnewObj = this.ReadChars(pr.PRlengthA[0]);
				return;
			}
			int num = Converter.TypeLength(pr.PRarrayElementTypeCode);
			pr.PRnewObj = Converter.CreatePrimitiveArray(pr.PRarrayElementTypeCode, pr.PRlengthA[0]);
			Array array = (Array)pr.PRnewObj;
			int i = 0;
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[4096];
			}
			while (i < array.Length)
			{
				int num2 = Math.Min(4096 / num, array.Length - i);
				int num3 = num2 * num;
				this.ReadBytes(this.byteBuffer, 0, num3);
				if (!BitConverter.IsLittleEndian)
				{
					for (int j = 0; j < num3; j += num)
					{
						for (int k = 0; k < num / 2; k++)
						{
							byte b = this.byteBuffer[j + k];
							this.byteBuffer[j + k] = this.byteBuffer[j + num - 1 - k];
							this.byteBuffer[j + num - 1 - k] = b;
						}
					}
				}
				Buffer.InternalBlockCopy(this.byteBuffer, 0, array, i * num, num3);
				i += num2;
			}
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000DED48 File Offset: 0x000DCF48
		[SecurityCritical]
		private void ReadMemberPrimitiveUnTyped()
		{
			ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
			if (this.memberPrimitiveUnTyped == null)
			{
				this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
			}
			this.memberPrimitiveUnTyped.Set((InternalPrimitiveTypeE)this.expectedTypeInformation);
			this.memberPrimitiveUnTyped.Read(this);
			this.memberPrimitiveUnTyped.Dump();
			this.prs.Init();
			this.prs.PRvarValue = this.memberPrimitiveUnTyped.value;
			this.prs.PRdtTypeCode = (InternalPrimitiveTypeE)this.expectedTypeInformation;
			this.prs.PRdtType = Converter.ToType(this.prs.PRdtTypeCode);
			this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
			this.prs.PRmemberValueEnum = InternalMemberValueE.InlineValue;
			if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
				this.prs.PRname = objectProgress.name;
			}
			else
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
			}
			this.objectReader.Parse(this.prs);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000DEE58 File Offset: 0x000DD058
		[SecurityCritical]
		private void ReadMemberReference()
		{
			if (this.memberReference == null)
			{
				this.memberReference = new MemberReference();
			}
			this.memberReference.Read(this);
			this.memberReference.Dump();
			ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
			this.prs.Init();
			this.prs.PRidRef = this.objectReader.GetId((long)this.memberReference.idRef);
			this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
			this.prs.PRmemberValueEnum = InternalMemberValueE.Reference;
			if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
				this.prs.PRname = objectProgress.name;
				this.prs.PRdtType = objectProgress.dtType;
			}
			else
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
			}
			this.objectReader.Parse(this.prs);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000DEF3C File Offset: 0x000DD13C
		[SecurityCritical]
		private void ReadObjectNull(BinaryHeaderEnum binaryHeaderEnum)
		{
			if (this.objectNull == null)
			{
				this.objectNull = new ObjectNull();
			}
			this.objectNull.Read(this, binaryHeaderEnum);
			this.objectNull.Dump();
			ObjectProgress objectProgress = (ObjectProgress)this.stack.Peek();
			this.prs.Init();
			this.prs.PRparseTypeEnum = InternalParseTypeE.Member;
			this.prs.PRmemberValueEnum = InternalMemberValueE.Null;
			if (objectProgress.objectTypeEnum == InternalObjectTypeE.Object)
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Field;
				this.prs.PRname = objectProgress.name;
				this.prs.PRdtType = objectProgress.dtType;
			}
			else
			{
				this.prs.PRmemberTypeEnum = InternalMemberTypeE.Item;
				this.prs.PRnullCount = this.objectNull.nullCount;
				objectProgress.ArrayCountIncrement(this.objectNull.nullCount - 1);
			}
			this.objectReader.Parse(this.prs);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000DF028 File Offset: 0x000DD228
		[SecurityCritical]
		private void ReadMessageEnd()
		{
			if (__BinaryParser.messageEnd == null)
			{
				__BinaryParser.messageEnd = new MessageEnd();
			}
			__BinaryParser.messageEnd.Read(this);
			__BinaryParser.messageEnd.Dump();
			if (!this.stack.IsEmpty())
			{
				throw new SerializationException(Environment.GetResourceString("End of Stream encountered before parsing was completed."));
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000DF080 File Offset: 0x000DD280
		internal object ReadValue(InternalPrimitiveTypeE code)
		{
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				return this.ReadBoolean();
			case InternalPrimitiveTypeE.Byte:
				return this.ReadByte();
			case InternalPrimitiveTypeE.Char:
				return this.ReadChar();
			case InternalPrimitiveTypeE.Decimal:
				return this.ReadDecimal();
			case InternalPrimitiveTypeE.Double:
				return this.ReadDouble();
			case InternalPrimitiveTypeE.Int16:
				return this.ReadInt16();
			case InternalPrimitiveTypeE.Int32:
				return this.ReadInt32();
			case InternalPrimitiveTypeE.Int64:
				return this.ReadInt64();
			case InternalPrimitiveTypeE.SByte:
				return this.ReadSByte();
			case InternalPrimitiveTypeE.Single:
				return this.ReadSingle();
			case InternalPrimitiveTypeE.TimeSpan:
				return this.ReadTimeSpan();
			case InternalPrimitiveTypeE.DateTime:
				return this.ReadDateTime();
			case InternalPrimitiveTypeE.UInt16:
				return this.ReadUInt16();
			case InternalPrimitiveTypeE.UInt32:
				return this.ReadUInt32();
			case InternalPrimitiveTypeE.UInt64:
				return this.ReadUInt64();
			}
			throw new SerializationException(Environment.GetResourceString("Invalid type code in stream '{0}'.", new object[]
			{
				code.ToString()
			}));
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000DF1EC File Offset: 0x000DD3EC
		private ObjectProgress GetOp()
		{
			ObjectProgress objectProgress;
			if (this.opPool != null && !this.opPool.IsEmpty())
			{
				objectProgress = (ObjectProgress)this.opPool.Pop();
				objectProgress.Init();
			}
			else
			{
				objectProgress = new ObjectProgress();
			}
			return objectProgress;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000DF230 File Offset: 0x000DD430
		private void PutOp(ObjectProgress op)
		{
			if (this.opPool == null)
			{
				this.opPool = new SerStack("opPool");
			}
			this.opPool.Push(op);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000DF256 File Offset: 0x000DD456
		// Note: this type is marked as 'beforefieldinit'.
		static __BinaryParser()
		{
		}

		// Token: 0x04002964 RID: 10596
		internal ObjectReader objectReader;

		// Token: 0x04002965 RID: 10597
		internal Stream input;

		// Token: 0x04002966 RID: 10598
		internal long topId;

		// Token: 0x04002967 RID: 10599
		internal long headerId;

		// Token: 0x04002968 RID: 10600
		internal SizedArray objectMapIdTable;

		// Token: 0x04002969 RID: 10601
		internal SizedArray assemIdToAssemblyTable;

		// Token: 0x0400296A RID: 10602
		internal SerStack stack = new SerStack("ObjectProgressStack");

		// Token: 0x0400296B RID: 10603
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x0400296C RID: 10604
		internal object expectedTypeInformation;

		// Token: 0x0400296D RID: 10605
		internal ParseRecord PRS;

		// Token: 0x0400296E RID: 10606
		private BinaryAssemblyInfo systemAssemblyInfo;

		// Token: 0x0400296F RID: 10607
		private BinaryReader dataReader;

		// Token: 0x04002970 RID: 10608
		private static Encoding encoding = new UTF8Encoding(false, true);

		// Token: 0x04002971 RID: 10609
		private SerStack opPool;

		// Token: 0x04002972 RID: 10610
		private BinaryObject binaryObject;

		// Token: 0x04002973 RID: 10611
		private BinaryObjectWithMap bowm;

		// Token: 0x04002974 RID: 10612
		private BinaryObjectWithMapTyped bowmt;

		// Token: 0x04002975 RID: 10613
		internal BinaryObjectString objectString;

		// Token: 0x04002976 RID: 10614
		internal BinaryCrossAppDomainString crossAppDomainString;

		// Token: 0x04002977 RID: 10615
		internal MemberPrimitiveTyped memberPrimitiveTyped;

		// Token: 0x04002978 RID: 10616
		private byte[] byteBuffer;

		// Token: 0x04002979 RID: 10617
		private const int chunkSize = 4096;

		// Token: 0x0400297A RID: 10618
		internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;

		// Token: 0x0400297B RID: 10619
		internal MemberReference memberReference;

		// Token: 0x0400297C RID: 10620
		internal ObjectNull objectNull;

		// Token: 0x0400297D RID: 10621
		internal static volatile MessageEnd messageEnd;
	}
}
