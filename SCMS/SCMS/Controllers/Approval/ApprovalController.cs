using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        #endregion Account Approval

        #region Product Approval

        [HttpGet("getproductapproval")]
        public List<ProductApproval> GetProductApproval()
        {
            var result = (from o in _context.ProductApprovals select o).ToList();

            return result;
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
