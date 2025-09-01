using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Security;
using System.Text;

namespace System.Resources
{
	/// <summary>Writes resources in the system-default format to an output file or an output stream. This class cannot be inherited.</summary>
	// Token: 0x0200086F RID: 2159
	[ComVisible(true)]
	public sealed class ResourceWriter : IResourceWriter, IDisposable
	{
		/// <summary>Gets or sets a delegate that enables resource assemblies to be written that target versions of the .NET Framework prior to the .NET Framework 4 by using qualified assembly names.</summary>
		/// <returns>The type that is encapsulated by the delegate.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x000EC342 File Offset: 0x000EA542
		// (set) Token: 0x060047FA RID: 18426 RVA: 0x000EC34A File Offset: 0x000EA54A
		public Func<Type, string> TypeNameConverter
		{
			get
			{
				return this.typeConverter;
			}
			set
			{
				this.typeConverter = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the specified file.</summary>
		/// <param name="fileName">The output file name.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047FB RID: 18427 RVA: 0x000EC354 File Offset: 0x000EA554
		public ResourceWriter(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this._output = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the provided stream.</summary>
		/// <param name="stream">The output stream.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047FC RID: 18428 RVA: 0x000EC3AC File Offset: 0x000EA5AC
		public ResourceWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this._output = stream;
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Adds a string resource to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The value of the resource.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this ResourceWriter.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable.</exception>
		// Token: 0x060047FD RID: 18429 RVA: 0x000EC414 File Offset: 0x000EA614
		public void AddResource(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a named resource specified as an object to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The value of the resource.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable.</exception>
		// Token: 0x060047FE RID: 18430 RVA: 0x000EC464 File Offset: 0x000EA664
		public void AddResource(string name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			if (value != null && value is Stream)
			{
				this.AddResourceInternal(name, (Stream)value, false);
				return;
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a named resource specified as a stream to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource to add.</param>
		/// <param name="value">The value of the resource to add. The resource must support the <see cref="P:System.IO.Stream.Length" /> property.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.  
		/// -or-  
		/// The stream does not support the <see cref="P:System.IO.Stream.Length" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed.</exception>
		// Token: 0x060047FF RID: 18431 RVA: 0x000EC4CB File Offset: 0x000EA6CB
		public void AddResource(string name, Stream value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this.AddResourceInternal(name, value, false);
		}

		/// <summary>Adds a named resource specified as a stream to the list of resources to be written, and specifies whether the stream should be closed after the <see cref="M:System.Resources.ResourceWriter.Generate" /> method is called.</summary>
		/// <param name="name">The name of the resource to add.</param>
		/// <param name="value">The value of the resource to add. The resource must support the <see cref="P:System.IO.Stream.Length" /> property.</param>
		/// <param name="closeAfterWrite">
		///   <see langword="true" /> to close the stream after the <see cref="M:System.Resources.ResourceWriter.Generate" /> method is called; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.  
		/// -or-  
		/// The stream does not support the <see cref="P:System.IO.Stream.Length" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed.</exception>
		// Token: 0x06004800 RID: 18432 RVA: 0x000EC4FC File Offset: 0x000EA6FC
		public void AddResource(string name, Stream value, bool closeAfterWrite)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this.AddResourceInternal(name, value, closeAfterWrite);
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x000EC530 File Offset: 0x000EA730
		private void AddResourceInternal(string name, Stream value, bool closeAfterWrite)
		{
			if (value == null)
			{
				this._caseInsensitiveDups.Add(name, null);
				this._resourceList.Add(name, value);
				return;
			}
			if (!value.CanSeek)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream does not support seeking."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, new ResourceWriter.StreamWrapper(value, closeAfterWrite));
		}

		/// <summary>Adds a named resource specified as a byte array to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">Value of the resource as an 8-bit unsigned integer array.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable.</exception>
		// Token: 0x06004802 RID: 18434 RVA: 0x000EC594 File Offset: 0x000EA794
		public void AddResource(string name, byte[] value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			this._resourceList.Add(name, value);
		}

		/// <summary>Adds a unit of data as a resource to the list of resources to be written.</summary>
		/// <param name="name">A name that identifies the resource that contains the added data.</param>
		/// <param name="typeName">The type name of the added data.</param>
		/// <param name="serializedData">A byte array that contains the binary representation of the added data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="typeName" />, or <paramref name="serializedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> (or a name that varies only by capitalization) has already been added to this <see cref="T:System.Resources.ResourceWriter" /> object.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceWriter" /> object is not initialized. The probable cause is that the <see cref="T:System.Resources.ResourceWriter" /> object is closed.</exception>
		// Token: 0x06004803 RID: 18435 RVA: 0x000EC5E4 File Offset: 0x000EA7E4
		public void AddResourceData(string name, string typeName, byte[] serializedData)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (serializedData == null)
			{
				throw new ArgumentNullException("serializedData");
			}
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			this._caseInsensitiveDups.Add(name, null);
			if (this._preserializedData == null)
			{
				this._preserializedData = new Dictionary<string, ResourceWriter.PrecannedResource>(FastResourceComparer.Default);
			}
			this._preserializedData.Add(name, new ResourceWriter.PrecannedResource(typeName, serializedData));
		}

		/// <summary>Saves the resources to the output stream and then closes it.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object.</exception>
		// Token: 0x06004804 RID: 18436 RVA: 0x000EC66B File Offset: 0x000EA86B
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x000EC674 File Offset: 0x000EA874
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._resourceList != null)
				{
					this.Generate();
				}
				if (this._output != null)
				{
					this._output.Close();
				}
			}
			this._output = null;
			this._caseInsensitiveDups = null;
		}

		/// <summary>Allows users to close the resource file or stream, explicitly releasing resources.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object.</exception>
		// Token: 0x06004806 RID: 18438 RVA: 0x000EC66B File Offset: 0x000EA86B
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Saves all resources to the output stream in the system default format.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error occurred during serialization of the object.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable.</exception>
		// Token: 0x06004807 RID: 18439 RVA: 0x000EC6A8 File Offset: 0x000EA8A8
		[SecuritySafeCritical]
		public void Generate()
		{
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			BinaryWriter binaryWriter = new BinaryWriter(this._output, Encoding.UTF8);
			List<string> list = new List<string>();
			binaryWriter.Write(ResourceManager.MagicNumber);
			binaryWriter.Write(ResourceManager.HeaderVersionNumber);
			MemoryStream memoryStream = new MemoryStream(240);
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
			binaryWriter2.Write(MultitargetingHelpers.GetAssemblyQualifiedName(typeof(ResourceReader), this.typeConverter));
			binaryWriter2.Write(ResourceManager.ResSetTypeName);
			binaryWriter2.Flush();
			binaryWriter.Write((int)memoryStream.Length);
			binaryWriter.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			binaryWriter.Write(2);
			int num = this._resourceList.Count;
			if (this._preserializedData != null)
			{
				num += this._preserializedData.Count;
			}
			binaryWriter.Write(num);
			int[] array = new int[num];
			int[] array2 = new int[num];
			int num2 = 0;
			MemoryStream memoryStream2 = new MemoryStream(num * 40);
			BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2, Encoding.Unicode);
			Stream stream = null;
			try
			{
				string tempFileName = Path.GetTempFileName();
				File.SetAttributes(tempFileName, FileAttributes.Temporary | FileAttributes.NotContentIndexed);
				stream = new FileStream(tempFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose | FileOptions.SequentialScan);
			}
			catch (UnauthorizedAccessException)
			{
				stream = new MemoryStream();
			}
			catch (IOException)
			{
				stream = new MemoryStream();
			}
			using (stream)
			{
				BinaryWriter binaryWriter4 = new BinaryWriter(stream, Encoding.UTF8);
				IFormatter objFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
				SortedList sortedList = new SortedList(this._resourceList, FastResourceComparer.Default);
				if (this._preserializedData != null)
				{
					foreach (KeyValuePair<string, ResourceWriter.PrecannedResource> keyValuePair in this._preserializedData)
					{
						sortedList.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IDictionaryEnumerator enumerator2 = sortedList.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					array[num2] = FastResourceComparer.HashFunction((string)enumerator2.Key);
					array2[num2++] = (int)binaryWriter3.Seek(0, SeekOrigin.Current);
					binaryWriter3.Write((string)enumerator2.Key);
					binaryWriter3.Write((int)binaryWriter4.Seek(0, SeekOrigin.Current));
					object value = enumerator2.Value;
					ResourceTypeCode resourceTypeCode = this.FindTypeCode(value, list);
					ResourceWriter.Write7BitEncodedInt(binaryWriter4, (int)resourceTypeCode);
					ResourceWriter.PrecannedResource precannedResource = value as ResourceWriter.PrecannedResource;
					if (precannedResource != null)
					{
						binaryWriter4.Write(precannedResource.Data);
					}
					else
					{
						this.WriteValue(resourceTypeCode, value, binaryWriter4, objFormatter);
					}
				}
				binaryWriter.Write(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					binaryWriter.Write(list[i]);
				}
				Array.Sort<int, int>(array, array2);
				binaryWriter.Flush();
				int num3 = (int)binaryWriter.BaseStream.Position & 7;
				if (num3 > 0)
				{
					for (int j = 0; j < 8 - num3; j++)
					{
						binaryWriter.Write("PAD"[j % 3]);
					}
				}
				foreach (int value2 in array)
				{
					binaryWriter.Write(value2);
				}
				foreach (int value3 in array2)
				{
					binaryWriter.Write(value3);
				}
				binaryWriter.Flush();
				binaryWriter3.Flush();
				binaryWriter4.Flush();
				int num4 = (int)(binaryWriter.Seek(0, SeekOrigin.Current) + memoryStream2.Length);
				num4 += 4;
				binaryWriter.Write(num4);
				binaryWriter.Write(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length);
				binaryWriter3.Close();
				stream.Position = 0L;
				stream.CopyTo(binaryWriter.BaseStream);
				binaryWriter4.Close();
			}
			binaryWriter.Flush();
			this._resourceList = null;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x000ECAD8 File Offset: 0x000EACD8
		private ResourceTypeCode FindTypeCode(object value, List<string> types)
		{
			if (value == null)
			{
				return ResourceTypeCode.Null;
			}
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return ResourceTypeCode.String;
			}
			if (type == typeof(int))
			{
				return ResourceTypeCode.Int32;
			}
			if (type == typeof(bool))
			{
				return ResourceTypeCode.Boolean;
			}
			if (type == typeof(char))
			{
				return ResourceTypeCode.Char;
			}
			if (type == typeof(byte))
			{
				return ResourceTypeCode.Byte;
			}
			if (type == typeof(sbyte))
			{
				return ResourceTypeCode.SByte;
			}
			if (type == typeof(short))
			{
				return ResourceTypeCode.Int16;
			}
			if (type == typeof(long))
			{
				return ResourceTypeCode.Int64;
			}
			if (type == typeof(ushort))
			{
				return ResourceTypeCode.UInt16;
			}
			if (type == typeof(uint))
			{
				return ResourceTypeCode.UInt32;
			}
			if (type == typeof(ulong))
			{
				return ResourceTypeCode.UInt64;
			}
			if (type == typeof(float))
			{
				return ResourceTypeCode.Single;
			}
			if (type == typeof(double))
			{
				return ResourceTypeCode.Double;
			}
			if (type == typeof(decimal))
			{
				return ResourceTypeCode.Decimal;
			}
			if (type == typeof(DateTime))
			{
				return ResourceTypeCode.DateTime;
			}
			if (type == typeof(TimeSpan))
			{
				return ResourceTypeCode.TimeSpan;
			}
			if (type == typeof(byte[]))
			{
				return ResourceTypeCode.ByteArray;
			}
			if (type == typeof(ResourceWriter.StreamWrapper))
			{
				return ResourceTypeCode.Stream;
			}
			string text;
			if (type == typeof(ResourceWriter.PrecannedResource))
			{
				text = ((ResourceWriter.PrecannedResource)value).TypeName;
				if (text.StartsWith("ResourceTypeCode.", StringComparison.Ordinal))
				{
					text = text.Substring(17);
					return (ResourceTypeCode)Enum.Parse(typeof(ResourceTypeCode), text);
				}
			}
			else
			{
				text = MultitargetingHelpers.GetAssemblyQualifiedName(type, this.typeConverter);
			}
			int num = types.IndexOf(text);
			if (num == -1)
			{
				num = types.Count;
				types.Add(text);
			}
			return num + ResourceTypeCode.StartOfUserTypes;
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x000ECCDC File Offset: 0x000EAEDC
		private void WriteValue(ResourceTypeCode typeCode, object value, BinaryWriter writer, IFormatter objFormatter)
		{
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return;
			case ResourceTypeCode.String:
				writer.Write((string)value);
				return;
			case ResourceTypeCode.Boolean:
				writer.Write((bool)value);
				return;
			case ResourceTypeCode.Char:
				writer.Write((ushort)((char)value));
				return;
			case ResourceTypeCode.Byte:
				writer.Write((byte)value);
				return;
			case ResourceTypeCode.SByte:
				writer.Write((sbyte)value);
				return;
			case ResourceTypeCode.Int16:
				writer.Write((short)value);
				return;
			case ResourceTypeCode.UInt16:
				writer.Write((ushort)value);
				return;
			case ResourceTypeCode.Int32:
				writer.Write((int)value);
				return;
			case ResourceTypeCode.UInt32:
				writer.Write((uint)value);
				return;
			case ResourceTypeCode.Int64:
				writer.Write((long)value);
				return;
			case ResourceTypeCode.UInt64:
				writer.Write((ulong)value);
				return;
			case ResourceTypeCode.Single:
				writer.Write((float)value);
				return;
			case ResourceTypeCode.Double:
				writer.Write((double)value);
				return;
			case ResourceTypeCode.Decimal:
				writer.Write((decimal)value);
				return;
			case ResourceTypeCode.DateTime:
			{
				long value2 = ((DateTime)value).ToBinary();
				writer.Write(value2);
				return;
			}
			case ResourceTypeCode.TimeSpan:
				writer.Write(((TimeSpan)value).Ticks);
				return;
			case ResourceTypeCode.ByteArray:
			{
				byte[] array = (byte[])value;
				writer.Write(array.Length);
				writer.Write(array, 0, array.Length);
				return;
			}
			case ResourceTypeCode.Stream:
			{
				ResourceWriter.StreamWrapper streamWrapper = (ResourceWriter.StreamWrapper)value;
				if (streamWrapper.m_stream.GetType() == typeof(MemoryStream))
				{
					MemoryStream memoryStream = (MemoryStream)streamWrapper.m_stream;
					if (memoryStream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					int index;
					int num;
					memoryStream.InternalGetOriginAndLength(out index, out num);
					byte[] buffer = memoryStream.InternalGetBuffer();
					writer.Write(num);
					writer.Write(buffer, index, num);
					return;
				}
				else
				{
					Stream stream = streamWrapper.m_stream;
					if (stream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					stream.Position = 0L;
					writer.Write((int)stream.Length);
					byte[] array2 = new byte[4096];
					int count;
					while ((count = stream.Read(array2, 0, array2.Length)) != 0)
					{
						writer.Write(array2, 0, count);
					}
					if (streamWrapper.m_closeAfterWrite)
					{
						stream.Close();
						return;
					}
					return;
				}
				break;
			}
			}
			objFormatter.Serialize(writer.BaseStream, value);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x000ECF7C File Offset: 0x000EB17C
		private static void Write7BitEncodedInt(BinaryWriter store, int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				store.Write((byte)(num | 128U));
			}
			store.Write((byte)num);
		}

		// Token: 0x04002E06 RID: 11782
		private Func<Type, string> typeConverter;

		// Token: 0x04002E07 RID: 11783
		private const int _ExpectedNumberOfResources = 1000;

		// Token: 0x04002E08 RID: 11784
		private const int AverageNameSize = 40;

		// Token: 0x04002E09 RID: 11785
		private const int AverageValueSize = 40;

		// Token: 0x04002E0A RID: 11786
		private Dictionary<string, object> _resourceList;

		// Token: 0x04002E0B RID: 11787
		internal Stream _output;

		// Token: 0x04002E0C RID: 11788
		private Dictionary<string, object> _caseInsensitiveDups;

		// Token: 0x04002E0D RID: 11789
		private Dictionary<string, ResourceWriter.PrecannedResource> _preserializedData;

		// Token: 0x04002E0E RID: 11790
		private const int _DefaultBufferSize = 4096;

		// Token: 0x02000870 RID: 2160
		private class PrecannedResource
		{
			// Token: 0x0600480B RID: 18443 RVA: 0x000ECFAF File Offset: 0x000EB1AF
			internal PrecannedResource(string typeName, byte[] data)
			{
				this.TypeName = typeName;
				this.Data = data;
			}

			// Token: 0x04002E0F RID: 11791
			internal string TypeName;

			// Token: 0x04002E10 RID: 11792
			internal byte[] Data;
		}

		// Token: 0x02000871 RID: 2161
		private class StreamWrapper
		{
			// Token: 0x0600480C RID: 18444 RVA: 0x000ECFC5 File Offset: 0x000EB1C5
			internal StreamWrapper(Stream s, bool closeAfterWrite)
			{
				this.m_stream = s;
				this.m_closeAfterWrite = closeAfterWrite;
			}

			// Token: 0x04002E11 RID: 11793
			internal Stream m_stream;

			// Token: 0x04002E12 RID: 11794
			internal bool m_closeAfterWrite;
		}
	}
}
