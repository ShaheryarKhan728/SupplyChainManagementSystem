using Microsoft.AspNetCore.Mvc;
using SCMS.DTO;
using SCMS.Models;

namespace SCMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetupController : ControllerBase
    {
        ScmsContext _context;
        public SetupController(ScmsContext context)
        {
            _context = context;
        }

        #region Category
        [HttpGet("getallcategory")]
        public List<CategoryDto> GetAllCategory()
        {
            var result = (from o in _context.Categories
                    join a in _context.Categories on o.Id equals a.CategoryHeaderId
                    join b in _context.CategoryTypes on o.CategoryTypeId equals b.Id
                    select new CategoryDto
                    {
            Id = o.Id,
            CategoryName = o.CategoryName,
            CategoryMargin = o.CategoryMargin,
            CategoryHeaderId = o.CategoryHeaderId,
            CategoryTypeId = o.CategoryTypeId,
            CategoryHeaderName = a.CategoryName,
            CategoryTypeName = b.CategoryTypeName
        })
        .ToList();

            return result;
        }

        [HttpPost("savecategory")]
        public SaveResponse SaveCategory(Category category)
        {
            if (category != null)
            {
                if (category.Id == 0)
                {
                    int id = (_context.Categories.Max(c => (int?)c.Id) ?? 0) + 1;
                    category.Id = id;
                    _context.Categories.Add(category);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    Category _category = _context.Categories.Find(category.Id);
                    if (_category != null)
                    {
                        _category.Id = category.Id;
                        _category.CategoryName = category.CategoryName;
                        _category.CategoryMargin = category.CategoryMargin;
                        _category.CategoryHeaderId = category.CategoryHeaderId;
                        _category.CategoryTypeId = category.CategoryTypeId;
                        _category.Status = category.Status;
                        _category.ModifiedBy = category.ModifiedBy;
                        _category.ModifiedOn = category.ModifiedOn;

                        _context.Categories.Update(_category);
                        _context.SaveChanges();
                        return new SaveResponse
                        {
                            Message = "Record updated successfully"
                        };
                    }
                    return new SaveResponse
                    {
                        Message = "Unable to update record"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deletecategory")]
        public SaveResponse DeleteCategory(int id)
        {
            if (id > 0)
            {
                Category category = _context.Categories.Find(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
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

        #endregion


        #region Category Type

        [HttpGet("getallcategorytype")]
        public List<CategoryTypeDto> GetAllCategoryType()
        {
            var result = (from o in _context.CategoryTypes
                          join a in _context.CategoryTypes on o.Id equals a.CategoryTypeHeaderId into joinedData
                          from a in joinedData.DefaultIfEmpty()
                          select new CategoryTypeDto
                          {
                              Id = o.Id,
                              CategoryTypeName = o.CategoryTypeName,
                              CategoryTypeHeaderId = o.CategoryTypeHeaderId,
                              CategoryTypeHeaderName = a.CategoryTypeName ?? null,
                          })
        .ToList();

            return result;
        }

        [HttpPost("savecategorytype")]
        public SaveResponse SaveCategoryType(CategoryType categoryType)
        {
            if (categoryType != null)
            {
                if (categoryType.Id == 0)
                {
                    int id = (_context.CategoryTypes.Max(c => (int?)c.Id) ?? 0) + 1;
                    categoryType.Id = id;
                    _context.CategoryTypes.Add(categoryType);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    CategoryType _categoryType = _context.CategoryTypes.Find(categoryType.Id);
                    if (_categoryType != null)
                    {
                        _categoryType.Id = categoryType.Id;
                        _categoryType.CategoryTypeName = categoryType.CategoryTypeName;
                        _categoryType.CategoryTypeHeaderId = categoryType.CategoryTypeHeaderId;
                        _categoryType.ModifiedBy = categoryType.ModifiedBy;
                        _categoryType.ModifiedOn = categoryType.ModifiedOn;

                        _context.CategoryTypes.Update(_categoryType);
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

        [HttpPost("deletecategorytype")]
        public SaveResponse DeleteCategoryType(int id)
        {
            if (id > 0)
            {
                CategoryType categoryType = _context.CategoryTypes.Find(id);
                if (categoryType != null)
                {
                    _context.CategoryTypes.Remove(categoryType);
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

        #endregion Category Type

        #region Accounts

        [HttpGet("getallaccounts")]
        public List<AccountsDto> GetAllAccounts()
        {
            var result = (from o in _context.Accounts
                          join a in _context.AccountTypes on o.AccountType equals a.Id into joinedData
                          from a in joinedData.DefaultIfEmpty()
                          select new AccountsDto
                          {
                              Id = o.Id,
                              UserId = o.UserId,
                              Username = o.Username,
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
                        _account.Username = account.Username;
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
                    _context.Accounts.Remove(account);
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

        [HttpGet("getallaccountusers")]
        public List<AccountUsersDto> GetAllAccountUsers()
        {
            var result = (from o in _context.AccountUsers
                          join a in _context.AccountTypes on o.UserAccountType equals a.Id into joinedData
                          from a in joinedData.DefaultIfEmpty()
                          select new AccountUsersDto
                          {
                              Id = o.Id,
                              UserId = o.UserId,
                              UserName = o.UserName,
                              UserAccountType = o.Id,
                              UserAddress = o.UserAddress,
                              UserEmail = o.UserEmail,
                              UserPhone1 = o.UserPhone1,
                              UserPhone2 = o.UserPhone2,
                              UserCnic = o.UserCnic,
                              UserArea = o.UserArea,
                              UserCity = o.UserCity,
                              UserRating = o.UserRating,
                              UserReturnRate = o.UserReturnRate,
                              UserAccountTypeName = a.AccountTypeName,
                          })
        .ToList();

            return result;
        }

        [HttpPost("saveaccountusers")]
        public SaveResponse SaveAccountUsers(AccountUser accountUsers)
        {
            if (accountUsers != null)
            {
                if (accountUsers.Id == 0)
                {
                    int id = (_context.AccountTypes.Max(c => (int?)c.Id) ?? 0) + 1;
                    accountUsers.Id = id;
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
                        _accountUsers.UserId = accountUsers.UserId;
                        _accountUsers.UserName = accountUsers.UserName;
                        _accountUsers.UserAccountType = accountUsers.UserAccountType;
                        _accountUsers.UserAddress = accountUsers.UserAddress;
                        _accountUsers.UserEmail = accountUsers.UserEmail;
                        _accountUsers.UserPhone1 = accountUsers.UserPhone1;
                        _accountUsers.UserPhone2 = accountUsers.UserPhone2;
                        _accountUsers.UserCnic = accountUsers.UserCnic;
                        _accountUsers.UserArea = accountUsers.UserArea;
                        _accountUsers.UserCity = accountUsers.UserCity;
                        _accountUsers.UserRating = accountUsers.UserRating;
                        _accountUsers.UserReturnRate = accountUsers.UserReturnRate;

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

        [HttpPost("deleteaccountusers")]
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
