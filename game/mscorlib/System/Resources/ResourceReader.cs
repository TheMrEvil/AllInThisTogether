using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
	/// <summary>Enumerates the resources in a binary resources (.resources) file by reading sequential resource name/value pairs.</summary>
	// Token: 0x0200086C RID: 2156
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified named resource file.</summary>
		/// <param name="fileName">The path and name of the resource file to read. filename is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.BadImageFormatException">The resource file has an invalid format. For example, the length of the file may be zero.</exception>
		// Token: 0x060047BD RID: 18365 RVA: 0x000EA580 File Offset: 0x000E8780
		[SecuritySafeCritical]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false, false, false), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">The input stream for reading resources.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not readable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred while accessing <paramref name="stream" />.</exception>
		// Token: 0x060047BE RID: 18366 RVA: 0x000EA5F8 File Offset: 0x000E87F8
		[SecurityCritical]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not readable."));
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this.ReadResources();
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000EA664 File Offset: 0x000E8864
		[SecurityCritical]
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this.ReadResources();
		}

		/// <summary>Releases all operating system resources associated with this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		// Token: 0x060047C0 RID: 18368 RVA: 0x000EA696 File Offset: 0x000E8896
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Resources.ResourceReader" /> class.</summary>
		// Token: 0x060047C1 RID: 18369 RVA: 0x000EA69F File Offset: 0x000E889F
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x000EA6A8 File Offset: 0x000E88A8
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x000EA70C File Offset: 0x000E890C
		[SecurityCritical]
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | (int)((byte*)p)[1] << 8 | (int)((byte*)p)[2] << 16 | (int)((byte*)p)[3] << 24;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x000EA734 File Offset: 0x000E8934
		private void SkipInt32()
		{
			this._store.BaseStream.Seek(4L, SeekOrigin.Current);
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x000EA74C File Offset: 0x000E894C
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000EA78D File Offset: 0x000E898D
		[SecuritySafeCritical]
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x000EA7B4 File Offset: 0x000E89B4
		[SecuritySafeCritical]
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into name section.", new object[]
				{
					num
				}));
			}
			return num;
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has already been closed and cannot be accessed.</exception>
		// Token: 0x060047C8 RID: 18376 RVA: 0x000EA81B File Offset: 0x000E8A1B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has been closed or disposed, and cannot be accessed.</exception>
		// Token: 0x060047C9 RID: 18377 RVA: 0x000EA823 File Offset: 0x000E8A23
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x000EA843 File Offset: 0x000E8A43
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x000EA84C File Offset: 0x000E8A4C
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[]
							{
								num5
							}));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x000EA9C0 File Offset: 0x000E8BC0
		[SecuritySafeCritical]
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. A resource name extends past the end of the stream."));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Resource name extends past the end of the file."));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x000EAA88 File Offset: 0x000E8C88
		[SecurityCritical]
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array3;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String for name index '{0}' extends past the end of the file.", new object[]
						{
							index
						}));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string result;
					if (!BitConverter.IsLittleEndian)
					{
						byte* ptr = (byte*)positionPointer;
						byte[] array = new byte[num2];
						for (int i = 0; i < num2; i += 2)
						{
							array[i] = (ptr + i)[1];
							array[i + 1] = ptr[i];
						}
						byte[] array2;
						byte* value;
						if ((array2 = array) == null || array2.Length == 0)
						{
							value = null;
						}
						else
						{
							value = &array2[0];
						}
						result = new string((char*)value, 0, num2 / 2);
						array2 = null;
					}
					else
					{
						result = new string(positionPointer, 0, num2 / 2);
					}
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[]
						{
							dataOffset
						}));
					}
					return result;
				}
				else
				{
					array3 = new byte[num2];
					int num3;
					for (int j = num2; j > 0; j -= num3)
					{
						num3 = this._store.Read(array3, num2 - j, j);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("Corrupt .resources file. The resource name for name index {0} extends past the end of the stream.", new object[]
							{
								index
							}));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[]
						{
							dataOffset
						}));
					}
				}
			}
			return Encoding.Unicode.GetString(array3, 0, num2);
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x000EACF8 File Offset: 0x000E8EF8
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object result;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[]
					{
						num2
					}));
				}
				if (this._version == 1)
				{
					result = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					result = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return result;
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x000EADC4 File Offset: 0x000E8FC4
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string result = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[]
					{
						this.FindType(num).FullName
					}));
				}
				result = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text = resourceTypeCode.ToString();
					}
					else
					{
						text = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[]
					{
						text
					}));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					result = this._store.ReadString();
				}
			}
			return result;
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x000EAEB0 File Offset: 0x000E90B0
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x000EAED8 File Offset: 0x000E90D8
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000EAF10 File Offset: 0x000E9110
		internal object LoadObjectV1(int pos)
		{
			object result;
			try
			{
				result = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), inner2);
			}
			return result;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x000EAF68 File Offset: 0x000E9168
		[SecuritySafeCritical]
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType left = this.FindType(num);
			if (left == typeof(string))
			{
				return this._store.ReadString();
			}
			if (left == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (left == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (left == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (left == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (left == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (left == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (left == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (left == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (left == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (left == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (left == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (left == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (left == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000EB1C0 File Offset: 0x000E93C0
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object result;
			try
			{
				result = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), inner2);
			}
			return result;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x000EB21C File Offset: 0x000E941C
		[SecuritySafeCritical]
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
				return DateTime.FromBinary(this._store.ReadInt64());
			case ResourceTypeCode.TimeSpan:
				return new TimeSpan(this._store.ReadInt64());
			case ResourceTypeCode.ByteArray:
			{
				int num = this._store.ReadInt32();
				if (num < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[]
					{
						num
					}));
				}
				if (this._ums == null)
				{
					if ((long)num > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[]
						{
							num
						}));
					}
					return this._store.ReadBytes(num);
				}
				else
				{
					if ((long)num > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[]
						{
							num
						}));
					}
					byte[] array = new byte[num];
					this._ums.Read(array, 0, num);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num2 = this._store.ReadInt32();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[]
					{
						num2
					}));
				}
				if (this._ums == null)
				{
					return new PinnedBufferMemoryStream(this._store.ReadBytes(num2));
				}
				if ((long)num2 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[]
					{
						num2
					}));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num2, (long)num2, FileAccess.Read);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."));
			}
			int typeIndex = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(typeIndex);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x000EB58C File Offset: 0x000E978C
		[SecurityCritical]
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			object obj = this._objFormatter.Deserialize(this._store.BaseStream);
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("The type serialized in the .resources file was not the same type that the .resources file said it contained. Expected '{0}' but read '{1}'.", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x000EB5F4 File Offset: 0x000E97F4
		[SecurityCritical]
		private void ReadResources()
		{
			BinaryFormatter objFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._objFormatter = objFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), inner);
			}
			catch (IndexOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), inner2);
			}
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x000EB660 File Offset: 0x000E9860
		[SecurityCritical]
		private unsafe void _ReadResources()
		{
			if (this._store.ReadInt32() != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream is not a valid resource file."));
			}
			int num = this._store.ReadInt32();
			int num2 = this._store.ReadInt32();
			if (num2 < 0 || num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			if (num > 1)
			{
				this._store.BaseStream.Seek((long)num2, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName asmName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, asmName))
				{
					throw new NotSupportedException(Environment.GetResourceString("This .resources file should not be read with this reader. The resource reader type is \"{0}\".", new object[]
					{
						text
					}));
				}
				this.SkipString();
			}
			int num3 = this._store.ReadInt32();
			if (num3 != 2 && num3 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The ResourceReader class does not know how to read this version of .resources files. Expected version: {0}  This file: {1}", new object[]
				{
					2,
					num3
				}));
			}
			this._version = num3;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			int num4 = this._store.ReadInt32();
			if (num4 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._typeTable = new RuntimeType[num4];
			this._typeNamePositions = new int[num4];
			for (int i = 0; i < num4; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			int num5 = (int)this._store.BaseStream.Position & 7;
			if (num5 != 0)
			{
				for (int j = 0; j < 8 - num5; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)-536870912)) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num6 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num6, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num7 = this._store.ReadInt32();
					if (num7 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
					}
					this._namePositions[l] = num7;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)-536870912)) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num8 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num8, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x000EB9E0 File Offset: 0x000E9BE0
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't exist."));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string typeName = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(typeName, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		/// <summary>Retrieves the type name and data of a named resource from an open resource file or stream.</summary>
		/// <param name="resourceName">The name of a resource.</param>
		/// <param name="resourceType">When this method returns, contains a string that represents the type name of the retrieved resource. This parameter is passed uninitialized.</param>
		/// <param name="resourceData">When this method returns, contains a byte array that is the binary representation of the retrieved type. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="resourceName" /> does not exist.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="resourceName" /> has an invalid type.</exception>
		/// <exception cref="T:System.FormatException">The retrieved resource data is corrupt.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceReader" /> object is not initialized, probably because it is closed.</exception>
		// Token: 0x060047DA RID: 18394 RVA: 0x000EBA94 File Offset: 0x000E9C94
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("The specified resource name \"{0}\" does not exist in the resource file.", new object[]
				{
					resourceName
				}));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into name section.", new object[]
						{
							num2
						}));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[]
						{
							num3
						}));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				int num5 = (int)(((num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length) - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't exist."));
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num5 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num5);
				if (array2.Length != num5)
				{
					throw new FormatException(Environment.GetResourceString("Corrupt .resources file. A resource name extends past the end of the stream."));
				}
				resourceData = array2;
			}
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x000EBCF0 File Offset: 0x000E9EF0
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string result;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				result = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return result;
		}

		// Token: 0x04002DEE RID: 11758
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04002DEF RID: 11759
		private BinaryReader _store;

		// Token: 0x04002DF0 RID: 11760
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x04002DF1 RID: 11761
		private long _nameSectionOffset;

		// Token: 0x04002DF2 RID: 11762
		private long _dataSectionOffset;

		// Token: 0x04002DF3 RID: 11763
		private int[] _nameHashes;

		// Token: 0x04002DF4 RID: 11764
		[SecurityCritical]
		private unsafe int* _nameHashesPtr;

		// Token: 0x04002DF5 RID: 11765
		private int[] _namePositions;

		// Token: 0x04002DF6 RID: 11766
		[SecurityCritical]
		private unsafe int* _namePositionsPtr;

		// Token: 0x04002DF7 RID: 11767
		private RuntimeType[] _typeTable;

		// Token: 0x04002DF8 RID: 11768
		private int[] _typeNamePositions;

		// Token: 0x04002DF9 RID: 11769
		private BinaryFormatter _objFormatter;

		// Token: 0x04002DFA RID: 11770
		private int _numResources;

		// Token: 0x04002DFB RID: 11771
		private UnmanagedMemoryStream _ums;

		// Token: 0x04002DFC RID: 11772
		private int _version;

		// Token: 0x0200086D RID: 2157
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060047DC RID: 18396 RVA: 0x000EBD7C File Offset: 0x000E9F7C
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x060047DD RID: 18397 RVA: 0x000EBD9C File Offset: 0x000E9F9C
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x17000B04 RID: 2820
			// (get) Token: 0x060047DE RID: 18398 RVA: 0x000EBDF8 File Offset: 0x000E9FF8
			public object Key
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x17000B05 RID: 2821
			// (get) Token: 0x060047DF RID: 18399 RVA: 0x000EBE6E File Offset: 0x000EA06E
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000B06 RID: 2822
			// (get) Token: 0x060047E0 RID: 18400 RVA: 0x000EBE7B File Offset: 0x000EA07B
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x17000B07 RID: 2823
			// (get) Token: 0x060047E1 RID: 18401 RVA: 0x000EBE84 File Offset: 0x000EA084
			public DictionaryEntry Entry
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string key;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							key = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(key, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(key, obj);
				}
			}

			// Token: 0x17000B08 RID: 2824
			// (get) Token: 0x060047E2 RID: 18402 RVA: 0x000EBFB4 File Offset: 0x000EA1B4
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x060047E3 RID: 18403 RVA: 0x000EC024 File Offset: 0x000EA224
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x04002DFD RID: 11773
			private const int ENUM_DONE = -2147483648;

			// Token: 0x04002DFE RID: 11774
			private const int ENUM_NOT_STARTED = -1;

			// Token: 0x04002DFF RID: 11775
			private ResourceReader _reader;

			// Token: 0x04002E00 RID: 11776
			private bool _currentIsValid;

			// Token: 0x04002E01 RID: 11777
			private int _currentName;

			// Token: 0x04002E02 RID: 11778
			private int _dataPosition;
		}
	}
}
