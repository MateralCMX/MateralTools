/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * DOM帮助类
     */
    export class DOMManager {
        /**
         * 根据页面元素对象获得页面元素对象
         * @param element 页面元素
         * @returns 页面元素对象
         */
        public static $(element: string | HTMLElement | Element): HTMLElement {
            if (ToolManager.GetType(element) === "string") {
                element = document.getElementById(element as string);
            }
            else {
                element = element;
            }
            return element as HTMLElement;
        }
        /**
         * 设置样式
         * @param element 页面元素
         * @param className 要设置的样式列表
         */
        public static SetClass(element: string | HTMLElement | Element, className: string | string[]): void {
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                let classStr: string = "";
                let TypeStr: string = ToolManager.GetType(className);
                let ClassList: string[];
                if (TypeStr === "string") {
                    classStr = (className as string).replace(/\s{2,}/g, " ");
                    classStr = ToolManager.Trim(classStr);
                }
                else if (TypeStr === "Array") {
                    classStr = (className as string[]).join(" ");
                }
                if (!ToolManager.IsNullOrUndefinedOrEmpty(classStr)) {
                    element.setAttribute("class", classStr);
                }
                else {
                    element.removeAttribute("class");
                }
            }
        }
        /**
         * 添加样式
         * @param id 页面元素ID
         * @param className 要添加的样式
         */
        public static AddClass(element: string | HTMLElement | Element, className: string | string[]): void {
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                element.classList.add(className[i]);
            }
        }
        /**
         * 删除样式
         * @param element 页面元素
         * @param className 要删除的样式列表
         */
        public static RemoveClass(element: string | HTMLElement | Element, className: string | string[]): void {
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                element.classList.remove(className[i]);
            }
        }
        /**
         * 是否有拥有样式
         * @param element 页面元素
         * @param className 要查找的样式列表
         * @returns 查询结果
         */
        public static HasClass(element: string | HTMLElement | Element, className: string | string[]): boolean {
            let resM = true;
            element = this.$(element);
            if (ToolManager.GetType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (var i = 0; i < className.length && resM; i++) {
                resM = element.classList.contains(className[i]);
            }
            return resM;
        }
        /**
         * 根据ClassName获得元素对象
         * @param element 父元素
         * @param className ClassName
         * @returns Element集合
         */
        public static GetElementsByClassName(element: string | HTMLElement | Element, className: string): Array<Element> | NodeListOf<Element> {
            element = this.$(element);
            let resultM: Array<Element> = new Array<Element>();
            let elements: NodeListOf<Element>;
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element["getElementsByClassName"])) {
                    elements = element["getElementsByClassName"](className);
                    return elements;
                }
                else {
                    elements = element.getElementsByTagName("*");
                    for (let i = 0; i < elements.length; i++) {
                        if (this.HasClass(elements[i], className)) {
                            resultM.push(elements[i]);
                        }
                    }
                    return resultM;
                }
            }
            return null;
        }
        /**
         * 获得事件触发元素
         * @param event 事件对象
         * @returns 触发元素 
         */
        public static GetEventTarget(event: Event): Element | EventTarget {
            return event["srcElement"] || event["target"]
        }
        /**
         * 添加事件
         * @param thisWindow window对象
         * @param type 事件类型
         * @param fun 执行方法
         */
        public static AddEvent(element: string | HTMLElement | Window, type: string, fun: Function): void {
            let typeName = ToolManager.GetType(element);
            if (ToolManager.GetType(element) === "string" || ToolManager.GetType(element) === "HTMLElement") {
                element = this.$(element as string | HTMLElement);
            }
            if (!ToolManager.IsNullOrUndefined(element["addEventListener"])) {
                element["addEventListener"](type, fun);
            }
            else {
                element["attachEvent"]("on" + type, fun);
            }
        }
        /**
         * 获得子节点
         * @param element 父元素
         * @returns 子节点
         */
        public static GetChildren(element: string | HTMLElement): HTMLCollection | Array<Node> {
            let children: HTMLCollection | Array<Node>;
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (element["children"]) {
                    children = element["children"];
                }
                else {
                    children = new Array<Node>();
                    let length = element.childNodes.length;
                    for (let i = 0; i < length; i++) {
                        if (element.childNodes[i].nodeType == 1) {
                            (children as Array<Node>).push(element.childNodes[i]);
                        }
                    }
                }
            }
            return children;
        }
        /**
         * 获得自定义属性
         * @param element 父节点
         * @returns 自定义属性
         */
        public static GetDataSet(element: string | HTMLElement): DOMStringMap | Object {
            let DataSet: DOMStringMap | Object;
            element = this.$(element);
            if (!ToolManager.IsNullOrUndefined(element)) {
                if (!ToolManager.IsNullOrUndefined(element.dataset)) {
                    DataSet = element.dataset;
                }
                else {
                    DataSet = new Object();
                    let length: number = element.attributes.length;
                    let item: any;
                    for (let i = 0; i < length; i++) {
                        item = element.attributes[i];
                        if (!ToolManager.IsNullOrUndefined(item.specified) && /^data-/.test(item.nodeName)) {
                            DataSet[item.nodeName.substring(5)] = item.nodeValue;
                        }
                    }
                }
                return DataSet;
            }
        }
        /**
         * 获得元素的实际样式
         * @param element 元素
         * @returns 实际样式
         */
        public static GetComputedStyle(element: string | HTMLElement): CSSStyleDeclaration {
            element = this.$(element);
            let cssStyle: CSSStyleDeclaration;
            if (element["currentStyle"]) {
                cssStyle = element["currentStyle"];
            }
            else {
                cssStyle = getComputedStyle(element);
            }
            return cssStyle;
        }
        /**
         * 设置<input type="date|time|datetime|datetime-local">的值
         * @param element 要设置的对象
         * @param dateTime 要设置的时间
         */
        public static SetInputDateTimeValue(element: string | HTMLInputElement, dateTime: Date): void {
            element = this.$(element) as HTMLInputElement;
            if (!ToolManager.IsNullOrUndefined(element)) {
                let elementType = element.getAttribute("type");
                if (elementType === "date" || elementType === "time" || elementType === "datetime" || elementType === "datetime-local") {
                    element.value = ToolManager.GetInputDateTimeValueStr(dateTime);
                }
            }
        }
    }
}