using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x0200000C RID: 12
	public class KeyBinderModule : MonoBehaviour
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002E91 File Offset: 0x00001091
		private void BlockInput()
		{
			this._blocked = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E9A File Offset: 0x0000109A
		private void UnblockInput()
		{
			this._blocked = false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002EA4 File Offset: 0x000010A4
		private void BindToConsoleInstance()
		{
			if (!this._consoleInstance)
			{
				this._consoleInstance = UnityEngine.Object.FindObjectOfType<QuantumConsole>();
			}
			if (this._consoleInstance)
			{
				this._consoleInstance.OnActivate += this.BlockInput;
				this._consoleInstance.OnDeactivate += this.UnblockInput;
				this._blocked = this._consoleInstance.IsActive;
				return;
			}
			this.UnblockInput();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F1C File Offset: 0x0000111C
		private void Awake()
		{
			this.BindToConsoleInstance();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F24 File Offset: 0x00001124
		private void Update()
		{
			if (!this._blocked)
			{
				foreach (KeyBinderModule.Binding binding in this._bindings)
				{
					if (InputHelper.GetKeyDown(binding.Key))
					{
						try
						{
							QuantumConsoleProcessor.InvokeCommand(binding.Command);
						}
						catch (Exception exception)
						{
							UnityEngine.Debug.LogException(exception);
						}
					}
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002FA8 File Offset: 0x000011A8
		private void AddBinding(KeyCode key, string command)
		{
			this._bindings.Add(new KeyBinderModule.Binding(key, command));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002FBC File Offset: 0x000011BC
		private void RemoveBindings(KeyCode key)
		{
			this._bindings.RemoveAll((KeyBinderModule.Binding x) => x.Key == key);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002FEE File Offset: 0x000011EE
		private void RemoveAllBindings()
		{
			this._bindings.Clear();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FFB File Offset: 0x000011FB
		private IEnumerable<object> DisplayAllBindings()
		{
			foreach (KeyBinderModule.Binding binding in from x in this._bindings
			orderby x.Key
			select x)
			{
				yield return new KeyValuePair<KeyCode, string>(binding.Key, binding.Command);
			}
			IEnumerator<KeyBinderModule.Binding> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000300B File Offset: 0x0000120B
		public KeyBinderModule()
		{
		}

		// Token: 0x0400000E RID: 14
		private readonly List<KeyBinderModule.Binding> _bindings = new List<KeyBinderModule.Binding>();

		// Token: 0x0400000F RID: 15
		private QuantumConsole _consoleInstance;

		// Token: 0x04000010 RID: 16
		private bool _blocked;

		// Token: 0x0200001A RID: 26
		private readonly struct Binding
		{
			// Token: 0x0600006F RID: 111 RVA: 0x00003FC6 File Offset: 0x000021C6
			public Binding(KeyCode key, string command)
			{
				this.Key = key;
				this.Command = command;
			}

			// Token: 0x04000031 RID: 49
			public readonly KeyCode Key;

			// Token: 0x04000032 RID: 50
			public readonly string Command;
		}

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06000070 RID: 112 RVA: 0x00003FD6 File Offset: 0x000021D6
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00003FDE File Offset: 0x000021DE
			internal bool <RemoveBindings>b__0(KeyBinderModule.Binding x)
			{
				return x.Key == this.key;
			}

			// Token: 0x04000033 RID: 51
			public KeyCode key;
		}

		// Token: 0x0200001C RID: 28
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00003FEE File Offset: 0x000021EE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00003FFA File Offset: 0x000021FA
			public <>c()
			{
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00004002 File Offset: 0x00002202
			internal KeyCode <DisplayAllBindings>b__12_0(KeyBinderModule.Binding x)
			{
				return x.Key;
			}

			// Token: 0x04000034 RID: 52
			public static readonly KeyBinderModule.<>c <>9 = new KeyBinderModule.<>c();

			// Token: 0x04000035 RID: 53
			public static Func<KeyBinderModule.Binding, KeyCode> <>9__12_0;
		}

		// Token: 0x0200001D RID: 29
		[CompilerGenerated]
		private sealed class <DisplayAllBindings>d__12 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000075 RID: 117 RVA: 0x0000400A File Offset: 0x0000220A
			[DebuggerHidden]
			public <DisplayAllBindings>d__12(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00004024 File Offset: 0x00002224
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

			// Token: 0x06000077 RID: 119 RVA: 0x0000405C File Offset: 0x0000225C
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					KeyBinderModule keyBinderModule = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = keyBinderModule._bindings.OrderBy(new Func<KeyBinderModule.Binding, KeyCode>(KeyBinderModule.<>c.<>9.<DisplayAllBindings>b__12_0)).GetEnumerator();
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
						KeyBinderModule.Binding binding = enumerator.Current;
						this.<>2__current = new KeyValuePair<KeyCode, string>(binding.Key, binding.Command);
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

			// Token: 0x06000078 RID: 120 RVA: 0x00004140 File Offset: 0x00002340
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000079 RID: 121 RVA: 0x0000415C File Offset: 0x0000235C
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00004164 File Offset: 0x00002364
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600007B RID: 123 RVA: 0x0000416B File Offset: 0x0000236B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00004174 File Offset: 0x00002374
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				KeyBinderModule.<DisplayAllBindings>d__12 <DisplayAllBindings>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<DisplayAllBindings>d__ = this;
				}
				else
				{
					<DisplayAllBindings>d__ = new KeyBinderModule.<DisplayAllBindings>d__12(0);
					<DisplayAllBindings>d__.<>4__this = this;
				}
				return <DisplayAllBindings>d__;
			}

			// Token: 0x0600007D RID: 125 RVA: 0x000041B7 File Offset: 0x000023B7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x04000036 RID: 54
			private int <>1__state;

			// Token: 0x04000037 RID: 55
			private object <>2__current;

			// Token: 0x04000038 RID: 56
			private int <>l__initialThreadId;

			// Token: 0x04000039 RID: 57
			public KeyBinderModule <>4__this;

			// Token: 0x0400003A RID: 58
			private IEnumerator<KeyBinderModule.Binding> <>7__wrap1;
		}
	}
}
