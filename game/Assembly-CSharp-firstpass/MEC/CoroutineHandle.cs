using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MEC
{
	// Token: 0x020000A5 RID: 165
	public struct CoroutineHandle : IEquatable<CoroutineHandle>
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00034500 File Offset: 0x00032700
		public byte Key
		{
			get
			{
				return (byte)(this._id & 15);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0003450C File Offset: 0x0003270C
		public CoroutineHandle(byte ind)
		{
			if (ind > 15)
			{
				ind -= 15;
			}
			this._id = CoroutineHandle.NextIndex[(int)ind] + (int)ind;
			CoroutineHandle.NextIndex[(int)ind] += 16;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0003453A File Offset: 0x0003273A
		public CoroutineHandle(CoroutineHandle other)
		{
			this._id = other._id;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00034548 File Offset: 0x00032748
		public bool Equals(CoroutineHandle other)
		{
			return this._id == other._id;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00034558 File Offset: 0x00032758
		public override bool Equals(object other)
		{
			return other is CoroutineHandle && this.Equals((CoroutineHandle)other);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00034570 File Offset: 0x00032770
		public static bool operator ==(CoroutineHandle a, CoroutineHandle b)
		{
			return a._id == b._id;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00034580 File Offset: 0x00032780
		public static bool operator !=(CoroutineHandle a, CoroutineHandle b)
		{
			return a._id != b._id;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00034593 File Offset: 0x00032793
		public override int GetHashCode()
		{
			return this._id;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0003459C File Offset: 0x0003279C
		public override string ToString()
		{
			if (Timing.GetTag(this) == null)
			{
				if (Timing.GetLayer(this) == null)
				{
					return Timing.GetDebugName(this);
				}
				return Timing.GetDebugName(this) + " Layer: " + Timing.GetLayer(this).ToString();
			}
			else
			{
				if (Timing.GetLayer(this) == null)
				{
					return Timing.GetDebugName(this) + " Tag: " + Timing.GetTag(this);
				}
				return string.Concat(new string[]
				{
					Timing.GetDebugName(this),
					" Tag: ",
					Timing.GetTag(this),
					" Layer: ",
					Timing.GetLayer(this).ToString()
				});
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0003468F File Offset: 0x0003288F
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0003469C File Offset: 0x0003289C
		public string Tag
		{
			get
			{
				return Timing.GetTag(this);
			}
			set
			{
				Timing.SetTag(this, value, true);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000346AC File Offset: 0x000328AC
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x000346B9 File Offset: 0x000328B9
		public int? Layer
		{
			get
			{
				return Timing.GetLayer(this);
			}
			set
			{
				if (value == null)
				{
					Timing.RemoveLayer(this);
					return;
				}
				Timing.SetLayer(this, value.Value, true);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000346E5 File Offset: 0x000328E5
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x000346F2 File Offset: 0x000328F2
		public Segment Segment
		{
			get
			{
				return Timing.GetSegment(this);
			}
			set
			{
				Timing.SetSegment(this, value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00034701 File Offset: 0x00032901
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0003470E File Offset: 0x0003290E
		public bool IsRunning
		{
			get
			{
				return Timing.IsRunning(this);
			}
			set
			{
				if (!value)
				{
					Timing.KillCoroutines(this);
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0003471F File Offset: 0x0003291F
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0003472C File Offset: 0x0003292C
		public bool IsAliveAndPaused
		{
			get
			{
				return Timing.IsAliveAndPaused(this);
			}
			set
			{
				if (value)
				{
					Timing.PauseCoroutines(this);
					return;
				}
				Timing.ResumeCoroutines(this);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0003474A File Offset: 0x0003294A
		public bool IsValid
		{
			get
			{
				return this.Key > 0;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00034758 File Offset: 0x00032958
		public CoroutineHandle OnDestroy(Action action, Segment segment = Segment.Update)
		{
			Timing instance = Timing.GetInstance(this.Key);
			if (action == null || instance == null)
			{
				return default(CoroutineHandle);
			}
			return instance.RunCoroutineOnInstance(CoroutineHandle._OnDestroy(this, action), segment);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0003479C File Offset: 0x0003299C
		public CoroutineHandle OnDestroy(IEnumerator<float> action, Segment segment = Segment.Update)
		{
			Timing instance = Timing.GetInstance(this.Key);
			if (action == null || instance == null)
			{
				return default(CoroutineHandle);
			}
			return instance.RunCoroutineOnInstance(CoroutineHandle._OnDestroy(this, action), segment);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000347DE File Offset: 0x000329DE
		private static IEnumerator<float> _OnDestroy(CoroutineHandle watched, Action action)
		{
			while (watched.IsRunning)
			{
				yield return float.NegativeInfinity;
			}
			action();
			yield break;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000347F4 File Offset: 0x000329F4
		private static IEnumerator<float> _OnDestroy(CoroutineHandle watched, IEnumerator<float> action)
		{
			while (watched.IsRunning)
			{
				yield return float.NegativeInfinity;
			}
			while (action.MoveNext())
			{
				float num = action.Current;
				yield return num;
			}
			yield break;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0003480A File Offset: 0x00032A0A
		// Note: this type is marked as 'beforefieldinit'.
		static CoroutineHandle()
		{
			int[] array = new int[16];
			array[0] = 16;
			CoroutineHandle.NextIndex = array;
		}

		// Token: 0x04000602 RID: 1538
		private const byte ReservedSpace = 15;

		// Token: 0x04000603 RID: 1539
		private static readonly int[] NextIndex;

		// Token: 0x04000604 RID: 1540
		private readonly int _id;

		// Token: 0x020001DE RID: 478
		[CompilerGenerated]
		private sealed class <_OnDestroy>d__32 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06000FFA RID: 4090 RVA: 0x0006474A File Offset: 0x0006294A
			[DebuggerHidden]
			public <_OnDestroy>d__32(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FFB RID: 4091 RVA: 0x00064759 File Offset: 0x00062959
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x0006475C File Offset: 0x0006295C
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
				}
				else
				{
					this.<>1__state = -1;
				}
				if (!watched.IsRunning)
				{
					action();
					return false;
				}
				this.<>2__current = float.NegativeInfinity;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000647B6 File Offset: 0x000629B6
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x000647BE File Offset: 0x000629BE
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000FFF RID: 4095 RVA: 0x000647C5 File Offset: 0x000629C5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E49 RID: 3657
			private int <>1__state;

			// Token: 0x04000E4A RID: 3658
			private float <>2__current;

			// Token: 0x04000E4B RID: 3659
			public CoroutineHandle watched;

			// Token: 0x04000E4C RID: 3660
			public Action action;
		}

		// Token: 0x020001DF RID: 479
		[CompilerGenerated]
		private sealed class <_OnDestroy>d__33 : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x06001000 RID: 4096 RVA: 0x000647D2 File Offset: 0x000629D2
			[DebuggerHidden]
			public <_OnDestroy>d__33(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x000647E1 File Offset: 0x000629E1
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x000647E4 File Offset: 0x000629E4
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
					goto IL_6F;
				default:
					return false;
				}
				if (watched.IsRunning)
				{
					this.<>2__current = float.NegativeInfinity;
					this.<>1__state = 1;
					return true;
				}
				IL_6F:
				if (!action.MoveNext())
				{
					return false;
				}
				this.<>2__current = action.Current;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06001003 RID: 4099 RVA: 0x0006486E File Offset: 0x00062A6E
			float IEnumerator<float>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001004 RID: 4100 RVA: 0x00064876 File Offset: 0x00062A76
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06001005 RID: 4101 RVA: 0x0006487D File Offset: 0x00062A7D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E4D RID: 3661
			private int <>1__state;

			// Token: 0x04000E4E RID: 3662
			private float <>2__current;

			// Token: 0x04000E4F RID: 3663
			public CoroutineHandle watched;

			// Token: 0x04000E50 RID: 3664
			public IEnumerator<float> action;
		}
	}
}
