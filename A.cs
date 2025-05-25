using System;
using Assets.src.e;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class GameCanvas : IActionListener
{
	// Token: 0x06000430 RID: 1072 RVA: 0x0004D550 File Offset: 0x0004B750
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		int num2 = num;
		if (num2 != -1)
		{
			if (num2 != 2)
			{
				Main.main.doClearRMS();
				Rms.saveRMSInt("languageVersion", 2);
			}
		}
		else
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		bool flag = GameCanvas.clearOldData != 1;
		if (flag)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0004D61C File Offset: 0x0004B81C
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0004D634 File Offset: 0x0004B834
	public void initGame()
	{
		try
		{
			MotherCanvas.instance.setChildCanvas(this);
			GameCanvas.w = MotherCanvas.instance.getWidthz();
			GameCanvas.h = MotherCanvas.instance.getHeightz();
			GameCanvas.hw = GameCanvas.w / 2;
			GameCanvas.hh = GameCanvas.h / 2;
			GameCanvas.isTouch = true;
			bool flag = GameCanvas.w >= 240;
			if (flag)
			{
				GameCanvas.isTouchControl = true;
			}
			bool flag2 = GameCanvas.w < 320;
			if (flag2)
			{
				GameCanvas.isTouchControlSmallScreen = true;
			}
			bool flag3 = GameCanvas.w >= 320;
			if (flag3)
			{
				GameCanvas.isTouchControlLargeScreen = true;
			}
			GameCanvas.msgdlg = new MsgDlg();
			bool flag4 = GameCanvas.h <= 160;
			if (flag4)
			{
				Paint.hTab = 15;
				mScreen.cmdH = 17;
			}
			GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
			GameCanvas.instance = this;
			mFont.init();
			mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
			this.initPaint();
			this.loadDust();
			this.loadWaterSplash();
			GameCanvas.panel = new Panel();
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
			int num = Rms.loadRMSInt("clienttype");
			bool flag5 = num != -1;
			if (flag5)
			{
				bool flag6 = num > 7;
				if (flag6)
				{
					Rms.saveRMSInt("clienttype", mSystem.clientType);
				}
				else
				{
					mSystem.clientType = num;
				}
			}
			bool flag7 = mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty);
			if (flag7)
			{
				GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
			}
			GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
			GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
			GameCanvas.debugUpdate = new MyVector();
			GameCanvas.debugPaint = new MyVector();
			GameCanvas.debugSession = new MyVector();
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i.ToString() + ".png");
			}
			GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
			GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
			GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
			GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
			Panel.graphics = Rms.loadRMSInt("lowGraphic");
			GameCanvas.lowGraphic = (Rms.loadRMSInt("lowGraphic") == 1);
			GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
			global::Char.isPaintAura = (Rms.loadRMSInt("isPaintAura") == 1);
			global::Char.isPaintAura2 = (Rms.loadRMSInt("isPaintAura2") == 1);
			Res.init();
			SmallImage.loadBigImage();
			Panel.WIDTH_PANEL = 176;
			bool flag8 = Panel.WIDTH_PANEL > GameCanvas.w;
			if (flag8)
			{
				Panel.WIDTH_PANEL = GameCanvas.w;
			}
			InfoMe.gI().loadCharId();
			Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
			Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
			Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
			Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
			Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
			Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
			GameCanvas.serverScreen = new ServerListScreen();
			GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
			for (int j = 0; j < 7; j++)
			{
				GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j.ToString() + ".png");
				GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j.ToString() + ".png");
			}
			ServerListScreen.createDeleteRMS();
			GameCanvas.serverScr = new ServerScr();
			GameCanvas.loginScr = new LoginScr();
			GameCanvas._SelectCharScr = new SelectCharScr();
		}
		catch (Exception)
		{
			Debug.LogError("----------------->>>>>>>>>>errr");
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x0004DA70 File Offset: 0x0004BC70
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00004C12 File Offset: 0x00002E12
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00004C1F File Offset: 0x00002E1F
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0004DA88 File Offset: 0x0004BC88
	public void update()
	{
		bool flag = GameCanvas.currentScreen == GameCanvas._SelectCharScr;
		if (flag)
		{
			bool flag2 = GameCanvas.gameTick % 2 == 0 && SmallImage.vt_images_watingDowload.size() > 0;
			if (flag2)
			{
				Small small = (Small)SmallImage.vt_images_watingDowload.elementAt(0);
				Service.gI().requestIcon(small.id);
				SmallImage.vt_images_watingDowload.removeElementAt(0);
			}
		}
		else
		{
			bool flag3 = GameCanvas.isRequestMapID == 2 && GameCanvas.waitingTimeChangeMap < mSystem.currentTimeMillis() && GameCanvas.gameTick % 2 == 0 && GameCanvas.currentScreen != null;
			if (flag3)
			{
				bool flag4 = GameCanvas.currentScreen == GameScr.gI();
				if (flag4)
				{
					bool isLoadingMap = global::Char.isLoadingMap;
					if (isLoadingMap)
					{
						global::Char.isLoadingMap = false;
					}
					bool waitToLogin = ServerListScreen.waitToLogin;
					if (waitToLogin)
					{
						ServerListScreen.waitToLogin = false;
					}
				}
				bool flag5 = SmallImage.vt_images_watingDowload.size() > 0;
				if (flag5)
				{
					Small small2 = (Small)SmallImage.vt_images_watingDowload.elementAt(0);
					Service.gI().requestIcon(small2.id);
					SmallImage.vt_images_watingDowload.removeElementAt(0);
				}
				bool flag6 = Effect.dowloadEff.size() <= 0;
				if (flag6)
				{
				}
			}
		}
		bool flag7 = mSystem.currentTimeMillis() > this.timefps;
		if (flag7)
		{
			this.timefps += 1000L;
			GameCanvas.max = GameCanvas.fps;
			GameCanvas.fps = 0;
		}
		GameCanvas.fps++;
		bool flag8 = GameCanvas.messageServer.size() > 0 && GameCanvas.thongBaoTest == null;
		if (flag8)
		{
			GameCanvas.startserverThongBao((string)GameCanvas.messageServer.elementAt(0));
			GameCanvas.messageServer.removeElementAt(0);
		}
		bool flag9 = GameCanvas.gameTick % 5 == 0;
		if (flag9)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			bool visible = global::TouchScreenKeyboard.visible;
			if (visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				bool flag10 = GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5);
				if (flag10)
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num = mSystem.currentTimeMillis();
			bool flag11 = num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1;
			if (flag11)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			bool flag12 = num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2;
			if (flag12)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			bool flag13 = GameCanvas.taskTick > 0;
			if (flag13)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			bool flag14 = GameCanvas.gameTick > 10000;
			if (flag14)
			{
				bool flag15 = mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr;
				if (flag15)
				{
					GameMidlet.instance.exit();
				}
				GameCanvas.gameTick = 0;
			}
			bool flag16 = GameCanvas.currentScreen != null;
			if (flag16)
			{
				bool flag17 = ChatPopup.serverChatPopUp != null;
				if (flag17)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else
				{
					bool flag18 = ChatPopup.currChatPopup != null;
					if (flag18)
					{
						ChatPopup.currChatPopup.update();
						ChatPopup.currChatPopup.updateKey();
					}
					else
					{
						bool flag19 = GameCanvas.currentDialog != null;
						if (flag19)
						{
							GameCanvas.debug("B", 0);
							GameCanvas.currentDialog.update();
						}
						else
						{
							bool showMenu = GameCanvas.menu.showMenu;
							if (showMenu)
							{
								GameCanvas.debug("C", 0);
								GameCanvas.menu.updateMenu();
								GameCanvas.debug("D", 0);
								GameCanvas.menu.updateMenuKey();
							}
							else
							{
								bool isShow = GameCanvas.panel.isShow;
								if (isShow)
								{
									GameCanvas.panel.update();
									bool flag20 = GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H);
									if (flag20)
									{
										GameCanvas.isFocusPanel2 = false;
									}
									bool flag21 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
									if (flag21)
									{
										GameCanvas.panel2.update();
										bool flag22 = GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
										if (flag22)
										{
											GameCanvas.isFocusPanel2 = true;
										}
									}
									bool flag23 = GameCanvas.panel2 != null;
									if (flag23)
									{
										bool flag24 = GameCanvas.isFocusPanel2;
										if (flag24)
										{
											GameCanvas.panel2.updateKey();
										}
										else
										{
											GameCanvas.panel.updateKey();
										}
									}
									else
									{
										GameCanvas.panel.updateKey();
									}
									bool flag25 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
									if (flag25)
									{
										GameCanvas.panel.chatTFUpdateKey();
									}
									else
									{
										bool flag26 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
										if (flag26)
										{
											GameCanvas.panel2.chatTFUpdateKey();
										}
										else
										{
											bool flag27 = (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null;
											if (flag27)
											{
												GameCanvas.panel.updateKey();
											}
											else
											{
												bool flag28 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
												if (flag28)
												{
													GameCanvas.panel2.updateKey();
												}
											}
										}
									}
									bool flag29 = GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine;
									if (flag29)
									{
										GameCanvas.panel.hide();
									}
								}
							}
						}
					}
				}
				GameCanvas.debug("E", 0);
				bool flag30 = !GameCanvas.isLoading;
				if (flag30)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				bool flag31 = !GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null;
				if (flag31)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			bool flag32 = this.resetToLoginScr;
			if (flag32)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.loginScr);
			}
			GameCanvas.debug("Zzz", 0);
			bool flag33 = (GameCanvas.currentScreen != GameCanvas.serverScr || !GameCanvas.serverScr.isPaintNewUi) && Controller.isConnectOK;
			if (flag33)
			{
				bool isMain = Controller.isMain;
				if (isMain)
				{
					ServerListScreen.testConnect = 2;
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			bool isDisconnected = Controller.isDisconnected;
			if (isDisconnected)
			{
				bool flag34 = !Controller.isMain;
				if (flag34)
				{
					bool flag35 = GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession;
					if (flag35)
					{
						GameCanvas.serverScreen.cancel();
					}
					bool flag36 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag36)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			bool isConnectionFail = Controller.isConnectionFail;
			if (isConnectionFail)
			{
				bool flag37 = !Controller.isMain;
				if (flag37)
				{
					bool flag38 = GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession;
					if (flag38)
					{
						ServerListScreen.testConnect = 0;
						GameCanvas.serverScreen.cancel();
						Debug.Log("connect fail 1");
					}
					bool flag39 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag39)
					{
						this.onConnectionFail();
						Debug.Log("connect fail 2");
					}
				}
				else
				{
					bool flag40 = Session_ME.gI().isCompareIPConnect();
					if (flag40)
					{
						this.onConnectionFail();
					}
					Debug.Log("connect fail 3");
				}
				Controller.isConnectionFail = false;
			}
			bool flag41 = Main.isResume;
			if (flag41)
			{
				Main.isResume = false;
				bool flag42 = GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null;
				if (flag42)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
			bool flag43 = GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr;
			if (flag43)
			{
				GameCanvas.xThongBaoTranslate += GameCanvas.dir_ * 2;
				bool flag44 = GameCanvas.xThongBaoTranslate - Panel.imgNew.getWidth() <= 60;
				if (flag44)
				{
					GameCanvas.dir_ = 0;
					this.tickWaitThongBao++;
					bool flag45 = this.tickWaitThongBao > 150;
					if (flag45)
					{
						this.tickWaitThongBao = 0;
						GameCanvas.thongBaoTest = null;
					}
				}
			}
			bool flag46 = GameCanvas.currentScreen != null && GameCanvas.currentScreen.Equals(GameScr.gI());
			if (flag46)
			{
				bool flag47 = GameScr.info1 != null;
				if (flag47)
				{
					GameScr.info1.update();
				}
				bool flag48 = GameScr.info2 != null;
				if (flag48)
				{
					GameScr.info2.update();
				}
			}
			GameCanvas.isPointerSelect = false;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0004E4F0 File Offset: 0x0004C6F0
	public void onDisconnected()
	{
		bool isConnectionFail = Controller.isConnectionFail;
		if (isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		Session_ME.gI().close();
		Session_ME2.gI().close();
		bool isLoadingData = Controller.isLoadingData;
		if (isLoadingData)
		{
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
		}
		else
		{
			Debug.LogError(">>>>onDisconnected");
			bool flag = GameCanvas.currentScreen != GameCanvas.serverScreen;
			if (flag)
			{
				GameCanvas.serverScreen.switchToMe();
				GameCanvas.startOK(mResources.maychutathoacmatsong + " [4]", 8884, null);
				Main.exit();
			}
			else
			{
				GameCanvas.endDlg();
			}
			global::Char.isLoadingMap = false;
			bool isMain = Controller.isMain;
			if (isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			mSystem.endKey();
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
	public void onConnectionFail()
	{
		bool flag = GameCanvas.currentScreen.Equals(SplashScr.instance);
		if (flag)
		{
			GameCanvas.startOK(mResources.maychutathoacmatsong + " [1]", 8884, null);
		}
		else
		{
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			bool isLoadingData = Controller.isLoadingData;
			if (isLoadingData)
			{
				GameCanvas.startOK(mResources.maychutathoacmatsong + " [2]", 8884, null);
				Controller.isConnectionFail = false;
			}
			else
			{
				GameCanvas.isResume = true;
				LoginScr.isContinueToLogin = false;
				LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
				bool flag2 = GameCanvas.currentScreen != GameCanvas.serverScreen;
				if (flag2)
				{
					ServerListScreen.countDieConnect = 0;
				}
				else
				{
					GameCanvas.endDlg();
					ServerListScreen.loadScreen = true;
					GameCanvas.serverScreen.switchToMe();
				}
				global::Char.isLoadingMap = false;
				bool isMain = Controller.isMain;
				if (isMain)
				{
					ServerListScreen.testConnect = 0;
				}
				mSystem.endKey();
			}
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0004E724 File Offset: 0x0004C924
	public static void connect()
	{
		bool flag = !Session_ME.gI().isConnected();
		if (flag)
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0004E75C File Offset: 0x0004C95C
	public static void connect2()
	{
		bool flag = !Session_ME2.gI().isConnected();
		if (flag)
		{
			Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2.ToString());
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00004C34 File Offset: 0x00002E34
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
	public static void resetTransGameScr(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.translate(0, 0);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-GameScr.cmx, -GameScr.cmy);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0004E80C File Offset: 0x0004CA0C
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00003E4C File Offset: 0x0000204C
	public void start()
	{
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0004E928 File Offset: 0x0004CB28
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0004E940 File Offset: 0x0004CB40
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00003E4C File Offset: 0x0000204C
	public static void debug(string s, int type)
	{
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0004E958 File Offset: 0x0004CB58
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = (GameCanvas.isTouch ? -1 : 0);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			bool flag = GameCanvas.panel.tabIcon != null;
			if (flag)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
		ServerListScreen.isAutoConect = true;
		ServerListScreen.countDieConnect = 0;
		ServerListScreen.testConnect = -1;
		ServerListScreen.loadScreen = true;
		bool flag3 = ServerListScreen.ipSelect == -1;
		if (flag3)
		{
			GameCanvas.serverScr.switchToMe();
		}
		else
		{
			bool flag4 = GameCanvas.serverScreen == null;
			if (flag4)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			GameCanvas.serverScreen.switchToMe();
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00003E4C File Offset: 0x0000204C
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00003E4C File Offset: 0x0000204C
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00003E4C File Offset: 0x0000204C
	public static void updateBG()
	{
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0004EB80 File Offset: 0x0004CD80
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		bool flag = cmy > GameCanvas.h;
		if (flag)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY != 0) ? (cmy >> detalY) : 0), w, h + ((detalY != 0) ? (cmy >> detalY) : 0));
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0004EBDC File Offset: 0x0004CDDC
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			bool flag = num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks);
			if (flag)
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				bool flag2 = GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7;
				if (flag2)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				bool flag3 = GameScr.gI().isFireWorks && !GameCanvas.lowGraphic;
				if (flag3)
				{
					FireWorkEff.paint(g);
				}
			}
			else
			{
				bool flag4 = GameCanvas.imgBG == null || GameCanvas.imgBG[num] == null;
				if (!flag4)
				{
					bool flag5 = GameCanvas.moveX[num] != 0;
					if (flag5)
					{
						GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
					}
					int cmy = GameScr.cmy;
					bool flag6 = cmy > GameCanvas.h;
					if (flag6)
					{
						cmy = GameCanvas.h;
					}
					bool flag7 = GameCanvas.layerSpeed[num] != 0;
					if (flag7)
					{
						for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					else
					{
						for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					bool flag8 = color1 != -1;
					if (flag8)
					{
						bool flag9 = num == GameCanvas.nBg - 1;
						if (flag9)
						{
							GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
						}
					}
					bool flag10 = color2 != -1;
					if (flag10)
					{
						bool flag11 = num == 0;
						if (flag11)
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
						}
					}
					bool flag12 = GameCanvas.currentScreen == GameScr.instance;
					if (flag12)
					{
						bool flag13 = layer == 1 && GameCanvas.typeBg == 11;
						if (flag13)
						{
							g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
						}
						bool flag14 = layer == 1 && GameCanvas.typeBg == 13;
						if (flag14)
						{
							g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + TileMap.tmw * 24 / 4, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
							g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + TileMap.tmw * 24 / 4 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						}
						bool flag15 = layer == 3 && TileMap.mapID == 1;
						if (flag15)
						{
							for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
							{
								g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
							}
						}
					}
					int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
					EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0004F0D8 File Offset: 0x0004D2D8
	public static void drawSun1(mGraphics g)
	{
		bool flag = GameCanvas.imgSun != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		bool flag2 = !GameCanvas.isBoltEff;
		if (!flag2)
		{
			bool flag3 = GameCanvas.gameTick % 200 == 0;
			if (flag3)
			{
				GameCanvas.boltActive = true;
			}
			bool flag4 = GameCanvas.boltActive;
			if (flag4)
			{
				GameCanvas.tBolt++;
				bool flag5 = GameCanvas.tBolt == 10;
				if (flag5)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				bool flag6 = GameCanvas.tBolt % 2 == 0;
				if (flag6)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0004F19C File Offset: 0x0004D39C
	public static void drawSun2(mGraphics g)
	{
		bool flag = GameCanvas.imgSun2 != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x0004F1D0 File Offset: 0x0004D3D0
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
	public static void paint_ios_bg(mGraphics g)
	{
		bool flag = mSystem.clientType != 5;
		if (!flag)
		{
			bool flag2 = GameCanvas.imgBgIOS != null;
			if (flag2)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				for (int i = 0; i < 3; i++)
				{
					g.drawImage(GameCanvas.imgBgIOS, GameCanvas.imgBgIOS.getWidth() * i, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			else
			{
				GameCanvas.imgBgIOS = mSystem.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
			}
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0004F2B0 File Offset: 0x0004D4B0
	public static void paintBGGameScr(mGraphics g)
	{
		bool flag = !GameCanvas.isLoadBGok;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			int gW = GameScr.gW;
			int gH = GameScr.gH;
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setColor(8421504);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00003E4C File Offset: 0x0000204C
	public static void resetBg()
	{
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0004F338 File Offset: 0x0004D538
	public static void getYBackground(int typeBg)
	{
		try
		{
			int gH = GameScr.gH23;
			switch (typeBg)
			{
			case 0:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_688;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_688;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_688;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_688;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_688;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_688;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_688;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_688;
			case 8:
			{
				GameCanvas.yb[0] = gH - 103 + 150;
				bool flag = TileMap.mapID == 103;
				if (flag)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_688;
			}
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_688;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_688;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_688;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_688;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_688;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_688;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_688;
			case 19:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_688;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_688:;
		}
		catch (Exception)
		{
			int gH2 = GameScr.gH23;
			for (int i = 0; i < GameCanvas.yb.Length; i++)
			{
				GameCanvas.yb[i] = 1;
			}
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0004FA20 File Offset: 0x0004DC20
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			bool flag = GameCanvas.typeBg == 12;
			if (flag)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			bool flag2 = (TileMap.lastBgID == typeBG && TileMap.lastType == TileMap.bgType) || typeBG == -1;
			if (!flag2)
			{
				GameCanvas.transY = 12;
				TileMap.lastBgID = (int)((sbyte)typeBG);
				TileMap.lastType = (int)((sbyte)TileMap.bgType);
				GameCanvas.layerSpeed = new int[]
				{
					1,
					2,
					3,
					7,
					8
				};
				GameCanvas.moveX = new int[5];
				GameCanvas.moveXSpeed = new int[5];
				GameCanvas.typeBg = typeBG;
				GameCanvas.isBoltEff = false;
				GameScr.firstY = GameScr.cmy;
				GameCanvas.imgBG = null;
				GameCanvas.imgCloud = null;
				GameCanvas.imgSun = null;
				GameCanvas.imgCaycot = null;
				GameScr.firstY = -1;
				switch (GameCanvas.typeBg)
				{
				case 0:
				{
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					bool flag3 = TileMap.bgType == 2;
					if (flag3)
					{
						GameCanvas.transY = 8;
					}
					goto IL_33E;
				}
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_33E;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_33E;
				case 4:
				{
					BackgroudEffect.addEffect(3);
					int[] array3 = new int[5];
					array3[1] = 1;
					GameCanvas.moveX = array3;
					int[] array4 = new int[5];
					array4[1] = 1;
					GameCanvas.moveXSpeed = array4;
					GameCanvas.nBg = 4;
					goto IL_33E;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_33E;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_33E;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_33E;
				case 12:
				{
					int[] array7 = new int[5];
					array7[0] = 1;
					array7[1] = 1;
					GameCanvas.moveX = array7;
					int[] array8 = new int[5];
					array8[0] = 2;
					array8[1] = 1;
					GameCanvas.moveXSpeed = array8;
					GameCanvas.nBg = 3;
					goto IL_33E;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_33E;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_33E;
				case 16:
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					goto IL_33E;
				case 19:
				{
					int[] array9 = new int[5];
					array9[1] = 2;
					array9[2] = 1;
					GameCanvas.moveX = array9;
					int[] array10 = new int[5];
					array10[1] = 2;
					array10[2] = 1;
					GameCanvas.moveXSpeed = array10;
					GameCanvas.nBg = 5;
					goto IL_33E;
				}
				}
				GameCanvas.layerSpeed = new int[]
				{
					1,
					3,
					5,
					7
				};
				GameCanvas.nBg = 4;
				IL_33E:
				bool flag4 = typeBG <= 16;
				if (flag4)
				{
					GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
				}
				else
				{
					try
					{
						string path = "/bg/b" + GameCanvas.typeBg.ToString() + 3.ToString() + ".png";
						bool flag5 = TileMap.bgType != 0;
						if (flag5)
						{
							path = string.Concat(new string[]
							{
								"/bg/b",
								GameCanvas.typeBg.ToString(),
								3.ToString(),
								"-",
								TileMap.bgType.ToString(),
								".png"
							});
						}
						int[] array11 = new int[1];
						Image image = GameCanvas.loadImageRMS(path);
						image.getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
						GameCanvas.skyColor = array11[0];
					}
					catch (Exception)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
					}
				}
				GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
				GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
				for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
				{
					GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
					GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
				}
				bool flag6 = GameCanvas.lowGraphic;
				if (flag6)
				{
					GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
				}
				else
				{
					GameCanvas.imgBG = new Image[GameCanvas.nBg];
					GameCanvas.bgW = new int[GameCanvas.nBg];
					GameCanvas.bgH = new int[GameCanvas.nBg];
					GameCanvas.colorBotton = new int[GameCanvas.nBg];
					GameCanvas.colorTop = new int[GameCanvas.nBg];
					bool flag7 = TileMap.bgType == 100;
					if (flag7)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							bool flag8 = GameCanvas.imgBG[j] != null;
							if (flag8)
							{
								int[] array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
								GameCanvas.colorTop[j] = array12[0];
								array12 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array12, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
								GameCanvas.colorBotton[j] = array12[0];
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
							else
							{
								bool flag9 = GameCanvas.nBg > 1;
								if (flag9)
								{
									GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
									GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < GameCanvas.nBg; k++)
						{
							string path2 = "/bg/b" + GameCanvas.typeBg.ToString() + k.ToString() + ".png";
							bool flag10 = TileMap.bgType != 0;
							if (flag10)
							{
								path2 = string.Concat(new string[]
								{
									"/bg/b",
									GameCanvas.typeBg.ToString(),
									k.ToString(),
									"-",
									TileMap.bgType.ToString(),
									".png"
								});
							}
							GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(path2);
							bool flag11 = GameCanvas.imgBG[k] != null;
							if (flag11)
							{
								int[] array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
								GameCanvas.colorTop[k] = array13[0];
								array13 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array13, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
								GameCanvas.colorBotton[k] = array13[0];
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
							else
							{
								bool flag12 = GameCanvas.nBg > 1;
								if (flag12)
								{
									GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg.ToString() + "0.png");
									GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
									GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
								}
							}
						}
					}
					GameCanvas.getYBackground(GameCanvas.typeBg);
					GameCanvas.cloudX = new int[]
					{
						GameScr.gW / 2 - 40,
						GameScr.gW / 2 + 40,
						GameScr.gW / 2 - 100,
						GameScr.gW / 2 - 80,
						GameScr.gW / 2 - 120
					};
					GameCanvas.cloudY = new int[]
					{
						130,
						100,
						150,
						140,
						80
					};
					GameCanvas.imgSunSpec = null;
					bool flag13 = GameCanvas.typeBg != 0;
					if (flag13)
					{
						bool flag14 = GameCanvas.typeBg == 2;
						if (flag14)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
							TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts");
						}
						else
						{
							bool flag15 = GameCanvas.typeBg == 19;
							if (flag15)
							{
								TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_32");
							}
							else
							{
								bool flag16 = GameCanvas.typeBg == 4;
								if (flag16)
								{
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
									GameCanvas.sunX = GameScr.gW / 2 + 30;
									GameCanvas.sunY = GameCanvas.yb[3];
								}
								else
								{
									bool flag17 = GameCanvas.typeBg == 7;
									if (flag17)
									{
										GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
										GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
										GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
										GameCanvas.sunY = GameCanvas.yb[3] - 80;
										GameCanvas.sunX2 = GameCanvas.sunX - 100;
										GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
									}
									else
									{
										bool flag18 = GameCanvas.typeBg == 6;
										if (flag18)
										{
											GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
											GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
											GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
											GameCanvas.sunY = GameCanvas.yb[4];
											GameCanvas.sunX2 = GameCanvas.sunX - 100;
											GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
										}
										else
										{
											bool flag19 = typeBG == 5;
											if (flag19)
											{
												GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
												GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
												GameCanvas.sunX = GameScr.gW / 2 - 50;
												GameCanvas.sunY = GameCanvas.yb[3] + 20;
												GameCanvas.sunX2 = GameScr.gW / 2 + 20;
												GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
											}
											else
											{
												bool flag20 = GameCanvas.typeBg == 8 && TileMap.mapID < 90;
												if (flag20)
												{
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
													GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
													GameCanvas.sunX = GameScr.gW / 2 - 30;
													GameCanvas.sunY = GameCanvas.yb[3] + 60;
													GameCanvas.sunX2 = GameScr.gW / 2 + 20;
													GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
												}
												else
												{
													switch (typeBG)
													{
													case 9:
														GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
														GameCanvas.sunY = GameCanvas.yb[4] + 20;
														GameCanvas.sunX2 = GameCanvas.sunX - 80;
														GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
														goto IL_1119;
													case 10:
														GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
														GameCanvas.sunY = GameCanvas.yb[1] - 30;
														GameCanvas.sunX2 = GameCanvas.sunX - 80;
														GameCanvas.sunY2 = GameCanvas.yb[1];
														goto IL_1119;
													case 11:
														GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
														GameCanvas.sunX = GameScr.gW / 2 - 30;
														GameCanvas.sunY = GameCanvas.yb[2] - 30;
														goto IL_1119;
													case 12:
														GameCanvas.cloudY = new int[]
														{
															200,
															170,
															220,
															150,
															250
														};
														goto IL_1119;
													case 16:
														GameCanvas.cloudX = new int[]
														{
															90,
															170,
															250,
															320,
															400,
															450,
															500
														};
														GameCanvas.cloudY = new int[]
														{
															GameCanvas.yb[2] + 5,
															GameCanvas.yb[2] - 20,
															GameCanvas.yb[2] - 50,
															GameCanvas.yb[2] - 30,
															GameCanvas.yb[2] - 50,
															GameCanvas.yb[2],
															GameCanvas.yb[2] - 40
														};
														GameCanvas.imgSunSpec = new Image[7];
														for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
														{
															int num = 161;
															bool flag21 = l == 0 || l == 2 || l == 3 || l == 2 || l == 6;
															if (flag21)
															{
																num = 160;
															}
															GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num.ToString() + ".png");
														}
														goto IL_1119;
													case 19:
													{
														int[] array14 = new int[5];
														array14[1] = 2;
														array14[2] = 1;
														GameCanvas.moveX = array14;
														int[] array15 = new int[5];
														array15[1] = 2;
														array15[2] = 1;
														GameCanvas.moveXSpeed = array15;
														GameCanvas.nBg = 5;
														goto IL_1119;
													}
													}
													GameCanvas.imgCloud = null;
													GameCanvas.imgSun = null;
													GameCanvas.imgSun2 = null;
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun" + typeBG.ToString() + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType.ToString()) : string.Empty) + ".png");
													bool flag22 = GameCanvas.loadImageRMS("/tWater/water_flow_" + typeBG.ToString()) != null;
													if (flag22)
													{
														TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/water_flow_" + typeBG.ToString());
													}
													GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
													GameCanvas.sunY = GameCanvas.yb[2] - 30;
													IL_1119:;
												}
											}
										}
									}
								}
							}
						}
					}
					GameCanvas.paintBG = false;
					bool flag23 = !GameCanvas.paintBG;
					if (flag23)
					{
						GameCanvas.paintBG = true;
					}
				}
			}
		}
		catch (Exception)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00050BA8 File Offset: 0x0004EDA8
	private static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			bool flag = typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0;
			if (flag)
			{
				BackgroudEffect.addEffect(0);
				break;
			}
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00050BF4 File Offset: 0x0004EDF4
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		bool flag = (keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31;
		if (flag)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00050C50 File Offset: 0x0004EE50
	public void mapKeyPress(int keyCode)
	{
		bool flag = GameCanvas.currentDialog != null;
		if (flag)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
		}
		else
		{
			GameCanvas.currentScreen.keyPress(keyCode);
			if (keyCode <= -22)
			{
				if (keyCode <= -38)
				{
					if (keyCode == -39)
					{
						goto IL_179;
					}
					if (keyCode != -38)
					{
						return;
					}
				}
				else
				{
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode != -22)
					{
						return;
					}
					goto IL_3FF;
				}
			}
			else
			{
				if (keyCode <= -1)
				{
					if (keyCode != -21)
					{
						switch (keyCode)
						{
						case -8:
							GameCanvas.keyHold[14] = true;
							GameCanvas.keyPressed[14] = true;
							return;
						case -7:
							goto IL_3FF;
						case -6:
							break;
						case -5:
							goto IL_275;
						case -4:
						{
							bool flag2 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
							if (flag2)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
							}
							else
							{
								GameCanvas.keyHold[24] = true;
								GameCanvas.keyPressed[24] = true;
							}
							return;
						}
						case -3:
						{
							bool flag3 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
							if (flag3)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
							}
							else
							{
								GameCanvas.keyHold[23] = true;
								GameCanvas.keyPressed[23] = true;
							}
							return;
						}
						case -2:
							goto IL_179;
						case -1:
							goto IL_127;
						default:
							return;
						}
					}
					GameCanvas.keyHold[12] = true;
					GameCanvas.keyPressed[12] = true;
					return;
				}
				if (keyCode != 10)
				{
					switch (keyCode)
					{
					case 35:
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
						return;
					case 42:
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					case 48:
						GameCanvas.keyHold[0] = true;
						GameCanvas.keyPressed[0] = true;
						return;
					case 49:
					{
						bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag4)
						{
							GameCanvas.keyHold[1] = true;
							GameCanvas.keyPressed[1] = true;
						}
						return;
					}
					case 50:
					{
						bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag5)
						{
							GameCanvas.keyHold[2] = true;
							GameCanvas.keyPressed[2] = true;
						}
						return;
					}
					case 51:
					{
						bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag6)
						{
							GameCanvas.keyHold[3] = true;
							GameCanvas.keyPressed[3] = true;
						}
						return;
					}
					case 52:
					{
						bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag7)
						{
							GameCanvas.keyHold[4] = true;
							GameCanvas.keyPressed[4] = true;
						}
						return;
					}
					case 53:
					{
						bool flag8 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag8)
						{
							GameCanvas.keyHold[5] = true;
							GameCanvas.keyPressed[5] = true;
						}
						return;
					}
					case 54:
					{
						bool flag9 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag9)
						{
							GameCanvas.keyHold[6] = true;
							GameCanvas.keyPressed[6] = true;
						}
						return;
					}
					case 55:
						GameCanvas.keyHold[7] = true;
						GameCanvas.keyPressed[7] = true;
						return;
					case 56:
					{
						bool flag10 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag10)
						{
							GameCanvas.keyHold[8] = true;
							GameCanvas.keyPressed[8] = true;
						}
						return;
					}
					case 57:
						GameCanvas.keyHold[9] = true;
						GameCanvas.keyPressed[9] = true;
						return;
					default:
						if (keyCode != 113)
						{
							return;
						}
						GameCanvas.keyHold[17] = true;
						GameCanvas.keyPressed[17] = true;
						return;
					}
				}
				IL_275:
				bool flag11 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
				if (flag11)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[25] = true;
				GameCanvas.keyPressed[25] = true;
				GameCanvas.keyHold[15] = true;
				GameCanvas.keyPressed[15] = true;
				return;
			}
			IL_127:
			bool flag12 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
			if (flag12)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			else
			{
				GameCanvas.keyHold[21] = true;
				GameCanvas.keyPressed[21] = true;
			}
			return;
			IL_179:
			bool flag13 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
			if (flag13)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			else
			{
				GameCanvas.keyHold[22] = true;
				GameCanvas.keyPressed[22] = true;
			}
			return;
			IL_3FF:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00004C5F File Offset: 0x00002E5F
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00051238 File Offset: 0x0004F438
	public void mapKeyRelease(int keyCode)
	{
		if (keyCode > -22)
		{
			if (keyCode <= -1)
			{
				if (keyCode != -21)
				{
					switch (keyCode)
					{
					case -8:
						GameCanvas.keyHold[14] = false;
						return;
					case -7:
						goto IL_278;
					case -6:
						break;
					case -5:
						goto IL_12F;
					case -4:
						GameCanvas.keyHold[24] = false;
						return;
					case -3:
						GameCanvas.keyHold[23] = false;
						return;
					case -2:
						goto IL_105;
					case -1:
						goto IL_F7;
					default:
						return;
					}
				}
				GameCanvas.keyHold[12] = false;
				GameCanvas.keyReleased[12] = true;
				return;
			}
			if (keyCode != 10)
			{
				switch (keyCode)
				{
				case 35:
					GameCanvas.keyHold[11] = false;
					GameCanvas.keyReleased[11] = true;
					return;
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
					return;
				case 42:
					GameCanvas.keyHold[10] = false;
					GameCanvas.keyReleased[10] = true;
					return;
				case 48:
					GameCanvas.keyHold[0] = false;
					GameCanvas.keyReleased[0] = true;
					return;
				case 49:
				{
					bool flag = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag)
					{
						GameCanvas.keyHold[1] = false;
						GameCanvas.keyReleased[1] = true;
					}
					return;
				}
				case 50:
				{
					bool flag2 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag2)
					{
						GameCanvas.keyHold[2] = false;
						GameCanvas.keyReleased[2] = true;
					}
					return;
				}
				case 51:
				{
					bool flag3 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag3)
					{
						GameCanvas.keyHold[3] = false;
						GameCanvas.keyReleased[3] = true;
					}
					return;
				}
				case 52:
				{
					bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag4)
					{
						GameCanvas.keyHold[4] = false;
						GameCanvas.keyReleased[4] = true;
					}
					return;
				}
				case 53:
				{
					bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag5)
					{
						GameCanvas.keyHold[5] = false;
						GameCanvas.keyReleased[5] = true;
					}
					return;
				}
				case 54:
				{
					bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag6)
					{
						GameCanvas.keyHold[6] = false;
						GameCanvas.keyReleased[6] = true;
					}
					return;
				}
				case 55:
					GameCanvas.keyHold[7] = false;
					GameCanvas.keyReleased[7] = true;
					return;
				case 56:
				{
					bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag7)
					{
						GameCanvas.keyHold[8] = false;
						GameCanvas.keyReleased[8] = true;
					}
					return;
				}
				case 57:
					GameCanvas.keyHold[9] = false;
					GameCanvas.keyReleased[9] = true;
					return;
				default:
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
			}
			IL_12F:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
		}
		if (keyCode <= -38)
		{
			if (keyCode == -39)
			{
				goto IL_105;
			}
			if (keyCode != -38)
			{
				return;
			}
		}
		else
		{
			if (keyCode == -26)
			{
				GameCanvas.keyHold[16] = false;
				return;
			}
			if (keyCode != -22)
			{
				return;
			}
			goto IL_278;
		}
		IL_F7:
		GameCanvas.keyHold[21] = false;
		return;
		IL_105:
		GameCanvas.keyHold[22] = false;
		return;
		IL_278:
		GameCanvas.keyHold[13] = false;
		GameCanvas.keyReleased[13] = true;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00004C70 File Offset: 0x00002E70
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00051684 File Offset: 0x0004F884
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		bool flag = GameCanvas.panel != null && GameCanvas.panel.isShow;
		if (flag)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x000516C0 File Offset: 0x0004F8C0
	public void pointerDragged(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		bool flag = Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10;
		if (flag)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		bool flag2 = GameCanvas.curPos > 3;
		if (flag2)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00051750 File Offset: 0x0004F950
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00051784 File Offset: 0x0004F984
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerSelect = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerMove = false;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x000517E4 File Offset: 0x0004F9E4
	public void pointerReleased(int x, int y)
	{
		bool flag = !GameCanvas.isPointerMove;
		if (flag)
		{
			GameCanvas.isPointerSelect = true;
		}
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerMove = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerClick = true;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00051830 File Offset: 0x0004FA30
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		bool flag = !GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
			result = flag2;
		}
		return result;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00051894 File Offset: 0x0004FA94
	public static bool isPointSelect(int x, int y, int w, int h)
	{
		bool flag = !GameCanvas.isPointerSelect;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
			result = flag2;
		}
		return result;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x000518EC File Offset: 0x0004FAEC
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00051934 File Offset: 0x0004FB34
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00051968 File Offset: 0x0004FB68
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00051998 File Offset: 0x0004FB98
	public static void checkBackButton()
	{
		bool flag = ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000519F4 File Offset: 0x0004FBF4
	public void paintChangeMap(mGraphics g)
	{
		string empty = string.Empty;
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin.ToString() + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00051AAC File Offset: 0x0004FCAC
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			bool flag = GameCanvas.currentScreen != null;
			if (flag)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool isShow = GameCanvas.panel.isShow;
			if (isShow)
			{
				GameCanvas.panel.paint(this.g);
				bool flag2 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
				if (flag2)
				{
					GameCanvas.panel2.paint(this.g);
				}
				bool flag3 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
				if (flag3)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
				if (flag4)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			bool flag5 = GameCanvas.currentDialog != null;
			if (flag5)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else
			{
				bool showMenu = GameCanvas.menu.showMenu;
				if (showMenu)
				{
					GameCanvas.debug("PD", 1);
					GameCanvas.resetTrans(this.g);
					GameCanvas.menu.paintMenu(this.g);
				}
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			bool flag6 = GameScr.gI().popUpYesNo != null;
			if (flag6)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			bool flag7 = ChatPopup.currChatPopup != null;
			if (flag7)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			bool flag8 = ChatPopup.serverChatPopUp != null;
			if (flag8)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				bool flag9 = effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp);
				if (flag9)
				{
					effect.paint(this.g);
				}
			}
			bool flag10 = GameCanvas.currentDialog != null;
			if (flag10)
			{
				GameCanvas.currentDialog.paint(this.g);
			}
			bool flag11 = GameCanvas.isWait();
			if (flag11)
			{
				this.paintChangeMap(this.g);
				bool flag12 = GameCanvas.timeLoading > 0 && LoginScr.timeLogin <= 0 && mSystem.currentTimeMillis() - GameCanvas.TIMEOUT >= 1000L;
				if (flag12)
				{
					GameCanvas.timeLoading--;
					bool flag13 = GameCanvas.timeLoading == 0;
					if (flag13)
					{
						GameCanvas.timeLoading = 15;
					}
					GameCanvas.TIMEOUT = mSystem.currentTimeMillis();
				}
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			bool flag14 = GameCanvas.open3Hour && !GameCanvas.isLoading;
			if (flag14)
			{
				bool flag15 = GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr;
				if (flag15)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				bool flag16 = GameCanvas.currentScreen == CreateCharScr.instance;
				if (flag16)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
			GameCanvas.resetTrans(this.g);
			int num = GameCanvas.h / 4;
			bool flag17 = GameCanvas.currentScreen != null && GameCanvas.currentScreen is GameScr && GameCanvas.thongBaoTest != null;
			if (flag17)
			{
				this.g.setClip(60, num, GameCanvas.w - 120, mFont.tahoma_7_white.getHeight() + 2);
				mFont.tahoma_7_grey.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num + 1, 0);
				mFont.tahoma_7_yellow.drawString(this.g, GameCanvas.thongBaoTest, GameCanvas.xThongBaoTranslate, num, 0);
				this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00051FB0 File Offset: 0x000501B0
	public static void endDlg()
	{
		bool flag = GameCanvas.inputDlg != null;
		if (flag)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00051FF0 File Offset: 0x000501F0
	public static void startOKDlg(string info)
	{
		bool flag = info == "Không thể đổi khu vực trong map này";
		if (!flag)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00052044 File Offset: 0x00050244
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00052044 File Offset: 0x00050244
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00004C7F File Offset: 0x00002E7F
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00004C8E File Offset: 0x00002E8E
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00004CC3 File Offset: 0x00002EC3
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00052090 File Offset: 0x00050290
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00004CF6 File Offset: 0x00002EF6
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00004D19 File Offset: 0x00002F19
	public static void startserverThongBao(string msgSv)
	{
		GameCanvas.thongBaoTest = msgSv;
		GameCanvas.xThongBaoTranslate = GameCanvas.w - 60;
		GameCanvas.dir_ = -1;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x000520F0 File Offset: 0x000502F0
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			bool flag = m >= 1000;
			if (!flag)
			{
				text = m.ToString() + text;
				break;
			}
			int num2 = m % 1000;
			text = ((num2 != 0) ? ((num2 >= 10) ? ((num2 >= 100) ? ("." + num2.ToString() + text) : (".0" + num2.ToString() + text)) : (".00" + num2.ToString() + text)) : (".000" + text));
			m /= 1000;
		}
		return text;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x000521BC File Offset: 0x000503BC
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000521D8 File Offset: 0x000503D8
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00003E4C File Offset: 0x0000204C
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x000521F4 File Offset: 0x000503F4
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00052208 File Offset: 0x00050408
	public static Image loadImageRMS(string path)
	{
		path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				string filename = "x" + mGraphics.zoomLevel.ToString() + array[array.Length - 1];
				sbyte[] array2 = Rms.loadRMS(filename);
				bool flag = array2 != null;
				if (flag)
				{
					result = Image.createImage(array2, 0, array2.Length);
				}
			}
			catch (Exception)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
			}
		}
		return result;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x000522DC File Offset: 0x000504DC
	public static Image loadImage(string path)
	{
		path = Main.res + "/x" + mGraphics.zoomLevel.ToString() + path;
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00052338 File Offset: 0x00050538
	public static string cutPng(string str)
	{
		string result = str;
		bool flag = str.Contains(".png");
		if (flag)
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00052370 File Offset: 0x00050570
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00052394 File Offset: 0x00050594
	public bool startDust(int dir, int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (dir != 1) ? 1 : 0;
			bool flag2 = this.dustState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				this.dustState[num] = 0;
				this.dustX[num] = x;
				this.dustY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x000523F0 File Offset: 0x000505F0
	public void loadWaterSplash()
	{
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			GameCanvas.imgWS = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i.ToString() + ".png");
			}
			GameCanvas.wsX = new int[2];
			GameCanvas.wsY = new int[2];
			GameCanvas.wsState = new int[2];
			GameCanvas.wsF = new int[2];
			GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0005248C File Offset: 0x0005068C
	public bool startWaterSplash(int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
			bool flag2 = GameCanvas.wsState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				GameCanvas.wsState[num] = 0;
				GameCanvas.wsX[num] = x;
				GameCanvas.wsY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000524EC File Offset: 0x000506EC
	public void updateWaterSplash()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = GameCanvas.wsState[i] == -1;
				if (!flag2)
				{
					GameCanvas.wsY[i]--;
					bool flag3 = GameCanvas.gameTick % 2 == 0;
					if (flag3)
					{
						GameCanvas.wsState[i]++;
						bool flag4 = GameCanvas.wsState[i] > 2;
						if (flag4)
						{
							GameCanvas.wsState[i] = -1;
						}
						else
						{
							GameCanvas.wsF[i] = GameCanvas.wsState[i];
						}
					}
				}
			}
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0005258C File Offset: 0x0005078C
	public void updateDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1;
				if (flag2)
				{
					this.dustState[i]++;
					bool flag3 = this.dustState[i] >= 5;
					if (flag3)
					{
						this.dustState[i] = -1;
					}
					bool flag4 = i == 0;
					if (flag4)
					{
						this.dustX[i]--;
					}
					else
					{
						this.dustX[i]++;
					}
					this.dustY[i]--;
				}
			}
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00052648 File Offset: 0x00050848
	public static bool isPaint(int x, int y)
	{
		bool flag = x < GameScr.cmx;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = x > GameScr.cmx + GameScr.gW;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = y < GameScr.cmy;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = y > GameScr.cmy + GameScr.gH + 30;
					result = !flag4;
				}
			}
		}
		return result;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000526B0 File Offset: 0x000508B0
	public void paintDust(mGraphics g)
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]);
				if (flag2)
				{
					g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
				}
			}
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00052730 File Offset: 0x00050930
	public void loadDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			bool flag2 = GameCanvas.imgDust == null;
			if (flag2)
			{
				GameCanvas.imgDust = new Image[2][];
				for (int i = 0; i < GameCanvas.imgDust.Length; i++)
				{
					GameCanvas.imgDust[i] = new Image[5];
				}
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						GameCanvas.imgDust[j][k] = GameCanvas.loadImage("/e/d" + j.ToString() + k.ToString() + ".png");
					}
				}
			}
			this.dustX = new int[2];
			this.dustY = new int[2];
			this.dustState = new int[2];
			this.dustState[0] = (this.dustState[1] = -1);
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00052828 File Offset: 0x00050A28
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00004D35 File Offset: 0x00002F35
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00051830 File Offset: 0x0004FA30
	public static bool isPointer(int x, int y, int w, int h)
	{
		bool flag = !GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
			result = flag2;
		}
		return result;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00052860 File Offset: 0x00050A60
	public void perform(int idAction, object p)
	{
		if (idAction <= 88839)
		{
			if (idAction <= 8889)
			{
				if (idAction == 999)
				{
					mSystem.closeBanner();
					GameCanvas.endDlg();
					return;
				}
				switch (idAction)
				{
				case 8881:
				{
					string url = (string)p;
					try
					{
						GameMidlet.instance.platformRequest(url);
					}
					catch (Exception)
					{
					}
					GameCanvas.currentDialog = null;
					return;
				}
				case 8882:
					InfoDlg.hide();
					GameCanvas.currentDialog = null;
					ServerListScreen.isAutoConect = false;
					ServerListScreen.countDieConnect = 0;
					return;
				case 8883:
					return;
				case 8884:
				{
					GameCanvas.endDlg();
					bool flag = GameCanvas.serverScr == null;
					if (flag)
					{
						GameCanvas.serverScr = new ServerScr();
					}
					GameCanvas.serverScr.switchToMe();
					return;
				}
				case 8885:
					GameMidlet.instance.exit();
					return;
				case 8886:
				{
					GameCanvas.endDlg();
					string name = (string)p;
					Service.gI().addFriend(name);
					return;
				}
				case 8887:
				{
					GameCanvas.endDlg();
					int charId = (int)p;
					Service.gI().addPartyAccept(charId);
					return;
				}
				case 8888:
				{
					int charId2 = (int)p;
					Service.gI().addPartyCancel(charId2);
					GameCanvas.endDlg();
					return;
				}
				case 8889:
				{
					string str = (string)p;
					GameCanvas.endDlg();
					Service.gI().acceptPleaseParty(str);
					return;
				}
				default:
					return;
				}
			}
			else
			{
				if (idAction == 9000)
				{
					GameCanvas.endDlg();
					SplashScr.imgLogo = null;
					SmallImage.loadBigRMS();
					mSystem.gcc();
					ServerListScreen.bigOk = true;
					ServerListScreen.loadScreen = true;
					GameScr.gI().loadGameScr();
					bool flag2 = GameCanvas.currentScreen != GameCanvas.loginScr;
					if (flag2)
					{
						GameCanvas.serverScreen.switchToMe2();
					}
					return;
				}
				if (idAction == 9999)
				{
					GameCanvas.endDlg();
					GameCanvas.connect();
					Service.gI().setClientType();
					bool flag3 = GameCanvas.loginScr == null;
					if (flag3)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.loginScr.doLogin();
					return;
				}
				switch (idAction)
				{
				case 88810:
				{
					int playerMapId = (int)p;
					GameCanvas.endDlg();
					Service.gI().acceptInviteTrade(playerMapId);
					return;
				}
				case 88811:
					GameCanvas.endDlg();
					Service.gI().cancelInviteTrade();
					return;
				case 88812:
				case 88813:
				case 88815:
				case 88816:
				case 88830:
				case 88831:
				case 88832:
				case 88833:
				case 88834:
				case 88835:
				case 88838:
					return;
				case 88814:
				{
					Item[] items = (Item[])p;
					GameCanvas.endDlg();
					Service.gI().crystalCollectLock(items);
					return;
				}
				case 88817:
					ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
					Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
					return;
				case 88818:
				{
					short menuId = (short)p;
					Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
					GameCanvas.endDlg();
					return;
				}
				case 88819:
				{
					short menuId2 = (short)p;
					Service.gI().menuId(menuId2);
					return;
				}
				case 88820:
				{
					string[] array = (string[])p;
					bool flag4 = global::Char.myCharz().npcFocus == null;
					if (flag4)
					{
						return;
					}
					int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
					bool flag5 = array.Length > 1;
					if (flag5)
					{
						MyVector myVector = new MyVector();
						for (int i = 0; i < array.Length - 1; i++)
						{
							myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
						}
						GameCanvas.menu.startAt(myVector, 3);
					}
					else
					{
						ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
						Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
					}
					return;
				}
				case 88821:
				{
					int menuId3 = (int)p;
					ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
					Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
					return;
				}
				case 88822:
					ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
					Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
					return;
				case 88823:
					GameCanvas.startOKDlg(mResources.SENTMSG);
					return;
				case 88824:
					GameCanvas.startOKDlg(mResources.NOSENDMSG);
					return;
				case 88825:
					GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
					return;
				case 88826:
					GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
					return;
				case 88827:
					GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
					return;
				case 88828:
					GameCanvas.startOKDlg(mResources.sendMsgFail);
					return;
				case 88829:
				{
					string text = GameCanvas.inputDlg.tfInput.getText();
					bool flag6 = !text.Equals(string.Empty);
					if (flag6)
					{
						Service.gI().changeName(text, (int)p);
						InfoDlg.showWait();
					}
					return;
				}
				case 88836:
					GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
					GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
					return;
				case 88837:
					break;
				case 88839:
					goto IL_775;
				default:
					return;
				}
			}
		}
		else if (idAction <= 100016)
		{
			switch (idAction)
			{
			case 100001:
				Service.gI().getFlag(0, -1);
				InfoDlg.showWait();
				return;
			case 100002:
			{
				bool flag7 = GameCanvas.loginScr == null;
				if (flag7)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.backToRegister();
				return;
			}
			case 100003:
			case 100004:
				return;
			case 100005:
			{
				bool flag8 = global::Char.myCharz().statusMe == 14;
				if (flag8)
				{
					GameCanvas.startOKDlg(mResources.can_not_do_when_die);
				}
				else
				{
					Service.gI().openUIZone();
				}
				return;
			}
			case 100006:
				mSystem.onDisconnected();
				return;
			default:
				if (idAction != 100016)
				{
					return;
				}
				ServerListScreen.SetIpSelect(17, false);
				GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
				ServerListScreen.waitToLogin = true;
				GameCanvas.endDlg();
				return;
			}
		}
		else
		{
			switch (idAction)
			{
			case 101023:
				Main.numberQuit = 0;
				return;
			case 101024:
				Res.outz("output 101024");
				GameCanvas.endDlg();
				return;
			case 101025:
			{
				GameCanvas.endDlg();
				bool loadScreen = ServerListScreen.loadScreen;
				if (loadScreen)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				else
				{
					GameCanvas.serverScreen.show2();
				}
				return;
			}
			case 101026:
				mSystem.onDisconnected();
				return;
			default:
				if (idAction != 888361)
				{
					switch (idAction)
					{
					case 888391:
						goto IL_7EE;
					case 888392:
						Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
						return;
					case 888393:
					{
						bool flag9 = GameCanvas.loginScr == null;
						if (flag9)
						{
							GameCanvas.loginScr = new LoginScr();
						}
						GameCanvas.loginScr.doLogin();
						Main.closeKeyBoard();
						return;
					}
					case 888394:
						GameCanvas.endDlg();
						return;
					case 888395:
						GameCanvas.endDlg();
						return;
					case 888396:
						GameCanvas.endDlg();
						return;
					case 888397:
					{
						string text2 = (string)p;
						return;
					}
					default:
						return;
					}
				}
				else
				{
					string text3 = GameCanvas.inputDlg.tfInput.getText();
					GameCanvas.endDlg();
					bool flag10 = text3.Length < 6 || text3.Equals(string.Empty);
					if (flag10)
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
						return;
					}
					try
					{
						Service.gI().activeAccProtect(int.Parse(text3));
						return;
					}
					catch (Exception ex)
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
						Cout.println("Loi tai 888361 Gamescavas " + ex.ToString());
						return;
					}
				}
				break;
			}
		}
		string text4 = GameCanvas.inputDlg.tfInput.getText();
		GameCanvas.endDlg();
		try
		{
			Service.gI().openLockAccProtect(int.Parse(text4.Trim()));
			return;
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tai 88837 " + ex2.ToString());
			return;
		}
		IL_775:
		string text5 = GameCanvas.inputDlg.tfInput.getText();
		GameCanvas.endDlg();
		bool flag11 = text5.Length < 6 || text5.Equals(string.Empty);
		if (flag11)
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			return;
		}
		try
		{
			GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text5, 8882, null);
			return;
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
			return;
		}
		IL_7EE:
		string s = (string)p;
		GameCanvas.endDlg();
		Service.gI().clearAccProtect(int.Parse(s));
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00004D3F File Offset: 0x00002F3F
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerSelect = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00053218 File Offset: 0x00051418
	public static bool isWait()
	{
		return global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait || SelectCharScr.isWait;
	}

	// Token: 0x040006B9 RID: 1721
	public static long timeNow = 0L;

	// Token: 0x040006BA RID: 1722
	public static bool open3Hour;

	// Token: 0x040006BB RID: 1723
	public static bool lowGraphic = true;

	// Token: 0x040006BC RID: 1724
	public static bool serverchat = false;

	// Token: 0x040006BD RID: 1725
	public static bool isMoveNumberPad = true;

	// Token: 0x040006BE RID: 1726
	public static bool isLoading;

	// Token: 0x040006BF RID: 1727
	public static bool isTouch = false;

	// Token: 0x040006C0 RID: 1728
	public static bool isTouchControl;

	// Token: 0x040006C1 RID: 1729
	public static bool isTouchControlSmallScreen;

	// Token: 0x040006C2 RID: 1730
	public static bool isTouchControlLargeScreen;

	// Token: 0x040006C3 RID: 1731
	public static bool isConnectFail;

	// Token: 0x040006C4 RID: 1732
	public static GameCanvas instance;

	// Token: 0x040006C5 RID: 1733
	public static bool bRun;

	// Token: 0x040006C6 RID: 1734
	public static bool[] keyPressed = new bool[30];

	// Token: 0x040006C7 RID: 1735
	public static bool[] keyReleased = new bool[30];

	// Token: 0x040006C8 RID: 1736
	public static bool[] keyHold = new bool[30];

	// Token: 0x040006C9 RID: 1737
	public static bool isPointerDown;

	// Token: 0x040006CA RID: 1738
	public static bool isPointerClick;

	// Token: 0x040006CB RID: 1739
	public static bool isPointerJustRelease;

	// Token: 0x040006CC RID: 1740
	public static bool isPointerSelect;

	// Token: 0x040006CD RID: 1741
	public static bool isPointerMove;

	// Token: 0x040006CE RID: 1742
	public static int px;

	// Token: 0x040006CF RID: 1743
	public static int py;

	// Token: 0x040006D0 RID: 1744
	public static int pxFirst;

	// Token: 0x040006D1 RID: 1745
	public static int pyFirst;

	// Token: 0x040006D2 RID: 1746
	public static int pxLast;

	// Token: 0x040006D3 RID: 1747
	public static int pyLast;

	// Token: 0x040006D4 RID: 1748
	public static int pxMouse;

	// Token: 0x040006D5 RID: 1749
	public static int pyMouse;

	// Token: 0x040006D6 RID: 1750
	public static Position[] arrPos = new Position[4];

	// Token: 0x040006D7 RID: 1751
	public static int gameTick;

	// Token: 0x040006D8 RID: 1752
	public static int taskTick;

	// Token: 0x040006D9 RID: 1753
	public static bool isEff1;

	// Token: 0x040006DA RID: 1754
	public static bool isEff2;

	// Token: 0x040006DB RID: 1755
	public static long timeTickEff1;

	// Token: 0x040006DC RID: 1756
	public static long timeTickEff2;

	// Token: 0x040006DD RID: 1757
	public static int w;

	// Token: 0x040006DE RID: 1758
	public static int h;

	// Token: 0x040006DF RID: 1759
	public static int hw;

	// Token: 0x040006E0 RID: 1760
	public static int hh;

	// Token: 0x040006E1 RID: 1761
	public static int wd3;

	// Token: 0x040006E2 RID: 1762
	public static int hd3;

	// Token: 0x040006E3 RID: 1763
	public static int w2d3;

	// Token: 0x040006E4 RID: 1764
	public static int h2d3;

	// Token: 0x040006E5 RID: 1765
	public static int w3d4;

	// Token: 0x040006E6 RID: 1766
	public static int h3d4;

	// Token: 0x040006E7 RID: 1767
	public static int wd6;

	// Token: 0x040006E8 RID: 1768
	public static int hd6;

	// Token: 0x040006E9 RID: 1769
	public static mScreen currentScreen;

	// Token: 0x040006EA RID: 1770
	public static Menu menu = new Menu();

	// Token: 0x040006EB RID: 1771
	public static Panel panel;

	// Token: 0x040006EC RID: 1772
	public static Panel panel2;

	// Token: 0x040006ED RID: 1773
	public static ChooseCharScr chooseCharScr;

	// Token: 0x040006EE RID: 1774
	public static LoginScr loginScr;

	// Token: 0x040006EF RID: 1775
	public static RegisterScreen registerScr;

	// Token: 0x040006F0 RID: 1776
	public static Dialog currentDialog;

	// Token: 0x040006F1 RID: 1777
	public static MsgDlg msgdlg;

	// Token: 0x040006F2 RID: 1778
	public static InputDlg inputDlg;

	// Token: 0x040006F3 RID: 1779
	public static MyVector currentPopup = new MyVector();

	// Token: 0x040006F4 RID: 1780
	public static int requestLoseCount;

	// Token: 0x040006F5 RID: 1781
	public static MyVector listPoint;

	// Token: 0x040006F6 RID: 1782
	public static Paint paintz;

	// Token: 0x040006F7 RID: 1783
	public static bool isGetResFromServer;

	// Token: 0x040006F8 RID: 1784
	public static Image[] imgBG;

	// Token: 0x040006F9 RID: 1785
	public static int skyColor;

	// Token: 0x040006FA RID: 1786
	public static int curPos = 0;

	// Token: 0x040006FB RID: 1787
	public static int[] bgW;

	// Token: 0x040006FC RID: 1788
	public static int[] bgH;

	// Token: 0x040006FD RID: 1789
	public static int planet = 0;

	// Token: 0x040006FE RID: 1790
	private mGraphics g = new mGraphics();

	// Token: 0x040006FF RID: 1791
	public static Image img12;

	// Token: 0x04000700 RID: 1792
	public static Image[] imgBlue = new Image[7];

	// Token: 0x04000701 RID: 1793
	public static Image[] imgViolet = new Image[7];

	// Token: 0x04000702 RID: 1794
	public static MyHashTable danhHieu = new MyHashTable();

	// Token: 0x04000703 RID: 1795
	public static MyVector messageServer = new MyVector(string.Empty);

	// Token: 0x04000704 RID: 1796
	public static bool isPlaySound = false;

	// Token: 0x04000705 RID: 1797
	private static int clearOldData;

	// Token: 0x04000706 RID: 1798
	public static int timeOpenKeyBoard;

	// Token: 0x04000707 RID: 1799
	public static bool isFocusPanel2;

	// Token: 0x04000708 RID: 1800
	public static int fps = 0;

	// Token: 0x04000709 RID: 1801
	public static int max;

	// Token: 0x0400070A RID: 1802
	public static int up;

	// Token: 0x0400070B RID: 1803
	public static int upmax;

	// Token: 0x0400070C RID: 1804
	private long timefps = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x0400070D RID: 1805
	private long timeup = mSystem.currentTimeMillis() + 1000L;

	// Token: 0x0400070E RID: 1806
	public static int isRequestMapID = -1;

	// Token: 0x0400070F RID: 1807
	public static long waitingTimeChangeMap;

	// Token: 0x04000710 RID: 1808
	private static int dir_ = -1;

	// Token: 0x04000711 RID: 1809
	private int tickWaitThongBao;

	// Token: 0x04000712 RID: 1810
	public bool isPaintCarret;

	// Token: 0x04000713 RID: 1811
	public static MyVector debugUpdate;

	// Token: 0x04000714 RID: 1812
	public static MyVector debugPaint;

	// Token: 0x04000715 RID: 1813
	public static MyVector debugSession;

	// Token: 0x04000716 RID: 1814
	private static bool isShowErrorForm = false;

	// Token: 0x04000717 RID: 1815
	public static bool paintBG;

	// Token: 0x04000718 RID: 1816
	public static int gsskyHeight;

	// Token: 0x04000719 RID: 1817
	public static int gsgreenField1Y;

	// Token: 0x0400071A RID: 1818
	public static int gsgreenField2Y;

	// Token: 0x0400071B RID: 1819
	public static int gshouseY;

	// Token: 0x0400071C RID: 1820
	public static int gsmountainY;

	// Token: 0x0400071D RID: 1821
	public static int bgLayer0y;

	// Token: 0x0400071E RID: 1822
	public static int bgLayer1y;

	// Token: 0x0400071F RID: 1823
	public static Image imgCloud;

	// Token: 0x04000720 RID: 1824
	public static Image imgSun;

	// Token: 0x04000721 RID: 1825
	public static Image imgSun2;

	// Token: 0x04000722 RID: 1826
	public static Image imgClear;

	// Token: 0x04000723 RID: 1827
	public static Image[] imgBorder = new Image[3];

	// Token: 0x04000724 RID: 1828
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x04000725 RID: 1829
	public static int borderConnerW;

	// Token: 0x04000726 RID: 1830
	public static int borderConnerH;

	// Token: 0x04000727 RID: 1831
	public static int borderCenterW;

	// Token: 0x04000728 RID: 1832
	public static int borderCenterH;

	// Token: 0x04000729 RID: 1833
	public static int[] cloudX;

	// Token: 0x0400072A RID: 1834
	public static int[] cloudY;

	// Token: 0x0400072B RID: 1835
	public static int sunX;

	// Token: 0x0400072C RID: 1836
	public static int sunY;

	// Token: 0x0400072D RID: 1837
	public static int sunX2;

	// Token: 0x0400072E RID: 1838
	public static int sunY2;

	// Token: 0x0400072F RID: 1839
	public static int[] layerSpeed;

	// Token: 0x04000730 RID: 1840
	public static int[] moveX;

	// Token: 0x04000731 RID: 1841
	public static int[] moveXSpeed;

	// Token: 0x04000732 RID: 1842
	public static bool isBoltEff;

	// Token: 0x04000733 RID: 1843
	public static bool boltActive;

	// Token: 0x04000734 RID: 1844
	public static int tBolt;

	// Token: 0x04000735 RID: 1845
	public static Image imgBgIOS;

	// Token: 0x04000736 RID: 1846
	public static int typeBg = -1;

	// Token: 0x04000737 RID: 1847
	public static int transY;

	// Token: 0x04000738 RID: 1848
	public static int[] yb = new int[5];

	// Token: 0x04000739 RID: 1849
	public static int[] colorTop;

	// Token: 0x0400073A RID: 1850
	public static int[] colorBotton;

	// Token: 0x0400073B RID: 1851
	public static int yb1;

	// Token: 0x0400073C RID: 1852
	public static int yb2;

	// Token: 0x0400073D RID: 1853
	public static int yb3;

	// Token: 0x0400073E RID: 1854
	public static int nBg = 0;

	// Token: 0x0400073F RID: 1855
	public static int lastBg = -1;

	// Token: 0x04000740 RID: 1856
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x04000741 RID: 1857
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x04000742 RID: 1858
	public static Image imgCaycot;

	// Token: 0x04000743 RID: 1859
	public static Image tam;

	// Token: 0x04000744 RID: 1860
	public static int typeBackGround = -1;

	// Token: 0x04000745 RID: 1861
	public static int saveIDBg = -10;

	// Token: 0x04000746 RID: 1862
	public static bool isLoadBGok;

	// Token: 0x04000747 RID: 1863
	private static long lastTimePress = 0L;

	// Token: 0x04000748 RID: 1864
	public static int keyAsciiPress;

	// Token: 0x04000749 RID: 1865
	public static int pXYScrollMouse;

	// Token: 0x0400074A RID: 1866
	private static Image imgSignal;

	// Token: 0x0400074B RID: 1867
	public static MyVector flyTexts = new MyVector();

	// Token: 0x0400074C RID: 1868
	public int longTime;

	// Token: 0x0400074D RID: 1869
	public static long timeBreakLoading;

	// Token: 0x0400074E RID: 1870
	private static string thongBaoTest;

	// Token: 0x0400074F RID: 1871
	public static int xThongBaoTranslate = GameCanvas.w - 60;

	// Token: 0x04000750 RID: 1872
	public static bool isPointerJustDown = false;

	// Token: 0x04000751 RID: 1873
	private int count = 1;

	// Token: 0x04000752 RID: 1874
	public static bool csWait;

	// Token: 0x04000753 RID: 1875
	public static MyRandom r = new MyRandom();

	// Token: 0x04000754 RID: 1876
	public static bool isBlackScreen;

	// Token: 0x04000755 RID: 1877
	public static int[] bgSpeed;

	// Token: 0x04000756 RID: 1878
	public static int cmdBarX;

	// Token: 0x04000757 RID: 1879
	public static int cmdBarY;

	// Token: 0x04000758 RID: 1880
	public static int cmdBarW;

	// Token: 0x04000759 RID: 1881
	public static int cmdBarH;

	// Token: 0x0400075A RID: 1882
	public static int cmdBarLeftW;

	// Token: 0x0400075B RID: 1883
	public static int cmdBarRightW;

	// Token: 0x0400075C RID: 1884
	public static int cmdBarCenterW;

	// Token: 0x0400075D RID: 1885
	public static int hpBarX;

	// Token: 0x0400075E RID: 1886
	public static int hpBarY;

	// Token: 0x0400075F RID: 1887
	public static int hpBarW;

	// Token: 0x04000760 RID: 1888
	public static int expBarW;

	// Token: 0x04000761 RID: 1889
	public static int lvPosX;

	// Token: 0x04000762 RID: 1890
	public static int moneyPosX;

	// Token: 0x04000763 RID: 1891
	public static int hpBarH;

	// Token: 0x04000764 RID: 1892
	public static int girlHPBarY;

	// Token: 0x04000765 RID: 1893
	public int timeOut;

	// Token: 0x04000766 RID: 1894
	public int[] dustX;

	// Token: 0x04000767 RID: 1895
	public int[] dustY;

	// Token: 0x04000768 RID: 1896
	public int[] dustState;

	// Token: 0x04000769 RID: 1897
	public static int[] wsX;

	// Token: 0x0400076A RID: 1898
	public static int[] wsY;

	// Token: 0x0400076B RID: 1899
	public static int[] wsState;

	// Token: 0x0400076C RID: 1900
	public static int[] wsF;

	// Token: 0x0400076D RID: 1901
	public static Image[] imgWS;

	// Token: 0x0400076E RID: 1902
	public static Image imgShuriken;

	// Token: 0x0400076F RID: 1903
	public static Image[][] imgDust;

	// Token: 0x04000770 RID: 1904
	public static bool isResume;

	// Token: 0x04000771 RID: 1905
	public static ServerListScreen serverScreen;

	// Token: 0x04000772 RID: 1906
	public static ServerScr serverScr;

	// Token: 0x04000773 RID: 1907
	public static SelectCharScr _SelectCharScr;

	// Token: 0x04000774 RID: 1908
	public bool resetToLoginScr;

	// Token: 0x04000775 RID: 1909
	public static long TIMEOUT;

	// Token: 0x04000776 RID: 1910
	public static int timeLoading = 15;
}
