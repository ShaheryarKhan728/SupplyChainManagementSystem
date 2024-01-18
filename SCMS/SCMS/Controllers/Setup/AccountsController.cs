using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.DTO;
using SCMS.Models;

namespace SCMS.Controllers.Setup
{
    [ApiController]
    [Route("setup")]
    public class AccountsController : ControllerBase
    {
        ScmsContext _context;
        public AccountsController(ScmsContext context)
        {
            _context = context;
        }
        #region Accounts

        [HttpGet("getallaccounts")]
        public List<AccountsDto> GetAllAccounts()
        {
            var result = (from o in _context.Accounts
                          join a in _context.AccountTypes on o.AccountType equals a.Id into joinedData
                          from a in joinedData.DefaultIfEmpty()
                          where o.Status == true
                          select new AccountsDto
                          {
                              Id = o.Id,
                              UserId = o.UserId,
                              UserEmail = o.UserEmail,
                              UserPassword = o.UserPassword,
                              CreatedOn = o.CreatedOn,
                              AccountType = o.AccountType,
                              AccountTypeName = a.AccountTypeName
                          })
        .ToList();

            return result;
        }

        [HttpPost("saveaccount")]
        public SaveResponse SaveAccounts(Account account)
        {
            if (account != null)
            {
                if (account.Id == 0)
                {
                    int id = (_context.Accounts.Max(c => (int?)c.Id) ?? 0) + 1;
                    account.Id = id;
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    Account _account = _context.Accounts.Find(account.Id);
                    if (_account != null)
                    {
                        _account.Id = account.Id;
                        _account.UserId = account.UserId;
                        _account.UserPassword = account.UserPassword;
                        _account.UserEmail = account.UserEmail;
                        _account.AccountType = account.AccountType;

                        _context.Accounts.Update(_account);
                        _context.SaveChanges();
                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "Record updated successfully"
                        };
                    }
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deleteaccount")]
        public SaveResponse DeleteAccounts(int id)
        {
            if (id > 0)
            {
                Account account = _context.Accounts.Find(id);
                if (account != null)
                {
                    account.Status = false;
                    _context.Accounts.Update(account);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record deleted successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid id"
            };

        }

        [HttpPost("login")]
        public SaveResponse Login(String userId, String password)
        {
            if (userId.IsNullOrEmpty() && password.IsNullOrEmpty())
            {
                Account account = _context.Accounts.FirstOrDefault(account => account.UserId == userId && account.UserPassword == password
                                    && account.Status == true);

                if (account != null)
                {
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "login successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "login error"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid userId or password"
            };

        }

        #endregion Accounts

        #region Account Type

        [HttpGet("getallaccounttype")]
        public List<AccountType> GetAllAccountType()
        {
            var result = (from o in _context.AccountTypes
                          select o)
        .ToList();

            return result;
        }

        [HttpPost("saveaccounttype")]
        public SaveResponse SaveAccountType(AccountType accountType)
        {
            if (accountType != null)
            {
                if (accountType.Id == 0)
                {
                    int id = (_context.AccountTypes.Max(c => (int?)c.Id) ?? 0) + 1;
                    accountType.Id = id;
                    _context.AccountTypes.Add(accountType);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    AccountType _accountType = _context.AccountTypes.Find(accountType.Id);
                    if (_accountType != null)
                    {
                        _accountType.Id = accountType.Id;
                        _accountType.AccountTypeName = accountType.AccountTypeName;

                        _context.AccountTypes.Update(_accountType);
                        _context.SaveChanges();
                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "Record updated successfully"
                        };
                    }
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deleteaccounttype")]
        public SaveResponse DeleteAccountType(int id)
        {
            if (id > 0)
            {
                AccountType accountType = _context.AccountTypes.Find(id);
                if (accountType != null)
                {
                    _context.AccountTypes.Remove(accountType);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record deleted successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid id"
            };

        }

        #endregion Account Type

        #region Account Users

        //[HttpGet("getallaccountusers")]
        //public List<AccountUsersDto> GetAllAccountUsers()
        //{
        //    var result = (from o in _context.AccountUsers
        //                  select new AccountUsersDto
        //                  {
        //                      Id = o.Id,
        //                      UserId = o.UserId,
        //                      UserName = o.UserName,
        //                      UserAccountType = o.Id,
        //                      UserAddress = o.UserAddress,
        //                      UserEmail = o.UserEmail,
        //                      UserPhone1 = o.UserPhone1,
        //                      UserPhone2 = o.UserPhone2,
        //                      UserCnic = o.UserCnic,
        //                      UserArea = o.UserArea,
        //                      UserCity = o.UserCity,
        //                      UserRating = o.UserRating,
        //                      UserReturnRate = o.UserReturnRate,
        //                      UserAccountTypeName = a.AccountTypeName,
        //                  })
        //.ToList();

        //    return result;
        //}

        [HttpGet("getprofilebyid")]
        public List<AccountUsersDto> GetAccountUsersById(int id)
        {
            var result = (from o in _context.AccountUsers
                          join a in _context.Accounts on o.AccountId equals a.Id into accounts
                          from a in accounts.DefaultIfEmpty()
                          join b in _context.AccountTypes on o.AccountId equals b.Id into accounttype
                          from b in accounttype.DefaultIfEmpty()
                          where o.Id == id && o.Status == true
                          select new AccountUsersDto
                          {
                              Id = o.Id,
                              Username = o.Username,
                              UserAddress = o.UserAddress,
                              UserPhone1 = o.UserPhone1,
                              UserPhone2 = o.UserPhone2,
                              UserCnic = o.UserCnic,
                              UserArea = o.UserArea,
                              UserCity = o.UserCity,
                              UserRating = o.UserRating,
                              UserReturnRate = o.UserReturnRate,
                              CreatedOn = o.CreatedOn,
                              Approve = o.Approve,
                              UserAccountTypeName = b.AccountTypeName,
                          })
        .ToList();

            return result;
        }

        [HttpPost("saveprofile")]
        public SaveResponse SaveAccountUsers(AccountUser accountUsers)
        {
            if (accountUsers != null)
            {
                if (accountUsers.Id == 0)
                {
                    int id = (_context.AccountUsers.Max(c => (int?)c.Id) ?? 0) + 1;
                    accountUsers.Id = id;
                    accountUsers.Approve = 0;
                    _context.AccountUsers.Add(accountUsers);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    AccountUser _accountUsers = _context.AccountUsers.Find(accountUsers.Id);
                    if (_accountUsers != null)
                    {
                        _accountUsers.Id = accountUsers.Id;
                        _accountUsers.Username = accountUsers.Username;
                        _accountUsers.UserAddress = accountUsers.UserAddress;
                        _accountUsers.UserPhone1 = accountUsers.UserPhone1;
                        _accountUsers.UserPhone2 = accountUsers.UserPhone2;
                        _accountUsers.UserCnic = accountUsers.UserCnic;
                        _accountUsers.UserArea = accountUsers.UserArea;
                        _accountUsers.UserCity = accountUsers.UserCity;
                        _accountUsers.UserRating = accountUsers.UserRating;
                        _accountUsers.UserReturnRate = accountUsers.UserReturnRate;
                        _accountUsers.Approve = 0;

                        _context.AccountUsers.Update(_accountUsers);
                        _context.SaveChanges();
                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "Record updated successfully"
                        };
                    }
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deleteprofile")]
        public SaveResponse DeleteAccountUsers(int id)
        {
            if (id > 0)
            {
                AccountUser accountUsers = _context.AccountUsers.Find(id);
                if (accountUsers != null)
                {
                    _context.AccountUsers.Remove(accountUsers);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record deleted successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid id"
            };

        }

        #endregion Account Users
    }
}
