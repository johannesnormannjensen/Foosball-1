﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Foosball.DAL;
using Foosball.Models.FoosballModels;
using Microsoft.AspNet.Identity;

namespace Foosball.Controllers
{
    public class GamesController : Controller
    {
        private FoosballDbContext db = new FoosballDbContext();
        private int LastGameId { get { return !db.Games.Any() ? 0 : db.Games.ToList().Last().Id; } }

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Location);
            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.PlayerId = new SelectList(db.Players, "Id", "Username");
            var game = new Game();
            return View(game);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationId,Date,Playergames")] Game game)
        {
            
            Player loggedInPlayer = LoggedInPlayer();
            if (game.PlayerGames.Count(x => x.PlayerId == loggedInPlayer.Id)==1)
            {
                foreach (PlayerGame playerGame in game.PlayerGames)
                {
                    playerGame.IsConfirmed = playerGame.PlayerId == loggedInPlayer.Id ? true : (bool?)null;
                }
                game.Date = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Games.Add(game);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(game);
            }
            return RedirectToAction("Create");
        }



        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", game.LocationId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationId,Date")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", game.LocationId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Confirm(int gameId)
        {
            Game game = db.Games.ToList().First(x => x.Id == gameId);
            if (ModelState.IsValid && game.HasThisPlayer(LoggedInPlayer()))
            {
                game.PlayerConfirm(LoggedInPlayer());
                db.SaveChanges();
                return Json(true);
            }

            return Json(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool isUserThisPlayer(Player p)
        {
            return p.ApplicationUserId == User.Identity.GetUserId();
        }
        private Player LoggedInPlayer()
        {
            return db.Players.ToList().First(x => x.ApplicationUserId == User.Identity.GetUserId());
        }
    }
}
