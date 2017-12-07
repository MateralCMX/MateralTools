/// <reference path="JsonManager.ts" />
/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * Http配置类
     */
    export class HttpConfigModel {
        //URL地址
        public url: string;
        //要发送的数据
        public data: Object;
        //成功方法
        public success: Function;
        //失败方法
        public error: Function;
        //成功错误都执行的方法
        public complete: Function;
        //类型
        public type: string;
        //超时时间
        public timeout: number = 15000;
        //异步发送
        public async: boolean = true;
        //数据类型
        public dataType: string;
        /**
         * 
         * @param url
         * @param type
         * @param data
         * @param dataType
         * @param success
         * @param error
         * @param complete
         */
        constructor(url: string, type: string = "post", data: Object = null, dataType: string = "json", success: Function = null, error: Function = null, complete: Function = null) {
            this.url = url;
            this.type = type;
            this.data = data;
            this.dataType = dataType;
            this.success = success;
            this.error = error;
            this.complete = complete;
        }
    }
    /**
     * Http帮助类
     */
    export class HttpManager {
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        private static GetHttpRequest(config: HttpConfigModel): XMLHttpRequest {
            let xhr: XMLHttpRequest;
            if (!ToolManager.IsNullOrUndefined(window["XMLHttpRequest"])) {
                xhr = new XMLHttpRequest();
            }
            else {
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhr.onreadystatechange = function () {
                HttpManager.Readystatechange(xhr, config);
            }
            return xhr;
        }
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        private static Readystatechange(xhr: XMLHttpRequest, config: HttpConfigModel): void {
            if (xhr.readyState == 4) {
                let res: Object;
                try {
                    res = JsonManager.JSONParse(xhr.responseText);
                }
                catch (ex) {
                    res = xhr.responseText;
                }
                if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
                    if (config.complete) {
                        config.complete(xhr, res);
                    }
                    if (config.success) {
                        config.success(res);
                    }
                }
                else {
                    if (config.complete) {
                        config.complete(xhr, res);
                    }
                    if (config.error) {
                        config.error(xhr, xhr.status, res);
                    }
                }
            }
        }
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串 
         */
        private static serialize(data: Object): string {
            let result: string[] = new Array<string>();
            let value: string = "";
            for (let name in data) {
                if (typeof data[name] === "function") {
                    continue;
                }
                if (ToolManager.GetType(data[name]) == "Object") {
                    result.push(this.serialize(data[name]));
                }
                else {
                    name = encodeURIComponent(name);
                    value = data[name].toString();
                    value = encodeURIComponent(value);
                    result.push(name + "=" + value);
                }
            };
            return result.join("&");
        }
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        private static SendPost(config: HttpConfigModel): void {
            let xhr: XMLHttpRequest = this.GetHttpRequest(config);
            xhr.open(config.type, config.url, config.async);
            switch (config.dataType) {
                case "json":
                    xhr.setRequestHeader("Content-type", "application/json");
                    break;
                case "data":
                    break;
                default:
                    break;
            }
            if (config.data) {
                switch (config.dataType) {
                    case "json":
                        xhr.send(JSON.stringify(config.data));
                        break;
                    case "data":
                        xhr.send(config.data);
                        break;
                    default:
                        break;
                }
            }
            else {
                xhr.send(null);
            }
        }
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        private static SendGet(config: HttpConfigModel): void {
            let xhr: XMLHttpRequest = HttpManager.GetHttpRequest(config);
            config.type = config.type.toLowerCase();
            let url: string = config.url;
            if (config.data) {
                url += "?" + HttpManager.serialize(config.data);
            }
            xhr.open(config.type, url, config.async);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(null);
        }
        /**
         * 发送请求
         * @param config 配置对象
         */
        public static Send(config: HttpConfigModel): void {
            config.type = config.type.toLowerCase();
            if (config.type == "post") {
                HttpManager.SendPost(config);
            }
            else {
                HttpManager.SendGet(config);
            }
        }
    }
}