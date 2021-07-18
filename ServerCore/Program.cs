using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static int number = 0;

        static void Thread_1()
        {
            // atomic = 원자성

            for (int i = 0; i < 100000; i++)
            {
                // All or Nothing
                // 먼저 실행되고 있으면 주도권을 갖고 계속 실행
                // 그동안 Decrement는 실행되고 있지 않다.
                Interlocked.Increment(ref number);
                // number++; 아래와 같음
                

                //int temp = number; // 0 
                //temp += 1; // 1 
                //number = temp; // number = 1
            }
        }
        static void Thread_2()
        {
            for (int i = 0; i < 100000; i++)
            {
                Interlocked.Decrement(ref number);
                //int temp = number; // 0 
                //temp -= 1; // -1 
                //number = temp; // number = -1

                //number--;
            }
        }
        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}
