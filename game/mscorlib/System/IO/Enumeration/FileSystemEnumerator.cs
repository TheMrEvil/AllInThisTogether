using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x02000B86 RID: 2950
	public abstract class FileSystemEnumerator<TResult> : CriticalFinalizerObject, IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06006B3C RID: 27452 RVA: 0x0016E830 File Offset: 0x0016CA30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool GetData()
		{
			Interop.NtDll.IO_STATUS_BLOCK io_STATUS_BLOCK;
			int num = Interop.NtDll.NtQueryDirectoryFile(this._directoryHandle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, out io_STATUS_BLOCK, this._buffer, (uint)this._bufferLength, Interop.NtDll.FILE_INFORMATION_CLASS.FileFullDirectoryInformation, Interop.BOOLEAN.FALSE, null, Interop.BOOLEAN.FALSE);
			uint num2 = (uint)num;
			if (num2 == 0U)
			{
				return true;
			}
			if (num2 == 2147483654U)
			{
				this.DirectoryFinished();
				return false;
			}
			int num3 = (int)Interop.NtDll.RtlNtStatusToDosError(num);
			if ((num3 == 5 && this._options.IgnoreInaccessible) || this.ContinueOnError(num3))
			{
				this.DirectoryFinished();
				return false;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num3, this._currentPath);
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x0016E8B8 File Offset: 0x0016CAB8
		private IntPtr CreateRelativeDirectoryHandle(ReadOnlySpan<char> relativePath, string fullPath)
		{
			ValueTuple<int, IntPtr> valueTuple = Interop.NtDll.CreateFile(relativePath, this._directoryHandle, Interop.NtDll.CreateDisposition.FILE_OPEN, Interop.NtDll.DesiredAccess.FILE_READ_DATA | Interop.NtDll.DesiredAccess.SYNCHRONIZE, FileShare.Read | FileShare.Write | FileShare.Delete, (FileAttributes)0, (Interop.NtDll.CreateOptions)16417U, Interop.NtDll.ObjectAttributes.OBJ_CASE_INSENSITIVE);
			int item = valueTuple.Item1;
			IntPtr item2 = valueTuple.Item2;
			if (item == 0)
			{
				return item2;
			}
			int num = (int)Interop.NtDll.RtlNtStatusToDosError(item);
			if (this.ContinueOnDirectoryError(num, true))
			{
				return IntPtr.Zero;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x0016E910 File Offset: 0x0016CB10
		public FileSystemEnumerator(string directory, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._originalRootDirectory = directory;
			this._rootDirectory = PathInternal.TrimEndingDirectorySeparator(Path.GetFullPath(directory));
			this._options = (options ?? EnumerationOptions.Default);
			using (default(DisableMediaInsertionPrompt))
			{
				this._directoryHandle = this.CreateDirectoryHandle(this._rootDirectory, false);
				if (this._directoryHandle == IntPtr.Zero)
				{
					this._lastEntryFound = true;
				}
			}
			this._currentPath = this._rootDirectory;
			int bufferSize = this._options.BufferSize;
			this._bufferLength = ((bufferSize <= 0) ? 4096 : Math.Max(1024, bufferSize));
			try
			{
				this._buffer = Marshal.AllocHGlobal(this._bufferLength);
			}
			catch
			{
				this.CloseDirectoryHandle();
				throw;
			}
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x0016EA18 File Offset: 0x0016CC18
		private void CloseDirectoryHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this._directoryHandle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				Interop.Kernel32.CloseHandle(intPtr);
			}
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x0016EA4C File Offset: 0x0016CC4C
		private IntPtr CreateDirectoryHandle(string path, bool ignoreNotFound = false)
		{
			IntPtr intPtr = Interop.Kernel32.CreateFile_IntPtr(path, 1, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, 33554432);
			if (!(intPtr == IntPtr.Zero) && !(intPtr == (IntPtr)(-1)))
			{
				return intPtr;
			}
			int num = Marshal.GetLastWin32Error();
			if (this.ContinueOnDirectoryError(num, ignoreNotFound))
			{
				return IntPtr.Zero;
			}
			if (num == 2)
			{
				num = 3;
			}
			throw Win32Marshal.GetExceptionForWin32Error(num, path);
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x0016EAA8 File Offset: 0x0016CCA8
		private bool ContinueOnDirectoryError(int error, bool ignoreNotFound)
		{
			return (ignoreNotFound && (error == 2 || error == 3 || error == 267)) || (error == 5 && this._options.IgnoreInaccessible) || this.ContinueOnError(error);
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x0016EAD8 File Offset: 0x0016CCD8
		public unsafe bool MoveNext()
		{
			if (this._lastEntryFound)
			{
				return false;
			}
			FileSystemEntry fileSystemEntry = default(FileSystemEntry);
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				if (this._lastEntryFound)
				{
					result = false;
				}
				else
				{
					for (;;)
					{
						this.FindNextEntry();
						if (this._lastEntryFound)
						{
							break;
						}
						FileSystemEntry.Initialize(ref fileSystemEntry, this._entry, this._currentPath, this._rootDirectory, this._originalRootDirectory);
						if ((this._entry->FileAttributes & this._options.AttributesToSkip) == (FileAttributes)0)
						{
							if ((this._entry->FileAttributes & FileAttributes.Directory) != (FileAttributes)0)
							{
								if (this._entry->FileName.Length <= 2 && *this._entry->FileName[0] == 46 && (this._entry->FileName.Length != 2 || *this._entry->FileName[1] == 46))
								{
									if (!this._options.ReturnSpecialDirectories)
									{
										continue;
									}
								}
								else if (this._options.RecurseSubdirectories && this.ShouldRecurseIntoEntry(ref fileSystemEntry))
								{
									string text = Path.Join(this._currentPath, this._entry->FileName);
									IntPtr intPtr = this.CreateRelativeDirectoryHandle(this._entry->FileName, text);
									if (intPtr != IntPtr.Zero)
									{
										try
										{
											if (this._pending == null)
											{
												this._pending = new Queue<ValueTuple<IntPtr, string>>();
											}
											this._pending.Enqueue(new ValueTuple<IntPtr, string>(intPtr, text));
										}
										catch
										{
											Interop.Kernel32.CloseHandle(intPtr);
											throw;
										}
									}
								}
							}
							if (this.ShouldIncludeEntry(ref fileSystemEntry))
							{
								goto Block_15;
							}
						}
					}
					return false;
					Block_15:
					this._current = this.TransformEntry(ref fileSystemEntry);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x0016ECE8 File Offset: 0x0016CEE8
		private unsafe void FindNextEntry()
		{
			this._entry = Interop.NtDll.FILE_FULL_DIR_INFORMATION.GetNextInfo(this._entry);
			if (this._entry != null)
			{
				return;
			}
			if (this.GetData())
			{
				this._entry = (Interop.NtDll.FILE_FULL_DIR_INFORMATION*)((void*)this._buffer);
			}
		}

		// Token: 0x06006B44 RID: 27460 RVA: 0x0016ED20 File Offset: 0x0016CF20
		private bool DequeueNextDirectory()
		{
			if (this._pending == null || this._pending.Count == 0)
			{
				return false;
			}
			ValueTuple<IntPtr, string> valueTuple = this._pending.Dequeue();
			this._directoryHandle = valueTuple.Item1;
			this._currentPath = valueTuple.Item2;
			return true;
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x0016ED6C File Offset: 0x0016CF6C
		private void InternalDispose(bool disposing)
		{
			if (this._lock != null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._lastEntryFound = true;
					this.CloseDirectoryHandle();
					if (this._pending != null)
					{
						while (this._pending.Count > 0)
						{
							Interop.Kernel32.CloseHandle(this._pending.Dequeue().Item1);
						}
						this._pending = null;
					}
					if (this._buffer != (IntPtr)0)
					{
						Marshal.FreeHGlobal(this._buffer);
					}
					this._buffer = 0;
				}
			}
			this.Dispose(disposing);
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x000040F7 File Offset: 0x000022F7
		protected virtual bool ShouldIncludeEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x000040F7 File Offset: 0x000022F7
		protected virtual bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06006B48 RID: 27464
		protected abstract TResult TransformEntry(ref FileSystemEntry entry);

		// Token: 0x06006B49 RID: 27465 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void OnDirectoryFinished(ReadOnlySpan<char> directory)
		{
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected virtual bool ContinueOnError(int error)
		{
			return false;
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06006B4B RID: 27467 RVA: 0x0016EE20 File Offset: 0x0016D020
		public TResult Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06006B4C RID: 27468 RVA: 0x0016EE28 File Offset: 0x0016D028
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x0016EE35 File Offset: 0x0016D035
		private unsafe void DirectoryFinished()
		{
			this._entry = default(Interop.NtDll.FILE_FULL_DIR_INFORMATION*);
			this.CloseDirectoryHandle();
			this.OnDirectoryFinished(this._currentPath);
			if (!this.DequeueNextDirectory())
			{
				this._lastEntryFound = true;
				return;
			}
			this.FindNextEntry();
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x000472C8 File Offset: 0x000454C8
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x0016EE70 File Offset: 0x0016D070
		public void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x0016EE80 File Offset: 0x0016D080
		~FileSystemEnumerator()
		{
			this.InternalDispose(false);
		}

		// Token: 0x04003DC7 RID: 15815
		private const int StandardBufferSize = 4096;

		// Token: 0x04003DC8 RID: 15816
		private const int MinimumBufferSize = 1024;

		// Token: 0x04003DC9 RID: 15817
		private readonly string _originalRootDirectory;

		// Token: 0x04003DCA RID: 15818
		private readonly string _rootDirectory;

		// Token: 0x04003DCB RID: 15819
		private readonly EnumerationOptions _options;

		// Token: 0x04003DCC RID: 15820
		private readonly object _lock = new object();

		// Token: 0x04003DCD RID: 15821
		private unsafe Interop.NtDll.FILE_FULL_DIR_INFORMATION* _entry;

		// Token: 0x04003DCE RID: 15822
		private TResult _current;

		// Token: 0x04003DCF RID: 15823
		private IntPtr _buffer;

		// Token: 0x04003DD0 RID: 15824
		private int _bufferLength;

		// Token: 0x04003DD1 RID: 15825
		private IntPtr _directoryHandle;

		// Token: 0x04003DD2 RID: 15826
		private string _currentPath;

		// Token: 0x04003DD3 RID: 15827
		private bool _lastEntryFound;

		// Token: 0x04003DD4 RID: 15828
		[TupleElementNames(new string[]
		{
			"Handle",
			"Path"
		})]
		private Queue<ValueTuple<IntPtr, string>> _pending;
	}
}
