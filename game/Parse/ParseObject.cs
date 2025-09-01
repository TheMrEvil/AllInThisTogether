using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Control;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Objects;

namespace Parse
{
	// Token: 0x0200000E RID: 14
	public class ParseObject : IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000031DA File Offset: 0x000013DA
		internal static string AutoClassName
		{
			[CompilerGenerated]
			get
			{
				return ParseObject.<AutoClassName>k__BackingField;
			}
		} = "_Automatic";

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000031E1 File Offset: 0x000013E1
		internal static ThreadLocal<bool> CreatingPointer
		{
			[CompilerGenerated]
			get
			{
				return ParseObject.<CreatingPointer>k__BackingField;
			}
		} = new ThreadLocal<bool>(() => false);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000031E8 File Offset: 0x000013E8
		internal TaskQueue TaskQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<TaskQueue>k__BackingField;
			}
		} = new TaskQueue();

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000031F0 File Offset: 0x000013F0
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000031F8 File Offset: 0x000013F8
		public IServiceHub Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Services>k__BackingField = value;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003204 File Offset: 0x00001404
		public ParseObject(string className, IServiceHub serviceHub = null)
		{
			bool value = ParseObject.CreatingPointer.Value;
			ParseObject.CreatingPointer.Value = false;
			string autoClassName = ParseObject.AutoClassName;
			string text = className;
			if (text == null)
			{
				throw new ArgumentException("You must specify a Parse class name when creating a new ParseObject.");
			}
			if (autoClassName.Equals(text))
			{
				className = base.GetType().GetParseClassName();
			}
			if ((this.Services = (serviceHub ?? ParseClient.Instance)) != null && !this.Services.ClassController.GetClassMatch(className, base.GetType()))
			{
				throw new ArgumentException("You must create this type of ParseObject using ParseObject.Create() or the proper subclass.");
			}
			this.State = new MutableObjectState
			{
				ClassName = className
			};
			this.OnPropertyChanged("ClassName");
			this.OperationSetQueue.AddLast(new Dictionary<string, IParseFieldOperation>());
			if (!value)
			{
				this.Fetched = true;
				this.IsDirty = true;
				this.SetDefaultValues();
				return;
			}
			this.IsDirty = false;
			this.Fetched = false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003318 File Offset: 0x00001518
		protected ParseObject(IServiceHub serviceHub = null) : this(ParseObject.AutoClassName, serviceHub)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003328 File Offset: 0x00001528
		public ParseObject Bind(IServiceHub serviceHub)
		{
			this.Services = serviceHub;
			return new ValueTuple<ParseObject, IServiceHub>(this, serviceHub).Item1;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005B RID: 91 RVA: 0x0000334A File Offset: 0x0000154A
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00003358 File Offset: 0x00001558
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this.PropertyChangedHandler.Add(value);
			}
			remove
			{
				this.PropertyChangedHandler.Remove(value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003366 File Offset: 0x00001566
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003374 File Offset: 0x00001574
		[ParseFieldName("ACL")]
		public ParseACL ACL
		{
			get
			{
				return this.GetProperty<ParseACL>(null, "ACL");
			}
			set
			{
				this.SetProperty<ParseACL>(value, "ACL");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003382 File Offset: 0x00001582
		public string ClassName
		{
			get
			{
				return this.State.ClassName;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000338F File Offset: 0x0000158F
		[ParseFieldName("createdAt")]
		public DateTime? CreatedAt
		{
			get
			{
				return this.State.CreatedAt;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000339C File Offset: 0x0000159C
		public bool IsDataAvailable
		{
			get
			{
				object mutex = this.Mutex;
				bool fetched;
				lock (mutex)
				{
					fetched = this.Fetched;
				}
				return fetched;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000033E0 File Offset: 0x000015E0
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003424 File Offset: 0x00001624
		public bool IsDirty
		{
			get
			{
				object mutex = this.Mutex;
				bool result;
				lock (mutex)
				{
					result = this.CheckIsDirty(true);
				}
				return result;
			}
			internal set
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					this.Dirty = value;
					this.OnPropertyChanged("IsDirty");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003480 File Offset: 0x00001680
		public bool IsNew
		{
			get
			{
				return this.State.IsNew;
			}
			internal set
			{
				this.MutateState(delegate(MutableObjectState mutableClone)
				{
					mutableClone.IsNew = value;
				});
				this.OnPropertyChanged("IsNew");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000034B8 File Offset: 0x000016B8
		public ICollection<string> Keys
		{
			get
			{
				object mutex = this.Mutex;
				ICollection<string> keys;
				lock (mutex)
				{
					keys = this.EstimatedData.Keys;
				}
				return keys;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003500 File Offset: 0x00001700
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000350D File Offset: 0x0000170D
		[ParseFieldName("objectId")]
		public string ObjectId
		{
			get
			{
				return this.State.ObjectId;
			}
			set
			{
				this.IsDirty = true;
				this.SetObjectIdInternal(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000351D File Offset: 0x0000171D
		[ParseFieldName("updatedAt")]
		public DateTime? UpdatedAt
		{
			get
			{
				return this.State.UpdatedAt;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000352C File Offset: 0x0000172C
		public IDictionary<string, IParseFieldOperation> CurrentOperations
		{
			get
			{
				object mutex = this.Mutex;
				IDictionary<string, IParseFieldOperation> value;
				lock (mutex)
				{
					value = this.OperationSetQueue.Last.Value;
				}
				return value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003578 File Offset: 0x00001778
		internal object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003580 File Offset: 0x00001780
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003588 File Offset: 0x00001788
		public IObjectState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003594 File Offset: 0x00001794
		internal bool CanBeSerialized
		{
			get
			{
				object mutex = this.Mutex;
				bool result;
				lock (mutex)
				{
					result = this.Services.CanBeSerializedAsValue(this.EstimatedData);
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000035E4 File Offset: 0x000017E4
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000035EC File Offset: 0x000017EC
		private bool Dirty
		{
			[CompilerGenerated]
			get
			{
				return this.<Dirty>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dirty>k__BackingField = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000035F5 File Offset: 0x000017F5
		internal IDictionary<string, object> EstimatedData
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatedData>k__BackingField;
			}
		} = new Dictionary<string, object>();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000035FD File Offset: 0x000017FD
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003605 File Offset: 0x00001805
		internal bool Fetched
		{
			[CompilerGenerated]
			get
			{
				return this.<Fetched>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Fetched>k__BackingField = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003610 File Offset: 0x00001810
		private bool HasDirtyChildren
		{
			get
			{
				object mutex = this.Mutex;
				bool result;
				lock (mutex)
				{
					result = (this.FindUnsavedChildren().FirstOrDefault<ParseObject>() != null);
				}
				return result;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000365C File Offset: 0x0000185C
		private LinkedList<IDictionary<string, IParseFieldOperation>> OperationSetQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<OperationSetQueue>k__BackingField;
			}
		} = new LinkedList<IDictionary<string, IParseFieldOperation>>();

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003664 File Offset: 0x00001864
		private SynchronizedEventHandler<PropertyChangedEventArgs> PropertyChangedHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyChangedHandler>k__BackingField;
			}
		} = new SynchronizedEventHandler<PropertyChangedEventArgs>();

		// Token: 0x17000036 RID: 54
		public virtual object this[string key]
		{
			get
			{
				object mutex = this.Mutex;
				object result;
				lock (mutex)
				{
					this.CheckGetAccess(key);
					object obj = this.EstimatedData[key];
					ParseRelationBase parseRelationBase = obj as ParseRelationBase;
					if (parseRelationBase != null)
					{
						parseRelationBase.EnsureParentAndKey(this, key);
					}
					result = obj;
				}
				return result;
			}
			set
			{
				object mutex = this.Mutex;
				lock (mutex)
				{
					this.CheckKeyIsMutable(key);
					this.Set(key, value);
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000371C File Offset: 0x0000191C
		public void Add(string key, object value)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (this.ContainsKey(key))
				{
					throw new ArgumentException("Key already exists", key);
				}
				this[key] = value;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003774 File Offset: 0x00001974
		public void AddRangeToList<T>(string key, IEnumerable<T> values)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, new ParseAddOperation(values.Cast<object>()));
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000037C8 File Offset: 0x000019C8
		public void AddRangeUniqueToList<T>(string key, IEnumerable<T> values)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, new ParseAddUniqueOperation(values.Cast<object>()));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000381C File Offset: 0x00001A1C
		public void AddToList(string key, object value)
		{
			this.AddRangeToList<object>(key, new object[]
			{
				value
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000382F File Offset: 0x00001A2F
		public void AddUniqueToList(string key, object value)
		{
			this.AddRangeUniqueToList<object>(key, new object[]
			{
				value
			});
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003844 File Offset: 0x00001A44
		public bool ContainsKey(string key)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = this.EstimatedData.ContainsKey(key);
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000388C File Offset: 0x00001A8C
		public Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.TaskQueue.Enqueue<Task>((Task toAwait) => this.DeleteAsync(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000038CA File Offset: 0x00001ACA
		public T Get<T>(string key)
		{
			return Conversion.To<T>(this[key]);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038D8 File Offset: 0x00001AD8
		public ParseRelation<T> GetRelation<T>(string key) where T : ParseObject
		{
			ParseRelation<T> parseRelation;
			this.TryGetValue<ParseRelation<T>>(key, out parseRelation);
			return parseRelation ?? new ParseRelation<T>(this, key);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000038FC File Offset: 0x00001AFC
		public bool HasSameId(ParseObject other)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = (other != null && object.Equals(this.ClassName, other.ClassName) && object.Equals(this.ObjectId, other.ObjectId));
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003964 File Offset: 0x00001B64
		public void Increment(string key)
		{
			this.Increment(key, 1L);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003970 File Offset: 0x00001B70
		public void Increment(string key, long amount)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, new ParseIncrementOperation(amount));
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000039C4 File Offset: 0x00001BC4
		public void Increment(string key, double amount)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, new ParseIncrementOperation(amount));
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003A18 File Offset: 0x00001C18
		public bool IsKeyDirty(string key)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = this.CurrentOperations.ContainsKey(key);
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003A60 File Offset: 0x00001C60
		public virtual void Remove(string key)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, ParseDeleteOperation.Instance);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public void RemoveAllFromList<T>(string key, IEnumerable<T> values)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.CheckKeyIsMutable(key);
				this.PerformOperation(key, new ParseRemoveOperation(values.Cast<object>()));
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B04 File Offset: 0x00001D04
		public void Revert()
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (this.CurrentOperations.Count > 0)
				{
					this.CurrentOperations.Clear();
					this.RebuildEstimatedData();
					this.OnPropertyChanged("IsDirty");
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B68 File Offset: 0x00001D68
		public Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.TaskQueue.Enqueue<Task>((Task toAwait) => this.SaveAsync(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public bool TryGetValue<T>(string key, out T result)
		{
			object mutex = this.Mutex;
			bool result2;
			lock (mutex)
			{
				if (this.ContainsKey(key))
				{
					try
					{
						T t = Conversion.To<T>(this[key]);
						result = t;
						return true;
					}
					catch
					{
						result = default(T);
						return false;
					}
				}
				result = default(T);
				result2 = false;
			}
			return result2;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C28 File Offset: 0x00001E28
		internal Task DeleteAsync(Task toAwait, CancellationToken cancellationToken)
		{
			if (this.ObjectId == null)
			{
				return Task.FromResult<int>(0);
			}
			string sessionToken = this.Services.GetCurrentSessionToken();
			return toAwait.OnSuccess((Task _) => this.Services.ObjectController.DeleteAsync(this.State, sessionToken, cancellationToken)).Unwrap().OnSuccess((Task _) => this.IsDirty = true);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C94 File Offset: 0x00001E94
		internal virtual Task<ParseObject> FetchAsyncInternal(Task toAwait, CancellationToken cancellationToken)
		{
			return toAwait.OnSuccess(delegate(Task _)
			{
				if (this.ObjectId != null)
				{
					return this.Services.ObjectController.FetchAsync(this.State, this.Services.GetCurrentSessionToken(), this.Services, cancellationToken);
				}
				throw new InvalidOperationException("Cannot refresh an object that hasn't been saved to the server.");
			}).Unwrap<IObjectState>().OnSuccess(delegate(Task<IObjectState> task)
			{
				this.HandleFetchResult(task.Result);
				return this;
			});
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003CE0 File Offset: 0x00001EE0
		internal Task<ParseObject> FetchAsyncInternal(CancellationToken cancellationToken)
		{
			return this.TaskQueue.Enqueue<Task<ParseObject>>((Task toAwait) => this.FetchAsyncInternal(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D1E File Offset: 0x00001F1E
		internal Task<ParseObject> FetchIfNeededAsyncInternal(Task toAwait, CancellationToken cancellationToken)
		{
			if (this.IsDataAvailable)
			{
				return Task.FromResult<ParseObject>(this);
			}
			return this.FetchAsyncInternal(toAwait, cancellationToken);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003D38 File Offset: 0x00001F38
		internal Task<ParseObject> FetchIfNeededAsyncInternal(CancellationToken cancellationToken)
		{
			return this.TaskQueue.Enqueue<Task<ParseObject>>((Task toAwait) => this.FetchIfNeededAsyncInternal(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D78 File Offset: 0x00001F78
		internal void HandleFailedSave(IDictionary<string, IParseFieldOperation> operationsBeforeSave)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				LinkedListNode<IDictionary<string, IParseFieldOperation>> linkedListNode = this.OperationSetQueue.Find(operationsBeforeSave);
				IDictionary<string, IParseFieldOperation> value = linkedListNode.Next.Value;
				bool flag2 = value.Count > 0;
				this.OperationSetQueue.Remove(linkedListNode);
				foreach (KeyValuePair<string, IParseFieldOperation> keyValuePair in operationsBeforeSave)
				{
					IParseFieldOperation value2 = keyValuePair.Value;
					IParseFieldOperation parseFieldOperation;
					value.TryGetValue(keyValuePair.Key, out parseFieldOperation);
					value[keyValuePair.Key] = ((parseFieldOperation != null) ? parseFieldOperation.MergeWithPrevious(value2) : value2);
				}
				if (!flag2 && value == this.CurrentOperations && operationsBeforeSave.Count > 0)
				{
					this.OnPropertyChanged("IsDirty");
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E70 File Offset: 0x00002070
		public virtual void HandleFetchResult(IObjectState serverState)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.MergeFromServer(serverState);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003EB4 File Offset: 0x000020B4
		internal virtual void HandleSave(IObjectState serverState)
		{
			object mutex = this.Mutex;
			bool flag = false;
			try
			{
				Monitor.Enter(mutex, ref flag);
				IDictionary<string, IParseFieldOperation> operationsBeforeSave = this.OperationSetQueue.First.Value;
				this.OperationSetQueue.RemoveFirst();
				this.MutateState(delegate(MutableObjectState mutableClone)
				{
					mutableClone.Apply(operationsBeforeSave);
				});
				this.MergeFromServer(serverState);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(mutex);
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003F30 File Offset: 0x00002130
		internal void MergeFromObject(ParseObject other)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (this == other)
				{
					return;
				}
			}
			if (this.OperationSetQueue.Count != 1)
			{
				throw new InvalidOperationException("Attempt to MergeFromObject during save.");
			}
			this.OperationSetQueue.Clear();
			foreach (IDictionary<string, IParseFieldOperation> source in other.OperationSetQueue)
			{
				this.OperationSetQueue.AddLast(source.ToDictionary((KeyValuePair<string, IParseFieldOperation> entry) => entry.Key, (KeyValuePair<string, IParseFieldOperation> entry) => entry.Value));
			}
			mutex = this.Mutex;
			lock (mutex)
			{
				this.State = other.State;
			}
			this.RebuildEstimatedData();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004060 File Offset: 0x00002260
		internal virtual void MergeFromServer(IObjectState serverState)
		{
			Dictionary<string, object> newServerData = serverState.ToDictionary((KeyValuePair<string, object> t) => t.Key, (KeyValuePair<string, object> t) => t.Value);
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (serverState.ObjectId != null)
				{
					this.Fetched = true;
					this.OnPropertyChanged("IsDataAvailable");
				}
				if (serverState.UpdatedAt != null)
				{
					this.OnPropertyChanged("UpdatedAt");
				}
				if (serverState.CreatedAt != null)
				{
					this.OnPropertyChanged("CreatedAt");
				}
				IDictionary<string, ParseObject> dictionary = this.CollectFetchedObjects();
				foreach (KeyValuePair<string, object> keyValuePair in serverState)
				{
					object obj = keyValuePair.Value;
					if (obj is ParseObject)
					{
						ParseObject parseObject = obj as ParseObject;
						if (dictionary.ContainsKey(parseObject.ObjectId))
						{
							obj = dictionary[parseObject.ObjectId];
						}
					}
					newServerData[keyValuePair.Key] = obj;
				}
				this.IsDirty = false;
				Action<MutableObjectState> <>9__3;
				this.MutateState(delegate(MutableObjectState mutableClone)
				{
					IObjectState serverState2 = serverState;
					Action<MutableObjectState> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = delegate(MutableObjectState mutableClone)
						{
							mutableClone.ServerData = newServerData;
						});
					}
					mutableClone.Apply(serverState2.MutatedClone(func));
				});
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004218 File Offset: 0x00002418
		internal void MutateState(Action<MutableObjectState> mutator)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.State = this.State.MutatedClone(mutator);
				this.RebuildEstimatedData();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000426C File Offset: 0x0000246C
		internal virtual void OnSettingValue(ref string key, ref object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004280 File Offset: 0x00002480
		internal void PerformOperation(string key, IParseFieldOperation operation)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				object oldValue;
				this.EstimatedData.TryGetValue(key, out oldValue);
				object obj = operation.Apply(oldValue, key);
				if (obj != ParseDeleteOperation.Token)
				{
					this.EstimatedData[key] = obj;
				}
				else
				{
					this.EstimatedData.Remove(key);
				}
				bool flag2 = this.CurrentOperations.Count > 0;
				IParseFieldOperation previous;
				this.CurrentOperations.TryGetValue(key, out previous);
				IParseFieldOperation value = operation.MergeWithPrevious(previous);
				this.CurrentOperations[key] = value;
				if (!flag2)
				{
					this.OnPropertyChanged("IsDirty");
				}
				this.OnFieldsChanged(new string[]
				{
					key
				});
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004348 File Offset: 0x00002548
		internal void RebuildEstimatedData()
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.EstimatedData.Clear();
				foreach (KeyValuePair<string, object> item in this.State)
				{
					this.EstimatedData.Add(item);
				}
				foreach (IDictionary<string, IParseFieldOperation> operations in this.OperationSetQueue)
				{
					this.ApplyOperations(operations, this.EstimatedData);
				}
				this.OnFieldsChanged(null);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004420 File Offset: 0x00002620
		public IDictionary<string, object> ServerDataToJSONObjectForSerialization()
		{
			return PointerOrLocalIdEncoder.Instance.Encode(this.State.ToDictionary((KeyValuePair<string, object> pair) => pair.Key, (KeyValuePair<string, object> pair) => pair.Value), this.Services) as IDictionary<string, object>;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000448C File Offset: 0x0000268C
		public void Set(string key, object value)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.OnSettingValue(ref key, ref value);
				if (!ParseDataEncoder.Validate(value))
				{
					throw new ArgumentException("Invalid type for value: " + value.GetType().ToString());
				}
				this.PerformOperation(key, new ParseSetOperation(value));
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004500 File Offset: 0x00002700
		internal virtual void SetDefaultValues()
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004504 File Offset: 0x00002704
		public void SetIfDifferent<T>(string key, T value)
		{
			T t;
			bool flag = this.TryGetValue<T>(key, out t);
			if (value == null)
			{
				if (flag)
				{
					this.PerformOperation(key, ParseDeleteOperation.Instance);
				}
				return;
			}
			if (!flag || !value.Equals(t))
			{
				this.Set(key, value);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004558 File Offset: 0x00002758
		internal IDictionary<string, IParseFieldOperation> StartSave()
		{
			object mutex = this.Mutex;
			IDictionary<string, IParseFieldOperation> result;
			lock (mutex)
			{
				IDictionary<string, IParseFieldOperation> currentOperations = this.CurrentOperations;
				this.OperationSetQueue.AddLast(new Dictionary<string, IParseFieldOperation>());
				this.OnPropertyChanged("IsDirty");
				result = currentOperations;
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000045B8 File Offset: 0x000027B8
		protected T GetProperty<T>([CallerMemberName] string propertyName = null)
		{
			return this.GetProperty<T>(default(T), propertyName);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000045D8 File Offset: 0x000027D8
		protected T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyName = null)
		{
			T result;
			if (!this.TryGetValue<T>(this.Services.GetFieldForPropertyName(this.ClassName, propertyName), out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004604 File Offset: 0x00002804
		protected ParseRelation<T> GetRelationProperty<T>([CallerMemberName] string propertyName = null) where T : ParseObject
		{
			return this.GetRelation<T>(this.Services.GetFieldForPropertyName(this.ClassName, propertyName));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000461E File Offset: 0x0000281E
		protected virtual bool CheckKeyMutable(string key)
		{
			return true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004624 File Offset: 0x00002824
		protected void OnFieldsChanged(IEnumerable<string> fields)
		{
			IDictionary<string, string> propertyMappings = this.Services.ClassController.GetPropertyMappings(this.ClassName);
			IEnumerable<string> enumerable;
			if (propertyMappings == null)
			{
				enumerable = Enumerable.Empty<string>();
			}
			else if (fields == null)
			{
				IEnumerable<string> keys = propertyMappings.Keys;
				enumerable = keys;
			}
			else
			{
				enumerable = propertyMappings.Join(fields, delegate(KeyValuePair<string, string> mapping)
				{
					KeyValuePair<string, string> keyValuePair = mapping;
					return keyValuePair.Value;
				}, (string field) => field, delegate(KeyValuePair<string, string> mapping, string field)
				{
					KeyValuePair<string, string> keyValuePair = mapping;
					return keyValuePair.Key;
				});
			}
			foreach (string propertyName in enumerable)
			{
				this.OnPropertyChanged(propertyName);
			}
			this.OnPropertyChanged("Item[]");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004708 File Offset: 0x00002908
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChangedHandler.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004720 File Offset: 0x00002920
		protected virtual Task SaveAsync(Task toAwait, CancellationToken cancellationToken)
		{
			IDictionary<string, IParseFieldOperation> currentOperations = null;
			if (!this.IsDirty)
			{
				return Task.CompletedTask;
			}
			object mutex = this.Mutex;
			string sessionToken;
			Task task2;
			lock (mutex)
			{
				currentOperations = this.StartSave();
				sessionToken = this.Services.GetCurrentSessionToken();
				task2 = this.Services.DeepSaveAsync(this.EstimatedData, sessionToken, cancellationToken);
			}
			return task2.OnSuccess((Task _) => toAwait).Unwrap().OnSuccess((Task _) => this.Services.ObjectController.SaveAsync(this.State, currentOperations, sessionToken, this.Services, cancellationToken)).Unwrap<IObjectState>().ContinueWith<Task<IObjectState>>(delegate(Task<IObjectState> task)
			{
				if (task.IsFaulted || task.IsCanceled)
				{
					this.HandleFailedSave(currentOperations);
				}
				else
				{
					this.HandleSave(task.Result);
				}
				return task;
			}).Unwrap<IObjectState>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004808 File Offset: 0x00002A08
		protected void SetProperty<T>(T value, [CallerMemberName] string propertyName = null)
		{
			this[this.Services.GetFieldForPropertyName(this.ClassName, propertyName)] = value;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004828 File Offset: 0x00002A28
		private void ApplyOperations(IDictionary<string, IParseFieldOperation> operations, IDictionary<string, object> map)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				foreach (KeyValuePair<string, IParseFieldOperation> keyValuePair in operations)
				{
					object oldValue;
					map.TryGetValue(keyValuePair.Key, out oldValue);
					object obj = keyValuePair.Value.Apply(oldValue, keyValuePair.Key);
					if (obj != ParseDeleteOperation.Token)
					{
						map[keyValuePair.Key] = obj;
					}
					else
					{
						map.Remove(keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000048E0 File Offset: 0x00002AE0
		private void CheckGetAccess(string key)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				if (!this.CheckIsDataAvailable(key))
				{
					throw new InvalidOperationException("ParseObject has no data for this key. Call FetchIfNeededAsync() to get the data.");
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004930 File Offset: 0x00002B30
		private bool CheckIsDataAvailable(string key)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = (this.IsDataAvailable || this.EstimatedData.ContainsKey(key));
			}
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004984 File Offset: 0x00002B84
		internal bool CheckIsDirty(bool considerChildren)
		{
			object mutex = this.Mutex;
			bool result;
			lock (mutex)
			{
				result = (this.Dirty || this.CurrentOperations.Count > 0 || (considerChildren && this.HasDirtyChildren));
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000049E8 File Offset: 0x00002BE8
		private void CheckKeyIsMutable(string key)
		{
			if (!this.CheckKeyMutable(key))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot change the \"",
					key,
					"\" property of a \"",
					this.ClassName,
					"\" object."
				}));
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004A34 File Offset: 0x00002C34
		private IDictionary<string, ParseObject> CollectFetchedObjects()
		{
			return (from o in this.Services.TraverseObjectDeep(this.EstimatedData, false, false).OfType<ParseObject>()
			where o.ObjectId != null && o.IsDataAvailable
			group o by o.ObjectId).ToDictionary((IGrouping<string, ParseObject> group) => group.Key, (IGrouping<string, ParseObject> group) => group.Last<ParseObject>());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private IEnumerable<ParseObject> FindUnsavedChildren()
		{
			return from o in this.Services.TraverseObjectDeep(this.EstimatedData, false, false).OfType<ParseObject>()
			where o.IsDirty
			select o;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004B24 File Offset: 0x00002D24
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			object mutex = this.Mutex;
			IEnumerator<KeyValuePair<string, object>> enumerator;
			lock (mutex)
			{
				enumerator = this.EstimatedData.GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004B6C File Offset: 0x00002D6C
		IEnumerator IEnumerable.GetEnumerator()
		{
			object mutex = this.Mutex;
			IEnumerator enumerator;
			lock (mutex)
			{
				enumerator = ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004BB0 File Offset: 0x00002DB0
		private void SetObjectIdInternal(string objectId)
		{
			object mutex = this.Mutex;
			lock (mutex)
			{
				this.MutateState(delegate(MutableObjectState mutableClone)
				{
					mutableClone.ObjectId = objectId;
				});
				this.OnPropertyChanged("ObjectId");
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004C14 File Offset: 0x00002E14
		// Note: this type is marked as 'beforefieldinit'.
		static ParseObject()
		{
		}

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private static readonly string <AutoClassName>k__BackingField;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private static readonly ThreadLocal<bool> <CreatingPointer>k__BackingField;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private readonly TaskQueue <TaskQueue>k__BackingField;

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		private IServiceHub <Services>k__BackingField;

		// Token: 0x04000016 RID: 22
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x04000017 RID: 23
		[CompilerGenerated]
		private IObjectState <State>k__BackingField;

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private bool <Dirty>k__BackingField;

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		private readonly IDictionary<string, object> <EstimatedData>k__BackingField;

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private bool <Fetched>k__BackingField;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private readonly LinkedList<IDictionary<string, IParseFieldOperation>> <OperationSetQueue>k__BackingField;

		// Token: 0x0400001C RID: 28
		[CompilerGenerated]
		private readonly SynchronizedEventHandler<PropertyChangedEventArgs> <PropertyChangedHandler>k__BackingField;

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x060005C3 RID: 1475 RVA: 0x00012D99 File Offset: 0x00010F99
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x00012DA1 File Offset: 0x00010FA1
			internal void <set_IsNew>b__0(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = this.value;
			}

			// Token: 0x04000110 RID: 272
			public bool value;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		private sealed class <>c__DisplayClass80_0
		{
			// Token: 0x060005C5 RID: 1477 RVA: 0x00012DAF File Offset: 0x00010FAF
			public <>c__DisplayClass80_0()
			{
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x00012DB7 File Offset: 0x00010FB7
			internal Task <DeleteAsync>b__0(Task toAwait)
			{
				return this.<>4__this.DeleteAsync(toAwait, this.cancellationToken);
			}

			// Token: 0x04000111 RID: 273
			public ParseObject <>4__this;

			// Token: 0x04000112 RID: 274
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		private sealed class <>c__DisplayClass91_0
		{
			// Token: 0x060005C7 RID: 1479 RVA: 0x00012DCB File Offset: 0x00010FCB
			public <>c__DisplayClass91_0()
			{
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00012DD3 File Offset: 0x00010FD3
			internal Task <SaveAsync>b__0(Task toAwait)
			{
				return this.<>4__this.SaveAsync(toAwait, this.cancellationToken);
			}

			// Token: 0x04000113 RID: 275
			public ParseObject <>4__this;

			// Token: 0x04000114 RID: 276
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		private sealed class <>c__DisplayClass93_0
		{
			// Token: 0x060005C9 RID: 1481 RVA: 0x00012DE7 File Offset: 0x00010FE7
			public <>c__DisplayClass93_0()
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x00012DEF File Offset: 0x00010FEF
			internal Task <DeleteAsync>b__0(Task _)
			{
				return this.<>4__this.Services.ObjectController.DeleteAsync(this.<>4__this.State, this.sessionToken, this.cancellationToken);
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x00012E20 File Offset: 0x00011020
			internal bool <DeleteAsync>b__1(Task _)
			{
				return this.<>4__this.IsDirty = true;
			}

			// Token: 0x04000115 RID: 277
			public ParseObject <>4__this;

			// Token: 0x04000116 RID: 278
			public string sessionToken;

			// Token: 0x04000117 RID: 279
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		private sealed class <>c__DisplayClass94_0
		{
			// Token: 0x060005CC RID: 1484 RVA: 0x00012E3C File Offset: 0x0001103C
			public <>c__DisplayClass94_0()
			{
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x00012E44 File Offset: 0x00011044
			internal Task<IObjectState> <FetchAsyncInternal>b__0(Task _)
			{
				if (this.<>4__this.ObjectId != null)
				{
					return this.<>4__this.Services.ObjectController.FetchAsync(this.<>4__this.State, this.<>4__this.Services.GetCurrentSessionToken(), this.<>4__this.Services, this.cancellationToken);
				}
				throw new InvalidOperationException("Cannot refresh an object that hasn't been saved to the server.");
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x00012EAA File Offset: 0x000110AA
			internal ParseObject <FetchAsyncInternal>b__1(Task<IObjectState> task)
			{
				this.<>4__this.HandleFetchResult(task.Result);
				return this.<>4__this;
			}

			// Token: 0x04000118 RID: 280
			public ParseObject <>4__this;

			// Token: 0x04000119 RID: 281
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		private sealed class <>c__DisplayClass95_0
		{
			// Token: 0x060005CF RID: 1487 RVA: 0x00012EC3 File Offset: 0x000110C3
			public <>c__DisplayClass95_0()
			{
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00012ECB File Offset: 0x000110CB
			internal Task<ParseObject> <FetchAsyncInternal>b__0(Task toAwait)
			{
				return this.<>4__this.FetchAsyncInternal(toAwait, this.cancellationToken);
			}

			// Token: 0x0400011A RID: 282
			public ParseObject <>4__this;

			// Token: 0x0400011B RID: 283
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class <>c__DisplayClass97_0
		{
			// Token: 0x060005D1 RID: 1489 RVA: 0x00012EDF File Offset: 0x000110DF
			public <>c__DisplayClass97_0()
			{
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x00012EE7 File Offset: 0x000110E7
			internal Task<ParseObject> <FetchIfNeededAsyncInternal>b__0(Task toAwait)
			{
				return this.<>4__this.FetchIfNeededAsyncInternal(toAwait, this.cancellationToken);
			}

			// Token: 0x0400011C RID: 284
			public ParseObject <>4__this;

			// Token: 0x0400011D RID: 285
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private sealed class <>c__DisplayClass100_0
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x00012EFB File Offset: 0x000110FB
			public <>c__DisplayClass100_0()
			{
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x00012F03 File Offset: 0x00011103
			internal void <HandleSave>b__0(MutableObjectState mutableClone)
			{
				mutableClone.Apply(this.operationsBeforeSave);
			}

			// Token: 0x0400011E RID: 286
			public IDictionary<string, IParseFieldOperation> operationsBeforeSave;
		}

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005D5 RID: 1493 RVA: 0x00012F11 File Offset: 0x00011111
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x00012F1D File Offset: 0x0001111D
			public <>c()
			{
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x00012F25 File Offset: 0x00011125
			internal string <MergeFromObject>b__101_0(KeyValuePair<string, IParseFieldOperation> entry)
			{
				return entry.Key;
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x00012F2E File Offset: 0x0001112E
			internal IParseFieldOperation <MergeFromObject>b__101_1(KeyValuePair<string, IParseFieldOperation> entry)
			{
				return entry.Value;
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x00012F37 File Offset: 0x00011137
			internal string <MergeFromServer>b__102_0(KeyValuePair<string, object> t)
			{
				return t.Key;
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00012F40 File Offset: 0x00011140
			internal object <MergeFromServer>b__102_1(KeyValuePair<string, object> t)
			{
				return t.Value;
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x00012F49 File Offset: 0x00011149
			internal string <ServerDataToJSONObjectForSerialization>b__107_0(KeyValuePair<string, object> pair)
			{
				return pair.Key;
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x00012F52 File Offset: 0x00011152
			internal object <ServerDataToJSONObjectForSerialization>b__107_1(KeyValuePair<string, object> pair)
			{
				return pair.Value;
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x00012F5C File Offset: 0x0001115C
			internal string <OnFieldsChanged>b__116_0(KeyValuePair<string, string> mapping)
			{
				KeyValuePair<string, string> keyValuePair = mapping;
				return keyValuePair.Value;
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x00012F72 File Offset: 0x00011172
			internal string <OnFieldsChanged>b__116_1(string field)
			{
				return field;
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x00012F78 File Offset: 0x00011178
			internal string <OnFieldsChanged>b__116_2(KeyValuePair<string, string> mapping, string field)
			{
				KeyValuePair<string, string> keyValuePair = mapping;
				return keyValuePair.Key;
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x00012F8E File Offset: 0x0001118E
			internal bool <CollectFetchedObjects>b__125_0(ParseObject o)
			{
				return o.ObjectId != null && o.IsDataAvailable;
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x00012FA0 File Offset: 0x000111A0
			internal string <CollectFetchedObjects>b__125_1(ParseObject o)
			{
				return o.ObjectId;
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x00012FA8 File Offset: 0x000111A8
			internal string <CollectFetchedObjects>b__125_2(IGrouping<string, ParseObject> group)
			{
				return group.Key;
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x00012FB0 File Offset: 0x000111B0
			internal ParseObject <CollectFetchedObjects>b__125_3(IGrouping<string, ParseObject> group)
			{
				return group.Last<ParseObject>();
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x00012FB8 File Offset: 0x000111B8
			internal bool <FindUnsavedChildren>b__126_0(ParseObject o)
			{
				return o.IsDirty;
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x00012FC0 File Offset: 0x000111C0
			internal bool <.cctor>b__130_0()
			{
				return false;
			}

			// Token: 0x0400011F RID: 287
			public static readonly ParseObject.<>c <>9 = new ParseObject.<>c();

			// Token: 0x04000120 RID: 288
			public static Func<KeyValuePair<string, IParseFieldOperation>, string> <>9__101_0;

			// Token: 0x04000121 RID: 289
			public static Func<KeyValuePair<string, IParseFieldOperation>, IParseFieldOperation> <>9__101_1;

			// Token: 0x04000122 RID: 290
			public static Func<KeyValuePair<string, object>, string> <>9__102_0;

			// Token: 0x04000123 RID: 291
			public static Func<KeyValuePair<string, object>, object> <>9__102_1;

			// Token: 0x04000124 RID: 292
			public static Func<KeyValuePair<string, object>, string> <>9__107_0;

			// Token: 0x04000125 RID: 293
			public static Func<KeyValuePair<string, object>, object> <>9__107_1;

			// Token: 0x04000126 RID: 294
			public static Func<KeyValuePair<string, string>, string> <>9__116_0;

			// Token: 0x04000127 RID: 295
			public static Func<string, string> <>9__116_1;

			// Token: 0x04000128 RID: 296
			public static Func<KeyValuePair<string, string>, string, string> <>9__116_2;

			// Token: 0x04000129 RID: 297
			public static Func<ParseObject, bool> <>9__125_0;

			// Token: 0x0400012A RID: 298
			public static Func<ParseObject, string> <>9__125_1;

			// Token: 0x0400012B RID: 299
			public static Func<IGrouping<string, ParseObject>, string> <>9__125_2;

			// Token: 0x0400012C RID: 300
			public static Func<IGrouping<string, ParseObject>, ParseObject> <>9__125_3;

			// Token: 0x0400012D RID: 301
			public static Func<ParseObject, bool> <>9__126_0;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		private sealed class <>c__DisplayClass102_0
		{
			// Token: 0x060005E6 RID: 1510 RVA: 0x00012FC3 File Offset: 0x000111C3
			public <>c__DisplayClass102_0()
			{
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x00012FCC File Offset: 0x000111CC
			internal void <MergeFromServer>b__2(MutableObjectState mutableClone)
			{
				IObjectState objectState = this.serverState;
				Action<MutableObjectState> func;
				if ((func = this.<>9__3) == null)
				{
					func = (this.<>9__3 = delegate(MutableObjectState mutableClone)
					{
						mutableClone.ServerData = this.newServerData;
					});
				}
				mutableClone2.Apply(objectState.MutatedClone(func));
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x00013009 File Offset: 0x00011209
			internal void <MergeFromServer>b__3(MutableObjectState mutableClone)
			{
				mutableClone.ServerData = this.newServerData;
			}

			// Token: 0x0400012E RID: 302
			public IObjectState serverState;

			// Token: 0x0400012F RID: 303
			public Dictionary<string, object> newServerData;

			// Token: 0x04000130 RID: 304
			public Action<MutableObjectState> <>9__3;
		}

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private sealed class <>c__DisplayClass118_0
		{
			// Token: 0x060005E9 RID: 1513 RVA: 0x00013017 File Offset: 0x00011217
			public <>c__DisplayClass118_0()
			{
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0001301F File Offset: 0x0001121F
			internal Task <SaveAsync>b__0(Task _)
			{
				return this.toAwait;
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x00013027 File Offset: 0x00011227
			internal Task<IObjectState> <SaveAsync>b__1(Task _)
			{
				return this.<>4__this.Services.ObjectController.SaveAsync(this.<>4__this.State, this.currentOperations, this.sessionToken, this.<>4__this.Services, this.cancellationToken);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x00013066 File Offset: 0x00011266
			internal Task<IObjectState> <SaveAsync>b__2(Task<IObjectState> task)
			{
				if (task.IsFaulted || task.IsCanceled)
				{
					this.<>4__this.HandleFailedSave(this.currentOperations);
				}
				else
				{
					this.<>4__this.HandleSave(task.Result);
				}
				return task;
			}

			// Token: 0x04000131 RID: 305
			public Task toAwait;

			// Token: 0x04000132 RID: 306
			public ParseObject <>4__this;

			// Token: 0x04000133 RID: 307
			public IDictionary<string, IParseFieldOperation> currentOperations;

			// Token: 0x04000134 RID: 308
			public string sessionToken;

			// Token: 0x04000135 RID: 309
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class <>c__DisplayClass129_0
		{
			// Token: 0x060005ED RID: 1517 RVA: 0x0001309D File Offset: 0x0001129D
			public <>c__DisplayClass129_0()
			{
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x000130A5 File Offset: 0x000112A5
			internal void <SetObjectIdInternal>b__0(MutableObjectState mutableClone)
			{
				mutableClone.ObjectId = this.objectId;
			}

			// Token: 0x04000136 RID: 310
			public string objectId;
		}
	}
}
