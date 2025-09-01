using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Represents a mutable string of characters. This class cannot be inherited.</summary>
	// Token: 0x020003AB RID: 939
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class StringBuilder : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class.</summary>
		// Token: 0x06002676 RID: 9846 RVA: 0x000887B3 File Offset: 0x000869B3
		public StringBuilder()
		{
			this.m_MaxCapacity = int.MaxValue;
			this.m_ChunkChars = new char[16];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified capacity.</summary>
		/// <param name="capacity">The suggested starting size of this instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002677 RID: 9847 RVA: 0x000887D3 File Offset: 0x000869D3
		public StringBuilder(int capacity) : this(capacity, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		// Token: 0x06002678 RID: 9848 RVA: 0x000887E1 File Offset: 0x000869E1
		public StringBuilder(string value) : this(value, 16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002679 RID: 9849 RVA: 0x000887EC File Offset: 0x000869EC
		public StringBuilder(string value, int capacity) : this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value" /> is <see langword="null" />, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />).</param>
		/// <param name="startIndex">The position within <paramref name="value" /> where the substring begins.</param>
		/// <param name="length">The number of characters in the substring.</param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="length" /> is not a position within <paramref name="value" />.</exception>
		// Token: 0x0600267A RID: 9850 RVA: 0x00088804 File Offset: 0x00086A04
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format("'{0}' must be non-negative.", "length"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			capacity = Math.Max(capacity, length);
			this.m_ChunkChars = new char[capacity];
			this.m_ChunkLength = length;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				StringBuilder.ThreadSafeCopy(ptr + startIndex, this.m_ChunkChars, 0, length);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class that starts with a specified capacity and can grow to a specified maximum.</summary>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />.</param>
		/// <param name="maxCapacity">The maximum number of characters the current string can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxCapacity" /> is less than one, <paramref name="capacity" /> is less than zero, or <paramref name="capacity" /> is greater than <paramref name="maxCapacity" />.</exception>
		// Token: 0x0600267B RID: 9851 RVA: 0x000888E4 File Offset: 0x00086AE4
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity exceeds maximum capacity.");
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", "MaxCapacity must be one or greater.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = new char[capacity];
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00088960 File Offset: 0x00086B60
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException("The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length.");
			}
			if (!flag)
			{
				num = Math.Min(Math.Max(16, text.Length), num2);
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException("The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length.");
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = new char[num];
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
			this.m_ChunkPrevious = null;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600267D RID: 9853 RVA: 0x00088A90 File Offset: 0x00086C90
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00088AEC File Offset: 0x00086CEC
		[Conditional("DEBUG")]
		private void AssertInvariants()
		{
			StringBuilder stringBuilder = this;
			int maxCapacity = this.m_MaxCapacity;
			for (;;)
			{
				StringBuilder chunkPrevious = stringBuilder.m_ChunkPrevious;
				if (chunkPrevious == null)
				{
					break;
				}
				stringBuilder = chunkPrevious;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.</summary>
		/// <returns>The maximum number of characters that can be contained in the memory allocated by the current instance. Its value can range from <see cref="P:System.Text.StringBuilder.Length" /> to <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.  
		///  -or-  
		///  The value specified for a set operation is greater than the maximum capacity.</exception>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x00088B10 File Offset: 0x00086D10
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x00088B24 File Offset: 0x00086D24
		public int Capacity
		{
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity must be positive.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity exceeds maximum capacity.");
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (this.Capacity != value)
				{
					char[] array = new char[value - this.m_ChunkOffset];
					Array.Copy(this.m_ChunkChars, 0, array, 0, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		/// <summary>Gets the maximum capacity of this instance.</summary>
		/// <returns>The maximum number of characters this instance can hold.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00088BA9 File Offset: 0x00086DA9
		public int MaxCapacity
		{
			get
			{
				return this.m_MaxCapacity;
			}
		}

		/// <summary>Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder" /> is at least the specified value.</summary>
		/// <param name="capacity">The minimum capacity to ensure.</param>
		/// <returns>The new capacity of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002682 RID: 9858 RVA: 0x00088BB1 File Offset: 0x00086DB1
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		/// <summary>Converts the value of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as this instance.</returns>
		// Token: 0x06002683 RID: 9859 RVA: 0x00088BE0 File Offset: 0x00086DE0
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (;;)
				{
					if (stringBuilder.m_ChunkLength > 0)
					{
						char[] chunkChars = stringBuilder.m_ChunkChars;
						int chunkOffset = stringBuilder.m_ChunkOffset;
						int chunkLength = stringBuilder.m_ChunkLength;
						if (chunkLength + chunkOffset > text.Length || chunkLength > chunkChars.Length)
						{
							break;
						}
						fixed (char* ptr2 = &chunkChars[0])
						{
							char* smem = ptr2;
							string.wstrcpy(ptr + chunkOffset, smem, chunkLength);
						}
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						return text;
					}
				}
				throw new ArgumentOutOfRangeException("chunkLength", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		/// <summary>Converts the value of a substring of this instance to a <see cref="T:System.String" />.</summary>
		/// <param name="startIndex">The starting position of the substring in this instance.</param>
		/// <param name="length">The length of the substring.</param>
		/// <returns>A string whose value is the same as the specified substring of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.  
		/// -or-  
		/// The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the length of the current instance.</exception>
		// Token: 0x06002684 RID: 9860 RVA: 0x00088C90 File Offset: 0x00086E90
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be larger than length of string.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			string text;
			string result = text = string.FastAllocateString(length);
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			this.CopyTo(startIndex, new Span<char>((void*)ptr, length), length);
			return result;
		}

		/// <summary>Removes all characters from the current <see cref="T:System.Text.StringBuilder" /> instance.</summary>
		/// <returns>An object whose <see cref="P:System.Text.StringBuilder.Length" /> is 0 (zero).</returns>
		// Token: 0x06002685 RID: 9861 RVA: 0x00088D1B File Offset: 0x00086F1B
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		/// <summary>Gets or sets the length of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>The length of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00088D25 File Offset: 0x00086F25
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x00088D34 File Offset: 0x00086F34
		public int Length
		{
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Length cannot be less than zero.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					int num2 = Math.Min(this.Capacity, Math.Max(this.Length * 6 / 5, this.m_ChunkChars.Length)) - stringBuilder.m_ChunkOffset;
					if (num2 > stringBuilder.m_ChunkChars.Length)
					{
						char[] array = new char[num2];
						Array.Copy(stringBuilder.m_ChunkChars, 0, array, 0, stringBuilder.m_ChunkLength);
						this.m_ChunkChars = array;
					}
					else
					{
						this.m_ChunkChars = stringBuilder.m_ChunkChars;
					}
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		/// <summary>Gets or sets the character at the specified character position in this instance.</summary>
		/// <param name="index">The position of the character.</param>
		/// <returns>The Unicode character at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while setting a character.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while getting a character.</exception>
		// Token: 0x170004BA RID: 1210
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to this instance.</summary>
		/// <param name="value">The character to append.</param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="repeatCount" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		// Token: 0x0600268A RID: 9866 RVA: 0x00088ED4 File Offset: 0x000870D4
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "Count cannot be less than zero.");
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.Length + repeatCount;
			if (num > this.m_MaxCapacity || num < repeatCount)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "The length cannot be greater than the capacity.");
			}
			int num2 = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num2 < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num2++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num2;
					this.ExpandByABlock(repeatCount);
					num2 = 0;
				}
			}
			this.m_ChunkLength = num2;
			return this;
		}

		/// <summary>Appends the string representation of a specified subarray of Unicode characters to this instance.</summary>
		/// <param name="value">A character array.</param>
		/// <param name="startIndex">The starting position in <paramref name="value" />.</param>
		/// <param name="charCount">The number of characters to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> + <paramref name="charCount" /> is greater than the length of <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600268B RID: 9867 RVA: 0x00088F64 File Offset: 0x00087164
		public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value must be positive.");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("charCount", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* value2 = ptr;
					this.Append(value2, charCount);
					return this;
				}
			}
		}

		/// <summary>Appends a copy of the specified string to this instance.</summary>
		/// <param name="value">The string to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600268C RID: 9868 RVA: 0x00088FE4 File Offset: 0x000871E4
		public unsafe StringBuilder Append(string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				int num = chunkLength + length;
				if (num < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						fixed (string text = value)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (char* ptr2 = &chunkChars[chunkLength])
							{
								string.wstrcpy(ptr2, ptr, length);
							}
						}
					}
					this.m_ChunkLength = num;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0008907C File Offset: 0x0008727C
		private unsafe void AppendHelper(string value)
		{
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr, value.Length);
			}
		}

		/// <summary>Appends a copy of a specified substring to this instance.</summary>
		/// <param name="value">The string that contains the substring to append.</param>
		/// <param name="startIndex">The starting position of the substring within <paramref name="value" />.</param>
		/// <param name="count">The number of characters in <paramref name="value" /> to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="count" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600268E RID: 9870 RVA: 0x000890AC File Offset: 0x000872AC
		public unsafe StringBuilder Append(string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				char* ptr = value;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr + startIndex, count);
				return this;
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00089136 File Offset: 0x00087336
		public StringBuilder Append(StringBuilder value)
		{
			if (value != null && value.Length != 0)
			{
				return this.AppendCore(value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00089154 File Offset: 0x00087354
		public StringBuilder Append(StringBuilder value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (count > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this.AppendCore(value, startIndex, count);
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000891C8 File Offset: 0x000873C8
		private StringBuilder AppendCore(StringBuilder value, int startIndex, int count)
		{
			if (value == this)
			{
				return this.Append(value.ToString(startIndex, count));
			}
			if (this.Length + count > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("Capacity", "Capacity exceeds maximum capacity.");
			}
			while (count > 0)
			{
				int num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				if (num == 0)
				{
					this.ExpandByABlock(count);
					num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				}
				value.CopyTo(startIndex, new Span<char>(this.m_ChunkChars, this.m_ChunkLength, num), num);
				this.m_ChunkLength += num;
				startIndex += num;
				count -= num;
			}
			return this;
		}

		/// <summary>Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002692 RID: 9874 RVA: 0x00089275 File Offset: 0x00087475
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="value">The string to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002693 RID: 9875 RVA: 0x00089282 File Offset: 0x00087482
		public StringBuilder AppendLine(string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		/// <summary>Copies the characters from a specified segment of this instance to a specified segment of a destination <see cref="T:System.Char" /> array.</summary>
		/// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.</param>
		/// <param name="destination">The array where characters will be copied.</param>
		/// <param name="destinationIndex">The starting position in <paramref name="destination" /> where characters will be copied. The index is zero-based.</param>
		/// <param name="count">The number of characters to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" />, is less than zero.  
		/// -or-  
		/// <paramref name="sourceIndex" /> is greater than the length of this instance.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceIndex" /> + <paramref name="count" /> is greater than the length of this instance.  
		/// -or-  
		/// <paramref name="destinationIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="destination" />.</exception>
		// Token: 0x06002694 RID: 9876 RVA: 0x00089298 File Offset: 0x00087498
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.Format("'{0}' must be non-negative.", "destinationIndex"));
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentException("Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.");
			}
			this.CopyTo(sourceIndex, new Span<char>(destination).Slice(destinationIndex), count);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00089300 File Offset: 0x00087500
		public void CopyTo(int sourceIndex, Span<char> destination, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException("Source string was not long enough. Check sourceIndex and count.");
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					num3 = Math.Min(num3, stringBuilder.m_ChunkLength);
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The string to insert.</param>
		/// <param name="count">The number of times to insert <paramref name="value" />.</param>
		/// <returns>A reference to this instance after insertion has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> times <paramref name="count" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002696 RID: 9878 RVA: 0x000893B4 File Offset: 0x000875B4
		public unsafe StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (string.IsNullOrEmpty(value) || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			char* ptr = value;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			while (count > 0)
			{
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
				count--;
			}
			return this;
		}

		/// <summary>Removes the specified range of characters from this instance.</summary>
		/// <param name="startIndex">The zero-based position in this instance where removal begins.</param>
		/// <param name="length">The number of characters to remove.</param>
		/// <returns>A reference to this instance after the excise operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, or <paramref name="startIndex" /> + <paramref name="length" /> is greater than the length of this instance.</exception>
		// Token: 0x06002697 RID: 9879 RVA: 0x00089464 File Offset: 0x00087664
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified Boolean value to this instance.</summary>
		/// <param name="value">The Boolean value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002698 RID: 9880 RVA: 0x000894DA File Offset: 0x000876DA
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified <see cref="T:System.Char" /> object to this instance.</summary>
		/// <param name="value">The UTF-16-encoded code unit to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x06002699 RID: 9881 RVA: 0x000894EC File Offset: 0x000876EC
		public StringBuilder Append(char value)
		{
			if (this.m_ChunkLength < this.m_ChunkChars.Length)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				this.m_ChunkLength = chunkLength + 1;
				chunkChars[chunkLength] = value;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified 8-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269A RID: 9882 RVA: 0x0008952E File Offset: 0x0008772E
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.AppendSpanFormattable<sbyte>(value);
		}

		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269B RID: 9883 RVA: 0x00089537 File Offset: 0x00087737
		public StringBuilder Append(byte value)
		{
			return this.AppendSpanFormattable<byte>(value);
		}

		/// <summary>Appends the string representation of a specified 16-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269C RID: 9884 RVA: 0x00089540 File Offset: 0x00087740
		public StringBuilder Append(short value)
		{
			return this.AppendSpanFormattable<short>(value);
		}

		/// <summary>Appends the string representation of a specified 32-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269D RID: 9885 RVA: 0x00089549 File Offset: 0x00087749
		public StringBuilder Append(int value)
		{
			return this.AppendSpanFormattable<int>(value);
		}

		/// <summary>Appends the string representation of a specified 64-bit signed integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269E RID: 9886 RVA: 0x00089552 File Offset: 0x00087752
		public StringBuilder Append(long value)
		{
			return this.AppendSpanFormattable<long>(value);
		}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x0600269F RID: 9887 RVA: 0x0008955B File Offset: 0x0008775B
		public StringBuilder Append(float value)
		{
			return this.AppendSpanFormattable<float>(value);
		}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A0 RID: 9888 RVA: 0x00089564 File Offset: 0x00087764
		public StringBuilder Append(double value)
		{
			return this.AppendSpanFormattable<double>(value);
		}

		/// <summary>Appends the string representation of a specified decimal number to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A1 RID: 9889 RVA: 0x0008956D File Offset: 0x0008776D
		public StringBuilder Append(decimal value)
		{
			return this.AppendSpanFormattable<decimal>(value);
		}

		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A2 RID: 9890 RVA: 0x00089576 File Offset: 0x00087776
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.AppendSpanFormattable<ushort>(value);
		}

		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A3 RID: 9891 RVA: 0x0008957F File Offset: 0x0008777F
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.AppendSpanFormattable<uint>(value);
		}

		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to this instance.</summary>
		/// <param name="value">The value to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A4 RID: 9892 RVA: 0x00089588 File Offset: 0x00087788
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.AppendSpanFormattable<ulong>(value);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00089591 File Offset: 0x00087791
		private StringBuilder AppendSpanFormattable<T>(T value) where T : IFormattable
		{
			return this.Append(value.ToString(null, CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified object to this instance.</summary>
		/// <param name="value">The object to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A6 RID: 9894 RVA: 0x000895AC File Offset: 0x000877AC
		public StringBuilder Append(object value)
		{
			if (value != null)
			{
				return this.Append(value.ToString());
			}
			return this;
		}

		/// <summary>Appends the string representation of the Unicode characters in a specified array to this instance.</summary>
		/// <param name="value">The array of characters to append.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026A7 RID: 9895 RVA: 0x000895C0 File Offset: 0x000877C0
		public unsafe StringBuilder Append(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* value2 = ptr;
					this.Append(value2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000895F0 File Offset: 0x000877F0
		public unsafe StringBuilder Append(ReadOnlySpan<char> value)
		{
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference;
					this.Append(value2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00089624 File Offset: 0x00087824
		public unsafe StringBuilder AppendJoin(string separator, params object[] values)
		{
			separator = (separator ?? string.Empty);
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<object>(ptr, separator.Length, values);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0008965C File Offset: 0x0008785C
		public unsafe StringBuilder AppendJoin<T>(string separator, IEnumerable<T> values)
		{
			separator = (separator ?? string.Empty);
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<T>(ptr, separator.Length, values);
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00089694 File Offset: 0x00087894
		public unsafe StringBuilder AppendJoin(string separator, params string[] values)
		{
			separator = (separator ?? string.Empty);
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<string>(ptr, separator.Length, values);
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000896CB File Offset: 0x000878CB
		public unsafe StringBuilder AppendJoin(char separator, params object[] values)
		{
			return this.AppendJoinCore<object>(&separator, 1, values);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000896D8 File Offset: 0x000878D8
		public unsafe StringBuilder AppendJoin<T>(char separator, IEnumerable<T> values)
		{
			return this.AppendJoinCore<T>(&separator, 1, values);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000896E5 File Offset: 0x000878E5
		public unsafe StringBuilder AppendJoin(char separator, params string[] values)
		{
			return this.AppendJoinCore<string>(&separator, 1, values);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000896F4 File Offset: 0x000878F4
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, IEnumerable<T> values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					return this;
				}
				T t = enumerator.Current;
				if (t != null)
				{
					this.Append(t.ToString());
				}
				while (enumerator.MoveNext())
				{
					this.Append(separator, separatorLength);
					t = enumerator.Current;
					if (t != null)
					{
						this.Append(t.ToString());
					}
				}
			}
			return this;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00089798 File Offset: 0x00087998
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, T[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			if (values.Length == 0)
			{
				return this;
			}
			if (values[0] != null)
			{
				this.Append(values[0].ToString());
			}
			for (int i = 1; i < values.Length; i++)
			{
				this.Append(separator, separatorLength);
				if (values[i] != null)
				{
					this.Append(values[i].ToString());
				}
			}
			return this;
		}

		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The string to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.  
		/// -or-  
		/// The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B1 RID: 9905 RVA: 0x00089820 File Offset: 0x00087A20
		public unsafe StringBuilder Insert(int index, string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value != null)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts the string representation of a Boolean value into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B2 RID: 9906 RVA: 0x0008986A File Offset: 0x00087A6A
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B3 RID: 9907 RVA: 0x0008987B File Offset: 0x00087A7B
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B4 RID: 9908 RVA: 0x0008988C File Offset: 0x00087A8C
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B5 RID: 9909 RVA: 0x0008989D File Offset: 0x00087A9D
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B6 RID: 9910 RVA: 0x000898AE File Offset: 0x00087AAE
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		/// <summary>Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The character array to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B7 RID: 9911 RVA: 0x000898BC File Offset: 0x00087ABC
		public StringBuilder Insert(int index, char[] value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value != null)
			{
				this.Insert(index, value, 0, value.Length);
			}
			return this;
		}

		/// <summary>Inserts the string representation of a specified subarray of Unicode characters into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">A character array.</param>
		/// <param name="startIndex">The starting index within <paramref name="value" />.</param>
		/// <param name="charCount">The number of characters to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />, <paramref name="startIndex" />, or <paramref name="charCount" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of this instance.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="charCount" /> is not a position within <paramref name="value" />.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B8 RID: 9912 RVA: 0x000898E8 File Offset: 0x00087AE8
		public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
		{
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
				}
				if (charCount < 0)
				{
					throw new ArgumentOutOfRangeException("charCount", "Value must be positive.");
				}
				if (startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (charCount > 0)
				{
					fixed (char* ptr = &value[startIndex])
					{
						char* value2 = ptr;
						this.Insert(index, value2, charCount);
					}
				}
				return this;
			}
		}

		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026B9 RID: 9913 RVA: 0x0008998C File Offset: 0x00087B8C
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BA RID: 9914 RVA: 0x0008999D File Offset: 0x00087B9D
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a single-precision floating point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BB RID: 9915 RVA: 0x000899AE File Offset: 0x00087BAE
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BC RID: 9916 RVA: 0x000899BF File Offset: 0x00087BBF
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a decimal number into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BD RID: 9917 RVA: 0x000899D0 File Offset: 0x00087BD0
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BE RID: 9918 RVA: 0x000899E1 File Offset: 0x00087BE1
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026BF RID: 9919 RVA: 0x000899F2 File Offset: 0x00087BF2
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The value to insert.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C0 RID: 9920 RVA: 0x00089A03 File Offset: 0x00087C03
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of an object into this instance at the specified character position.</summary>
		/// <param name="index">The position in this instance where insertion begins.</param>
		/// <param name="value">The object to insert, or <see langword="null" />.</param>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C1 RID: 9921 RVA: 0x00089A14 File Offset: 0x00087C14
		public StringBuilder Insert(int index, object value)
		{
			if (value != null)
			{
				return this.Insert(index, value.ToString(), 1);
			}
			return this;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00089A2C File Offset: 0x00087C2C
		public unsafe StringBuilder Insert(int index, ReadOnlySpan<char> value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference;
					this.Insert(index, value2, value.Length);
				}
			}
			return this;
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">An object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C3 RID: 9923 RVA: 0x00089A79 File Offset: 0x00087C79
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 2.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C4 RID: 9924 RVA: 0x00089A89 File Offset: 0x00087C89
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <param name="arg2">The third object to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C5 RID: 9925 RVA: 0x00089A9A File Offset: 0x00087C9A
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An array of objects to format.</param>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C6 RID: 9926 RVA: 0x00089AAD File Offset: 0x00087CAD
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> in which any format specification is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to one (1).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C7 RID: 9927 RVA: 0x00089AD5 File Offset: 0x00087CD5
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 2 (two).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C8 RID: 9928 RVA: 0x00089AE5 File Offset: 0x00087CE5
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format.</param>
		/// <param name="arg1">The second object to format.</param>
		/// <param name="arg2">The third object to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to 3 (three).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026C9 RID: 9929 RVA: 0x00089AF7 File Offset: 0x00087CF7
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array using a specified format provider.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An array of objects to format.</param>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026CA RID: 9930 RVA: 0x00089B0B File Offset: 0x00087D0B
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00089B33 File Offset: 0x00087D33
		private static void FormatError()
		{
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00089B40 File Offset: 0x00087D40
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			StringBuilder stringBuilder = null;
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_91;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_91:
				if (num == length)
				{
					return this;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				string text = null;
				ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
				if (c == ':')
				{
					num++;
					int num4 = num;
					for (;;)
					{
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						num++;
						if (c == '}' || c == '{')
						{
							if (c == '{')
							{
								if (num < length && format[num] == '{')
								{
									num++;
								}
								else
								{
									StringBuilder.FormatError();
								}
							}
							else
							{
								if (num >= length || format[num] != '}')
								{
									break;
								}
								num++;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.Append(format, num4, num - num4 - 1);
							num4 = num;
						}
					}
					num--;
					if (stringBuilder == null || stringBuilder.Length == 0)
					{
						if (num4 != num)
						{
							readOnlySpan = format.AsSpan(num4, num - num4);
						}
					}
					else
					{
						stringBuilder.Append(format, num4, num - num4);
						readOnlySpan = (text = stringBuilder.ToString());
						stringBuilder.Clear();
					}
				}
				if (c != '}')
				{
					StringBuilder.FormatError();
				}
				num++;
				string text2 = null;
				if (customFormatter != null)
				{
					if (readOnlySpan.Length != 0 && text == null)
					{
						text = new string(readOnlySpan);
					}
					text2 = customFormatter.Format(text, obj, provider);
				}
				if (text2 == null)
				{
					ISpanFormattable spanFormattable = obj as ISpanFormattable;
					int num5;
					if (spanFormattable != null && (flag || num3 == 0) && spanFormattable.TryFormat(this.RemainingCurrentChunk, out num5, readOnlySpan, provider))
					{
						this.m_ChunkLength += num5;
						int num6 = num3 - num5;
						if (flag && num6 > 0)
						{
							this.Append(' ', num6);
							continue;
						}
						continue;
					}
					else
					{
						IFormattable formattable = obj as IFormattable;
						if (formattable != null)
						{
							if (readOnlySpan.Length != 0 && text == null)
							{
								text = new string(readOnlySpan);
							}
							text2 = formattable.ToString(text, provider);
						}
						else if (obj != null)
						{
							text2 = obj.ToString();
						}
					}
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				int num7 = num3 - text2.Length;
				if (!flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
				this.Append(text2);
				if (flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
			}
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <param name="oldValue">The string to replace.</param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or <see langword="null" />.</param>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026CD RID: 9933 RVA: 0x00089F28 File Offset: 0x00088128
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="sb">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance and <paramref name="sb" /> have equal string, <see cref="P:System.Text.StringBuilder.Capacity" />, and <see cref="P:System.Text.StringBuilder.MaxCapacity" /> values; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026CE RID: 9934 RVA: 0x00089F3C File Offset: 0x0008813C
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Capacity != sb.Capacity || this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_49:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_7F:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_8;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_49;
					}
				}
				goto IL_7F;
			}
			Block_8:
			return j < 0;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00089FF0 File Offset: 0x000881F0
		public bool Equals(ReadOnlySpan<char> span)
		{
			if (span.Length != this.Length)
			{
				return false;
			}
			StringBuilder stringBuilder = this;
			int num = 0;
			for (;;)
			{
				int chunkLength = stringBuilder.m_ChunkLength;
				num += chunkLength;
				if (!new ReadOnlySpan<char>(stringBuilder.m_ChunkChars, 0, chunkLength).EqualsOrdinal(span.Slice(span.Length - num, chunkLength)))
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
				if (stringBuilder == null)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.</summary>
		/// <param name="oldValue">The string to replace.</param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or <see langword="null" />.</param>
		/// <param name="startIndex">The position in this instance where the substring begins.</param>
		/// <param name="count">The length of the substring.</param>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> plus <paramref name="count" /> indicates a character position not within this instance.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		// Token: 0x060026D0 RID: 9936 RVA: 0x0008A050 File Offset: 0x00088250
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "oldValue");
			}
			newValue = (newValue ?? string.Empty);
			int length2 = newValue.Length;
			int length3 = oldValue.Length;
			int[] array = null;
			int num = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num2 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num2, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num >= array.Length)
					{
						Array.Resize<int>(ref array, array.Length * 3 / 2 + 4);
					}
					array[num++] = num2;
					num2 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num2++;
					count--;
				}
				if (num2 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num3 = num2 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num, stringBuilder, oldValue.Length, newValue);
					num3 += (newValue.Length - oldValue.Length) * num;
					num = 0;
					stringBuilder = this.FindChunkForIndex(num3);
					num2 = num3 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		/// <summary>Replaces all occurrences of a specified character in this instance with another specified character.</summary>
		/// <param name="oldChar">The character to replace.</param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />.</param>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" />.</returns>
		// Token: 0x060026D1 RID: 9937 RVA: 0x0008A1A8 File Offset: 0x000883A8
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this.Length);
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified character with another specified character.</summary>
		/// <param name="oldChar">The character to replace.</param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />.</param>
		/// <param name="startIndex">The position in this instance where the substring begins.</param>
		/// <param name="count">The length of the substring.</param>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of the value of this instance.  
		/// -or-  
		/// <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.</exception>
		// Token: 0x060026D2 RID: 9938 RVA: 0x0008A1BC File Offset: 0x000883BC
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = startIndex + count;
			StringBuilder stringBuilder = this;
			for (;;)
			{
				int num2 = num - stringBuilder.m_ChunkOffset;
				int num3 = startIndex - stringBuilder.m_ChunkOffset;
				if (num2 >= 0)
				{
					int i = Math.Max(num3, 0);
					int num4 = Math.Min(stringBuilder.m_ChunkLength, num2);
					while (i < num4)
					{
						if (stringBuilder.m_ChunkChars[i] == oldChar)
						{
							stringBuilder.m_ChunkChars[i] = newChar;
						}
						i++;
					}
				}
				if (num3 >= 0)
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return this;
		}

		/// <summary>Appends an array of Unicode characters starting at a specified address to this instance.</summary>
		/// <param name="value">A pointer to an array of characters.</param>
		/// <param name="valueCount">The number of characters in the array.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="valueCount" /> is less than zero.  
		/// -or-  
		/// Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="value" /> is a null pointer.</exception>
		// Token: 0x060026D3 RID: 9939 RVA: 0x0008A26C File Offset: 0x0008846C
		[CLSCompliant(false)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", "Count cannot be less than zero.");
			}
			int num = this.Length + valueCount;
			if (num > this.m_MaxCapacity || num < valueCount)
			{
				throw new ArgumentOutOfRangeException("valueCount", "The length cannot be greater than the capacity.");
			}
			int num2 = valueCount + this.m_ChunkLength;
			if (num2 <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num2;
			}
			else
			{
				int num3 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num3 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num3);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num4 = valueCount - num3;
				this.ExpandByABlock(num4);
				StringBuilder.ThreadSafeCopy(value + num3, this.m_ChunkChars, 0, num4);
				this.m_ChunkLength = num4;
			}
			return this;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0008A340 File Offset: 0x00088540
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0008A384 File Offset: 0x00088584
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = (value.Length - removeCount) * replacementsCount;
				StringBuilder stringBuilder = sourceChunk;
				int num2 = replacements[0];
				if (num > 0)
				{
					this.MakeRoom(stringBuilder.m_ChunkOffset + num2, num, out stringBuilder, out num2, true);
				}
				int num3 = 0;
				for (;;)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					int num4 = replacements[num3] + removeCount;
					num3++;
					if (num3 >= replacementsCount)
					{
						break;
					}
					int num5 = replacements[num3];
					if (num != 0)
					{
						fixed (char* ptr2 = &sourceChunk.m_ChunkChars[num4])
						{
							char* value2 = ptr2;
							this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, value2, num5 - num4);
						}
					}
					else
					{
						num2 += num5 - num4;
					}
				}
				if (num < 0)
				{
					this.Remove(stringBuilder.m_ChunkOffset + num2, -num, out stringBuilder, out num2);
				}
			}
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0008A458 File Offset: 0x00088658
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0008A4B8 File Offset: 0x000886B8
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int num = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num);
					indexInChunk += num;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num;
					if (count == 0)
					{
						break;
					}
					value += num;
				}
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0008A520 File Offset: 0x00088720
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					string.wstrcpy(ptr, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0008A564 File Offset: 0x00088764
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, Span<char> destination, int destinationIndex, int count)
		{
			if (count > 0)
			{
				if (sourceIndex > source.Length || count > source.Length - sourceIndex)
				{
					throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (destinationIndex > destination.Length || count > destination.Length - destinationIndex)
				{
					throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				fixed (char* ptr = &source[sourceIndex])
				{
					char* smem = ptr;
					fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
					{
						string.wstrcpy(reference + destinationIndex, smem, count);
					}
				}
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0008A5E4 File Offset: 0x000887E4
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0008A608 File Offset: 0x00088808
		private StringBuilder FindChunkForByte(int byteIndex)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0008A62C File Offset: 0x0008882C
		private Span<char> RemainingCurrentChunk
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Span<char>(this.m_ChunkChars, this.m_ChunkLength, this.m_ChunkChars.Length - this.m_ChunkLength);
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0008A64E File Offset: 0x0008884E
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk != this)
			{
				return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
			}
			return null;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0008A66C File Offset: 0x0008886C
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length > this.m_MaxCapacity || minBlockCharCount + this.Length < minBlockCharCount)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			if (this.m_ChunkOffset + num < num)
			{
				this.m_ChunkChars = null;
				throw new OutOfMemoryException();
			}
			this.m_ChunkChars = new char[num];
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0008A708 File Offset: 0x00088908
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0008A758 File Offset: 0x00088958
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doNotMoveFollowingChars)
		{
			if (count + this.Length > this.m_MaxCapacity || count + this.Length < count)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doNotMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				fixed (char* ptr = &chunk.m_ChunkChars[0])
				{
					char* ptr2 = ptr;
					StringBuilder.ThreadSafeCopy(ptr2, stringBuilder.m_ChunkChars, 0, num);
					int num2 = indexInChunk - num;
					if (num2 >= 0)
					{
						StringBuilder.ThreadSafeCopy(ptr2 + num, chunk.m_ChunkChars, 0, num2);
						indexInChunk = num2;
					}
				}
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0008A8BC File Offset: 0x00088ABC
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = new char[size];
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int count2 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, count2);
			}
		}

		// Token: 0x04001DD0 RID: 7632
		internal char[] m_ChunkChars;

		// Token: 0x04001DD1 RID: 7633
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04001DD2 RID: 7634
		internal int m_ChunkLength;

		// Token: 0x04001DD3 RID: 7635
		internal int m_ChunkOffset;

		// Token: 0x04001DD4 RID: 7636
		internal int m_MaxCapacity;

		// Token: 0x04001DD5 RID: 7637
		internal const int DefaultCapacity = 16;

		// Token: 0x04001DD6 RID: 7638
		private const string CapacityField = "Capacity";

		// Token: 0x04001DD7 RID: 7639
		private const string MaxCapacityField = "m_MaxCapacity";

		// Token: 0x04001DD8 RID: 7640
		private const string StringValueField = "m_StringValue";

		// Token: 0x04001DD9 RID: 7641
		private const string ThreadIDField = "m_currentThread";

		// Token: 0x04001DDA RID: 7642
		internal const int MaxChunkSize = 8000;

		// Token: 0x04001DDB RID: 7643
		private const int IndexLimit = 1000000;

		// Token: 0x04001DDC RID: 7644
		private const int WidthLimit = 1000000;
	}
}
