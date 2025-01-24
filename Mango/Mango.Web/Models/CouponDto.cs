
namespace Mango.Web.Models;

    public class CouponDto
    {       
            public int CouponId { get; set; }
            public string CouponCode { get; set; }
            public double DiscountAmount { get; set; }
            public int MinAmount { get; set; }


    //public static implicit operator string(CouponDto v)
    //{
    //    if (v == null)
    //    {
    //        return string.Empty;
    //    }

    //    // Format the string representation of the CouponDto
    //    return $"CouponId: {v.CouponId}, CouponCode: {v.CouponCode}, " +
    //           $"DiscountAmount: {v.DiscountAmount:C}, MinAmount: {v.MinAmount:C}";
    //}
}

