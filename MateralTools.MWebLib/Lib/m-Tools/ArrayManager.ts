/// <reference path="ToolManager.ts" />
'use strict';
namespace MateralTools {
    /**
     * 数组帮助类
     */
    export class ArrayManager {
        /**
         * 查询所在数组的位序
         * @param array 要查询的数组
         * @param item 要查询的对象
         * @returns 位序
         */
        public static ArrayIndexOf<T>(array: Array<T>, item: T, formIndex: number = 0): number {
            let index: number = -1;
            if (ToolManager.IsNullOrUndefined(array.indexOf)) {
                for (let i = formIndex; i < array.length; i++) {
                    if (array[i] == item) {
                        index = i;
                    }
                }
            }
            else {
                index = array.indexOf(item, formIndex);
            }
            return index;
        }
        /**
         * 清空数组
         * @param array 要清空的数组
         * @returns 清空后的数组
         */
        public static ArrayClear<T>(array: Array<T>): Array<T> {
            array.splice(0, array.length);
            return array;
        }
        /**
         * 插入数组
         * @param array 要插入的数组
         * @param index 要插入的对象
         * @returns 插入后的数组
         */
        public static ArrayInsert<T>(array: Array<T>, item: T, index: number): Array<T> {
            array.splice(index, 0, item);
            return array;
        }
        /**
         * 删除数组
         * @param array 要删除的数组
         * @param index 要删除的位序
         * @returns 删除后的数组
         */
        public static ArrayRemoveTo<T>(array: Array<T>, index: number): Array<T> {
            let count = array.length;
            array.splice(index, 1);
            if (count === array.length && count === 1) {
                array = [];
            }
            return array;
        }
        /**
         * 删除数组
         * @param array 要删除的数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        public static ArrayRemove<T>(array: Array<T>, item: T): Array<T> {
            let index: number = this.ArrayIndexOf(array, item);
            if (index >= 0) {
                this.ArrayRemoveTo(array, index);
            }
            return array;
        }
        /**
         * 删除所有数组
         * @param array 要删除的数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        public static ArrayRomeveAll<T>(array: Array<T>, item: T): Array<T> {
            let index: number = this.ArrayIndexOf(array, item);
            while (index >= 0) {
                this.ArrayRemoveTo(array, index);
                index = this.ArrayIndexOf(array, item);
            }
            return array;
        }
    }
}