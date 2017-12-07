/// <reference path="ObjectManager.ts" />
/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * 实现引擎模型
     */
    export class EngineInfoModel {
        //是否为Trident引擎
        public Trident: boolean = false;
        //是否为Gecko引擎
        public Gecko: boolean = false;
        //是否为WebKit引擎
        public WebKit: boolean = false;
        //是否为KHTML引擎
        public KHTML: boolean = false;
        //是否为Presto引擎
        public Presto: boolean = false;
        //具体版本号
        public Version: string = "";
    }
    /**
     * 浏览器模型
     */
    export class BrowserInfoModel {
        //是否为IE浏览器
        public IE: boolean = false;
        //是否为Firefox浏览器
        public Firefox: boolean = false;
        //是否为Safari浏览器
        public Safari: boolean = false;
        //是否为Konqueror浏览器
        public Konqueror: boolean = false;
        //是否为Opera浏览器
        public Opera: boolean = false;
        //是否为Chrome浏览器
        public Chrome: boolean = false;
        //是否为Edge浏览器
        public Edge: boolean = false;
        //是否为QQ浏览器
        public QQ: boolean = false;
        //是否为UC浏览器
        public UC: boolean = false;
        //是否为Maxthon(遨游)浏览器
        public Maxthon: boolean = false;
        //是否为微信浏览器
        public WeChat: boolean = false;
        //具体版本号
        public Version: string = "";
    }
    /**
     * 系统模型
     */
    export class SystemInfoModel {
        //是否为Windows操作系统
        public Windows: boolean = false;
        //是否为WindowsMobile操作系统
        public WindowsMobile: boolean = false;
        //Windows版本
        public WindowsVersion: string = "";
        //是否为Mac操作系统
        public Mac: boolean = false;
        //是否为Unix操作系统
        public Unix: boolean = false;
        //是否为Linux操作系统
        public Linux: boolean = false;
        //是否为iPhone操作系统
        public iPhone: boolean = false;
        //是否为iPod操作系统
        public iPod: boolean = false;
        //是否为Windows操作系统
        public iPad: boolean = false;
        //是否为Windows操作系统
        public IOS: boolean = false;
        //IOS版本
        public IOSVersion: string = "";
        //是否为Android操作系统
        public Android: boolean = false;
        //Android版本
        public AndroidVersion: string = "";
        //是否为NokiaN操作系统
        public NokiaN: boolean = false;
        //是否为Wii操作系统
        public Wii: boolean = false;
        //是否为PS操作系统
        public PS: boolean = false;
    }
    /**
     * 客户端信息模型
     */
    export class ClientInfoModel {
        private _engineM: EngineInfoModel = new EngineInfoModel();
        private _browserM: BrowserInfoModel = new BrowserInfoModel();
        private _systemM: SystemInfoModel = new SystemInfoModel();
        /**
         * 实现引擎信息
         */
        public get EngineInfoM(): EngineInfoModel {
            return (ObjectManager.Clone(this._engineM) as EngineInfoModel);
        }
        /**
         * 浏览器信息
         */
        public get BrowserInfoM(): BrowserInfoModel {
            return (ObjectManager.Clone(this._browserM) as BrowserInfoModel);
        }
        /**
         * 系统信息
         */
        public get SystemInfoM(): SystemInfoModel {
            return (ObjectManager.Clone(this._systemM) as SystemInfoModel);
        }
        /**
         * 客户端信息模型
         */
        constructor() {
            //检测呈现引擎和浏览器
            let userAgent: string = navigator.userAgent;
            if (!ToolManager.IsNullOrUndefined(window["opera"])) {
                this._engineM.Version = this._engineM.Version = window["opera"].version();
                this._engineM.Presto = this._browserM.Opera = true;
            }
            else if (/AppleWebKit\/(\S+)/.test(userAgent)) {
                this._engineM.Version = RegExp["$1"];
                this._engineM.WebKit = true;
                if (/MicroMessenger\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.WeChat = true;
                }
                else if (/Edge\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Edge = true;
                }
                else if (/QQBrowser\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.QQ = true;
                }
                else if (/UBrowser\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.UC = true;
                }
                else if (/Maxthon\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Maxthon = true;
                }
                else if (/Chrome\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Chrome = true;
                }
                else if (/Safari\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Safari = true;
                }
                else {
                    if (this._engineM.WebKit) {
                        let safariVersion: string = "";
                        let WebKitVersion: number = parseInt(this._engineM.Version);
                        if (WebKitVersion < 100) {
                            safariVersion = "1";
                        }
                        else if (WebKitVersion < 312) {
                            safariVersion = "1.2";
                        }
                        else if (WebKitVersion < 412) {
                            safariVersion = "1.3";
                        }
                        else {
                            safariVersion = "2";
                        }
                        this._browserM.Version = safariVersion;
                        this._browserM.Safari = true;
                    }
                }
            }
            else if (/KHTML\/(\S+)/.test(userAgent) || /Konqueror\/([^;]+)/.test(userAgent)) {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.KHTML = this._browserM.Konqueror = true;
            }
            else if (/rv:([^\)]+)\) Gecko\/\d{8}/.test(userAgent)) {
                this._engineM.Version = RegExp["$1"];
                this._engineM.Gecko = true;
                if (/Firefox\/(\S+)/.test(userAgent)) {
                    this._browserM.Version = RegExp["$1"];
                    this._browserM.Firefox = true;
                }
            }
            else if (/MSIE ([^;]+)/.test(userAgent)) {
                this._engineM.Version = this._browserM.Version = RegExp["$1"];
                this._engineM.Trident = this._browserM.IE = true;
            }
            else {
                if (!ToolManager.IsNullOrUndefined(window["ActiveXObject"]) || "ActiveXObject" in window) {
                    this._engineM.Version = this._browserM.Version = "11";
                    this._engineM.Trident = this._browserM.IE = true
                }
            }
            //检测平台
            var p = navigator.platform;
            this._systemM.Windows = p.indexOf("Win") == 0;
            if (this._systemM.Windows) {
                if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(userAgent)) {
                    if (RegExp["$1"] == "NT") {
                        switch (RegExp["$2"]) {
                            case "5.0":
                                this._systemM.WindowsVersion = "2000";
                                break;
                            case "5.1":
                                this._systemM.WindowsVersion = "XP";
                                break;
                            case "6.0":
                                this._systemM.WindowsVersion = "Vista";
                                break;
                            case "6.1":
                                this._systemM.WindowsVersion = "7";
                                break;
                            case "6.2":
                                this._systemM.WindowsVersion = "8";
                                break;
                            case "10.0":
                                this._systemM.WindowsVersion = "10";
                                break;
                            default:
                                this._systemM.WindowsVersion = "NT";
                                break;
                        }
                    }
                    else if (RegExp["$1"] == "9X") {
                        this._systemM.WindowsVersion = "ME";
                    }
                    else {
                        this._systemM.WindowsVersion = RegExp["$1"];
                    }
                }
                if (this._systemM.WindowsVersion == "CE") {
                    this._systemM.WindowsMobile = true;
                }
                else if (this._systemM.WindowsVersion == "Ph") {
                    if (/Windows Phone OS (\d+.\d+)/.test(userAgent)) {
                        this._systemM.WindowsMobile = true;
                    }
                }
            }
            this._systemM.Mac = p.indexOf("Mac") == 0;
            if (this._systemM.Mac && userAgent.indexOf("Mobile") > -1) {
                if (/CPU (?:iPhone)?OS (\d+_\d+)/.test(userAgent)) {
                    this._systemM.IOS = true;
                    this._systemM.IOSVersion = RegExp["$1"].replace("_", ".");
                }
                else {
                    this._systemM.IOS = true;
                    this._systemM.IOSVersion = "2";
                }
            }
            this._systemM.Unix = p.indexOf("X11") == 0;
            this._systemM.Linux = p.indexOf("Linux") == 0;
            this._systemM.iPhone = p.indexOf("iPhone") == 0;
            this._systemM.iPod = p.indexOf("iPod") == 0;
            this._systemM.iPad = p.indexOf("iPad") == 0;
            this._systemM.NokiaN = userAgent.indexOf("NokiaN") > -1;
            this._systemM.Wii = userAgent.indexOf("Wii") > -1;
            this._systemM.PS = /playstation/i.test(userAgent);
            if (/Android (\d+\.\d+)/.test(userAgent)) {
                this._systemM.Android = true;
                this._systemM.AndroidVersion = RegExp["$1"];
            }
        }
    }
}