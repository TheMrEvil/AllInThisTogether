using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021A RID: 538
	public interface IPointerEvent
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001063 RID: 4195
		int pointerId { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001064 RID: 4196
		string pointerType { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001065 RID: 4197
		bool isPrimary { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001066 RID: 4198
		int button { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001067 RID: 4199
		int pressedButtons { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001068 RID: 4200
		Vector3 position { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001069 RID: 4201
		Vector3 localPosition { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600106A RID: 4202
		Vector3 deltaPosition { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600106B RID: 4203
		float deltaTime { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600106C RID: 4204
		int clickCount { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600106D RID: 4205
		float pressure { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600106E RID: 4206
		float tangentialPressure { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600106F RID: 4207
		float altitudeAngle { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001070 RID: 4208
		float azimuthAngle { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001071 RID: 4209
		float twist { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001072 RID: 4210
		Vector2 radius { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001073 RID: 4211
		Vector2 radiusVariance { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001074 RID: 4212
		EventModifiers modifiers { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001075 RID: 4213
		bool shiftKey { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001076 RID: 4214
		bool ctrlKey { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001077 RID: 4215
		bool commandKey { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001078 RID: 4216
		bool altKey { get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001079 RID: 4217
		bool actionKey { get; }
	}
}
