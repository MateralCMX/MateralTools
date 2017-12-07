/// <reference path="MateralToolsEnums.ts" />
/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * 本地存储帮助类
     */
    export class LocalDataManager {
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        public static IsLocalStorage(): boolean {
            if (window.localStorage) {
                return true;
            }
            else {
                return false;
            }
        }
        /**
         * 清空本地存储对象
         */
        public static CleanLocalData(): void {
            if (this.IsLocalStorage() == true) {
                window.localStorage.clear();
            }
        }
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        public static RemoveLocalData(key: string): void {
            if (this.IsLocalStorage() == true && key) {
                window.localStorage.removeItem(key);
            }
        }
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        public static SetLocalData(key: string, value: string | string[] | Object, isJson: boolean = true): void {
            if (this.IsLocalStorage() && key && value) {
                this.RemoveLocalData(key);
                if (isJson) {
                    window.localStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.localStorage.setItem(key, value.toString());
                }
            }
        }
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        public static GetLocalData(key: string, isJson: boolean = true): Object | string {
            if (this.IsLocalStorage() == true && key) {
                if (isJson) {
                    return JSON.parse(window.localStorage.getItem(key));
                }
                else {
                    return window.localStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 是否支持网页存储
         * @returns 是否支持 
         */
        public static IsSessionStorage(): boolean {
            if (window.sessionStorage) {
                return true;
            }
            else {
                return false;
            }
        }
        /**
         * 清空网页存储对象
         */
        public static CleanSessionData(): void {
            if (this.IsSessionStorage() == true) {
                window.sessionStorage.clear();
            }
        }
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        public static RemoveSessionData(key: string) {
            if (this.IsSessionStorage() == true && key) {
                window.sessionStorage.removeItem(key);
            }
        }
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        public static SetSessionData(key: string, value: string | string[] | Object, isJson: boolean = true) {
            if (!isJson && isJson != false) {
                isJson = true;
            }
            if (this.IsSessionStorage() && key && value) {
                this.RemoveSessionData(key);
                if (isJson) {
                    window.sessionStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.sessionStorage.setItem(key, value.toString());
                }
            }
        }
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        public static GetSessionData(key: string, isJson: boolean = true): Object | string {
            if (this.IsSessionStorage() == true && key) {
                if (isJson) {
                    return JSON.parse(window.sessionStorage.getItem(key));
                }
                else {
                    return window.sessionStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        private static Gettime(timeValue: number = 10000, timeType: TimeType = TimeType.Minutes): number {
            switch (timeType) {
                case TimeType.Years:
                    timeValue = 60 * 60 * 24 * 365 * timeValue * 1000;
                    break;
                case TimeType.Months:
                    timeValue = 60 * 60 * 24 * 30 * timeValue * 1000;
                    break;
                case TimeType.Day:
                    timeValue = 60 * 60 * 24 * timeValue * 1000;
                    break;
                case TimeType.Hours:
                    timeValue = 60 * 60 * timeValue * 1000;
                    break;
                case TimeType.Minutes:
                    timeValue = 60 * timeValue * 1000;
                    break;
                case TimeType.Seconds:
                    timeValue = timeValue * 1000;
                    break;
                case TimeType.Milliseconds:
                    timeValue = timeValue;
                    break;
            }
            return timeValue;
        }
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        public static SetCookie(key: string, value: string | string[] | Object, isJson: boolean = true, timeValue: number = 60, timeType: TimeType = TimeType.Minutes) {
            if (isJson) {
                document.cookie = key + "=" + JSON.stringify(value) + ";max-age=" + this.Gettime(timeValue, timeType);
            }
            else {
                document.cookie = key + "=" + value + ";max-age=" + this.Gettime(timeValue, timeType);
            }
        }
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        public static RemoveCookie(key: string) {
            document.cookie = key + "=;max-age=0";
        }
        /**
         * 获得所有Cookie
         * @returns Cookie对象 
         */
        public static GetAllCookie(): Object {
            let cookies: string[] = document.cookie.split(";");
            let cookie: Array<string[]> = new Array();
            let LocalCookie = new Object();
            for (var i = 0; i < cookies.length; i++) {
                if (!ToolManager.IsNullOrUndefinedOrEmpty(cookies[i])) {
                    cookie[i] = cookies[i].trim().split("=");
                    if (cookie[i][0] && cookie[i][1]) {
                        LocalCookie[cookie[i][0]] = cookie[i][1];
                    }
                }
            }
            return LocalCookie;
        }
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        public static GetCookie(key: string, isJson: boolean): Object {
            if (!isJson && isJson != false) {
                isJson = true;
            }
            let resM: Object = this.GetAllCookie();
            if (isJson && !ToolManager.IsNullOrUndefined(resM) && !ToolManager.IsNullOrUndefined(resM[key])) {
                return JSON.parse(resM[key]);
            }
            else {
                return null;
            }
        }
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        public static SetData(key: string, value: string | string[] | Object, isJson: boolean = true, time: number = 60, timeType: TimeType = TimeType.Minutes): void {
            if (this.IsLocalStorage()) {
                this.SetLocalData(key, value, isJson);
            }
            else {
                this.SetCookie(key, value, isJson, time, timeType);
            }
        }
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns [0]是localStorage [1]是Cookie
         */
        public static GetData(key: string, isJson: boolean = true): Array<Object> | Array<string> {
            let resM = [];
            resM.push(this.GetLocalData(key, isJson));
            resM.push(this.GetCookie(key, isJson));
            return resM;
        }
    }
}