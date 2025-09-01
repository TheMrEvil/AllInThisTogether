using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a reader that can read a sequential series of characters.</summary>
	// Token: 0x02000B1E RID: 2846
	[Serializable]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextReader" /> class.</summary>
		// Token: 0x060065B8 RID: 26040 RVA: 0x00053949 File Offset: 0x00051B49
		protected TextReader()
		{
		}

		/// <summary>Closes the <see cref="T:System.IO.TextReader" /> and releases any system resources associated with the <see langword="TextReader" />.</summary>
		// Token: 0x060065B9 RID: 26041 RVA: 0x0015C1DA File Offset: 0x0015A3DA
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextReader" /> object.</summary>
		// Token: 0x060065BA RID: 26042 RVA: 0x0015C1DA File Offset: 0x0015A3DA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060065BB RID: 26043 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the reader.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the reader does not support seeking.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060065BC RID: 26044 RVA: 0x0012275A File Offset: 0x0012095A
		public virtual int Peek()
		{
			return -1;
		}

		/// <summary>Reads the next character from the text reader and advances the character position by one character.</summary>
		/// <returns>The next character from the text reader, or -1 if no more characters are available. The default implementation returns -1.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060065BD RID: 26045 RVA: 0x0012275A File Offset: 0x0012095A
		public virtual int Read()
		{
			return -1;
		}

		/// <summary>Reads a specified maximum number of characters from the current reader and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the reader is reached before the specified number of characters is read into the buffer, the method returns.</param>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether the data is available within the reader. This method returns 0 (zero) if it is called when no more characters are left to read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060065BE RID: 26046 RVA: 0x0015C1EC File Offset: 0x0015A3EC
		public virtual int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.Read();
				if (num == -1)
				{
					break;
				}
				buffer[index + i] = (char)num;
			}
			return i;
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x0015C268 File Offset: 0x0015A468
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("The read operation returned an invalid length.");
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		/// <summary>Reads all characters from the current position to the end of the text reader and returns them as one string.</summary>
		/// <returns>A string that contains all characters from the current position to the end of the text reader.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x060065C0 RID: 26048 RVA: 0x0015C2E4 File Offset: 0x0015A4E4
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int charCount;
			while ((charCount = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, charCount);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> -1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060065C1 RID: 26049 RVA: 0x0015C328 File Offset: 0x0015A528
		public virtual int ReadBlock(char[] buffer, int index, int count)
		{
			int num = 0;
			int num2;
			do
			{
				num += (num2 = this.Read(buffer, index + num, count - num));
			}
			while (num2 > 0 && num < count);
			return num;
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0015C354 File Offset: 0x0015A554
		public virtual int ReadBlock(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int result;
			try
			{
				int num = this.ReadBlock(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("The read operation returned an invalid length.");
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				result = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		/// <summary>Reads a line of characters from the text reader and returns the data as a string.</summary>
		/// <returns>The next line from the reader, or <see langword="null" /> if all characters have been read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x060065C3 RID: 26051 RVA: 0x0015C3D0 File Offset: 0x0015A5D0
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_43;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_43:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		/// <summary>Reads a line of characters asynchronously and returns the data as a string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the text reader, or is <see langword="null" /> if all of the characters have been read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060065C4 RID: 26052 RVA: 0x0015C431 File Offset: 0x0015A631
		public virtual Task<string> ReadLineAsync()
		{
			return Task<string>.Factory.StartNew((object state) => ((TextReader)state).ReadLine(), this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Reads all characters from the current position to the end of the text reader asynchronously and returns them as one string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the text reader.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060065C5 RID: 26053 RVA: 0x0015C468 File Offset: 0x0015A668
		public virtual Task<string> ReadToEndAsync()
		{
			TextReader.<ReadToEndAsync>d__14 <ReadToEndAsync>d__;
			<ReadToEndAsync>d__.<>4__this = this;
			<ReadToEndAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadToEndAsync>d__.<>1__state = -1;
			<ReadToEndAsync>d__.<>t__builder.Start<TextReader.<ReadToEndAsync>d__14>(ref <ReadToEndAsync>d__);
			return <ReadToEndAsync>d__.<>t__builder.Task;
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060065C6 RID: 26054 RVA: 0x0015C4AC File Offset: 0x0015A6AC
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x0015C524 File Offset: 0x0015A724
		public virtual ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.Read(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0015C59C File Offset: 0x0015A79C
		internal virtual ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			Tuple<TextReader, Memory<char>> state2 = new Tuple<TextReader, Memory<char>>(this, buffer);
			return new ValueTask<int>(Task<int>.Factory.StartNew(delegate(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}, state2, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060065C9 RID: 26057 RVA: 0x0015C5E8 File Offset: 0x0015A7E8
		public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadBlockAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x0015C660 File Offset: 0x0015A860
		public virtual ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.ReadBlock(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadBlockAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x0015C6D8 File Offset: 0x0015A8D8
		internal ValueTask<int> ReadBlockAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			TextReader.<ReadBlockAsyncInternal>d__20 <ReadBlockAsyncInternal>d__;
			<ReadBlockAsyncInternal>d__.<>4__this = this;
			<ReadBlockAsyncInternal>d__.buffer = buffer;
			<ReadBlockAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadBlockAsyncInternal>d__.<>t__builder = AsyncValueTaskMethodBuilder<int>.Create();
			<ReadBlockAsyncInternal>d__.<>1__state = -1;
			<ReadBlockAsyncInternal>d__.<>t__builder.Start<TextReader.<ReadBlockAsyncInternal>d__20>(ref <ReadBlockAsyncInternal>d__);
			return <ReadBlockAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Creates a thread-safe wrapper around the specified <see langword="TextReader" />.</summary>
		/// <param name="reader">The <see langword="TextReader" /> to synchronize.</param>
		/// <returns>A thread-safe <see cref="T:System.IO.TextReader" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060065CC RID: 26060 RVA: 0x0015C72B File Offset: 0x0015A92B
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (!(reader is TextReader.SyncTextReader))
			{
				return new TextReader.SyncTextReader(reader);
			}
			return reader;
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x0015C74B File Offset: 0x0015A94B
		// Note: this type is marked as 'beforefieldinit'.
		static TextReader()
		{
		}

		/// <summary>Provides a <see langword="TextReader" /> with no data to read from.</summary>
		// Token: 0x04003BE9 RID: 15337
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x02000B1F RID: 2847
		[Serializable]
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x060065CE RID: 26062 RVA: 0x0015C757 File Offset: 0x0015A957
			public NullTextReader()
			{
			}

			// Token: 0x060065CF RID: 26063 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x060065D0 RID: 26064 RVA: 0x0000AF5E File Offset: 0x0000915E
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x02000B20 RID: 2848
		[Serializable]
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x060065D1 RID: 26065 RVA: 0x0015C75F File Offset: 0x0015A95F
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x060065D2 RID: 26066 RVA: 0x0015C76E File Offset: 0x0015A96E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x060065D3 RID: 26067 RVA: 0x0015C77B File Offset: 0x0015A97B
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x060065D4 RID: 26068 RVA: 0x0015C78B File Offset: 0x0015A98B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x060065D5 RID: 26069 RVA: 0x0015C798 File Offset: 0x0015A998
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x060065D6 RID: 26070 RVA: 0x0015C7A5 File Offset: 0x0015A9A5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read(char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x060065D7 RID: 26071 RVA: 0x0015C7B5 File Offset: 0x0015A9B5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int ReadBlock(char[] buffer, int index, int count)
			{
				return this._in.ReadBlock(buffer, index, count);
			}

			// Token: 0x060065D8 RID: 26072 RVA: 0x0015C7C5 File Offset: 0x0015A9C5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x060065D9 RID: 26073 RVA: 0x0015C7D2 File Offset: 0x0015A9D2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x060065DA RID: 26074 RVA: 0x0015C7DF File Offset: 0x0015A9DF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadLineAsync()
			{
				return Task.FromResult<string>(this.ReadLine());
			}

			// Token: 0x060065DB RID: 26075 RVA: 0x0015C7EC File Offset: 0x0015A9EC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadToEndAsync()
			{
				return Task.FromResult<string>(this.ReadToEnd());
			}

			// Token: 0x060065DC RID: 26076 RVA: 0x0015C7FC File Offset: 0x0015A9FC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer", "Buffer cannot be null.");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
			}

			// Token: 0x060065DD RID: 26077 RVA: 0x0015C860 File Offset: 0x0015AA60
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<int> ReadAsync(char[] buffer, int index, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer", "Buffer cannot be null.");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x04003BEA RID: 15338
			internal readonly TextReader _in;
		}

		// Token: 0x02000B21 RID: 2849
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060065DE RID: 26078 RVA: 0x0015C8C4 File Offset: 0x0015AAC4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060065DF RID: 26079 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x060065E0 RID: 26080 RVA: 0x0015C8D0 File Offset: 0x0015AAD0
			internal string <ReadLineAsync>b__13_0(object state)
			{
				return ((TextReader)state).ReadLine();
			}

			// Token: 0x060065E1 RID: 26081 RVA: 0x0015C8E0 File Offset: 0x0015AAE0
			internal int <ReadAsync>b__16_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}

			// Token: 0x060065E2 RID: 26082 RVA: 0x0015C910 File Offset: 0x0015AB10
			internal int <ReadAsyncInternal>b__17_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}

			// Token: 0x060065E3 RID: 26083 RVA: 0x0015C940 File Offset: 0x0015AB40
			internal int <ReadBlockAsync>b__19_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.ReadBlock(tuple.Item2.Span);
			}

			// Token: 0x04003BEB RID: 15339
			public static readonly TextReader.<>c <>9 = new TextReader.<>c();

			// Token: 0x04003BEC RID: 15340
			public static Func<object, string> <>9__13_0;

			// Token: 0x04003BED RID: 15341
			public static Func<object, int> <>9__16_0;

			// Token: 0x04003BEE RID: 15342
			public static Func<object, int> <>9__17_0;

			// Token: 0x04003BEF RID: 15343
			public static Func<object, int> <>9__19_0;
		}

		// Token: 0x02000B22 RID: 2850
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x060065E4 RID: 26084 RVA: 0x0015C970 File Offset: 0x0015AB70
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				TextReader textReader = this.<>4__this;
				string result2;
				try
				{
					if (num != 0)
					{
						this.<sb>5__2 = new StringBuilder(4096);
						this.<chars>5__3 = ArrayPool<char>.Shared.Rent(4096);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_CA;
						}
						IL_50:
						awaiter = textReader.ReadAsyncInternal(this.<chars>5__3, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, TextReader.<ReadToEndAsync>d__14>(ref awaiter, ref this);
							return;
						}
						IL_CA:
						int result;
						if ((result = awaiter.GetResult()) != 0)
						{
							this.<sb>5__2.Append(this.<chars>5__3, 0, result);
							goto IL_50;
						}
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<char>.Shared.Return(this.<chars>5__3, false);
						}
					}
					result2 = this.<sb>5__2.ToString();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<chars>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<chars>5__3 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060065E5 RID: 26085 RVA: 0x0015CAE0 File Offset: 0x0015ACE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BF0 RID: 15344
			public int <>1__state;

			// Token: 0x04003BF1 RID: 15345
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003BF2 RID: 15346
			public TextReader <>4__this;

			// Token: 0x04003BF3 RID: 15347
			private StringBuilder <sb>5__2;

			// Token: 0x04003BF4 RID: 15348
			private char[] <chars>5__3;

			// Token: 0x04003BF5 RID: 15349
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x02000B23 RID: 2851
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBlockAsyncInternal>d__20 : IAsyncStateMachine
		{
			// Token: 0x060065E6 RID: 26086 RVA: 0x0015CAF0 File Offset: 0x0015ACF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				TextReader textReader = this.<>4__this;
				int result2;
				try
				{
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						this.<>1__state = -1;
						goto IL_94;
					}
					this.<n>5__2 = 0;
					IL_18:
					awaiter = textReader.ReadAsyncInternal(this.buffer.Slice(this.<n>5__2), this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, TextReader.<ReadBlockAsyncInternal>d__20>(ref awaiter, ref this);
						return;
					}
					IL_94:
					int result = awaiter.GetResult();
					this.<n>5__2 += result;
					if (result > 0 && this.<n>5__2 < this.buffer.Length)
					{
						goto IL_18;
					}
					result2 = this.<n>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060065E7 RID: 26087 RVA: 0x0015CC08 File Offset: 0x0015AE08
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003BF6 RID: 15350
			public int <>1__state;

			// Token: 0x04003BF7 RID: 15351
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003BF8 RID: 15352
			public TextReader <>4__this;

			// Token: 0x04003BF9 RID: 15353
			public Memory<char> buffer;

			// Token: 0x04003BFA RID: 15354
			public CancellationToken cancellationToken;

			// Token: 0x04003BFB RID: 15355
			private int <n>5__2;

			// Token: 0x04003BFC RID: 15356
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}
	}
}
