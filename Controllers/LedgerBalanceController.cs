using LedgerBalanceService.Dtos;
using LedgerBalanceService.Interfaces;
using LedgerBalanceService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LedgerBalanceService.Controllers
{
    [Route("api/ledger")]
    [ApiController]
    public class LedgerBalanceController : ControllerBase
    {
        private readonly ILedgerService _ledgerService;
        public LedgerBalanceController(ILedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        [HttpPost]
        [Route("add-balance")]
        public IActionResult AddBalance([FromBody]AddBalanceDto addBalanceDto)
        {
            LedgerBalance ledgerBalance = new LedgerBalance(addBalanceDto.ClientId, addBalanceDto.Balance);

            _ledgerService.AddLedgerBalance(ledgerBalance);
            return Ok(ledgerBalance);
        }

        [HttpGet]
        [Route("get-balance/{clientId}")]
        public IActionResult GetBalance(long clientId)
        {
            var balance = _ledgerService.GetLedgerBalance(clientId);
            return Ok("Balance: " + balance);

        }

        [HttpGet]
        [Route("get-chain")]
        public IActionResult GetChain()
        {
            var chain = _ledgerService.PrintLedgerBlockChain();
            return Ok(chain);

        }
    }
}