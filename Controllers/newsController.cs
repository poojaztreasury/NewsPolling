using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPolling.Models;


namespace NewsPolling.Controllers
{
    public class newsController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        Repoter re = new Repoter();
        Logandvote log = new Logandvote();
        IEnumerable<News> es;
        // IEnumerable<Repoter> rep;
        [HttpGet]
        public IActionResult Display(int currentpage = 1, int pagesize = 2 )
        
        {
            es = dal.getNews();
            var pagniationModel = new PaginationModel<News>()
            {
                Count = es.Count(),
                CurrentPage = currentpage,
                Data = es.Skip((currentpage - 1) * pagesize).Take(pagesize),
                PageSize = pagesize
            };

            return View(pagniationModel);
        }

        [HttpGet]
        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        //[Route("api/news/AddNews")]
        public ActionResult AddNews(News e)
        {
            int row = dal.AddNewsInTab(e);
            if (row == 1)
            {
                return RedirectToAction("Display");
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        [Route("vote/{newsid}")]
        public ActionResult Vote(int newsid)
        {
            log = dal.findvote(newsid);
            return View(log);
        }

        [HttpPost]
        [Route("vote/{newsid}")]
        public ActionResult Vote(int newsid, Logandvote l)
        {

            int voteadd = dal.AddVote(newsid);
            if (voteadd == 1)
            {
                return RedirectToAction("Display");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}