using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        // 1. 근성
        // 2. 양보
        // 3. 갑질

        // 상호배제
        // Monitor
        static object _lock = new object();
        static SpinLock _lock2 = new SpinLock(); // 내부 구현으로는 중간에 한번씩 양보함.
        static Mutex _lock3 = new Mutex();

        // [] [] [] [] [] [] 
        class Reward
        {

        }

        // RWLock ReaderWriteLock
        static ReaderWriterLockSlim _lock4 = new ReaderWriterLockSlim();

        // 99.999999%
        static Reward GetRewardById(int id)
        {
            _lock4.EnterReadLock();

            _lock4.ExitReadLock();

            lock(_lock)
            {

            }
            return null;
        }

        // 0.000001%
        static void AddReward(Reward reward)
        {
            _lock4.EnterWriteLock();

            _lock4.ExitWriteLock();
        }

        static void Main(string[] args)
        {
            lock(_lock)
            {

            }

            bool lockTaken = false;
            try
            {
                _lock2.Enter(ref lockTaken);
            }
            finally
            {
                if(lockTaken)
                _lock2.Exit();
            }
        }
    }
}
