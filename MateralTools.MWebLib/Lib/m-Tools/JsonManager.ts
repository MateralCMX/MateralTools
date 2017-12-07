/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     *JSON帮助类
     */
    export class JsonManager {
        /**
         * Json字符串转换为Json对象
         * @param jsonStr Json字符串
         * @returns Json对象
         */
        public static JSONParse(jsonStr: string): Object {
            let resM: Object;
            if (!ToolManager.IsNullOrUndefined(JSON.parse)) {
                resM = JSON.parse(jsonStr)
            }
            else {
                resM = eval("(" + jsonStr + ")");
            }
            return resM;
        }
        /**
         * Json对象转换为Json字符串
         * @param jsonObj Json对象
         * @returns Json字符串
         */
        public static JSONStringify(jsonObj: Object): string {
            let resM: string;
            if (!ToolManager.IsNullOrUndefined(JSON.stringify)) {
                resM = JSON.stringify(jsonObj)
            }
            else {
                let IsArray: boolean;
                let TypeStr: string;
                for (let key in jsonObj) {
                    IsArray = false;
                    TypeStr = ToolManager.GetType(jsonObj[key]);
                    if (jsonObj instanceof Array) {
                        IsArray = true;
                    }
                    if (TypeStr == "string") {
                        if (IsArray) {
                            resM += "\"" + jsonObj[key].toString() + "\",";
                        }
                        else {
                            resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                        }
                    }
                    else if (jsonObj[key] instanceof RegExp) {
                        if (IsArray) {
                            resM += jsonObj[key].toString() + ",";
                        }
                        else {
                            resM += "\"" + key + "\":\"" + jsonObj[key].toString() + "\",";
                        }
                    }
                    else if (jsonObj[key] instanceof Array) {
                        resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
                    }
                    else if (TypeStr == "boolean") {
                        if (IsArray) {
                            resM += jsonObj[key].toString() + ",";
                        }
                        else {
                            resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                        }
                    }
                    else if (TypeStr == "number") {
                        if (IsArray) {
                            resM += jsonObj[key].toString() + ",";
                        }
                        else {
                            resM += "\"" + key + "\":" + jsonObj[key].toString() + ",";
                        }
                    }
                    else if (jsonObj[key] instanceof Object) {
                        if (IsArray) {
                            resM += this.JSONStringify(jsonObj[key]) + ",";
                        }
                        else {
                            resM += "\"" + key + "\":" + this.JSONStringify(jsonObj[key]) + ",";
                        }
                    }
                    else if (!jsonObj[key] || jsonObj[key] instanceof Function) {
                        if (IsArray) {
                            resM += "null,";
                        }
                        else {
                            resM += "\"" + key + "\":null,";
                        }
                    }
                }
                if (IsArray) {
                    resM = "[" + resM.slice(0, -1) + "]";
                }
                else {
                    resM = "{" + resM.slice(0, -1) + "}";
                }
            }
            return resM;
        }
    }
}