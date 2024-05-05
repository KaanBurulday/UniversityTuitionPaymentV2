using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Dto;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class BankAccountTransferController : ControllerBase
    {
        private IBankAccountTransferService _bankAccountTransferService;

        public BankAccountTransferController(IBankAccountTransferService bankAccountTransferService)
        {
            _bankAccountTransferService = bankAccountTransferService;
        }

        [HttpGet]
        public List<BankAccountTransferDto> Get()
        {
            List<BankAccountTransfer> datas = _bankAccountTransferService.Get().ToList();
            List<BankAccountTransferDto> ret = new List<BankAccountTransferDto>();
            datas.ForEach(data => ret.Add(createBankAccountTransferDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public BankAccountTransferDto Get(int id)
        {
            BankAccountTransfer data = _bankAccountTransferService.Get(id);
            return createBankAccountTransferDto(data);
        }

        [HttpPost("InsertBankAccountTransfer")]
        public BankAccountTransferResultDto InsertBankAccountTransfer([FromBody] BankAccountTransferDto bankAccountTransferDto)
        {
            BankAccountTransferResultDto ret = new BankAccountTransferResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int bankAccountTransferId = _bankAccountTransferService.Insert(createBankAccountTransfer(bankAccountTransferDto));
                if (bankAccountTransferId != -1)
                {
                    ret.Id = _bankAccountTransferService.Get(bankAccountTransferId).Id;
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
        public int UpdateBankAccountTransfer([FromBody] BankAccountTransferDto bankAccountTransferDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _bankAccountTransferService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _bankAccountTransferService.Delete(id);
        }

        private BankAccountTransferDto createBankAccountTransferDto(BankAccountTransfer bankAccountTransfer)
        {
            BankAccountTransferDto ret = new BankAccountTransferDto()
            {
                ReceiverCode = bankAccountTransfer.ReceiverCode,
                SenderCode = bankAccountTransfer.SenderCode,
                TransferAmount = bankAccountTransfer.TransferAmount,
                Status = bankAccountTransfer.Status,
                StatusMessage = bankAccountTransfer.StatusMessage,
            };
            return ret;
        }

        private BankAccountTransfer createBankAccountTransfer(BankAccountTransferDto bankAccountTransferDto)
        {
            BankAccountTransfer ret = new BankAccountTransfer()
            {
                ReceiverCode = bankAccountTransferDto.ReceiverCode,
                SenderCode = bankAccountTransferDto.SenderCode,
                TransferAmount = bankAccountTransferDto.TransferAmount,
                Status = bankAccountTransferDto.Status,
                StatusMessage = bankAccountTransferDto.StatusMessage,
            };
            return ret;
        }
    }
}
