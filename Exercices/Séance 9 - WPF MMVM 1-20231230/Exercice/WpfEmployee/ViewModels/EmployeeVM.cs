using System.Collections.Generic;
using System.Linq;
using WpfApplication1.ViewModels;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeeVM
    {
        #region fields

        private NorthwindContext dc = new NorthwindContext();
        private List<EmployeeModel> _EmployeesList;
        private List<string> _listTitle;

        #endregion

        public List<EmployeeModel> EmployeesList
        {
            get
            {
                return _EmployeesList = _EmployeesList ?? loadEmployee();
            }
        }

        private List<EmployeeModel> loadEmployee()
        {
            List<EmployeeModel> localCollection = new List<EmployeeModel>();
            foreach (var item in dc.Employees)
            {
                localCollection.Add(new EmployeeModel(item));
            }

            return localCollection;
        }

        public List<string> ListTitle
        {
            get { return _listTitle = _listTitle ?? LoadTitleOfCourtesy(); }
        }

        private List<string> LoadTitleOfCourtesy()
        {
            return dc.Employees.Select(e => e.TitleOfCourtesy).Distinct().ToList();
        }
    }
}
