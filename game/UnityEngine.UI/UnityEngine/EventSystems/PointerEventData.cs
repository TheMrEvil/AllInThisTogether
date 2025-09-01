using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004F RID: 79
	public class PointerEventData : BaseEventData
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00017DBE File Offset: 0x00015FBE
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00017DC6 File Offset: 0x00015FC6
		public GameObject pointerEnter
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerEnter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerEnter>k__BackingField = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00017DCF File Offset: 0x00015FCF
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00017DD7 File Offset: 0x00015FD7
		public GameObject lastPress
		{
			[CompilerGenerated]
			get
			{
				return this.<lastPress>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<lastPress>k__BackingField = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00017DE0 File Offset: 0x00015FE0
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00017DE8 File Offset: 0x00015FE8
		public GameObject rawPointerPress
		{
			[CompilerGenerated]
			get
			{
				return this.<rawPointerPress>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rawPointerPress>k__BackingField = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00017DF1 File Offset: 0x00015FF1
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00017DF9 File Offset: 0x00015FF9
		public GameObject pointerDrag
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerDrag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerDrag>k__BackingField = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00017E02 File Offset: 0x00016002
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00017E0A File Offset: 0x0001600A
		public GameObject pointerClick
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerClick>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerClick>k__BackingField = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00017E13 File Offset: 0x00016013
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00017E1B File Offset: 0x0001601B
		public RaycastResult pointerCurrentRaycast
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerCurrentRaycast>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerCurrentRaycast>k__BackingField = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00017E24 File Offset: 0x00016024
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00017E2C File Offset: 0x0001602C
		public RaycastResult pointerPressRaycast
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerPressRaycast>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerPressRaycast>k__BackingField = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00017E35 File Offset: 0x00016035
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00017E3D File Offset: 0x0001603D
		public bool eligibleForClick
		{
			[CompilerGenerated]
			get
			{
				return this.<eligibleForClick>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<eligibleForClick>k__BackingField = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00017E46 File Offset: 0x00016046
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00017E4E File Offset: 0x0001604E
		public int pointerId
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pointerId>k__BackingField = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00017E57 File Offset: 0x00016057
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00017E5F File Offset: 0x0001605F
		public Vector2 position
		{
			[CompilerGenerated]
			get
			{
				return this.<position>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<position>k__BackingField = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00017E68 File Offset: 0x00016068
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00017E70 File Offset: 0x00016070
		public Vector2 delta
		{
			[CompilerGenerated]
			get
			{
				return this.<delta>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<delta>k__BackingField = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00017E79 File Offset: 0x00016079
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00017E81 File Offset: 0x00016081
		public Vector2 pressPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<pressPosition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pressPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00017E8A File Offset: 0x0001608A
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00017E92 File Offset: 0x00016092
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<worldPosition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<worldPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00017E9B File Offset: 0x0001609B
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00017EA3 File Offset: 0x000160A3
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal
		{
			[CompilerGenerated]
			get
			{
				return this.<worldNormal>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<worldNormal>k__BackingField = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00017EAC File Offset: 0x000160AC
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00017EB4 File Offset: 0x000160B4
		public float clickTime
		{
			[CompilerGenerated]
			get
			{
				return this.<clickTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<clickTime>k__BackingField = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00017EBD File Offset: 0x000160BD
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00017EC5 File Offset: 0x000160C5
		public int clickCount
		{
			[CompilerGenerated]
			get
			{
				return this.<clickCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<clickCount>k__BackingField = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00017ECE File Offset: 0x000160CE
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00017ED6 File Offset: 0x000160D6
		public Vector2 scrollDelta
		{
			[CompilerGenerated]
			get
			{
				return this.<scrollDelta>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<scrollDelta>k__BackingField = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00017EDF File Offset: 0x000160DF
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00017EE7 File Offset: 0x000160E7
		public bool useDragThreshold
		{
			[CompilerGenerated]
			get
			{
				return this.<useDragThreshold>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<useDragThreshold>k__BackingField = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00017EF0 File Offset: 0x000160F0
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00017EF8 File Offset: 0x000160F8
		public bool dragging
		{
			[CompilerGenerated]
			get
			{
				return this.<dragging>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dragging>k__BackingField = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00017F01 File Offset: 0x00016101
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00017F09 File Offset: 0x00016109
		public PointerEventData.InputButton button
		{
			[CompilerGenerated]
			get
			{
				return this.<button>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<button>k__BackingField = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00017F12 File Offset: 0x00016112
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00017F1A File Offset: 0x0001611A
		public float pressure
		{
			[CompilerGenerated]
			get
			{
				return this.<pressure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pressure>k__BackingField = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00017F23 File Offset: 0x00016123
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00017F2B File Offset: 0x0001612B
		public float tangentialPressure
		{
			[CompilerGenerated]
			get
			{
				return this.<tangentialPressure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<tangentialPressure>k__BackingField = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00017F34 File Offset: 0x00016134
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00017F3C File Offset: 0x0001613C
		public float altitudeAngle
		{
			[CompilerGenerated]
			get
			{
				return this.<altitudeAngle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<altitudeAngle>k__BackingField = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00017F45 File Offset: 0x00016145
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00017F4D File Offset: 0x0001614D
		public float azimuthAngle
		{
			[CompilerGenerated]
			get
			{
				return this.<azimuthAngle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<azimuthAngle>k__BackingField = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00017F56 File Offset: 0x00016156
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00017F5E File Offset: 0x0001615E
		public float twist
		{
			[CompilerGenerated]
			get
			{
				return this.<twist>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<twist>k__BackingField = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00017F67 File Offset: 0x00016167
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00017F6F File Offset: 0x0001616F
		public Vector2 radius
		{
			[CompilerGenerated]
			get
			{
				return this.<radius>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<radius>k__BackingField = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00017F78 File Offset: 0x00016178
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00017F80 File Offset: 0x00016180
		public Vector2 radiusVariance
		{
			[CompilerGenerated]
			get
			{
				return this.<radiusVariance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<radiusVariance>k__BackingField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00017F89 File Offset: 0x00016189
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00017F91 File Offset: 0x00016191
		public bool fullyExited
		{
			[CompilerGenerated]
			get
			{
				return this.<fullyExited>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<fullyExited>k__BackingField = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00017F9A File Offset: 0x0001619A
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00017FA2 File Offset: 0x000161A2
		public bool reentered
		{
			[CompilerGenerated]
			get
			{
				return this.<reentered>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reentered>k__BackingField = value;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017FAC File Offset: 0x000161AC
		public PointerEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001807C File Offset: 0x0001627C
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000180A0 File Offset: 0x000162A0
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000180C2 File Offset: 0x000162C2
		public Camera enterEventCamera
		{
			get
			{
				if (!(this.pointerCurrentRaycast.module == null))
				{
					return this.pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000180E9 File Offset: 0x000162E9
		public Camera pressEventCamera
		{
			get
			{
				if (!(this.pointerPressRaycast.module == null))
				{
					return this.pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018110 File Offset: 0x00016310
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00018118 File Offset: 0x00016318
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001813C File Offset: 0x0001633C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position.ToString());
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta.ToString());
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick.ToString());
			string str = "<b>pointerEnter</b>: ";
			GameObject pointerEnter = this.pointerEnter;
			stringBuilder.AppendLine(str + ((pointerEnter != null) ? pointerEnter.ToString() : null));
			string str2 = "<b>pointerPress</b>: ";
			GameObject pointerPress = this.pointerPress;
			stringBuilder.AppendLine(str2 + ((pointerPress != null) ? pointerPress.ToString() : null));
			string str3 = "<b>lastPointerPress</b>: ";
			GameObject lastPress = this.lastPress;
			stringBuilder.AppendLine(str3 + ((lastPress != null) ? lastPress.ToString() : null));
			string str4 = "<b>pointerDrag</b>: ";
			GameObject pointerDrag = this.pointerDrag;
			stringBuilder.AppendLine(str4 + ((pointerDrag != null) ? pointerDrag.ToString() : null));
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold.ToString());
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			stringBuilder.AppendLine("<b>pressure</b>: " + this.pressure.ToString());
			stringBuilder.AppendLine("<b>tangentialPressure</b>: " + this.tangentialPressure.ToString());
			stringBuilder.AppendLine("<b>altitudeAngle</b>: " + this.altitudeAngle.ToString());
			stringBuilder.AppendLine("<b>azimuthAngle</b>: " + this.azimuthAngle.ToString());
			stringBuilder.AppendLine("<b>twist</b>: " + this.twist.ToString());
			stringBuilder.AppendLine("<b>radius</b>: " + this.radius.ToString());
			stringBuilder.AppendLine("<b>radiusVariance</b>: " + this.radiusVariance.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040001AF RID: 431
		[CompilerGenerated]
		private GameObject <pointerEnter>k__BackingField;

		// Token: 0x040001B0 RID: 432
		private GameObject m_PointerPress;

		// Token: 0x040001B1 RID: 433
		[CompilerGenerated]
		private GameObject <lastPress>k__BackingField;

		// Token: 0x040001B2 RID: 434
		[CompilerGenerated]
		private GameObject <rawPointerPress>k__BackingField;

		// Token: 0x040001B3 RID: 435
		[CompilerGenerated]
		private GameObject <pointerDrag>k__BackingField;

		// Token: 0x040001B4 RID: 436
		[CompilerGenerated]
		private GameObject <pointerClick>k__BackingField;

		// Token: 0x040001B5 RID: 437
		[CompilerGenerated]
		private RaycastResult <pointerCurrentRaycast>k__BackingField;

		// Token: 0x040001B6 RID: 438
		[CompilerGenerated]
		private RaycastResult <pointerPressRaycast>k__BackingField;

		// Token: 0x040001B7 RID: 439
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x040001B8 RID: 440
		[CompilerGenerated]
		private bool <eligibleForClick>k__BackingField;

		// Token: 0x040001B9 RID: 441
		[CompilerGenerated]
		private int <pointerId>k__BackingField;

		// Token: 0x040001BA RID: 442
		[CompilerGenerated]
		private Vector2 <position>k__BackingField;

		// Token: 0x040001BB RID: 443
		[CompilerGenerated]
		private Vector2 <delta>k__BackingField;

		// Token: 0x040001BC RID: 444
		[CompilerGenerated]
		private Vector2 <pressPosition>k__BackingField;

		// Token: 0x040001BD RID: 445
		[CompilerGenerated]
		private Vector3 <worldPosition>k__BackingField;

		// Token: 0x040001BE RID: 446
		[CompilerGenerated]
		private Vector3 <worldNormal>k__BackingField;

		// Token: 0x040001BF RID: 447
		[CompilerGenerated]
		private float <clickTime>k__BackingField;

		// Token: 0x040001C0 RID: 448
		[CompilerGenerated]
		private int <clickCount>k__BackingField;

		// Token: 0x040001C1 RID: 449
		[CompilerGenerated]
		private Vector2 <scrollDelta>k__BackingField;

		// Token: 0x040001C2 RID: 450
		[CompilerGenerated]
		private bool <useDragThreshold>k__BackingField;

		// Token: 0x040001C3 RID: 451
		[CompilerGenerated]
		private bool <dragging>k__BackingField;

		// Token: 0x040001C4 RID: 452
		[CompilerGenerated]
		private PointerEventData.InputButton <button>k__BackingField;

		// Token: 0x040001C5 RID: 453
		[CompilerGenerated]
		private float <pressure>k__BackingField;

		// Token: 0x040001C6 RID: 454
		[CompilerGenerated]
		private float <tangentialPressure>k__BackingField;

		// Token: 0x040001C7 RID: 455
		[CompilerGenerated]
		private float <altitudeAngle>k__BackingField;

		// Token: 0x040001C8 RID: 456
		[CompilerGenerated]
		private float <azimuthAngle>k__BackingField;

		// Token: 0x040001C9 RID: 457
		[CompilerGenerated]
		private float <twist>k__BackingField;

		// Token: 0x040001CA RID: 458
		[CompilerGenerated]
		private Vector2 <radius>k__BackingField;

		// Token: 0x040001CB RID: 459
		[CompilerGenerated]
		private Vector2 <radiusVariance>k__BackingField;

		// Token: 0x040001CC RID: 460
		[CompilerGenerated]
		private bool <fullyExited>k__BackingField;

		// Token: 0x040001CD RID: 461
		[CompilerGenerated]
		private bool <reentered>k__BackingField;

		// Token: 0x020000BF RID: 191
		public enum InputButton
		{
			// Token: 0x04000339 RID: 825
			Left,
			// Token: 0x0400033A RID: 826
			Right,
			// Token: 0x0400033B RID: 827
			Middle
		}

		// Token: 0x020000C0 RID: 192
		public enum FramePressState
		{
			// Token: 0x0400033D RID: 829
			Pressed,
			// Token: 0x0400033E RID: 830
			Released,
			// Token: 0x0400033F RID: 831
			PressedAndReleased,
			// Token: 0x04000340 RID: 832
			NotChanged
		}
	}
}
