using CumulativeProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject1.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CumulativeProject1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
           
            

            return View(NewTeacher);
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();

        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(int TeacherId, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            if (TeacherId == null)
            {
                return RedirectToAction("New");
            }
            if (TeacherFname == null)
            {
                return RedirectToAction("New");
            }
            if (TeacherLname == null)
            {
                return RedirectToAction("New");
            }
            if (EmployeeNumber == null)
            {
                return RedirectToAction("New");
            }
            if (HireDate == null)
            {
                return RedirectToAction("New");
            }
            if (Salary == null)
            {
                return RedirectToAction("New");
            }

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherId = TeacherId;
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);


            return RedirectToAction("List");
            
            
               
        }

        //GET:/Teacher/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            Teacher SelectedTeacher = MyController.FindTeacher(id);


            //Views/Teacher/Edit.cshtml
            return View(SelectedTeacher);
        }

        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
        

        
    }
}