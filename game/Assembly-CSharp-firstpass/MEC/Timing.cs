using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace MEC
{
	// Token: 0x020000A1 RID: 161
	public class Timing : MonoBehaviour
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0002A853 File Offset: 0x00028A53
		public static float LocalTime
		{
			get
			{
				return Timing.Instance.localTime;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0002A85F File Offset: 0x00028A5F
		public static float DeltaTime
		{
			get
			{
				return Timing.Instance.deltaTime;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600064E RID: 1614 RVA: 0x0002A86C File Offset: 0x00028A6C
		// (remove) Token: 0x0600064F RID: 1615 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		public static event Action OnPreExecute
		{
			[CompilerGenerated]
			add
			{
				Action action = Timing.OnPreExecute;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Timing.OnPreExecute, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = Timing.OnPreExecute;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Timing.OnPreExecute, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0002A8D3 File Offset: 0x00028AD3
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0002A8DA File Offset: 0x00028ADA
		public static Thread MainThread
		{
			[CompilerGenerated]
			get
			{
				return Timing.<MainThread>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				Timing.<MainThread>k__BackingField = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0002A8E4 File Offset: 0x00028AE4
		public static CoroutineHandle CurrentCoroutine
		{
			get
			{
				for (int i = 0; i < Timing.ActiveInstances.Length; i++)
				{
					if (Timing.ActiveInstances[i] != null && Timing.ActiveInstances[i].currentCoroutine.IsValid)
					{
						return Timing.ActiveInstances[i].currentCoroutine;
					}
				}
				return default(CoroutineHandle);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0002A93E File Offset: 0x00028B3E
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0002A946 File Offset: 0x00028B46
		public CoroutineHandle currentCoroutine
		{
			[CompilerGenerated]
			get
			{
				return this.<currentCoroutine>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<currentCoroutine>k__BackingField = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0002A950 File Offset: 0x00028B50
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0002A9CA File Offset: 0x00028BCA
		public static Timing Instance
		{
			get
			{
				if (Timing._instance == null || !Timing._instance.gameObject)
				{
					GameObject gameObject = GameObject.Find("Timing Controller");
					if (gameObject == null)
					{
						gameObject = new GameObject
						{
							name = "Timing Controller"
						};
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
					Timing._instance = (gameObject.GetComponent<Timing>() ?? gameObject.AddComponent<Timing>());
					Timing._instance.InitializeInstanceID();
				}
				return Timing._instance;
			}
			set
			{
				Timing._instance = value;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0002A9D2 File Offset: 0x00028BD2
		private void OnDestroy()
		{
			if (Timing._instance == this)
			{
				Timing._instance = null;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0002A9E8 File Offset: 0x00028BE8
		private void OnEnable()
		{
			if (Timing.MainThread == null)
			{
				Timing.MainThread = Thread.CurrentThread;
			}
			if (this._nextEditorUpdateProcessSlot > 0 || this._nextEditorSlowUpdateProcessSlot > 0)
			{
				this.OnEditorStart();
			}
			this.InitializeInstanceID();
			if (this._nextEndOfFrameProcessSlot > 0)
			{
				this.RunCoroutineSingletonOnInstance(this._EOFPumpWatcher(), "MEC_EOFPumpWatcher", SingletonBehavior.Abort);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002AA41 File Offset: 0x00028C41
		private void OnDisable()
		{
			if ((int)this._instanceID < Timing.ActiveInstances.Length)
			{
				Timing.ActiveInstances[(int)this._instanceID] = null;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0002AA60 File Offset: 0x00028C60
		private void InitializeInstanceID()
		{
			if (Timing.ActiveInstances[(int)this._instanceID] == null)
			{
				if (this._instanceID == 0)
				{
					this._instanceID += 1;
				}
				while (this._instanceID <= 16)
				{
					if (this._instanceID == 16)
					{
						UnityEngine.Object.Destroy(base.gameObject);
						throw new OverflowException("You are only allowed 15 different contexts for MEC to run inside at one time.");
					}
					if (Timing.ActiveInstances[(int)this._instanceID] == null)
					{
						Timing.ActiveInstances[(int)this._instanceID] = this;
						return;
					}
					this._instanceID += 1;
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002AAF8 File Offset: 0x00028CF8
		private void Update()
		{
			if (Timing.OnPreExecute != null)
			{
				Timing.OnPreExecute();
			}
			if (this._lastSlowUpdateTime + this.TimeBetweenSlowUpdateCalls < Time.realtimeSinceStartup && this._nextSlowUpdateProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex = new Timing.ProcessIndex
				{
					seg = Segment.SlowUpdate
				};
				if (this.UpdateTimeValues(processIndex.seg))
				{
					this._lastSlowUpdateProcessSlot = this._nextSlowUpdateProcessSlot;
				}
				processIndex.i = 0;
				while (processIndex.i < this._lastSlowUpdateProcessSlot)
				{
					try
					{
						if (!this.SlowUpdatePaused[processIndex.i] && !this.SlowUpdateHeld[processIndex.i] && this.SlowUpdateProcesses[processIndex.i] != null && this.localTime >= this.SlowUpdateProcesses[processIndex.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex);
							}
							if (!this.SlowUpdateProcesses[processIndex.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
								}
							}
							else if (this.SlowUpdateProcesses[processIndex.i] != null && float.IsNaN(this.SlowUpdateProcesses[processIndex.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.SlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
									Timing.ReplacementFunction = null;
								}
								processIndex.i--;
							}
							DebugInfoType profilerDebugAmount = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogException(ex);
						if (ex is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.SlowUpdate);");
						}
					}
					processIndex.i++;
				}
			}
			if (this._nextRealtimeUpdateProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex2 = new Timing.ProcessIndex
				{
					seg = Segment.RealtimeUpdate
				};
				if (this.UpdateTimeValues(processIndex2.seg))
				{
					this._lastRealtimeUpdateProcessSlot = this._nextRealtimeUpdateProcessSlot;
				}
				processIndex2.i = 0;
				while (processIndex2.i < this._lastRealtimeUpdateProcessSlot)
				{
					try
					{
						if (!this.RealtimeUpdatePaused[processIndex2.i] && !this.RealtimeUpdateHeld[processIndex2.i] && this.RealtimeUpdateProcesses[processIndex2.i] != null && this.localTime >= this.RealtimeUpdateProcesses[processIndex2.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex2];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex2);
							}
							if (!this.RealtimeUpdateProcesses[processIndex2.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex2))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex2]);
								}
							}
							else if (this.RealtimeUpdateProcesses[processIndex2.i] != null && float.IsNaN(this.RealtimeUpdateProcesses[processIndex2.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.RealtimeUpdateProcesses[processIndex2.i] = Timing.ReplacementFunction(this.RealtimeUpdateProcesses[processIndex2.i], this._indexToHandle[processIndex2]);
									Timing.ReplacementFunction = null;
								}
								processIndex2.i--;
							}
							DebugInfoType profilerDebugAmount2 = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex2)
					{
						UnityEngine.Debug.LogException(ex2);
						if (ex2 is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.RealtimeUpdate);");
						}
					}
					processIndex2.i++;
				}
			}
			if (this._nextUpdateProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex3 = new Timing.ProcessIndex
				{
					seg = Segment.Update
				};
				if (this.UpdateTimeValues(processIndex3.seg))
				{
					this._lastUpdateProcessSlot = this._nextUpdateProcessSlot;
				}
				processIndex3.i = 0;
				while (processIndex3.i < this._lastUpdateProcessSlot)
				{
					try
					{
						if (!this.UpdatePaused[processIndex3.i] && !this.UpdateHeld[processIndex3.i] && this.UpdateProcesses[processIndex3.i] != null && this.localTime >= this.UpdateProcesses[processIndex3.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex3];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex3);
							}
							if (!this.UpdateProcesses[processIndex3.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex3))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex3]);
								}
							}
							else if (this.UpdateProcesses[processIndex3.i] != null && float.IsNaN(this.UpdateProcesses[processIndex3.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.UpdateProcesses[processIndex3.i] = Timing.ReplacementFunction(this.UpdateProcesses[processIndex3.i], this._indexToHandle[processIndex3]);
									Timing.ReplacementFunction = null;
								}
								processIndex3.i--;
							}
							DebugInfoType profilerDebugAmount3 = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex3)
					{
						UnityEngine.Debug.LogException(ex3);
						if (ex3 is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.Update);");
						}
					}
					processIndex3.i++;
				}
			}
			if (this.AutoTriggerManualTimeframe)
			{
				this.TriggerManualTimeframeUpdate();
			}
			else
			{
				ushort num = this._framesSinceUpdate + 1;
				this._framesSinceUpdate = num;
				if (num > 64)
				{
					this._framesSinceUpdate = 0;
					DebugInfoType profilerDebugAmount4 = this.ProfilerDebugAmount;
					this.RemoveUnused();
					DebugInfoType profilerDebugAmount5 = this.ProfilerDebugAmount;
				}
			}
			this.currentCoroutine = default(CoroutineHandle);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002B0CC File Offset: 0x000292CC
		private void FixedUpdate()
		{
			if (Timing.OnPreExecute != null)
			{
				Timing.OnPreExecute();
			}
			if (this._nextFixedUpdateProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex = new Timing.ProcessIndex
				{
					seg = Segment.FixedUpdate
				};
				if (this.UpdateTimeValues(processIndex.seg))
				{
					this._lastFixedUpdateProcessSlot = this._nextFixedUpdateProcessSlot;
				}
				processIndex.i = 0;
				while (processIndex.i < this._lastFixedUpdateProcessSlot)
				{
					try
					{
						if (!this.FixedUpdatePaused[processIndex.i] && !this.FixedUpdateHeld[processIndex.i] && this.FixedUpdateProcesses[processIndex.i] != null && this.localTime >= this.FixedUpdateProcesses[processIndex.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex);
							}
							if (!this.FixedUpdateProcesses[processIndex.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
								}
							}
							else if (this.FixedUpdateProcesses[processIndex.i] != null && float.IsNaN(this.FixedUpdateProcesses[processIndex.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.FixedUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
									Timing.ReplacementFunction = null;
								}
								processIndex.i--;
							}
							DebugInfoType profilerDebugAmount = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogException(ex);
						if (ex is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.FixedUpdate);");
						}
					}
					processIndex.i++;
				}
				this.currentCoroutine = default(CoroutineHandle);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0002B2C0 File Offset: 0x000294C0
		private void LateUpdate()
		{
			if (Timing.OnPreExecute != null)
			{
				Timing.OnPreExecute();
			}
			if (this._nextLateUpdateProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex = new Timing.ProcessIndex
				{
					seg = Segment.LateUpdate
				};
				if (this.UpdateTimeValues(processIndex.seg))
				{
					this._lastLateUpdateProcessSlot = this._nextLateUpdateProcessSlot;
				}
				processIndex.i = 0;
				while (processIndex.i < this._lastLateUpdateProcessSlot)
				{
					try
					{
						if (!this.LateUpdatePaused[processIndex.i] && !this.LateUpdateHeld[processIndex.i] && this.LateUpdateProcesses[processIndex.i] != null && this.localTime >= this.LateUpdateProcesses[processIndex.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex);
							}
							if (!this.LateUpdateProcesses[processIndex.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
								}
							}
							else if (this.LateUpdateProcesses[processIndex.i] != null && float.IsNaN(this.LateUpdateProcesses[processIndex.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.LateUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
									Timing.ReplacementFunction = null;
								}
								processIndex.i--;
							}
							DebugInfoType profilerDebugAmount = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogException(ex);
						if (ex is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.LateUpdate);");
						}
					}
					processIndex.i++;
				}
				this.currentCoroutine = default(CoroutineHandle);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0002B4B4 File Offset: 0x000296B4
		public void TriggerManualTimeframeUpdate()
		{
			if (Timing.OnPreExecute != null)
			{
				Timing.OnPreExecute();
			}
			if (this._nextManualTimeframeProcessSlot > 0)
			{
				Timing.ProcessIndex processIndex = new Timing.ProcessIndex
				{
					seg = Segment.ManualTimeframe
				};
				if (this.UpdateTimeValues(processIndex.seg))
				{
					this._lastManualTimeframeProcessSlot = this._nextManualTimeframeProcessSlot;
				}
				processIndex.i = 0;
				while (processIndex.i < this._lastManualTimeframeProcessSlot)
				{
					try
					{
						if (!this.ManualTimeframePaused[processIndex.i] && !this.ManualTimeframeHeld[processIndex.i] && this.ManualTimeframeProcesses[processIndex.i] != null && this.localTime >= this.ManualTimeframeProcesses[processIndex.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex);
							}
							if (!this.ManualTimeframeProcesses[processIndex.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
								}
							}
							else if (this.ManualTimeframeProcesses[processIndex.i] != null && float.IsNaN(this.ManualTimeframeProcesses[processIndex.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.ManualTimeframeProcesses[processIndex.i] = Timing.ReplacementFunction(this.ManualTimeframeProcesses[processIndex.i], this._indexToHandle[processIndex]);
									Timing.ReplacementFunction = null;
								}
								processIndex.i--;
							}
							DebugInfoType profilerDebugAmount = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogException(ex);
						if (ex is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.ManualTimeframe);");
						}
					}
					processIndex.i++;
				}
			}
			ushort num = this._framesSinceUpdate + 1;
			this._framesSinceUpdate = num;
			if (num > 64)
			{
				this._framesSinceUpdate = 0;
				DebugInfoType profilerDebugAmount2 = this.ProfilerDebugAmount;
				this.RemoveUnused();
				DebugInfoType profilerDebugAmount3 = this.ProfilerDebugAmount;
			}
			this.currentCoroutine = default(CoroutineHandle);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0002B6D8 File Offset: 0x000298D8
		private bool OnEditorStart()
		{
			return false;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0002B6DB File Offset: 0x000298DB
		private IEnumerator<float> _EOFPumpWatcher()
		{
			while (this._nextEndOfFrameProcessSlot > 0)
			{
				if (!this._EOFPumpRan)
				{
					base.StartCoroutine(this._EOFPump());
				}
				this._EOFPumpRan = false;
				yield return float.NegativeInfinity;
			}
			this._EOFPumpRan = false;
			yield break;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002B6EA File Offset: 0x000298EA
		private IEnumerator _EOFPump()
		{
			while (this._nextEndOfFrameProcessSlot > 0)
			{
				yield return Timing.EofWaitObject;
				if (Timing.OnPreExecute != null)
				{
					Timing.OnPreExecute();
				}
				Timing.ProcessIndex processIndex = new Timing.ProcessIndex
				{
					seg = Segment.EndOfFrame
				};
				this._EOFPumpRan = true;
				if (this.UpdateTimeValues(processIndex.seg))
				{
					this._lastEndOfFrameProcessSlot = this._nextEndOfFrameProcessSlot;
				}
				processIndex.i = 0;
				while (processIndex.i < this._lastEndOfFrameProcessSlot)
				{
					try
					{
						if (!this.EndOfFramePaused[processIndex.i] && !this.EndOfFrameHeld[processIndex.i] && this.EndOfFrameProcesses[processIndex.i] != null && this.localTime >= this.EndOfFrameProcesses[processIndex.i].Current)
						{
							this.currentCoroutine = this._indexToHandle[processIndex];
							if (this.ProfilerDebugAmount != DebugInfoType.None)
							{
								this._indexToHandle.ContainsKey(processIndex);
							}
							if (!this.EndOfFrameProcesses[processIndex.i].MoveNext())
							{
								if (this._indexToHandle.ContainsKey(processIndex))
								{
									this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
								}
							}
							else if (this.EndOfFrameProcesses[processIndex.i] != null && float.IsNaN(this.EndOfFrameProcesses[processIndex.i].Current))
							{
								if (Timing.ReplacementFunction != null)
								{
									this.EndOfFrameProcesses[processIndex.i] = Timing.ReplacementFunction(this.EndOfFrameProcesses[processIndex.i], this._indexToHandle[processIndex]);
									Timing.ReplacementFunction = null;
								}
								processIndex.i--;
							}
							DebugInfoType profilerDebugAmount = this.ProfilerDebugAmount;
						}
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogException(ex);
						if (ex is MissingReferenceException)
						{
							UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.EndOfFrame);");
						}
					}
					processIndex.i++;
				}
			}
			this.currentCoroutine = default(CoroutineHandle);
			yield break;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0002B6FC File Offset: 0x000298FC
		private void RemoveUnused()
		{
			foreach (KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair in this._waitingTriggers)
			{
				if (keyValuePair.Value.Count == 0)
				{
					Dictionary<CoroutineHandle, HashSet<CoroutineHandle>> waitingTriggers = this._waitingTriggers;
					Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator;
					keyValuePair = enumerator.Current;
					waitingTriggers.Remove(keyValuePair.Key);
					enumerator = this._waitingTriggers.GetEnumerator();
				}
				else
				{
					Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex = this._handleToIndex;
					Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator;
					keyValuePair = enumerator.Current;
					if (handleToIndex.ContainsKey(keyValuePair.Key))
					{
						Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex2 = this._handleToIndex;
						keyValuePair = enumerator.Current;
						if (this.CoindexIsNull(handleToIndex2[keyValuePair.Key]))
						{
							keyValuePair = enumerator.Current;
							this.CloseWaitingProcess(keyValuePair.Key);
							enumerator = this._waitingTriggers.GetEnumerator();
						}
					}
				}
			}
			Timing.ProcessIndex processIndex;
			Timing.ProcessIndex processIndex2;
			processIndex.seg = (processIndex2.seg = Segment.Update);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextUpdateProcessSlot)
			{
				if (this.UpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.UpdateProcesses[processIndex2.i] = this.UpdateProcesses[processIndex.i];
						this.UpdatePaused[processIndex2.i] = this.UpdatePaused[processIndex.i];
						this.UpdateHeld[processIndex2.i] = this.UpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextUpdateProcessSlot)
			{
				this.UpdateProcesses[processIndex.i] = null;
				this.UpdatePaused[processIndex.i] = false;
				this.UpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.UpdateCoroutines = (this._nextUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.FixedUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextFixedUpdateProcessSlot)
			{
				if (this.FixedUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.FixedUpdateProcesses[processIndex2.i] = this.FixedUpdateProcesses[processIndex.i];
						this.FixedUpdatePaused[processIndex2.i] = this.FixedUpdatePaused[processIndex.i];
						this.FixedUpdateHeld[processIndex2.i] = this.FixedUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextFixedUpdateProcessSlot)
			{
				this.FixedUpdateProcesses[processIndex.i] = null;
				this.FixedUpdatePaused[processIndex.i] = false;
				this.FixedUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.FixedUpdateCoroutines = (this._nextFixedUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.LateUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextLateUpdateProcessSlot)
			{
				if (this.LateUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.LateUpdateProcesses[processIndex2.i] = this.LateUpdateProcesses[processIndex.i];
						this.LateUpdatePaused[processIndex2.i] = this.LateUpdatePaused[processIndex.i];
						this.LateUpdateHeld[processIndex2.i] = this.LateUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextLateUpdateProcessSlot)
			{
				this.LateUpdateProcesses[processIndex.i] = null;
				this.LateUpdatePaused[processIndex.i] = false;
				this.LateUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.LateUpdateCoroutines = (this._nextLateUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.SlowUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextSlowUpdateProcessSlot)
			{
				if (this.SlowUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.SlowUpdateProcesses[processIndex2.i] = this.SlowUpdateProcesses[processIndex.i];
						this.SlowUpdatePaused[processIndex2.i] = this.SlowUpdatePaused[processIndex.i];
						this.SlowUpdateHeld[processIndex2.i] = this.SlowUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextSlowUpdateProcessSlot)
			{
				this.SlowUpdateProcesses[processIndex.i] = null;
				this.SlowUpdatePaused[processIndex.i] = false;
				this.SlowUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.SlowUpdateCoroutines = (this._nextSlowUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.RealtimeUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextRealtimeUpdateProcessSlot)
			{
				if (this.RealtimeUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.RealtimeUpdateProcesses[processIndex2.i] = this.RealtimeUpdateProcesses[processIndex.i];
						this.RealtimeUpdatePaused[processIndex2.i] = this.RealtimeUpdatePaused[processIndex.i];
						this.RealtimeUpdateHeld[processIndex2.i] = this.RealtimeUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextRealtimeUpdateProcessSlot)
			{
				this.RealtimeUpdateProcesses[processIndex.i] = null;
				this.RealtimeUpdatePaused[processIndex.i] = false;
				this.RealtimeUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.RealtimeUpdateCoroutines = (this._nextRealtimeUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.EndOfFrame);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextEndOfFrameProcessSlot)
			{
				if (this.EndOfFrameProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.EndOfFrameProcesses[processIndex2.i] = this.EndOfFrameProcesses[processIndex.i];
						this.EndOfFramePaused[processIndex2.i] = this.EndOfFramePaused[processIndex.i];
						this.EndOfFrameHeld[processIndex2.i] = this.EndOfFrameHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextEndOfFrameProcessSlot)
			{
				this.EndOfFrameProcesses[processIndex.i] = null;
				this.EndOfFramePaused[processIndex.i] = false;
				this.EndOfFrameHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.EndOfFrameCoroutines = (this._nextEndOfFrameProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.ManualTimeframe);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextManualTimeframeProcessSlot)
			{
				if (this.ManualTimeframeProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.ManualTimeframeProcesses[processIndex2.i] = this.ManualTimeframeProcesses[processIndex.i];
						this.ManualTimeframePaused[processIndex2.i] = this.ManualTimeframePaused[processIndex.i];
						this.ManualTimeframeHeld[processIndex2.i] = this.ManualTimeframeHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextManualTimeframeProcessSlot)
			{
				this.ManualTimeframeProcesses[processIndex.i] = null;
				this.ManualTimeframePaused[processIndex.i] = false;
				this.ManualTimeframeHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.ManualTimeframeCoroutines = (this._nextManualTimeframeProcessSlot = processIndex2.i);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0002C5A4 File Offset: 0x0002A7A4
		private void EditorRemoveUnused()
		{
			Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator = this._waitingTriggers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex = this._handleToIndex;
				KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair = enumerator.Current;
				if (handleToIndex.ContainsKey(keyValuePair.Key))
				{
					Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex2 = this._handleToIndex;
					keyValuePair = enumerator.Current;
					if (this.CoindexIsNull(handleToIndex2[keyValuePair.Key]))
					{
						keyValuePair = enumerator.Current;
						this.CloseWaitingProcess(keyValuePair.Key);
						enumerator = this._waitingTriggers.GetEnumerator();
					}
				}
			}
			Timing.ProcessIndex processIndex;
			Timing.ProcessIndex processIndex2;
			processIndex.seg = (processIndex2.seg = Segment.EditorUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextEditorUpdateProcessSlot)
			{
				if (this.EditorUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.EditorUpdateProcesses[processIndex2.i] = this.EditorUpdateProcesses[processIndex.i];
						this.EditorUpdatePaused[processIndex2.i] = this.EditorUpdatePaused[processIndex.i];
						this.EditorUpdateHeld[processIndex2.i] = this.EditorUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextEditorUpdateProcessSlot)
			{
				this.EditorUpdateProcesses[processIndex.i] = null;
				this.EditorUpdatePaused[processIndex.i] = false;
				this.EditorUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.EditorUpdateCoroutines = (this._nextEditorUpdateProcessSlot = processIndex2.i);
			processIndex.seg = (processIndex2.seg = Segment.EditorSlowUpdate);
			processIndex.i = (processIndex2.i = 0);
			while (processIndex.i < this._nextEditorSlowUpdateProcessSlot)
			{
				if (this.EditorSlowUpdateProcesses[processIndex.i] != null)
				{
					if (processIndex.i != processIndex2.i)
					{
						this.EditorSlowUpdateProcesses[processIndex2.i] = this.EditorSlowUpdateProcesses[processIndex.i];
						this.EditorUpdatePaused[processIndex2.i] = this.EditorUpdatePaused[processIndex.i];
						this.EditorUpdateHeld[processIndex2.i] = this.EditorUpdateHeld[processIndex.i];
						if (this._indexToHandle.ContainsKey(processIndex2))
						{
							this.RemoveGraffiti(this._indexToHandle[processIndex2]);
							this._handleToIndex.Remove(this._indexToHandle[processIndex2]);
							this._indexToHandle.Remove(processIndex2);
						}
						this._handleToIndex[this._indexToHandle[processIndex]] = processIndex2;
						this._indexToHandle.Add(processIndex2, this._indexToHandle[processIndex]);
						this._indexToHandle.Remove(processIndex);
					}
					processIndex2.i++;
				}
				processIndex.i++;
			}
			processIndex.i = processIndex2.i;
			while (processIndex.i < this._nextEditorSlowUpdateProcessSlot)
			{
				this.EditorSlowUpdateProcesses[processIndex.i] = null;
				this.EditorSlowUpdatePaused[processIndex.i] = false;
				this.EditorSlowUpdateHeld[processIndex.i] = false;
				if (this._indexToHandle.ContainsKey(processIndex))
				{
					this.RemoveGraffiti(this._indexToHandle[processIndex]);
					this._handleToIndex.Remove(this._indexToHandle[processIndex]);
					this._indexToHandle.Remove(processIndex);
				}
				processIndex.i++;
			}
			this.EditorSlowUpdateCoroutines = (this._nextEditorSlowUpdateProcessSlot = processIndex2.i);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0002CA20 File Offset: 0x0002AC20
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, GameObject gameObj)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, int layer)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002CB24 File Offset: 0x0002AD24
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, GameObject gameObj, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002CB74 File Offset: 0x0002AD74
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, int layer, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0002CBEC File Offset: 0x0002ADEC
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment, GameObject gameObj)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002CC3C File Offset: 0x0002AE3C
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment, int layer)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0002CC78 File Offset: 0x0002AE78
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public static CoroutineHandle RunCoroutine(IEnumerator<float> coroutine, Segment segment, int layer, string tag)
		{
			if (coroutine != null)
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002CD40 File Offset: 0x0002AF40
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0002CD74 File Offset: 0x0002AF74
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, GameObject gameObj)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002CDBC File Offset: 0x0002AFBC
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, int layer)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002CDF0 File Offset: 0x0002AFF0
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002CE24 File Offset: 0x0002B024
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, GameObject gameObj, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002CE6C File Offset: 0x0002B06C
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, int layer, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, 0, false, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002CED4 File Offset: 0x0002B0D4
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment, GameObject gameObj)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002CF1C File Offset: 0x0002B11C
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment, int layer)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0002CF50 File Offset: 0x0002B150
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0002CF84 File Offset: 0x0002B184
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, (gameObj == null) ? 0 : gameObj.GetInstanceID(), gameObj != null, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		public CoroutineHandle RunCoroutineOnInstance(IEnumerator<float> coroutine, Segment segment, int layer, string tag)
		{
			if (coroutine != null)
			{
				return this.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0002D004 File Offset: 0x0002B204
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, CoroutineHandle handle, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(handle);
			}
			else if (Timing.IsRunning(handle))
			{
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
					return handle;
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, handle, false);
					return coroutineHandle;
				}
				case SingletonBehavior.AbortAndUnpause:
					Timing.ResumeCoroutines(handle);
					return handle;
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002D09D File Offset: 0x0002B29D
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return Timing.RunCoroutine(coroutine);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, int layer, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(layer);
			}
			else if (Timing.Instance._layeredProcesses.ContainsKey(layer))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					Timing._instance.ResumeCoroutinesOnInstance(Timing._instance._layeredProcesses[layer]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._layeredProcesses[layer].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, Timing._instance._layeredProcesses[layer], false);
					return coroutineHandle;
				}
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(tag);
			}
			else if (Timing.Instance._taggedProcesses.ContainsKey(tag))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					Timing._instance.ResumeCoroutinesOnInstance(Timing._instance._taggedProcesses[tag]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._taggedProcesses[tag].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, Timing._instance._taggedProcesses[tag], false);
					return coroutineHandle;
				}
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002D2C9 File Offset: 0x0002B4C9
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return Timing.RunCoroutineSingleton(coroutine, tag, behaviorOnCollision);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(layer, tag);
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			if (!Timing.Instance._taggedProcesses.ContainsKey(tag) || !Timing.Instance._layeredProcesses.ContainsKey(layer))
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				Timing.ResumeCoroutines(layer, tag);
			}
			if (behaviorOnCollision == SingletonBehavior.Abort || behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._taggedProcesses[tag].GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (Timing._instance._processLayers.ContainsKey(enumerator.Current) && Timing._instance._processLayers[enumerator.Current] == layer)
					{
						return enumerator.Current;
					}
				}
			}
			if (behaviorOnCollision == SingletonBehavior.Wait)
			{
				List<CoroutineHandle> list = new List<CoroutineHandle>();
				HashSet<CoroutineHandle>.Enumerator enumerator2 = Timing.Instance._taggedProcesses[tag].GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (Timing.Instance._processLayers.ContainsKey(enumerator2.Current) && Timing.Instance._processLayers[enumerator2.Current] == layer)
					{
						list.Add(enumerator2.Current);
					}
				}
				if (list.Count > 0)
				{
					CoroutineHandle coroutineHandle = Timing._instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(Timing._instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, list, false);
					return coroutineHandle;
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, CoroutineHandle handle, Segment segment, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(handle);
			}
			else if (Timing.IsRunning(handle))
			{
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
					return handle;
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, handle, false);
					return coroutineHandle;
				}
				case SingletonBehavior.AbortAndUnpause:
					Timing.ResumeCoroutines(handle);
					return handle;
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, null, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002D539 File Offset: 0x0002B739
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, segment, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return Timing.RunCoroutine(coroutine, segment);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002D55C File Offset: 0x0002B75C
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, Segment segment, int layer, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(layer);
			}
			else if (Timing.Instance._layeredProcesses.ContainsKey(layer))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					Timing._instance.ResumeCoroutinesOnInstance(Timing._instance._layeredProcesses[layer]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._layeredProcesses[layer].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, Timing._instance._layeredProcesses[layer], false);
					return coroutineHandle;
				}
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0002D664 File Offset: 0x0002B864
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, Segment segment, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(tag);
			}
			else if (Timing.Instance._taggedProcesses.ContainsKey(tag))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					Timing._instance.ResumeCoroutinesOnInstance(Timing._instance._taggedProcesses[tag]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._taggedProcesses[tag].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, Timing._instance._taggedProcesses[tag], false);
					return coroutineHandle;
				}
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002D769 File Offset: 0x0002B969
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return Timing.RunCoroutineSingleton(coroutine, segment, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return Timing.RunCoroutineSingleton(coroutine, segment, tag, behaviorOnCollision);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0002D790 File Offset: 0x0002B990
		public static CoroutineHandle RunCoroutineSingleton(IEnumerator<float> coroutine, Segment segment, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				Timing.KillCoroutines(layer, tag);
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			if (!Timing.Instance._taggedProcesses.ContainsKey(tag) || !Timing.Instance._layeredProcesses.ContainsKey(layer))
			{
				return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
			}
			if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				Timing.ResumeCoroutines(layer, tag);
			}
			if (behaviorOnCollision == SingletonBehavior.Abort || behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Instance._taggedProcesses[tag].GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (Timing._instance._processLayers.ContainsKey(enumerator.Current) && Timing._instance._processLayers[enumerator.Current] == layer)
					{
						return enumerator.Current;
					}
				}
			}
			else if (behaviorOnCollision == SingletonBehavior.Wait)
			{
				List<CoroutineHandle> list = new List<CoroutineHandle>();
				HashSet<CoroutineHandle>.Enumerator enumerator2 = Timing.Instance._taggedProcesses[tag].GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (Timing._instance._processLayers.ContainsKey(enumerator2.Current) && Timing._instance._processLayers[enumerator2.Current] == layer)
					{
						list.Add(enumerator2.Current);
					}
				}
				if (list.Count > 0)
				{
					CoroutineHandle coroutineHandle = Timing._instance.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(Timing._instance._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, list, false);
					return coroutineHandle;
				}
			}
			return Timing.Instance.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(Timing.Instance._instanceID), true);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002D950 File Offset: 0x0002BB50
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, CoroutineHandle handle, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(handle);
			}
			else if (this._handleToIndex.ContainsKey(handle) && !this.CoindexIsNull(this._handleToIndex[handle]))
			{
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
					return handle;
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, handle, false);
					return coroutineHandle;
				}
				case SingletonBehavior.AbortAndUnpause:
					this.ResumeCoroutinesOnInstance(handle);
					return handle;
				}
			}
			return this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, null, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002D9F5 File Offset: 0x0002BBF5
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return this.RunCoroutineSingletonOnInstance(coroutine, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return this.RunCoroutineOnInstance(coroutine);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002DA18 File Offset: 0x0002BC18
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, int layer, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(layer);
			}
			else if (this._layeredProcesses.ContainsKey(layer))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					this.ResumeCoroutinesOnInstance(this._layeredProcesses[layer]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, this._layeredProcesses[layer], false);
					return coroutineHandle;
				}
				}
			}
			return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, null, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(tag);
			}
			else if (this._taggedProcesses.ContainsKey(tag))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					this.ResumeCoroutinesOnInstance(this._taggedProcesses[tag]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, this._taggedProcesses[tag], false);
					return coroutineHandle;
				}
				}
			}
			return this.RunCoroutineInternal(coroutine, Segment.Update, 0, false, tag, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002DBDE File Offset: 0x0002BDDE
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return this.RunCoroutineSingletonOnInstance(coroutine, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return this.RunCoroutineSingletonOnInstance(coroutine, tag, behaviorOnCollision);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002DC04 File Offset: 0x0002BE04
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(layer, tag);
				return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
			{
				return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				this.ResumeCoroutinesOnInstance(layer, tag);
			}
			if (behaviorOnCollision == SingletonBehavior.Abort || behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer)
					{
						return enumerator.Current;
					}
				}
			}
			if (behaviorOnCollision == SingletonBehavior.Wait)
			{
				List<CoroutineHandle> list = new List<CoroutineHandle>();
				HashSet<CoroutineHandle>.Enumerator enumerator2 = this._taggedProcesses[tag].GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (this._processLayers.ContainsKey(enumerator2.Current) && this._processLayers[enumerator2.Current] == layer)
					{
						list.Add(enumerator2.Current);
					}
				}
				if (list.Count > 0)
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, list, false);
					return coroutineHandle;
				}
			}
			return this.RunCoroutineInternal(coroutine, Segment.Update, layer, true, tag, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0002DD7E File Offset: 0x0002BF7E
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return this.RunCoroutineSingletonOnInstance(coroutine, segment, gameObj.GetInstanceID(), behaviorOnCollision);
			}
			return this.RunCoroutineOnInstance(coroutine, segment);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, Segment segment, int layer, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(layer);
			}
			else if (this._layeredProcesses.ContainsKey(layer))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					this.ResumeCoroutinesOnInstance(this._layeredProcesses[layer]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, this._layeredProcesses[layer], false);
					return coroutineHandle;
				}
				}
			}
			return this.RunCoroutineInternal(coroutine, segment, layer, true, null, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0002DE8C File Offset: 0x0002C08C
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, Segment segment, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(tag);
			}
			else if (this._taggedProcesses.ContainsKey(tag))
			{
				if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
				{
					this.ResumeCoroutinesOnInstance(this._taggedProcesses[tag]);
				}
				switch (behaviorOnCollision)
				{
				case SingletonBehavior.Abort:
				case SingletonBehavior.AbortAndUnpause:
				{
					HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (Timing.IsRunning(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
					break;
				}
				case SingletonBehavior.Wait:
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, this._taggedProcesses[tag], false);
					return coroutineHandle;
				}
				}
			}
			return this.RunCoroutineInternal(coroutine, segment, 0, false, tag, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002DF71 File Offset: 0x0002C171
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, Segment segment, GameObject gameObj, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (!(gameObj == null))
			{
				return this.RunCoroutineSingletonOnInstance(coroutine, segment, gameObj.GetInstanceID(), tag, behaviorOnCollision);
			}
			return this.RunCoroutineSingletonOnInstance(coroutine, segment, tag, behaviorOnCollision);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002DF9C File Offset: 0x0002C19C
		public CoroutineHandle RunCoroutineSingletonOnInstance(IEnumerator<float> coroutine, Segment segment, int layer, string tag, SingletonBehavior behaviorOnCollision)
		{
			if (coroutine == null)
			{
				return default(CoroutineHandle);
			}
			if (behaviorOnCollision == SingletonBehavior.Overwrite)
			{
				this.KillCoroutinesOnInstance(layer, tag);
				return this.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
			{
				return this.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(this._instanceID), true);
			}
			if (behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				this.ResumeCoroutinesOnInstance(layer, tag);
			}
			if (behaviorOnCollision == SingletonBehavior.Abort || behaviorOnCollision == SingletonBehavior.AbortAndUnpause)
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer)
					{
						return enumerator.Current;
					}
				}
			}
			else if (behaviorOnCollision == SingletonBehavior.Wait)
			{
				List<CoroutineHandle> list = new List<CoroutineHandle>();
				HashSet<CoroutineHandle>.Enumerator enumerator2 = this._taggedProcesses[tag].GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (this._processLayers.ContainsKey(enumerator2.Current) && this._processLayers[enumerator2.Current] == layer)
					{
						list.Add(enumerator2.Current);
					}
				}
				if (list.Count > 0)
				{
					CoroutineHandle coroutineHandle = this.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(this._instanceID), false);
					Timing.WaitForOtherHandles(coroutineHandle, list, false);
					return coroutineHandle;
				}
			}
			return this.RunCoroutineInternal(coroutine, segment, layer, true, tag, new CoroutineHandle(this._instanceID), true);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002E124 File Offset: 0x0002C324
		private CoroutineHandle RunCoroutineInternal(IEnumerator<float> coroutine, Segment segment, int layer, bool layerHasValue, string tag, CoroutineHandle handle, bool prewarm)
		{
			Timing.ProcessIndex processIndex = new Timing.ProcessIndex
			{
				seg = segment
			};
			if (this._handleToIndex.ContainsKey(handle))
			{
				this._indexToHandle.Remove(this._handleToIndex[handle]);
				this._handleToIndex.Remove(handle);
			}
			float num = this.localTime;
			float num2 = this.deltaTime;
			CoroutineHandle currentCoroutine = this.currentCoroutine;
			this.currentCoroutine = handle;
			try
			{
				switch (segment)
				{
				case Segment.Update:
				{
					if (this._nextUpdateProcessSlot >= this.UpdateProcesses.Length)
					{
						IEnumerator<float>[] updateProcesses = this.UpdateProcesses;
						bool[] updatePaused = this.UpdatePaused;
						bool[] updateHeld = this.UpdateHeld;
						ushort num3 = (ushort)this.UpdateProcesses.Length;
						ushort num4 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.UpdateProcesses = new IEnumerator<float>[(int)(num3 + num4 * expansions)];
						this.UpdatePaused = new bool[this.UpdateProcesses.Length];
						this.UpdateHeld = new bool[this.UpdateProcesses.Length];
						for (int i = 0; i < updateProcesses.Length; i++)
						{
							this.UpdateProcesses[i] = updateProcesses[i];
							this.UpdatePaused[i] = updatePaused[i];
							this.UpdateHeld[i] = updateHeld[i];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastUpdateProcessSlot = this._nextUpdateProcessSlot;
					}
					int num5 = this._nextUpdateProcessSlot;
					this._nextUpdateProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.UpdateProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					while (prewarm)
					{
						if (!this.UpdateProcesses[processIndex.i].MoveNext())
						{
							if (this._indexToHandle.ContainsKey(processIndex))
							{
								this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
							}
							prewarm = false;
						}
						else if (this.UpdateProcesses[processIndex.i] != null && float.IsNaN(this.UpdateProcesses[processIndex.i].Current))
						{
							if (Timing.ReplacementFunction != null)
							{
								this.UpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.UpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
								Timing.ReplacementFunction = null;
							}
							prewarm = (!this.UpdatePaused[processIndex.i] && !this.UpdateHeld[processIndex.i]);
						}
						else
						{
							prewarm = false;
						}
					}
					goto IL_D75;
				}
				case Segment.FixedUpdate:
				{
					if (this._nextFixedUpdateProcessSlot >= this.FixedUpdateProcesses.Length)
					{
						IEnumerator<float>[] fixedUpdateProcesses = this.FixedUpdateProcesses;
						bool[] fixedUpdatePaused = this.FixedUpdatePaused;
						bool[] fixedUpdateHeld = this.FixedUpdateHeld;
						ushort num6 = (ushort)this.FixedUpdateProcesses.Length;
						ushort num7 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.FixedUpdateProcesses = new IEnumerator<float>[(int)(num6 + num7 * expansions)];
						this.FixedUpdatePaused = new bool[this.FixedUpdateProcesses.Length];
						this.FixedUpdateHeld = new bool[this.FixedUpdateProcesses.Length];
						for (int j = 0; j < fixedUpdateProcesses.Length; j++)
						{
							this.FixedUpdateProcesses[j] = fixedUpdateProcesses[j];
							this.FixedUpdatePaused[j] = fixedUpdatePaused[j];
							this.FixedUpdateHeld[j] = fixedUpdateHeld[j];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastFixedUpdateProcessSlot = this._nextFixedUpdateProcessSlot;
					}
					int num5 = this._nextFixedUpdateProcessSlot;
					this._nextFixedUpdateProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.FixedUpdateProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					while (prewarm)
					{
						if (!this.FixedUpdateProcesses[processIndex.i].MoveNext())
						{
							if (this._indexToHandle.ContainsKey(processIndex))
							{
								this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
							}
							prewarm = false;
						}
						else if (this.FixedUpdateProcesses[processIndex.i] != null && float.IsNaN(this.FixedUpdateProcesses[processIndex.i].Current))
						{
							if (Timing.ReplacementFunction != null)
							{
								this.FixedUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.FixedUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
								Timing.ReplacementFunction = null;
							}
							prewarm = (!this.FixedUpdatePaused[processIndex.i] && !this.FixedUpdateHeld[processIndex.i]);
						}
						else
						{
							prewarm = false;
						}
					}
					goto IL_D75;
				}
				case Segment.LateUpdate:
				{
					if (this._nextLateUpdateProcessSlot >= this.LateUpdateProcesses.Length)
					{
						IEnumerator<float>[] lateUpdateProcesses = this.LateUpdateProcesses;
						bool[] lateUpdatePaused = this.LateUpdatePaused;
						bool[] lateUpdateHeld = this.LateUpdateHeld;
						ushort num8 = (ushort)this.LateUpdateProcesses.Length;
						ushort num9 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.LateUpdateProcesses = new IEnumerator<float>[(int)(num8 + num9 * expansions)];
						this.LateUpdatePaused = new bool[this.LateUpdateProcesses.Length];
						this.LateUpdateHeld = new bool[this.LateUpdateProcesses.Length];
						for (int k = 0; k < lateUpdateProcesses.Length; k++)
						{
							this.LateUpdateProcesses[k] = lateUpdateProcesses[k];
							this.LateUpdatePaused[k] = lateUpdatePaused[k];
							this.LateUpdateHeld[k] = lateUpdateHeld[k];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastLateUpdateProcessSlot = this._nextLateUpdateProcessSlot;
					}
					int num5 = this._nextLateUpdateProcessSlot;
					this._nextLateUpdateProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.LateUpdateProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					while (prewarm)
					{
						if (!this.LateUpdateProcesses[processIndex.i].MoveNext())
						{
							if (this._indexToHandle.ContainsKey(processIndex))
							{
								this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
							}
							prewarm = false;
						}
						else if (this.LateUpdateProcesses[processIndex.i] != null && float.IsNaN(this.LateUpdateProcesses[processIndex.i].Current))
						{
							if (Timing.ReplacementFunction != null)
							{
								this.LateUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.LateUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
								Timing.ReplacementFunction = null;
							}
							prewarm = (!this.LateUpdatePaused[processIndex.i] && !this.LateUpdateHeld[processIndex.i]);
						}
						else
						{
							prewarm = false;
						}
					}
					goto IL_D75;
				}
				case Segment.SlowUpdate:
				{
					if (this._nextSlowUpdateProcessSlot >= this.SlowUpdateProcesses.Length)
					{
						IEnumerator<float>[] slowUpdateProcesses = this.SlowUpdateProcesses;
						bool[] slowUpdatePaused = this.SlowUpdatePaused;
						bool[] slowUpdateHeld = this.SlowUpdateHeld;
						ushort num10 = (ushort)this.SlowUpdateProcesses.Length;
						ushort num11 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.SlowUpdateProcesses = new IEnumerator<float>[(int)(num10 + num11 * expansions)];
						this.SlowUpdatePaused = new bool[this.SlowUpdateProcesses.Length];
						this.SlowUpdateHeld = new bool[this.SlowUpdateProcesses.Length];
						for (int l = 0; l < slowUpdateProcesses.Length; l++)
						{
							this.SlowUpdateProcesses[l] = slowUpdateProcesses[l];
							this.SlowUpdatePaused[l] = slowUpdatePaused[l];
							this.SlowUpdateHeld[l] = slowUpdateHeld[l];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastSlowUpdateProcessSlot = this._nextSlowUpdateProcessSlot;
					}
					int num5 = this._nextSlowUpdateProcessSlot;
					this._nextSlowUpdateProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.SlowUpdateProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					while (prewarm)
					{
						if (!this.SlowUpdateProcesses[processIndex.i].MoveNext())
						{
							if (this._indexToHandle.ContainsKey(processIndex))
							{
								this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
							}
							prewarm = false;
						}
						else if (this.SlowUpdateProcesses[processIndex.i] != null && float.IsNaN(this.SlowUpdateProcesses[processIndex.i].Current))
						{
							if (Timing.ReplacementFunction != null)
							{
								this.SlowUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.SlowUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
								Timing.ReplacementFunction = null;
							}
							prewarm = (!this.SlowUpdatePaused[processIndex.i] && !this.SlowUpdateHeld[processIndex.i]);
						}
						else
						{
							prewarm = false;
						}
					}
					goto IL_D75;
				}
				case Segment.RealtimeUpdate:
				{
					if (this._nextRealtimeUpdateProcessSlot >= this.RealtimeUpdateProcesses.Length)
					{
						IEnumerator<float>[] realtimeUpdateProcesses = this.RealtimeUpdateProcesses;
						bool[] realtimeUpdatePaused = this.RealtimeUpdatePaused;
						bool[] realtimeUpdateHeld = this.RealtimeUpdateHeld;
						ushort num12 = (ushort)this.RealtimeUpdateProcesses.Length;
						ushort num13 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.RealtimeUpdateProcesses = new IEnumerator<float>[(int)(num12 + num13 * expansions)];
						this.RealtimeUpdatePaused = new bool[this.RealtimeUpdateProcesses.Length];
						this.RealtimeUpdateHeld = new bool[this.RealtimeUpdateProcesses.Length];
						for (int m = 0; m < realtimeUpdateProcesses.Length; m++)
						{
							this.RealtimeUpdateProcesses[m] = realtimeUpdateProcesses[m];
							this.RealtimeUpdatePaused[m] = realtimeUpdatePaused[m];
							this.RealtimeUpdateHeld[m] = realtimeUpdateHeld[m];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastRealtimeUpdateProcessSlot = this._nextRealtimeUpdateProcessSlot;
					}
					int num5 = this._nextRealtimeUpdateProcessSlot;
					this._nextRealtimeUpdateProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.RealtimeUpdateProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					while (prewarm)
					{
						if (!this.RealtimeUpdateProcesses[processIndex.i].MoveNext())
						{
							if (this._indexToHandle.ContainsKey(processIndex))
							{
								this.KillCoroutinesOnInstance(this._indexToHandle[processIndex]);
							}
							prewarm = false;
						}
						else if (this.RealtimeUpdateProcesses[processIndex.i] != null && float.IsNaN(this.RealtimeUpdateProcesses[processIndex.i].Current))
						{
							if (Timing.ReplacementFunction != null)
							{
								this.RealtimeUpdateProcesses[processIndex.i] = Timing.ReplacementFunction(this.RealtimeUpdateProcesses[processIndex.i], this._indexToHandle[processIndex]);
								Timing.ReplacementFunction = null;
							}
							prewarm = (!this.RealtimeUpdatePaused[processIndex.i] && !this.RealtimeUpdateHeld[processIndex.i]);
						}
						else
						{
							prewarm = false;
						}
					}
					goto IL_D75;
				}
				case Segment.EndOfFrame:
				{
					if (this._nextEndOfFrameProcessSlot >= this.EndOfFrameProcesses.Length)
					{
						IEnumerator<float>[] endOfFrameProcesses = this.EndOfFrameProcesses;
						bool[] endOfFramePaused = this.EndOfFramePaused;
						bool[] endOfFrameHeld = this.EndOfFrameHeld;
						ushort num14 = (ushort)this.EndOfFrameProcesses.Length;
						ushort num15 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.EndOfFrameProcesses = new IEnumerator<float>[(int)(num14 + num15 * expansions)];
						this.EndOfFramePaused = new bool[this.EndOfFrameProcesses.Length];
						this.EndOfFrameHeld = new bool[this.EndOfFrameProcesses.Length];
						for (int n = 0; n < endOfFrameProcesses.Length; n++)
						{
							this.EndOfFrameProcesses[n] = endOfFrameProcesses[n];
							this.EndOfFramePaused[n] = endOfFramePaused[n];
							this.EndOfFrameHeld[n] = endOfFrameHeld[n];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastEndOfFrameProcessSlot = this._nextEndOfFrameProcessSlot;
					}
					int num5 = this._nextEndOfFrameProcessSlot;
					this._nextEndOfFrameProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.EndOfFrameProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					this._eofWatcherHandle = this.RunCoroutineSingletonOnInstance(this._EOFPumpWatcher(), this._eofWatcherHandle, SingletonBehavior.Abort);
					goto IL_D75;
				}
				case Segment.ManualTimeframe:
				{
					if (this._nextManualTimeframeProcessSlot >= this.ManualTimeframeProcesses.Length)
					{
						IEnumerator<float>[] manualTimeframeProcesses = this.ManualTimeframeProcesses;
						bool[] manualTimeframePaused = this.ManualTimeframePaused;
						bool[] manualTimeframeHeld = this.ManualTimeframeHeld;
						ushort num16 = (ushort)this.ManualTimeframeProcesses.Length;
						ushort num17 = 64;
						ushort expansions = this._expansions;
						this._expansions = expansions + 1;
						this.ManualTimeframeProcesses = new IEnumerator<float>[(int)(num16 + num17 * expansions)];
						this.ManualTimeframePaused = new bool[this.ManualTimeframeProcesses.Length];
						this.ManualTimeframeHeld = new bool[this.ManualTimeframeProcesses.Length];
						for (int num18 = 0; num18 < manualTimeframeProcesses.Length; num18++)
						{
							this.ManualTimeframeProcesses[num18] = manualTimeframeProcesses[num18];
							this.ManualTimeframePaused[num18] = manualTimeframePaused[num18];
							this.ManualTimeframeHeld[num18] = manualTimeframeHeld[num18];
						}
					}
					if (this.UpdateTimeValues(processIndex.seg))
					{
						this._lastManualTimeframeProcessSlot = this._nextManualTimeframeProcessSlot;
					}
					int num5 = this._nextManualTimeframeProcessSlot;
					this._nextManualTimeframeProcessSlot = num5 + 1;
					processIndex.i = num5;
					this.ManualTimeframeProcesses[processIndex.i] = coroutine;
					if (tag != null)
					{
						this.AddTagOnInstance(tag, handle);
					}
					if (layerHasValue)
					{
						this.AddLayerOnInstance(layer, handle);
					}
					this._indexToHandle.Add(processIndex, handle);
					this._handleToIndex.Add(handle, processIndex);
					goto IL_D75;
				}
				}
				handle = default(CoroutineHandle);
				IL_D75:;
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
			this.localTime = num;
			this.deltaTime = num2;
			this.currentCoroutine = currentCoroutine;
			return handle;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		public static int KillCoroutines()
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance();
			}
			return 0;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002EF00 File Offset: 0x0002D100
		public int KillCoroutinesOnInstance()
		{
			int result = this._nextUpdateProcessSlot + this._nextLateUpdateProcessSlot + this._nextFixedUpdateProcessSlot + this._nextSlowUpdateProcessSlot + this._nextRealtimeUpdateProcessSlot + this._nextEditorUpdateProcessSlot + this._nextEditorSlowUpdateProcessSlot + this._nextEndOfFrameProcessSlot + this._nextManualTimeframeProcessSlot;
			this.UpdateProcesses = new IEnumerator<float>[256];
			this.UpdatePaused = new bool[256];
			this.UpdateHeld = new bool[256];
			this.UpdateCoroutines = 0;
			this._nextUpdateProcessSlot = 0;
			this.LateUpdateProcesses = new IEnumerator<float>[8];
			this.LateUpdatePaused = new bool[8];
			this.LateUpdateHeld = new bool[8];
			this.LateUpdateCoroutines = 0;
			this._nextLateUpdateProcessSlot = 0;
			this.FixedUpdateProcesses = new IEnumerator<float>[64];
			this.FixedUpdatePaused = new bool[64];
			this.FixedUpdateHeld = new bool[64];
			this.FixedUpdateCoroutines = 0;
			this._nextFixedUpdateProcessSlot = 0;
			this.SlowUpdateProcesses = new IEnumerator<float>[64];
			this.SlowUpdatePaused = new bool[64];
			this.SlowUpdateHeld = new bool[64];
			this.SlowUpdateCoroutines = 0;
			this._nextSlowUpdateProcessSlot = 0;
			this.RealtimeUpdateProcesses = new IEnumerator<float>[8];
			this.RealtimeUpdatePaused = new bool[8];
			this.RealtimeUpdateHeld = new bool[8];
			this.RealtimeUpdateCoroutines = 0;
			this._nextRealtimeUpdateProcessSlot = 0;
			this.EditorUpdateProcesses = new IEnumerator<float>[8];
			this.EditorUpdatePaused = new bool[8];
			this.EditorUpdateHeld = new bool[8];
			this.EditorUpdateCoroutines = 0;
			this._nextEditorUpdateProcessSlot = 0;
			this.EditorSlowUpdateProcesses = new IEnumerator<float>[8];
			this.EditorSlowUpdatePaused = new bool[8];
			this.EditorSlowUpdateHeld = new bool[8];
			this.EditorSlowUpdateCoroutines = 0;
			this._nextEditorSlowUpdateProcessSlot = 0;
			this.EndOfFrameProcesses = new IEnumerator<float>[8];
			this.EndOfFramePaused = new bool[8];
			this.EndOfFrameHeld = new bool[8];
			this.EndOfFrameCoroutines = 0;
			this._nextEndOfFrameProcessSlot = 0;
			this.ManualTimeframeProcesses = new IEnumerator<float>[8];
			this.ManualTimeframePaused = new bool[8];
			this.ManualTimeframeHeld = new bool[8];
			this.ManualTimeframeCoroutines = 0;
			this._nextManualTimeframeProcessSlot = 0;
			this._processTags.Clear();
			this._taggedProcesses.Clear();
			this._processLayers.Clear();
			this._layeredProcesses.Clear();
			this._handleToIndex.Clear();
			this._indexToHandle.Clear();
			this._waitingTriggers.Clear();
			this._expansions = this._expansions / 2 + 1;
			Timing.Links.Clear();
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002F187 File Offset: 0x0002D387
		public static int KillCoroutines(CoroutineHandle handle)
		{
			if (!(Timing.ActiveInstances[(int)handle.Key] != null))
			{
				return 0;
			}
			return Timing.GetInstance(handle.Key).KillCoroutinesOnInstance(handle);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		public static int KillCoroutines(params CoroutineHandle[] handles)
		{
			int num = 0;
			for (int i = 0; i < handles.Length; i++)
			{
				num += ((Timing.ActiveInstances[(int)handles[i].Key] != null) ? Timing.GetInstance(handles[i].Key).KillCoroutinesOnInstance(handles[i]) : 0);
			}
			return num;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0002F210 File Offset: 0x0002D410
		public int KillCoroutinesOnInstance(CoroutineHandle handle)
		{
			int num = 0;
			if (this._handleToIndex.ContainsKey(handle))
			{
				if (this._waitingTriggers.ContainsKey(handle))
				{
					this.CloseWaitingProcess(handle);
				}
				if (this.Nullify(handle))
				{
					num++;
				}
				this.RemoveGraffiti(handle);
			}
			if (Timing.Links.ContainsKey(handle))
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = Timing.Links[handle].GetEnumerator();
				Timing.Links.Remove(handle);
				while (enumerator.MoveNext())
				{
					CoroutineHandle handle2 = enumerator.Current;
					num += Timing.KillCoroutines(handle2);
				}
			}
			return num;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002F29B File Offset: 0x0002D49B
		public static int KillCoroutines(GameObject gameObj)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance(gameObj.GetInstanceID());
			}
			return 0;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		public int KillCoroutinesOnInstance(GameObject gameObj)
		{
			return this.KillCoroutinesOnInstance(gameObj.GetInstanceID());
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002F2CA File Offset: 0x0002D4CA
		public static int KillCoroutines(int layer)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance(layer);
			}
			return 0;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002F2E8 File Offset: 0x0002D4E8
		public int KillCoroutinesOnInstance(int layer)
		{
			int num = 0;
			while (this._layeredProcesses.ContainsKey(layer))
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
				enumerator.MoveNext();
				if (this.Nullify(enumerator.Current))
				{
					if (this._waitingTriggers.ContainsKey(enumerator.Current))
					{
						this.CloseWaitingProcess(enumerator.Current);
					}
					num++;
				}
				this.RemoveGraffiti(enumerator.Current);
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle>.Enumerator enumerator2 = Timing.Links[enumerator.Current].GetEnumerator();
					Timing.Links.Remove(enumerator.Current);
					while (enumerator2.MoveNext())
					{
						CoroutineHandle handle = enumerator2.Current;
						num += Timing.KillCoroutines(handle);
					}
				}
			}
			return num;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0002F3BF File Offset: 0x0002D5BF
		public static int KillCoroutines(string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance(tag);
			}
			return 0;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		public int KillCoroutinesOnInstance(string tag)
		{
			if (tag == null)
			{
				return 0;
			}
			int num = 0;
			while (this._taggedProcesses.ContainsKey(tag))
			{
				HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
				enumerator.MoveNext();
				if (this.Nullify(this._handleToIndex[enumerator.Current]))
				{
					if (this._waitingTriggers.ContainsKey(enumerator.Current))
					{
						this.CloseWaitingProcess(enumerator.Current);
					}
					num++;
				}
				this.RemoveGraffiti(enumerator.Current);
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle>.Enumerator enumerator2 = Timing.Links[enumerator.Current].GetEnumerator();
					Timing.Links.Remove(enumerator.Current);
					while (enumerator2.MoveNext())
					{
						CoroutineHandle handle = enumerator2.Current;
						num += Timing.KillCoroutines(handle);
					}
				}
			}
			return num;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002F4C3 File Offset: 0x0002D6C3
		public static int KillCoroutines(GameObject gameObj, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
			}
			return 0;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0002F4E5 File Offset: 0x0002D6E5
		public int KillCoroutinesOnInstance(GameObject gameObj, string tag)
		{
			return this.KillCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
		public static int KillCoroutines(int layer, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.KillCoroutinesOnInstance(layer, tag);
			}
			return 0;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002F514 File Offset: 0x0002D714
		public int KillCoroutinesOnInstance(int layer, string tag)
		{
			if (tag == null)
			{
				return this.KillCoroutinesOnInstance(layer);
			}
			if (!this._layeredProcesses.ContainsKey(layer) || !this._taggedProcesses.ContainsKey(tag))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && this._layeredProcesses[layer].Contains(enumerator.Current) && this.Nullify(enumerator.Current))
				{
					if (this._waitingTriggers.ContainsKey(enumerator.Current))
					{
						this.CloseWaitingProcess(enumerator.Current);
					}
					num++;
					this.RemoveGraffiti(enumerator.Current);
					if (Timing.Links.ContainsKey(enumerator.Current))
					{
						HashSet<CoroutineHandle>.Enumerator enumerator2 = Timing.Links[enumerator.Current].GetEnumerator();
						Timing.Links.Remove(enumerator.Current);
						while (enumerator2.MoveNext())
						{
							CoroutineHandle handle = enumerator2.Current;
							Timing.KillCoroutines(handle);
						}
					}
					if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
					{
						break;
					}
					enumerator = this._taggedProcesses[tag].GetEnumerator();
				}
			}
			return num;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002F670 File Offset: 0x0002D870
		public static Timing GetInstance(byte ID)
		{
			if (ID >= 16)
			{
				return null;
			}
			return Timing.ActiveInstances[(int)ID];
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002F680 File Offset: 0x0002D880
		public static float WaitForSeconds(float waitTime)
		{
			if (float.IsNaN(waitTime))
			{
				waitTime = 0f;
			}
			return Timing.LocalTime + waitTime;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002F698 File Offset: 0x0002D898
		public float WaitForSecondsOnInstance(float waitTime)
		{
			if (float.IsNaN(waitTime))
			{
				waitTime = 0f;
			}
			return this.localTime + waitTime;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		private bool UpdateTimeValues(Segment segment)
		{
			switch (segment)
			{
			case Segment.Update:
				if (this._currentUpdateFrame != Time.frameCount)
				{
					this.deltaTime = Time.deltaTime;
					this._lastUpdateTime += this.deltaTime;
					this.localTime = this._lastUpdateTime;
					this._currentUpdateFrame = Time.frameCount;
					return true;
				}
				this.deltaTime = Time.deltaTime;
				this.localTime = this._lastUpdateTime;
				return false;
			case Segment.FixedUpdate:
				this.deltaTime = Time.fixedDeltaTime;
				this.localTime = Time.fixedTime;
				if (this._lastFixedUpdateTime + 0.0001f < Time.fixedTime)
				{
					this._lastFixedUpdateTime = Time.fixedTime;
					return true;
				}
				return false;
			case Segment.LateUpdate:
				if (this._currentLateUpdateFrame != Time.frameCount)
				{
					this.deltaTime = Time.deltaTime;
					this._lastLateUpdateTime += this.deltaTime;
					this.localTime = this._lastLateUpdateTime;
					this._currentLateUpdateFrame = Time.frameCount;
					return true;
				}
				this.deltaTime = Time.deltaTime;
				this.localTime = this._lastLateUpdateTime;
				return false;
			case Segment.SlowUpdate:
				if (this._currentSlowUpdateFrame != Time.frameCount)
				{
					this.deltaTime = (this._lastSlowUpdateDeltaTime = Time.realtimeSinceStartup - this._lastSlowUpdateTime);
					this.localTime = (this._lastSlowUpdateTime = Time.realtimeSinceStartup);
					this._currentSlowUpdateFrame = Time.frameCount;
					return true;
				}
				this.localTime = this._lastSlowUpdateTime;
				this.deltaTime = this._lastSlowUpdateDeltaTime;
				return false;
			case Segment.RealtimeUpdate:
				if (this._currentRealtimeUpdateFrame != Time.frameCount)
				{
					this.deltaTime = Time.unscaledDeltaTime;
					this._lastRealtimeUpdateTime += this.deltaTime;
					this.localTime = this._lastRealtimeUpdateTime;
					this._currentRealtimeUpdateFrame = Time.frameCount;
					return true;
				}
				this.deltaTime = Time.unscaledDeltaTime;
				this.localTime = this._lastRealtimeUpdateTime;
				return false;
			case Segment.EndOfFrame:
				if (this._currentEndOfFrameFrame != Time.frameCount)
				{
					this.deltaTime = Time.deltaTime;
					this._lastEndOfFrameTime += this.deltaTime;
					this.localTime = this._lastEndOfFrameTime;
					this._currentEndOfFrameFrame = Time.frameCount;
					return true;
				}
				this.deltaTime = Time.deltaTime;
				this.localTime = this._lastEndOfFrameTime;
				return false;
			case Segment.ManualTimeframe:
			{
				float num = (this.SetManualTimeframeTime == null) ? Time.time : this.SetManualTimeframeTime(this._lastManualTimeframeTime);
				if ((double)this._lastManualTimeframeTime + 0.0001 < (double)num && (double)this._lastManualTimeframeTime - 0.0001 > (double)num)
				{
					this.localTime = num;
					this.deltaTime = this.localTime - this._lastManualTimeframeTime;
					if (this.deltaTime > Time.maximumDeltaTime)
					{
						this.deltaTime = Time.maximumDeltaTime;
					}
					this._lastManualTimeframeDeltaTime = this.deltaTime;
					this._lastManualTimeframeTime = num;
					return true;
				}
				this.deltaTime = this._lastManualTimeframeDeltaTime;
				this.localTime = this._lastManualTimeframeTime;
				return false;
			}
			}
			return true;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002F9AC File Offset: 0x0002DBAC
		private float GetSegmentTime(Segment segment)
		{
			switch (segment)
			{
			case Segment.Update:
				if (this._currentUpdateFrame == Time.frameCount)
				{
					return this._lastUpdateTime;
				}
				return this._lastUpdateTime + Time.deltaTime;
			case Segment.FixedUpdate:
				return Time.fixedTime;
			case Segment.LateUpdate:
				if (this._currentUpdateFrame == Time.frameCount)
				{
					return this._lastLateUpdateTime;
				}
				return this._lastLateUpdateTime + Time.deltaTime;
			case Segment.SlowUpdate:
				return Time.realtimeSinceStartup;
			case Segment.RealtimeUpdate:
				if (this._currentRealtimeUpdateFrame == Time.frameCount)
				{
					return this._lastRealtimeUpdateTime;
				}
				return this._lastRealtimeUpdateTime + Time.unscaledDeltaTime;
			case Segment.EndOfFrame:
				if (this._currentUpdateFrame == Time.frameCount)
				{
					return this._lastEndOfFrameTime;
				}
				return this._lastEndOfFrameTime + Time.deltaTime;
			case Segment.ManualTimeframe:
				return this._lastManualTimeframeTime;
			}
			return 0f;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002FA84 File Offset: 0x0002DC84
		public static int PauseCoroutines()
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance();
			}
			return 0;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
		public int PauseCoroutinesOnInstance()
		{
			int num = 0;
			for (int i = 0; i < this._nextUpdateProcessSlot; i++)
			{
				if (!this.UpdatePaused[i] && this.UpdateProcesses[i] != null)
				{
					num++;
					this.UpdatePaused[i] = true;
					if (this.UpdateProcesses[i].Current > this.GetSegmentTime(Segment.Update))
					{
						this.UpdateProcesses[i] = this._InjectDelay(this.UpdateProcesses[i], this.UpdateProcesses[i].Current - this.GetSegmentTime(Segment.Update));
					}
				}
			}
			for (int i = 0; i < this._nextLateUpdateProcessSlot; i++)
			{
				if (!this.LateUpdatePaused[i] && this.LateUpdateProcesses[i] != null)
				{
					num++;
					this.LateUpdatePaused[i] = true;
					if (this.LateUpdateProcesses[i].Current > this.GetSegmentTime(Segment.LateUpdate))
					{
						this.LateUpdateProcesses[i] = this._InjectDelay(this.LateUpdateProcesses[i], this.LateUpdateProcesses[i].Current - this.GetSegmentTime(Segment.LateUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextFixedUpdateProcessSlot; i++)
			{
				if (!this.FixedUpdatePaused[i] && this.FixedUpdateProcesses[i] != null)
				{
					num++;
					this.FixedUpdatePaused[i] = true;
					if (this.FixedUpdateProcesses[i].Current > this.GetSegmentTime(Segment.FixedUpdate))
					{
						this.FixedUpdateProcesses[i] = this._InjectDelay(this.FixedUpdateProcesses[i], this.FixedUpdateProcesses[i].Current - this.GetSegmentTime(Segment.FixedUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextSlowUpdateProcessSlot; i++)
			{
				if (!this.SlowUpdatePaused[i] && this.SlowUpdateProcesses[i] != null)
				{
					num++;
					this.SlowUpdatePaused[i] = true;
					if (this.SlowUpdateProcesses[i].Current > this.GetSegmentTime(Segment.SlowUpdate))
					{
						this.SlowUpdateProcesses[i] = this._InjectDelay(this.SlowUpdateProcesses[i], this.SlowUpdateProcesses[i].Current - this.GetSegmentTime(Segment.SlowUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextRealtimeUpdateProcessSlot; i++)
			{
				if (!this.RealtimeUpdatePaused[i] && this.RealtimeUpdateProcesses[i] != null)
				{
					num++;
					this.RealtimeUpdatePaused[i] = true;
					if (this.RealtimeUpdateProcesses[i].Current > this.GetSegmentTime(Segment.RealtimeUpdate))
					{
						this.RealtimeUpdateProcesses[i] = this._InjectDelay(this.RealtimeUpdateProcesses[i], this.RealtimeUpdateProcesses[i].Current - this.GetSegmentTime(Segment.RealtimeUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextEditorUpdateProcessSlot; i++)
			{
				if (!this.EditorUpdatePaused[i] && this.EditorUpdateProcesses[i] != null)
				{
					num++;
					this.EditorUpdatePaused[i] = true;
					if (this.EditorUpdateProcesses[i].Current > this.GetSegmentTime(Segment.EditorUpdate))
					{
						this.EditorUpdateProcesses[i] = this._InjectDelay(this.EditorUpdateProcesses[i], this.EditorUpdateProcesses[i].Current - this.GetSegmentTime(Segment.EditorUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextEditorSlowUpdateProcessSlot; i++)
			{
				if (!this.EditorSlowUpdatePaused[i] && this.EditorSlowUpdateProcesses[i] != null)
				{
					num++;
					this.EditorSlowUpdatePaused[i] = true;
					if (this.EditorSlowUpdateProcesses[i].Current > this.GetSegmentTime(Segment.EditorSlowUpdate))
					{
						this.EditorSlowUpdateProcesses[i] = this._InjectDelay(this.EditorSlowUpdateProcesses[i], this.EditorSlowUpdateProcesses[i].Current - this.GetSegmentTime(Segment.EditorSlowUpdate));
					}
				}
			}
			for (int i = 0; i < this._nextEndOfFrameProcessSlot; i++)
			{
				if (!this.EndOfFramePaused[i] && this.EndOfFrameProcesses[i] != null)
				{
					num++;
					this.EndOfFramePaused[i] = true;
					if (this.EndOfFrameProcesses[i].Current > this.GetSegmentTime(Segment.EndOfFrame))
					{
						this.EndOfFrameProcesses[i] = this._InjectDelay(this.EndOfFrameProcesses[i], this.EndOfFrameProcesses[i].Current - this.GetSegmentTime(Segment.EndOfFrame));
					}
				}
			}
			for (int i = 0; i < this._nextManualTimeframeProcessSlot; i++)
			{
				if (!this.ManualTimeframePaused[i] && this.ManualTimeframeProcesses[i] != null)
				{
					num++;
					this.ManualTimeframePaused[i] = true;
					if (this.ManualTimeframeProcesses[i].Current > this.GetSegmentTime(Segment.ManualTimeframe))
					{
						this.ManualTimeframeProcesses[i] = this._InjectDelay(this.ManualTimeframeProcesses[i], this.ManualTimeframeProcesses[i].Current - this.GetSegmentTime(Segment.ManualTimeframe));
					}
				}
			}
			Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator = Timing.Links.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex = this._handleToIndex;
				KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair = enumerator.Current;
				if (handleToIndex.ContainsKey(keyValuePair.Key))
				{
					keyValuePair = enumerator.Current;
					foreach (CoroutineHandle handle in keyValuePair.Value)
					{
						num += Timing.PauseCoroutines(handle);
					}
				}
			}
			return num;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0002FF20 File Offset: 0x0002E120
		public int PauseCoroutinesOnInstance(CoroutineHandle handle)
		{
			int num = 0;
			if (this._handleToIndex.ContainsKey(handle) && !this.CoindexIsNull(this._handleToIndex[handle]) && !this.SetPause(this._handleToIndex[handle], true))
			{
				num++;
			}
			if (Timing.Links.ContainsKey(handle))
			{
				HashSet<CoroutineHandle> hashSet = Timing.Links[handle];
				Timing.Links.Remove(handle);
				foreach (CoroutineHandle handle2 in hashSet)
				{
					num += Timing.PauseCoroutines(handle2);
				}
				Timing.Links.Add(handle, hashSet);
			}
			return num;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0002FFBD File Offset: 0x0002E1BD
		public static int PauseCoroutines(CoroutineHandle handle)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(handle);
			}
			return 0;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0002FFDC File Offset: 0x0002E1DC
		public static int PauseCoroutines(params CoroutineHandle[] handles)
		{
			int num = 0;
			for (int i = 0; i < handles.Length; i++)
			{
				num += ((Timing.ActiveInstances[(int)handles[i].Key] != null) ? Timing.GetInstance(handles[i].Key).PauseCoroutinesOnInstance(handles[i]) : 0);
			}
			return num;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00030037 File Offset: 0x0002E237
		public static int PauseCoroutines(GameObject gameObj)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(gameObj);
			}
			return 0;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00030053 File Offset: 0x0002E253
		public int PauseCoroutinesOnInstance(GameObject gameObj)
		{
			if (!(gameObj == null))
			{
				return this.PauseCoroutinesOnInstance(gameObj.GetInstanceID());
			}
			return 0;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003006C File Offset: 0x0002E26C
		public static int PauseCoroutines(int layer)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(layer);
			}
			return 0;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00030088 File Offset: 0x0002E288
		public int PauseCoroutinesOnInstance(int layer)
		{
			if (!this._layeredProcesses.ContainsKey(layer))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && !this.SetPause(this._handleToIndex[enumerator.Current], true))
				{
					num++;
				}
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
					Timing.Links.Remove(enumerator.Current);
					foreach (CoroutineHandle handle in hashSet)
					{
						num += Timing.PauseCoroutines(handle);
					}
					Timing.Links.Add(enumerator.Current, hashSet);
				}
			}
			return num;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0003016E File Offset: 0x0002E36E
		public static int PauseCoroutines(string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(tag);
			}
			return 0;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0003018C File Offset: 0x0002E38C
		public int PauseCoroutinesOnInstance(string tag)
		{
			if (tag == null || !this._taggedProcesses.ContainsKey(tag))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && !this.SetPause(this._handleToIndex[enumerator.Current], true))
				{
					num++;
				}
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
					Timing.Links.Remove(enumerator.Current);
					foreach (CoroutineHandle handle in hashSet)
					{
						num += Timing.PauseCoroutines(handle);
					}
					Timing.Links.Add(enumerator.Current, hashSet);
				}
			}
			return num;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00030275 File Offset: 0x0002E475
		public static int PauseCoroutines(GameObject gameObj, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
			}
			return 0;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00030297 File Offset: 0x0002E497
		public int PauseCoroutinesOnInstance(GameObject gameObj, string tag)
		{
			if (!(gameObj == null))
			{
				return this.PauseCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
			}
			return 0;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000302B1 File Offset: 0x0002E4B1
		public static int PauseCoroutines(int layer, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.PauseCoroutinesOnInstance(layer, tag);
			}
			return 0;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000302D0 File Offset: 0x0002E4D0
		public int PauseCoroutinesOnInstance(int layer, string tag)
		{
			if (tag == null)
			{
				return this.PauseCoroutinesOnInstance(layer);
			}
			if (!this._taggedProcesses.ContainsKey(tag) || !this._layeredProcesses.ContainsKey(layer))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this._processLayers.ContainsKey(enumerator.Current) && this._processLayers[enumerator.Current] == layer && !this.CoindexIsNull(this._handleToIndex[enumerator.Current]))
				{
					if (!this.SetPause(this._handleToIndex[enumerator.Current], true))
					{
						num++;
					}
					if (Timing.Links.ContainsKey(enumerator.Current))
					{
						HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
						Timing.Links.Remove(enumerator.Current);
						foreach (CoroutineHandle handle in hashSet)
						{
							num += Timing.PauseCoroutines(handle);
						}
						Timing.Links.Add(enumerator.Current, hashSet);
					}
				}
			}
			return num;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00030401 File Offset: 0x0002E601
		public static int ResumeCoroutines()
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance();
			}
			return 0;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003041C File Offset: 0x0002E61C
		public int ResumeCoroutinesOnInstance()
		{
			int num = 0;
			Timing.ProcessIndex processIndex;
			processIndex.i = 0;
			processIndex.seg = Segment.Update;
			while (processIndex.i < this._nextUpdateProcessSlot)
			{
				if (this.UpdatePaused[processIndex.i] && this.UpdateProcesses[processIndex.i] != null)
				{
					this.UpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.LateUpdate;
			while (processIndex.i < this._nextLateUpdateProcessSlot)
			{
				if (this.LateUpdatePaused[processIndex.i] && this.LateUpdateProcesses[processIndex.i] != null)
				{
					this.LateUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.FixedUpdate;
			while (processIndex.i < this._nextFixedUpdateProcessSlot)
			{
				if (this.FixedUpdatePaused[processIndex.i] && this.FixedUpdateProcesses[processIndex.i] != null)
				{
					this.FixedUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.SlowUpdate;
			while (processIndex.i < this._nextSlowUpdateProcessSlot)
			{
				if (this.SlowUpdatePaused[processIndex.i] && this.SlowUpdateProcesses[processIndex.i] != null)
				{
					this.SlowUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.RealtimeUpdate;
			while (processIndex.i < this._nextRealtimeUpdateProcessSlot)
			{
				if (this.RealtimeUpdatePaused[processIndex.i] && this.RealtimeUpdateProcesses[processIndex.i] != null)
				{
					this.RealtimeUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.EditorUpdate;
			while (processIndex.i < this._nextEditorUpdateProcessSlot)
			{
				if (this.EditorUpdatePaused[processIndex.i] && this.EditorUpdateProcesses[processIndex.i] != null)
				{
					this.EditorUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.EditorSlowUpdate;
			while (processIndex.i < this._nextEditorSlowUpdateProcessSlot)
			{
				if (this.EditorSlowUpdatePaused[processIndex.i] && this.EditorSlowUpdateProcesses[processIndex.i] != null)
				{
					this.EditorSlowUpdatePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.EndOfFrame;
			while (processIndex.i < this._nextEndOfFrameProcessSlot)
			{
				if (this.EndOfFramePaused[processIndex.i] && this.EndOfFrameProcesses[processIndex.i] != null)
				{
					this.EndOfFramePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			processIndex.i = 0;
			processIndex.seg = Segment.ManualTimeframe;
			while (processIndex.i < this._nextManualTimeframeProcessSlot)
			{
				if (this.ManualTimeframePaused[processIndex.i] && this.ManualTimeframeProcesses[processIndex.i] != null)
				{
					this.ManualTimeframePaused[processIndex.i] = false;
					num++;
				}
				processIndex.i++;
			}
			Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator = Timing.Links.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<CoroutineHandle, Timing.ProcessIndex> handleToIndex = this._handleToIndex;
				KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair = enumerator.Current;
				if (handleToIndex.ContainsKey(keyValuePair.Key))
				{
					keyValuePair = enumerator.Current;
					foreach (CoroutineHandle handle in keyValuePair.Value)
					{
						num += Timing.ResumeCoroutines(handle);
					}
				}
			}
			return num;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000307CB File Offset: 0x0002E9CB
		public static int ResumeCoroutines(CoroutineHandle handle)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(handle);
			}
			return 0;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000307E8 File Offset: 0x0002E9E8
		public static int ResumeCoroutines(params CoroutineHandle[] handles)
		{
			int num = 0;
			for (int i = 0; i < handles.Length; i++)
			{
				num += ((Timing.ActiveInstances[(int)handles[i].Key] != null) ? Timing.GetInstance(handles[i].Key).ResumeCoroutinesOnInstance(handles[i]) : 0);
			}
			return num;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00030844 File Offset: 0x0002EA44
		public int ResumeCoroutinesOnInstance(CoroutineHandle handle)
		{
			int num = 0;
			if (this._handleToIndex.ContainsKey(handle) && !this.CoindexIsNull(this._handleToIndex[handle]) && this.SetPause(this._handleToIndex[handle], false))
			{
				num++;
			}
			if (Timing.Links.ContainsKey(handle))
			{
				HashSet<CoroutineHandle> hashSet = Timing.Links[handle];
				Timing.Links.Remove(handle);
				foreach (CoroutineHandle handle2 in hashSet)
				{
					num += Timing.ResumeCoroutines(handle2);
				}
				Timing.Links.Add(handle, hashSet);
			}
			return num;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000308E4 File Offset: 0x0002EAE4
		public int ResumeCoroutinesOnInstance(IEnumerable<CoroutineHandle> handles)
		{
			int result = 0;
			IEnumerator<CoroutineHandle> enumerator = handles.GetEnumerator();
			while (!enumerator.MoveNext())
			{
				this.ResumeCoroutinesOnInstance(enumerator.Current);
			}
			return result;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00030912 File Offset: 0x0002EB12
		public static int ResumeCoroutines(GameObject gameObj)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(gameObj.GetInstanceID());
			}
			return 0;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00030933 File Offset: 0x0002EB33
		public int ResumeCoroutinesOnInstance(GameObject gameObj)
		{
			if (!(gameObj == null))
			{
				return this.ResumeCoroutinesOnInstance(gameObj.GetInstanceID());
			}
			return 0;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0003094C File Offset: 0x0002EB4C
		public static int ResumeCoroutines(int layer)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(layer);
			}
			return 0;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00030968 File Offset: 0x0002EB68
		public int ResumeCoroutinesOnInstance(int layer)
		{
			if (!this._layeredProcesses.ContainsKey(layer))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._layeredProcesses[layer].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && this.SetPause(this._handleToIndex[enumerator.Current], false))
				{
					num++;
				}
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
					Timing.Links.Remove(enumerator.Current);
					foreach (CoroutineHandle handle in hashSet)
					{
						num += Timing.ResumeCoroutines(handle);
					}
					Timing.Links.Add(enumerator.Current, hashSet);
				}
			}
			return num;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00030A4E File Offset: 0x0002EC4E
		public static int ResumeCoroutines(string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(tag);
			}
			return 0;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00030A6C File Offset: 0x0002EC6C
		public int ResumeCoroutinesOnInstance(string tag)
		{
			if (tag == null || !this._taggedProcesses.ContainsKey(tag))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && this.SetPause(this._handleToIndex[enumerator.Current], false))
				{
					num++;
				}
				if (Timing.Links.ContainsKey(enumerator.Current))
				{
					HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
					Timing.Links.Remove(enumerator.Current);
					foreach (CoroutineHandle handle in hashSet)
					{
						num += Timing.ResumeCoroutines(handle);
					}
					Timing.Links.Add(enumerator.Current, hashSet);
				}
			}
			return num;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00030B55 File Offset: 0x0002ED55
		public static int ResumeCoroutines(GameObject gameObj, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
			}
			return 0;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00030B77 File Offset: 0x0002ED77
		public int ResumeCoroutinesOnInstance(GameObject gameObj, string tag)
		{
			if (!(gameObj == null))
			{
				return this.ResumeCoroutinesOnInstance(gameObj.GetInstanceID(), tag);
			}
			return 0;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00030B91 File Offset: 0x0002ED91
		public static int ResumeCoroutines(int layer, string tag)
		{
			if (!(Timing._instance == null))
			{
				return Timing._instance.ResumeCoroutinesOnInstance(layer, tag);
			}
			return 0;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00030BB0 File Offset: 0x0002EDB0
		public int ResumeCoroutinesOnInstance(int layer, string tag)
		{
			if (tag == null)
			{
				return this.ResumeCoroutinesOnInstance(layer);
			}
			if (!this._layeredProcesses.ContainsKey(layer) || !this._taggedProcesses.ContainsKey(tag))
			{
				return 0;
			}
			int num = 0;
			HashSet<CoroutineHandle>.Enumerator enumerator = this._taggedProcesses[tag].GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.CoindexIsNull(this._handleToIndex[enumerator.Current]) && this._layeredProcesses[layer].Contains(enumerator.Current))
				{
					if (this.SetPause(this._handleToIndex[enumerator.Current], false))
					{
						num++;
					}
					if (Timing.Links.ContainsKey(enumerator.Current))
					{
						HashSet<CoroutineHandle> hashSet = Timing.Links[enumerator.Current];
						Timing.Links.Remove(enumerator.Current);
						foreach (CoroutineHandle handle in hashSet)
						{
							num += Timing.ResumeCoroutines(handle);
						}
						Timing.Links.Add(enumerator.Current, hashSet);
					}
				}
			}
			return num;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00030CD0 File Offset: 0x0002EED0
		public static string GetTag(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (!(instance != null) || !instance._handleToIndex.ContainsKey(handle) || !instance._processTags.ContainsKey(handle))
			{
				return null;
			}
			return instance._processTags[handle];
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00030D20 File Offset: 0x0002EF20
		public static int? GetLayer(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (!(instance != null) || !instance._handleToIndex.ContainsKey(handle) || !instance._processLayers.ContainsKey(handle))
			{
				return null;
			}
			return new int?(instance._processLayers[handle]);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00030D7C File Offset: 0x0002EF7C
		public static string GetDebugName(CoroutineHandle handle)
		{
			if (handle.Key == 0)
			{
				return "Uninitialized handle";
			}
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance == null)
			{
				return "Invalid handle";
			}
			if (!instance._handleToIndex.ContainsKey(handle))
			{
				return "Expired coroutine";
			}
			return instance.CoindexPeek(instance._handleToIndex[handle]).ToString();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00030DE0 File Offset: 0x0002EFE0
		public static Segment GetSegment(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (!(instance != null) || !instance._handleToIndex.ContainsKey(handle))
			{
				return Segment.Invalid;
			}
			return instance._handleToIndex[handle].seg;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00030E24 File Offset: 0x0002F024
		public static bool SetTag(CoroutineHandle handle, string newTag, bool overwriteExisting = true)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance == null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]) || (!overwriteExisting && instance._processTags.ContainsKey(handle)))
			{
				return false;
			}
			instance.RemoveTagOnInstance(handle);
			instance.AddTagOnInstance(newTag, handle);
			return true;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00030E8C File Offset: 0x0002F08C
		public static bool SetLayer(CoroutineHandle handle, int newLayer, bool overwriteExisting = true)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance == null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]) || (!overwriteExisting && instance._processLayers.ContainsKey(handle)))
			{
				return false;
			}
			instance.RemoveLayerOnInstance(handle);
			instance.AddLayerOnInstance(newLayer, handle);
			return true;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00030EF4 File Offset: 0x0002F0F4
		public static bool SetSegment(CoroutineHandle handle, Segment newSegment)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance == null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]))
			{
				return false;
			}
			Timing.ProcessIndex processIndex = instance._handleToIndex[handle];
			IEnumerator<float> enumerator = instance.CoindexExtract(processIndex);
			bool newHeldState = instance.CoindexIsHeld(processIndex);
			bool newPausedState = instance.CoindexIsPaused(processIndex);
			if (enumerator.Current > instance.GetSegmentTime(processIndex.seg))
			{
				enumerator = instance._InjectDelay(enumerator, enumerator.Current - instance.GetSegmentTime(processIndex.seg));
			}
			instance.RunCoroutineInternal(enumerator, newSegment, 0, false, null, handle, false);
			processIndex = instance._handleToIndex[handle];
			instance.SetHeld(processIndex, newHeldState);
			instance.SetPause(processIndex, newPausedState);
			return true;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00030FBF File Offset: 0x0002F1BF
		public static bool RemoveTag(CoroutineHandle handle)
		{
			return Timing.SetTag(handle, null, true);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00030FCC File Offset: 0x0002F1CC
		public static bool RemoveLayer(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance == null || !instance._handleToIndex.ContainsKey(handle) || instance.CoindexIsNull(instance._handleToIndex[handle]))
			{
				return false;
			}
			instance.RemoveLayerOnInstance(handle);
			return true;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003101C File Offset: 0x0002F21C
		public static bool IsRunning(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			return instance != null && instance._handleToIndex.ContainsKey(handle) && !instance.CoindexIsNull(instance._handleToIndex[handle]);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00031064 File Offset: 0x0002F264
		public static bool IsAliveAndPaused(CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			return instance != null && instance._handleToIndex.ContainsKey(handle) && !instance.CoindexIsNull(instance._handleToIndex[handle]) && instance.CoindexIsPaused(instance._handleToIndex[handle]);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000310C0 File Offset: 0x0002F2C0
		private void AddTagOnInstance(string tag, CoroutineHandle handle)
		{
			this._processTags.Add(handle, tag);
			if (this._taggedProcesses.ContainsKey(tag))
			{
				this._taggedProcesses[tag].Add(handle);
				return;
			}
			this._taggedProcesses.Add(tag, new HashSet<CoroutineHandle>
			{
				handle
			});
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00031118 File Offset: 0x0002F318
		private void AddLayerOnInstance(int layer, CoroutineHandle handle)
		{
			this._processLayers.Add(handle, layer);
			if (this._layeredProcesses.ContainsKey(layer))
			{
				this._layeredProcesses[layer].Add(handle);
				return;
			}
			this._layeredProcesses.Add(layer, new HashSet<CoroutineHandle>
			{
				handle
			});
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00031170 File Offset: 0x0002F370
		private void RemoveTagOnInstance(CoroutineHandle handle)
		{
			if (this._processTags.ContainsKey(handle))
			{
				if (this._taggedProcesses[this._processTags[handle]].Count > 1)
				{
					this._taggedProcesses[this._processTags[handle]].Remove(handle);
				}
				else
				{
					this._taggedProcesses.Remove(this._processTags[handle]);
				}
				this._processTags.Remove(handle);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000311F0 File Offset: 0x0002F3F0
		private void RemoveLayerOnInstance(CoroutineHandle handle)
		{
			if (this._processLayers.ContainsKey(handle))
			{
				if (this._layeredProcesses[this._processLayers[handle]].Count > 1)
				{
					this._layeredProcesses[this._processLayers[handle]].Remove(handle);
				}
				else
				{
					this._layeredProcesses.Remove(this._processLayers[handle]);
				}
				this._processLayers.Remove(handle);
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00031270 File Offset: 0x0002F470
		private void RemoveGraffiti(CoroutineHandle handle)
		{
			if (this._processLayers.ContainsKey(handle))
			{
				if (this._layeredProcesses[this._processLayers[handle]].Count > 1)
				{
					this._layeredProcesses[this._processLayers[handle]].Remove(handle);
				}
				else
				{
					this._layeredProcesses.Remove(this._processLayers[handle]);
				}
				this._processLayers.Remove(handle);
			}
			if (this._processTags.ContainsKey(handle))
			{
				if (this._taggedProcesses[this._processTags[handle]].Count > 1)
				{
					this._taggedProcesses[this._processTags[handle]].Remove(handle);
				}
				else
				{
					this._taggedProcesses.Remove(this._processTags[handle]);
				}
				this._processTags.Remove(handle);
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00031364 File Offset: 0x0002F564
		private IEnumerator<float> CoindexExtract(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
			{
				IEnumerator<float> result = this.UpdateProcesses[coindex.i];
				this.UpdateProcesses[coindex.i] = null;
				return result;
			}
			case Segment.FixedUpdate:
			{
				IEnumerator<float> result2 = this.FixedUpdateProcesses[coindex.i];
				this.FixedUpdateProcesses[coindex.i] = null;
				return result2;
			}
			case Segment.LateUpdate:
			{
				IEnumerator<float> result3 = this.LateUpdateProcesses[coindex.i];
				this.LateUpdateProcesses[coindex.i] = null;
				return result3;
			}
			case Segment.SlowUpdate:
			{
				IEnumerator<float> result4 = this.SlowUpdateProcesses[coindex.i];
				this.SlowUpdateProcesses[coindex.i] = null;
				return result4;
			}
			case Segment.RealtimeUpdate:
			{
				IEnumerator<float> result5 = this.RealtimeUpdateProcesses[coindex.i];
				this.RealtimeUpdateProcesses[coindex.i] = null;
				return result5;
			}
			case Segment.EditorUpdate:
			{
				IEnumerator<float> result6 = this.EditorUpdateProcesses[coindex.i];
				this.EditorUpdateProcesses[coindex.i] = null;
				return result6;
			}
			case Segment.EditorSlowUpdate:
			{
				IEnumerator<float> result7 = this.EditorSlowUpdateProcesses[coindex.i];
				this.EditorSlowUpdateProcesses[coindex.i] = null;
				return result7;
			}
			case Segment.EndOfFrame:
			{
				IEnumerator<float> result8 = this.EndOfFrameProcesses[coindex.i];
				this.EndOfFrameProcesses[coindex.i] = null;
				return result8;
			}
			case Segment.ManualTimeframe:
			{
				IEnumerator<float> result9 = this.ManualTimeframeProcesses[coindex.i];
				this.ManualTimeframeProcesses[coindex.i] = null;
				return result9;
			}
			default:
				return null;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000314A4 File Offset: 0x0002F6A4
		private bool CoindexIsNull(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
				return this.UpdateProcesses[coindex.i] == null;
			case Segment.FixedUpdate:
				return this.FixedUpdateProcesses[coindex.i] == null;
			case Segment.LateUpdate:
				return this.LateUpdateProcesses[coindex.i] == null;
			case Segment.SlowUpdate:
				return this.SlowUpdateProcesses[coindex.i] == null;
			case Segment.RealtimeUpdate:
				return this.RealtimeUpdateProcesses[coindex.i] == null;
			case Segment.EditorUpdate:
				return this.EditorUpdateProcesses[coindex.i] == null;
			case Segment.EditorSlowUpdate:
				return this.EditorSlowUpdateProcesses[coindex.i] == null;
			case Segment.EndOfFrame:
				return this.EndOfFrameProcesses[coindex.i] == null;
			case Segment.ManualTimeframe:
				return this.ManualTimeframeProcesses[coindex.i] == null;
			default:
				return true;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00031584 File Offset: 0x0002F784
		private IEnumerator<float> CoindexPeek(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
				return this.UpdateProcesses[coindex.i];
			case Segment.FixedUpdate:
				return this.FixedUpdateProcesses[coindex.i];
			case Segment.LateUpdate:
				return this.LateUpdateProcesses[coindex.i];
			case Segment.SlowUpdate:
				return this.SlowUpdateProcesses[coindex.i];
			case Segment.RealtimeUpdate:
				return this.RealtimeUpdateProcesses[coindex.i];
			case Segment.EditorUpdate:
				return this.EditorUpdateProcesses[coindex.i];
			case Segment.EditorSlowUpdate:
				return this.EditorSlowUpdateProcesses[coindex.i];
			case Segment.EndOfFrame:
				return this.EndOfFrameProcesses[coindex.i];
			case Segment.ManualTimeframe:
				return this.ManualTimeframeProcesses[coindex.i];
			default:
				return null;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00031643 File Offset: 0x0002F843
		private bool Nullify(CoroutineHandle handle)
		{
			return this.Nullify(this._handleToIndex[handle]);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00031658 File Offset: 0x0002F858
		private bool Nullify(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
			{
				bool result = this.UpdateProcesses[coindex.i] != null;
				this.UpdateProcesses[coindex.i] = null;
				return result;
			}
			case Segment.FixedUpdate:
			{
				bool result2 = this.FixedUpdateProcesses[coindex.i] != null;
				this.FixedUpdateProcesses[coindex.i] = null;
				return result2;
			}
			case Segment.LateUpdate:
			{
				bool result3 = this.LateUpdateProcesses[coindex.i] != null;
				this.LateUpdateProcesses[coindex.i] = null;
				return result3;
			}
			case Segment.SlowUpdate:
			{
				bool result4 = this.SlowUpdateProcesses[coindex.i] != null;
				this.SlowUpdateProcesses[coindex.i] = null;
				return result4;
			}
			case Segment.RealtimeUpdate:
			{
				bool result5 = this.RealtimeUpdateProcesses[coindex.i] != null;
				this.RealtimeUpdateProcesses[coindex.i] = null;
				return result5;
			}
			case Segment.EditorUpdate:
			{
				bool result6 = this.UpdateProcesses[coindex.i] != null;
				this.EditorUpdateProcesses[coindex.i] = null;
				return result6;
			}
			case Segment.EditorSlowUpdate:
			{
				bool result7 = this.EditorSlowUpdateProcesses[coindex.i] != null;
				this.EditorSlowUpdateProcesses[coindex.i] = null;
				return result7;
			}
			case Segment.EndOfFrame:
			{
				bool result8 = this.EndOfFrameProcesses[coindex.i] != null;
				this.EndOfFrameProcesses[coindex.i] = null;
				return result8;
			}
			case Segment.ManualTimeframe:
			{
				bool result9 = this.ManualTimeframeProcesses[coindex.i] != null;
				this.ManualTimeframeProcesses[coindex.i] = null;
				return result9;
			}
			default:
				return false;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000317B4 File Offset: 0x0002F9B4
		private bool SetPause(Timing.ProcessIndex coindex, bool newPausedState)
		{
			if (this.CoindexPeek(coindex) == null)
			{
				return false;
			}
			switch (coindex.seg)
			{
			case Segment.Update:
			{
				bool result = this.UpdatePaused[coindex.i];
				this.UpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.UpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.UpdateProcesses[coindex.i] = this._InjectDelay(this.UpdateProcesses[coindex.i], this.UpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result;
			}
			case Segment.FixedUpdate:
			{
				bool result2 = this.FixedUpdatePaused[coindex.i];
				this.FixedUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.FixedUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.FixedUpdateProcesses[coindex.i] = this._InjectDelay(this.FixedUpdateProcesses[coindex.i], this.FixedUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result2;
			}
			case Segment.LateUpdate:
			{
				bool result3 = this.LateUpdatePaused[coindex.i];
				this.LateUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.LateUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.LateUpdateProcesses[coindex.i] = this._InjectDelay(this.LateUpdateProcesses[coindex.i], this.LateUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result3;
			}
			case Segment.SlowUpdate:
			{
				bool result4 = this.SlowUpdatePaused[coindex.i];
				this.SlowUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.SlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.SlowUpdateProcesses[coindex.i] = this._InjectDelay(this.SlowUpdateProcesses[coindex.i], this.SlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result4;
			}
			case Segment.RealtimeUpdate:
			{
				bool result5 = this.RealtimeUpdatePaused[coindex.i];
				this.RealtimeUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.RealtimeUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.RealtimeUpdateProcesses[coindex.i] = this._InjectDelay(this.RealtimeUpdateProcesses[coindex.i], this.RealtimeUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result5;
			}
			case Segment.EditorUpdate:
			{
				bool result6 = this.EditorUpdatePaused[coindex.i];
				this.EditorUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.EditorUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EditorUpdateProcesses[coindex.i] = this._InjectDelay(this.EditorUpdateProcesses[coindex.i], this.EditorUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result6;
			}
			case Segment.EditorSlowUpdate:
			{
				bool result7 = this.EditorSlowUpdatePaused[coindex.i];
				this.EditorSlowUpdatePaused[coindex.i] = newPausedState;
				if (newPausedState && this.EditorSlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EditorSlowUpdateProcesses[coindex.i] = this._InjectDelay(this.EditorSlowUpdateProcesses[coindex.i], this.EditorSlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result7;
			}
			case Segment.EndOfFrame:
			{
				bool result8 = this.EndOfFramePaused[coindex.i];
				this.EndOfFramePaused[coindex.i] = newPausedState;
				if (newPausedState && this.EndOfFrameProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EndOfFrameProcesses[coindex.i] = this._InjectDelay(this.EndOfFrameProcesses[coindex.i], this.EndOfFrameProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result8;
			}
			case Segment.ManualTimeframe:
			{
				bool result9 = this.ManualTimeframePaused[coindex.i];
				this.ManualTimeframePaused[coindex.i] = newPausedState;
				if (newPausedState && this.ManualTimeframeProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.ManualTimeframeProcesses[coindex.i] = this._InjectDelay(this.ManualTimeframeProcesses[coindex.i], this.ManualTimeframeProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result9;
			}
			default:
				return false;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00031C74 File Offset: 0x0002FE74
		private bool SetHeld(Timing.ProcessIndex coindex, bool newHeldState)
		{
			if (this.CoindexPeek(coindex) == null)
			{
				return false;
			}
			switch (coindex.seg)
			{
			case Segment.Update:
			{
				bool result = this.UpdateHeld[coindex.i];
				this.UpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.UpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.UpdateProcesses[coindex.i] = this._InjectDelay(this.UpdateProcesses[coindex.i], this.UpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result;
			}
			case Segment.FixedUpdate:
			{
				bool result2 = this.FixedUpdateHeld[coindex.i];
				this.FixedUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.FixedUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.FixedUpdateProcesses[coindex.i] = this._InjectDelay(this.FixedUpdateProcesses[coindex.i], this.FixedUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result2;
			}
			case Segment.LateUpdate:
			{
				bool result3 = this.LateUpdateHeld[coindex.i];
				this.LateUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.LateUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.LateUpdateProcesses[coindex.i] = this._InjectDelay(this.LateUpdateProcesses[coindex.i], this.LateUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result3;
			}
			case Segment.SlowUpdate:
			{
				bool result4 = this.SlowUpdateHeld[coindex.i];
				this.SlowUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.SlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.SlowUpdateProcesses[coindex.i] = this._InjectDelay(this.SlowUpdateProcesses[coindex.i], this.SlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result4;
			}
			case Segment.RealtimeUpdate:
			{
				bool result5 = this.RealtimeUpdateHeld[coindex.i];
				this.RealtimeUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.RealtimeUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.RealtimeUpdateProcesses[coindex.i] = this._InjectDelay(this.RealtimeUpdateProcesses[coindex.i], this.RealtimeUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result5;
			}
			case Segment.EditorUpdate:
			{
				bool result6 = this.EditorUpdateHeld[coindex.i];
				this.EditorUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.EditorUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EditorUpdateProcesses[coindex.i] = this._InjectDelay(this.EditorUpdateProcesses[coindex.i], this.EditorUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result6;
			}
			case Segment.EditorSlowUpdate:
			{
				bool result7 = this.EditorSlowUpdateHeld[coindex.i];
				this.EditorSlowUpdateHeld[coindex.i] = newHeldState;
				if (newHeldState && this.EditorSlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EditorSlowUpdateProcesses[coindex.i] = this._InjectDelay(this.EditorSlowUpdateProcesses[coindex.i], this.EditorSlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result7;
			}
			case Segment.EndOfFrame:
			{
				bool result8 = this.EndOfFrameHeld[coindex.i];
				this.EndOfFrameHeld[coindex.i] = newHeldState;
				if (newHeldState && this.EndOfFrameProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.EndOfFrameProcesses[coindex.i] = this._InjectDelay(this.EndOfFrameProcesses[coindex.i], this.EndOfFrameProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result8;
			}
			case Segment.ManualTimeframe:
			{
				bool result9 = this.ManualTimeframeHeld[coindex.i];
				this.ManualTimeframeHeld[coindex.i] = newHeldState;
				if (newHeldState && this.ManualTimeframeProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					this.ManualTimeframeProcesses[coindex.i] = this._InjectDelay(this.ManualTimeframeProcesses[coindex.i], this.ManualTimeframeProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return result9;
			}
			default:
				return false;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00032134 File Offset: 0x00030334
		private IEnumerator<float> CreateHold(Timing.ProcessIndex coindex, IEnumerator<float> coptr)
		{
			if (this.CoindexPeek(coindex) == null)
			{
				return null;
			}
			switch (coindex.seg)
			{
			case Segment.Update:
				this.UpdateHeld[coindex.i] = true;
				if (this.UpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.UpdateProcesses[coindex.i], this.UpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.FixedUpdate:
				this.FixedUpdateHeld[coindex.i] = true;
				if (this.FixedUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.FixedUpdateProcesses[coindex.i], this.FixedUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.LateUpdate:
				this.LateUpdateHeld[coindex.i] = true;
				if (this.LateUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.LateUpdateProcesses[coindex.i], this.LateUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.SlowUpdate:
				this.SlowUpdateHeld[coindex.i] = true;
				if (this.SlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.SlowUpdateProcesses[coindex.i], this.SlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.RealtimeUpdate:
				this.RealtimeUpdateHeld[coindex.i] = true;
				if (this.RealtimeUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.RealtimeUpdateProcesses[coindex.i], this.RealtimeUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.EditorUpdate:
				this.EditorUpdateHeld[coindex.i] = true;
				if (this.EditorUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.EditorUpdateProcesses[coindex.i], this.EditorUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.EditorSlowUpdate:
				this.EditorSlowUpdateHeld[coindex.i] = true;
				if (this.EditorSlowUpdateProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.EditorSlowUpdateProcesses[coindex.i], this.EditorSlowUpdateProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.EndOfFrame:
				this.EndOfFrameHeld[coindex.i] = true;
				if (this.EndOfFrameProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.EndOfFrameProcesses[coindex.i], this.EndOfFrameProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			case Segment.ManualTimeframe:
				this.ManualTimeframeHeld[coindex.i] = true;
				if (this.ManualTimeframeProcesses[coindex.i].Current > this.GetSegmentTime(coindex.seg))
				{
					coptr = this._InjectDelay(this.ManualTimeframeProcesses[coindex.i], this.ManualTimeframeProcesses[coindex.i].Current - this.GetSegmentTime(coindex.seg));
				}
				return coptr;
			default:
				return coptr;
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00032508 File Offset: 0x00030708
		private bool CoindexIsPaused(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
				return this.UpdatePaused[coindex.i];
			case Segment.FixedUpdate:
				return this.FixedUpdatePaused[coindex.i];
			case Segment.LateUpdate:
				return this.LateUpdatePaused[coindex.i];
			case Segment.SlowUpdate:
				return this.SlowUpdatePaused[coindex.i];
			case Segment.RealtimeUpdate:
				return this.RealtimeUpdatePaused[coindex.i];
			case Segment.EditorUpdate:
				return this.EditorUpdatePaused[coindex.i];
			case Segment.EditorSlowUpdate:
				return this.EditorSlowUpdatePaused[coindex.i];
			case Segment.EndOfFrame:
				return this.EndOfFramePaused[coindex.i];
			case Segment.ManualTimeframe:
				return this.ManualTimeframePaused[coindex.i];
			default:
				return false;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000325C8 File Offset: 0x000307C8
		private bool CoindexIsHeld(Timing.ProcessIndex coindex)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
				return this.UpdateHeld[coindex.i];
			case Segment.FixedUpdate:
				return this.FixedUpdateHeld[coindex.i];
			case Segment.LateUpdate:
				return this.LateUpdateHeld[coindex.i];
			case Segment.SlowUpdate:
				return this.SlowUpdateHeld[coindex.i];
			case Segment.RealtimeUpdate:
				return this.RealtimeUpdateHeld[coindex.i];
			case Segment.EditorUpdate:
				return this.EditorUpdateHeld[coindex.i];
			case Segment.EditorSlowUpdate:
				return this.EditorSlowUpdateHeld[coindex.i];
			case Segment.EndOfFrame:
				return this.EndOfFrameHeld[coindex.i];
			case Segment.ManualTimeframe:
				return this.ManualTimeframeHeld[coindex.i];
			default:
				return false;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00032688 File Offset: 0x00030888
		private void CoindexReplace(Timing.ProcessIndex coindex, IEnumerator<float> replacement)
		{
			switch (coindex.seg)
			{
			case Segment.Update:
				this.UpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.FixedUpdate:
				this.FixedUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.LateUpdate:
				this.LateUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.SlowUpdate:
				this.SlowUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.RealtimeUpdate:
				this.RealtimeUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.EditorUpdate:
				this.EditorUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.EditorSlowUpdate:
				this.EditorSlowUpdateProcesses[coindex.i] = replacement;
				return;
			case Segment.EndOfFrame:
				this.EndOfFrameProcesses[coindex.i] = replacement;
				return;
			case Segment.ManualTimeframe:
				this.ManualTimeframeProcesses[coindex.i] = replacement;
				return;
			default:
				return;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00032750 File Offset: 0x00030950
		public static float WaitUntilDone(IEnumerator<float> newCoroutine)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, Timing.CurrentCoroutine.Segment), true);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00032778 File Offset: 0x00030978
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, string tag)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, Timing.CurrentCoroutine.Segment, tag), true);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000327A0 File Offset: 0x000309A0
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, int layer)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, Timing.CurrentCoroutine.Segment, layer), true);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000327C8 File Offset: 0x000309C8
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, int layer, string tag)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, Timing.CurrentCoroutine.Segment, layer, tag), true);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000327F0 File Offset: 0x000309F0
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment), true);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000327FF File Offset: 0x000309FF
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment, string tag)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, tag), true);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0003280F File Offset: 0x00030A0F
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment, int layer)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, layer), true);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0003281F File Offset: 0x00030A1F
		public static float WaitUntilDone(IEnumerator<float> newCoroutine, Segment segment, int layer, string tag)
		{
			return Timing.WaitUntilDone(Timing.RunCoroutine(newCoroutine, segment, layer, tag), true);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00032830 File Offset: 0x00030A30
		public static float WaitUntilDone(CoroutineHandle otherCoroutine)
		{
			return Timing.WaitUntilDone(otherCoroutine, true);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0003283C File Offset: 0x00030A3C
		public static float WaitUntilDone(CoroutineHandle otherCoroutine, bool warnOnIssue)
		{
			Timing instance = Timing.GetInstance(otherCoroutine.Key);
			if (!(instance != null) || !instance._handleToIndex.ContainsKey(otherCoroutine))
			{
				return 0f;
			}
			if (instance.CoindexIsNull(instance._handleToIndex[otherCoroutine]))
			{
				return 0f;
			}
			if (!instance._waitingTriggers.ContainsKey(otherCoroutine))
			{
				instance.CoindexReplace(instance._handleToIndex[otherCoroutine], instance._StartWhenDone(otherCoroutine, instance.CoindexPeek(instance._handleToIndex[otherCoroutine])));
				instance._waitingTriggers.Add(otherCoroutine, new HashSet<CoroutineHandle>());
			}
			if (instance.currentCoroutine == otherCoroutine)
			{
				return float.NegativeInfinity;
			}
			if (!instance.currentCoroutine.IsValid)
			{
				return float.NegativeInfinity;
			}
			instance._waitingTriggers[otherCoroutine].Add(instance.currentCoroutine);
			if (!instance._allWaiting.Contains(instance.currentCoroutine))
			{
				instance._allWaiting.Add(instance.currentCoroutine);
			}
			instance.SetHeld(instance._handleToIndex[instance.currentCoroutine], true);
			instance.SwapToLast(otherCoroutine, instance.currentCoroutine);
			return float.NaN;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00032970 File Offset: 0x00030B70
		public static void WaitForOtherHandles(CoroutineHandle handle, CoroutineHandle otherHandle, bool warnOnIssue = true)
		{
			if (!Timing.IsRunning(handle) || !Timing.IsRunning(otherHandle))
			{
				return;
			}
			if (handle == otherHandle)
			{
				return;
			}
			if (handle.Key != otherHandle.Key)
			{
				return;
			}
			Timing instance = Timing.GetInstance(handle.Key);
			if (instance != null && instance._handleToIndex.ContainsKey(handle) && instance._handleToIndex.ContainsKey(otherHandle) && !instance.CoindexIsNull(instance._handleToIndex[otherHandle]))
			{
				if (!instance._waitingTriggers.ContainsKey(otherHandle))
				{
					instance.CoindexReplace(instance._handleToIndex[otherHandle], instance._StartWhenDone(otherHandle, instance.CoindexPeek(instance._handleToIndex[otherHandle])));
					instance._waitingTriggers.Add(otherHandle, new HashSet<CoroutineHandle>());
				}
				instance._waitingTriggers[otherHandle].Add(handle);
				if (!instance._allWaiting.Contains(handle))
				{
					instance._allWaiting.Add(handle);
				}
				instance.SetHeld(instance._handleToIndex[handle], true);
				instance.SwapToLast(otherHandle, handle);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00032A90 File Offset: 0x00030C90
		public static void WaitForOtherHandles(CoroutineHandle handle, IEnumerable<CoroutineHandle> otherHandles, bool warnOnIssue = true)
		{
			if (!Timing.IsRunning(handle))
			{
				return;
			}
			Timing instance = Timing.GetInstance(handle.Key);
			IEnumerator<CoroutineHandle> enumerator = otherHandles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (Timing.IsRunning(enumerator.Current) && !(handle == enumerator.Current))
				{
					byte key = handle.Key;
					CoroutineHandle coroutineHandle = enumerator.Current;
					if (key == coroutineHandle.Key)
					{
						if (!instance._waitingTriggers.ContainsKey(enumerator.Current))
						{
							instance.CoindexReplace(instance._handleToIndex[enumerator.Current], instance._StartWhenDone(enumerator.Current, instance.CoindexPeek(instance._handleToIndex[enumerator.Current])));
							instance._waitingTriggers.Add(enumerator.Current, new HashSet<CoroutineHandle>());
						}
						instance._waitingTriggers[enumerator.Current].Add(handle);
						if (!instance._allWaiting.Contains(handle))
						{
							instance._allWaiting.Add(handle);
						}
						instance.SetHeld(instance._handleToIndex[handle], true);
						instance.SwapToLast(enumerator.Current, handle);
					}
				}
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00032BBC File Offset: 0x00030DBC
		private void SwapToLast(CoroutineHandle firstHandle, CoroutineHandle lastHandle)
		{
			if (firstHandle.Key != lastHandle.Key)
			{
				return;
			}
			Timing.ProcessIndex processIndex = this._handleToIndex[firstHandle];
			Timing.ProcessIndex processIndex2 = this._handleToIndex[lastHandle];
			if (processIndex.seg != processIndex2.seg || processIndex.i <= processIndex2.i)
			{
				return;
			}
			IEnumerator<float> replacement = this.CoindexPeek(processIndex);
			this.CoindexReplace(processIndex, this.CoindexPeek(processIndex2));
			this.CoindexReplace(processIndex2, replacement);
			this._indexToHandle[processIndex] = lastHandle;
			this._indexToHandle[processIndex2] = firstHandle;
			this._handleToIndex[firstHandle] = processIndex2;
			this._handleToIndex[lastHandle] = processIndex;
			bool flag = this.SetPause(processIndex, this.CoindexIsPaused(processIndex2));
			this.SetPause(processIndex2, flag);
			flag = this.SetHeld(processIndex, this.CoindexIsHeld(processIndex2));
			this.SetHeld(processIndex2, flag);
			if (this._waitingTriggers.ContainsKey(lastHandle))
			{
				foreach (CoroutineHandle lastHandle2 in this._waitingTriggers[lastHandle])
				{
					this.SwapToLast(lastHandle, lastHandle2);
				}
			}
			if (this._allWaiting.Contains(firstHandle))
			{
				foreach (KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair in this._waitingTriggers)
				{
					HashSet<CoroutineHandle>.Enumerator enumerator3 = keyValuePair.Value.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						if (enumerator3.Current == firstHandle)
						{
							Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>.Enumerator enumerator2;
							keyValuePair = enumerator2.Current;
							this.SwapToLast(keyValuePair.Key, firstHandle);
						}
					}
				}
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00032D3B File Offset: 0x00030F3B
		private IEnumerator<float> _StartWhenDone(CoroutineHandle handle, IEnumerator<float> proc)
		{
			if (!this._waitingTriggers.ContainsKey(handle))
			{
				yield break;
			}
			try
			{
				if (proc.Current > this.localTime)
				{
					yield return proc.Current;
				}
				while (proc.MoveNext())
				{
					float num = proc.Current;
					yield return num;
				}
			}
			finally
			{
				this.CloseWaitingProcess(handle);
			}
			yield break;
			yield break;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00032D58 File Offset: 0x00030F58
		private void CloseWaitingProcess(CoroutineHandle handle)
		{
			if (!this._waitingTriggers.ContainsKey(handle))
			{
				return;
			}
			HashSet<CoroutineHandle>.Enumerator enumerator = this._waitingTriggers[handle].GetEnumerator();
			this._waitingTriggers.Remove(handle);
			while (enumerator.MoveNext())
			{
				if (this._handleToIndex.ContainsKey(enumerator.Current) && !this.HandleIsInWaitingList(enumerator.Current))
				{
					this.SetHeld(this._handleToIndex[enumerator.Current], false);
					this._allWaiting.Remove(enumerator.Current);
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00032DF0 File Offset: 0x00030FF0
		private bool HandleIsInWaitingList(CoroutineHandle handle)
		{
			foreach (KeyValuePair<CoroutineHandle, HashSet<CoroutineHandle>> keyValuePair in this._waitingTriggers)
			{
				if (keyValuePair.Value.Contains(handle))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00032E2E File Offset: 0x0003102E
		private static IEnumerator<float> ReturnTmpRefForRepFunc(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			return Timing._tmpRef as IEnumerator<float>;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00032E3C File Offset: 0x0003103C
		public static float WaitUntilDone(AsyncOperation operation)
		{
			if (operation == null || operation.isDone)
			{
				return float.NaN;
			}
			CoroutineHandle currentCoroutine = Timing.CurrentCoroutine;
			Timing instance = Timing.GetInstance(Timing.CurrentCoroutine.Key);
			if (instance == null)
			{
				return float.NaN;
			}
			Timing._tmpRef = Timing._StartWhenDone(operation, instance.CoindexPeek(instance._handleToIndex[currentCoroutine]));
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			return float.NaN;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00032EB5 File Offset: 0x000310B5
		private static IEnumerator<float> _StartWhenDone(AsyncOperation operation, IEnumerator<float> pausedProc)
		{
			while (!operation.isDone)
			{
				yield return float.NegativeInfinity;
			}
			Timing._tmpRef = pausedProc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			yield return float.NaN;
			yield break;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00032ECC File Offset: 0x000310CC
		public static float WaitUntilDone(CustomYieldInstruction operation)
		{
			if (operation == null || !operation.keepWaiting)
			{
				return float.NaN;
			}
			CoroutineHandle currentCoroutine = Timing.CurrentCoroutine;
			Timing instance = Timing.GetInstance(Timing.CurrentCoroutine.Key);
			if (instance == null)
			{
				return float.NaN;
			}
			Timing._tmpRef = Timing._StartWhenDone(operation, instance.CoindexPeek(instance._handleToIndex[currentCoroutine]));
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			return float.NaN;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00032F45 File Offset: 0x00031145
		private static IEnumerator<float> _StartWhenDone(CustomYieldInstruction operation, IEnumerator<float> pausedProc)
		{
			while (operation.keepWaiting)
			{
				yield return float.NegativeInfinity;
			}
			Timing._tmpRef = pausedProc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			yield return float.NaN;
			yield break;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00032F5B File Offset: 0x0003115B
		public static float WaitUntilTrue(Func<bool> evaluatorFunc)
		{
			if (evaluatorFunc == null || evaluatorFunc())
			{
				return float.NaN;
			}
			Timing._tmpRef = evaluatorFunc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.WaitUntilTrueHelper);
			return float.NaN;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00032F8A File Offset: 0x0003118A
		private static IEnumerator<float> WaitUntilTrueHelper(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			return Timing._StartWhenDone(Timing._tmpRef as Func<bool>, false, coptr);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00032F9D File Offset: 0x0003119D
		public static float WaitUntilFalse(Func<bool> evaluatorFunc)
		{
			if (evaluatorFunc == null || !evaluatorFunc())
			{
				return float.NaN;
			}
			Timing._tmpRef = evaluatorFunc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.WaitUntilFalseHelper);
			return float.NaN;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00032FCC File Offset: 0x000311CC
		private static IEnumerator<float> WaitUntilFalseHelper(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			return Timing._StartWhenDone(Timing._tmpRef as Func<bool>, true, coptr);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00032FDF File Offset: 0x000311DF
		private static IEnumerator<float> _StartWhenDone(Func<bool> evaluatorFunc, bool continueOn, IEnumerator<float> pausedProc)
		{
			while (evaluatorFunc() == continueOn)
			{
				yield return float.NegativeInfinity;
			}
			Timing._tmpRef = pausedProc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			yield return float.NaN;
			yield break;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00032FFC File Offset: 0x000311FC
		private IEnumerator<float> _InjectDelay(IEnumerator<float> proc, float waitTime)
		{
			yield return this.WaitForSecondsOnInstance(waitTime);
			Timing._tmpRef = proc;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
			yield return float.NaN;
			yield break;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0003301C File Offset: 0x0003121C
		public bool LockCoroutine(CoroutineHandle coroutine, CoroutineHandle key)
		{
			if (coroutine.Key != this._instanceID || key == default(CoroutineHandle) || key.Key != 0)
			{
				return false;
			}
			if (!this._waitingTriggers.ContainsKey(key))
			{
				this._waitingTriggers.Add(key, new HashSet<CoroutineHandle>
				{
					coroutine
				});
			}
			else
			{
				this._waitingTriggers[key].Add(coroutine);
			}
			this._allWaiting.Add(coroutine);
			this.SetHeld(this._handleToIndex[coroutine], true);
			return true;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000330B4 File Offset: 0x000312B4
		public bool UnlockCoroutine(CoroutineHandle coroutine, CoroutineHandle key)
		{
			if (coroutine.Key != this._instanceID || key == default(CoroutineHandle) || !this._handleToIndex.ContainsKey(coroutine) || !this._waitingTriggers.ContainsKey(key))
			{
				return false;
			}
			if (this._waitingTriggers[key].Count == 1)
			{
				this._waitingTriggers.Remove(key);
			}
			else
			{
				this._waitingTriggers[key].Remove(coroutine);
			}
			if (!this.HandleIsInWaitingList(coroutine))
			{
				this.SetHeld(this._handleToIndex[coroutine], false);
				this._allWaiting.Remove(coroutine);
			}
			return true;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00033160 File Offset: 0x00031360
		public static int LinkCoroutines(CoroutineHandle master, CoroutineHandle slave)
		{
			if (!Timing.IsRunning(slave) || !master.IsValid)
			{
				return 0;
			}
			if (!Timing.IsRunning(master))
			{
				Timing.KillCoroutines(slave);
				return 1;
			}
			if (!Timing.Links.ContainsKey(master))
			{
				Timing.Links.Add(master, new HashSet<CoroutineHandle>
				{
					slave
				});
				return 1;
			}
			if (!Timing.Links[master].Contains(slave))
			{
				Timing.Links[master].Add(slave);
				return 1;
			}
			return 0;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000331E0 File Offset: 0x000313E0
		public static int UnlinkCoroutines(CoroutineHandle master, CoroutineHandle slave, bool twoWay = false)
		{
			int num = 0;
			if (Timing.Links.ContainsKey(master) && Timing.Links[master].Contains(slave))
			{
				if (Timing.Links[master].Count <= 1)
				{
					Timing.Links.Remove(master);
				}
				else
				{
					Timing.Links[master].Remove(slave);
				}
				num++;
			}
			if (twoWay && Timing.Links.ContainsKey(slave) && Timing.Links[slave].Contains(master))
			{
				if (Timing.Links[slave].Count <= 1)
				{
					Timing.Links.Remove(slave);
				}
				else
				{
					Timing.Links[slave].Remove(master);
				}
				num++;
			}
			return num;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000332A1 File Offset: 0x000314A1
		[Obsolete("Use Timing.CurrentCoroutine instead.", false)]
		public static float GetMyHandle(Action<CoroutineHandle> reciever)
		{
			Timing._tmpRef = reciever;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.GetHandleHelper);
			return float.NaN;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000332C0 File Offset: 0x000314C0
		private static IEnumerator<float> GetHandleHelper(IEnumerator<float> input, CoroutineHandle handle)
		{
			Action<CoroutineHandle> action = Timing._tmpRef as Action<CoroutineHandle>;
			if (action != null)
			{
				action(handle);
			}
			return input;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000332E3 File Offset: 0x000314E3
		public static float SwitchCoroutine(Segment newSegment)
		{
			Timing._tmpSegment = newSegment;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepS);
			return float.NaN;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00033301 File Offset: 0x00031501
		private static IEnumerator<float> SwitchCoroutineRepS(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing.GetInstance(handle.Key).RunCoroutineInternal(coptr, Timing._tmpSegment, 0, false, null, handle, false);
			return null;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00033321 File Offset: 0x00031521
		public static float SwitchCoroutine(Segment newSegment, string newTag)
		{
			Timing._tmpSegment = newSegment;
			Timing._tmpRef = newTag;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepST);
			return float.NaN;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00033348 File Offset: 0x00031548
		private static IEnumerator<float> SwitchCoroutineRepST(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			instance.RemoveTagOnInstance(handle);
			if (Timing._tmpRef is string)
			{
				instance.AddTagOnInstance((string)Timing._tmpRef, handle);
			}
			instance.RunCoroutineInternal(coptr, Timing._tmpSegment, 0, false, null, handle, false);
			return null;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00033399 File Offset: 0x00031599
		public static float SwitchCoroutine(Segment newSegment, int newLayer)
		{
			Timing._tmpSegment = newSegment;
			Timing._tmpInt = newLayer;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepSL);
			return float.NaN;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000333BD File Offset: 0x000315BD
		private static IEnumerator<float> SwitchCoroutineRepSL(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			Timing.RemoveLayer(handle);
			instance.AddLayerOnInstance(Timing._tmpInt, handle);
			instance.RunCoroutineInternal(coptr, Timing._tmpSegment, Timing._tmpInt, false, null, handle, false);
			return null;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000333F4 File Offset: 0x000315F4
		public static float SwitchCoroutine(Segment newSegment, int newLayer, string newTag)
		{
			Timing._tmpSegment = newSegment;
			Timing._tmpInt = newLayer;
			Timing._tmpRef = newTag;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepSLT);
			return float.NaN;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00033420 File Offset: 0x00031620
		private static IEnumerator<float> SwitchCoroutineRepSLT(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			instance.RemoveTagOnInstance(handle);
			if (Timing._tmpRef is string)
			{
				instance.AddTagOnInstance((string)Timing._tmpRef, handle);
			}
			Timing.RemoveLayer(handle);
			instance.AddLayerOnInstance(Timing._tmpInt, handle);
			instance.RunCoroutineInternal(coptr, Timing._tmpSegment, Timing._tmpInt, false, null, handle, false);
			return null;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00033488 File Offset: 0x00031688
		public static float SwitchCoroutine(string newTag)
		{
			Timing._tmpRef = newTag;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepT);
			return float.NaN;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000334A8 File Offset: 0x000316A8
		private static IEnumerator<float> SwitchCoroutineRepT(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			instance.RemoveTagOnInstance(handle);
			if (Timing._tmpRef is string)
			{
				instance.AddTagOnInstance((string)Timing._tmpRef, handle);
			}
			return coptr;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000334E7 File Offset: 0x000316E7
		public static float SwitchCoroutine(int newLayer)
		{
			Timing._tmpInt = newLayer;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepL);
			return float.NaN;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00033505 File Offset: 0x00031705
		private static IEnumerator<float> SwitchCoroutineRepL(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing.RemoveLayer(handle);
			Timing.GetInstance(handle.Key).AddLayerOnInstance(Timing._tmpInt, handle);
			return coptr;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00033526 File Offset: 0x00031726
		public static float SwitchCoroutine(int newLayer, string newTag)
		{
			Timing._tmpInt = newLayer;
			Timing._tmpRef = newTag;
			Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.SwitchCoroutineRepLT);
			return float.NaN;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0003354C File Offset: 0x0003174C
		private static IEnumerator<float> SwitchCoroutineRepLT(IEnumerator<float> coptr, CoroutineHandle handle)
		{
			Timing instance = Timing.GetInstance(handle.Key);
			instance.RemoveLayerOnInstance(handle);
			instance.AddLayerOnInstance(Timing._tmpInt, handle);
			instance.RemoveTagOnInstance(handle);
			if (Timing._tmpRef is string)
			{
				instance.AddTagOnInstance((string)Timing._tmpRef, handle);
			}
			return coptr;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000335A0 File Offset: 0x000317A0
		public static CoroutineHandle CallDelayed(float delay, Action action)
		{
			if (action != null)
			{
				return Timing.RunCoroutine(Timing.Instance._DelayedCall(delay, action, null));
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000335CC File Offset: 0x000317CC
		public CoroutineHandle CallDelayedOnInstance(float delay, Action action)
		{
			if (action != null)
			{
				return this.RunCoroutineOnInstance(this._DelayedCall(delay, action, null));
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000335F8 File Offset: 0x000317F8
		public static CoroutineHandle CallDelayed(float delay, Action action, GameObject gameObject)
		{
			if (action != null)
			{
				return Timing.RunCoroutine(Timing.Instance._DelayedCall(delay, action, gameObject), gameObject);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00033628 File Offset: 0x00031828
		public CoroutineHandle CallDelayedOnInstance(float delay, Action action, GameObject gameObject)
		{
			if (action != null)
			{
				return this.RunCoroutineOnInstance(this._DelayedCall(delay, action, gameObject), gameObject);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00033652 File Offset: 0x00031852
		private IEnumerator<float> _DelayedCall(float delay, Action action, GameObject cancelWith)
		{
			yield return this.WaitForSecondsOnInstance(delay);
			if (cancelWith == null || cancelWith != null)
			{
				action();
			}
			yield break;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00033678 File Offset: 0x00031878
		public static CoroutineHandle CallDelayed(float delay, Segment segment, Action action)
		{
			if (action != null)
			{
				return Timing.RunCoroutine(Timing.Instance._DelayedCall(delay, action, null), segment);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000336A8 File Offset: 0x000318A8
		public CoroutineHandle CallDelayedOnInstance(float delay, Segment segment, Action action)
		{
			if (action != null)
			{
				return this.RunCoroutineOnInstance(this._DelayedCall(delay, action, null), segment);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000336D4 File Offset: 0x000318D4
		public static CoroutineHandle CallDelayed(float delay, Segment segment, Action action, GameObject gameObject)
		{
			if (action != null)
			{
				return Timing.RunCoroutine(Timing.Instance._DelayedCall(delay, action, gameObject), segment, gameObject);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00033704 File Offset: 0x00031904
		public CoroutineHandle CallDelayedOnInstance(float delay, Segment segment, Action action, GameObject gameObject)
		{
			if (action != null)
			{
				return this.RunCoroutineOnInstance(this._DelayedCall(delay, action, gameObject), segment, gameObject);
			}
			return default(CoroutineHandle);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00033734 File Offset: 0x00031934
		public static CoroutineHandle CallPeriodically(float timeframe, float period, Action action, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(period, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, null, onDone));
			}
			return coroutineHandle;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00033780 File Offset: 0x00031980
		public CoroutineHandle CallPeriodicallyOnInstance(float timeframe, float period, Action action, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(period, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, null, onDone));
			}
			return coroutineHandle;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000337C8 File Offset: 0x000319C8
		public static CoroutineHandle CallPeriodically(float timeframe, float period, Action action, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(period, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0003381C File Offset: 0x00031A1C
		public CoroutineHandle CallPeriodicallyOnInstance(float timeframe, float period, Action action, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(period, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00033870 File Offset: 0x00031A70
		public static CoroutineHandle CallPeriodically(float timeframe, float period, Action action, Segment timing, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(period, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, null, onDone), timing);
			}
			return coroutineHandle;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000338C0 File Offset: 0x00031AC0
		public CoroutineHandle CallPeriodicallyOnInstance(float timeframe, float period, Action action, Segment timing, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(period, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00033910 File Offset: 0x00031B10
		public static CoroutineHandle CallPeriodically(float timeframe, float period, Action action, Segment timing, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(period, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0003396C File Offset: 0x00031B6C
		public CoroutineHandle CallPeriodicallyOnInstance(float timeframe, float period, Action action, Segment timing, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(period, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000339C4 File Offset: 0x00031BC4
		public static CoroutineHandle CallContinuously(float timeframe, Action action, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(0f, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00033A1C File Offset: 0x00031C1C
		public CoroutineHandle CallContinuouslyOnInstance(float timeframe, Action action, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(0f, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00033A6C File Offset: 0x00031C6C
		public static CoroutineHandle CallContinuously(float timeframe, Action action, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(0f, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00033AC4 File Offset: 0x00031CC4
		public CoroutineHandle CallContinuouslyOnInstance(float timeframe, Action action, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(0f, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00033B18 File Offset: 0x00031D18
		public static CoroutineHandle CallContinuously(float timeframe, Action action, Segment timing, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(0f, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00033B70 File Offset: 0x00031D70
		public CoroutineHandle CallContinuouslyOnInstance(float timeframe, Action action, Segment timing, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(0f, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00033BC4 File Offset: 0x00031DC4
		public static CoroutineHandle CallContinuously(float timeframe, Action action, Segment timing, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously(0f, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall(timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00033C20 File Offset: 0x00031E20
		public CoroutineHandle CallContinuouslyOnInstance(float timeframe, Action action, Segment timing, GameObject gameObject, Action onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously(0f, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall(timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00033C78 File Offset: 0x00031E78
		private IEnumerator<float> _WatchCall(float timeframe, CoroutineHandle handle, GameObject gObject, Action onDone)
		{
			yield return this.WaitForSecondsOnInstance(timeframe);
			this.KillCoroutinesOnInstance(handle);
			if (onDone != null && (gObject == null || gObject != null))
			{
				onDone();
			}
			yield break;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00033CA4 File Offset: 0x00031EA4
		private IEnumerator<float> _CallContinuously(float period, Action action, GameObject gObject)
		{
			while (gObject == null || gObject != null)
			{
				yield return this.WaitForSecondsOnInstance(period);
				if (gObject == null || (gObject != null && gObject.activeInHierarchy))
				{
					action();
				}
			}
			yield break;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00033CC8 File Offset: 0x00031EC8
		public static CoroutineHandle CallPeriodically<T>(T reference, float timeframe, float period, Action<T> action, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, period, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00033D1C File Offset: 0x00031F1C
		public CoroutineHandle CallPeriodicallyOnInstance<T>(T reference, float timeframe, float period, Action<T> action, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, period, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00033D6C File Offset: 0x00031F6C
		public static CoroutineHandle CallPeriodically<T>(T reference, float timeframe, float period, Action<T> action, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, period, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00033DC8 File Offset: 0x00031FC8
		public CoroutineHandle CallPeriodicallyOnInstance<T>(T reference, float timeframe, float period, Action<T> action, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, period, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00033E20 File Offset: 0x00032020
		public static CoroutineHandle CallPeriodically<T>(T reference, float timeframe, float period, Action<T> action, Segment timing, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, period, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00033E78 File Offset: 0x00032078
		public CoroutineHandle CallPeriodicallyOnInstance<T>(T reference, float timeframe, float period, Action<T> action, Segment timing, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, period, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00033ECC File Offset: 0x000320CC
		public static CoroutineHandle CallPeriodically<T>(T reference, float timeframe, float period, Action<T> action, Segment timing, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, period, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00033F2C File Offset: 0x0003212C
		public CoroutineHandle CallPeriodicallyOnInstance<T>(T reference, float timeframe, float period, Action<T> action, Segment timing, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, period, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00033F88 File Offset: 0x00032188
		public static CoroutineHandle CallContinuously<T>(T reference, float timeframe, Action<T> action, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, 0f, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00033FE0 File Offset: 0x000321E0
		public CoroutineHandle CallContinuouslyOnInstance<T>(T reference, float timeframe, Action<T> action, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, 0f, action, null));
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone)));
			}
			return coroutineHandle;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00034034 File Offset: 0x00032234
		public static CoroutineHandle CallContinuously<T>(T reference, float timeframe, Action<T> action, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, 0f, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00034090 File Offset: 0x00032290
		public CoroutineHandle CallContinuouslyOnInstance<T>(T reference, float timeframe, Action<T> action, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, 0f, action, gameObject), gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000340E8 File Offset: 0x000322E8
		public static CoroutineHandle CallContinuously<T>(T reference, float timeframe, Action<T> action, Segment timing, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, 0f, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00034144 File Offset: 0x00032344
		public CoroutineHandle CallContinuouslyOnInstance<T>(T reference, float timeframe, Action<T> action, Segment timing, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, 0f, action, null), timing);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, null, onDone), timing));
			}
			return coroutineHandle;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0003419C File Offset: 0x0003239C
		public static CoroutineHandle CallContinuously<T>(T reference, float timeframe, Action<T> action, Segment timing, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : Timing.RunCoroutine(Timing.Instance._CallContinuously<T>(reference, 0f, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, Timing.RunCoroutine(Timing.Instance._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000341FC File Offset: 0x000323FC
		public CoroutineHandle CallContinuouslyOnInstance<T>(T reference, float timeframe, Action<T> action, Segment timing, GameObject gameObject, Action<T> onDone = null)
		{
			CoroutineHandle coroutineHandle = (action == null) ? default(CoroutineHandle) : this.RunCoroutineOnInstance(this._CallContinuously<T>(reference, 0f, action, gameObject), timing, gameObject);
			if (!float.IsPositiveInfinity(timeframe))
			{
				Timing.LinkCoroutines(coroutineHandle, this.RunCoroutineOnInstance(this._WatchCall<T>(reference, timeframe, coroutineHandle, gameObject, onDone), timing, gameObject));
			}
			return coroutineHandle;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00034258 File Offset: 0x00032458
		private IEnumerator<float> _WatchCall<T>(T reference, float timeframe, CoroutineHandle handle, GameObject gObject, Action<T> onDone)
		{
			yield return this.WaitForSecondsOnInstance(timeframe);
			this.KillCoroutinesOnInstance(handle);
			if (onDone != null && (gObject == null || gObject != null))
			{
				onDone(reference);
			}
			yield break;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0003428C File Offset: 0x0003248C
		private IEnumerator<float> _CallContinuously<T>(T reference, float period, Action<T> action, GameObject gObject)
		{
			while (gObject == null || gObject != null)
			{
				yield return this.WaitForSecondsOnInstance(period);
				if (gObject == null || (gObject != null && gObject.activeInHierarchy))
				{
					action(reference);
				}
			}
			yield break;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000342B8 File Offset: 0x000324B8
		[Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
		public new Coroutine StartCoroutine(IEnumerator routine)
		{
			return null;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000342BB File Offset: 0x000324BB
		[Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
		public new Coroutine StartCoroutine(string methodName, object value)
		{
			return null;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000342BE File Offset: 0x000324BE
		[Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
		public new Coroutine StartCoroutine(string methodName)
		{
			return null;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000342C1 File Offset: 0x000324C1
		[Obsolete("Unity coroutine function, use RunCoroutine instead.", true)]
		public new Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return null;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000342C4 File Offset: 0x000324C4
		[Obsolete("Unity coroutine function, use KillCoroutines instead.", true)]
		public new void StopCoroutine(string methodName)
		{
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000342C6 File Offset: 0x000324C6
		[Obsolete("Unity coroutine function, use KillCoroutines instead.", true)]
		public new void StopCoroutine(IEnumerator routine)
		{
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000342C8 File Offset: 0x000324C8
		[Obsolete("Unity coroutine function, use KillCoroutines instead.", true)]
		public new void StopCoroutine(Coroutine routine)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000342CA File Offset: 0x000324CA
		[Obsolete("Unity coroutine function, use KillCoroutines instead.", true)]
		public new void StopAllCoroutines()
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000342CC File Offset: 0x000324CC
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void Destroy(UnityEngine.Object obj)
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000342CE File Offset: 0x000324CE
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void Destroy(UnityEngine.Object obj, float f)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000342D0 File Offset: 0x000324D0
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void DestroyObject(UnityEngine.Object obj)
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000342D2 File Offset: 0x000324D2
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void DestroyObject(UnityEngine.Object obj, float f)
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000342D4 File Offset: 0x000324D4
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void DestroyImmediate(UnityEngine.Object obj)
		{
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000342D6 File Offset: 0x000324D6
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void DestroyImmediate(UnityEngine.Object obj, bool b)
		{
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000342D8 File Offset: 0x000324D8
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void Instantiate(UnityEngine.Object obj)
		{
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000342DA File Offset: 0x000324DA
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation)
		{
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000342DC File Offset: 0x000324DC
		[Obsolete("Use your own GameObject for this.", true)]
		public new static void Instantiate<T>(T original) where T : UnityEngine.Object
		{
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000342E0 File Offset: 0x000324E0
		[Obsolete("Just.. no.", true)]
		public new static T FindObjectOfType<T>() where T : UnityEngine.Object
		{
			return default(T);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000342F6 File Offset: 0x000324F6
		[Obsolete("Just.. no.", true)]
		public new static UnityEngine.Object FindObjectOfType(Type t)
		{
			return null;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000342F9 File Offset: 0x000324F9
		[Obsolete("Just.. no.", true)]
		public new static T[] FindObjectsOfType<T>() where T : UnityEngine.Object
		{
			return null;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000342FC File Offset: 0x000324FC
		[Obsolete("Just.. no.", true)]
		public new static UnityEngine.Object[] FindObjectsOfType(Type t)
		{
			return null;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000342FF File Offset: 0x000324FF
		[Obsolete("Just.. no.", true)]
		public new static void print(object message)
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00034304 File Offset: 0x00032504
		public Timing()
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000344DE File Offset: 0x000326DE
		// Note: this type is marked as 'beforefieldinit'.
		static Timing()
		{
		}

		// Token: 0x04000584 RID: 1412
		[Tooltip("How quickly the SlowUpdate segment ticks.")]
		public float TimeBetweenSlowUpdateCalls = 0.14285715f;

		// Token: 0x04000585 RID: 1413
		[Tooltip("How much data should be sent to the profiler window when it's open.")]
		public DebugInfoType ProfilerDebugAmount;

		// Token: 0x04000586 RID: 1414
		[Tooltip("When using manual timeframe, should it run automatically after the update loop or only when TriggerManualTimframeUpdate is called.")]
		public bool AutoTriggerManualTimeframe = true;

		// Token: 0x04000587 RID: 1415
		[Tooltip("A count of the number of Update coroutines that are currently running.")]
		[Space(12f)]
		public int UpdateCoroutines;

		// Token: 0x04000588 RID: 1416
		[Tooltip("A count of the number of FixedUpdate coroutines that are currently running.")]
		public int FixedUpdateCoroutines;

		// Token: 0x04000589 RID: 1417
		[Tooltip("A count of the number of LateUpdate coroutines that are currently running.")]
		public int LateUpdateCoroutines;

		// Token: 0x0400058A RID: 1418
		[Tooltip("A count of the number of SlowUpdate coroutines that are currently running.")]
		public int SlowUpdateCoroutines;

		// Token: 0x0400058B RID: 1419
		[Tooltip("A count of the number of RealtimeUpdate coroutines that are currently running.")]
		public int RealtimeUpdateCoroutines;

		// Token: 0x0400058C RID: 1420
		[Tooltip("A count of the number of EditorUpdate coroutines that are currently running.")]
		public int EditorUpdateCoroutines;

		// Token: 0x0400058D RID: 1421
		[Tooltip("A count of the number of EditorSlowUpdate coroutines that are currently running.")]
		public int EditorSlowUpdateCoroutines;

		// Token: 0x0400058E RID: 1422
		[Tooltip("A count of the number of EndOfFrame coroutines that are currently running.")]
		public int EndOfFrameCoroutines;

		// Token: 0x0400058F RID: 1423
		[Tooltip("A count of the number of ManualTimeframe coroutines that are currently running.")]
		public int ManualTimeframeCoroutines;

		// Token: 0x04000590 RID: 1424
		[NonSerialized]
		public float localTime;

		// Token: 0x04000591 RID: 1425
		[NonSerialized]
		public float deltaTime;

		// Token: 0x04000592 RID: 1426
		public Func<float, float> SetManualTimeframeTime;

		// Token: 0x04000593 RID: 1427
		public static Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>> ReplacementFunction;

		// Token: 0x04000594 RID: 1428
		[CompilerGenerated]
		private static Action OnPreExecute;

		// Token: 0x04000595 RID: 1429
		public const float WaitForOneFrame = float.NegativeInfinity;

		// Token: 0x04000596 RID: 1430
		[CompilerGenerated]
		private static Thread <MainThread>k__BackingField;

		// Token: 0x04000597 RID: 1431
		[CompilerGenerated]
		private CoroutineHandle <currentCoroutine>k__BackingField;

		// Token: 0x04000598 RID: 1432
		private static object _tmpRef;

		// Token: 0x04000599 RID: 1433
		private static int _tmpInt;

		// Token: 0x0400059A RID: 1434
		private static bool _tmpBool;

		// Token: 0x0400059B RID: 1435
		private static Segment _tmpSegment;

		// Token: 0x0400059C RID: 1436
		private static CoroutineHandle _tmpHandle;

		// Token: 0x0400059D RID: 1437
		private int _currentUpdateFrame;

		// Token: 0x0400059E RID: 1438
		private int _currentLateUpdateFrame;

		// Token: 0x0400059F RID: 1439
		private int _currentSlowUpdateFrame;

		// Token: 0x040005A0 RID: 1440
		private int _currentRealtimeUpdateFrame;

		// Token: 0x040005A1 RID: 1441
		private int _currentEndOfFrameFrame;

		// Token: 0x040005A2 RID: 1442
		private int _nextUpdateProcessSlot;

		// Token: 0x040005A3 RID: 1443
		private int _nextLateUpdateProcessSlot;

		// Token: 0x040005A4 RID: 1444
		private int _nextFixedUpdateProcessSlot;

		// Token: 0x040005A5 RID: 1445
		private int _nextSlowUpdateProcessSlot;

		// Token: 0x040005A6 RID: 1446
		private int _nextRealtimeUpdateProcessSlot;

		// Token: 0x040005A7 RID: 1447
		private int _nextEditorUpdateProcessSlot;

		// Token: 0x040005A8 RID: 1448
		private int _nextEditorSlowUpdateProcessSlot;

		// Token: 0x040005A9 RID: 1449
		private int _nextEndOfFrameProcessSlot;

		// Token: 0x040005AA RID: 1450
		private int _nextManualTimeframeProcessSlot;

		// Token: 0x040005AB RID: 1451
		private int _lastUpdateProcessSlot;

		// Token: 0x040005AC RID: 1452
		private int _lastLateUpdateProcessSlot;

		// Token: 0x040005AD RID: 1453
		private int _lastFixedUpdateProcessSlot;

		// Token: 0x040005AE RID: 1454
		private int _lastSlowUpdateProcessSlot;

		// Token: 0x040005AF RID: 1455
		private int _lastRealtimeUpdateProcessSlot;

		// Token: 0x040005B0 RID: 1456
		private int _lastEndOfFrameProcessSlot;

		// Token: 0x040005B1 RID: 1457
		private int _lastManualTimeframeProcessSlot;

		// Token: 0x040005B2 RID: 1458
		private float _lastUpdateTime;

		// Token: 0x040005B3 RID: 1459
		private float _lastLateUpdateTime;

		// Token: 0x040005B4 RID: 1460
		private float _lastFixedUpdateTime;

		// Token: 0x040005B5 RID: 1461
		private float _lastSlowUpdateTime;

		// Token: 0x040005B6 RID: 1462
		private float _lastRealtimeUpdateTime;

		// Token: 0x040005B7 RID: 1463
		private float _lastEndOfFrameTime;

		// Token: 0x040005B8 RID: 1464
		private float _lastManualTimeframeTime;

		// Token: 0x040005B9 RID: 1465
		private float _lastSlowUpdateDeltaTime;

		// Token: 0x040005BA RID: 1466
		private float _lastEditorUpdateDeltaTime;

		// Token: 0x040005BB RID: 1467
		private float _lastEditorSlowUpdateDeltaTime;

		// Token: 0x040005BC RID: 1468
		private float _lastManualTimeframeDeltaTime;

		// Token: 0x040005BD RID: 1469
		private ushort _framesSinceUpdate;

		// Token: 0x040005BE RID: 1470
		private ushort _expansions = 1;

		// Token: 0x040005BF RID: 1471
		[SerializeField]
		[HideInInspector]
		private byte _instanceID;

		// Token: 0x040005C0 RID: 1472
		private bool _EOFPumpRan;

		// Token: 0x040005C1 RID: 1473
		private static readonly Dictionary<CoroutineHandle, HashSet<CoroutineHandle>> Links = new Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>();

		// Token: 0x040005C2 RID: 1474
		private static readonly WaitForEndOfFrame EofWaitObject = new WaitForEndOfFrame();

		// Token: 0x040005C3 RID: 1475
		private readonly Dictionary<CoroutineHandle, HashSet<CoroutineHandle>> _waitingTriggers = new Dictionary<CoroutineHandle, HashSet<CoroutineHandle>>();

		// Token: 0x040005C4 RID: 1476
		private readonly HashSet<CoroutineHandle> _allWaiting = new HashSet<CoroutineHandle>();

		// Token: 0x040005C5 RID: 1477
		private readonly Dictionary<CoroutineHandle, Timing.ProcessIndex> _handleToIndex = new Dictionary<CoroutineHandle, Timing.ProcessIndex>();

		// Token: 0x040005C6 RID: 1478
		private readonly Dictionary<Timing.ProcessIndex, CoroutineHandle> _indexToHandle = new Dictionary<Timing.ProcessIndex, CoroutineHandle>();

		// Token: 0x040005C7 RID: 1479
		private readonly Dictionary<CoroutineHandle, string> _processTags = new Dictionary<CoroutineHandle, string>();

		// Token: 0x040005C8 RID: 1480
		private readonly Dictionary<string, HashSet<CoroutineHandle>> _taggedProcesses = new Dictionary<string, HashSet<CoroutineHandle>>();

		// Token: 0x040005C9 RID: 1481
		private readonly Dictionary<CoroutineHandle, int> _processLayers = new Dictionary<CoroutineHandle, int>();

		// Token: 0x040005CA RID: 1482
		private readonly Dictionary<int, HashSet<CoroutineHandle>> _layeredProcesses = new Dictionary<int, HashSet<CoroutineHandle>>();

		// Token: 0x040005CB RID: 1483
		private IEnumerator<float>[] UpdateProcesses = new IEnumerator<float>[256];

		// Token: 0x040005CC RID: 1484
		private IEnumerator<float>[] LateUpdateProcesses = new IEnumerator<float>[8];

		// Token: 0x040005CD RID: 1485
		private IEnumerator<float>[] FixedUpdateProcesses = new IEnumerator<float>[64];

		// Token: 0x040005CE RID: 1486
		private IEnumerator<float>[] SlowUpdateProcesses = new IEnumerator<float>[64];

		// Token: 0x040005CF RID: 1487
		private IEnumerator<float>[] RealtimeUpdateProcesses = new IEnumerator<float>[8];

		// Token: 0x040005D0 RID: 1488
		private IEnumerator<float>[] EditorUpdateProcesses = new IEnumerator<float>[8];

		// Token: 0x040005D1 RID: 1489
		private IEnumerator<float>[] EditorSlowUpdateProcesses = new IEnumerator<float>[8];

		// Token: 0x040005D2 RID: 1490
		private IEnumerator<float>[] EndOfFrameProcesses = new IEnumerator<float>[8];

		// Token: 0x040005D3 RID: 1491
		private IEnumerator<float>[] ManualTimeframeProcesses = new IEnumerator<float>[8];

		// Token: 0x040005D4 RID: 1492
		private bool[] UpdatePaused = new bool[256];

		// Token: 0x040005D5 RID: 1493
		private bool[] LateUpdatePaused = new bool[8];

		// Token: 0x040005D6 RID: 1494
		private bool[] FixedUpdatePaused = new bool[64];

		// Token: 0x040005D7 RID: 1495
		private bool[] SlowUpdatePaused = new bool[64];

		// Token: 0x040005D8 RID: 1496
		private bool[] RealtimeUpdatePaused = new bool[8];

		// Token: 0x040005D9 RID: 1497
		private bool[] EditorUpdatePaused = new bool[8];

		// Token: 0x040005DA RID: 1498
		private bool[] EditorSlowUpdatePaused = new bool[8];

		// Token: 0x040005DB RID: 1499
		private bool[] EndOfFramePaused = new bool[8];

		// Token: 0x040005DC RID: 1500
		private bool[] ManualTimeframePaused = new bool[8];

		// Token: 0x040005DD RID: 1501
		private bool[] UpdateHeld = new bool[256];

		// Token: 0x040005DE RID: 1502
		private bool[] LateUpdateHeld = new bool[8];

		// Token: 0x040005DF RID: 1503
		private bool[] FixedUpdateHeld = new bool[64];

		// Token: 0x040005E0 RID: 1504
		private bool[] SlowUpdateHeld = new bool[64];

		// Token: 0x040005E1 RID: 1505
		private bool[] RealtimeUpdateHeld = new bool[8];

		// Token: 0x040005E2 RID: 1506
		private bool[] EditorUpdateHeld = new bool[8];

		// Token: 0x040005E3 RID: 1507
		private bool[] EditorSlowUpdateHeld = new bool[8];

		// Token: 0x040005E4 RID: 1508
		private bool[] EndOfFrameHeld = new bool[8];

		// Token: 0x040005E5 RID: 1509
		private bool[] ManualTimeframeHeld = new bool[8];

		// Token: 0x040005E6 RID: 1510
		private CoroutineHandle _eofWatcherHandle;

		// Token: 0x040005E7 RID: 1511
		private const ushort FramesUntilMaintenance = 64;

		// Token: 0x040005E8 RID: 1512
		private const int ProcessArrayChunkSize = 64;

		// Token: 0x040005E9 RID: 1513
		private const int InitialBufferSizeLarge = 256;

		// Token: 0x040005EA RID: 1514
		private const int InitialBufferSizeMedium = 64;

		// Token: 0x040005EB RID: 1515
		private const int InitialBufferSizeSmall = 8;

		// Token: 0x040005EC RID: 1516
		private static Timing[] ActiveInstances = new Timing[16];

		// Token: 0x040005ED RID: 1517
		private static Timing _instance;

		// Token: 0x020001D1 RID: 465
		private struct ProcessIndex : IEquatable<Timing.ProcessIndex>
		{
			// Token: 0x06000FAC RID: 4012 RVA: 0x00063BA8 File Offset: 0x00061DA8
			public bool Equals(Timing.ProcessIndex other)
			{
				return this.seg == other.seg && this.i == other.i;
			}

			// Token: 0x06000FAD RID: 4013 RVA: 0x00063BC8 File Offset: 0x00061DC8
			public override bool Equals(object other)
			{
				return other is Timing.ProcessIndex && this.Equals((Timing.ProcessIndex)other);
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x00063BE0 File Offset: 0x00061DE0
			public static bool operator ==(Timing.ProcessIndex a, Timing.ProcessIndex b)
			{
				return a.seg == b.seg && a.i == b.i;
			}

			// Token: 0x06000FAF RID: 4015 RVA: 0x00063C00 File Offset: 0x00061E00
			public static bool operator !=(Timing.ProcessIndex a, Timing.ProcessIndex b)
			{
				return a.seg != b.seg || a.i != b.i;
			}

			// Token: 0x06000FB0 RID: 4016 RVA: 0x00063C23 File Offset: 0x00061E23
			public override int GetHashCode()
			{
				return (this.seg - Segment.RealtimeUpdate) * 306783378 + this.i;
			}

			// Token: 0x04000E08 RID: 3592
			public Segment seg;

			// Token: 0x04000E09 RID: 3593
			public int i;
		}

		// Token: 0x020001D2 RID: 466
		[CompilerGenerated]
		private sealed class <_EOFPumpWatcher>d__132 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FB1 RID: 4017 RVA: 0x00063C3A File Offset: 0x00061E3A
			[DebuggerHidden]
			public <_EOFPumpWatcher>d__132(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FB2 RID: 4018 RVA: 0x00063C49 File Offset: 0x00061E49
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x00063C4C File Offset: 0x00061E4C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				if (timing._nextEndOfFrameProcessSlot <= 0)
				{
					timing._EOFPumpRan = false;
					return false;
				}
				if (!timing._EOFPumpRan)
				{
					timing.StartCoroutine(timing._EOFPump());
				}
				timing._EOFPumpRan = false;
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00063CC1 File Offset: 0x00061EC1
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FB5 RID: 4021 RVA: 0x00063CC9 File Offset: 0x00061EC9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00063CD0 File Offset: 0x00061ED0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E0A RID: 3594
			private int <>1__state;

			// Token: 0x04000E0B RID: 3595
			private float <>2__current;

			// Token: 0x04000E0C RID: 3596
			public Timing <>4__this;
		}

		// Token: 0x020001D3 RID: 467
		[CompilerGenerated]
		private sealed class <_EOFPump>d__133 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000FB7 RID: 4023 RVA: 0x00063CDD File Offset: 0x00061EDD
			[DebuggerHidden]
			public <_EOFPump>d__133(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FB8 RID: 4024 RVA: 0x00063CEC File Offset: 0x00061EEC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FB9 RID: 4025 RVA: 0x00063CF0 File Offset: 0x00061EF0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (Timing.OnPreExecute != null)
					{
						Timing.OnPreExecute();
					}
					Timing.ProcessIndex processIndex = new Timing.ProcessIndex
					{
						seg = Segment.EndOfFrame
					};
					timing._EOFPumpRan = true;
					if (timing.UpdateTimeValues(processIndex.seg))
					{
						timing._lastEndOfFrameProcessSlot = timing._nextEndOfFrameProcessSlot;
					}
					processIndex.i = 0;
					while (processIndex.i < timing._lastEndOfFrameProcessSlot)
					{
						try
						{
							if (!timing.EndOfFramePaused[processIndex.i] && !timing.EndOfFrameHeld[processIndex.i] && timing.EndOfFrameProcesses[processIndex.i] != null && timing.localTime >= timing.EndOfFrameProcesses[processIndex.i].Current)
							{
								timing.currentCoroutine = timing._indexToHandle[processIndex];
								if (timing.ProfilerDebugAmount != DebugInfoType.None)
								{
									timing._indexToHandle.ContainsKey(processIndex);
								}
								if (!timing.EndOfFrameProcesses[processIndex.i].MoveNext())
								{
									if (timing._indexToHandle.ContainsKey(processIndex))
									{
										timing.KillCoroutinesOnInstance(timing._indexToHandle[processIndex]);
									}
								}
								else if (timing.EndOfFrameProcesses[processIndex.i] != null && float.IsNaN(timing.EndOfFrameProcesses[processIndex.i].Current))
								{
									if (Timing.ReplacementFunction != null)
									{
										timing.EndOfFrameProcesses[processIndex.i] = Timing.ReplacementFunction(timing.EndOfFrameProcesses[processIndex.i], timing._indexToHandle[processIndex]);
										Timing.ReplacementFunction = null;
									}
									processIndex.i--;
								}
								DebugInfoType profilerDebugAmount = timing.ProfilerDebugAmount;
							}
						}
						catch (Exception ex)
						{
							UnityEngine.Debug.LogException(ex);
							if (ex is MissingReferenceException)
							{
								UnityEngine.Debug.LogError("This exception can probably be fixed by adding \"CancelWith(gameObject)\" when you run the coroutine.\nExample: Timing.RunCoroutine(_foo().CancelWith(gameObject), Segment.EndOfFrame);");
							}
						}
						processIndex.i++;
					}
				}
				else
				{
					this.<>1__state = -1;
				}
				if (timing._nextEndOfFrameProcessSlot <= 0)
				{
					timing.currentCoroutine = default(CoroutineHandle);
					return false;
				}
				this.<>2__current = Timing.EofWaitObject;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00063F2C File Offset: 0x0006212C
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FBB RID: 4027 RVA: 0x00063F34 File Offset: 0x00062134
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00063F3B File Offset: 0x0006213B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E0D RID: 3597
			private int <>1__state;

			// Token: 0x04000E0E RID: 3598
			private object <>2__current;

			// Token: 0x04000E0F RID: 3599
			public Timing <>4__this;
		}

		// Token: 0x020001D4 RID: 468
		[CompilerGenerated]
		private sealed class <_StartWhenDone>d__275 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FBD RID: 4029 RVA: 0x00063F43 File Offset: 0x00062143
			[DebuggerHidden]
			public <_StartWhenDone>d__275(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FBE RID: 4030 RVA: 0x00063F54 File Offset: 0x00062154
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
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

			// Token: 0x06000FBF RID: 4031 RVA: 0x00063F90 File Offset: 0x00062190
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					Timing timing = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						if (!timing._waitingTriggers.ContainsKey(handle))
						{
							return false;
						}
						this.<>1__state = -3;
						if (proc.Current > timing.localTime)
						{
							this.<>2__current = proc.Current;
							this.<>1__state = 1;
							return true;
						}
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -3;
						break;
					default:
						return false;
					}
					if (!proc.MoveNext())
					{
						this.<>m__Finally1();
						result = false;
					}
					else
					{
						this.<>2__current = proc.Current;
						this.<>1__state = 2;
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

			// Token: 0x06000FC0 RID: 4032 RVA: 0x0006407C File Offset: 0x0006227C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				base.CloseWaitingProcess(handle);
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00064096 File Offset: 0x00062296
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FC2 RID: 4034 RVA: 0x0006409E File Offset: 0x0006229E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000640A5 File Offset: 0x000622A5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E10 RID: 3600
			private int <>1__state;

			// Token: 0x04000E11 RID: 3601
			private float <>2__current;

			// Token: 0x04000E12 RID: 3602
			public Timing <>4__this;

			// Token: 0x04000E13 RID: 3603
			public CoroutineHandle handle;

			// Token: 0x04000E14 RID: 3604
			public IEnumerator<float> proc;
		}

		// Token: 0x020001D5 RID: 469
		[CompilerGenerated]
		private sealed class <_StartWhenDone>d__280 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FC4 RID: 4036 RVA: 0x000640B2 File Offset: 0x000622B2
			[DebuggerHidden]
			public <_StartWhenDone>d__280(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FC5 RID: 4037 RVA: 0x000640C1 File Offset: 0x000622C1
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FC6 RID: 4038 RVA: 0x000640C4 File Offset: 0x000622C4
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (operation.isDone)
				{
					Timing._tmpRef = pausedProc;
					Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
					this.<>2__current = float.NaN;
					this.<>1__state = 2;
					return true;
				}
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00064155 File Offset: 0x00062355
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FC8 RID: 4040 RVA: 0x0006415D File Offset: 0x0006235D
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00064164 File Offset: 0x00062364
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E15 RID: 3605
			private int <>1__state;

			// Token: 0x04000E16 RID: 3606
			private float <>2__current;

			// Token: 0x04000E17 RID: 3607
			public AsyncOperation operation;

			// Token: 0x04000E18 RID: 3608
			public IEnumerator<float> pausedProc;
		}

		// Token: 0x020001D6 RID: 470
		[CompilerGenerated]
		private sealed class <_StartWhenDone>d__282 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FCA RID: 4042 RVA: 0x00064171 File Offset: 0x00062371
			[DebuggerHidden]
			public <_StartWhenDone>d__282(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FCB RID: 4043 RVA: 0x00064180 File Offset: 0x00062380
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FCC RID: 4044 RVA: 0x00064184 File Offset: 0x00062384
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (!operation.keepWaiting)
				{
					Timing._tmpRef = pausedProc;
					Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
					this.<>2__current = float.NaN;
					this.<>1__state = 2;
					return true;
				}
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00064215 File Offset: 0x00062415
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FCE RID: 4046 RVA: 0x0006421D File Offset: 0x0006241D
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00064224 File Offset: 0x00062424
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E19 RID: 3609
			private int <>1__state;

			// Token: 0x04000E1A RID: 3610
			private float <>2__current;

			// Token: 0x04000E1B RID: 3611
			public CustomYieldInstruction operation;

			// Token: 0x04000E1C RID: 3612
			public IEnumerator<float> pausedProc;
		}

		// Token: 0x020001D7 RID: 471
		[CompilerGenerated]
		private sealed class <_StartWhenDone>d__287 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FD0 RID: 4048 RVA: 0x00064231 File Offset: 0x00062431
			[DebuggerHidden]
			public <_StartWhenDone>d__287(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FD1 RID: 4049 RVA: 0x00064240 File Offset: 0x00062440
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FD2 RID: 4050 RVA: 0x00064244 File Offset: 0x00062444
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (evaluatorFunc() != continueOn)
				{
					Timing._tmpRef = pausedProc;
					Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
					this.<>2__current = float.NaN;
					this.<>1__state = 2;
					return true;
				}
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x000642DB File Offset: 0x000624DB
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FD4 RID: 4052 RVA: 0x000642E3 File Offset: 0x000624E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000642EA File Offset: 0x000624EA
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E1D RID: 3613
			private int <>1__state;

			// Token: 0x04000E1E RID: 3614
			private float <>2__current;

			// Token: 0x04000E1F RID: 3615
			public Func<bool> evaluatorFunc;

			// Token: 0x04000E20 RID: 3616
			public bool continueOn;

			// Token: 0x04000E21 RID: 3617
			public IEnumerator<float> pausedProc;
		}

		// Token: 0x020001D8 RID: 472
		[CompilerGenerated]
		private sealed class <_InjectDelay>d__288 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FD6 RID: 4054 RVA: 0x000642F7 File Offset: 0x000624F7
			[DebuggerHidden]
			public <_InjectDelay>d__288(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FD7 RID: 4055 RVA: 0x00064306 File Offset: 0x00062506
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FD8 RID: 4056 RVA: 0x00064308 File Offset: 0x00062508
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = timing.WaitForSecondsOnInstance(waitTime);
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					Timing._tmpRef = proc;
					Timing.ReplacementFunction = new Func<IEnumerator<float>, CoroutineHandle, IEnumerator<float>>(Timing.ReturnTmpRefForRepFunc);
					this.<>2__current = float.NaN;
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00064398 File Offset: 0x00062598
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FDA RID: 4058 RVA: 0x000643A0 File Offset: 0x000625A0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000643A7 File Offset: 0x000625A7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E22 RID: 3618
			private int <>1__state;

			// Token: 0x04000E23 RID: 3619
			private float <>2__current;

			// Token: 0x04000E24 RID: 3620
			public Timing <>4__this;

			// Token: 0x04000E25 RID: 3621
			public float waitTime;

			// Token: 0x04000E26 RID: 3622
			public IEnumerator<float> proc;
		}

		// Token: 0x020001D9 RID: 473
		[CompilerGenerated]
		private sealed class <_DelayedCall>d__313 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FDC RID: 4060 RVA: 0x000643B4 File Offset: 0x000625B4
			[DebuggerHidden]
			public <_DelayedCall>d__313(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FDD RID: 4061 RVA: 0x000643C3 File Offset: 0x000625C3
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FDE RID: 4062 RVA: 0x000643C8 File Offset: 0x000625C8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = timing.WaitForSecondsOnInstance(delay);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				if (cancelWith == null || cancelWith != null)
				{
					action();
				}
				return false;
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00064437 File Offset: 0x00062637
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FE0 RID: 4064 RVA: 0x0006443F File Offset: 0x0006263F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00064446 File Offset: 0x00062646
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E27 RID: 3623
			private int <>1__state;

			// Token: 0x04000E28 RID: 3624
			private float <>2__current;

			// Token: 0x04000E29 RID: 3625
			public Timing <>4__this;

			// Token: 0x04000E2A RID: 3626
			public float delay;

			// Token: 0x04000E2B RID: 3627
			public GameObject cancelWith;

			// Token: 0x04000E2C RID: 3628
			public Action action;
		}

		// Token: 0x020001DA RID: 474
		[CompilerGenerated]
		private sealed class <_WatchCall>d__334 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FE2 RID: 4066 RVA: 0x00064453 File Offset: 0x00062653
			[DebuggerHidden]
			public <_WatchCall>d__334(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FE3 RID: 4067 RVA: 0x00064462 File Offset: 0x00062662
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x00064464 File Offset: 0x00062664
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = timing.WaitForSecondsOnInstance(timeframe);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				timing.KillCoroutinesOnInstance(handle);
				if (onDone != null && (gObject == null || gObject != null))
				{
					onDone();
				}
				return false;
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000644E8 File Offset: 0x000626E8
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x000644F0 File Offset: 0x000626F0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x000644F7 File Offset: 0x000626F7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E2D RID: 3629
			private int <>1__state;

			// Token: 0x04000E2E RID: 3630
			private float <>2__current;

			// Token: 0x04000E2F RID: 3631
			public Timing <>4__this;

			// Token: 0x04000E30 RID: 3632
			public float timeframe;

			// Token: 0x04000E31 RID: 3633
			public CoroutineHandle handle;

			// Token: 0x04000E32 RID: 3634
			public Action onDone;

			// Token: 0x04000E33 RID: 3635
			public GameObject gObject;
		}

		// Token: 0x020001DB RID: 475
		[CompilerGenerated]
		private sealed class <_CallContinuously>d__335 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FE8 RID: 4072 RVA: 0x00064504 File Offset: 0x00062704
			[DebuggerHidden]
			public <_CallContinuously>d__335(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x00064513 File Offset: 0x00062713
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00064518 File Offset: 0x00062718
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (gObject == null || (gObject != null && gObject.activeInHierarchy))
					{
						action();
					}
				}
				else
				{
					this.<>1__state = -1;
				}
				if (gObject != null && !(gObject != null))
				{
					return false;
				}
				this.<>2__current = timing.WaitForSecondsOnInstance(period);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000FEB RID: 4075 RVA: 0x000645AC File Offset: 0x000627AC
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x000645B4 File Offset: 0x000627B4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000FED RID: 4077 RVA: 0x000645BB File Offset: 0x000627BB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E34 RID: 3636
			private int <>1__state;

			// Token: 0x04000E35 RID: 3637
			private float <>2__current;

			// Token: 0x04000E36 RID: 3638
			public Timing <>4__this;

			// Token: 0x04000E37 RID: 3639
			public float period;

			// Token: 0x04000E38 RID: 3640
			public GameObject gObject;

			// Token: 0x04000E39 RID: 3641
			public Action action;
		}

		// Token: 0x020001DC RID: 476
		[CompilerGenerated]
		private sealed class <_WatchCall>d__352<T> : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FEE RID: 4078 RVA: 0x000645C8 File Offset: 0x000627C8
			[DebuggerHidden]
			public <_WatchCall>d__352(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x000645D7 File Offset: 0x000627D7
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FF0 RID: 4080 RVA: 0x000645DC File Offset: 0x000627DC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = timing.WaitForSecondsOnInstance(timeframe);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				timing.KillCoroutinesOnInstance(handle);
				if (onDone != null && (gObject == null || gObject != null))
				{
					onDone(reference);
				}
				return false;
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00064666 File Offset: 0x00062866
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x0006466E File Offset: 0x0006286E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00064675 File Offset: 0x00062875
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E3A RID: 3642
			private int <>1__state;

			// Token: 0x04000E3B RID: 3643
			private float <>2__current;

			// Token: 0x04000E3C RID: 3644
			public Timing <>4__this;

			// Token: 0x04000E3D RID: 3645
			public float timeframe;

			// Token: 0x04000E3E RID: 3646
			public CoroutineHandle handle;

			// Token: 0x04000E3F RID: 3647
			public Action<T> onDone;

			// Token: 0x04000E40 RID: 3648
			public GameObject gObject;

			// Token: 0x04000E41 RID: 3649
			public T reference;
		}

		// Token: 0x020001DD RID: 477
		[CompilerGenerated]
		private sealed class <_CallContinuously>d__353<T> : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FF4 RID: 4084 RVA: 0x00064682 File Offset: 0x00062882
			[DebuggerHidden]
			public <_CallContinuously>d__353(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x00064691 File Offset: 0x00062891
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FF6 RID: 4086 RVA: 0x00064694 File Offset: 0x00062894
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Timing timing = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (gObject == null || (gObject != null && gObject.activeInHierarchy))
					{
						action(reference);
					}
				}
				else
				{
					this.<>1__state = -1;
				}
				if (gObject != null && !(gObject != null))
				{
					return false;
				}
				this.<>2__current = timing.WaitForSecondsOnInstance(period);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0006472E File Offset: 0x0006292E
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FF8 RID: 4088 RVA: 0x00064736 File Offset: 0x00062936
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0006473D File Offset: 0x0006293D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E42 RID: 3650
			private int <>1__state;

			// Token: 0x04000E43 RID: 3651
			private float <>2__current;

			// Token: 0x04000E44 RID: 3652
			public Timing <>4__this;

			// Token: 0x04000E45 RID: 3653
			public float period;

			// Token: 0x04000E46 RID: 3654
			public GameObject gObject;

			// Token: 0x04000E47 RID: 3655
			public Action<T> action;

			// Token: 0x04000E48 RID: 3656
			public T reference;
		}
	}
}
