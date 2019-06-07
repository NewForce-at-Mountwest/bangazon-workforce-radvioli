using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforce.Controllers
{
    public class TrainingProgramController : Controller
    {
        public TrainingProgramController(IConfiguration config)
        {
            TrainingProgramRepository.SetConfig(config);
        }
        // GET: TrainingProgram
        public ActionResult Index()
        {
            List<TrainingProgram> tpsReport = TrainingProgramRepository.GetTrainingPrograms();
            return View(tpsReport);
        }

        // GET: TrainingProgram/Details/5
        public ActionResult Details(int id)
        {
            TrainingProgram trainingProgram = TrainingProgramRepository.GetOneProgram(id);

            return View(trainingProgram);
        }

        // GET: TrainingProgram/Create
        public ActionResult Create()
        {

            TrainingProgram TrainingProgram = new TrainingProgram();
            return View(TrainingProgram);
        }

        // POST: TrainingProgram/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingProgram model)
        {
            try
            {
                TrainingProgramRepository.CreateTrainingProgram(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingProgram/Edit/5
        public ActionResult Edit(int id)
        {
            TrainingProgram trainingProgram = TrainingProgramRepository.GetOneProgram(id);

            return View(trainingProgram);
        }

        // POST: TrainingProgram/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrainingProgram trainingProgram)
        {
            try
            {
                TrainingProgramRepository.EditProgram(id, trainingProgram);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception taco)
            {
                return View(trainingProgram);
            }
        }
        

        // GET: TrainingProgram/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainingProgram/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}