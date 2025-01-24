using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure;
using Mango.Services.CouponAPI.Model.DTO;
using AutoMapper;
using Mango.Services.CouponAPI.Models;


namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        ResponseDto _reponse;
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _reponse = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
               IEnumerable<Coupon> objlist = _db.Coupon.ToList();
                _reponse.Result = _mapper.Map<IEnumerable<CouponDto>>(objlist);               
            }
            catch (Exception ex) {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
        [HttpGet]
        [Route("{Id:int}")]
        public ResponseDto Get(int Id)
        {
            try
            {
                Coupon obj = _db.Coupon.First(u => u.CouponId == Id);                
                _reponse.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupon.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _reponse.IsSuccess = false;
                    _reponse.Message = "No Coupon Found";
                }
                _reponse.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
        [HttpPost]        
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Add(obj);
                _db.SaveChanges();
                _reponse.Result = _mapper.Map<CouponDto>(obj);
                _reponse.Message = "Success";

            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Update(obj);
                _db.SaveChanges();
                _reponse.Result = _mapper.Map<CouponDto>(obj);
                _reponse.Message = "Success";

            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupon.First(u => u.CouponId == id);
                _db.Remove(obj);
                _db.SaveChanges();
                _reponse.Result = _mapper.Map<CouponDto>(obj);
                _reponse.Message = "Success";

            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Message = ex.Message;
            }
            return _reponse;
        }
    }
}
