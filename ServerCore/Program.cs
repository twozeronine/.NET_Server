﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Lock
    {
        // bool <= 커널에서 관리하는 변수
        AutoResetEvent _available = new AutoResetEvent(true);

        public void Acquire()
        {
            _available.WaitOne(); // 입장 시도 // 자동으로 Reset 해줌
            //_available.Reset(); // bool = false 
        }
        public void Release()
        {
            _available.Set(); // flag = true
        }
    }

    class Program
    {
        static int _num = 0;
        static Lock _lock = new Lock();
        static void Thread_1()
        {
            for(int i=0; i<10000; i++)
            {
                _lock.Acquire();
                _num++;
                _lock.Release();
            }
        }

        static void Thread_2()
        {
            for (int i = 0; i < 10000; i++)
            {
                _lock.Acquire();
                _num--;
                _lock.Release();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(_num);
        }
    }
}
