using System;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000AA RID: 170
	public struct Controller
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00010C28 File Offset: 0x0000EE28
		internal Controller(InputHandle_t inputHandle_t)
		{
			this.Handle = inputHandle_t;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00010C32 File Offset: 0x0000EE32
		public ulong Id
		{
			get
			{
				return this.Handle.Value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00010C3F File Offset: 0x0000EE3F
		public InputType InputType
		{
			get
			{
				return SteamInput.Internal.GetInputTypeForHandle(this.Handle);
			}
		}

		// Token: 0x17000086 RID: 134
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00010C51 File Offset: 0x0000EE51
		public string ActionSet
		{
			set
			{
				SteamInput.Internal.ActivateActionSet(this.Handle, SteamInput.Internal.GetActionSetHandle(value));
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00010C6F File Offset: 0x0000EE6F
		public void DeactivateLayer(string layer)
		{
			SteamInput.Internal.DeactivateActionSetLayer(this.Handle, SteamInput.Internal.GetActionSetHandle(layer));
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00010C8D File Offset: 0x0000EE8D
		public void ActivateLayer(string layer)
		{
			SteamInput.Internal.ActivateActionSetLayer(this.Handle, SteamInput.Internal.GetActionSetHandle(layer));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00010CAB File Offset: 0x0000EEAB
		public void ClearLayers()
		{
			SteamInput.Internal.DeactivateAllActionSetLayers(this.Handle);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public DigitalState GetDigitalState(string actionName)
		{
			return SteamInput.Internal.GetDigitalActionData(this.Handle, SteamInput.GetDigitalActionHandle(actionName));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public AnalogState GetAnalogState(string actionName)
		{
			return SteamInput.Internal.GetAnalogActionData(this.Handle, SteamInput.GetAnalogActionHandle(actionName));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00010D10 File Offset: 0x0000EF10
		public override string ToString()
		{
			return string.Format("{0}.{1}", this.InputType, this.Handle.Value);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00010D37 File Offset: 0x0000EF37
		public static bool operator ==(Controller a, Controller b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00010D41 File Offset: 0x0000EF41
		public static bool operator !=(Controller a, Controller b)
		{
			return !(a == b);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00010D4D File Offset: 0x0000EF4D
		public override bool Equals(object p)
		{
			return this.Equals((Controller)p);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00010D5B File Offset: 0x0000EF5B
		public override int GetHashCode()
		{
			return this.Handle.GetHashCode();
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00010D6E File Offset: 0x0000EF6E
		public bool Equals(Controller p)
		{
			return p.Handle == this.Handle;
		}

		// Token: 0x04000743 RID: 1859
		internal InputHandle_t Handle;
	}
}
