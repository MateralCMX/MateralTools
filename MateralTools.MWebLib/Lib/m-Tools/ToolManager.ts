/// <reference path="MateralToolsEnums.ts" />
'use strict';
namespace MateralTools {
    /**
     * 普通工具类
     */
    export class ToolManager {
        /**
         * 判断对象是否为Null
         * @param obj 任意值或对象
         * @returns 是否为Null
         */
        public static IsNull(obj: any): boolean {
            return obj === null;
        }
        /**
         * 判断对象是否为Undefined
         * @param obj 任意值或对象
         * @returns 是否为Undefined
         */
        public static IsUndefined(obj: any): boolean {
            return typeof obj === "undefined";
        }
        /**
         * 判断对象是否为Null或Undefined
         * @param obj 任意值或对象
         * @returns 是否为Null或Undefined
         */
        public static IsNullOrUndefined(obj: any): boolean {
            return this.IsNull(obj) || this.IsUndefined(obj);
        }
        /**
         * 判断字符串是否为空字符串
         * @param str 字符串
         * @returns 是否为空字符串
         */
        public static IsEmpty(str: string): boolean {
            return str === "";
        }
        /**
         * 判断字符串是否为Null或Undefined或空字符串
         * @param str 字符串
         * @returns 是否为Null或Undefined或空字符串
         */
        public static IsNullOrUndefinedOrEmpty(str: string): boolean {
            return this.IsNull(str) || this.IsUndefined(str) || this.IsEmpty(str);
        }
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param IncludeCustom 包括自定义类型
         * @returns 对象类型 
         */
        public static GetType(obj: any, IncludeCustom: boolean = true): string {
            let Lowercase: boolean = true;
            let resStr: string = typeof obj;
            if (resStr === "object") {
                if (this.IsNull(obj)) {
                    resStr = "null";
                }
                else {
                    Lowercase = false;
                    resStr = Object.prototype.toString.call(obj).slice(8, -1);
                    if (resStr === "Object" && !this.IsNullOrUndefined(obj.constructor) && obj.constructor.name != "Object" && IncludeCustom) {
                        resStr = obj.constructor.name;
                    }
                }
            }
            if (!this.IsNullOrUndefinedOrEmpty(resStr) && Lowercase) {
                resStr = resStr.toLowerCase();
            }
            return resStr;
        }
        /**
         * 删除字符串两端空格
         * @param str 要删除空格的字符串
         * @returns 已删除空格的字符串
         */
        public static Trim(str: string): string {
            if (this.IsNullOrUndefined(String.prototype.trim)) {
                while (str.substr(0, 1) === " ") {
                    str = str.substr(1, str.length - 1);
                }
                while (str.substr(str.length - 2, 1) === " ") {
                    str = str.substr(0, str.length - 2);
                }
            }
            else {
                str = str.trim();
            }
            return str;
        }
        /**
         * 获得URL参数
         * @returns URL参数
         */
        public static GetURLParams(): Object {
            let params: Object = new Object();
            let paramsStr: string = window.location.search;
            let paramsStrs: string[] = new Array<string>();
            if (!this.IsNullOrUndefinedOrEmpty(paramsStr)) {
                paramsStr = paramsStr.substring(1, paramsStr.length);
                paramsStrs = paramsStr.split("&");
                for (let i = 0; i < paramsStrs.length; i++) {
                    let temp = paramsStrs[i].split("=");
                    if (temp.length == 2) {
                        params[temp[0]] = temp[1];
                    }
                    else if (temp.length == 1) {
                        params[temp[0]] = null;
                    }
                }
            }
            return params;
        }
        /**
         * 左侧填充字符
         * @param str 原字符串
         * @param length 位数
         * @param character 填充字符
         */
        public static PadLeft(str: string, length: number, character: string = " "): string {
            for (let i = str.length; i < length; i++) {
                str = character + str;
            }
            return str;
        }
        /**
         * 右侧填充字符
         * @param str 原字符串
         * @param length 位数
         * @param character 填充字符
         */
        public static PadRight(str: string, length: number, character: string = " "): string {
            for (let i = str.length; i < length; i++) {
                str = str + character;
            }
            return str;
        }
        /**
         * 获得时间差
         * @param date1 时间1
         * @param date2 时间2
         * @param TimeType 返回类型[ms毫秒s秒m分钟H小时D天数]
         */
        public static GetTimeDifference(date1: Date, date2: Date, timeType: TimeType = TimeType.Seconds): number {
            let timeDifference: number = date1.getTime() - date2.getTime();
            switch (timeType) {
                case TimeType.Day:
                    timeDifference = Math.floor(timeDifference / (24 * 3600 * 1000));
                    break;
                case TimeType.Hours:
                    timeDifference = Math.floor(timeDifference / (3600 * 1000));
                    break;
                case TimeType.Minutes:
                    timeDifference = Math.floor(timeDifference / (60 * 1000));
                    break;
                case TimeType.Seconds:
                    timeDifference = Math.floor(timeDifference / 1000);
                    break;
                case TimeType.Milliseconds:
                    timeDifference = timeDifference;
                    break;
                default:
            }
            return timeDifference;
        }
        /**
         * 时间字符串格式化
         * @param dateTime 时间对象
         * @param formatStr 格式化字符串
         */
        public static DateTimeFormat(dateTime: Date, formatStr: string): string {
            let formatData: Object = {
                "M+": dateTime.getMonth() + 1, //月份 
                "d+": dateTime.getDate(), //日 
                "H+": dateTime.getHours(), //小时 
                "m+": dateTime.getMinutes(), //分 
                "s+": dateTime.getSeconds(), //秒 
                "q+": Math.floor((dateTime.getMonth() + 3) / 3), //季度 
                "S": dateTime.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(formatStr)) {
                formatStr = formatStr.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var data in formatData) {
                if (new RegExp("(" + data + ")").test(formatStr)) {
                    formatStr = formatStr.replace(RegExp.$1, (RegExp.$1.length == 1) ? (formatData[data]) : (("00" + formatData[data]).substr(("" + formatData[data]).length)));
                }
            }
            return formatStr;
        }
        /**
         * 获取Input dateTime设置值字符串
         * @param dateTime 要设置的时间
         */
        public static GetInputDateTimeValueStr(dateTime: Date): string {
            return ToolManager.DateTimeFormat(dateTime, "yyyy-MM-ddTHH:mm:ss");
        }
    }
}