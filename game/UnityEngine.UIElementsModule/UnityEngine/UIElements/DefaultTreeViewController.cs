using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010B RID: 267
	internal sealed class DefaultTreeViewController<T> : TreeViewController
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0001FEBF File Offset: 0x0001E0BF
		public void SetRootItems(IList<TreeViewItemData<T>> items)
		{
			this.m_TreeData = new TreeData<T>(items);
			base.RebuildTree();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public void AddItem(in TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree = true)
		{
			this.m_TreeData.AddItem(item, parentId, childIndex);
			if (rebuildTree)
			{
				base.RebuildTree();
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001FF08 File Offset: 0x0001E108
		public override bool TryRemoveItem(int id, bool rebuildTree = true)
		{
			bool flag = this.m_TreeData.TryRemove(id);
			bool result;
			if (flag)
			{
				if (rebuildTree)
				{
					base.RebuildTree();
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001FF3C File Offset: 0x0001E13C
		public T GetDataForId(int id)
		{
			return this.m_TreeData.GetDataForId(id).data;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001FF64 File Offset: 0x0001E164
		public T GetDataForIndex(int index)
		{
			int idForIndex = this.GetIdForIndex(index);
			return this.GetDataForId(idForIndex);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001FF88 File Offset: 0x0001E188
		public override object GetItemForIndex(int index)
		{
			return this.GetDataForIndex(index);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		public override int GetParentId(int id)
		{
			return this.m_TreeData.GetParentId(id);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		public override bool HasChildren(int id)
		{
			return this.m_TreeData.GetDataForId(id).hasChildren;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001FFEE File Offset: 0x0001E1EE
		private static IEnumerable<int> GetItemIds(IEnumerable<TreeViewItemData<T>> items)
		{
			bool flag = items == null;
			if (flag)
			{
				yield break;
			}
			foreach (TreeViewItemData<T> item in items)
			{
				yield return item.id;
				item = default(TreeViewItemData<T>);
			}
			IEnumerator<TreeViewItemData<T>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00020000 File Offset: 0x0001E200
		public override IEnumerable<int> GetChildrenIds(int id)
		{
			return DefaultTreeViewController<T>.GetItemIds(this.m_TreeData.GetDataForId(id).children);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002002C File Offset: 0x0001E22C
		public override void Move(int id, int newParentId, int childIndex = -1, bool rebuildTree = true)
		{
			bool flag = id == newParentId;
			if (!flag)
			{
				bool flag2 = this.IsChildOf(newParentId, id);
				if (!flag2)
				{
					this.m_TreeData.Move(id, newParentId, childIndex);
					if (rebuildTree)
					{
						base.RebuildTree();
						base.RaiseItemParentChanged(id, newParentId);
					}
				}
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002007C File Offset: 0x0001E27C
		private bool IsChildOf(int childId, int id)
		{
			return this.m_TreeData.GetDataForId(id).HasChildRecursive(childId);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000200AC File Offset: 0x0001E2AC
		public override IEnumerable<int> GetAllItemIds(IEnumerable<int> rootIds = null)
		{
			bool flag = rootIds == null;
			if (flag)
			{
				bool flag2 = this.m_TreeData.rootItemIds == null;
				if (flag2)
				{
					yield break;
				}
				rootIds = this.m_TreeData.rootItemIds;
			}
			IEnumerator<int> currentIterator = rootIds.GetEnumerator();
			for (;;)
			{
				bool hasNext = currentIterator.MoveNext();
				bool flag3 = !hasNext;
				if (flag3)
				{
					bool flag4 = this.m_IteratorStack.Count > 0;
					if (!flag4)
					{
						break;
					}
					currentIterator = this.m_IteratorStack.Pop();
				}
				else
				{
					int currentItemId = currentIterator.Current;
					yield return currentItemId;
					bool flag5 = this.HasChildren(currentItemId);
					if (flag5)
					{
						this.m_IteratorStack.Push(currentIterator);
						currentIterator = this.GetChildrenIds(currentItemId).GetEnumerator();
					}
				}
			}
			yield break;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000200C3 File Offset: 0x0001E2C3
		public DefaultTreeViewController()
		{
		}

		// Token: 0x04000374 RID: 884
		private TreeData<T> m_TreeData;

		// Token: 0x04000375 RID: 885
		private Stack<IEnumerator<int>> m_IteratorStack = new Stack<IEnumerator<int>>();

		// Token: 0x0200010C RID: 268
		[CompilerGenerated]
		private sealed class <GetItemIds>d__10 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
		{
			// Token: 0x06000887 RID: 2183 RVA: 0x000200D7 File Offset: 0x0001E2D7
			[DebuggerHidden]
			public <GetItemIds>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x000200F8 File Offset: 0x0001E2F8
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x00020138 File Offset: 0x0001E338
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						item = default(TreeViewItemData<T>);
					}
					else
					{
						this.<>1__state = -1;
						bool flag = items == null;
						if (flag)
						{
							return false;
						}
						enumerator = items.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						item = enumerator.Current;
						this.<>2__current = item.id;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x00020214 File Offset: 0x0001E414
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600088B RID: 2187 RVA: 0x00020231 File Offset: 0x0001E431
			int IEnumerator<int>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600088D RID: 2189 RVA: 0x00020239 File Offset: 0x0001E439
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x00020248 File Offset: 0x0001E448
			[DebuggerHidden]
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				DefaultTreeViewController<T>.<GetItemIds>d__10 <GetItemIds>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetItemIds>d__ = this;
				}
				else
				{
					<GetItemIds>d__ = new DefaultTreeViewController<T>.<GetItemIds>d__10(0);
				}
				<GetItemIds>d__.items = items;
				return <GetItemIds>d__;
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x00020290 File Offset: 0x0001E490
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();
			}

			// Token: 0x04000376 RID: 886
			private int <>1__state;

			// Token: 0x04000377 RID: 887
			private int <>2__current;

			// Token: 0x04000378 RID: 888
			private int <>l__initialThreadId;

			// Token: 0x04000379 RID: 889
			private IEnumerable<TreeViewItemData<T>> items;

			// Token: 0x0400037A RID: 890
			public IEnumerable<TreeViewItemData<T>> <>3__items;

			// Token: 0x0400037B RID: 891
			private IEnumerator<TreeViewItemData<T>> <>s__1;

			// Token: 0x0400037C RID: 892
			private TreeViewItemData<T> <item>5__2;
		}

		// Token: 0x0200010D RID: 269
		[CompilerGenerated]
		private sealed class <GetAllItemIds>d__14 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
		{
			// Token: 0x06000890 RID: 2192 RVA: 0x00020298 File Offset: 0x0001E498
			[DebuggerHidden]
			public <GetAllItemIds>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x000202B8 File Offset: 0x0001E4B8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					bool flag = this.HasChildren(currentItemId);
					if (flag)
					{
						this.m_IteratorStack.Push(currentIterator);
						currentIterator = this.GetChildrenIds(currentItemId).GetEnumerator();
					}
				}
				else
				{
					this.<>1__state = -1;
					bool flag2 = rootIds == null;
					if (flag2)
					{
						bool flag3 = this.m_TreeData.rootItemIds == null;
						if (flag3)
						{
							return false;
						}
						rootIds = this.m_TreeData.rootItemIds;
					}
					currentIterator = rootIds.GetEnumerator();
				}
				for (;;)
				{
					hasNext = currentIterator.MoveNext();
					bool flag4 = !hasNext;
					if (!flag4)
					{
						goto IL_CE;
					}
					bool flag5 = this.m_IteratorStack.Count > 0;
					if (!flag5)
					{
						break;
					}
					currentIterator = this.m_IteratorStack.Pop();
				}
				return false;
				IL_CE:
				currentItemId = currentIterator.Current;
				this.<>2__current = currentItemId;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x00020416 File Offset: 0x0001E616
			int IEnumerator<int>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000895 RID: 2197 RVA: 0x0002041E File Offset: 0x0001E61E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x0002042C File Offset: 0x0001E62C
			[DebuggerHidden]
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				DefaultTreeViewController<T>.<GetAllItemIds>d__14 <GetAllItemIds>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAllItemIds>d__ = this;
				}
				else
				{
					<GetAllItemIds>d__ = new DefaultTreeViewController<T>.<GetAllItemIds>d__14(0);
					<GetAllItemIds>d__.<>4__this = this;
				}
				<GetAllItemIds>d__.rootIds = rootIds;
				return <GetAllItemIds>d__;
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x00020480 File Offset: 0x0001E680
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();
			}

			// Token: 0x0400037D RID: 893
			private int <>1__state;

			// Token: 0x0400037E RID: 894
			private int <>2__current;

			// Token: 0x0400037F RID: 895
			private int <>l__initialThreadId;

			// Token: 0x04000380 RID: 896
			private IEnumerable<int> rootIds;

			// Token: 0x04000381 RID: 897
			public IEnumerable<int> <>3__rootIds;

			// Token: 0x04000382 RID: 898
			public DefaultTreeViewController<T> <>4__this;

			// Token: 0x04000383 RID: 899
			private IEnumerator<int> <currentIterator>5__1;

			// Token: 0x04000384 RID: 900
			private bool <hasNext>5__2;

			// Token: 0x04000385 RID: 901
			private int <currentItemId>5__3;
		}
	}
}
