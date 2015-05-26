using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Foosball.DAL;
using Foosball.Models.FoosballModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Foosball.Controllers
{
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

            ViewBag.gwl = new SelectList(sortedPlayers.Select(x => x.PlayerGames.Last().Game), "IsWin");
            return View(sortedPlayers);
        }

        // GET: Players
        [AllowAnonymous]
        public ActionResult List()
        {
            var players = db.Players.ToList();

            var sortedPlayers = players.OrderByDescending(x => x.Elo);

            ViewBag.gwl = new SelectList(sortedPlayers.Select(x => x.PlayerGames.Last().Game), "IsWin");
            return View(sortedPlayers);
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Edit([Bind(Include = "Id,Elo,Username,ApplicationUserId")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
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
