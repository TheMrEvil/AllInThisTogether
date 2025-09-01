using System;
using System.Threading;

namespace System.Collections
{
	/// <summary>Manages a compact array of bit values, which are represented as Booleans, where <see langword="true" /> indicates that the bit is on (1) and <see langword="false" /> indicates the bit is off (0).</summary>
	// Token: 0x02000A36 RID: 2614
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to <see langword="false" />.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06005CDA RID: 23770 RVA: 0x0013811B File Offset: 0x0013631B
		public BitArray(int length) : this(length, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to the specified value.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />.</param>
		/// <param name="defaultValue">The Boolean value to assign to each bit.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06005CDB RID: 23771 RVA: 0x00138128 File Offset: 0x00136328
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Non-negative number required.");
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = defaultValue ? -1 : 0;
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of bytes.</summary>
		/// <param name="bytes">An array of bytes containing the values to copy, where each byte represents eight consecutive bits.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="bytes" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06005CDC RID: 23772 RVA: 0x0013819C File Offset: 0x0013639C
		public BitArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length > 268435455)
			{
				throw new ArgumentException(SR.Format("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", 8), "bytes");
			}
			this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
			this.m_length = bytes.Length * 8;
			int num = 0;
			int num2 = 0;
			while (bytes.Length - num2 >= 4)
			{
				this.m_array[num++] = ((int)(bytes[num2] & byte.MaxValue) | (int)(bytes[num2 + 1] & byte.MaxValue) << 8 | (int)(bytes[num2 + 2] & byte.MaxValue) << 16 | (int)(bytes[num2 + 3] & byte.MaxValue) << 24);
				num2 += 4;
			}
			switch (bytes.Length - num2)
			{
			case 1:
				goto IL_FA;
			case 2:
				break;
			case 3:
				this.m_array[num] = (int)(bytes[num2 + 2] & byte.MaxValue) << 16;
				break;
			default:
				goto IL_113;
			}
			this.m_array[num] |= (int)(bytes[num2 + 1] & byte.MaxValue) << 8;
			IL_FA:
			this.m_array[num] |= (int)(bytes[num2] & byte.MaxValue);
			IL_113:
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of Booleans.</summary>
		/// <param name="values">An array of Booleans to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		// Token: 0x06005CDD RID: 23773 RVA: 0x001382C4 File Offset: 0x001364C4
		public BitArray(bool[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
			this.m_length = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i])
				{
					this.m_array[i / 32] |= 1 << i % 32;
				}
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of 32-bit integers.</summary>
		/// <param name="values">An array of integers containing the values to copy, where each integer represents 32 consecutive bits.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="values" /> is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x06005CDE RID: 23774 RVA: 0x0013833C File Offset: 0x0013653C
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(SR.Format("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", 32), "values");
			}
			this.m_array = new int[values.Length];
			Array.Copy(values, 0, this.m_array, 0, values.Length);
			this.m_length = values.Length * 32;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="bits">The <see cref="T:System.Collections.BitArray" /> to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is <see langword="null" />.</exception>
		// Token: 0x06005CDF RID: 23775 RVA: 0x001383B8 File Offset: 0x001365B8
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			Array.Copy(bits.m_array, 0, this.m_array, 0, arrayLength);
			this.m_length = bits.m_length;
			this._version = bits._version;
		}

		/// <summary>Gets or sets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.BitArray.Count" />.</exception>
		// Token: 0x1700102C RID: 4140
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		/// <summary>Gets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />.</exception>
		// Token: 0x06005CE2 RID: 23778 RVA: 0x00138432 File Offset: 0x00136632
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (this.m_array[index / 32] & 1 << index % 32) != 0;
		}

		/// <summary>Sets the bit at a specific position in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="index">The zero-based index of the bit to set.</param>
		/// <param name="value">The Boolean value to assign to the bit.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />.</exception>
		// Token: 0x06005CE3 RID: 23779 RVA: 0x00138470 File Offset: 0x00136670
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		/// <summary>Sets all bits in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="value">The Boolean value to assign to all bits.</param>
		// Token: 0x06005CE4 RID: 23780 RVA: 0x001384EC File Offset: 0x001366EC
		public void SetAll(bool value)
		{
			int num = value ? -1 : 0;
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		/// <summary>Performs the bitwise AND operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object and the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise AND operation.</summary>
		/// <param name="value">The array with which to perform the bitwise AND operation.</param>
		/// <returns>An array containing the result of the bitwise AND operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x06005CE5 RID: 23781 RVA: 0x00138534 File Offset: 0x00136734
		public BitArray And(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] &= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise OR operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object and the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise OR operation.</summary>
		/// <param name="value">The array with which to perform the bitwise OR operation.</param>
		/// <returns>An array containing the result of the bitwise OR operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x06005CE6 RID: 23782 RVA: 0x001385AC File Offset: 0x001367AC
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise exclusive OR operation between the elements of the current <see cref="T:System.Collections.BitArray" /> object against the corresponding elements in the specified array. The current <see cref="T:System.Collections.BitArray" /> object will be modified to store the result of the bitwise exclusive OR operation.</summary>
		/// <param name="value">The array with which to perform the bitwise exclusive OR operation.</param>
		/// <returns>An array containing the result of the bitwise exclusive OR operation, which is a reference to the current <see cref="T:System.Collections.BitArray" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements.</exception>
		// Token: 0x06005CE7 RID: 23783 RVA: 0x00138624 File Offset: 0x00136824
		public BitArray Xor(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] ^= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Inverts all the bit values in the current <see cref="T:System.Collections.BitArray" />, so that elements set to <see langword="true" /> are changed to <see langword="false" />, and elements set to <see langword="false" /> are changed to <see langword="true" />.</summary>
		/// <returns>The current instance with inverted bit values.</returns>
		// Token: 0x06005CE8 RID: 23784 RVA: 0x0013869C File Offset: 0x0013689C
		public BitArray Not()
		{
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = ~this.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x001386E4 File Offset: 0x001368E4
		public BitArray RightShift(int count)
		{
			if (count > 0)
			{
				int num = 0;
				int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
				if (count < this.m_length)
				{
					int i = count / 32;
					int num2 = count - i * 32;
					if (num2 == 0)
					{
						uint num3 = uint.MaxValue >> 32 - this.m_length % 32;
						this.m_array[arrayLength - 1] &= (int)num3;
						Array.Copy(this.m_array, i, this.m_array, 0, arrayLength - i);
						num = arrayLength - i;
					}
					else
					{
						int num4 = arrayLength - 1;
						while (i < num4)
						{
							uint num5 = (uint)this.m_array[i] >> num2;
							int num6 = this.m_array[++i] << (32 - num2 & 31);
							this.m_array[num++] = (num6 | (int)num5);
						}
						uint num7 = uint.MaxValue >> 32 - this.m_length % 32;
						num7 &= (uint)this.m_array[i];
						this.m_array[num++] = (int)(num7 >> num2);
					}
				}
				Array.Clear(this.m_array, num, arrayLength - num);
				this._version++;
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non-negative number required.");
			}
			this._version++;
			return this;
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x00138820 File Offset: 0x00136A20
		public BitArray LeftShift(int count)
		{
			if (count > 0)
			{
				int num2;
				if (count < this.m_length)
				{
					int num = (this.m_length - 1) / 32;
					num2 = count / 32;
					int num3 = count - num2 * 32;
					if (num3 == 0)
					{
						Array.Copy(this.m_array, 0, this.m_array, num2, num + 1 - num2);
					}
					else
					{
						int i = num - num2;
						while (i > 0)
						{
							int num4 = this.m_array[i] << num3;
							uint num5 = (uint)this.m_array[--i] >> (32 - num3 & 31);
							this.m_array[num] = (num4 | (int)num5);
							num--;
						}
						this.m_array[num] = this.m_array[i] << num3;
					}
				}
				else
				{
					num2 = BitArray.GetArrayLength(this.m_length, 32);
				}
				Array.Clear(this.m_array, 0, num2);
				this._version++;
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non-negative number required.");
			}
			this._version++;
			return this;
		}

		/// <summary>Gets or sets the number of elements in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value that is less than zero.</exception>
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06005CEB RID: 23787 RVA: 0x0013891D File Offset: 0x00136B1D
		// (set) Token: 0x06005CEC RID: 23788 RVA: 0x00138928 File Offset: 0x00136B28
		public int Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					Array.Resize<int>(ref this.m_array, arrayLength);
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.BitArray" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.BitArray" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.BitArray" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.BitArray" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005CED RID: 23789 RVA: 0x001389E8 File Offset: 0x00136BE8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			int[] array2 = array as int[];
			if (array2 != null)
			{
				int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
				int num2 = this.m_length % 32;
				if (num2 == 0)
				{
					Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32));
					return;
				}
				Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32) - 1);
				array2[index + num] = (this.m_array[num] & (1 << num2) - 1);
				return;
			}
			else if (array is byte[])
			{
				int num3 = this.m_length % 8;
				int num4 = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < num4)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (num3 > 0)
				{
					num4--;
				}
				byte[] array3 = (byte[])array;
				for (int i = 0; i < num4; i++)
				{
					array3[index + i] = (byte)(this.m_array[i / 4] >> i % 4 * 8 & 255);
				}
				if (num3 > 0)
				{
					int num5 = num4;
					array3[index + num5] = (byte)(this.m_array[num5 / 4] >> num5 % 4 * 8 & (1 << num3) - 1);
					return;
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException("Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].", "array");
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				bool[] array4 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array4[index + j] = ((this.m_array[j / 32] >> j % 32 & 1) != 0);
				}
				return;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x0013891D File Offset: 0x00136B1D
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005CEF RID: 23791 RVA: 0x00138BD0 File Offset: 0x00136DD0
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.BitArray" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always <see langword="false" />.</returns>
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005CF0 RID: 23792 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.BitArray" /> is read-only.</summary>
		/// <returns>This property is always <see langword="false" />.</returns>
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x06005CF2 RID: 23794 RVA: 0x00138BF2 File Offset: 0x00136DF2
		public object Clone()
		{
			return new BitArray(this);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.BitArray" />.</returns>
		// Token: 0x06005CF3 RID: 23795 RVA: 0x00138BFA File Offset: 0x00136DFA
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00138C02 File Offset: 0x00136E02
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x040038C2 RID: 14530
		private int[] m_array;

		// Token: 0x040038C3 RID: 14531
		private int m_length;

		// Token: 0x040038C4 RID: 14532
		private int _version;

		// Token: 0x040038C5 RID: 14533
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040038C6 RID: 14534
		private const int _ShrinkThreshold = 256;

		// Token: 0x040038C7 RID: 14535
		private const int BitsPerInt32 = 32;

		// Token: 0x040038C8 RID: 14536
		private const int BytesPerInt32 = 4;

		// Token: 0x040038C9 RID: 14537
		private const int BitsPerByte = 8;

		// Token: 0x02000A37 RID: 2615
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06005CF5 RID: 23797 RVA: 0x00138C11 File Offset: 0x00136E11
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x06005CF6 RID: 23798 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005CF7 RID: 23799 RVA: 0x00138C34 File Offset: 0x00136E34
			public virtual bool MoveNext()
			{
				ICollection collection = this.bitarray;
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.index < collection.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = collection.Count;
				return false;
			}

			// Token: 0x17001032 RID: 4146
			// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x00138CAA File Offset: 0x00136EAA
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this.index >= ((ICollection)this.bitarray).Count)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this.currentElement;
				}
			}

			// Token: 0x06005CF9 RID: 23801 RVA: 0x00138CE9 File Offset: 0x00136EE9
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.index = -1;
			}

			// Token: 0x040038CA RID: 14538
			private BitArray bitarray;

			// Token: 0x040038CB RID: 14539
			private int index;

			// Token: 0x040038CC RID: 14540
			private int version;

			// Token: 0x040038CD RID: 14541
			private bool currentElement;
		}
	}
}
