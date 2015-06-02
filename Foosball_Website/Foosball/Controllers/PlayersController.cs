using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Foosball.DAL;
using Foosball.Models.FoosballModels;
using Microsoft.AspNet.Identity;

namespace Foosball.Controllers
{
//    Created by Ferenc, Edited by Johannes


    [Authorize]
    public class PlayersController : Controller
    {
        private FoosballDbContext db = new FoosballDbContext();

        // GET: Players
        [AllowAnonymous]
        public ActionResult Index()
        {
            var players = db.Players.ToList();

            var sortedPlayers = players.OrderByDescending(x => x.Elo); 

            return View(sortedPlayers);
        }

        // GET: Players
        [AllowAnonymous]
        public ActionResult ListPlayers()
        {
            List<Player> playersSorted = db.Players.ToList().Where(x=>x.PlayerGames.Count>0).OrderByDescending(x => x.Elo).ToList();
            List<bool> pgs = new List<bool>(playersSorted.Select(x => x.PlayerGames.Last().IsWin));
            return View(db.Players.ToList());
        }


        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {

                Player p = db.Players.ToList().First(x => x.ApplicationUserId == User.Identity.GetUserId());
                return View(p);
            }

            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Elo,Username")] Player player)
        {

            player.ApplicationUserId = User.Identity.GetUserId();
            if (db.Players.Any(playerFromDb => playerFromDb.ApplicationUserId == player.ApplicationUserId))
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult MyProfile()
        {
            Player p = db.Players.ToList().First(x => x.ApplicationUserId == User.Identity.GetUserId());
            var userId = User.Identity.GetUserId();
            Player player = db.Players.SingleOrDefault(x => x.ApplicationUserId==userId);

            int[] gwl = {0,0,0};
            if (player != null)
            {
                foreach (var playergame in player.PlayerGames)
                {
                    if (!playergame.Game.IsConfirmed()) continue;
                    gwl[0]++;
                    if (playergame.IsWin)
                    {
                        gwl[1]++;
                    }
                    else
                    {
                        gwl[2]++;
                    }
                }

                ViewBag.player = player;
            }
            ViewBag.gwl = gwl;

            return View(p);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username")] Player player)
        {
            if (ModelState.IsValid)
            {
                Player p = db.Players.ToList().First(x => x.ApplicationUserId == User.Identity.GetUserId());
                p.Username = player.Username;
                player = p;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
