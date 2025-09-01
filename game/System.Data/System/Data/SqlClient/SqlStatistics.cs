using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200022B RID: 555
	internal sealed class SqlStatistics
	{
		// Token: 0x06001AB6 RID: 6838 RVA: 0x0007B085 File Offset: 0x00079285
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0007B096 File Offset: 0x00079296
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0007B0A1 File Offset: 0x000792A1
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0007B0A9 File Offset: 0x000792A9
		internal bool WaitForDoneAfterRow
		{
			get
			{
				return this._waitForDoneAfterRow;
			}
			set
			{
				this._waitForDoneAfterRow = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0007B0B2 File Offset: 0x000792B2
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00003D93 File Offset: 0x00001F93
		internal SqlStatistics()
		{
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0007B0BA File Offset: 0x000792BA
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0007B0DC File Offset: 0x000792DC
		internal IDictionary GetDictionary()
		{
			return new SqlStatistics.StatisticsDictionary(18)
			{
				{
					"BuffersReceived",
					this._buffersReceived
				},
				{
					"BuffersSent",
					this._buffersSent
				},
				{
					"BytesReceived",
					this._bytesReceived
				},
				{
					"BytesSent",
					this._bytesSent
				},
				{
					"CursorOpens",
					this._cursorOpens
				},
				{
					"IduCount",
					this._iduCount
				},
				{
					"IduRows",
					this._iduRows
				},
				{
					"PreparedExecs",
					this._preparedExecs
				},
				{
					"Prepares",
					this._prepares
				},
				{
					"SelectCount",
					this._selectCount
				},
				{
					"SelectRows",
					this._selectRows
				},
				{
					"ServerRoundtrips",
					this._serverRoundtrips
				},
				{
					"SumResultSets",
					this._sumResultSets
				},
				{
					"Transactions",
					this._transactions
				},
				{
					"UnpreparedExecs",
					this._unpreparedExecs
				},
				{
					"ConnectionTime",
					ADP.TimerToMilliseconds(this._connectionTime)
				},
				{
					"ExecutionTime",
					ADP.TimerToMilliseconds(this._executionTime)
				},
				{
					"NetworkServerTime",
					ADP.TimerToMilliseconds(this._networkServerTime)
				}
			};
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0007B28B File Offset: 0x0007948B
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startExecutionTimestamp);
				return true;
			}
			return false;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0007B2A3 File Offset: 0x000794A3
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startNetworkServerTimestamp);
			}
			this._waitForReply = true;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0007B2BF File Offset: 0x000794BF
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				this._executionTime += ADP.TimerCurrent() - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0007B2EC File Offset: 0x000794EC
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				this._networkServerTime += ADP.TimerCurrent() - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0007B328 File Offset: 0x00079528
		internal void Reset()
		{
			this._buffersReceived = 0L;
			this._buffersSent = 0L;
			this._bytesReceived = 0L;
			this._bytesSent = 0L;
			this._connectionTime = 0L;
			this._cursorOpens = 0L;
			this._executionTime = 0L;
			this._iduCount = 0L;
			this._iduRows = 0L;
			this._networkServerTime = 0L;
			this._preparedExecs = 0L;
			this._prepares = 0L;
			this._selectCount = 0L;
			this._selectRows = 0L;
			this._serverRoundtrips = 0L;
			this._sumResultSets = 0L;
			this._transactions = 0L;
			this._unpreparedExecs = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
			this._startExecutionTimestamp = 0L;
			this._startNetworkServerTimestamp = 0L;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0007B3E3 File Offset: 0x000795E3
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0007B406 File Offset: 0x00079606
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0007B41E File Offset: 0x0007961E
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp)
			{
				this.SafeAdd(ref this._connectionTime, this._closeTimestamp - this._openTimestamp);
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x04001112 RID: 4370
		internal long _closeTimestamp;

		// Token: 0x04001113 RID: 4371
		internal long _openTimestamp;

		// Token: 0x04001114 RID: 4372
		internal long _startExecutionTimestamp;

		// Token: 0x04001115 RID: 4373
		internal long _startFetchTimestamp;

		// Token: 0x04001116 RID: 4374
		internal long _startNetworkServerTimestamp;

		// Token: 0x04001117 RID: 4375
		internal long _buffersReceived;

		// Token: 0x04001118 RID: 4376
		internal long _buffersSent;

		// Token: 0x04001119 RID: 4377
		internal long _bytesReceived;

		// Token: 0x0400111A RID: 4378
		internal long _bytesSent;

		// Token: 0x0400111B RID: 4379
		internal long _connectionTime;

		// Token: 0x0400111C RID: 4380
		internal long _cursorOpens;

		// Token: 0x0400111D RID: 4381
		internal long _executionTime;

		// Token: 0x0400111E RID: 4382
		internal long _iduCount;

		// Token: 0x0400111F RID: 4383
		internal long _iduRows;

		// Token: 0x04001120 RID: 4384
		internal long _networkServerTime;

		// Token: 0x04001121 RID: 4385
		internal long _preparedExecs;

		// Token: 0x04001122 RID: 4386
		internal long _prepares;

		// Token: 0x04001123 RID: 4387
		internal long _selectCount;

		// Token: 0x04001124 RID: 4388
		internal long _selectRows;

		// Token: 0x04001125 RID: 4389
		internal long _serverRoundtrips;

		// Token: 0x04001126 RID: 4390
		internal long _sumResultSets;

		// Token: 0x04001127 RID: 4391
		internal long _transactions;

		// Token: 0x04001128 RID: 4392
		internal long _unpreparedExecs;

		// Token: 0x04001129 RID: 4393
		private bool _waitForDoneAfterRow;

		// Token: 0x0400112A RID: 4394
		private bool _waitForReply;

		// Token: 0x0200022C RID: 556
		private sealed class StatisticsDictionary : Dictionary<object, object>, IDictionary, ICollection, IEnumerable
		{
			// Token: 0x06001AC6 RID: 6854 RVA: 0x0007B457 File Offset: 0x00079657
			public StatisticsDictionary(int capacity) : base(capacity)
			{
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0007B460 File Offset: 0x00079660
			ICollection IDictionary.Keys
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection result;
					if ((result = this._keys) == null)
					{
						result = (this._keys = new SqlStatistics.StatisticsDictionary.Collection(this, base.Keys));
					}
					return result;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0007B48C File Offset: 0x0007968C
			ICollection IDictionary.Values
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection result;
					if ((result = this._values) == null)
					{
						result = (this._values = new SqlStatistics.StatisticsDictionary.Collection(this, base.Values));
					}
					return result;
				}
			}

			// Token: 0x06001AC9 RID: 6857 RVA: 0x0007B4B8 File Offset: 0x000796B8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IDictionary)this).GetEnumerator();
			}

			// Token: 0x06001ACA RID: 6858 RVA: 0x0007B4C0 File Offset: 0x000796C0
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}

			// Token: 0x06001ACB RID: 6859 RVA: 0x0007B538 File Offset: 0x00079738
			private void CopyKeys(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Key, arrayIndex++);
				}
			}

			// Token: 0x06001ACC RID: 6860 RVA: 0x0007B59C File Offset: 0x0007979C
			private void CopyValues(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Value, arrayIndex++);
				}
			}

			// Token: 0x06001ACD RID: 6861 RVA: 0x0007B600 File Offset: 0x00079800
			private void ValidateCopyToArguments(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < base.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
			}

			// Token: 0x0400112B RID: 4395
			private SqlStatistics.StatisticsDictionary.Collection _keys;

			// Token: 0x0400112C RID: 4396
			private SqlStatistics.StatisticsDictionary.Collection _values;

			// Token: 0x0200022D RID: 557
			private sealed class Collection : ICollection, IEnumerable
			{
				// Token: 0x06001ACE RID: 6862 RVA: 0x0007B65E File Offset: 0x0007985E
				public Collection(SqlStatistics.StatisticsDictionary dictionary, ICollection collection)
				{
					this._dictionary = dictionary;
					this._collection = collection;
				}

				// Token: 0x17000506 RID: 1286
				// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0007B674 File Offset: 0x00079874
				int ICollection.Count
				{
					get
					{
						return this._collection.Count;
					}
				}

				// Token: 0x17000507 RID: 1287
				// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0007B681 File Offset: 0x00079881
				bool ICollection.IsSynchronized
				{
					get
					{
						return this._collection.IsSynchronized;
					}
				}

				// Token: 0x17000508 RID: 1288
				// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0007B68E File Offset: 0x0007988E
				object ICollection.SyncRoot
				{
					get
					{
						return this._collection.SyncRoot;
					}
				}

				// Token: 0x06001AD2 RID: 6866 RVA: 0x0007B69B File Offset: 0x0007989B
				void ICollection.CopyTo(Array array, int arrayIndex)
				{
					if (this._collection is Dictionary<object, object>.KeyCollection)
					{
						this._dictionary.CopyKeys(array, arrayIndex);
						return;
					}
					this._dictionary.CopyValues(array, arrayIndex);
				}

				// Token: 0x06001AD3 RID: 6867 RVA: 0x0007B6C5 File Offset: 0x000798C5
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._collection.GetEnumerator();
				}

				// Token: 0x0400112D RID: 4397
				private readonly SqlStatistics.StatisticsDictionary _dictionary;

				// Token: 0x0400112E RID: 4398
				private readonly ICollection _collection;
			}
		}
	}
}
