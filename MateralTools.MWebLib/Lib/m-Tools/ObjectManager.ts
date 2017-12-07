/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * 对象帮助类
     */
    export class ObjectManager {
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        public static Clone(obj: any): any {
            let ObjectType: string = ToolManager.GetType(obj, false);
            let result: any;
            if (ObjectType == "object") {
                result = new Object();
            }
            else if (ObjectType == "array") {
                result = new Array();
            }
            else {
                result = obj;
            }
            for (var i in obj) {
                let copy = obj[i];
                let SubObjectType: string = ToolManager.GetType(copy, false);
                if (SubObjectType == "object" || SubObjectType == "array") {
                    result[i] = arguments.callee(copy);
                }
                else {
                    result[i] = copy;
                }
            }
            return result;
        }
    }
}