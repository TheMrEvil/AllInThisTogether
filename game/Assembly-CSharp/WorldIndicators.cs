using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class WorldIndicators : MonoBehaviour
{
	// Token: 0x060012DE RID: 4830 RVA: 0x0007440F File Offset: 0x0007260F
	private void Awake()
	{
		WorldIndicators.instance = this;
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x00074418 File Offset: 0x00072618
	private void LateUpdate()
	{
		WorldIndicators.inUse = WorldIndicators.inUse.Where(delegate(KeyValuePair<Indicatable, UIIndicator> kv)
		{
			KeyValuePair<Indicatable, UIIndicator> keyValuePair = kv;
			return keyValuePair.Key != null;
		}).ToDictionary((KeyValuePair<Indicatable, UIIndicator> kv) => kv.Key, (KeyValuePair<Indicatable, UIIndicator> kv) => kv.Value);
		foreach (UIIndicator uiindicator in this.indicators)
		{
			uiindicator.UpdateOpacity();
			uiindicator.UpdatePosition(WorldIndicators.NextIndex());
		}
		foreach (PlayerIndicator playerIndicator in this.playerIndicators)
		{
			playerIndicator.UpdateOpacity();
			playerIndicator.UpdatePosition(WorldIndicators.NextIndex());
		}
		foreach (PingIndicator pingIndicator in this.pingIndicators)
		{
			pingIndicator.UpdateOpacity();
			pingIndicator.UpdatePosition(WorldIndicators.NextIndex());
		}
		WorldIndicators.OffsetIndex = 0;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x00074580 File Offset: 0x00072780
	public static int NextIndex()
	{
		WorldIndicators.OffsetIndex++;
		return WorldIndicators.OffsetIndex;
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x00074593 File Offset: 0x00072793
	public static bool IsIndicating(Indicatable o)
	{
		return WorldIndicators.inUse.ContainsKey(o);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000745A0 File Offset: 0x000727A0
	public static void Indicate(Indicatable o)
	{
		if (WorldIndicators.inUse.ContainsKey(o))
		{
			return;
		}
		UIIndicator indicator = WorldIndicators.instance.GetIndicator();
		if (indicator == null)
		{
			Debug.LogError("No free indicator available");
			return;
		}
		indicator.Setup(o);
		WorldIndicators.inUse.Add(o, indicator);
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000745F0 File Offset: 0x000727F0
	public static void Indicate(PlayerWorldUI player)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(WorldIndicators.instance.PlayerRef, WorldIndicators.instance.PlayerRef.transform.parent);
		gameObject.SetActive(true);
		PlayerIndicator component = gameObject.GetComponent<PlayerIndicator>();
		component.Setup(player);
		WorldIndicators.instance.playerIndicators.Add(component);
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x00074644 File Offset: 0x00072844
	public static void Indicate(PlayerPing ping)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(WorldIndicators.instance.PingRef, WorldIndicators.instance.PingRef.transform.parent);
		gameObject.SetActive(true);
		PingIndicator component = gameObject.GetComponent<PingIndicator>();
		component.Setup(ping);
		WorldIndicators.instance.pingIndicators.Add(component);
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x00074698 File Offset: 0x00072898
	public static void ReleaseIndicator(Indicatable o)
	{
		if (WorldIndicators.inUse.ContainsKey(o))
		{
			WorldIndicators.inUse.Remove(o);
		}
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000746B4 File Offset: 0x000728B4
	public static void ReleaseIndicator(PlayerWorldUI indicator)
	{
		for (int i = WorldIndicators.instance.playerIndicators.Count - 1; i >= 0; i--)
		{
			if (!(WorldIndicators.instance.playerIndicators[i].target != indicator))
			{
				if (WorldIndicators.instance.playerIndicators[i] != null)
				{
					UnityEngine.Object.Destroy(WorldIndicators.instance.playerIndicators[i].gameObject);
				}
				WorldIndicators.instance.playerIndicators.RemoveAt(i);
			}
		}
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x0007473C File Offset: 0x0007293C
	public static void ReleaseIndicator(PlayerPing o)
	{
		for (int i = WorldIndicators.instance.pingIndicators.Count - 1; i >= 0; i--)
		{
			if (!(WorldIndicators.instance.pingIndicators[i] != o))
			{
				UnityEngine.Object.Destroy(WorldIndicators.instance.pingIndicators[i].gameObject);
				WorldIndicators.instance.pingIndicators.RemoveAt(i);
			}
		}
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000747A8 File Offset: 0x000729A8
	private UIIndicator GetIndicator()
	{
		if (this.indicators.Count == WorldIndicators.inUse.Count)
		{
			return this.AddIndicator();
		}
		foreach (UIIndicator uiindicator in this.indicators)
		{
			if (!this.UsingIndicator(uiindicator))
			{
				return uiindicator;
			}
		}
		return null;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00074824 File Offset: 0x00072A24
	private UIIndicator AddIndicator()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.IndicatorRef, this.IndicatorRef.transform.parent);
		gameObject.SetActive(true);
		UIIndicator component = gameObject.GetComponent<UIIndicator>();
		this.indicators.Add(component);
		return component;
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00074868 File Offset: 0x00072A68
	private bool UsingIndicator(UIIndicator indicator)
	{
		foreach (KeyValuePair<Indicatable, UIIndicator> keyValuePair in WorldIndicators.inUse)
		{
			if (keyValuePair.Value == indicator)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000748CC File Offset: 0x00072ACC
	public WorldIndicators()
	{
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x000748F5 File Offset: 0x00072AF5
	// Note: this type is marked as 'beforefieldinit'.
	static WorldIndicators()
	{
	}

	// Token: 0x04001200 RID: 4608
	public GameObject IndicatorRef;

	// Token: 0x04001201 RID: 4609
	public GameObject PlayerRef;

	// Token: 0x04001202 RID: 4610
	public GameObject PingRef;

	// Token: 0x04001203 RID: 4611
	private List<UIIndicator> indicators = new List<UIIndicator>();

	// Token: 0x04001204 RID: 4612
	private static Dictionary<Indicatable, UIIndicator> inUse = new Dictionary<Indicatable, UIIndicator>();

	// Token: 0x04001205 RID: 4613
	private static WorldIndicators instance;

	// Token: 0x04001206 RID: 4614
	private List<PlayerIndicator> playerIndicators = new List<PlayerIndicator>();

	// Token: 0x04001207 RID: 4615
	private List<PingIndicator> pingIndicators = new List<PingIndicator>();

	// Token: 0x04001208 RID: 4616
	internal static int OffsetIndex;

	// Token: 0x04001209 RID: 4617
	public int DictCount;

	// Token: 0x0200058C RID: 1420
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002558 RID: 9560 RVA: 0x000D1309 File Offset: 0x000CF509
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000D1315 File Offset: 0x000CF515
		public <>c()
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000D1320 File Offset: 0x000CF520
		internal bool <LateUpdate>b__11_0(KeyValuePair<Indicatable, UIIndicator> kv)
		{
			KeyValuePair<Indicatable, UIIndicator> keyValuePair = kv;
			return keyValuePair.Key != null;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000D133C File Offset: 0x000CF53C
		internal Indicatable <LateUpdate>b__11_1(KeyValuePair<Indicatable, UIIndicator> kv)
		{
			return kv.Key;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000D1345 File Offset: 0x000CF545
		internal UIIndicator <LateUpdate>b__11_2(KeyValuePair<Indicatable, UIIndicator> kv)
		{
			return kv.Value;
		}

		// Token: 0x0400279F RID: 10143
		public static readonly WorldIndicators.<>c <>9 = new WorldIndicators.<>c();

		// Token: 0x040027A0 RID: 10144
		public static Func<KeyValuePair<Indicatable, UIIndicator>, bool> <>9__11_0;

		// Token: 0x040027A1 RID: 10145
		public static Func<KeyValuePair<Indicatable, UIIndicator>, Indicatable> <>9__11_1;

		// Token: 0x040027A2 RID: 10146
		public static Func<KeyValuePair<Indicatable, UIIndicator>, UIIndicator> <>9__11_2;
	}
}
