using System;
using System.Collections;
using System.Data;
using System.IO;

namespace Horse_Jump
{
    public class Horse
    {
        public static int Direction = 8;   //马跳的8个方向
        public static int num = 0;         //可行方案的计数
        public static int Max_Step = 50;   //记录跳马步数的限制
        public static int IsBorder = 0;    //记录到达边界的次数

        public static int[,] path = new int[Max_Step + 1, 2];   //创建path数组来记录马跳的每一步的坐标

        //创建两个数组dx和dy，通过依次访问数组的相同下标来遍历马的跳跃操作
        public static int[] dx = new int[] { 0, 1, 2, 1, 2, -1, -2, -1, -2 };
        public static int[] dy = new int[] { 0, 2, 1, -2, -1, 2, 1, -2, -1 };

        //程序的主函数
        public static void Main()
        {
            //初始化棋盘,大小为5*5
            int Max_X = 4;  
            int Max_Y = 4;

            //随机生成马的初始点位置
            int X = new Random().Next(0, 5);
            int Y = new Random().Next(0, 5);

            //将马的初始点放在path数组的第一位
            path[0, 0] = X;
            path[0, 1] = Y;

            //创建棋盘的二维数组
            int[,] XY_Arr = new int[Max_X+1, Max_Y+1];

            //初始化二维数组
            for (int i = 0; i < Max_X + 1; i++)
            {
                for (int j = 0; j < Max_Y + 1; j++)
                {
                    XY_Arr[i,j] = 0;
                }
            }

            //若生成的随机初始点即在边界上，IsBorder减一
            if ((path[0, 0] == Max_X) || (path[0, 1] == Max_Y) || (path[0, 0] == 0) || (path[0, 1] == 0))
                IsBorder--;

            //递归函数求取可行方案
            Jump(Max_X, Max_Y, X, Y, 1, XY_Arr);

            Console.WriteLine("总方案数：" + num);
        }

        //递归函数的主体
        public static void Jump(int Max_X, int Max_Y, int X, int Y, int step, int[,] XY_Arr)
        {
            bool t1, t2, t3, t4;   //四个判定条件
            int x1, y1;  //记录马的当前位置

            Path(Max_X, Max_Y, step-1, XY_Arr); //为当前步数初始化二维数组，并将已经走过的位置标记为1

            for (int k = 1; k <= Direction; k++)
            {
                x1 = X + dx[k];
                y1 = Y + dy[k];

                if (x1 > Max_X || y1 > Max_Y || x1 < 0 || y1 < 0)
                    continue;

                t1 = ((x1 >= 0) && (x1 <= Max_X));  //判断位置是否在左右边界内
                t2 = ((y1 >= 0) && (y1 <= Max_Y));  //判断位置是否在上下边界内

                //判断是否返回初始点
                t3 = (x1 == path[0, 0]);  
                t4 = (y1 == path[0, 1]);


                //判断点是否到达了边界
                if ((x1 == Max_X) || (y1 == Max_Y) || (x1 == 0) || (y1 == 0))
                    IsBorder++;

                if (t1 && t2)
                {
                    if(Judgement(x1,y1,XY_Arr))
                    {
                        //将路线写入数组中
                        path[step,0] = x1;
                        path[step,1] = y1;

                        if (t3 && t4 && IsBorder>0)
                        {
                            //将路线打印输出
                            num++;
                            Console.WriteLine("方案" + num + ":");

                            for (int i = 0; i <= step; i++)
                            {
                                Console.Write("(" + path[i, 0] + "," + path[i, 1] + ")");
                                if (i != step)
                                    Console.Write("->");
                            }
                            Console.WriteLine("\n");
                            IsBorder = 0;
                        }
                        else
                        {
                            //下一层递归
                            Jump(Max_X, Max_Y, x1, y1, step + 1, XY_Arr);
                        }
                    }
                }
            }
        }

        //将走过的点标记为1
        public static void Path(int Max_X, int Max_Y, int step, int[,] XY_Arr)
        {
            //初始化二维数组
            for (int i = 0; i < Max_X + 1; i++)
            {
                for (int j = 0; j < Max_Y + 1; j++)
                {
                    XY_Arr[i, j] = 0;
                }
            }
            //根据当前的步数将已经走过的点标记为1
            for (int i = 1; i < step; i++)
            {
                XY_Arr[path[i,0],path[i,1]] = 1;
            }
        }

        //判断点是否走过
        public static bool Judgement(int x1, int y1, int[,] XY_Arr)
        {
            bool judge = true;
            if (XY_Arr[x1, y1] != 0)
                judge = false;
            else
                XY_Arr[x1, y1] = 1;
            return judge;
        }
    }
}
