using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000022 RID: 34
	internal sealed class InputQueue<T> : IDisposable where T : class
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004768 File Offset: 0x00002968
		public InputQueue()
		{
			this.itemQueue = new InputQueue<T>.ItemQueue();
			this.readerQueue = new Queue<InputQueue<T>.IQueueReader>();
			this.waiterList = new List<InputQueue<T>.IQueueWaiter>();
			this.queueState = InputQueue<T>.QueueState.Open;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004798 File Offset: 0x00002998
		public InputQueue(Func<Action<AsyncCallback, IAsyncResult>> asyncCallbackGenerator) : this()
		{
			this.AsyncCallbackGenerator = asyncCallbackGenerator;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000047A8 File Offset: 0x000029A8
		public int PendingCount
		{
			get
			{
				object thisLock = this.ThisLock;
				int itemCount;
				lock (thisLock)
				{
					itemCount = this.itemQueue.ItemCount;
				}
				return itemCount;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000047F0 File Offset: 0x000029F0
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000047F8 File Offset: 0x000029F8
		public Action<T> DisposeItemCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<DisposeItemCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisposeItemCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004801 File Offset: 0x00002A01
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004809 File Offset: 0x00002A09
		private Func<Action<AsyncCallback, IAsyncResult>> AsyncCallbackGenerator
		{
			[CompilerGenerated]
			get
			{
				return this.<AsyncCallbackGenerator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AsyncCallbackGenerator>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004812 File Offset: 0x00002A12
		private object ThisLock
		{
			get
			{
				return this.itemQueue;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000481C File Offset: 0x00002A1C
		public IAsyncResult BeginDequeue(TimeSpan timeout, AsyncCallback callback, object state)
		{
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (!this.itemQueue.HasAvailableItem)
					{
						InputQueue<T>.AsyncQueueReader asyncQueueReader = new InputQueue<T>.AsyncQueueReader(this, timeout, callback, state);
						this.readerQueue.Enqueue(asyncQueueReader);
						return asyncQueueReader;
					}
					item = this.itemQueue.DequeueAvailableItem();
				}
				else if (this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else if (this.itemQueue.HasAnyItem)
					{
						InputQueue<T>.AsyncQueueReader asyncQueueReader2 = new InputQueue<T>.AsyncQueueReader(this, timeout, callback, state);
						this.readerQueue.Enqueue(asyncQueueReader2);
						return asyncQueueReader2;
					}
				}
			}
			InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
			return new CompletedAsyncResult<T>(item.GetValue(), callback, state);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004908 File Offset: 0x00002B08
		public IAsyncResult BeginWaitForItem(TimeSpan timeout, AsyncCallback callback, object state)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (!this.itemQueue.HasAvailableItem)
					{
						InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter = new InputQueue<T>.AsyncQueueWaiter(timeout, callback, state);
						this.waiterList.Add(asyncQueueWaiter);
						return asyncQueueWaiter;
					}
				}
				else if (this.queueState == InputQueue<T>.QueueState.Shutdown && !this.itemQueue.HasAvailableItem && this.itemQueue.HasAnyItem)
				{
					InputQueue<T>.AsyncQueueWaiter asyncQueueWaiter2 = new InputQueue<T>.AsyncQueueWaiter(timeout, callback, state);
					this.waiterList.Add(asyncQueueWaiter2);
					return asyncQueueWaiter2;
				}
			}
			return new CompletedAsyncResult<bool>(true, callback, state);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000049BC File Offset: 0x00002BBC
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000049C4 File Offset: 0x00002BC4
		public T Dequeue(TimeSpan timeout)
		{
			T result;
			if (!this.Dequeue(timeout, out result))
			{
				throw Fx.Exception.AsError(new TimeoutException(InternalSR.TimeoutInputQueueDequeue(timeout)));
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000049F8 File Offset: 0x00002BF8
		public bool Dequeue(TimeSpan timeout, out T value)
		{
			InputQueue<T>.WaitQueueReader waitQueueReader = null;
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else
					{
						waitQueueReader = new InputQueue<T>.WaitQueueReader(this);
						this.readerQueue.Enqueue(waitQueueReader);
					}
				}
				else
				{
					if (this.queueState != InputQueue<T>.QueueState.Shutdown)
					{
						value = default(T);
						return true;
					}
					if (this.itemQueue.HasAvailableItem)
					{
						item = this.itemQueue.DequeueAvailableItem();
					}
					else
					{
						if (!this.itemQueue.HasAnyItem)
						{
							value = default(T);
							return true;
						}
						waitQueueReader = new InputQueue<T>.WaitQueueReader(this);
						this.readerQueue.Enqueue(waitQueueReader);
					}
				}
			}
			if (waitQueueReader != null)
			{
				return waitQueueReader.Wait(timeout, out value);
			}
			InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
			value = item.GetValue();
			return true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004AFC File Offset: 0x00002CFC
		public void Dispatch()
		{
			InputQueue<T>.IQueueReader queueReader = null;
			InputQueue<T>.Item item = default(InputQueue<T>.Item);
			InputQueue<T>.IQueueReader[] array = null;
			InputQueue<T>.IQueueWaiter[] array2 = null;
			bool itemAvailable = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				itemAvailable = (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown);
				this.GetWaiters(out array2);
				if (this.queueState != InputQueue<T>.QueueState.Closed)
				{
					this.itemQueue.MakePendingItemAvailable();
					if (this.readerQueue.Count > 0)
					{
						item = this.itemQueue.DequeueAvailableItem();
						queueReader = this.readerQueue.Dequeue();
						if (this.queueState == InputQueue<T>.QueueState.Shutdown && this.readerQueue.Count > 0 && this.itemQueue.ItemCount == 0)
						{
							array = new InputQueue<T>.IQueueReader[this.readerQueue.Count];
							this.readerQueue.CopyTo(array, 0);
							this.readerQueue.Clear();
							itemAvailable = false;
						}
					}
				}
			}
			if (array != null)
			{
				if (InputQueue<T>.completeOutstandingReadersCallback == null)
				{
					InputQueue<T>.completeOutstandingReadersCallback = new Action<object>(InputQueue<T>.CompleteOutstandingReadersCallback);
				}
				ActionItem.Schedule(InputQueue<T>.completeOutstandingReadersCallback, array);
			}
			if (array2 != null)
			{
				InputQueue<T>.CompleteWaitersLater(itemAvailable, array2);
			}
			if (queueReader != null)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				queueReader.Set(item);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004C44 File Offset: 0x00002E44
		public bool EndDequeue(IAsyncResult result, out T value)
		{
			if (result is CompletedAsyncResult<T>)
			{
				value = CompletedAsyncResult<T>.End(result);
				return true;
			}
			return InputQueue<T>.AsyncQueueReader.End(result, out value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004C64 File Offset: 0x00002E64
		public T EndDequeue(IAsyncResult result)
		{
			T result2;
			if (!this.EndDequeue(result, out result2))
			{
				throw Fx.Exception.AsError(new TimeoutException());
			}
			return result2;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C8D File Offset: 0x00002E8D
		public bool EndWaitForItem(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<bool>)
			{
				return CompletedAsyncResult<bool>.End(result);
			}
			return InputQueue<T>.AsyncQueueWaiter.End(result);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public void EnqueueAndDispatch(T item)
		{
			this.EnqueueAndDispatch(item, null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004CAE File Offset: 0x00002EAE
		public void EnqueueAndDispatch(T item, Action dequeuedCallback)
		{
			this.EnqueueAndDispatch(item, dequeuedCallback, true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004CB9 File Offset: 0x00002EB9
		public void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.EnqueueAndDispatch(new InputQueue<T>.Item(exception, dequeuedCallback), canDispatchOnThisThread);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004CC9 File Offset: 0x00002EC9
		public void EnqueueAndDispatch(T item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.EnqueueAndDispatch(new InputQueue<T>.Item(item, dequeuedCallback), canDispatchOnThisThread);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004CD9 File Offset: 0x00002ED9
		public bool EnqueueWithoutDispatch(T item, Action dequeuedCallback)
		{
			return this.EnqueueWithoutDispatch(new InputQueue<T>.Item(item, dequeuedCallback));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public bool EnqueueWithoutDispatch(Exception exception, Action dequeuedCallback)
		{
			return this.EnqueueWithoutDispatch(new InputQueue<T>.Item(exception, dequeuedCallback));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004CF7 File Offset: 0x00002EF7
		public void Shutdown()
		{
			this.Shutdown(null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004D00 File Offset: 0x00002F00
		public void Shutdown(Func<Exception> pendingExceptionGenerator)
		{
			InputQueue<T>.IQueueReader[] array = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					return;
				}
				if (this.queueState == InputQueue<T>.QueueState.Closed)
				{
					return;
				}
				this.queueState = InputQueue<T>.QueueState.Shutdown;
				if (this.readerQueue.Count > 0 && this.itemQueue.ItemCount == 0)
				{
					array = new InputQueue<T>.IQueueReader[this.readerQueue.Count];
					this.readerQueue.CopyTo(array, 0);
					this.readerQueue.Clear();
				}
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Exception exception = (pendingExceptionGenerator != null) ? pendingExceptionGenerator() : null;
					array[i].Set(new InputQueue<T>.Item(exception, null));
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public bool WaitForItem(TimeSpan timeout)
		{
			InputQueue<T>.WaitQueueWaiter waitQueueWaiter = null;
			bool result = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (this.itemQueue.HasAvailableItem)
					{
						result = true;
					}
					else
					{
						waitQueueWaiter = new InputQueue<T>.WaitQueueWaiter();
						this.waiterList.Add(waitQueueWaiter);
					}
				}
				else
				{
					if (this.queueState != InputQueue<T>.QueueState.Shutdown)
					{
						return true;
					}
					if (this.itemQueue.HasAvailableItem)
					{
						result = true;
					}
					else
					{
						if (!this.itemQueue.HasAnyItem)
						{
							return true;
						}
						waitQueueWaiter = new InputQueue<T>.WaitQueueWaiter();
						this.waiterList.Add(waitQueueWaiter);
					}
				}
			}
			if (waitQueueWaiter != null)
			{
				return waitQueueWaiter.Wait(timeout);
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004E94 File Offset: 0x00003094
		public void Dispose()
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState != InputQueue<T>.QueueState.Closed)
				{
					this.queueState = InputQueue<T>.QueueState.Closed;
					flag = true;
				}
			}
			if (flag)
			{
				while (this.readerQueue.Count > 0)
				{
					this.readerQueue.Dequeue().Set(default(InputQueue<T>.Item));
				}
				while (this.itemQueue.HasAnyItem)
				{
					InputQueue<T>.Item item = this.itemQueue.DequeueAnyItem();
					this.DisposeItem(item);
					InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004F40 File Offset: 0x00003140
		private void DisposeItem(InputQueue<T>.Item item)
		{
			T value = item.Value;
			if (value != null)
			{
				if (value is IDisposable)
				{
					((IDisposable)((object)value)).Dispose();
					return;
				}
				Action<T> disposeItemCallback = this.DisposeItemCallback;
				if (disposeItemCallback != null)
				{
					disposeItemCallback(value);
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004F8C File Offset: 0x0000318C
		private static void CompleteOutstandingReadersCallback(object state)
		{
			InputQueue<T>.IQueueReader[] array = (InputQueue<T>.IQueueReader[])state;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Set(default(InputQueue<T>.Item));
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004FC0 File Offset: 0x000031C0
		private static void CompleteWaiters(bool itemAvailable, InputQueue<T>.IQueueWaiter[] waiters)
		{
			for (int i = 0; i < waiters.Length; i++)
			{
				waiters[i].Set(itemAvailable);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004FE4 File Offset: 0x000031E4
		private static void CompleteWaitersFalseCallback(object state)
		{
			InputQueue<T>.CompleteWaiters(false, (InputQueue<T>.IQueueWaiter[])state);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004FF4 File Offset: 0x000031F4
		private static void CompleteWaitersLater(bool itemAvailable, InputQueue<T>.IQueueWaiter[] waiters)
		{
			if (itemAvailable)
			{
				if (InputQueue<T>.completeWaitersTrueCallback == null)
				{
					InputQueue<T>.completeWaitersTrueCallback = new Action<object>(InputQueue<T>.CompleteWaitersTrueCallback);
				}
				ActionItem.Schedule(InputQueue<T>.completeWaitersTrueCallback, waiters);
				return;
			}
			if (InputQueue<T>.completeWaitersFalseCallback == null)
			{
				InputQueue<T>.completeWaitersFalseCallback = new Action<object>(InputQueue<T>.CompleteWaitersFalseCallback);
			}
			ActionItem.Schedule(InputQueue<T>.completeWaitersFalseCallback, waiters);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000504B File Offset: 0x0000324B
		private static void CompleteWaitersTrueCallback(object state)
		{
			InputQueue<T>.CompleteWaiters(true, (InputQueue<T>.IQueueWaiter[])state);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005059 File Offset: 0x00003259
		private static void InvokeDequeuedCallback(Action dequeuedCallback)
		{
			if (dequeuedCallback != null)
			{
				dequeuedCallback();
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005064 File Offset: 0x00003264
		private static void InvokeDequeuedCallbackLater(Action dequeuedCallback)
		{
			if (dequeuedCallback != null)
			{
				if (InputQueue<T>.onInvokeDequeuedCallback == null)
				{
					InputQueue<T>.onInvokeDequeuedCallback = new Action<object>(InputQueue<T>.OnInvokeDequeuedCallback);
				}
				ActionItem.Schedule(InputQueue<T>.onInvokeDequeuedCallback, dequeuedCallback);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000508C File Offset: 0x0000328C
		private static void OnDispatchCallback(object state)
		{
			((InputQueue<T>)state).Dispatch();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005099 File Offset: 0x00003299
		private static void OnInvokeDequeuedCallback(object state)
		{
			((Action)state)();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000050A8 File Offset: 0x000032A8
		private void EnqueueAndDispatch(InputQueue<T>.Item item, bool canDispatchOnThisThread)
		{
			bool flag = false;
			InputQueue<T>.IQueueReader queueReader = null;
			bool flag2 = false;
			InputQueue<T>.IQueueWaiter[] array = null;
			bool itemAvailable = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				itemAvailable = (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown);
				this.GetWaiters(out array);
				if (this.queueState == InputQueue<T>.QueueState.Open)
				{
					if (canDispatchOnThisThread)
					{
						if (this.readerQueue.Count == 0)
						{
							this.itemQueue.EnqueueAvailableItem(item);
						}
						else
						{
							queueReader = this.readerQueue.Dequeue();
						}
					}
					else if (this.readerQueue.Count == 0)
					{
						this.itemQueue.EnqueueAvailableItem(item);
					}
					else
					{
						this.itemQueue.EnqueuePendingItem(item);
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (array != null)
			{
				if (canDispatchOnThisThread)
				{
					InputQueue<T>.CompleteWaiters(itemAvailable, array);
				}
				else
				{
					InputQueue<T>.CompleteWaitersLater(itemAvailable, array);
				}
			}
			if (queueReader != null)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				queueReader.Set(item);
			}
			if (flag2)
			{
				if (InputQueue<T>.onDispatchCallback == null)
				{
					InputQueue<T>.onDispatchCallback = new Action<object>(InputQueue<T>.OnDispatchCallback);
				}
				ActionItem.Schedule(InputQueue<T>.onDispatchCallback, this);
				return;
			}
			if (flag)
			{
				InputQueue<T>.InvokeDequeuedCallback(item.DequeuedCallback);
				this.DisposeItem(item);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000051E0 File Offset: 0x000033E0
		private bool EnqueueWithoutDispatch(InputQueue<T>.Item item)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState != InputQueue<T>.QueueState.Closed && this.queueState != InputQueue<T>.QueueState.Shutdown)
				{
					if (this.readerQueue.Count == 0 && this.waiterList.Count == 0)
					{
						this.itemQueue.EnqueueAvailableItem(item);
						return false;
					}
					this.itemQueue.EnqueuePendingItem(item);
					return true;
				}
			}
			this.DisposeItem(item);
			InputQueue<T>.InvokeDequeuedCallbackLater(item.DequeuedCallback);
			return false;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000527C File Offset: 0x0000347C
		private void GetWaiters(out InputQueue<T>.IQueueWaiter[] waiters)
		{
			if (this.waiterList.Count > 0)
			{
				waiters = this.waiterList.ToArray();
				this.waiterList.Clear();
				return;
			}
			waiters = null;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000052A8 File Offset: 0x000034A8
		private bool RemoveReader(InputQueue<T>.IQueueReader reader)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.queueState == InputQueue<T>.QueueState.Open || this.queueState == InputQueue<T>.QueueState.Shutdown)
				{
					bool result = false;
					for (int i = this.readerQueue.Count; i > 0; i--)
					{
						InputQueue<T>.IQueueReader queueReader = this.readerQueue.Dequeue();
						if (queueReader == reader)
						{
							result = true;
						}
						else
						{
							this.readerQueue.Enqueue(queueReader);
						}
					}
					return result;
				}
			}
			return false;
		}

		// Token: 0x040000B1 RID: 177
		private static Action<object> completeOutstandingReadersCallback;

		// Token: 0x040000B2 RID: 178
		private static Action<object> completeWaitersFalseCallback;

		// Token: 0x040000B3 RID: 179
		private static Action<object> completeWaitersTrueCallback;

		// Token: 0x040000B4 RID: 180
		private static Action<object> onDispatchCallback;

		// Token: 0x040000B5 RID: 181
		private static Action<object> onInvokeDequeuedCallback;

		// Token: 0x040000B6 RID: 182
		private InputQueue<T>.QueueState queueState;

		// Token: 0x040000B7 RID: 183
		private InputQueue<T>.ItemQueue itemQueue;

		// Token: 0x040000B8 RID: 184
		private Queue<InputQueue<T>.IQueueReader> readerQueue;

		// Token: 0x040000B9 RID: 185
		private List<InputQueue<T>.IQueueWaiter> waiterList;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private Action<T> <DisposeItemCallback>k__BackingField;

		// Token: 0x040000BB RID: 187
		[CompilerGenerated]
		private Func<Action<AsyncCallback, IAsyncResult>> <AsyncCallbackGenerator>k__BackingField;

		// Token: 0x02000074 RID: 116
		private enum QueueState
		{
			// Token: 0x04000291 RID: 657
			Open,
			// Token: 0x04000292 RID: 658
			Shutdown,
			// Token: 0x04000293 RID: 659
			Closed
		}

		// Token: 0x02000075 RID: 117
		private interface IQueueReader
		{
			// Token: 0x060003A9 RID: 937
			void Set(InputQueue<T>.Item item);
		}

		// Token: 0x02000076 RID: 118
		private interface IQueueWaiter
		{
			// Token: 0x060003AA RID: 938
			void Set(bool itemAvailable);
		}

		// Token: 0x02000077 RID: 119
		private struct Item
		{
			// Token: 0x060003AB RID: 939 RVA: 0x00011BC8 File Offset: 0x0000FDC8
			public Item(T value, Action dequeuedCallback)
			{
				this = new InputQueue<T>.Item(value, null, dequeuedCallback);
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00011BD4 File Offset: 0x0000FDD4
			public Item(Exception exception, Action dequeuedCallback)
			{
				this = new InputQueue<T>.Item(default(T), exception, dequeuedCallback);
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00011BF2 File Offset: 0x0000FDF2
			private Item(T value, Exception exception, Action dequeuedCallback)
			{
				this.value = value;
				this.exception = exception;
				this.dequeuedCallback = dequeuedCallback;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060003AE RID: 942 RVA: 0x00011C09 File Offset: 0x0000FE09
			public Action DequeuedCallback
			{
				get
				{
					return this.dequeuedCallback;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003AF RID: 943 RVA: 0x00011C11 File Offset: 0x0000FE11
			public Exception Exception
			{
				get
				{
					return this.exception;
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011C19 File Offset: 0x0000FE19
			public T Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00011C21 File Offset: 0x0000FE21
			public T GetValue()
			{
				if (this.exception != null)
				{
					throw Fx.Exception.AsError(this.exception);
				}
				return this.value;
			}

			// Token: 0x04000294 RID: 660
			private Action dequeuedCallback;

			// Token: 0x04000295 RID: 661
			private Exception exception;

			// Token: 0x04000296 RID: 662
			private T value;
		}

		// Token: 0x02000078 RID: 120
		private class AsyncQueueReader : AsyncResult, InputQueue<T>.IQueueReader
		{
			// Token: 0x060003B2 RID: 946 RVA: 0x00011C44 File Offset: 0x0000FE44
			public AsyncQueueReader(InputQueue<T> inputQueue, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (inputQueue.AsyncCallbackGenerator != null)
				{
					base.VirtualCallback = inputQueue.AsyncCallbackGenerator();
				}
				this.inputQueue = inputQueue;
				if (timeout != TimeSpan.MaxValue)
				{
					this.timer = new IOThreadTimer(InputQueue<T>.AsyncQueueReader.timerCallback, this, false);
					this.timer.Set(timeout);
				}
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00011CA8 File Offset: 0x0000FEA8
			public static bool End(IAsyncResult result, out T value)
			{
				InputQueue<T>.AsyncQueueReader asyncQueueReader = AsyncResult.End<InputQueue<T>.AsyncQueueReader>(result);
				if (asyncQueueReader.expired)
				{
					value = default(T);
					return false;
				}
				value = asyncQueueReader.item;
				return true;
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x00011CDA File Offset: 0x0000FEDA
			public void Set(InputQueue<T>.Item item)
			{
				this.item = item.Value;
				if (this.timer != null)
				{
					this.timer.Cancel();
				}
				base.Complete(false, item.Exception);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x00011D0C File Offset: 0x0000FF0C
			private static void TimerCallback(object state)
			{
				InputQueue<T>.AsyncQueueReader asyncQueueReader = (InputQueue<T>.AsyncQueueReader)state;
				if (asyncQueueReader.inputQueue.RemoveReader(asyncQueueReader))
				{
					asyncQueueReader.expired = true;
					asyncQueueReader.Complete(false);
				}
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x00011D3C File Offset: 0x0000FF3C
			// Note: this type is marked as 'beforefieldinit'.
			static AsyncQueueReader()
			{
			}

			// Token: 0x04000297 RID: 663
			private static Action<object> timerCallback = new Action<object>(InputQueue<T>.AsyncQueueReader.TimerCallback);

			// Token: 0x04000298 RID: 664
			private bool expired;

			// Token: 0x04000299 RID: 665
			private InputQueue<T> inputQueue;

			// Token: 0x0400029A RID: 666
			private T item;

			// Token: 0x0400029B RID: 667
			private IOThreadTimer timer;
		}

		// Token: 0x02000079 RID: 121
		private class AsyncQueueWaiter : AsyncResult, InputQueue<T>.IQueueWaiter
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x00011D4F File Offset: 0x0000FF4F
			public AsyncQueueWaiter(TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (timeout != TimeSpan.MaxValue)
				{
					this.timer = new IOThreadTimer(InputQueue<T>.AsyncQueueWaiter.timerCallback, this, false);
					this.timer.Set(timeout);
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011D8F File Offset: 0x0000FF8F
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x00011D97 File Offset: 0x0000FF97
			public static bool End(IAsyncResult result)
			{
				return AsyncResult.End<InputQueue<T>.AsyncQueueWaiter>(result).itemAvailable;
			}

			// Token: 0x060003BA RID: 954 RVA: 0x00011DA4 File Offset: 0x0000FFA4
			public void Set(bool itemAvailable)
			{
				object obj = this.ThisLock;
				bool flag2;
				lock (obj)
				{
					flag2 = (this.timer == null || this.timer.Cancel());
					this.itemAvailable = itemAvailable;
				}
				if (flag2)
				{
					base.Complete(false);
				}
			}

			// Token: 0x060003BB RID: 955 RVA: 0x00011E08 File Offset: 0x00010008
			private static void TimerCallback(object state)
			{
				((InputQueue<T>.AsyncQueueWaiter)state).Complete(false);
			}

			// Token: 0x060003BC RID: 956 RVA: 0x00011E16 File Offset: 0x00010016
			// Note: this type is marked as 'beforefieldinit'.
			static AsyncQueueWaiter()
			{
			}

			// Token: 0x0400029C RID: 668
			private static Action<object> timerCallback = new Action<object>(InputQueue<T>.AsyncQueueWaiter.TimerCallback);

			// Token: 0x0400029D RID: 669
			private bool itemAvailable;

			// Token: 0x0400029E RID: 670
			private object thisLock = new object();

			// Token: 0x0400029F RID: 671
			private IOThreadTimer timer;
		}

		// Token: 0x0200007A RID: 122
		private class ItemQueue
		{
			// Token: 0x060003BD RID: 957 RVA: 0x00011E29 File Offset: 0x00010029
			public ItemQueue()
			{
				this.items = new InputQueue<T>.Item[1];
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00011E3D File Offset: 0x0001003D
			public bool HasAnyItem
			{
				get
				{
					return this.totalCount > 0;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060003BF RID: 959 RVA: 0x00011E48 File Offset: 0x00010048
			public bool HasAvailableItem
			{
				get
				{
					return this.totalCount > this.pendingCount;
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011E58 File Offset: 0x00010058
			public int ItemCount
			{
				get
				{
					return this.totalCount;
				}
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00011E60 File Offset: 0x00010060
			public InputQueue<T>.Item DequeueAnyItem()
			{
				if (this.pendingCount == this.totalCount)
				{
					this.pendingCount--;
				}
				return this.DequeueItemCore();
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00011E84 File Offset: 0x00010084
			public InputQueue<T>.Item DequeueAvailableItem()
			{
				Fx.AssertAndThrow(this.totalCount != this.pendingCount, "ItemQueue does not contain any available items");
				return this.DequeueItemCore();
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00011EA7 File Offset: 0x000100A7
			public void EnqueueAvailableItem(InputQueue<T>.Item item)
			{
				this.EnqueueItemCore(item);
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x00011EB0 File Offset: 0x000100B0
			public void EnqueuePendingItem(InputQueue<T>.Item item)
			{
				this.EnqueueItemCore(item);
				this.pendingCount++;
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x00011EC7 File Offset: 0x000100C7
			public void MakePendingItemAvailable()
			{
				Fx.AssertAndThrow(this.pendingCount != 0, "ItemQueue does not contain any pending items");
				this.pendingCount--;
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00011EEC File Offset: 0x000100EC
			private InputQueue<T>.Item DequeueItemCore()
			{
				Fx.AssertAndThrow(this.totalCount != 0, "ItemQueue does not contain any items");
				InputQueue<T>.Item result = this.items[this.head];
				this.items[this.head] = default(InputQueue<T>.Item);
				this.totalCount--;
				this.head = (this.head + 1) % this.items.Length;
				return result;
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x00011F5C File Offset: 0x0001015C
			private void EnqueueItemCore(InputQueue<T>.Item item)
			{
				if (this.totalCount == this.items.Length)
				{
					InputQueue<T>.Item[] array = new InputQueue<T>.Item[this.items.Length * 2];
					for (int i = 0; i < this.totalCount; i++)
					{
						array[i] = this.items[(this.head + i) % this.items.Length];
					}
					this.head = 0;
					this.items = array;
				}
				int num = (this.head + this.totalCount) % this.items.Length;
				this.items[num] = item;
				this.totalCount++;
			}

			// Token: 0x040002A0 RID: 672
			private int head;

			// Token: 0x040002A1 RID: 673
			private InputQueue<T>.Item[] items;

			// Token: 0x040002A2 RID: 674
			private int pendingCount;

			// Token: 0x040002A3 RID: 675
			private int totalCount;
		}

		// Token: 0x0200007B RID: 123
		private class WaitQueueReader : InputQueue<T>.IQueueReader
		{
			// Token: 0x060003C8 RID: 968 RVA: 0x00011FFD File Offset: 0x000101FD
			public WaitQueueReader(InputQueue<T> inputQueue)
			{
				this.inputQueue = inputQueue;
				this.waitEvent = new ManualResetEvent(false);
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00012018 File Offset: 0x00010218
			public void Set(InputQueue<T>.Item item)
			{
				lock (this)
				{
					this.exception = item.Exception;
					this.item = item.Value;
					this.waitEvent.Set();
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00012074 File Offset: 0x00010274
			public bool Wait(TimeSpan timeout, out T value)
			{
				bool flag = false;
				try
				{
					if (!TimeoutHelper.WaitOne(this.waitEvent, timeout))
					{
						if (this.inputQueue.RemoveReader(this))
						{
							value = default(T);
							flag = true;
							return false;
						}
						this.waitEvent.WaitOne();
					}
					flag = true;
				}
				finally
				{
					if (flag)
					{
						this.waitEvent.Close();
					}
				}
				if (this.exception != null)
				{
					throw Fx.Exception.AsError(this.exception);
				}
				value = this.item;
				return true;
			}

			// Token: 0x040002A4 RID: 676
			private Exception exception;

			// Token: 0x040002A5 RID: 677
			private InputQueue<T> inputQueue;

			// Token: 0x040002A6 RID: 678
			private T item;

			// Token: 0x040002A7 RID: 679
			private ManualResetEvent waitEvent;
		}

		// Token: 0x0200007C RID: 124
		private class WaitQueueWaiter : InputQueue<T>.IQueueWaiter
		{
			// Token: 0x060003CB RID: 971 RVA: 0x00012104 File Offset: 0x00010304
			public WaitQueueWaiter()
			{
				this.waitEvent = new ManualResetEvent(false);
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00012118 File Offset: 0x00010318
			public void Set(bool itemAvailable)
			{
				lock (this)
				{
					this.itemAvailable = itemAvailable;
					this.waitEvent.Set();
				}
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00012160 File Offset: 0x00010360
			public bool Wait(TimeSpan timeout)
			{
				return TimeoutHelper.WaitOne(this.waitEvent, timeout) && this.itemAvailable;
			}

			// Token: 0x040002A8 RID: 680
			private bool itemAvailable;

			// Token: 0x040002A9 RID: 681
			private ManualResetEvent waitEvent;
		}
	}
}
