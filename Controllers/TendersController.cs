using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeIhale.Data;
using ProjeIhale.Dtos;
using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjeIhale.Controllers
{
    [Produces("application/json")]
    [Route("api/Tenders")]
    public class TendersController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private DataContext _context;

        public TendersController(IAppRepository appRepository, IMapper mapper, DataContext context)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _context = context;

        }
        public ActionResult GetTenders()
        {
            var tenders = _appRepository.GetTenders();
            var tendersToReturn = _mapper.Map<List<TenderForListDtos>>(tenders);
            return Ok(tendersToReturn);
        }
     

        [HttpGet]
        [Route("status")]
        public ActionResult GetTendersByStatus(string status)
        {
            
            var tenders = _appRepository.GetTendersByStatus();
            var tendersToReturn = _mapper.Map<List<TenderForDetailDto>>(tenders);
            return Ok(tendersToReturn);
        }

        [HttpGet]
        [Route("mytender")]
        public ActionResult GetTendersByAdminId(int adminId)
        {
            var tenders = _appRepository.GetTendersByAdminId(adminId);
            var tendersToResult = _mapper.Map<List<TenderForDetailDto>>(tenders);
            return Ok(tendersToResult);
        }

        [HttpGet]
        [Route("bids")]
        public ActionResult GetBidsByTender(int tenderid)
        {
            var bids = _appRepository.GetBidsByTender(tenderid);
            var bidsToReturn = _mapper.Map<List<BidForListDto>>(bids);
            return Ok(bidsToReturn);
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetTender(int tenderId)
        {
            var tender = _appRepository.GetTenderById(tenderId);
            var tenderToReturn = _mapper.Map<TenderForDetailDto>(tender);
            return Ok(tenderToReturn);

        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Tender tender)
        {
            _appRepository.Add(tender);
            _appRepository.SaveAll();
            return Ok(tender);

        }
     

        [HttpPost]
        [Route("bidadd")]
        public ActionResult AddBid([FromBody]Bid bid)
        {

            _appRepository.Add(bid);
            _appRepository.SaveAll();
            return Ok(bid);
        }
        [HttpPost]
        [Route("compadd")]
        public ActionResult AddComp([FromBody] Complete complete)
        {
            var tender = _context.Tenders.Find(complete.TenderId);
            tender.Status = "Tamamlanmis";
            _context.SaveChanges();
            _appRepository.Add(complete);
            _appRepository.SaveAll();
            return Ok(complete);
        }
        [HttpGet]
        [Route("update")]
        public ActionResult Update([FromBody] Tender tender)
        {
            _appRepository.UpDate(tender);
            _appRepository.SaveAll();
            return Ok(tender);

;
        }

        [HttpGet]
        [Route("cancel")]
        public ActionResult TenderCancel(int tenderId)
        {
            var tender = _context.Tenders.Find(tenderId);
            tender.Status = "İptal";
            _context.SaveChanges();
            return Ok(tender);
        }

        [HttpGet]
        [Route("loseOut")]
        public  ActionResult GetTenderLoseOut(int userId)
            
        {

            var bids = _context.Bids.Include(p => p.Tender).Include(p=>p.Tender.Complete).
                Where(p => p.UserId == userId && p.Tender.Status == "Tamamlanmis" && p.Tender.Complete.UserId != userId).ToList() ;

            return Ok(bids);

          
        }

      [HttpGet]
      [Route("changeStatus/tenderId={tenderId}/status={status}")]
  
        public IActionResult Get(int tenderId, string status)
        {
            var tender = _context.Tenders.Find(tenderId);
            tender.Status = status;
            _context.SaveChanges();
            return Ok(tender);

        }
        

        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete([FromBody] Tender tender)
        {
            _appRepository.Delete(tender);
            _appRepository.SaveAll();
            return Ok(tender);
        }

        [HttpGet]
        [Route("completes")]
        public ActionResult GetCompletes()
        {
            var completes= _appRepository.GetCompletes();
            var completesToReturn = _mapper.Map<List<CompleteForListDto>>(completes);
            return Ok(completesToReturn);
        }
        [HttpGet]
        [Route("mybids")]
        public ActionResult GetBidsByUser(int userId)
        {
            var bids = _appRepository.GetBidsByUser(userId);
            var bidsToReturn = _mapper.Map<List<BidForListDto>>(bids);
            return Ok(bidsToReturn);
        }
        [HttpGet]
        [Route("mycompletes")]
        public ActionResult GetCompletesByUser(int userId)
        {
            var completes = _appRepository.GetCompletesByUser(userId);
            var completesToReturn = _mapper.Map<List<CompleteForListDto>>(completes);
            return Ok(completesToReturn);
        }

    }
}
