using MicroBiz.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroBiz
{
    public class Utilities
    {
        //private static CustomerDbContext db = new CustomerDbContext();
        public static Customer CustomerAdd(Customer customer)
        {
            if (customer != null)
            {
                using(CustomerDbContext db = new CustomerDbContext())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
            }

            return customer;
        }

        public static IEnumerable<SelectListItem> States
        {
            get
            {

                List<SelectListItem> _States = new List<SelectListItem>();

                _States.Add(new SelectListItem { Text = "Select", Value = "", Selected = false });
                _States.Add(new SelectListItem { Text = "Arizona", Value = "AZ", Selected = false });
                _States.Add(new SelectListItem { Text = "California", Value = "CA", Selected = false });
                _States.Add(new SelectListItem { Text = "Texas", Value = "TX", Selected = false });
                _States.Add(new SelectListItem { Text = "Washington", Value = "WA", Selected = false });
                _States.Add(new SelectListItem { Text = "Iowa", Value = "IO", Selected = false });

                /*

                foreach (Industry industry in Enum.GetValues(typeof(Industry)))
                {

                    Industries.Add(new SelectListItem { Text = industry.ToString(), Value = industry.ToString(), Selected = false });

                }
                */


                return _States.AsEnumerable();

            }

        }
        public static IEnumerable<SelectListItem> StatesList
        {
            get
            {

                List<SelectListItem> _StatesList = new List<SelectListItem>();
                _StatesList.Add(new SelectListItem { Text = "Select", Value = "", Selected = false });

                using(StateDbContext db = new StateDbContext())
                {
                    var states = db.States;

                    foreach (State _state in states)
                    {

                        _StatesList.Add(new SelectListItem { Text = _state.Name.ToString(), Value = _state.Code.ToString(), Selected = false });

                    }
                }
                
                return _StatesList.AsEnumerable();

            }

        }
        public static IEnumerable<SelectListItem> CountryList
        {
            get
            {

                List<SelectListItem> _CountryList = new List<SelectListItem>();
                _CountryList.Add(new SelectListItem { Text = "Select", Value = "", Selected = false });

                using(CountryDbContext db = new CountryDbContext())
                {
                    var countries = db.Countries;

                    foreach (Country _country in countries)
                    {

                        _CountryList.Add(new SelectListItem { Text = _country.Name.ToString(), Value = _country.Code.ToString(), Selected = false });

                    }
                }
                
                
                return _CountryList.AsEnumerable();

            }

        }

        public static IEnumerable<SelectListItem> CategoryList
        {
            get
            {

                List<SelectListItem> _CategoryList = new List<SelectListItem>();
                _CategoryList.Add(new SelectListItem { Text = "Select", Value = "", Selected = false });

                using(CategoryDbContext db = new CategoryDbContext())
                {
                    var categories = db.Categories;

                    foreach (Category _Category in categories)
                    {

                        _CategoryList.Add(new SelectListItem { Text = _Category.Description.ToString(), Value = _Category.Id.ToString(), Selected = false });

                    }
                }
                
                return _CategoryList.AsEnumerable();

            }

        }
        public static IEnumerable<SelectListItem> SubCategoryList
        {
            get
            {
                List<SelectListItem> _SubCategoryList = new List<SelectListItem>();
                _SubCategoryList.Add(new SelectListItem { Text = "Select", Value = "", Selected = false });

                using(SubCategoryDbContext db = new SubCategoryDbContext())
                {
                    var subcategories = db.SubCategories;

                    foreach (SubCategory _subCategory in subcategories)
                    {

                        _SubCategoryList.Add(new SelectListItem { Text = _subCategory.Description.ToString(), Value = _subCategory.Id.ToString(), Selected = false });

                    }
                }                

                return _SubCategoryList.AsEnumerable();

            }

        }
    }
    
}