using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        // 전역 변수는 메인과 쓰레드에서 동시에 접근 가능.
        static bool _stop = false;

        static void ThreadMain()
        {
            Console.WriteLine("쓰레드 시작 !");

            while (_stop == false)
            { 
                // 누군가 stop 신호를 해주기를 기다린다.
            }
            
            Console.WriteLine("쓰레드 종료 !");
        }

        static void Main(string[] args)
        {
            Task t = new Task(ThreadMain);
            t.Start();

            Thread.Sleep(1000); // 1초 대기

            _stop = true;

            Console.WriteLine("Stop 호출");
            Console.WriteLine("종료 대기중");
            t.Wait(); // 쓰레드가 끝남을 알림.
            Console.WriteLine("종료 성공");
        }
    }
}
