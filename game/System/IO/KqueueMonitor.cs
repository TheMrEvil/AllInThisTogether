using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000520 RID: 1312
	internal class KqueueMonitor : IDisposable
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x00091947 File Offset: 0x0008FB47
		public int Connection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00091950 File Offset: 0x0008FB50
		public KqueueMonitor(FileSystemWatcher fsw)
		{
			this.fsw = fsw;
			this.conn = -1;
			if (!KqueueMonitor.initialized)
			{
				KqueueMonitor.initialized = true;
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_DARWIN_WATCHER_MAXFDS");
				int num;
				if (environmentVariable != null && int.TryParse(environmentVariable, out num))
				{
					this.maxFds = num;
				}
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000919E0 File Offset: 0x0008FBE0
		public void Dispose()
		{
			this.CleanUp();
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000919E8 File Offset: 0x0008FBE8
		public void Start()
		{
			object obj = this.stateLock;
			lock (obj)
			{
				if (!this.started)
				{
					this.conn = KqueueMonitor.kqueue();
					if (this.conn == -1)
					{
						throw new IOException(string.Format("kqueue() error at init, error code = '{0}'", Marshal.GetLastWin32Error()));
					}
					this.thread = new Thread(delegate()
					{
						this.DoMonitor();
					});
					this.thread.IsBackground = true;
					this.thread.Start();
					this.startedEvent.WaitOne();
					if (this.exc != null)
					{
						this.thread.Join();
						this.CleanUp();
						throw this.exc;
					}
					this.started = true;
				}
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00091AC0 File Offset: 0x0008FCC0
		public void Stop()
		{
			object obj = this.stateLock;
			lock (obj)
			{
				if (this.started)
				{
					this.requestStop = true;
					if (!this.inDispatch)
					{
						object obj2 = this.connLock;
						lock (obj2)
						{
							if (this.conn != -1)
							{
								KqueueMonitor.close(this.conn);
							}
							this.conn = -1;
							goto IL_78;
						}
						IL_6D:
						this.thread.Interrupt();
						IL_78:
						if (!this.thread.Join(2000))
						{
							goto IL_6D;
						}
						this.requestStop = false;
						this.started = false;
						if (this.exc != null)
						{
							throw this.exc;
						}
					}
				}
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00091BA0 File Offset: 0x0008FDA0
		private void CleanUp()
		{
			object obj = this.connLock;
			lock (obj)
			{
				if (this.conn != -1)
				{
					KqueueMonitor.close(this.conn);
				}
				this.conn = -1;
			}
			foreach (int fd in this.fdsDict.Keys)
			{
				KqueueMonitor.close(fd);
			}
			this.fdsDict.Clear();
			this.pathsDict.Clear();
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00091C54 File Offset: 0x0008FE54
		private void DoMonitor()
		{
			try
			{
				this.Setup();
			}
			catch (Exception ex)
			{
				this.exc = ex;
			}
			finally
			{
				this.startedEvent.Set();
			}
			if (this.exc != null)
			{
				this.fsw.DispatchErrorEvents(new ErrorEventArgs(this.exc));
				return;
			}
			try
			{
				this.Monitor();
			}
			catch (Exception ex2)
			{
				this.exc = ex2;
			}
			finally
			{
				this.CleanUp();
				if (!this.requestStop)
				{
					this.started = false;
					this.inDispatch = false;
					this.fsw.EnableRaisingEvents = false;
				}
				if (this.exc != null)
				{
					this.fsw.DispatchErrorEvents(new ErrorEventArgs(this.exc));
				}
				this.requestStop = false;
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x00091D38 File Offset: 0x0008FF38
		private void Setup()
		{
			List<int> list = new List<int>();
			if (this.fsw.FullPath != "/" && this.fsw.FullPath.EndsWith("/", StringComparison.Ordinal))
			{
				this.fullPathNoLastSlash = this.fsw.FullPath.Substring(0, this.fsw.FullPath.Length - 1);
			}
			else
			{
				this.fullPathNoLastSlash = this.fsw.FullPath;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (KqueueMonitor.realpath(this.fsw.FullPath, stringBuilder) == IntPtr.Zero)
			{
				throw new IOException(string.Format("realpath({0}) failed, error code = '{1}'", this.fsw.FullPath, Marshal.GetLastWin32Error()));
			}
			string a = stringBuilder.ToString();
			if (a != this.fullPathNoLastSlash)
			{
				this.fixupPath = a;
			}
			else
			{
				this.fixupPath = null;
			}
			this.Scan(this.fullPathNoLastSlash, false, ref list);
			timespec timespec = new timespec
			{
				tv_sec = (IntPtr)0,
				tv_nsec = (IntPtr)0
			};
			kevent[] array = new kevent[0];
			kevent[] array2 = this.CreateChangeList(ref list);
			int num = 0;
			int num2;
			do
			{
				num2 = KqueueMonitor.kevent(this.conn, array2, array2.Length, array, array.Length, ref timespec);
				if (num2 == -1)
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			while (num2 == -1 && num == 4);
			if (num2 == -1)
			{
				throw new IOException(string.Format("kevent() error at initial event registration, error code = '{0}'", num));
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00091EC0 File Offset: 0x000900C0
		private kevent[] CreateChangeList(ref List<int> FdList)
		{
			if (FdList.Count == 0)
			{
				return KqueueMonitor.emptyEventList;
			}
			List<kevent> list = new List<kevent>();
			foreach (int num in FdList)
			{
				kevent item = new kevent
				{
					ident = (UIntPtr)((ulong)((long)num)),
					filter = EventFilter.Vnode,
					flags = (EventFlags.Add | EventFlags.Enable | EventFlags.Clear),
					fflags = (FilterFlags.ReadLowWaterMark | FilterFlags.VNodeWrite | FilterFlags.VNodeExtend | FilterFlags.VNodeAttrib | FilterFlags.VNodeLink | FilterFlags.VNodeRename | FilterFlags.VNodeRevoke),
					data = IntPtr.Zero,
					udata = IntPtr.Zero
				};
				list.Add(item);
			}
			FdList.Clear();
			return list.ToArray();
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00091F80 File Offset: 0x00090180
		private void Monitor()
		{
			kevent[] array = new kevent[32];
			List<int> newFds = new List<int>();
			List<PathData> list = new List<PathData>();
			List<string> list2 = new List<string>();
			int num = 0;
			Action<string> <>9__0;
			while (!this.requestStop)
			{
				kevent[] array2 = this.CreateChangeList(ref newFds);
				int num2 = Marshal.SizeOf<kevent>();
				IntPtr intPtr = Marshal.AllocHGlobal(num2 * array2.Length);
				for (int i = 0; i < array2.Length; i++)
				{
					Marshal.StructureToPtr<kevent>(array2[i], intPtr + i * num2, false);
				}
				IntPtr intPtr2 = Marshal.AllocHGlobal(num2 * array.Length);
				int num3 = KqueueMonitor.kevent_notimeout(ref this.conn, intPtr, array2.Length, intPtr2, array.Length);
				Marshal.FreeHGlobal(intPtr);
				for (int j = 0; j < num3; j++)
				{
					array[j] = Marshal.PtrToStructure<kevent>(intPtr2 + j * num2);
				}
				Marshal.FreeHGlobal(intPtr2);
				if (num3 == -1)
				{
					if (this.requestStop)
					{
						break;
					}
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 4 && ++num == 3)
					{
						throw new IOException(string.Format("persistent kevent() error, error code = '{0}'", lastWin32Error));
					}
				}
				else
				{
					num = 0;
					for (int k = 0; k < num3; k++)
					{
						kevent kevent = array[k];
						if (this.fdsDict.ContainsKey((int)((uint)kevent.ident)))
						{
							PathData pathData = this.fdsDict[(int)((uint)kevent.ident)];
							if ((kevent.flags & EventFlags.Error) == EventFlags.Error)
							{
								string message = string.Format("kevent() error watching path '{0}', error code = '{1}'", pathData.Path, kevent.data);
								this.fsw.DispatchErrorEvents(new ErrorEventArgs(new IOException(message)));
							}
							else if ((kevent.fflags & FilterFlags.ReadLowWaterMark) == FilterFlags.ReadLowWaterMark || (kevent.fflags & FilterFlags.VNodeRevoke) == FilterFlags.VNodeRevoke)
							{
								if (pathData.Path == this.fullPathNoLastSlash)
								{
									return;
								}
								list.Add(pathData);
							}
							else
							{
								if ((kevent.fflags & FilterFlags.VNodeRename) == FilterFlags.VNodeRename)
								{
									this.UpdatePath(pathData);
								}
								if ((kevent.fflags & FilterFlags.VNodeWrite) == FilterFlags.VNodeWrite)
								{
									if (pathData.IsDirectory)
									{
										list2.Add(pathData.Path);
									}
									else
									{
										this.PostEvent(FileAction.Modified, pathData.Path, null);
									}
								}
								if ((kevent.fflags & FilterFlags.VNodeAttrib) == FilterFlags.VNodeAttrib || (kevent.fflags & FilterFlags.VNodeExtend) == FilterFlags.VNodeExtend)
								{
									this.PostEvent(FileAction.Modified, pathData.Path, null);
								}
							}
						}
					}
					list.ForEach(new Action<PathData>(this.Remove));
					list.Clear();
					List<string> list3 = list2;
					Action<string> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(string path)
						{
							this.Scan(path, true, ref newFds);
						});
					}
					list3.ForEach(action);
					list2.Clear();
				}
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00092258 File Offset: 0x00090458
		private PathData Add(string path, bool postEvents, ref List<int> fds)
		{
			PathData pathData;
			this.pathsDict.TryGetValue(path, out pathData);
			if (pathData != null)
			{
				return pathData;
			}
			if (this.fdsDict.Count >= this.maxFds)
			{
				throw new IOException("kqueue() FileSystemWatcher has reached the maximum number of files to watch.");
			}
			int num = KqueueMonitor.open(path, 32768, 0);
			if (num == -1)
			{
				this.fsw.DispatchErrorEvents(new ErrorEventArgs(new IOException(string.Format("open() error while attempting to process path '{0}', error code = '{1}'", path, Marshal.GetLastWin32Error()))));
				return null;
			}
			PathData result;
			try
			{
				fds.Add(num);
				FileAttributes attributes = File.GetAttributes(path);
				pathData = new PathData
				{
					Path = path,
					Fd = num,
					IsDirectory = ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
				};
				this.pathsDict.Add(path, pathData);
				this.fdsDict.Add(num, pathData);
				if (postEvents)
				{
					this.PostEvent(FileAction.Added, path, null);
				}
				result = pathData;
			}
			catch (Exception exception)
			{
				KqueueMonitor.close(num);
				this.fsw.DispatchErrorEvents(new ErrorEventArgs(exception));
				result = null;
			}
			return result;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00092360 File Offset: 0x00090560
		private void Remove(PathData pathData)
		{
			this.fdsDict.Remove(pathData.Fd);
			this.pathsDict.Remove(pathData.Path);
			KqueueMonitor.close(pathData.Fd);
			this.PostEvent(FileAction.Removed, pathData.Path, null);
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000923A0 File Offset: 0x000905A0
		private void RemoveTree(PathData pathData)
		{
			List<PathData> list = new List<PathData>();
			list.Add(pathData);
			if (pathData.IsDirectory)
			{
				string value = pathData.Path + Path.DirectorySeparatorChar.ToString();
				foreach (string text in this.pathsDict.Keys)
				{
					if (text.StartsWith(value))
					{
						list.Add(this.pathsDict[text]);
					}
				}
			}
			list.ForEach(new Action<PathData>(this.Remove));
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0009244C File Offset: 0x0009064C
		private void UpdatePath(PathData pathData)
		{
			string filenameFromFd = this.GetFilenameFromFd(pathData.Fd);
			if (!filenameFromFd.StartsWith(this.fullPathNoLastSlash))
			{
				this.RemoveTree(pathData);
				return;
			}
			List<PathData> list = new List<PathData>();
			string path = pathData.Path;
			list.Add(pathData);
			if (pathData.IsDirectory)
			{
				string value = path + Path.DirectorySeparatorChar.ToString();
				foreach (string text in this.pathsDict.Keys)
				{
					if (text.StartsWith(value))
					{
						list.Add(this.pathsDict[text]);
					}
				}
			}
			foreach (PathData pathData2 in list)
			{
				string path2 = pathData2.Path;
				string text2 = filenameFromFd + path2.Substring(path.Length);
				pathData2.Path = text2;
				this.pathsDict.Remove(path2);
				if (this.pathsDict.ContainsKey(text2))
				{
					PathData pathData3 = this.pathsDict[text2];
					if (this.GetFilenameFromFd(pathData2.Fd) == this.GetFilenameFromFd(pathData3.Fd))
					{
						this.Remove(pathData3);
					}
					else
					{
						this.UpdatePath(pathData3);
					}
				}
				this.pathsDict.Add(text2, pathData2);
			}
			this.PostEvent(FileAction.RenamedNewName, path, filenameFromFd);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000925E8 File Offset: 0x000907E8
		private void Scan(string path, bool postEvents, ref List<int> fds)
		{
			if (this.requestStop)
			{
				return;
			}
			PathData pathData = this.Add(path, postEvents, ref fds);
			if (pathData == null)
			{
				return;
			}
			if (!pathData.IsDirectory)
			{
				return;
			}
			List<string> list = new List<string>();
			list.Add(path);
			while (list.Count > 0)
			{
				string path2 = list[0];
				list.RemoveAt(0);
				DirectoryInfo directoryInfo = new DirectoryInfo(path2);
				FileSystemInfo[] array = null;
				try
				{
					array = directoryInfo.GetFileSystemInfos();
				}
				catch (IOException)
				{
					array = new FileSystemInfo[0];
				}
				foreach (FileSystemInfo fileSystemInfo in array)
				{
					if (((fileSystemInfo.Attributes & FileAttributes.Directory) != FileAttributes.Directory || this.fsw.IncludeSubdirectories) && ((fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory || this.fsw.Pattern.IsMatch(fileSystemInfo.FullName)))
					{
						PathData pathData2 = this.Add(fileSystemInfo.FullName, postEvents, ref fds);
						if (pathData2 != null && pathData2.IsDirectory)
						{
							list.Add(fileSystemInfo.FullName);
						}
					}
				}
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00092704 File Offset: 0x00090904
		private void PostEvent(FileAction action, string path, string newPath = null)
		{
			RenamedEventArgs renamedEventArgs = null;
			if (this.requestStop || action == (FileAction)0)
			{
				return;
			}
			string text = (path.Length > this.fullPathNoLastSlash.Length) ? path.Substring(this.fullPathNoLastSlash.Length + 1) : string.Empty;
			if (!this.fsw.Pattern.IsMatch(path) && (newPath == null || !this.fsw.Pattern.IsMatch(newPath)))
			{
				return;
			}
			if (action == FileAction.RenamedNewName)
			{
				string name = (newPath.Length > this.fullPathNoLastSlash.Length) ? newPath.Substring(this.fullPathNoLastSlash.Length + 1) : string.Empty;
				renamedEventArgs = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.fsw.Path, name, text);
			}
			this.fsw.DispatchEvents(action, text, ref renamedEventArgs);
			if (this.fsw.Waiting)
			{
				FileSystemWatcher obj = this.fsw;
				lock (obj)
				{
					this.fsw.Waiting = false;
					System.Threading.Monitor.PulseAll(this.fsw);
				}
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00092824 File Offset: 0x00090A24
		private string GetFilenameFromFd(int fd)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (KqueueMonitor.fcntl(fd, 50, stringBuilder) != -1)
			{
				if (this.fixupPath != null)
				{
					stringBuilder.Replace(this.fixupPath, this.fullPathNoLastSlash, 0, this.fixupPath.Length);
				}
				return stringBuilder.ToString();
			}
			this.fsw.DispatchErrorEvents(new ErrorEventArgs(new IOException(string.Format("fcntl() error while attempting to get path for fd '{0}', error code = '{1}'", fd, Marshal.GetLastWin32Error()))));
			return string.Empty;
		}

		// Token: 0x06002A67 RID: 10855
		[DllImport("libc", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int fcntl(int file_names_by_descriptor, int cmd, StringBuilder sb);

		// Token: 0x06002A68 RID: 10856
		[DllImport("libc", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr realpath(string pathname, StringBuilder sb);

		// Token: 0x06002A69 RID: 10857
		[DllImport("libc", SetLastError = true)]
		private static extern int open(string path, int flags, int mode_t);

		// Token: 0x06002A6A RID: 10858
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x06002A6B RID: 10859
		[DllImport("libc", SetLastError = true)]
		private static extern int kqueue();

		// Token: 0x06002A6C RID: 10860
		[DllImport("libc", SetLastError = true)]
		private static extern int kevent(int kq, [In] kevent[] ev, int nchanges, [Out] kevent[] evtlist, int nevents, [In] ref timespec time);

		// Token: 0x06002A6D RID: 10861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int kevent_notimeout(ref int kq, IntPtr ev, int nchanges, IntPtr evtlist, int nevents);

		// Token: 0x06002A6E RID: 10862 RVA: 0x000928AA File Offset: 0x00090AAA
		// Note: this type is marked as 'beforefieldinit'.
		static KqueueMonitor()
		{
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000928B7 File Offset: 0x00090AB7
		[CompilerGenerated]
		private void <Start>b__5_0()
		{
			this.DoMonitor();
		}

		// Token: 0x040016CA RID: 5834
		private static bool initialized;

		// Token: 0x040016CB RID: 5835
		private const int O_EVTONLY = 32768;

		// Token: 0x040016CC RID: 5836
		private const int F_GETPATH = 50;

		// Token: 0x040016CD RID: 5837
		private const int __DARWIN_MAXPATHLEN = 1024;

		// Token: 0x040016CE RID: 5838
		private const int EINTR = 4;

		// Token: 0x040016CF RID: 5839
		private static readonly kevent[] emptyEventList = new kevent[0];

		// Token: 0x040016D0 RID: 5840
		private int maxFds = int.MaxValue;

		// Token: 0x040016D1 RID: 5841
		private FileSystemWatcher fsw;

		// Token: 0x040016D2 RID: 5842
		private int conn;

		// Token: 0x040016D3 RID: 5843
		private Thread thread;

		// Token: 0x040016D4 RID: 5844
		private volatile bool requestStop;

		// Token: 0x040016D5 RID: 5845
		private AutoResetEvent startedEvent = new AutoResetEvent(false);

		// Token: 0x040016D6 RID: 5846
		private bool started;

		// Token: 0x040016D7 RID: 5847
		private bool inDispatch;

		// Token: 0x040016D8 RID: 5848
		private Exception exc;

		// Token: 0x040016D9 RID: 5849
		private object stateLock = new object();

		// Token: 0x040016DA RID: 5850
		private object connLock = new object();

		// Token: 0x040016DB RID: 5851
		private readonly Dictionary<string, PathData> pathsDict = new Dictionary<string, PathData>();

		// Token: 0x040016DC RID: 5852
		private readonly Dictionary<int, PathData> fdsDict = new Dictionary<int, PathData>();

		// Token: 0x040016DD RID: 5853
		private string fixupPath;

		// Token: 0x040016DE RID: 5854
		private string fullPathNoLastSlash;

		// Token: 0x02000521 RID: 1313
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06002A70 RID: 10864 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06002A71 RID: 10865 RVA: 0x000928BF File Offset: 0x00090ABF
			internal void <Monitor>b__0(string path)
			{
				this.<>4__this.Scan(path, true, ref this.newFds);
			}

			// Token: 0x040016DF RID: 5855
			public KqueueMonitor <>4__this;

			// Token: 0x040016E0 RID: 5856
			public List<int> newFds;

			// Token: 0x040016E1 RID: 5857
			public Action<string> <>9__0;
		}
	}
}
