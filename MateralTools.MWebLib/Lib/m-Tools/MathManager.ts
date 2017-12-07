'use strict';
namespace MateralTools {
    /**
     * 数学帮助类
     */
    export class MathManager {
        /**
         * 返回一个随机数
         * @param min 最小值
         * @param max 最大值
         * @returns 随机数
         */
        public GetRandom(min: number, max: number): number {
            return Math.floor(Math.random() * max + min);
        }
        /**
         * 获取四边形的外接圆半径
         * @param length 长
         * @param width 宽
         * @param IsRound 是圆形
         */
        public GetCircumcircleRadius(length: number, width: number = length, IsRound: boolean = true): number {
            let max: number = Math.max(length, width);
            //正方形的对角线=边长^2*2
            let diameter: number = Math.sqrt(Math.pow(max, 2) * 2);
            //外接圆的直径=正方形的对角线
            //圆的半径=直径/2
            let radius: number = diameter / 2;
            if (IsRound) {
                radius = Math.round(radius);
            }
            return radius;
        }
    }
}