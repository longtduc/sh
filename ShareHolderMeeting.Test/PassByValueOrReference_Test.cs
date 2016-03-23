using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class PassByValueOrReference_Test
    {
        [TestMethod]
        public void ByReference()
        {
            var s = new Student() { Id = 1, Name = "Khanh" };
            StudentServices.ChangeStudent(s);
            Assert.AreEqual("New Name", s.Name);
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }


    }

    class StudentServices
    {
        public static void ChangeStudent(Student s)
        {
            s.Name = "New Name";
            s.Id = 100;
        }
    }
}
