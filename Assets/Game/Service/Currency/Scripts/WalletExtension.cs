namespace Currency
{
    public static class WalletExtension
    {
        public static bool FundsIsEnough (this Wallet wallet, CurrecnyValue value)
        {
            int funds = wallet.GetFunds(value.currency.Key);
            return funds >= value.value;
        }

        public static void SpendFunds (this Wallet wallet, CurrecnyValue value)
        {
            wallet.SpendFunds(value.currency.Key, value.value);
        }
    }
}