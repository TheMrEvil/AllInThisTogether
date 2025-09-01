using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000098 RID: 152
	internal class StylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x000139DF File Offset: 0x00011BDF
		public StylePropertyAnimationSystem()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013A14 File Offset: 0x00011C14
		private T GetOrCreate<T>(ref T values) where T : new()
		{
			T t = values;
			return (t != null) ? t : (values = Activator.CreateInstance<T>());
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013A48 File Offset: 0x00011C48
		private bool StartTransition<T>(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, StylePropertyAnimationSystem.Values<T> values)
		{
			this.m_PropertyToValues[prop] = values;
			bool result = values.StartTransition(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.CurrentTimeMs());
			this.UpdateTracking<T>(values);
			return result;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013A8C File Offset: 0x00011C8C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<float>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFloat>(ref this.m_Floats));
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00013ABC File Offset: 0x00011CBC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<int>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesInt>(ref this.m_Ints));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013AEC File Offset: 0x00011CEC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Length>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesLength>(ref this.m_Lengths));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013B1C File Offset: 0x00011D1C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Color>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesColor>(ref this.m_Colors));
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013B4C File Offset: 0x00011D4C
		public bool StartAnimationEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<int>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesEnum>(ref this.m_Enums));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013B7C File Offset: 0x00011D7C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Background>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackground>(ref this.m_Backgrounds));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013BAC File Offset: 0x00011DAC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<FontDefinition>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFontDefinition>(ref this.m_FontDefinitions));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013BDC File Offset: 0x00011DDC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Font>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFont>(ref this.m_Fonts));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013C0C File Offset: 0x00011E0C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TextShadow>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTextShadow>(ref this.m_TextShadows));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00013C3C File Offset: 0x00011E3C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Scale>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesScale>(ref this.m_Scale));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013C6C File Offset: 0x00011E6C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Rotate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesRotate>(ref this.m_Rotate));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013C9C File Offset: 0x00011E9C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Translate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTranslate>(ref this.m_Translate));
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013CCC File Offset: 0x00011ECC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TransformOrigin>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTransformOrigin>(ref this.m_TransformOrigin));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013CFC File Offset: 0x00011EFC
		public void CancelAllAnimations()
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations();
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013D54 File Offset: 0x00011F54
		public void CancelAllAnimations(VisualElement owner)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations(owner);
			}
			Assert.AreEqual(0, owner.styleAnimation.runningAnimationCount);
			Assert.AreEqual(0, owner.styleAnimation.completedAnimationCount);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.CancelAnimation(owner, id);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013E00 File Offset: 0x00012000
		public bool HasRunningAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			return this.m_PropertyToValues.TryGetValue(id, out values) && values.HasRunningAnimation(owner, id);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013E30 File Offset: 0x00012030
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.UpdateAnimation(owner, id);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013E5C File Offset: 0x0001205C
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.GetAllAnimations(owner, propertyIds);
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013EB4 File Offset: 0x000120B4
		private void UpdateTracking<T>(StylePropertyAnimationSystem.Values<T> values)
		{
			bool flag = !values.isEmpty && !this.m_AllValues.Contains(values);
			if (flag)
			{
				this.m_AllValues.Add(values);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013EF0 File Offset: 0x000120F0
		private long CurrentTimeMs()
		{
			return this.m_CurrentTimeMs;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013F08 File Offset: 0x00012108
		public void Update()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			int count = this.m_AllValues.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_AllValues[i].Update(this.m_CurrentTimeMs);
			}
		}

		// Token: 0x0400021E RID: 542
		private long m_CurrentTimeMs = 0L;

		// Token: 0x0400021F RID: 543
		private StylePropertyAnimationSystem.ValuesFloat m_Floats;

		// Token: 0x04000220 RID: 544
		private StylePropertyAnimationSystem.ValuesInt m_Ints;

		// Token: 0x04000221 RID: 545
		private StylePropertyAnimationSystem.ValuesLength m_Lengths;

		// Token: 0x04000222 RID: 546
		private StylePropertyAnimationSystem.ValuesColor m_Colors;

		// Token: 0x04000223 RID: 547
		private StylePropertyAnimationSystem.ValuesEnum m_Enums;

		// Token: 0x04000224 RID: 548
		private StylePropertyAnimationSystem.ValuesBackground m_Backgrounds;

		// Token: 0x04000225 RID: 549
		private StylePropertyAnimationSystem.ValuesFontDefinition m_FontDefinitions;

		// Token: 0x04000226 RID: 550
		private StylePropertyAnimationSystem.ValuesFont m_Fonts;

		// Token: 0x04000227 RID: 551
		private StylePropertyAnimationSystem.ValuesTextShadow m_TextShadows;

		// Token: 0x04000228 RID: 552
		private StylePropertyAnimationSystem.ValuesScale m_Scale;

		// Token: 0x04000229 RID: 553
		private StylePropertyAnimationSystem.ValuesRotate m_Rotate;

		// Token: 0x0400022A RID: 554
		private StylePropertyAnimationSystem.ValuesTranslate m_Translate;

		// Token: 0x0400022B RID: 555
		private StylePropertyAnimationSystem.ValuesTransformOrigin m_TransformOrigin;

		// Token: 0x0400022C RID: 556
		private readonly List<StylePropertyAnimationSystem.Values> m_AllValues = new List<StylePropertyAnimationSystem.Values>();

		// Token: 0x0400022D RID: 557
		private readonly Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values> m_PropertyToValues = new Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values>();

		// Token: 0x02000099 RID: 153
		[Flags]
		private enum TransitionState
		{
			// Token: 0x0400022F RID: 559
			None = 0,
			// Token: 0x04000230 RID: 560
			Running = 1,
			// Token: 0x04000231 RID: 561
			Started = 2,
			// Token: 0x04000232 RID: 562
			Ended = 4,
			// Token: 0x04000233 RID: 563
			Canceled = 8
		}

		// Token: 0x0200009A RID: 154
		private struct AnimationDataSet<TTimingData, TStyleData>
		{
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x00013F57 File Offset: 0x00012157
			// (set) Token: 0x0600054E RID: 1358 RVA: 0x00013F61 File Offset: 0x00012161
			private int capacity
			{
				get
				{
					return this.elements.Length;
				}
				set
				{
					Array.Resize<VisualElement>(ref this.elements, value);
					Array.Resize<StylePropertyId>(ref this.properties, value);
					Array.Resize<TTimingData>(ref this.timing, value);
					Array.Resize<TStyleData>(ref this.style, value);
				}
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x00013F98 File Offset: 0x00012198
			private void LocalInit()
			{
				this.elements = new VisualElement[2];
				this.properties = new StylePropertyId[2];
				this.timing = new TTimingData[2];
				this.style = new TStyleData[2];
				this.indices = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x00013FE8 File Offset: 0x000121E8
			public static StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> Create()
			{
				StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> result = default(StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData>);
				result.LocalInit();
				return result;
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x0001400C File Offset: 0x0001220C
			public bool IndexOf(VisualElement ve, StylePropertyId prop, out int index)
			{
				return this.indices.TryGetValue(new StylePropertyAnimationSystem.ElementPropertyPair(ve, prop), out index);
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x00014034 File Offset: 0x00012234
			public void Add(VisualElement owner, StylePropertyId prop, TTimingData timingData, TStyleData styleData)
			{
				bool flag = this.count >= this.capacity;
				if (flag)
				{
					this.capacity *= 2;
				}
				int num = this.count;
				this.count = num + 1;
				int num2 = num;
				this.elements[num2] = owner;
				this.properties[num2] = prop;
				this.timing[num2] = timingData;
				this.style[num2] = styleData;
				this.indices.Add(new StylePropertyAnimationSystem.ElementPropertyPair(owner, prop), num2);
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x000140BC File Offset: 0x000122BC
			public void Remove(int cancelledIndex)
			{
				int num = this.count - 1;
				this.count = num;
				int num2 = num;
				this.indices.Remove(new StylePropertyAnimationSystem.ElementPropertyPair(this.elements[cancelledIndex], this.properties[cancelledIndex]));
				bool flag = cancelledIndex != num2;
				if (flag)
				{
					VisualElement element = this.elements[cancelledIndex] = this.elements[num2];
					StylePropertyId property = this.properties[cancelledIndex] = this.properties[num2];
					this.timing[cancelledIndex] = this.timing[num2];
					this.style[cancelledIndex] = this.style[num2];
					this.indices[new StylePropertyAnimationSystem.ElementPropertyPair(element, property)] = cancelledIndex;
				}
				this.elements[num2] = null;
				this.properties[num2] = StylePropertyId.Unknown;
				this.timing[num2] = default(TTimingData);
				this.style[num2] = default(TStyleData);
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x000141BA File Offset: 0x000123BA
			public void Replace(int index, TTimingData timingData, TStyleData styleData)
			{
				this.timing[index] = timingData;
				this.style[index] = styleData;
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x000141D8 File Offset: 0x000123D8
			public void RemoveAll(VisualElement ve)
			{
				int num = this.count;
				for (int i = num - 1; i >= 0; i--)
				{
					bool flag = this.elements[i] == ve;
					if (flag)
					{
						this.Remove(i);
					}
				}
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x0001421C File Offset: 0x0001241C
			public void RemoveAll()
			{
				this.capacity = 2;
				int length = Mathf.Min(this.count, this.capacity);
				Array.Clear(this.elements, 0, length);
				Array.Clear(this.properties, 0, length);
				Array.Clear(this.timing, 0, length);
				Array.Clear(this.style, 0, length);
				this.count = 0;
				this.indices.Clear();
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x00014290 File Offset: 0x00012490
			public void GetActivePropertiesForElement(VisualElement ve, List<StylePropertyId> outProperties)
			{
				int num = this.count;
				for (int i = num - 1; i >= 0; i--)
				{
					bool flag = this.elements[i] == ve;
					if (flag)
					{
						outProperties.Add(this.properties[i]);
					}
				}
			}

			// Token: 0x04000234 RID: 564
			private const int InitialSize = 2;

			// Token: 0x04000235 RID: 565
			public VisualElement[] elements;

			// Token: 0x04000236 RID: 566
			public StylePropertyId[] properties;

			// Token: 0x04000237 RID: 567
			public TTimingData[] timing;

			// Token: 0x04000238 RID: 568
			public TStyleData[] style;

			// Token: 0x04000239 RID: 569
			public int count;

			// Token: 0x0400023A RID: 570
			private Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int> indices;
		}

		// Token: 0x0200009B RID: 155
		private struct ElementPropertyPair
		{
			// Token: 0x06000558 RID: 1368 RVA: 0x000142DA File Offset: 0x000124DA
			public ElementPropertyPair(VisualElement element, StylePropertyId property)
			{
				this.element = element;
				this.property = property;
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x000142EB File Offset: 0x000124EB
			// Note: this type is marked as 'beforefieldinit'.
			static ElementPropertyPair()
			{
			}

			// Token: 0x0400023B RID: 571
			public static readonly IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair> Comparer = new StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer();

			// Token: 0x0400023C RID: 572
			public readonly VisualElement element;

			// Token: 0x0400023D RID: 573
			public readonly StylePropertyId property;

			// Token: 0x0200009C RID: 156
			private class EqualityComparer : IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair>
			{
				// Token: 0x0600055A RID: 1370 RVA: 0x000142F8 File Offset: 0x000124F8
				public bool Equals(StylePropertyAnimationSystem.ElementPropertyPair x, StylePropertyAnimationSystem.ElementPropertyPair y)
				{
					return x.element == y.element && x.property == y.property;
				}

				// Token: 0x0600055B RID: 1371 RVA: 0x0001432C File Offset: 0x0001252C
				public int GetHashCode(StylePropertyAnimationSystem.ElementPropertyPair obj)
				{
					return obj.element.GetHashCode() * 397 ^ (int)obj.property;
				}

				// Token: 0x0600055C RID: 1372 RVA: 0x000020C2 File Offset: 0x000002C2
				public EqualityComparer()
				{
				}
			}
		}

		// Token: 0x0200009D RID: 157
		private abstract class Values
		{
			// Token: 0x0600055D RID: 1373
			public abstract void CancelAllAnimations();

			// Token: 0x0600055E RID: 1374
			public abstract void CancelAllAnimations(VisualElement ve);

			// Token: 0x0600055F RID: 1375
			public abstract void CancelAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x06000560 RID: 1376
			public abstract bool HasRunningAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x06000561 RID: 1377
			public abstract void UpdateAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x06000562 RID: 1378
			public abstract void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds);

			// Token: 0x06000563 RID: 1379
			public abstract void Update(long currentTimeMs);

			// Token: 0x06000564 RID: 1380
			protected abstract void UpdateValues();

			// Token: 0x06000565 RID: 1381
			protected abstract void UpdateComputedStyle();

			// Token: 0x06000566 RID: 1382
			protected abstract void UpdateComputedStyle(int i);

			// Token: 0x06000567 RID: 1383 RVA: 0x000020C2 File Offset: 0x000002C2
			protected Values()
			{
			}
		}

		// Token: 0x0200009E RID: 158
		private abstract class Values<T> : StylePropertyAnimationSystem.Values
		{
			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x00014357 File Offset: 0x00012557
			public bool isEmpty
			{
				get
				{
					return this.running.count + this.completed.count == 0;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000569 RID: 1385
			public abstract Func<T, T, bool> SameFunc { get; }

			// Token: 0x0600056A RID: 1386 RVA: 0x00014374 File Offset: 0x00012574
			protected virtual bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref T a, ref T b)
			{
				return true;
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00014388 File Offset: 0x00012588
			protected Values()
			{
				this.running = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData>.Create();
				this.completed = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T>.Create();
				this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x000143DC File Offset: 0x000125DC
			private void SwapFrameStates()
			{
				StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState currentFrameEventsState = this.m_CurrentFrameEventsState;
				this.m_CurrentFrameEventsState = this.m_NextFrameEventsState;
				this.m_NextFrameEventsState = currentFrameEventsState;
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00014404 File Offset: 0x00012604
			private void QueueEvent(EventBase evt, StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				evt.target = epp.element;
				Queue<EventBase> pooledQueue;
				bool flag = !this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out pooledQueue);
				if (flag)
				{
					pooledQueue = StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.GetPooledQueue();
					this.m_NextFrameEventsState.elementPropertyQueuedEvents.Add(epp, pooledQueue);
				}
				pooledQueue.Enqueue(evt);
				bool flag2 = this.m_NextFrameEventsState.panel == null;
				if (flag2)
				{
					this.m_NextFrameEventsState.panel = epp.element.panel;
				}
				this.m_NextFrameEventsState.RegisterChange();
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x00014490 File Offset: 0x00012690
			private void ClearEventQueue(StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				Queue<EventBase> queue;
				bool flag = this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out queue);
				if (flag)
				{
					while (queue.Count > 0)
					{
						queue.Dequeue().Dispose();
						this.m_NextFrameEventsState.UnregisterChange();
					}
				}
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x000144E0 File Offset: 0x000126E0
			private void QueueTransitionRunEvent(VisualElement ve, int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
				StylePropertyId stylePropertyId = this.running.properties[runningIndex];
				int num = (ptr.delayMs < 0) ? Mathf.Min(Mathf.Max(-ptr.delayMs, 0), ptr.durationMs) : 0;
				StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
				TransitionRunEvent pooled = TransitionEventBase<TransitionRunEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
				bool flag = this.m_NextFrameEventsState.elementPropertyStateDelta.ContainsKey(elementPropertyPair);
				if (flag)
				{
					Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = this.m_NextFrameEventsState.elementPropertyStateDelta;
					StylePropertyAnimationSystem.ElementPropertyPair key = elementPropertyPair;
					elementPropertyStateDelta[key] |= StylePropertyAnimationSystem.TransitionState.Running;
				}
				else
				{
					this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Running);
				}
				this.QueueEvent(pooled, elementPropertyPair);
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x000145B0 File Offset: 0x000127B0
			private void QueueTransitionStartEvent(VisualElement ve, int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
				StylePropertyId stylePropertyId = this.running.properties[runningIndex];
				int num = (ptr.delayMs < 0) ? Mathf.Min(Mathf.Max(-ptr.delayMs, 0), ptr.durationMs) : 0;
				StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
				TransitionStartEvent pooled = TransitionEventBase<TransitionStartEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
				bool flag = this.m_NextFrameEventsState.elementPropertyStateDelta.ContainsKey(elementPropertyPair);
				if (flag)
				{
					Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = this.m_NextFrameEventsState.elementPropertyStateDelta;
					StylePropertyAnimationSystem.ElementPropertyPair key = elementPropertyPair;
					elementPropertyStateDelta[key] |= StylePropertyAnimationSystem.TransitionState.Started;
				}
				else
				{
					this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Started);
				}
				this.QueueEvent(pooled, elementPropertyPair);
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x00014680 File Offset: 0x00012880
			private void QueueTransitionEndEvent(VisualElement ve, int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
				StylePropertyId stylePropertyId = this.running.properties[runningIndex];
				StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
				TransitionEndEvent pooled = TransitionEventBase<TransitionEndEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)ptr.durationMs / 1000f));
				bool flag = this.m_NextFrameEventsState.elementPropertyStateDelta.ContainsKey(elementPropertyPair);
				if (flag)
				{
					Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = this.m_NextFrameEventsState.elementPropertyStateDelta;
					StylePropertyAnimationSystem.ElementPropertyPair key = elementPropertyPair;
					elementPropertyStateDelta[key] |= StylePropertyAnimationSystem.TransitionState.Ended;
				}
				else
				{
					this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Ended);
				}
				this.QueueEvent(pooled, elementPropertyPair);
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x0001472C File Offset: 0x0001292C
			private void QueueTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
				StylePropertyId stylePropertyId = this.running.properties[runningIndex];
				long num = ptr.isStarted ? (panelElapsedMs - ptr.startTimeMs) : 0L;
				StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
				bool flag = ptr.delayMs < 0;
				if (flag)
				{
					num = (long)(-(long)ptr.delayMs) + num;
				}
				TransitionCancelEvent pooled = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
				bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.ContainsKey(elementPropertyPair);
				if (flag2)
				{
					bool flag3 = this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] == StylePropertyAnimationSystem.TransitionState.None || (this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] & StylePropertyAnimationSystem.TransitionState.Canceled) == StylePropertyAnimationSystem.TransitionState.Canceled;
					if (flag3)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = StylePropertyAnimationSystem.TransitionState.Canceled;
						this.ClearEventQueue(elementPropertyPair);
						this.QueueEvent(pooled, elementPropertyPair);
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = StylePropertyAnimationSystem.TransitionState.None;
						this.ClearEventQueue(elementPropertyPair);
					}
				}
				else
				{
					this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Canceled);
					this.QueueEvent(pooled, elementPropertyPair);
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x0001485C File Offset: 0x00012A5C
			private void SendTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
				StylePropertyId stylePropertyId = this.running.properties[runningIndex];
				long num = ptr.isStarted ? (panelElapsedMs - ptr.startTimeMs) : 0L;
				bool flag = ptr.delayMs < 0;
				if (flag)
				{
					num = (long)(-(long)ptr.delayMs) + num;
				}
				using (TransitionCancelEvent pooled = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f)))
				{
					pooled.target = ve;
					ve.SendEvent(pooled);
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00014904 File Offset: 0x00012B04
			public sealed override void CancelAllAnimations()
			{
				int count = this.running.count;
				bool flag = count > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < count; i++)
						{
							VisualElement visualElement = this.running.elements[i];
							this.SendTransitionCancelEvent(visualElement, i, this.m_CurrentTimeMs);
							this.ForceComputedStyleEndValue(i);
							IStylePropertyAnimations styleAnimation = visualElement.styleAnimation;
							int num = styleAnimation.runningAnimationCount;
							styleAnimation.runningAnimationCount = num - 1;
						}
					}
					this.running.RemoveAll();
				}
				int count2 = this.completed.count;
				for (int j = 0; j < count2; j++)
				{
					VisualElement visualElement2 = this.completed.elements[j];
					IStylePropertyAnimations styleAnimation2 = visualElement2.styleAnimation;
					int num = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num - 1;
				}
				this.completed.RemoveAll();
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00014A2C File Offset: 0x00012C2C
			public sealed override void CancelAllAnimations(VisualElement ve)
			{
				int count = this.running.count;
				bool flag = count > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < count; i++)
						{
							bool flag2 = this.running.elements[i] == ve;
							if (flag2)
							{
								this.SendTransitionCancelEvent(ve, i, this.m_CurrentTimeMs);
								this.ForceComputedStyleEndValue(i);
								IStylePropertyAnimations styleAnimation = this.running.elements[i].styleAnimation;
								int num = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num - 1;
							}
						}
					}
				}
				this.running.RemoveAll(ve);
				int count2 = this.completed.count;
				for (int j = 0; j < count2; j++)
				{
					bool flag3 = this.completed.elements[j] == ve;
					if (flag3)
					{
						IStylePropertyAnimations styleAnimation2 = this.completed.elements[j].styleAnimation;
						int num = styleAnimation2.completedAnimationCount;
						styleAnimation2.completedAnimationCount = num - 1;
					}
				}
				this.completed.RemoveAll(ve);
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x00014B80 File Offset: 0x00012D80
			public sealed override void CancelAnimation(VisualElement ve, StylePropertyId id)
			{
				int num;
				bool flag = this.running.IndexOf(ve, id, out num);
				if (flag)
				{
					this.QueueTransitionCancelEvent(ve, num, this.m_CurrentTimeMs);
					this.ForceComputedStyleEndValue(num);
					this.running.Remove(num);
					IStylePropertyAnimations styleAnimation = ve.styleAnimation;
					int num2 = styleAnimation.runningAnimationCount;
					styleAnimation.runningAnimationCount = num2 - 1;
				}
				int cancelledIndex;
				bool flag2 = this.completed.IndexOf(ve, id, out cancelledIndex);
				if (flag2)
				{
					this.completed.Remove(cancelledIndex);
					IStylePropertyAnimations styleAnimation2 = ve.styleAnimation;
					int num2 = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num2 - 1;
				}
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00014C18 File Offset: 0x00012E18
			public sealed override bool HasRunningAnimation(VisualElement ve, StylePropertyId id)
			{
				int num;
				return this.running.IndexOf(ve, id, out num);
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x00014C3C File Offset: 0x00012E3C
			public sealed override void UpdateAnimation(VisualElement ve, StylePropertyId id)
			{
				int i;
				bool flag = this.running.IndexOf(ve, id, out i);
				if (flag)
				{
					this.UpdateComputedStyle(i);
				}
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x00014C65 File Offset: 0x00012E65
			public sealed override void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds)
			{
				this.running.GetActivePropertiesForElement(ve, outPropertyIds);
				this.completed.GetActivePropertiesForElement(ve, outPropertyIds);
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x00014C84 File Offset: 0x00012E84
			private float ComputeReversingShorteningFactor(int oldIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[oldIndex];
				return Mathf.Clamp01(Mathf.Abs(1f - (1f - ptr.easedProgress) * ptr.reversingShorteningFactor));
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x00014CCC File Offset: 0x00012ECC
			private int ComputeReversingDuration(int newTransitionDurationMs, float newReversingShorteningFactor)
			{
				return Mathf.RoundToInt((float)newTransitionDurationMs * newReversingShorteningFactor);
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x00014CE8 File Offset: 0x00012EE8
			private int ComputeReversingDelay(int delayMs, float newReversingShorteningFactor)
			{
				return (delayMs < 0) ? Mathf.RoundToInt((float)delayMs * newReversingShorteningFactor) : delayMs;
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x00014D0C File Offset: 0x00012F0C
			public bool StartTransition(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, long currentTimeMs)
			{
				long startTimeMs = currentTimeMs + (long)delayMs;
				StylePropertyAnimationSystem.Values<T>.TimingData timingData = new StylePropertyAnimationSystem.Values<T>.TimingData
				{
					startTimeMs = startTimeMs,
					durationMs = durationMs,
					easingCurve = easingCurve,
					reversingShorteningFactor = 1f,
					delayMs = delayMs
				};
				StylePropertyAnimationSystem.Values<T>.StyleData styleData = new StylePropertyAnimationSystem.Values<T>.StyleData
				{
					startValue = startValue,
					endValue = endValue,
					currentValue = startValue,
					reversingAdjustedStartValue = startValue
				};
				int num = Mathf.Max(0, durationMs) + delayMs;
				bool flag = !this.ConvertUnits(owner, prop, ref styleData.startValue, ref styleData.endValue);
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					int num2;
					bool flag2 = this.completed.IndexOf(owner, prop, out num2);
					if (flag2)
					{
						bool flag3 = this.SameFunc(endValue, this.completed.style[num2]);
						if (flag3)
						{
							return false;
						}
						bool flag4 = num <= 0;
						if (flag4)
						{
							return false;
						}
						this.completed.Remove(num2);
						IStylePropertyAnimations styleAnimation = owner.styleAnimation;
						int num3 = styleAnimation.completedAnimationCount;
						styleAnimation.completedAnimationCount = num3 - 1;
					}
					int num4;
					bool flag5 = this.running.IndexOf(owner, prop, out num4);
					if (flag5)
					{
						bool flag6 = this.SameFunc(endValue, this.running.style[num4].endValue);
						if (flag6)
						{
							result = false;
						}
						else
						{
							bool flag7 = this.SameFunc(endValue, this.running.style[num4].currentValue);
							if (flag7)
							{
								this.QueueTransitionCancelEvent(owner, num4, currentTimeMs);
								this.running.Remove(num4);
								IStylePropertyAnimations styleAnimation2 = owner.styleAnimation;
								int num3 = styleAnimation2.runningAnimationCount;
								styleAnimation2.runningAnimationCount = num3 - 1;
								result = false;
							}
							else
							{
								bool flag8 = num <= 0;
								if (flag8)
								{
									this.QueueTransitionCancelEvent(owner, num4, currentTimeMs);
									this.running.Remove(num4);
									IStylePropertyAnimations styleAnimation3 = owner.styleAnimation;
									int num3 = styleAnimation3.runningAnimationCount;
									styleAnimation3.runningAnimationCount = num3 - 1;
									result = false;
								}
								else
								{
									styleData.startValue = this.running.style[num4].currentValue;
									bool flag9 = !this.ConvertUnits(owner, prop, ref styleData.startValue, ref styleData.endValue);
									if (flag9)
									{
										this.SendTransitionCancelEvent(owner, num4, currentTimeMs);
										this.running.Remove(num4);
										IStylePropertyAnimations styleAnimation4 = owner.styleAnimation;
										int num3 = styleAnimation4.runningAnimationCount;
										styleAnimation4.runningAnimationCount = num3 - 1;
										result = false;
									}
									else
									{
										styleData.currentValue = styleData.startValue;
										bool flag10 = this.SameFunc(endValue, this.running.style[num4].reversingAdjustedStartValue);
										if (flag10)
										{
											float newReversingShorteningFactor = timingData.reversingShorteningFactor = this.ComputeReversingShorteningFactor(num4);
											timingData.startTimeMs = currentTimeMs + (long)this.ComputeReversingDelay(delayMs, newReversingShorteningFactor);
											timingData.durationMs = this.ComputeReversingDuration(durationMs, newReversingShorteningFactor);
											styleData.reversingAdjustedStartValue = this.running.style[num4].endValue;
										}
										this.running.timing[num4].isStarted = false;
										this.QueueTransitionCancelEvent(owner, num4, currentTimeMs);
										this.QueueTransitionRunEvent(owner, num4);
										this.running.Replace(num4, timingData, styleData);
										result = true;
									}
								}
							}
						}
					}
					else
					{
						bool flag11 = num <= 0;
						if (flag11)
						{
							result = false;
						}
						else
						{
							bool flag12 = this.SameFunc(startValue, endValue);
							if (flag12)
							{
								result = false;
							}
							else
							{
								this.running.Add(owner, prop, timingData, styleData);
								IStylePropertyAnimations styleAnimation5 = owner.styleAnimation;
								int num3 = styleAnimation5.runningAnimationCount;
								styleAnimation5.runningAnimationCount = num3 + 1;
								this.QueueTransitionRunEvent(owner, this.running.count - 1);
								result = true;
							}
						}
					}
				}
				return result;
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x000150F4 File Offset: 0x000132F4
			private void ForceComputedStyleEndValue(int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.StyleData ptr = ref this.running.style[runningIndex];
				ptr.currentValue = ptr.endValue;
				this.UpdateComputedStyle(runningIndex);
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x00015128 File Offset: 0x00013328
			public sealed override void Update(long currentTimeMs)
			{
				this.m_CurrentTimeMs = currentTimeMs;
				this.UpdateProgress(currentTimeMs);
				this.UpdateValues();
				this.UpdateComputedStyle();
				bool flag = this.m_NextFrameEventsState.StateChanged();
				if (flag)
				{
					this.ProcessEventQueue();
				}
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x0001516C File Offset: 0x0001336C
			private void ProcessEventQueue()
			{
				this.SwapFrameStates();
				IPanel panel = this.m_CurrentFrameEventsState.panel;
				EventDispatcher d = (panel != null) ? panel.dispatcher : null;
				using (new EventDispatcherGate(d))
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> keyValuePair in this.m_CurrentFrameEventsState.elementPropertyQueuedEvents)
					{
						StylePropertyAnimationSystem.ElementPropertyPair key = keyValuePair.Key;
						Queue<EventBase> value = keyValuePair.Value;
						VisualElement element = keyValuePair.Key.element;
						while (value.Count > 0)
						{
							EventBase eventBase = value.Dequeue();
							element.SendEvent(eventBase);
							eventBase.Dispose();
						}
					}
					this.m_CurrentFrameEventsState.Clear();
				}
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00015264 File Offset: 0x00013464
			private void UpdateProgress(long currentTimeMs)
			{
				int num = this.running.count;
				bool flag = num > 0;
				if (flag)
				{
					for (int i = 0; i < num; i++)
					{
						ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[i];
						bool flag2 = currentTimeMs < ptr.startTimeMs;
						if (flag2)
						{
							ptr.easedProgress = 0f;
						}
						else
						{
							bool flag3 = currentTimeMs >= ptr.startTimeMs + (long)ptr.durationMs;
							if (flag3)
							{
								ref StylePropertyAnimationSystem.Values<T>.StyleData ptr2 = ref this.running.style[i];
								ref VisualElement ptr3 = ref this.running.elements[i];
								ptr2.currentValue = ptr2.endValue;
								this.UpdateComputedStyle(i);
								this.completed.Add(ptr3, this.running.properties[i], StylePropertyAnimationSystem.Values<T>.EmptyData.Default, ptr2.endValue);
								IStylePropertyAnimations styleAnimation = ptr3.styleAnimation;
								int num2 = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num2 - 1;
								IStylePropertyAnimations styleAnimation2 = ptr3.styleAnimation;
								num2 = styleAnimation2.completedAnimationCount;
								styleAnimation2.completedAnimationCount = num2 + 1;
								this.QueueTransitionEndEvent(ptr3, i);
								this.running.Remove(i);
								i--;
								num--;
							}
							else
							{
								bool flag4 = !ptr.isStarted;
								if (flag4)
								{
									ptr.isStarted = true;
									this.QueueTransitionStartEvent(this.running.elements[i], i);
								}
								float arg = (float)(currentTimeMs - ptr.startTimeMs) / (float)ptr.durationMs;
								ptr.easedProgress = ptr.easingCurve(arg);
							}
						}
					}
				}
			}

			// Token: 0x0400023E RID: 574
			private long m_CurrentTimeMs = 0L;

			// Token: 0x0400023F RID: 575
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_CurrentFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x04000240 RID: 576
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_NextFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x04000241 RID: 577
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData> running;

			// Token: 0x04000242 RID: 578
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T> completed;

			// Token: 0x0200009F RID: 159
			private class TransitionEventsFrameState
			{
				// Token: 0x06000582 RID: 1410 RVA: 0x00015408 File Offset: 0x00013608
				public static Queue<EventBase> GetPooledQueue()
				{
					return StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Get();
				}

				// Token: 0x06000583 RID: 1411 RVA: 0x00015424 File Offset: 0x00013624
				public void RegisterChange()
				{
					this.m_ChangesCount++;
				}

				// Token: 0x06000584 RID: 1412 RVA: 0x00015435 File Offset: 0x00013635
				public void UnregisterChange()
				{
					this.m_ChangesCount--;
				}

				// Token: 0x06000585 RID: 1413 RVA: 0x00015448 File Offset: 0x00013648
				public bool StateChanged()
				{
					return this.m_ChangesCount > 0;
				}

				// Token: 0x06000586 RID: 1414 RVA: 0x00015464 File Offset: 0x00013664
				public void Clear()
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> keyValuePair in this.elementPropertyQueuedEvents)
					{
						this.elementPropertyStateDelta[keyValuePair.Key] = StylePropertyAnimationSystem.TransitionState.None;
						keyValuePair.Value.Clear();
						StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Release(keyValuePair.Value);
					}
					this.elementPropertyQueuedEvents.Clear();
					this.panel = null;
					this.m_ChangesCount = 0;
				}

				// Token: 0x06000587 RID: 1415 RVA: 0x00015504 File Offset: 0x00013704
				public TransitionEventsFrameState()
				{
				}

				// Token: 0x06000588 RID: 1416 RVA: 0x0001552D File Offset: 0x0001372D
				// Note: this type is marked as 'beforefieldinit'.
				static TransitionEventsFrameState()
				{
				}

				// Token: 0x04000243 RID: 579
				private static readonly ObjectPool<Queue<EventBase>> k_EventQueuePool = new ObjectPool<Queue<EventBase>>(() => new Queue<EventBase>(4), null, null, null, true, 10, 10000);

				// Token: 0x04000244 RID: 580
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04000245 RID: 581
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> elementPropertyQueuedEvents = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04000246 RID: 582
				public IPanel panel;

				// Token: 0x04000247 RID: 583
				private int m_ChangesCount;

				// Token: 0x020000A0 RID: 160
				[CompilerGenerated]
				[Serializable]
				private sealed class <>c
				{
					// Token: 0x06000589 RID: 1417 RVA: 0x00015554 File Offset: 0x00013754
					// Note: this type is marked as 'beforefieldinit'.
					static <>c()
					{
					}

					// Token: 0x0600058A RID: 1418 RVA: 0x000020C2 File Offset: 0x000002C2
					public <>c()
					{
					}

					// Token: 0x0600058B RID: 1419 RVA: 0x00015560 File Offset: 0x00013760
					internal Queue<EventBase> <.cctor>b__11_0()
					{
						return new Queue<EventBase>(4);
					}

					// Token: 0x04000248 RID: 584
					public static readonly StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.<>c <>9 = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.<>c();
				}
			}

			// Token: 0x020000A1 RID: 161
			public struct TimingData
			{
				// Token: 0x04000249 RID: 585
				public long startTimeMs;

				// Token: 0x0400024A RID: 586
				public int durationMs;

				// Token: 0x0400024B RID: 587
				public Func<float, float> easingCurve;

				// Token: 0x0400024C RID: 588
				public float easedProgress;

				// Token: 0x0400024D RID: 589
				public float reversingShorteningFactor;

				// Token: 0x0400024E RID: 590
				public bool isStarted;

				// Token: 0x0400024F RID: 591
				public int delayMs;
			}

			// Token: 0x020000A2 RID: 162
			public struct StyleData
			{
				// Token: 0x04000250 RID: 592
				public T startValue;

				// Token: 0x04000251 RID: 593
				public T endValue;

				// Token: 0x04000252 RID: 594
				public T reversingAdjustedStartValue;

				// Token: 0x04000253 RID: 595
				public T currentValue;
			}

			// Token: 0x020000A3 RID: 163
			public struct EmptyData
			{
				// Token: 0x0600058C RID: 1420 RVA: 0x00015568 File Offset: 0x00013768
				// Note: this type is marked as 'beforefieldinit'.
				static EmptyData()
				{
				}

				// Token: 0x04000254 RID: 596
				public static StylePropertyAnimationSystem.Values<T>.EmptyData Default = default(StylePropertyAnimationSystem.Values<T>.EmptyData);
			}
		}

		// Token: 0x020000A4 RID: 164
		private class ValuesFloat : StylePropertyAnimationSystem.Values<float>
		{
			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00015575 File Offset: 0x00013775
			public override Func<float, float, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<float, float, bool>(StylePropertyAnimationSystem.ValuesFloat.IsSame);

			// Token: 0x0600058E RID: 1422 RVA: 0x0001557D File Offset: 0x0001377D
			private static bool IsSame(float a, float b)
			{
				return Mathf.Approximately(a, b);
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00015586 File Offset: 0x00013786
			private static float Lerp(float a, float b, float t)
			{
				return Mathf.LerpUnclamped(a, b, t);
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x00015590 File Offset: 0x00013790
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<float>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<float>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesFloat.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00015600 File Offset: 0x00013800
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x00015674 File Offset: 0x00013874
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x000156CA File Offset: 0x000138CA
			public ValuesFloat()
			{
			}

			// Token: 0x04000255 RID: 597
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<float, float, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000A5 RID: 165
		private class ValuesInt : StylePropertyAnimationSystem.Values<int>
		{
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000594 RID: 1428 RVA: 0x000156E5 File Offset: 0x000138E5
			public override Func<int, int, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<int, int, bool>(StylePropertyAnimationSystem.ValuesInt.IsSame);

			// Token: 0x06000595 RID: 1429 RVA: 0x000156ED File Offset: 0x000138ED
			private static bool IsSame(int a, int b)
			{
				return a == b;
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x000156F3 File Offset: 0x000138F3
			private static int Lerp(int a, int b, float t)
			{
				return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, t));
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00015704 File Offset: 0x00013904
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<int>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<int>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesInt.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x00015774 File Offset: 0x00013974
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x000157E8 File Offset: 0x000139E8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0001583E File Offset: 0x00013A3E
			public ValuesInt()
			{
			}

			// Token: 0x04000256 RID: 598
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<int, int, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000A6 RID: 166
		private class ValuesLength : StylePropertyAnimationSystem.Values<Length>
		{
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x00015859 File Offset: 0x00013A59
			public override Func<Length, Length, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<Length, Length, bool>(StylePropertyAnimationSystem.ValuesLength.IsSame);

			// Token: 0x0600059C RID: 1436 RVA: 0x00015861 File Offset: 0x00013A61
			private static bool IsSame(Length a, Length b)
			{
				return a.unit == b.unit && Mathf.Approximately(a.value, b.value);
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001588C File Offset: 0x00013A8C
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Length a, ref Length b)
			{
				return owner.TryConvertLengthUnits(prop, ref a, ref b, 0);
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x000158A9 File Offset: 0x00013AA9
			internal static Length Lerp(Length a, Length b, float t)
			{
				return new Length(Mathf.LerpUnclamped(a.value, b.value, t), b.unit);
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x000158CC File Offset: 0x00013ACC
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Length>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Length>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesLength.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x0001593C File Offset: 0x00013B3C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x000159B0 File Offset: 0x00013BB0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00015A06 File Offset: 0x00013C06
			public ValuesLength()
			{
			}

			// Token: 0x04000257 RID: 599
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<Length, Length, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000A7 RID: 167
		private class ValuesColor : StylePropertyAnimationSystem.Values<Color>
		{
			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00015A21 File Offset: 0x00013C21
			public override Func<Color, Color, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<Color, Color, bool>(StylePropertyAnimationSystem.ValuesColor.IsSame);

			// Token: 0x060005A4 RID: 1444 RVA: 0x00015A2C File Offset: 0x00013C2C
			private static bool IsSame(Color c, Color d)
			{
				return Mathf.Approximately(c.r, d.r) && Mathf.Approximately(c.g, d.g) && Mathf.Approximately(c.b, d.b) && Mathf.Approximately(c.a, d.a);
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00015A86 File Offset: 0x00013C86
			private static Color Lerp(Color a, Color b, float t)
			{
				return Color.LerpUnclamped(a, b, t);
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00015A90 File Offset: 0x00013C90
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Color>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Color>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesColor.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00015B00 File Offset: 0x00013D00
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00015B74 File Offset: 0x00013D74
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00015BCA File Offset: 0x00013DCA
			public ValuesColor()
			{
			}

			// Token: 0x04000258 RID: 600
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<Color, Color, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000A8 RID: 168
		private abstract class ValuesDiscrete<T> : StylePropertyAnimationSystem.Values<T>
		{
			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x00015BE5 File Offset: 0x00013DE5
			public override Func<T, T, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<T, T, bool>(StylePropertyAnimationSystem.ValuesDiscrete<T>.IsSame);

			// Token: 0x060005AB RID: 1451 RVA: 0x00015BED File Offset: 0x00013DED
			private static bool IsSame(T a, T b)
			{
				return EqualityComparer<T>.Default.Equals(a, b);
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00015BFB File Offset: 0x00013DFB
			private static T Lerp(T a, T b, float t)
			{
				return (t < 0.5f) ? a : b;
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00015C0C File Offset: 0x00013E0C
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<T>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesDiscrete<T>.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00015C7B File Offset: 0x00013E7B
			protected ValuesDiscrete()
			{
			}

			// Token: 0x04000259 RID: 601
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly Func<T, T, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000A9 RID: 169
		private class ValuesEnum : StylePropertyAnimationSystem.ValuesDiscrete<int>
		{
			// Token: 0x060005AF RID: 1455 RVA: 0x00015C98 File Offset: 0x00013E98
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x00015D0C File Offset: 0x00013F0C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x00015D62 File Offset: 0x00013F62
			public ValuesEnum()
			{
			}
		}

		// Token: 0x020000AA RID: 170
		private class ValuesBackground : StylePropertyAnimationSystem.ValuesDiscrete<Background>
		{
			// Token: 0x060005B2 RID: 1458 RVA: 0x00015D6C File Offset: 0x00013F6C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x00015DE0 File Offset: 0x00013FE0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x00015E36 File Offset: 0x00014036
			public ValuesBackground()
			{
			}
		}

		// Token: 0x020000AB RID: 171
		private class ValuesFontDefinition : StylePropertyAnimationSystem.ValuesDiscrete<FontDefinition>
		{
			// Token: 0x060005B5 RID: 1461 RVA: 0x00015E40 File Offset: 0x00014040
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x00015EB4 File Offset: 0x000140B4
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x00015F0A File Offset: 0x0001410A
			public ValuesFontDefinition()
			{
			}
		}

		// Token: 0x020000AC RID: 172
		private class ValuesFont : StylePropertyAnimationSystem.ValuesDiscrete<Font>
		{
			// Token: 0x060005B8 RID: 1464 RVA: 0x00015F14 File Offset: 0x00014114
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x00015F88 File Offset: 0x00014188
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x00015FDE File Offset: 0x000141DE
			public ValuesFont()
			{
			}
		}

		// Token: 0x020000AD RID: 173
		private class ValuesTextShadow : StylePropertyAnimationSystem.Values<TextShadow>
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x00015FE7 File Offset: 0x000141E7
			public override Func<TextShadow, TextShadow, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<TextShadow, TextShadow, bool>(StylePropertyAnimationSystem.ValuesTextShadow.IsSame);

			// Token: 0x060005BC RID: 1468 RVA: 0x00015FEF File Offset: 0x000141EF
			private static bool IsSame(TextShadow a, TextShadow b)
			{
				return a == b;
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x00015FF8 File Offset: 0x000141F8
			private static TextShadow Lerp(TextShadow a, TextShadow b, float t)
			{
				return TextShadow.LerpUnclamped(a, b, t);
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x00016004 File Offset: 0x00014204
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<TextShadow>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<TextShadow>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTextShadow.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x00016074 File Offset: 0x00014274
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x000160E8 File Offset: 0x000142E8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001613E File Offset: 0x0001433E
			public ValuesTextShadow()
			{
			}

			// Token: 0x0400025A RID: 602
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly Func<TextShadow, TextShadow, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000AE RID: 174
		private class ValuesScale : StylePropertyAnimationSystem.Values<Scale>
		{
			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00016159 File Offset: 0x00014359
			public override Func<Scale, Scale, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<Scale, Scale, bool>(StylePropertyAnimationSystem.ValuesScale.IsSame);

			// Token: 0x060005C3 RID: 1475 RVA: 0x00016161 File Offset: 0x00014361
			private static bool IsSame(Scale a, Scale b)
			{
				return a == b;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001616C File Offset: 0x0001436C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x000161E0 File Offset: 0x000143E0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x00016236 File Offset: 0x00014436
			private static Scale Lerp(Scale a, Scale b, float t)
			{
				return new Scale(Vector3.LerpUnclamped(a.value, b.value, t));
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x00016254 File Offset: 0x00014454
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Scale>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Scale>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesScale.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x000162C3 File Offset: 0x000144C3
			public ValuesScale()
			{
			}

			// Token: 0x0400025B RID: 603
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private readonly Func<Scale, Scale, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000AF RID: 175
		private class ValuesRotate : StylePropertyAnimationSystem.Values<Rotate>
		{
			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000162DE File Offset: 0x000144DE
			public override Func<Rotate, Rotate, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<Rotate, Rotate, bool>(StylePropertyAnimationSystem.ValuesRotate.IsSame);

			// Token: 0x060005CA RID: 1482 RVA: 0x000162E6 File Offset: 0x000144E6
			private static bool IsSame(Rotate a, Rotate b)
			{
				return a == b;
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x000162F0 File Offset: 0x000144F0
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x00016364 File Offset: 0x00014564
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x000163BC File Offset: 0x000145BC
			private static Rotate Lerp(Rotate a, Rotate b, float t)
			{
				return new Rotate(Mathf.LerpUnclamped(a.angle.ToDegrees(), b.angle.ToDegrees(), t));
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x000163F8 File Offset: 0x000145F8
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Rotate>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Rotate>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesRotate.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x00016467 File Offset: 0x00014667
			public ValuesRotate()
			{
			}

			// Token: 0x0400025C RID: 604
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<Rotate, Rotate, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000B0 RID: 176
		private class ValuesTranslate : StylePropertyAnimationSystem.Values<Translate>
		{
			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00016482 File Offset: 0x00014682
			public override Func<Translate, Translate, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<Translate, Translate, bool>(StylePropertyAnimationSystem.ValuesTranslate.IsSame);

			// Token: 0x060005D1 RID: 1489 RVA: 0x0001648A File Offset: 0x0001468A
			private static bool IsSame(Translate a, Translate b)
			{
				return a == b;
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x00016494 File Offset: 0x00014694
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Translate a, ref Translate b)
			{
				return owner.TryConvertTranslateUnits(ref a, ref b);
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x000164B0 File Offset: 0x000146B0
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x00016524 File Offset: 0x00014724
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0001657C File Offset: 0x0001477C
			private static Translate Lerp(Translate a, Translate b, float t)
			{
				return new Translate(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x000165CC File Offset: 0x000147CC
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Translate>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Translate>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTranslate.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x0001663B File Offset: 0x0001483B
			public ValuesTranslate()
			{
			}

			// Token: 0x0400025D RID: 605
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<Translate, Translate, bool> <SameFunc>k__BackingField;
		}

		// Token: 0x020000B1 RID: 177
		private class ValuesTransformOrigin : StylePropertyAnimationSystem.Values<TransformOrigin>
		{
			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00016656 File Offset: 0x00014856
			public override Func<TransformOrigin, TransformOrigin, bool> SameFunc
			{
				[CompilerGenerated]
				get
				{
					return this.<SameFunc>k__BackingField;
				}
			} = new Func<TransformOrigin, TransformOrigin, bool>(StylePropertyAnimationSystem.ValuesTransformOrigin.IsSame);

			// Token: 0x060005D9 RID: 1497 RVA: 0x0001665E File Offset: 0x0001485E
			private static bool IsSame(TransformOrigin a, TransformOrigin b)
			{
				return a == b;
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00016668 File Offset: 0x00014868
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref TransformOrigin a, ref TransformOrigin b)
			{
				return owner.TryConvertTransformOriginUnits(ref a, ref b);
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x00016684 File Offset: 0x00014884
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x000166F8 File Offset: 0x000148F8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x00016750 File Offset: 0x00014950
			private static TransformOrigin Lerp(TransformOrigin a, TransformOrigin b, float t)
			{
				return new TransformOrigin(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x000167A0 File Offset: 0x000149A0
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTransformOrigin.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0001680F File Offset: 0x00014A0F
			public ValuesTransformOrigin()
			{
			}

			// Token: 0x0400025E RID: 606
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly Func<TransformOrigin, TransformOrigin, bool> <SameFunc>k__BackingField;
		}
	}
}
