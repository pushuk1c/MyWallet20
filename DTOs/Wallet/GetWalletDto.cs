﻿namespace MyWallet.DTOs.Wallet
{
    public class GetWalletDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
    }
}
