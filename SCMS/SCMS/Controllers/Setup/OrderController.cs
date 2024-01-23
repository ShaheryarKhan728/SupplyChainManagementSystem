using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.Controllers.Helper;
using SCMS.DTO;
using SCMS.Models;
using SCMS.Requests;

namespace SCMS.Controllers.Setup
{
    [ApiController]
    [Route("setup")]
    public class OrderController : ControllerBase
    {
        ScmsContext _context;
        public OrderController(ScmsContext context)
        {
            _context = context;
        }

        //for admin

        [HttpGet("getallorder")]
        public List<OrderDto> GetAllOrder()
        {
            var result = (from o in _context.Orders
                          //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                          //from a in ProductAttributes.DefaultIfEmpty()
                          //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                          //from b in ProductBulks.DefaultIfEmpty()
                          //where o.OrdersStatus == true && o.Approve == 1
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
        .ToList();

            return result;
        }

        [HttpGet("getallcompletedorder")]
        public List<OrderDto> GetAllCompletedOrder()
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.OrdersStatus == 1
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
        .ToList();

            return result;
        }

        [HttpGet("getallpendingdorder")]
        public List<OrderDto> GetAllPendingOrder()
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.OrdersStatus == 0
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
        .ToList();

            return result;
        }

        [HttpGet("getallcanceldorder")]
        public List<OrderDto> GetAllCancelOrder()
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.OrdersStatus == 2
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
        .ToList();

            return result;
        }

        [HttpGet("getorderbyid")]
        public List<OrderDto> GetOrderById(int id)
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.Id == id
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
        .ToList();

            return result;
        }

        [HttpPost("saveorder")]
        public SaveResponse SaveOrder(OrderRequest orderRequest)
        {
            if (orderRequest != null)
            {

                if (!orderRequest.OrderDetails.IsNullOrEmpty())
                {
                    if (orderRequest.Id == 0)
                    {
                        //Order Save
                        int orderId = (_context.Orders.Max(c => (int?)c.Id) ?? 0) + 1;
                        orderRequest.Id = orderId;
                        orderRequest.Approve = 0;
                        orderRequest.OrdersStatus = 0;
                        //Product _product = product.getProductInstance(product);

                        // Access the returned values
                        var result = orderRequest.getOrderInstance(orderRequest);

                        Order order = result.Item1;
                        ICollection<OrderDetail> orderDetails = result.Item2;
                        
                        //productRequest.Dispose();

                        _context.Orders.Add(order);

                        
                        //Order Details Save
                        int orderDetailId = (_context.OrderDetails.Max(c => (int?)c.Id) ?? 0);
                        foreach (var orderDetail in orderDetails)
                        {
                            orderDetail.OrderId = orderDetailId;
                            orderDetail.Id = orderDetailId + 1;
                        }
                        _context.OrderDetails.AddRange(orderDetails);

                        _context.SaveChanges();

                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "record saved successfully",
                            Session = clsSession.GetLogin(HttpContext.Session),
                        };
                    }
                    else
                    {
                        Order _order = _context.Orders.Find(orderRequest.Id);
                        if (_order != null)
                        {
                            _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(c => c.OrderId == _order.Id));
                            _order.OrderDate = orderRequest.OrderDate;
                            _order.OrderAmount = orderRequest.OrderAmount;
                            _order.OrderQuantity = orderRequest.OrderQuantity;
                            _order.OrdersStatus = orderRequest.OrdersStatus;
                            _order.ModifiedBy = orderRequest.ModifiedBy;
                            _order.ModifiedOn = orderRequest.ModifiedOn;
                            _order.Approve = 0;
                            _order.OrdersStatus = 0;

                            _context.Orders.Update(_order);

                            if (_context.OrderDetails.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid order details",
                                    Session = clsSession.GetLogin(HttpContext.Session),
                                };
                            }
                            _context.OrderDetails.AddRange(orderRequest.OrderDetails);

                            _context.SaveChanges();
                            return new SaveResponse
                            {
                                StatusCode = "000",
                                Message = "Record updated successfully",
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
                }
                return new SaveResponse
                {
                    StatusCode = "002",
                    Message = "invalid order details",
                    Session = clsSession.GetLogin(HttpContext.Session),
                };
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data",
                Session = clsSession.GetLogin(HttpContext.Session),
            };

        }

        [HttpPost("cancelorder")]
        public SaveResponse CancelOrder(int id)
        {
            if (id != null)
            {

                Order _order = _context.Orders.Find(id);
                if (_order != null)
                {
                    _order.OrdersStatus = 2;

                    _context.Orders.Update(_order);

                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "Record updated successfully",
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
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data",
                Session = clsSession.GetLogin(HttpContext.Session),
            };

        }

        [HttpGet("getapproveorderbyretailer")]
        public List<OrderDto> GetApproveProductByRetailer(int id)
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.AccountId == id && o.Approve == 1 && o.OrdersStatus == 0
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
            .ToList();

            return result;
        }

        [HttpGet("getpendingorderbyretailer")]
        public List<OrderDto> GetPendingOrderByRetailer(int id)
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.AccountId == id && o.Approve == 0 && o.OrdersStatus == 0
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
            .ToList();

            return result;
        }

        [HttpGet("getcompletedorderbyseller")]
        public List<OrderDto> GetCompletedOrderBySeller(int id)
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.AccountId == id && o.OrdersStatus == 1
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
            .ToList();

            return result;
        }

        [HttpGet("getcanceldorderbyaccount")]
        public List<OrderDto> GetCancelOrderByAccount(int id)
        {
            var result = (from o in _context.Orders
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.AccountId == id && o.OrdersStatus == 2
                          select new OrderDto
                          {
                              Id = o.Id,
                              AccountId = o.AccountId,
                              OrderNumber = o.OrderNumber,
                              OrderDate = o.OrderDate,
                              OrderAmount = o.OrderAmount,
                              OrderQuantity = o.OrderQuantity,
                              Approve = o.Approve,
                              ApprovedOn = o.ApprovedOn,
                              OrdersStatus = o.OrdersStatus,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              OrderDetails = o.OrderDetails
                          })
            .ToList();

            return result;
        }
    }
}
