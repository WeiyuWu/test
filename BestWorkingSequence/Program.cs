using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace BestWorkingSequence
{
    public class Sequence
    {
        //Job类  （嵌套类）
        public class Job
        {
            public char id;        //工作的代号
            public int deadline;   //工作的截止日期
            public int profit;     //工作的利润
            public int time;       //工作所需要花费的时间
            
            //Job的构造函数
            public Job(char id,int deadline,int profit,int time)
            {
                this.id = id;
                this.deadline = deadline;
                this.profit = profit;
                this.time = time;
            }
        }

        //一个比较的迭代器，继承自IComparer接口，实现数组的递减排序  （嵌套类）
        public class JobDescent : IComparer
        {
            public int Compare(object x, object y)
            {
                //递减排序的依据是工作利润除以工作时间
                return (((Job)y).profit/((Job)y).time).CompareTo((((Job)x).profit)/((Job)y).time);
            }
        }

        //安排最佳工作序列的主体函数
        public static void JobScheduling(ArrayList job_list)
        {
            //对所给的工作序列数组进行递减排序
            JobDescent sd = new JobDescent();
            job_list.Sort(sd);

            int[] result = new int[job_list.Count];    //用于存储该日所选工作的代号
            bool[] slot = new bool[job_list.Count];    //用于存储该日是否被占用

            //初始化slot数组
            for (int i = 0; i < job_list.Count; i++)   
                slot[i] = false;

            //两重循环实现贪心算法
            for (int i = 0; i < job_list.Count; i++)
            {
                //根据所选择的工作所用时间，将slot数组被占用区域置为true,将工作序列排序过后的顺序填入result数组
                for (int j = Math.Min(job_list.Count, ((Job)job_list[i]).deadline) - ((Job)job_list[i]).time ; j >= 0; j -= ((Job)job_list[i]).time)   
                {
                    if (slot[j] == false)
                    {
                        for (int m = Math.Min(job_list.Count, ((Job)job_list[i]).deadline) - ((Job)job_list[i]).time; m < Math.Min(job_list.Count, ((Job)job_list[i]).deadline); m++)
                        {
                            result[m] = i;
                            slot[m] = true;
                        }
                        break;
                    }
                }
            }
            //将工作序列打印出来
            for (int i = 0; i < job_list.Count; i++)
                if (slot[i])
                {
                    if (i >= 0)
                    Console.Write(((Job)job_list[result[i]]).id + " ");
                }

        }
    }
    
    public class A
    {
        public static void Main()
        {
            Sequence.Job job1 = new Sequence.Job('a', 4, 100, 2);
            Sequence.Job job2 = new Sequence.Job('b', 1, 19, 1);
            Sequence.Job job3 = new Sequence.Job('c', 2, 27, 1);
            Sequence.Job job4 = new Sequence.Job('d', 1, 25, 1);
            Sequence.Job job5 = new Sequence.Job('e', 3, 15, 1);

            ArrayList job_list = new ArrayList();
            job_list.Add(job1);
            job_list.Add(job2);
            job_list.Add(job3);
            job_list.Add(job4);
            job_list.Add(job5);


            Console.WriteLine("The best working sequence is as follows:");
            Sequence.JobScheduling(job_list);
            Console.WriteLine();
        }
    }
}
