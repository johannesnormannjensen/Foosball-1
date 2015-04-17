
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foosball.Models.FoosballClasses;
using Foosball.Models.Repositories;

namespace Foosball.Controllers
{
    public class MatchController : Controller
    {
        IGenericRepository<Match> _matchRepository;
        IGenericRepository<Player> _playerRepository;
        // GET: Match
        public ActionResult Index()
        {
            return View(_matchRepository.All);
        }
        public MatchController(IGenericRepository<Match> matchRepository, IGenericRepository<Player> playerRepository)
        {

            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        // GET: Match/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            ViewBag.PlayerId = new SelectList(_playerRepository.All, "UserId", "UserId");
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        public ActionResult Create(Match match)
        {

            try
            {
                _matchRepository.Insert(match);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Match/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Match/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Match/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
