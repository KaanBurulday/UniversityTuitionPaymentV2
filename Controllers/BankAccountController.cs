using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class BankAccountController : ControllerBase
    {
        private IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet]
        public List<BankAccountDto> Get()
        {
            List<BankAccount> datas = _bankAccountService.Get().ToList();
            List<BankAccountDto> ret = new List<BankAccountDto>();
            datas.ForEach(data => ret.Add(createBankAccountDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public BankAccountDto Get(int id)
        {
            BankAccount data = _bankAccountService.Get(id);
            return createBankAccountDto(data);
        }

        [HttpPost("InsertBankAccount")]
        public BankAccountResultDto InsertBankAccount([FromBody] BankAccountDto bankAccountDto)
        {
            BankAccountResultDto ret = new BankAccountResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int bankAccountId = _bankAccountService.Insert(createBankAccount(bankAccountDto));
                if (bankAccountId != -1)
                {
                    ret.Id = _bankAccountService.Get(bankAccountId).Id;
                }
            }
            catch (Exception ex)
            {
                ret.Status = "FAILURE";
                ret.Message = ex.Message;
            }
            return ret;
        }

        [HttpPut("Put")]
        public int UpdateBankAccount([FromBody] BankAccountDto bankAccountDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _bankAccountService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _bankAccountService.Delete(id);
        }

        private BankAccountDto createBankAccountDto(BankAccount bankAccount)
        {
            BankAccountDto ret = new BankAccountDto()
            {
                AccountCode = bankAccount.AccountCode,
                Balance = bankAccount.Balance,
                TCKimlikNo = bankAccount.TCKimlikNo,
                AccountType = bankAccount.AccountType
            };
            return ret;
        }

        private BankAccount createBankAccount(BankAccountDto bankAccountDto)
        {
            BankAccount ret = new BankAccount()
            {
                AccountCode = bankAccountDto.AccountCode,
                Balance = bankAccountDto.Balance,
                TCKimlikNo = bankAccountDto.TCKimlikNo,
                AccountType = bankAccountDto.AccountType
            };
            return ret;
        }
    }
}
