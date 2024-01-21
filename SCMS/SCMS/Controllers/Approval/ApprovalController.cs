using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.Controllers.Helper;
using SCMS.DTO;
using SCMS.Models;

namespace SCMS.Controllers.Setup
{
    [ApiController]
    [Route("setup")]
    public class ApprovalController : ControllerBase
    {
        ScmsContext _context;
        public ApprovalController(ScmsContext context)
        {
            _context = context;
        }
        #region Account Approval

        [HttpGet("getaccountapproval")]
        public List<AccountApproval> GetAccountApproval()
        {
            var result = (from o in _context.AccountApprovals select o).ToList();

            return result;
        }


        [HttpPost("accountapproval")]
        public SaveResponse AccountApproval(int id, int approval)
        {
            AccountUser _account = _context.AccountUsers.FirstOrDefault(account => account.Id == id);
            if (_account != null)
            {
                _account.Approve = approval;
                _context.AccountUsers.Update(_account);
                _context.SaveChanges();

                return new SaveResponse
                {
                    StatusCode = "000",
                    Message = "record updated successfully",
                    Session = clsSession.GetLogin(HttpContext.Session),
                };
            }

            return new SaveResponse
            {
                StatusCode = "001",
                Message = "record not found",
                Session = clsSession.GetLogin(HttpContext.Session),
            };

        }



            #endregion Account Approval

            #region Product Approval

            [HttpGet("getproductapproval")]
        public List<ProductApproval> GetProductApproval()
        {
            var result = (from o in _context.ProductApprovals select o).ToList();

            return result;
        }

        [HttpPost("productapproval")]
        public SaveResponse ProductApproval(int id, int approval)
        {
            Product _product = _context.Products.FirstOrDefault(prodcut => prodcut.Id == id);
            if (_product != null)
            {
                _product.Approve = approval;
                _context.Products.Update(_product);
                _context.SaveChanges();

                return new SaveResponse
                {
                    StatusCode = "000",
                    Message = "record updated successfully",
                    Session = clsSession.GetLogin(HttpContext.Session),
                };
            }

            return new SaveResponse
            {
                StatusCode = "001",
                Message = "record not found",
                Session = clsSession.GetLogin(HttpContext.Session),
            };

        }

        #endregion Product Approval

        #region Order Users

        //[HttpGet("getorderapproval")]
        //public List<OrderApproval> GetOrderApproval()
        //{
        //    var result = (from o in _context.OrderApprovals select o).ToList();

        //    return result;
        //}

        #endregion Order Users
    }
}
