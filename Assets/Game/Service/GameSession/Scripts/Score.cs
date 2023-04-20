using System;

namespace Session
{
    public class Score
    {
        public Action<int> OnChange;
        private int _amount;

        public int Amount => _amount;

        public void Earn (int amount)
        {
            if (amount < 0)
                throw new Exception("Score must be positiove value");
            _amount += amount;
            OnChange?.Invoke(_amount);
        }
    }
}