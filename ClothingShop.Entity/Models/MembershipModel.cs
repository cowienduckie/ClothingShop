using System;
using System.Collections.Generic;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Models
{
    public class MembershipModel
    {
        public MembershipModel()
        {
            RankList = new List<RankModel>();
            VoucherList = new List<Voucher>();
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? TotalPoint { get; set; }

        public RankModel Rank { get; set; }

        public List<RankModel> RankList { get; set; }

        public List<Voucher> VoucherList { get; set; }
    }
}