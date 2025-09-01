using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001FA RID: 506
	internal class SqlDependencyPerAppDomainDispatcher : MarshalByRefObject
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x00071AEC File Offset: 0x0006FCEC
		private SqlDependencyPerAppDomainDispatcher()
		{
			this._dependencyIdToDependencyHash = new Dictionary<string, SqlDependency>();
			this._notificationIdToDependenciesHash = new Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList>();
			this._commandHashToNotificationId = new Dictionary<string, string>();
			this._timeoutTimer = ADP.UnsafeCreateTimer(new TimerCallback(SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback), null, -1, -1);
			this.SubscribeToAppDomainUnload();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00003E32 File Offset: 0x00002032
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00071B4C File Offset: 0x0006FD4C
		internal void AddDependencyEntry(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				this._dependencyIdToDependencyHash.Add(dep.Id, dep);
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00071B98 File Offset: 0x0006FD98
		internal string AddCommandEntry(string commandHash, SqlDependency dep)
		{
			string text = string.Empty;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(dep.Id))
				{
					if (this._commandHashToNotificationId.TryGetValue(commandHash, out text))
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
						if (!this._notificationIdToDependenciesHash.TryGetValue(text, out dependencyList))
						{
							throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyCommandHashIsNotAssociatedWithNotification);
						}
						if (!dependencyList.Contains(dep))
						{
							dependencyList.Add(dep);
						}
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0};{1}", SqlDependency.AppDomainKey, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture));
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList2 = new SqlDependencyPerAppDomainDispatcher.DependencyList(commandHash);
						dependencyList2.Add(dep);
						this._commandHashToNotificationId.Add(commandHash, text);
						this._notificationIdToDependenciesHash.Add(text, dependencyList2);
					}
				}
			}
			return text;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00071C84 File Offset: 0x0006FE84
		internal void InvalidateCommandID(SqlNotification sqlNotification)
		{
			List<SqlDependency> list = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				list = this.LookupCommandEntryWithRemove(sqlNotification.Key);
				if (list != null)
				{
					foreach (SqlDependency sqlDependency in list)
					{
						this.LookupDependencyEntryWithRemove(sqlDependency.Id);
						this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
					}
				}
			}
			if (list != null)
			{
				foreach (SqlDependency sqlDependency2 in list)
				{
					try
					{
						sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(e);
					}
				}
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00071D90 File Offset: 0x0006FF90
		internal void InvalidateServer(string server, SqlNotification sqlNotification)
		{
			List<SqlDependency> list = new List<SqlDependency>();
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				foreach (KeyValuePair<string, SqlDependency> keyValuePair in this._dependencyIdToDependencyHash)
				{
					SqlDependency value = keyValuePair.Value;
					if (value.ContainsServer(server))
					{
						list.Add(value);
					}
				}
				foreach (SqlDependency sqlDependency in list)
				{
					this.LookupDependencyEntryWithRemove(sqlDependency.Id);
					this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
				}
			}
			foreach (SqlDependency sqlDependency2 in list)
			{
				try
				{
					sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e);
				}
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00071EE4 File Offset: 0x000700E4
		internal SqlDependency LookupDependencyEntry(string id)
		{
			if (id == null)
			{
				throw ADP.ArgumentNull("id");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw SQL.SqlDependencyIdMismatch();
			}
			SqlDependency result = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					result = this._dependencyIdToDependencyHash[id];
				}
			}
			return result;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00071F58 File Offset: 0x00070158
		private void LookupDependencyEntryWithRemove(string id)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					this._dependencyIdToDependencyHash.Remove(id);
					if (this._dependencyIdToDependencyHash.Count == 0)
					{
						this._timeoutTimer.Change(-1, -1);
						this._sqlDependencyTimeOutTimerStarted = false;
					}
				}
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00071FD0 File Offset: 0x000701D0
		private List<SqlDependency> LookupCommandEntryWithRemove(string notificationId)
		{
			SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._notificationIdToDependenciesHash.TryGetValue(notificationId, out dependencyList))
				{
					this._notificationIdToDependenciesHash.Remove(notificationId);
					this._commandHashToNotificationId.Remove(dependencyList.CommandHash);
				}
			}
			return dependencyList;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0007203C File Offset: 0x0007023C
		private void RemoveDependencyFromCommandToDependenciesHash(SqlDependency dependency)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher.DependencyList> keyValuePair in this._notificationIdToDependenciesHash)
				{
					SqlDependencyPerAppDomainDispatcher.DependencyList value = keyValuePair.Value;
					if (value.Remove(dependency) && value.Count == 0)
					{
						list.Add(keyValuePair.Key);
						list2.Add(keyValuePair.Value.CommandHash);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					this._notificationIdToDependenciesHash.Remove(list[i]);
					this._commandHashToNotificationId.Remove(list2[i]);
				}
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00072138 File Offset: 0x00070338
		internal void StartTimer(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (!this._sqlDependencyTimeOutTimerStarted)
				{
					this._timeoutTimer.Change(15000, 15000);
					this._nextTimeout = dep.ExpirationTime;
					this._sqlDependencyTimeOutTimerStarted = true;
				}
				else if (this._nextTimeout > dep.ExpirationTime)
				{
					this._nextTimeout = dep.ExpirationTime;
				}
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000721C4 File Offset: 0x000703C4
		private static void TimeoutTimerCallback(object state)
		{
			object instanceLock = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			SqlDependency[] array;
			lock (instanceLock)
			{
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count == 0)
				{
					return;
				}
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout > DateTime.UtcNow)
				{
					return;
				}
				array = new SqlDependency[SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count];
				SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Values.CopyTo(array, 0);
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = DateTime.MaxValue;
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].ExpirationTime <= utcNow)
				{
					try
					{
						array[i].Invalidate(SqlNotificationType.Change, SqlNotificationInfo.Error, SqlNotificationSource.Timeout);
						goto IL_E0;
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(e);
						goto IL_E0;
					}
					goto IL_C0;
				}
				goto IL_C0;
				IL_E0:
				i++;
				continue;
				IL_C0:
				if (array[i].ExpirationTime < dateTime)
				{
					dateTime = array[i].ExpirationTime;
				}
				array[i] = null;
				goto IL_E0;
			}
			instanceLock = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			lock (instanceLock)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Remove(array[j].Id);
					}
				}
				if (dateTime < SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout)
				{
					SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout = dateTime;
				}
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00007EED File Offset: 0x000060ED
		private void SubscribeToAppDomainUnload()
		{
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00072358 File Offset: 0x00070558
		// Note: this type is marked as 'beforefieldinit'.
		static SqlDependencyPerAppDomainDispatcher()
		{
		}

		// Token: 0x04000FC0 RID: 4032
		internal static readonly SqlDependencyPerAppDomainDispatcher SingletonInstance = new SqlDependencyPerAppDomainDispatcher();

		// Token: 0x04000FC1 RID: 4033
		internal object _instanceLock = new object();

		// Token: 0x04000FC2 RID: 4034
		private Dictionary<string, SqlDependency> _dependencyIdToDependencyHash;

		// Token: 0x04000FC3 RID: 4035
		private Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList> _notificationIdToDependenciesHash;

		// Token: 0x04000FC4 RID: 4036
		private Dictionary<string, string> _commandHashToNotificationId;

		// Token: 0x04000FC5 RID: 4037
		private bool _sqlDependencyTimeOutTimerStarted;

		// Token: 0x04000FC6 RID: 4038
		private DateTime _nextTimeout;

		// Token: 0x04000FC7 RID: 4039
		private Timer _timeoutTimer;

		// Token: 0x020001FB RID: 507
		private sealed class DependencyList : List<SqlDependency>
		{
			// Token: 0x060018AE RID: 6318 RVA: 0x00072364 File Offset: 0x00070564
			internal DependencyList(string commandHash)
			{
				this.CommandHash = commandHash;
			}

			// Token: 0x04000FC8 RID: 4040
			public readonly string CommandHash;
		}
	}
}
