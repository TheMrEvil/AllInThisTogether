using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ExitGames.Client.Photon.StructWrapping;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200002D RID: 45
	public class Protocol16 : IProtocol
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D70C File Offset: 0x0000B90C
		public override string ProtocolType
		{
			get
			{
				return "GpBinaryV16";
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000D724 File Offset: 0x0000B924
		public override byte[] VersionBytes
		{
			get
			{
				return this.versionBytes;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D73C File Offset: 0x0000B93C
		private bool SerializeCustom(StreamBuffer dout, object serObject)
		{
			StructWrapper structWrapper = serObject as StructWrapper;
			bool flag = structWrapper == null;
			Type key;
			if (flag)
			{
				key = serObject.GetType();
			}
			else
			{
				key = structWrapper.ttype;
			}
			CustomType customType;
			bool flag2 = Protocol.TypeDict.TryGetValue(key, out customType);
			bool result;
			if (flag2)
			{
				bool flag3 = customType.SerializeStreamFunction == null;
				if (flag3)
				{
					byte[] array = customType.SerializeFunction(serObject);
					dout.WriteByte(99);
					dout.WriteByte(customType.Code);
					this.SerializeLengthAsShort(dout, array.Length, "Custom Type " + customType.Code.ToString());
					dout.Write(array, 0, array.Length);
					result = true;
				}
				else
				{
					dout.WriteByte(99);
					dout.WriteByte(customType.Code);
					int position = dout.Position;
					dout.Position += 2;
					short num = customType.SerializeStreamFunction(dout, serObject);
					long num2 = (long)dout.Position;
					dout.Position = position;
					this.SerializeLengthAsShort(dout, (int)num, "Custom Type " + customType.Code.ToString());
					dout.Position += (int)num;
					bool flag4 = (long)dout.Position != num2;
					if (flag4)
					{
						throw new Exception(string.Concat(new string[]
						{
							"Serialization failed. Stream position corrupted. Should be ",
							num2.ToString(),
							" is now: ",
							dout.Position.ToString(),
							" serializedLength: ",
							num.ToString()
						}));
					}
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		private object DeserializeCustom(StreamBuffer din, byte customTypeCode, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None)
		{
			short num = this.DeserializeShort(din);
			bool flag = num < 0;
			if (flag)
			{
				throw new InvalidDataException("DeserializeCustom read negative length value: " + num.ToString() + " before position: " + din.Position.ToString());
			}
			CustomType customType;
			bool flag2 = (int)num <= din.Available && Protocol.CodeDict.TryGetValue(customTypeCode, out customType);
			object result;
			if (flag2)
			{
				bool flag3 = customType.DeserializeStreamFunction == null;
				if (flag3)
				{
					byte[] array = new byte[(int)num];
					din.Read(array, 0, (int)num);
					result = customType.DeserializeFunction(array);
				}
				else
				{
					int position = din.Position;
					object obj = customType.DeserializeStreamFunction(din, num);
					int num2 = din.Position - position;
					bool flag4 = num2 != (int)num;
					if (flag4)
					{
						din.Position = position + (int)num;
					}
					result = obj;
				}
			}
			else
			{
				int num3 = (int)(((int)num <= din.Available) ? num : ((short)din.Available));
				byte[] array2 = new byte[num3];
				din.Read(array2, 0, num3);
				result = array2;
			}
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		private Type GetTypeOfCode(byte typeCode)
		{
			byte b = typeCode;
			byte b2 = b;
			if (b2 <= 42)
			{
				if (b2 == 0 || b2 == 42)
				{
					return typeof(object);
				}
			}
			else
			{
				if (b2 == 68)
				{
					return typeof(IDictionary);
				}
				switch (b2)
				{
				case 97:
					return typeof(string[]);
				case 98:
					return typeof(byte);
				case 99:
					return typeof(CustomType);
				case 100:
					return typeof(double);
				case 101:
					return typeof(EventData);
				case 102:
					return typeof(float);
				case 104:
					return typeof(Hashtable);
				case 105:
					return typeof(int);
				case 107:
					return typeof(short);
				case 108:
					return typeof(long);
				case 110:
					return typeof(int[]);
				case 111:
					return typeof(bool);
				case 112:
					return typeof(OperationResponse);
				case 113:
					return typeof(OperationRequest);
				case 115:
					return typeof(string);
				case 120:
					return typeof(byte[]);
				case 121:
					return typeof(Array);
				case 122:
					return typeof(object[]);
				}
			}
			Debug.WriteLine("missing type: " + typeCode.ToString());
			throw new Exception("deserialize(): " + typeCode.ToString());
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DC00 File Offset: 0x0000BE00
		private Protocol16.GpType GetCodeOfType(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			TypeCode typeCode2 = typeCode;
			TypeCode typeCode3 = typeCode2;
			switch (typeCode3)
			{
			case TypeCode.Boolean:
				return Protocol16.GpType.Boolean;
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				break;
			case TypeCode.Byte:
				return Protocol16.GpType.Byte;
			case TypeCode.Int16:
				return Protocol16.GpType.Short;
			case TypeCode.Int32:
				return Protocol16.GpType.Integer;
			case TypeCode.Int64:
				return Protocol16.GpType.Long;
			case TypeCode.Single:
				return Protocol16.GpType.Float;
			case TypeCode.Double:
				return Protocol16.GpType.Double;
			default:
				if (typeCode3 == TypeCode.String)
				{
					return Protocol16.GpType.String;
				}
				break;
			}
			bool isArray = type.IsArray;
			Protocol16.GpType result;
			if (isArray)
			{
				bool flag = type == typeof(byte[]);
				if (flag)
				{
					result = Protocol16.GpType.ByteArray;
				}
				else
				{
					result = Protocol16.GpType.Array;
				}
			}
			else
			{
				bool flag2 = type == typeof(Hashtable);
				if (flag2)
				{
					result = Protocol16.GpType.Hashtable;
				}
				else
				{
					bool flag3 = type == typeof(List<object>);
					if (flag3)
					{
						result = Protocol16.GpType.ObjectArray;
					}
					else
					{
						bool flag4 = type.IsGenericType && typeof(Dictionary<, >) == type.GetGenericTypeDefinition();
						if (flag4)
						{
							result = Protocol16.GpType.Dictionary;
						}
						else
						{
							bool flag5 = type == typeof(EventData);
							if (flag5)
							{
								result = Protocol16.GpType.EventData;
							}
							else
							{
								bool flag6 = type == typeof(OperationRequest);
								if (flag6)
								{
									result = Protocol16.GpType.OperationRequest;
								}
								else
								{
									bool flag7 = type == typeof(OperationResponse);
									if (flag7)
									{
										result = Protocol16.GpType.OperationResponse;
									}
									else
									{
										result = Protocol16.GpType.Unknown;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000DD94 File Offset: 0x0000BF94
		private Array CreateArrayByType(byte arrayType, short length)
		{
			return Array.CreateInstance(this.GetTypeOfCode(arrayType), (int)length);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000DDB3 File Offset: 0x0000BFB3
		public void SerializeOperationRequest(StreamBuffer stream, OperationRequest operation, bool setType)
		{
			this.SerializeOperationRequest(stream, operation.OperationCode, operation.Parameters, setType);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		[Obsolete("Use ParameterDictionary instead.")]
		public override void SerializeOperationRequest(StreamBuffer stream, byte operationCode, Dictionary<byte, object> parameters, bool setType)
		{
			if (setType)
			{
				stream.WriteByte(113);
			}
			stream.WriteByte(operationCode);
			this.SerializeParameterTable(stream, parameters);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		public override void SerializeOperationRequest(StreamBuffer stream, byte operationCode, ParameterDictionary parameters, bool setType)
		{
			if (setType)
			{
				stream.WriteByte(113);
			}
			stream.WriteByte(operationCode);
			this.SerializeParameterTable(stream, parameters);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public override OperationRequest DeserializeOperationRequest(StreamBuffer din, IProtocol.DeserializationFlags flags)
		{
			OperationRequest operationRequest = new OperationRequest();
			operationRequest.OperationCode = this.DeserializeByte(din);
			operationRequest.Parameters = this.DeserializeParameterDictionary(din, operationRequest.Parameters, flags);
			return operationRequest;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DE68 File Offset: 0x0000C068
		public override void SerializeOperationResponse(StreamBuffer stream, OperationResponse serObject, bool setType)
		{
			if (setType)
			{
				stream.WriteByte(112);
			}
			stream.WriteByte(serObject.OperationCode);
			this.SerializeShort(stream, serObject.ReturnCode, false);
			bool flag = string.IsNullOrEmpty(serObject.DebugMessage);
			if (flag)
			{
				stream.WriteByte(42);
			}
			else
			{
				this.SerializeString(stream, serObject.DebugMessage, false);
			}
			this.SerializeParameterTable(stream, serObject.Parameters);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public override DisconnectMessage DeserializeDisconnectMessage(StreamBuffer stream)
		{
			return new DisconnectMessage
			{
				Code = this.DeserializeShort(stream),
				DebugMessage = (this.Deserialize(stream, this.DeserializeByte(stream), IProtocol.DeserializationFlags.None) as string),
				Parameters = this.DeserializeParameterTable(stream, null)
			};
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DF30 File Offset: 0x0000C130
		public override OperationResponse DeserializeOperationResponse(StreamBuffer stream, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None)
		{
			return new OperationResponse
			{
				OperationCode = this.DeserializeByte(stream),
				ReturnCode = this.DeserializeShort(stream),
				DebugMessage = (this.Deserialize(stream, this.DeserializeByte(stream), IProtocol.DeserializationFlags.None) as string),
				Parameters = this.DeserializeParameterDictionary(stream, null, IProtocol.DeserializationFlags.None)
			};
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DF8C File Offset: 0x0000C18C
		public override void SerializeEventData(StreamBuffer stream, EventData serObject, bool setType)
		{
			if (setType)
			{
				stream.WriteByte(101);
			}
			stream.WriteByte(serObject.Code);
			this.SerializeParameterTable(stream, serObject.Parameters);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public override EventData DeserializeEventData(StreamBuffer din, EventData target = null, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None)
		{
			bool flag = target != null;
			EventData eventData;
			if (flag)
			{
				target.Reset();
				eventData = target;
			}
			else
			{
				eventData = new EventData();
			}
			eventData.Code = this.DeserializeByte(din);
			this.DeserializeParameterDictionary(din, eventData.Parameters, IProtocol.DeserializationFlags.None);
			return eventData;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E014 File Offset: 0x0000C214
		[Obsolete("Use ParameterDictionary instead of Dictionary<byte, object>.")]
		private void SerializeParameterTable(StreamBuffer stream, Dictionary<byte, object> parameters)
		{
			bool flag = parameters == null || parameters.Count == 0;
			if (flag)
			{
				this.SerializeShort(stream, 0, false);
			}
			else
			{
				this.SerializeLengthAsShort(stream, parameters.Count, "ParameterTable");
				foreach (KeyValuePair<byte, object> keyValuePair in parameters)
				{
					stream.WriteByte(keyValuePair.Key);
					this.Serialize(stream, keyValuePair.Value, true);
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		private void SerializeParameterTable(StreamBuffer stream, ParameterDictionary parameters)
		{
			bool flag = parameters == null || parameters.Count == 0;
			if (flag)
			{
				this.SerializeShort(stream, 0, false);
			}
			else
			{
				this.SerializeLengthAsShort(stream, parameters.Count, "Array");
				foreach (KeyValuePair<byte, object> keyValuePair in parameters)
				{
					stream.WriteByte(keyValuePair.Key);
					this.Serialize(stream, keyValuePair.Value, true);
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E154 File Offset: 0x0000C354
		private Dictionary<byte, object> DeserializeParameterTable(StreamBuffer stream, Dictionary<byte, object> target = null)
		{
			short num = this.DeserializeShort(stream);
			Dictionary<byte, object> dictionary = (target != null) ? target : new Dictionary<byte, object>((int)num);
			for (int i = 0; i < (int)num; i++)
			{
				byte key = stream.ReadByte();
				object value = this.Deserialize(stream, stream.ReadByte(), IProtocol.DeserializationFlags.None);
				dictionary[key] = value;
			}
			return dictionary;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		private ParameterDictionary DeserializeParameterDictionary(StreamBuffer stream, ParameterDictionary target = null, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None)
		{
			short num = this.DeserializeShort(stream);
			ParameterDictionary parameterDictionary = (target != null) ? target : new ParameterDictionary((int)num);
			for (int i = 0; i < (int)num; i++)
			{
				byte code = stream.ReadByte();
				object value = this.Deserialize(stream, stream.ReadByte(), flags);
				parameterDictionary.Add(code, value);
			}
			return parameterDictionary;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E21C File Offset: 0x0000C41C
		public override void Serialize(StreamBuffer dout, object serObject, bool setType)
		{
			bool flag = serObject == null;
			if (flag)
			{
				if (setType)
				{
					dout.WriteByte(42);
				}
			}
			else
			{
				StructWrapper structWrapper = serObject as StructWrapper;
				bool flag2 = structWrapper == null;
				Type type;
				if (flag2)
				{
					type = serObject.GetType();
				}
				else
				{
					type = structWrapper.ttype;
				}
				Protocol16.GpType codeOfType = this.GetCodeOfType(type);
				Protocol16.GpType gpType = codeOfType;
				Protocol16.GpType gpType2 = gpType;
				if (gpType2 != Protocol16.GpType.Dictionary)
				{
					switch (gpType2)
					{
					case Protocol16.GpType.Byte:
						this.SerializeByte(dout, serObject.Get<byte>(), setType);
						return;
					case Protocol16.GpType.Double:
						this.SerializeDouble(dout, serObject.Get<double>(), setType);
						return;
					case Protocol16.GpType.EventData:
						this.SerializeEventData(dout, (EventData)serObject, setType);
						return;
					case Protocol16.GpType.Float:
						this.SerializeFloat(dout, serObject.Get<float>(), setType);
						return;
					case Protocol16.GpType.Hashtable:
						this.SerializeHashTable(dout, (Hashtable)serObject, setType);
						return;
					case Protocol16.GpType.Integer:
						this.SerializeInteger(dout, serObject.Get<int>(), setType);
						return;
					case Protocol16.GpType.Short:
						this.SerializeShort(dout, serObject.Get<short>(), setType);
						return;
					case Protocol16.GpType.Long:
						this.SerializeLong(dout, serObject.Get<long>(), setType);
						return;
					case Protocol16.GpType.Boolean:
						this.SerializeBoolean(dout, serObject.Get<bool>(), setType);
						return;
					case Protocol16.GpType.OperationResponse:
						this.SerializeOperationResponse(dout, (OperationResponse)serObject, setType);
						return;
					case Protocol16.GpType.OperationRequest:
						this.SerializeOperationRequest(dout, (OperationRequest)serObject, setType);
						return;
					case Protocol16.GpType.String:
						this.SerializeString(dout, (string)serObject, setType);
						return;
					case Protocol16.GpType.ByteArray:
						this.SerializeByteArray(dout, (byte[])serObject, setType);
						return;
					case Protocol16.GpType.Array:
					{
						bool flag3 = serObject is int[];
						if (flag3)
						{
							this.SerializeIntArrayOptimized(dout, (int[])serObject, setType);
						}
						else
						{
							bool flag4 = type.GetElementType() == typeof(object);
							if (flag4)
							{
								this.SerializeObjectArray(dout, serObject as object[], setType);
							}
							else
							{
								this.SerializeArray(dout, (Array)serObject, setType);
							}
						}
						return;
					}
					case Protocol16.GpType.ObjectArray:
						this.SerializeObjectArray(dout, (IList)serObject, setType);
						return;
					}
					bool flag5 = serObject is ArraySegment<byte>;
					if (flag5)
					{
						ArraySegment<byte> arraySegment = (ArraySegment<byte>)serObject;
						this.SerializeByteArraySegment(dout, arraySegment.Array, arraySegment.Offset, arraySegment.Count, setType);
					}
					else
					{
						bool flag6 = !this.SerializeCustom(dout, serObject);
						if (flag6)
						{
							bool flag7 = serObject is StructWrapper;
							if (flag7)
							{
								throw new Exception("cannot serialize(): StructWrapper<" + (serObject as StructWrapper).ttype.Name + ">");
							}
							string str = "cannot serialize(): ";
							Type type2 = type;
							throw new Exception(str + ((type2 != null) ? type2.ToString() : null));
						}
					}
				}
				else
				{
					this.SerializeDictionary(dout, (IDictionary)serObject, setType);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E52C File Offset: 0x0000C72C
		private void SerializeByte(StreamBuffer dout, byte serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(98);
			}
			dout.WriteByte(serObject);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E554 File Offset: 0x0000C754
		private void SerializeBoolean(StreamBuffer dout, bool serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(111);
			}
			dout.WriteByte(serObject ? 1 : 0);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E580 File Offset: 0x0000C780
		public override void SerializeShort(StreamBuffer dout, short serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(107);
			}
			byte[] obj = this.memShort;
			lock (obj)
			{
				byte[] array = this.memShort;
				array[0] = (byte)(serObject >> 8);
				array[1] = (byte)serObject;
				dout.Write(array, 0, 2);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public void SerializeLengthAsShort(StreamBuffer dout, int serObject, string type)
		{
			bool flag = serObject > 32767 || serObject < 0;
			if (flag)
			{
				throw new NotSupportedException(string.Format("Exceeding 32767 (short.MaxValue) entries are not supported. Failed writing {0}. Length: {1}", type, serObject));
			}
			byte[] obj = this.memShort;
			lock (obj)
			{
				byte[] array = this.memShort;
				array[0] = (byte)(serObject >> 8);
				array[1] = (byte)serObject;
				dout.Write(array, 0, 2);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E674 File Offset: 0x0000C874
		private void SerializeInteger(StreamBuffer dout, int serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(105);
			}
			byte[] obj = this.memInteger;
			lock (obj)
			{
				byte[] array = this.memInteger;
				array[0] = (byte)(serObject >> 24);
				array[1] = (byte)(serObject >> 16);
				array[2] = (byte)(serObject >> 8);
				array[3] = (byte)serObject;
				dout.Write(array, 0, 4);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		private void SerializeLong(StreamBuffer dout, long serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(108);
			}
			long[] obj = this.memLongBlock;
			lock (obj)
			{
				this.memLongBlock[0] = serObject;
				Buffer.BlockCopy(this.memLongBlock, 0, this.memLongBlockBytes, 0, 8);
				byte[] array = this.memLongBlockBytes;
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					byte b = array[0];
					byte b2 = array[1];
					byte b3 = array[2];
					byte b4 = array[3];
					array[0] = array[7];
					array[1] = array[6];
					array[2] = array[5];
					array[3] = array[4];
					array[4] = b4;
					array[5] = b3;
					array[6] = b2;
					array[7] = b;
				}
				dout.Write(array, 0, 8);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		private void SerializeFloat(StreamBuffer dout, float serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(102);
			}
			byte[] obj = Protocol16.memFloatBlockBytes;
			lock (obj)
			{
				Protocol16.memFloatBlock[0] = serObject;
				Buffer.BlockCopy(Protocol16.memFloatBlock, 0, Protocol16.memFloatBlockBytes, 0, 4);
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					byte b = Protocol16.memFloatBlockBytes[0];
					byte b2 = Protocol16.memFloatBlockBytes[1];
					Protocol16.memFloatBlockBytes[0] = Protocol16.memFloatBlockBytes[3];
					Protocol16.memFloatBlockBytes[1] = Protocol16.memFloatBlockBytes[2];
					Protocol16.memFloatBlockBytes[2] = b2;
					Protocol16.memFloatBlockBytes[3] = b;
				}
				dout.Write(Protocol16.memFloatBlockBytes, 0, 4);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E880 File Offset: 0x0000CA80
		private void SerializeDouble(StreamBuffer dout, double serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(100);
			}
			byte[] obj = this.memDoubleBlockBytes;
			lock (obj)
			{
				this.memDoubleBlock[0] = serObject;
				Buffer.BlockCopy(this.memDoubleBlock, 0, this.memDoubleBlockBytes, 0, 8);
				byte[] array = this.memDoubleBlockBytes;
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					byte b = array[0];
					byte b2 = array[1];
					byte b3 = array[2];
					byte b4 = array[3];
					array[0] = array[7];
					array[1] = array[6];
					array[2] = array[5];
					array[3] = array[4];
					array[4] = b4;
					array[5] = b3;
					array[6] = b2;
					array[7] = b;
				}
				dout.Write(array, 0, 8);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public override void SerializeString(StreamBuffer stream, string value, bool setType)
		{
			if (setType)
			{
				stream.WriteByte(115);
			}
			int byteCount = Encoding.UTF8.GetByteCount(value);
			bool flag = byteCount > 32767;
			if (flag)
			{
				throw new NotSupportedException("Strings that exceed a UTF8-encoded byte-length of 32767 (short.MaxValue) are not supported. Yours is: " + byteCount.ToString());
			}
			this.SerializeLengthAsShort(stream, byteCount, "String");
			int byteIndex = 0;
			byte[] bufferAndAdvance = stream.GetBufferAndAdvance(byteCount, out byteIndex);
			Encoding.UTF8.GetBytes(value, 0, value.Length, bufferAndAdvance, byteIndex);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		private void SerializeArray(StreamBuffer dout, Array serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(121);
			}
			this.SerializeLengthAsShort(dout, serObject.Length, "Array");
			Type elementType = serObject.GetType().GetElementType();
			Protocol16.GpType codeOfType = this.GetCodeOfType(elementType);
			bool flag = codeOfType > Protocol16.GpType.Unknown;
			if (flag)
			{
				dout.WriteByte((byte)codeOfType);
				bool flag2 = codeOfType == Protocol16.GpType.Dictionary;
				if (flag2)
				{
					bool setKeyType;
					bool setValueType;
					this.SerializeDictionaryHeader(dout, serObject, out setKeyType, out setValueType);
					for (int i = 0; i < serObject.Length; i++)
					{
						object value = serObject.GetValue(i);
						this.SerializeDictionaryElements(dout, value, setKeyType, setValueType);
					}
				}
				else
				{
					for (int j = 0; j < serObject.Length; j++)
					{
						object value2 = serObject.GetValue(j);
						this.Serialize(dout, value2, false);
					}
				}
			}
			else
			{
				CustomType customType;
				bool flag3 = Protocol.TypeDict.TryGetValue(elementType, out customType);
				if (!flag3)
				{
					string str = "cannot serialize array of type ";
					Type type = elementType;
					throw new NotSupportedException(str + ((type != null) ? type.ToString() : null));
				}
				dout.WriteByte(99);
				dout.WriteByte(customType.Code);
				for (int k = 0; k < serObject.Length; k++)
				{
					object value3 = serObject.GetValue(k);
					bool flag4 = customType.SerializeStreamFunction == null;
					if (flag4)
					{
						byte[] array = customType.SerializeFunction(value3);
						this.SerializeLengthAsShort(dout, array.Length, "Custom Type " + customType.Code.ToString());
						dout.Write(array, 0, array.Length);
					}
					else
					{
						int position = dout.Position;
						dout.Position += 2;
						short num = customType.SerializeStreamFunction(dout, value3);
						long num2 = (long)dout.Position;
						dout.Position = position;
						this.SerializeLengthAsShort(dout, (int)num, "Custom Type " + customType.Code.ToString());
						dout.Position += (int)num;
						bool flag5 = (long)dout.Position != num2;
						if (flag5)
						{
							throw new Exception(string.Concat(new string[]
							{
								"Serialization failed. Stream position corrupted. Should be ",
								num2.ToString(),
								" is now: ",
								dout.Position.ToString(),
								" serializedLength: ",
								num.ToString()
							}));
						}
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000EC60 File Offset: 0x0000CE60
		private void SerializeByteArray(StreamBuffer dout, byte[] serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(120);
			}
			this.SerializeInteger(dout, serObject.Length, false);
			dout.Write(serObject, 0, serObject.Length);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000EC98 File Offset: 0x0000CE98
		private void SerializeByteArraySegment(StreamBuffer dout, byte[] serObject, int offset, int count, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(120);
			}
			this.SerializeInteger(dout, count, false);
			dout.Write(serObject, offset, count);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		private void SerializeIntArrayOptimized(StreamBuffer inWriter, int[] serObject, bool setType)
		{
			if (setType)
			{
				inWriter.WriteByte(121);
			}
			this.SerializeLengthAsShort(inWriter, serObject.Length, "int[]");
			inWriter.WriteByte(105);
			byte[] array = new byte[serObject.Length * 4];
			int num = 0;
			for (int i = 0; i < serObject.Length; i++)
			{
				array[num++] = (byte)(serObject[i] >> 24);
				array[num++] = (byte)(serObject[i] >> 16);
				array[num++] = (byte)(serObject[i] >> 8);
				array[num++] = (byte)serObject[i];
			}
			inWriter.Write(array, 0, array.Length);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		private void SerializeStringArray(StreamBuffer dout, string[] serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(97);
			}
			this.SerializeLengthAsShort(dout, serObject.Length, "string[]");
			for (int i = 0; i < serObject.Length; i++)
			{
				this.SerializeString(dout, serObject[i], false);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		private void SerializeObjectArray(StreamBuffer dout, IList objects, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(122);
			}
			this.SerializeLengthAsShort(dout, objects.Count, "object[]");
			for (int i = 0; i < objects.Count; i++)
			{
				object serObject = objects[i];
				this.Serialize(dout, serObject, true);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000EE18 File Offset: 0x0000D018
		private void SerializeHashTable(StreamBuffer dout, Hashtable serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(104);
			}
			this.SerializeLengthAsShort(dout, serObject.Count, "Hashtable");
			Dictionary<object, object>.KeyCollection keys = serObject.Keys;
			foreach (object obj in keys)
			{
				this.Serialize(dout, obj, true);
				this.Serialize(dout, serObject[obj], true);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		private void SerializeDictionary(StreamBuffer dout, IDictionary serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(68);
			}
			bool setKeyType;
			bool setValueType;
			this.SerializeDictionaryHeader(dout, serObject, out setKeyType, out setValueType);
			this.SerializeDictionaryElements(dout, serObject, setKeyType, setValueType);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		private void SerializeDictionaryHeader(StreamBuffer writer, Type dictType)
		{
			bool flag;
			bool flag2;
			this.SerializeDictionaryHeader(writer, dictType, out flag, out flag2);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000EEFC File Offset: 0x0000D0FC
		private void SerializeDictionaryHeader(StreamBuffer writer, object dict, out bool setKeyType, out bool setValueType)
		{
			Type[] genericArguments = dict.GetType().GetGenericArguments();
			setKeyType = (genericArguments[0] == typeof(object));
			setValueType = (genericArguments[1] == typeof(object));
			bool flag = setKeyType;
			if (flag)
			{
				writer.WriteByte(0);
			}
			else
			{
				Protocol16.GpType codeOfType = this.GetCodeOfType(genericArguments[0]);
				bool flag2 = codeOfType == Protocol16.GpType.Unknown || codeOfType == Protocol16.GpType.Dictionary;
				if (flag2)
				{
					string str = "Unexpected - cannot serialize Dictionary with key type: ";
					Type type = genericArguments[0];
					throw new Exception(str + ((type != null) ? type.ToString() : null));
				}
				writer.WriteByte((byte)codeOfType);
			}
			bool flag3 = setValueType;
			if (flag3)
			{
				writer.WriteByte(0);
			}
			else
			{
				Protocol16.GpType codeOfType2 = this.GetCodeOfType(genericArguments[1]);
				bool flag4 = codeOfType2 == Protocol16.GpType.Unknown;
				if (flag4)
				{
					string str2 = "Unexpected - cannot serialize Dictionary with value type: ";
					Type type2 = genericArguments[1];
					throw new Exception(str2 + ((type2 != null) ? type2.ToString() : null));
				}
				writer.WriteByte((byte)codeOfType2);
				bool flag5 = codeOfType2 == Protocol16.GpType.Dictionary;
				if (flag5)
				{
					this.SerializeDictionaryHeader(writer, genericArguments[1]);
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000F004 File Offset: 0x0000D204
		private void SerializeDictionaryElements(StreamBuffer writer, object dict, bool setKeyType, bool setValueType)
		{
			IDictionary dictionary = (IDictionary)dict;
			this.SerializeLengthAsShort(writer, dictionary.Count, "Dictionary elements");
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				bool flag = !setValueType && dictionaryEntry.Value == null;
				if (flag)
				{
					throw new Exception("Can't serialize null in Dictionary with specific value-type.");
				}
				bool flag2 = !setKeyType && dictionaryEntry.Key == null;
				if (flag2)
				{
					throw new Exception("Can't serialize null in Dictionary with specific key-type.");
				}
				this.Serialize(writer, dictionaryEntry.Key, setKeyType);
				this.Serialize(writer, dictionaryEntry.Value, setValueType);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public override object Deserialize(StreamBuffer din, byte type, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None)
		{
			byte b = type;
			byte b2 = b;
			if (b2 <= 42)
			{
				if (b2 == 0 || b2 == 42)
				{
					return null;
				}
			}
			else
			{
				if (b2 == 68)
				{
					return this.DeserializeDictionary(din);
				}
				switch (b2)
				{
				case 97:
					return this.DeserializeStringArray(din);
				case 98:
					return this.DeserializeByte(din);
				case 99:
				{
					byte customTypeCode = din.ReadByte();
					return this.DeserializeCustom(din, customTypeCode, IProtocol.DeserializationFlags.None);
				}
				case 100:
					return this.DeserializeDouble(din);
				case 101:
					return this.DeserializeEventData(din, null, IProtocol.DeserializationFlags.None);
				case 102:
					return this.DeserializeFloat(din);
				case 104:
					return this.DeserializeHashTable(din);
				case 105:
					return this.DeserializeInteger(din);
				case 107:
					return this.DeserializeShort(din);
				case 108:
					return this.DeserializeLong(din);
				case 110:
					return this.DeserializeIntArray(din, -1);
				case 111:
					return this.DeserializeBoolean(din);
				case 112:
					return this.DeserializeOperationResponse(din, flags);
				case 113:
					return this.DeserializeOperationRequest(din, flags);
				case 115:
					return this.DeserializeString(din);
				case 120:
					return this.DeserializeByteArray(din, -1);
				case 121:
					return this.DeserializeArray(din);
				case 122:
					return this.DeserializeObjectArray(din);
				}
			}
			throw new Exception(string.Concat(new string[]
			{
				"Deserialize(): ",
				type.ToString(),
				" pos: ",
				din.Position.ToString(),
				" bytes: ",
				din.Length.ToString(),
				". ",
				SupportClass.ByteArrayToString(din.GetBuffer(), -1)
			}));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000F31C File Offset: 0x0000D51C
		public override byte DeserializeByte(StreamBuffer din)
		{
			return din.ReadByte();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000F334 File Offset: 0x0000D534
		private bool DeserializeBoolean(StreamBuffer din)
		{
			return din.ReadByte() > 0;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000F350 File Offset: 0x0000D550
		public override short DeserializeShort(StreamBuffer din)
		{
			byte[] obj = this.memShort;
			short result;
			lock (obj)
			{
				byte[] array = this.memShort;
				din.Read(array, 0, 2);
				result = (short)((int)array[0] << 8 | (int)array[1]);
			}
			return result;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		private int DeserializeInteger(StreamBuffer din)
		{
			byte[] obj = this.memInteger;
			int result;
			lock (obj)
			{
				byte[] array = this.memInteger;
				din.Read(array, 0, 4);
				result = ((int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3]);
			}
			return result;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000F414 File Offset: 0x0000D614
		private long DeserializeLong(StreamBuffer din)
		{
			byte[] obj = this.memLong;
			long result;
			lock (obj)
			{
				byte[] array = this.memLong;
				din.Read(array, 0, 8);
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					result = (long)((ulong)array[0] << 56 | (ulong)array[1] << 48 | (ulong)array[2] << 40 | (ulong)array[3] << 32 | (ulong)array[4] << 24 | (ulong)array[5] << 16 | (ulong)array[6] << 8 | (ulong)array[7]);
				}
				else
				{
					result = BitConverter.ToInt64(array, 0);
				}
			}
			return result;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		private float DeserializeFloat(StreamBuffer din)
		{
			byte[] obj = this.memFloat;
			float result;
			lock (obj)
			{
				byte[] array = this.memFloat;
				din.Read(array, 0, 4);
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					byte b = array[0];
					byte b2 = array[1];
					array[0] = array[3];
					array[1] = array[2];
					array[2] = b2;
					array[3] = b;
				}
				result = BitConverter.ToSingle(array, 0);
			}
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000F540 File Offset: 0x0000D740
		private double DeserializeDouble(StreamBuffer din)
		{
			byte[] obj = this.memDouble;
			double result;
			lock (obj)
			{
				byte[] array = this.memDouble;
				din.Read(array, 0, 8);
				bool isLittleEndian = BitConverter.IsLittleEndian;
				if (isLittleEndian)
				{
					byte b = array[0];
					byte b2 = array[1];
					byte b3 = array[2];
					byte b4 = array[3];
					array[0] = array[7];
					array[1] = array[6];
					array[2] = array[5];
					array[3] = array[4];
					array[4] = b4;
					array[5] = b3;
					array[6] = b2;
					array[7] = b;
				}
				result = BitConverter.ToDouble(array, 0);
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		private string DeserializeString(StreamBuffer din)
		{
			short num = this.DeserializeShort(din);
			bool flag = num == 0;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				bool flag2 = num < 0;
				if (flag2)
				{
					throw new NotSupportedException("Received string type with unsupported length: " + num.ToString());
				}
				int index = 0;
				byte[] bufferAndAdvance = din.GetBufferAndAdvance((int)num, out index);
				result = Encoding.UTF8.GetString(bufferAndAdvance, index, (int)num);
			}
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F654 File Offset: 0x0000D854
		private Array DeserializeArray(StreamBuffer din)
		{
			short num = this.DeserializeShort(din);
			byte b = din.ReadByte();
			bool flag = b == 121;
			Array array2;
			if (flag)
			{
				Array array = this.DeserializeArray(din);
				Type type = array.GetType();
				array2 = Array.CreateInstance(type, (int)num);
				array2.SetValue(array, 0);
				for (short num2 = 1; num2 < num; num2 += 1)
				{
					array = this.DeserializeArray(din);
					array2.SetValue(array, (int)num2);
				}
			}
			else
			{
				bool flag2 = b == 120;
				if (flag2)
				{
					array2 = Array.CreateInstance(typeof(byte[]), (int)num);
					for (short num3 = 0; num3 < num; num3 += 1)
					{
						Array value = this.DeserializeByteArray(din, -1);
						array2.SetValue(value, (int)num3);
					}
				}
				else
				{
					bool flag3 = b == 98;
					if (flag3)
					{
						array2 = this.DeserializeByteArray(din, (int)num);
					}
					else
					{
						bool flag4 = b == 105;
						if (flag4)
						{
							array2 = this.DeserializeIntArray(din, (int)num);
						}
						else
						{
							bool flag5 = b == 99;
							if (flag5)
							{
								byte key = din.ReadByte();
								CustomType customType;
								bool flag6 = Protocol.CodeDict.TryGetValue(key, out customType);
								if (!flag6)
								{
									throw new Exception("Cannot find deserializer for custom type: " + key.ToString());
								}
								array2 = Array.CreateInstance(customType.Type, (int)num);
								for (int i = 0; i < (int)num; i++)
								{
									short num4 = this.DeserializeShort(din);
									bool flag7 = num4 < 0;
									if (flag7)
									{
										throw new InvalidDataException("DeserializeArray read negative objLength value: " + num4.ToString() + " before position: " + din.Position.ToString());
									}
									bool flag8 = customType.DeserializeStreamFunction == null;
									if (flag8)
									{
										byte[] array3 = new byte[(int)num4];
										din.Read(array3, 0, (int)num4);
										array2.SetValue(customType.DeserializeFunction(array3), i);
									}
									else
									{
										int position = din.Position;
										object value2 = customType.DeserializeStreamFunction(din, num4);
										int num5 = din.Position - position;
										bool flag9 = num5 != (int)num4;
										if (flag9)
										{
											din.Position = position + (int)num4;
										}
										array2.SetValue(value2, i);
									}
								}
							}
							else
							{
								bool flag10 = b == 68;
								if (flag10)
								{
									Array result = null;
									this.DeserializeDictionaryArray(din, num, out result);
									return result;
								}
								array2 = this.CreateArrayByType(b, num);
								for (short num6 = 0; num6 < num; num6 += 1)
								{
									array2.SetValue(this.Deserialize(din, b, IProtocol.DeserializationFlags.None), (int)num6);
								}
							}
						}
					}
				}
			}
			return array2;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000F900 File Offset: 0x0000DB00
		private byte[] DeserializeByteArray(StreamBuffer din, int size = -1)
		{
			bool flag = size == -1;
			if (flag)
			{
				size = this.DeserializeInteger(din);
			}
			byte[] array = new byte[size];
			din.Read(array, 0, size);
			return array;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000F938 File Offset: 0x0000DB38
		private int[] DeserializeIntArray(StreamBuffer din, int size = -1)
		{
			bool flag = size == -1;
			if (flag)
			{
				size = this.DeserializeInteger(din);
			}
			int[] array = new int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = this.DeserializeInteger(din);
			}
			return array;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000F984 File Offset: 0x0000DB84
		private string[] DeserializeStringArray(StreamBuffer din)
		{
			int num = (int)this.DeserializeShort(din);
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.DeserializeString(din);
			}
			return array;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private object[] DeserializeObjectArray(StreamBuffer din)
		{
			short num = this.DeserializeShort(din);
			object[] array = new object[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				byte type = din.ReadByte();
				array[i] = this.Deserialize(din, type, IProtocol.DeserializationFlags.None);
			}
			return array;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000FA10 File Offset: 0x0000DC10
		private Hashtable DeserializeHashTable(StreamBuffer din)
		{
			int num = (int)this.DeserializeShort(din);
			Hashtable hashtable = new Hashtable(num);
			for (int i = 0; i < num; i++)
			{
				object obj = this.Deserialize(din, din.ReadByte(), IProtocol.DeserializationFlags.None);
				object value = this.Deserialize(din, din.ReadByte(), IProtocol.DeserializationFlags.None);
				bool flag = obj == null;
				if (!flag)
				{
					hashtable[obj] = value;
				}
			}
			return hashtable;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000FA80 File Offset: 0x0000DC80
		private IDictionary DeserializeDictionary(StreamBuffer din)
		{
			byte b = din.ReadByte();
			byte b2 = din.ReadByte();
			bool flag = b == 68 || b == 121;
			if (flag)
			{
				throw new NotSupportedException("Client serialization protocol 1.6 does not support nesting Dictionary or Arrays into Dictionary keys.");
			}
			bool flag2 = b2 == 68 || b2 == 121;
			if (flag2)
			{
				throw new NotSupportedException("Client serialization protocol 1.6 does not support nesting Dictionary or Arrays into Dictionary values.");
			}
			int num = (int)this.DeserializeShort(din);
			bool flag3 = b == 0 || b == 42;
			bool flag4 = b2 == 0 || b2 == 42;
			Type typeOfCode = this.GetTypeOfCode(b);
			Type typeOfCode2 = this.GetTypeOfCode(b2);
			Type type = typeof(Dictionary<, >).MakeGenericType(new Type[]
			{
				typeOfCode,
				typeOfCode2
			});
			IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;
			for (int i = 0; i < num; i++)
			{
				object obj = this.Deserialize(din, flag3 ? din.ReadByte() : b, IProtocol.DeserializationFlags.None);
				object value = this.Deserialize(din, flag4 ? din.ReadByte() : b2, IProtocol.DeserializationFlags.None);
				bool flag5 = obj == null;
				if (!flag5)
				{
					dictionary.Add(obj, value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		private bool DeserializeDictionaryArray(StreamBuffer din, short size, out Array arrayResult)
		{
			byte b;
			byte b2;
			Type type = this.DeserializeDictionaryType(din, out b, out b2);
			arrayResult = Array.CreateInstance(type, (int)size);
			for (short num = 0; num < size; num += 1)
			{
				IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;
				bool flag = dictionary == null;
				if (flag)
				{
					return false;
				}
				short num2 = this.DeserializeShort(din);
				for (int i = 0; i < (int)num2; i++)
				{
					bool flag2 = b > 0;
					object obj;
					if (flag2)
					{
						obj = this.Deserialize(din, b, IProtocol.DeserializationFlags.None);
					}
					else
					{
						byte type2 = din.ReadByte();
						obj = this.Deserialize(din, type2, IProtocol.DeserializationFlags.None);
					}
					bool flag3 = b2 > 0;
					object value;
					if (flag3)
					{
						value = this.Deserialize(din, b2, IProtocol.DeserializationFlags.None);
					}
					else
					{
						byte type3 = din.ReadByte();
						value = this.Deserialize(din, type3, IProtocol.DeserializationFlags.None);
					}
					bool flag4 = obj == null;
					if (!flag4)
					{
						dictionary.Add(obj, value);
					}
				}
				arrayResult.SetValue(dictionary, (int)num);
			}
			return true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		private Type DeserializeDictionaryType(StreamBuffer reader, out byte keyTypeCode, out byte valTypeCode)
		{
			keyTypeCode = reader.ReadByte();
			valTypeCode = reader.ReadByte();
			Protocol16.GpType gpType = (Protocol16.GpType)keyTypeCode;
			Protocol16.GpType gpType2 = (Protocol16.GpType)valTypeCode;
			bool flag = gpType == Protocol16.GpType.Unknown;
			Type type;
			if (flag)
			{
				type = typeof(object);
			}
			else
			{
				bool flag2 = gpType == Protocol16.GpType.Dictionary || gpType == Protocol16.GpType.Array;
				if (flag2)
				{
					throw new NotSupportedException("Client serialization protocol 1.6 does not support nesting Dictionary or Arrays into Dictionary keys.");
				}
				type = this.GetTypeOfCode(keyTypeCode);
			}
			bool flag3 = gpType2 == Protocol16.GpType.Unknown;
			Type type2;
			if (flag3)
			{
				type2 = typeof(object);
			}
			else
			{
				bool flag4 = gpType2 == Protocol16.GpType.Dictionary || gpType2 == Protocol16.GpType.Array;
				if (flag4)
				{
					throw new NotSupportedException("Client serialization protocol 1.6 does not support nesting Dictionary or Arrays into Dictionary values.");
				}
				type2 = this.GetTypeOfCode(valTypeCode);
			}
			return typeof(Dictionary<, >).MakeGenericType(new Type[]
			{
				type,
				type2
			});
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public Protocol16()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000FE1C File Offset: 0x0000E01C
		// Note: this type is marked as 'beforefieldinit'.
		static Protocol16()
		{
		}

		// Token: 0x0400017D RID: 381
		private readonly byte[] versionBytes = new byte[]
		{
			1,
			6
		};

		// Token: 0x0400017E RID: 382
		private readonly byte[] memShort = new byte[2];

		// Token: 0x0400017F RID: 383
		private readonly long[] memLongBlock = new long[1];

		// Token: 0x04000180 RID: 384
		private readonly byte[] memLongBlockBytes = new byte[8];

		// Token: 0x04000181 RID: 385
		private static readonly float[] memFloatBlock = new float[1];

		// Token: 0x04000182 RID: 386
		private static readonly byte[] memFloatBlockBytes = new byte[4];

		// Token: 0x04000183 RID: 387
		private readonly double[] memDoubleBlock = new double[1];

		// Token: 0x04000184 RID: 388
		private readonly byte[] memDoubleBlockBytes = new byte[8];

		// Token: 0x04000185 RID: 389
		private readonly byte[] memInteger = new byte[4];

		// Token: 0x04000186 RID: 390
		private readonly byte[] memLong = new byte[8];

		// Token: 0x04000187 RID: 391
		private readonly byte[] memFloat = new byte[4];

		// Token: 0x04000188 RID: 392
		private readonly byte[] memDouble = new byte[8];

		// Token: 0x02000058 RID: 88
		public enum GpType : byte
		{
			// Token: 0x04000231 RID: 561
			Unknown,
			// Token: 0x04000232 RID: 562
			Array = 121,
			// Token: 0x04000233 RID: 563
			Boolean = 111,
			// Token: 0x04000234 RID: 564
			Byte = 98,
			// Token: 0x04000235 RID: 565
			ByteArray = 120,
			// Token: 0x04000236 RID: 566
			ObjectArray = 122,
			// Token: 0x04000237 RID: 567
			Short = 107,
			// Token: 0x04000238 RID: 568
			Float = 102,
			// Token: 0x04000239 RID: 569
			Dictionary = 68,
			// Token: 0x0400023A RID: 570
			Double = 100,
			// Token: 0x0400023B RID: 571
			Hashtable = 104,
			// Token: 0x0400023C RID: 572
			Integer,
			// Token: 0x0400023D RID: 573
			IntegerArray = 110,
			// Token: 0x0400023E RID: 574
			Long = 108,
			// Token: 0x0400023F RID: 575
			String = 115,
			// Token: 0x04000240 RID: 576
			StringArray = 97,
			// Token: 0x04000241 RID: 577
			Custom = 99,
			// Token: 0x04000242 RID: 578
			Null = 42,
			// Token: 0x04000243 RID: 579
			EventData = 101,
			// Token: 0x04000244 RID: 580
			OperationRequest = 113,
			// Token: 0x04000245 RID: 581
			OperationResponse = 112
		}
	}
}
