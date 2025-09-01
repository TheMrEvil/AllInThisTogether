using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000095 RID: 149
	public class SteamInput : SteamClientClass<SteamInput>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000BE6D File Offset: 0x0000A06D
		internal static ISteamInput Internal
		{
			get
			{
				return SteamClientClass<SteamInput>.Interface as ISteamInput;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000BE79 File Offset: 0x0000A079
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamInput(server));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000BE8A File Offset: 0x0000A08A
		public static void RunFrame()
		{
			SteamInput.Internal.RunFrame();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0000BE98 File Offset: 0x0000A098
		public static IEnumerable<Controller> Controllers
		{
			get
			{
				int num = SteamInput.Internal.GetConnectedControllers(SteamInput.queryArray);
				int num2;
				for (int i = 0; i < num; i = num2 + 1)
				{
					yield return new Controller(SteamInput.queryArray[i]);
					num2 = i;
				}
				yield break;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public static string GetDigitalActionGlyph(Controller controller, string action)
		{
			InputActionOrigin eOrigin = InputActionOrigin.None;
			SteamInput.Internal.GetDigitalActionOrigins(controller.Handle, SteamInput.Internal.GetCurrentActionSet(controller.Handle), SteamInput.GetDigitalActionHandle(action), ref eOrigin);
			return SteamInput.Internal.GetGlyphForActionOrigin(eOrigin);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		internal static InputDigitalActionHandle_t GetDigitalActionHandle(string name)
		{
			InputDigitalActionHandle_t digitalActionHandle;
			bool flag = SteamInput.DigitalHandles.TryGetValue(name, out digitalActionHandle);
			InputDigitalActionHandle_t result;
			if (flag)
			{
				result = digitalActionHandle;
			}
			else
			{
				digitalActionHandle = SteamInput.Internal.GetDigitalActionHandle(name);
				SteamInput.DigitalHandles.Add(name, digitalActionHandle);
				result = digitalActionHandle;
			}
			return result;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000BF3C File Offset: 0x0000A13C
		internal static InputAnalogActionHandle_t GetAnalogActionHandle(string name)
		{
			InputAnalogActionHandle_t analogActionHandle;
			bool flag = SteamInput.AnalogHandles.TryGetValue(name, out analogActionHandle);
			InputAnalogActionHandle_t result;
			if (flag)
			{
				result = analogActionHandle;
			}
			else
			{
				analogActionHandle = SteamInput.Internal.GetAnalogActionHandle(name);
				SteamInput.AnalogHandles.Add(name, analogActionHandle);
				result = analogActionHandle;
			}
			return result;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000BF80 File Offset: 0x0000A180
		internal static InputActionSetHandle_t GetActionSetHandle(string name)
		{
			InputActionSetHandle_t actionSetHandle;
			bool flag = SteamInput.ActionSets.TryGetValue(name, out actionSetHandle);
			InputActionSetHandle_t result;
			if (flag)
			{
				result = actionSetHandle;
			}
			else
			{
				actionSetHandle = SteamInput.Internal.GetActionSetHandle(name);
				SteamInput.ActionSets.Add(name, actionSetHandle);
				result = actionSetHandle;
			}
			return result;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000BFC1 File Offset: 0x0000A1C1
		public SteamInput()
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000BFCA File Offset: 0x0000A1CA
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInput()
		{
		}

		// Token: 0x040006EB RID: 1771
		internal const int STEAM_CONTROLLER_MAX_COUNT = 16;

		// Token: 0x040006EC RID: 1772
		private static readonly InputHandle_t[] queryArray = new InputHandle_t[16];

		// Token: 0x040006ED RID: 1773
		internal static Dictionary<string, InputDigitalActionHandle_t> DigitalHandles = new Dictionary<string, InputDigitalActionHandle_t>();

		// Token: 0x040006EE RID: 1774
		internal static Dictionary<string, InputAnalogActionHandle_t> AnalogHandles = new Dictionary<string, InputAnalogActionHandle_t>();

		// Token: 0x040006EF RID: 1775
		internal static Dictionary<string, InputActionSetHandle_t> ActionSets = new Dictionary<string, InputActionSetHandle_t>();

		// Token: 0x02000222 RID: 546
		[CompilerGenerated]
		private sealed class <get_Controllers>d__7 : IEnumerable<Controller>, IEnumerable, IEnumerator<Controller>, IDisposable, IEnumerator
		{
			// Token: 0x060010D2 RID: 4306 RVA: 0x0001C81E File Offset: 0x0001AA1E
			[DebuggerHidden]
			public <get_Controllers>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060010D3 RID: 4307 RVA: 0x0001C839 File Offset: 0x0001AA39
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060010D4 RID: 4308 RVA: 0x0001C83C File Offset: 0x0001AA3C
			bool IEnumerator.MoveNext()
			{
				int num2 = this.<>1__state;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num3 = i;
					i = num3 + 1;
				}
				else
				{
					this.<>1__state = -1;
					num = SteamInput.Internal.GetConnectedControllers(SteamInput.queryArray);
					i = 0;
				}
				if (i >= num)
				{
					return false;
				}
				this.<>2__current = new Controller(SteamInput.queryArray[i]);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0001C8D7 File Offset: 0x0001AAD7
			Controller IEnumerator<Controller>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010D6 RID: 4310 RVA: 0x0001C8DF File Offset: 0x0001AADF
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0001C8E6 File Offset: 0x0001AAE6
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010D8 RID: 4312 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
			[DebuggerHidden]
			IEnumerator<Controller> IEnumerable<Controller>.GetEnumerator()
			{
				SteamInput.<get_Controllers>d__7 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamInput.<get_Controllers>d__7(0);
				}
				return result;
			}

			// Token: 0x060010D9 RID: 4313 RVA: 0x0001C92B File Offset: 0x0001AB2B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Controller>.GetEnumerator();
			}

			// Token: 0x04000CA9 RID: 3241
			private int <>1__state;

			// Token: 0x04000CAA RID: 3242
			private Controller <>2__current;

			// Token: 0x04000CAB RID: 3243
			private int <>l__initialThreadId;

			// Token: 0x04000CAC RID: 3244
			private int <num>5__1;

			// Token: 0x04000CAD RID: 3245
			private int <i>5__2;
		}
	}
}
