using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.CoreFX;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004FB RID: 1275
	internal class CoreFXFileSystemWatcherProxy : IFileWatcher
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x0008F264 File Offset: 0x0008D464
		protected void Operation(Action<IDictionary<object, FileSystemWatcher>, ConditionalWeakTable<object, FileSystemWatcher>, IDictionary<object, object>, object> map_op = null, Action<FileSystemWatcher, FileSystemWatcher> object_op = null, object handle = null, Action<FileSystemWatcher, FileSystemWatcher> cancel_op = null)
		{
			FileSystemWatcher internal_fsw = null;
			FileSystemWatcher fsw = null;
			bool flag2;
			if (cancel_op != null)
			{
				bool flag = Monitor.TryEnter(CoreFXFileSystemWatcherProxy.instance, 300);
				flag2 = (handle != null && (CoreFXFileSystemWatcherProxy.internal_map.TryGetValue(handle, out internal_fsw) || CoreFXFileSystemWatcherProxy.external_map.TryGetValue(handle, out fsw)));
				if (flag2 && flag)
				{
					try
					{
						cancel_op(internal_fsw, fsw);
					}
					catch (Exception)
					{
					}
				}
				if (flag)
				{
					Monitor.Exit(CoreFXFileSystemWatcherProxy.instance);
				}
				if (flag2 && !flag)
				{
					try
					{
						Task.Run<bool>(delegate()
						{
							cancel_op(internal_fsw, fsw);
							return true;
						}).Wait(300);
					}
					catch (Exception)
					{
					}
				}
				return;
			}
			IFileWatcher obj;
			if (map_op != null && handle == null)
			{
				obj = CoreFXFileSystemWatcherProxy.instance;
				lock (obj)
				{
					try
					{
						map_op(CoreFXFileSystemWatcherProxy.internal_map, CoreFXFileSystemWatcherProxy.external_map, CoreFXFileSystemWatcherProxy.event_map, null);
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException("map_op", innerException);
					}
				}
				return;
			}
			if (handle == null)
			{
				return;
			}
			obj = CoreFXFileSystemWatcherProxy.instance;
			lock (obj)
			{
				flag2 = (CoreFXFileSystemWatcherProxy.internal_map.TryGetValue(handle, out internal_fsw) && CoreFXFileSystemWatcherProxy.external_map.TryGetValue(handle, out fsw));
				if (flag2 && map_op != null)
				{
					try
					{
						map_op(CoreFXFileSystemWatcherProxy.internal_map, CoreFXFileSystemWatcherProxy.external_map, CoreFXFileSystemWatcherProxy.event_map, handle);
					}
					catch (Exception innerException2)
					{
						throw new InvalidOperationException("map_op", innerException2);
					}
				}
			}
			if (!flag2 || object_op == null)
			{
				return;
			}
			try
			{
				object_op(internal_fsw, fsw);
			}
			catch (Exception innerException3)
			{
				throw new InvalidOperationException("object_op", innerException3);
			}
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0008F470 File Offset: 0x0008D670
		protected void ProxyDispatch(object sender, FileAction action, FileSystemEventArgs args)
		{
			RenamedEventArgs renamed = (action == FileAction.RenamedNewName) ? ((RenamedEventArgs)args) : null;
			object handle = null;
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object h)
			{
				event_map.TryGetValue(sender, out handle);
			}, null, null, null);
			this.Operation(null, delegate(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				if (!fsw.EnableRaisingEvents)
				{
					return;
				}
				fsw.DispatchEvents(action, args.Name, ref renamed);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					Monitor.PulseAll(fsw);
				}
			}, handle, null);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0008F4EC File Offset: 0x0008D6EC
		protected void ProxyDispatchError(object sender, ErrorEventArgs args)
		{
			object handle = null;
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				event_map.TryGetValue(sender, out handle);
			}, null, null, null);
			this.Operation(null, delegate(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				fsw.DispatchErrorEvents(args);
			}, handle, null);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0008F544 File Offset: 0x0008D744
		public object NewWatcher(FileSystemWatcher fsw)
		{
			object handle = new object();
			FileSystemWatcher result = new FileSystemWatcher();
			result.Changed += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate()
				{
					this.ProxyDispatch(o, FileAction.Modified, args);
				});
			};
			result.Created += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate()
				{
					this.ProxyDispatch(o, FileAction.Added, args);
				});
			};
			result.Deleted += delegate(object o, FileSystemEventArgs args)
			{
				Task.Run(delegate()
				{
					this.ProxyDispatch(o, FileAction.Removed, args);
				});
			};
			result.Renamed += delegate(object o, RenamedEventArgs args)
			{
				Task.Run(delegate()
				{
					this.ProxyDispatch(o, FileAction.RenamedNewName, args);
				});
			};
			result.Error += delegate(object o, ErrorEventArgs args)
			{
				Task.Run(delegate()
				{
					this.ProxyDispatchError(handle, args);
				});
			};
			this.Operation(delegate(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				in_map.Add(handle, result);
				out_map.Add(handle, fsw);
				event_map.Add(result, handle);
			}, null, null, null);
			return handle;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0008F609 File Offset: 0x0008D809
		public void StartDispatching(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				internal_fsw.Path = fsw.Path;
				internal_fsw.Filter = fsw.Filter;
				internal_fsw.IncludeSubdirectories = fsw.IncludeSubdirectories;
				internal_fsw.InternalBufferSize = fsw.InternalBufferSize;
				internal_fsw.NotifyFilter = fsw.NotifyFilter;
				internal_fsw.Site = fsw.Site;
				internal_fsw.EnableRaisingEvents = true;
			}, handle, null);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0008F637 File Offset: 0x0008D837
		public void StopDispatching(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, null, handle, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.EnableRaisingEvents = false;
				}
			});
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0008F668 File Offset: 0x0008D868
		public void Dispose(object handle)
		{
			if (handle == null)
			{
				return;
			}
			this.Operation(null, null, handle, delegate(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.Dispose();
				}
				FileSystemWatcher key = CoreFXFileSystemWatcherProxy.internal_map[handle];
				CoreFXFileSystemWatcherProxy.internal_map.Remove(handle);
				CoreFXFileSystemWatcherProxy.external_map.Remove(handle);
				CoreFXFileSystemWatcherProxy.event_map.Remove(key);
				handle = null;
			});
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0008F6A8 File Offset: 0x0008D8A8
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (CoreFXFileSystemWatcherProxy.instance != null)
			{
				watcher = CoreFXFileSystemWatcherProxy.instance;
				return true;
			}
			CoreFXFileSystemWatcherProxy.internal_map = new ConcurrentDictionary<object, FileSystemWatcher>();
			CoreFXFileSystemWatcherProxy.external_map = new ConditionalWeakTable<object, FileSystemWatcher>();
			CoreFXFileSystemWatcherProxy.event_map = new ConcurrentDictionary<object, object>();
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new CoreFXFileSystemWatcherProxy());
			CoreFXFileSystemWatcherProxy.instance = fileWatcher;
			return true;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0000219B File Offset: 0x0000039B
		public CoreFXFileSystemWatcherProxy()
		{
		}

		// Token: 0x040015FF RID: 5631
		private static IFileWatcher instance;

		// Token: 0x04001600 RID: 5632
		private static IDictionary<object, FileSystemWatcher> internal_map;

		// Token: 0x04001601 RID: 5633
		private static ConditionalWeakTable<object, FileSystemWatcher> external_map;

		// Token: 0x04001602 RID: 5634
		private static IDictionary<object, object> event_map;

		// Token: 0x04001603 RID: 5635
		private const int INTERRUPT_MS = 300;

		// Token: 0x020004FC RID: 1276
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060029BD RID: 10685 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060029BE RID: 10686 RVA: 0x0008F6F3 File Offset: 0x0008D8F3
			internal bool <Operation>b__0()
			{
				this.cancel_op(this.internal_fsw, this.fsw);
				return true;
			}

			// Token: 0x04001604 RID: 5636
			public Action<FileSystemWatcher, FileSystemWatcher> cancel_op;

			// Token: 0x04001605 RID: 5637
			public FileSystemWatcher internal_fsw;

			// Token: 0x04001606 RID: 5638
			public FileSystemWatcher fsw;
		}

		// Token: 0x020004FD RID: 1277
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x060029BF RID: 10687 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x060029C0 RID: 10688 RVA: 0x0008F70D File Offset: 0x0008D90D
			internal void <ProxyDispatch>b__0(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object h)
			{
				event_map.TryGetValue(this.sender, out this.handle);
			}

			// Token: 0x060029C1 RID: 10689 RVA: 0x0008F722 File Offset: 0x0008D922
			internal void <ProxyDispatch>b__1(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				if (!fsw.EnableRaisingEvents)
				{
					return;
				}
				fsw.DispatchEvents(this.action, this.args.Name, ref this.renamed);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					Monitor.PulseAll(fsw);
				}
			}

			// Token: 0x04001607 RID: 5639
			public object sender;

			// Token: 0x04001608 RID: 5640
			public object handle;

			// Token: 0x04001609 RID: 5641
			public FileAction action;

			// Token: 0x0400160A RID: 5642
			public FileSystemEventArgs args;

			// Token: 0x0400160B RID: 5643
			public RenamedEventArgs renamed;
		}

		// Token: 0x020004FE RID: 1278
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060029C2 RID: 10690 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060029C3 RID: 10691 RVA: 0x0008F75F File Offset: 0x0008D95F
			internal void <ProxyDispatchError>b__0(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				event_map.TryGetValue(this.sender, out this.handle);
			}

			// Token: 0x060029C4 RID: 10692 RVA: 0x0008F774 File Offset: 0x0008D974
			internal void <ProxyDispatchError>b__1(FileSystemWatcher _, FileSystemWatcher fsw)
			{
				fsw.DispatchErrorEvents(this.args);
			}

			// Token: 0x0400160C RID: 5644
			public object sender;

			// Token: 0x0400160D RID: 5645
			public object handle;

			// Token: 0x0400160E RID: 5646
			public ErrorEventArgs args;
		}

		// Token: 0x020004FF RID: 1279
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x060029C5 RID: 10693 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060029C6 RID: 10694 RVA: 0x0008F782 File Offset: 0x0008D982
			internal void <NewWatcher>b__0(object o, FileSystemEventArgs args)
			{
				Task.Run(new Action(new CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_1
				{
					CS$<>8__locals1 = this,
					o = o,
					args = args
				}.<NewWatcher>b__6));
			}

			// Token: 0x060029C7 RID: 10695 RVA: 0x0008F7AF File Offset: 0x0008D9AF
			internal void <NewWatcher>b__1(object o, FileSystemEventArgs args)
			{
				Task.Run(new Action(new CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_2
				{
					CS$<>8__locals2 = this,
					o = o,
					args = args
				}.<NewWatcher>b__7));
			}

			// Token: 0x060029C8 RID: 10696 RVA: 0x0008F7DC File Offset: 0x0008D9DC
			internal void <NewWatcher>b__2(object o, FileSystemEventArgs args)
			{
				Task.Run(new Action(new CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_3
				{
					CS$<>8__locals3 = this,
					o = o,
					args = args
				}.<NewWatcher>b__8));
			}

			// Token: 0x060029C9 RID: 10697 RVA: 0x0008F809 File Offset: 0x0008DA09
			internal void <NewWatcher>b__3(object o, RenamedEventArgs args)
			{
				Task.Run(new Action(new CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_4
				{
					CS$<>8__locals4 = this,
					o = o,
					args = args
				}.<NewWatcher>b__9));
			}

			// Token: 0x060029CA RID: 10698 RVA: 0x0008F836 File Offset: 0x0008DA36
			internal void <NewWatcher>b__4(object o, ErrorEventArgs args)
			{
				Task.Run(new Action(new CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_5
				{
					CS$<>8__locals5 = this,
					args = args
				}.<NewWatcher>b__10));
			}

			// Token: 0x060029CB RID: 10699 RVA: 0x0008F85C File Offset: 0x0008DA5C
			internal void <NewWatcher>b__5(IDictionary<object, FileSystemWatcher> in_map, ConditionalWeakTable<object, FileSystemWatcher> out_map, IDictionary<object, object> event_map, object _)
			{
				in_map.Add(this.handle, this.result);
				out_map.Add(this.handle, this.fsw);
				event_map.Add(this.result, this.handle);
			}

			// Token: 0x0400160F RID: 5647
			public CoreFXFileSystemWatcherProxy <>4__this;

			// Token: 0x04001610 RID: 5648
			public object handle;

			// Token: 0x04001611 RID: 5649
			public FileSystemWatcher result;

			// Token: 0x04001612 RID: 5650
			public FileSystemWatcher fsw;
		}

		// Token: 0x02000500 RID: 1280
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_1
		{
			// Token: 0x060029CC RID: 10700 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_1()
			{
			}

			// Token: 0x060029CD RID: 10701 RVA: 0x0008F894 File Offset: 0x0008DA94
			internal void <NewWatcher>b__6()
			{
				this.CS$<>8__locals1.<>4__this.ProxyDispatch(this.o, FileAction.Modified, this.args);
			}

			// Token: 0x04001613 RID: 5651
			public object o;

			// Token: 0x04001614 RID: 5652
			public FileSystemEventArgs args;

			// Token: 0x04001615 RID: 5653
			public CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_0 CS$<>8__locals1;
		}

		// Token: 0x02000501 RID: 1281
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_2
		{
			// Token: 0x060029CE RID: 10702 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_2()
			{
			}

			// Token: 0x060029CF RID: 10703 RVA: 0x0008F8B3 File Offset: 0x0008DAB3
			internal void <NewWatcher>b__7()
			{
				this.CS$<>8__locals2.<>4__this.ProxyDispatch(this.o, FileAction.Added, this.args);
			}

			// Token: 0x04001616 RID: 5654
			public object o;

			// Token: 0x04001617 RID: 5655
			public FileSystemEventArgs args;

			// Token: 0x04001618 RID: 5656
			public CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_0 CS$<>8__locals2;
		}

		// Token: 0x02000502 RID: 1282
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_3
		{
			// Token: 0x060029D0 RID: 10704 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_3()
			{
			}

			// Token: 0x060029D1 RID: 10705 RVA: 0x0008F8D2 File Offset: 0x0008DAD2
			internal void <NewWatcher>b__8()
			{
				this.CS$<>8__locals3.<>4__this.ProxyDispatch(this.o, FileAction.Removed, this.args);
			}

			// Token: 0x04001619 RID: 5657
			public object o;

			// Token: 0x0400161A RID: 5658
			public FileSystemEventArgs args;

			// Token: 0x0400161B RID: 5659
			public CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_0 CS$<>8__locals3;
		}

		// Token: 0x02000503 RID: 1283
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_4
		{
			// Token: 0x060029D2 RID: 10706 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_4()
			{
			}

			// Token: 0x060029D3 RID: 10707 RVA: 0x0008F8F1 File Offset: 0x0008DAF1
			internal void <NewWatcher>b__9()
			{
				this.CS$<>8__locals4.<>4__this.ProxyDispatch(this.o, FileAction.RenamedNewName, this.args);
			}

			// Token: 0x0400161C RID: 5660
			public object o;

			// Token: 0x0400161D RID: 5661
			public RenamedEventArgs args;

			// Token: 0x0400161E RID: 5662
			public CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_0 CS$<>8__locals4;
		}

		// Token: 0x02000504 RID: 1284
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_5
		{
			// Token: 0x060029D4 RID: 10708 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass8_5()
			{
			}

			// Token: 0x060029D5 RID: 10709 RVA: 0x0008F910 File Offset: 0x0008DB10
			internal void <NewWatcher>b__10()
			{
				this.CS$<>8__locals5.<>4__this.ProxyDispatchError(this.CS$<>8__locals5.handle, this.args);
			}

			// Token: 0x0400161F RID: 5663
			public ErrorEventArgs args;

			// Token: 0x04001620 RID: 5664
			public CoreFXFileSystemWatcherProxy.<>c__DisplayClass8_0 CS$<>8__locals5;
		}

		// Token: 0x02000505 RID: 1285
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060029D6 RID: 10710 RVA: 0x0008F933 File Offset: 0x0008DB33
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060029D7 RID: 10711 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x060029D8 RID: 10712 RVA: 0x0008F940 File Offset: 0x0008DB40
			internal void <StartDispatching>b__9_0(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				internal_fsw.Path = fsw.Path;
				internal_fsw.Filter = fsw.Filter;
				internal_fsw.IncludeSubdirectories = fsw.IncludeSubdirectories;
				internal_fsw.InternalBufferSize = fsw.InternalBufferSize;
				internal_fsw.NotifyFilter = fsw.NotifyFilter;
				internal_fsw.Site = fsw.Site;
				internal_fsw.EnableRaisingEvents = true;
			}

			// Token: 0x060029D9 RID: 10713 RVA: 0x0008F99C File Offset: 0x0008DB9C
			internal void <StopDispatching>b__10_0(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.EnableRaisingEvents = false;
				}
			}

			// Token: 0x04001621 RID: 5665
			public static readonly CoreFXFileSystemWatcherProxy.<>c <>9 = new CoreFXFileSystemWatcherProxy.<>c();

			// Token: 0x04001622 RID: 5666
			public static Action<FileSystemWatcher, FileSystemWatcher> <>9__9_0;

			// Token: 0x04001623 RID: 5667
			public static Action<FileSystemWatcher, FileSystemWatcher> <>9__10_0;
		}

		// Token: 0x02000506 RID: 1286
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x060029DA RID: 10714 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060029DB RID: 10715 RVA: 0x0008F9A8 File Offset: 0x0008DBA8
			internal void <Dispose>b__0(FileSystemWatcher internal_fsw, FileSystemWatcher fsw)
			{
				if (internal_fsw != null)
				{
					internal_fsw.Dispose();
				}
				FileSystemWatcher key = CoreFXFileSystemWatcherProxy.internal_map[this.handle];
				CoreFXFileSystemWatcherProxy.internal_map.Remove(this.handle);
				CoreFXFileSystemWatcherProxy.external_map.Remove(this.handle);
				CoreFXFileSystemWatcherProxy.event_map.Remove(key);
				this.handle = null;
			}

			// Token: 0x04001624 RID: 5668
			public object handle;
		}
	}
}
